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
		public List<TestSpellPart> SpellParts
		{
			get
			{
				return spellParts;
			}
		}

		public List<KeyValuePair<TestSpellPart, Vector2>> SpellPartsAndPositions
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

		private List<TestSpellPart> spellParts;

		private List<KeyValuePair<TestSpellPart, Vector2>> spellPartsAndPositions;

		private int id;

		public Player( int id )
		{
			this.id = id;

			this.spellParts = new List<TestSpellPart>();
		}

		public void InitSpellPartsAndPositions( List<KeyValuePair<TestSpellPart, Vector2>> spellPartsAndPositions )
		{
			this.spellPartsAndPositions = spellPartsAndPositions;

			foreach ( var entry in spellPartsAndPositions )
			{
				spellParts.Add( entry.Key );
			}
		}
	}
}
