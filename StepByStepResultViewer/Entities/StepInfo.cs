using MagicCellsProcessor.Entities.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepByStepResultViewer.Entities
{
	public class SpellPartInfo
	{
		public Vector2 pos;
		public int hp;
		public int playerId;

		public SpellPartInfo( int playerId, int hp, Vector2 pos )
		{
			this.playerId = playerId;
			this.hp = hp;
			this.pos = pos;
		}
	}

	public class StepInfo
	{
		public List<SpellPartInfo> hpSpellPartsPositions;

		public StepInfo( List<SpellPartInfo> step )
		{
			hpSpellPartsPositions = new List<SpellPartInfo>( step );
		}
	}
}
