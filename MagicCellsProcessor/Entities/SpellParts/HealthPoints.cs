using MagicCellsProcessor.Entities.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.SpellParts
{
	public class HealthPoints
	{
		public int CurrentHp
		{
			get
			{
				return currentHp;
			}

			set
			{
				currentHp = Utils.Clamp<int>( value, 0, maxHp );
			}
		}

		private int maxHp;

		private int currentHp;

		public HealthPoints( int hp )
		{
			maxHp = currentHp = hp;
		}

	}
}
