using MagicCellsProcessor.Entities.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.Logging.DataStructures
{
	public struct SpellPartInfo
	{
		public Vector2 position;
		public int playerId;
		public int id;
		public int hp;

		public SpellPartInfo( int id, int playerId, int hp, Vector2 position )
		{
			this.id = id;
			this.playerId = playerId;
			this.hp = hp;
			this.position = position;
		}

		public SpellPartInfo( SpellPartInfo info )
		{
			id = info.id;
			playerId = info.playerId;
			hp = info.hp;
			position = info.position;
		}
	}
}
