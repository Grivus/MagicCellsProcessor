using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicCellsProcessor.Entities.FieldParts;
using MagicCellsProcessor.Entities.SpellParts;
using MagicCellsProcessor.Entities.utils;

namespace MagicCellsProcessor.Entities.Algorithms.MoveStrategies
{
	/// <summary>
	/// Двигаемся по прямой в сторону ближайшего врага
	/// </summary>
	public class TestSpellPartMoveStrategy : ISpellPartMoveStrategy
	{
		public void DoCalculations( TestSpellPart spellPart )
		{
			var allMyEnemies = new List<TestSpellPart>();

			foreach ( var spellPart2 in spellPart.CurrentCell.Field.AllSpellParts )
			{
				if ( spellPart2.PlayerOwner.Id != spellPart.PlayerOwner.Id )
				{
					allMyEnemies.Add( spellPart2 );
				}
			}

			// ищем "глупо" ближайшего врага, по сетке координат
			var nearestEnemy = allMyEnemies[ 0 ];
			var minDistance = CellsConstants.CELL_WEIGHT_INFINITY;

			foreach ( var enemy in allMyEnemies )
			{
				var curDistance = Utils.GetDistance( spellPart.CurrentCell.Position, enemy.CurrentCell.Position );
				if ( curDistance < minDistance )
				{
					minDistance = curDistance;
					nearestEnemy = enemy;
				}
			}

			// запомнили ближайшего к нам врага по "прямой"
			spellPart.NearestEnemyInfo = new NearestEnemyInfo( nearestEnemy, minDistance );
		}

		public Cell GetCellToMoveFor( TestSpellPart spellPart )
		{
			Cell result = null;

			// если в радиусе клетки есть враг - двигаться никуда не надо, дерёмся
			if ( spellPart.GetEnemyNeighborsSpellParts().Count != 0 )
				return result;

			// иначе ищем, куда лучше подвигаться, чтобы дойти до выбранного врага
			var neighbors = spellPart.CurrentCell.GetNeighborsCells();
			var minDistance = Utils.GetDistance( spellPart.CurrentCell.Position, spellPart.NearestEnemyInfo.nearestEnemy.CurrentCell.Position );
			Cell bestNeighbor = null;
			// по всем соседним клеткам
			foreach ( var neighbor in neighbors )
			{
				var curDistance = Utils.GetDistance( neighbor.Position, spellPart.NearestEnemyInfo.nearestEnemy.CurrentCell.Position );
				// если из неё ближе и она свободна
				if ( curDistance < minDistance && neighbor.SpellPart == null )
				{
					minDistance = curDistance;
					bestNeighbor = neighbor;
				}
			}

			// если двигаться не нашли куда - стоим на месте
			if ( bestNeighbor == null )
				return result;

			return bestNeighbor;
		}
	}
}
