using MagicCellsProcessor.Entities.FieldParts;
using MagicCellsProcessor.Entities.SpellParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.Algorithms
{
	public interface ISpellPartMoveStrategy
	{
		Cell GetCellToMoveFor( TestSpellPart spellPart );

		void DoCalculations( TestSpellPart spellPart );
	}
}
