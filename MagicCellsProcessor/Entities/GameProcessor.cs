using MagicCellsProcessor.Entities.Algorithms;
using MagicCellsProcessor.Entities.FieldParts;
using MagicCellsProcessor.Entities.SpellParts;
using MagicCellsProcessor.Entities.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MagicCellsProcessor.Entities.Logging;

namespace MagicCellsProcessor.Entities
{
	public class GameProcessor
	{
		private static volatile GameProcessor instance;
		private static object syncRoot = new Object();

		private GameProcessor()
		{
			actionsOrderList = new List<Action>()
			{
				CalculateDistances,
				MoveCells,
				CalculateImpacts,
				SaveStep,
				ProcessCells,
				SaveStep,
                EndStep
			};

            printer = new JsonGroupPrinter();
		}

		public static GameProcessor Instance
		{
			get
			{
				if ( instance == null )
				{
					lock ( syncRoot )
					{
						if ( instance == null )
							instance = new GameProcessor();
					}
				}

				return instance;
			}
		}

		public Field Field
		{
			get
			{
				return field;
			}
		}

        public Logger Logger
        {
            get
            {
                return logger;
            }
        }

        private List<Action> actionsOrderList;

		private Field field;

		private Algorithms.DistancesInfoStorage distanceInfoStorage;

		private int stepsCounter = 0;

		private const int MAX_STEPS = 100;

        private Logger logger = new Logger();

        private ILogPrinter printer;

		private void MoveCells()
		{
			#region old
			/*
			var moveCompleteList = new List<TestSpellPart>();

			// сортируем всё так, чтобы первыми двигались те, кто ближе к врагу
			var allSpellPartsSorted = field.AllSpellParts;
			allSpellPartsSorted.Sort( (sp1, sp2) => sp1.NearestEnemyInfo.distance.CompareTo( sp2.NearestEnemyInfo.distance ) );

			// двигаем все клетки
			foreach ( var spellPart in allSpellPartsSorted )
			{
				// если в радиусе клетки есть враг - двигаться никуда не надо, дерёмся
				if ( spellPart.GetEnemyNeighborsSpellParts().Count != 0 )
					continue;

				// если туда ещё никто не пошёл и там никого нет, идём 
				if ( moveCompleteList.Find( x => x.IndexToMove == spellPart.IndexToMove ) == null && field.GetCellByIndex( spellPart.IndexToMove ).SpellPart == null )
				{
					Cell destination = field.GetCellByIndex( spellPart.IndexToMove );
					spellPart.MoveTo( destination );
					moveCompleteList.Add( spellPart );
				}
				// если туда уже кто-то пошёл или там кто-то есть - танцы с бубном
				else
				{
					var neighborsCells = spellPart.CurrentCell.GetNeighborsCells();

					float minDistance = CellsConstants.CELL_WEIGHT_INFINITY;
					int indexToGo = -1;

					foreach ( var neighborCell in neighborsCells )
					{
						int neigborIndex = Utils.ConvertVector2ToOne( neighborCell.Position, field.Cells.Count );
						if ( distanceInfoStorage.distances[ neigborIndex ][ spellPart.NearestEnemyInfo.nearestEnemy.CurrentCell.Index ] < minDistance && 
							// если оттуда до цели можно дойти лучше чем есть сейчас 
							 neighborCell.SpellPart == null )// и там нет клетки 
						{
							minDistance = distanceInfoStorage.distances[ neigborIndex ][ spellPart.NearestEnemyInfo.nearestEnemy.CurrentCell.Index ];
							indexToGo = neigborIndex;
						}
					}

					// этой клетке лучше постоять на месте - двигаться некуда
					if ( indexToGo == -1 )
						continue;

					// а этой лучше подвигаться в найденное место
					spellPart.IndexToMove = indexToGo;
					Cell destination = field.GetCellByIndex( indexToGo );
					spellPart.MoveTo( destination );
					moveCompleteList.Add( spellPart );
				}
			}
			*/
			#endregion

			// сортируем всё так, чтобы первыми двигались те, кто ближе к врагу
			var allSpellPartsSorted = field.AllSpellParts;
			allSpellPartsSorted.Sort( ( sp1, sp2 ) => sp1.NearestEnemyInfo.distance.CompareTo( sp2.NearestEnemyInfo.distance ) );
			//allSpellPartsSorted.Sort( ( sp1, sp2 ) => sp1.PlayerOwner.Id.CompareTo( sp2.PlayerOwner.Id ) );


			// двигаем все клетки
			foreach ( var spellPart in allSpellPartsSorted )
			{
				Cell destination = spellPart.ChooseCellToMove();

				if ( destination != null )
					spellPart.MoveTo( destination );

                // log all alive spellparts
                logger.LogSpellPart( spellPart.Id, spellPart.PlayerOwner.Id, spellPart.HealthPoints.CurrentHp, spellPart.CurrentCell.Position );
			}
			
			// for test
			foreach ( var spellPart in field.AllSpellParts )
				if ( spellPart.CurrentCell.SpellPart != spellPart )
					Console.WriteLine( "fuck" );
		}

		private void CalculateImpacts()
		{
			// сначала мы очищаем все влияния, которые были
			foreach ( var spellPart in field.AllSpellParts )
			{
				spellPart.AdditionalParameters.ResetValues();
			}

			// потом раздаём новые настройки влияний
			foreach ( var spellPart in field.AllSpellParts )
			{
				var neighborsAllies = spellPart.GetAlliedNeighborsSpellParts();
				foreach ( var ally in neighborsAllies )
				{
					ally.AdditionalParameters += AdditionalParametersFactory.GetAdditionalParametersForMe( spellPart );
				}
			}
		}

