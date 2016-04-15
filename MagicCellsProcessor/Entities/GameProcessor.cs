using MagicCellsProcessor.Entities.FieldParts;
using MagicCellsProcessor.Entities.SpellParts;
using MagicCellsProcessor.Entities.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				ProcessCells,
				SaveStep
			};
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

		private List<Action> actionsOrderList;

		private Field field;

		private Algorithms.DistancesInfoStorage distanceInfoStorage;

		private int stepsCounter = 0;

		private const int MAX_STEPS = 100000;

		private void MoveCells()
		{
			var moveCompleteList = new List<ISpellPart>();

			// двигаем все клетки
			foreach ( var spellPart in field.AllSpellParts )
			{
				// если в радиусе клетки есть враг - двигаться никуда не надо, дерёмся
				if ( spellPart.GetEnemyNeighborsSpellParts().Count != 0 )
					continue;

				// если туда ещё никто не пошёл, идём
				if ( moveCompleteList.Find( x => x.IndexToMove == spellPart.IndexToMove ) == null )
				{
					Cell destination = field.GetCellByIndex( spellPart.IndexToMove );
					spellPart.MoveTo( destination );
					moveCompleteList.Add( spellPart );
				}
				// если туда уже кто-то пошёл - танцы с бубном
				else
				{
					var neighborsCells = spellPart.CurrentCell.GetNeighborsCells();

					float minDistance = CellsConstants.CELL_WEIGHT_INFINITY;
					int indexToGo = -1;

					foreach ( var neighborCell in neighborsCells )
					{
						int neigborIndex = Utils.ConvertVector2ToOne( spellPart.CurrentCell.Position, field.Cells.Count );
						if ( distanceInfoStorage.distances[ neigborIndex ][ spellPart.NearestEnemy.CurrentCell.Index ] < minDistance && 
							// если оттуда до цели можно дойти лучше чем есть сейчас 
							 neighborCell.SpellPart == null )// и там нет клетки 
						{
							minDistance = distanceInfoStorage.distances[ neigborIndex ][ spellPart.NearestEnemy.CurrentCell.Index ];
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
		}

		private void CalculateImpacts()
		{
			// TODO: implement me
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
					spellPart.Die();
				}
			}
		}

		private void CalculateDistances()
		{
			distanceInfoStorage = Algorithms.FindAllPathsOnField.GetAllPaths( field );

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

						spellPart.NearestEnemy = field.GetSpellPartByIndex( index );
					}
				}

				spellPart.IndexToMove = indexToMove;
			}
		}

		private void SaveBaseInfo()
		{
			StreamWriter wr = new StreamWriter( @"h:\cellsTest.txt", false);
			wr.WriteLine( field.Cells.Count );
			wr.WriteLine( field.Cells.Count );
			wr.Close();
		}

		private void SaveStep()
		{
			StreamWriter wr = new StreamWriter( @"h:\cellsTest.txt", true );

			wr.WriteLine( field.AllSpellParts.Count );

			foreach ( var spellPart in field.AllSpellParts )
			{
				wr.WriteLine( spellPart.CurrentCell.Position.ToString() );
				wr.WriteLine( spellPart.HealthPoints.CurrentHp );
			}

			wr.Close();
		}

		private void SaveEnd()
		{
			StreamWriter wr = new StreamWriter( @"h:\cellsTest.txt", true );
			wr.WriteLine( "End" );
			wr.Close();
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

		public void Run( Field field )
		{
			this.field = field;

			stepsCounter = 0;

			SaveBaseInfo();

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
