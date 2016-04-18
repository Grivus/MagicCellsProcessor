using MagicCellsProcessor.Entities.SpellParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.Algorithms
{
	public class NearestEnemyInfo
	{
		public TestSpellPart nearestEnemy;

		public float distance;

		public NearestEnemyInfo( TestSpellPart enemy, float distance )
		{
			this.nearestEnemy = enemy;
			this.distance = distance;
		}
	}
}