		private void ProcessCells()
		{
			// all attacks and actions
			foreach ( var spellPart in field.AllSpellParts )
			{
				spellPart.DoSelf();
			}

			// check to delete died
			foreach ( var spellPart in field.AllSpellParts )
			{
				if ( spellPart.HealthPoints.CurrentHp == 0 )
				{
                    // log death of the spellpart
                    logger.LogAction( new ActionTypeDie( spellPart.Id ) );

					spellPart.Die();
				}
			}
		}

		private void CalculateDistances()
		{
			#region old
			/*
			distanceInfoStorage = Algorithms.FindAllPathsOnField.GetAllPaths( field );

			Console.WriteLine( field.AllSpellParts.Count );

			foreach ( var spellPart in field.AllSpellParts )
			{
				var allMyEnemiesIndexes = new List<int>();

				foreach ( var spellPart2 in field.AllSpellParts )
					if ( spellPart2.PlayerOwner.Id != spellPart.PlayerOwner.Id )
						allMyEnemiesIndexes.Add( Utils.ConvertVector2ToOne( spellPart2.CurrentCell.Position, field.Cells.Count ) );

				float minDistance = CellsConstants.CELL_WEIGHT_INFINITY;
				int indexToMove = -1;

				int myIndex = Utils.ConvertVector2ToOne( spellPart.CurrentCell.Position, field.Cells.Count );

				// ищем ближайшего врага, и собираемся двигаться в его сторону
				foreach ( var index in allMyEnemiesIndexes )
				{
					if ( distanceInfoStorage.distances[ myIndex ][ index ] < minDistance )
					{
						minDistance = distanceInfoStorage.distances[ myIndex ][ index ];
						indexToMove = Algorithms.FindAllPathsOnField.GetFirstWayIndex( myIndex, index, distanceInfoStorage.paths );//distanceInfoStorage.paths[ myIndex ][ index ];

						spellPart.NearestEnemyInfo = new NearestEnemyInfo( field.GetSpellPartByIndex( index ), minDistance );
					}
				}

				spellPart.IndexToMove = indexToMove;
			}
			*/
			#endregion

			Console.WriteLine( field.AllSpellParts.Count );


			foreach ( var spellPart in field.AllSpellParts )
			{
				spellPart.DoPremoveCalculations();
			}

		}

        private void EndStep()
        {
            logger.PrintState( printer );

            logger.NextTurn();
        }

        private void SaveBaseInfo()
		{
			var filename = ConfigurationManager.AppSettings[ "workFile" ];
			StreamWriter wr = new StreamWriter( filename, false);
			wr.WriteLine( field.Cells.Count );
			wr.WriteLine( field.Cells.Count );
			wr.Close();
		}

		private void SaveStep()
		{
            #region old save step
            {
                var filename = ConfigurationManager.AppSettings["workFile"];

                StreamWriter wr = new StreamWriter(filename, true);

                //wr.WriteLine();
                //wr.WriteLine();
                //wr.WriteLine();


                wr.WriteLine( /*"SpellParts alive: " +*/ field.AllSpellParts.Count);

                foreach (var spellPart in field.AllSpellParts)
                {
                    wr.WriteLine(spellPart.PlayerOwner.Id);
                    wr.WriteLine( /*"position of cell: " +*/ spellPart.CurrentCell.Position);
                    wr.WriteLine( /*"hp of cell: " + */spellPart.HealthPoints.CurrentHp);
                    //wr.WriteLine();

                }

                wr.Close();
            }
            #endregion

            //#region new save step
            //{
            //    var filename = ConfigurationManager.AppSettings["outputFileForPlayer"];

            //    StreamWriter wr = new StreamWriter(filename, true);

            //    //wr.WriteLine();
            //    //wr.WriteLine();
            //    //wr.WriteLine();


            //    wr.WriteLine( /*"SpellParts alive: " +*/ field.AllSpellParts.Count);

            //    foreach (var spellPart in field.AllSpellParts)
            //    {
            //        wr.WriteLine(spellPart.Id);
            //        wr.WriteLine(spellPart.PlayerOwner.Id);
            //        wr.WriteLine( /*"position of cell: " +*/ spellPart.CurrentCell.Position);
            //        wr.WriteLine( /*"hp of cell: " + */spellPart.HealthPoints.CurrentHp);
            //        //wr.WriteLine();

            //    }

            //    wr.Close();
            //}
            //#endregion
        }

        private void SaveEnd()
		{
            //var winner = GetWinner();
            //StreamWriter wr = new StreamWriter( @"h:\cellsTest.txt", true );
            //wr.WriteLine( "Winner is " + ( (winner != null) ? " Player " + winner.Id.ToString() : "no one") );
            //wr.WriteLine( "End" );
            //wr.Close();

            (printer as JsonGroupPrinter).PrintAll();
		}

		private bool CheckWin()
		{
			if ( field.Players.Count == GetLoosers().Count + 1 )
			{
				return true;
			}
			else
				return false;
		}

		private bool IsNeedMainCycleRun()
		{
			return CheckWin() == false && GetLoosers().Count != field.Players.Count && stepsCounter <= MAX_STEPS;
		}

		public List<Player> GetLoosers()
		{
			var result = new List<Player>();

			foreach ( var player in field.Players )
				if ( player.SpellParts.Count == 0 )
					result.Add( player );

			return result;
		}

		private Player GetWinner()
		{
			foreach ( var player in field.Players )
				if ( player.SpellParts.Count != 0 )
					return player;
			return null;
		}

		public void Run( Field field )
		{
			this.field = field;

			stepsCounter = 0;

			SaveBaseInfo();
			SaveStep();

			do
			{
				foreach ( var action in actionsOrderList )
					action();

				stepsCounter++;
			}
			while ( IsNeedMainCycleRun() );


			SaveEnd();
		}
	}
}
