using MagicCellsProcessor.Entities.SpellParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.Algorithms.MoveStrategies
{
	public static class MoveStrategyFactory
	{
		private static Dictionary<Type, ISpellPartMoveStrategy> typeSettingsMap = new Dictionary<Type, ISpellPartMoveStrategy>()
		{
			{ typeof(TestSpellPart), new TestSpellPartMoveStrategy() }
		};

		public static ISpellPartMoveStrategy GetMoveStrategyForMe( TestSpellPart spellPart )
		{
			return typeSettingsMap[ spellPart.GetType() ];
		}
	}
}
