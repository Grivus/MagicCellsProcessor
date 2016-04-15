using MagicCellsProcessor.Entities.FieldParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.SpellParts
{
	public interface ISpellPart
	{
		Player PlayerOwner
		{
			get;
		}

		Cell CurrentCell
		{
			get;
		}

		int IndexToMove
		{
			get;
			set;
		}

		ISpellPart NearestEnemy
		{
			get;
			set;
		}

		void TakeDamage( int damage );

		HealthPoints HealthPoints
		{
			get;
		}

		void DoSelf();

		void Die();

		void MoveTo( Cell destination );

		List<ISpellPart> GetNeighborsSpellParts();

		List<ISpellPart> GetAlliedNeighborsSpellParts();

		List<ISpellPart> GetEnemyNeighborsSpellParts();

	}
}
