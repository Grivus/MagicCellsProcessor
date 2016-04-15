using MagicCellsProcessor.Entities.SpellParts;
using MagicCellsProcessor.Entities.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities
{
	public class Player
	{
		public List<ISpellPart> SpellParts
		{
			get
			{
				return spellParts;
			}
		}

		public List<KeyValuePair<ISpellPart, Vector2>> SpellPartsAndPositions
		{
			get
			{
				return spellPartsAndPositions;
			}
		}

		public int Id
		{
			get
			{
				return id;
			}
		}

		private List<ISpellPart> spellParts;

		private List<KeyValuePair<ISpellPart, Vector2>> spellPartsAndPositions;

		private int id;

		public Player( int id )
		{
			this.id = id;

			this.spellParts = new List<ISpellPart>();
		}

		public void InitSpellPartsAndPositions( List<KeyValuePair<ISpellPart, Vector2>> spellPartsAndPositions )
		{
			this.spellPartsAndPositions = spellPartsAndPositions;

			foreach ( var entry in spellPartsAndPositions )
			{
				spellParts.Add( entry.Key );
			}
		}
	}
}
