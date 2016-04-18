using MagicCellsProcessor.Entities.SpellParts;
using MagicCellsProcessor.Entities.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.FieldParts
{
	public class Field
	{
		/// <summary>
		/// Cells array on the field
		/// </summary>
		public List<List<Cell>> Cells
		{
			get
			{
				return cells;
			}
		}

		public int CellsCount
		{
			get
			{
				return cellsCount;
			}
		}

		/// <summary>
		/// all spell parts 
		/// </summary>
		public List<TestSpellPart> AllSpellParts
		{
			get
			{
				var allSpellParts = new List<TestSpellPart>();

				foreach ( var player in players )
				{
					allSpellParts.AddRange( player.SpellParts );
				}

				return allSpellParts;
			}
			
		}

		/// <summary>
		/// 
		/// </summary>
		public List<Player> Players
		{
			get
			{
				return players;
			}
		}

		private List<List<Cell>> cells;

		//private List<ISpellPart> allSpellParts;

		private List<Player> players;

		private int cellsCount;

		public Field( List<Player> players, 
					  int fieldSizeX = FieldConstants.FIELD_SIZE_X, 
					  int fieldSizeY = FieldConstants.FIELD_SIZE_Y )
		{
			// init cells
			cells = new List<List<Cell>>( fieldSizeX );
			for ( int i = 0; i < fieldSizeX; ++i )
			{
				cells.Add( new List<Cell>( fieldSizeY ) );
				for ( int j = 0; j < fieldSizeY; ++j )
					cells[ i ].Add( new Cell( this, i, j ) );
			}

			cellsCount = fieldSizeX * fieldSizeY;

			// locate players on field
			this.players = new List<Player>();
			foreach ( var player in players )
			{
				this.players.Add( player );
				LocatePlayersPartsOnField( player );
			}
			
		}

		private void LocatePlayersPartsOnField( Player player )
		{
			Vector2 offset = FieldConstants.playersOffsetsOnField[ player.Id ];
			foreach ( var spellPart in player.SpellPartsAndPositions )
			{
				var destination = cells[ spellPart.Value.x + offset.x ][ spellPart.Value.y + offset.y ];
				destination.SetSpellPart( spellPart.Key );
				spellPart.Key.MoveTo( destination );
			}
		}

		public TestSpellPart GetSpellPartByIndex( int index )
		{
			var pos = Utils.ConvertOneIndexToVector2( index, Cells.Count );
			var spellPart = Cells[ pos.x ][ pos.y ].SpellPart;

			return spellPart;
		}

		public Cell GetCellByIndex( int index )
		{
			var pos = Utils.ConvertOneIndexToVector2( index, Cells.Count );
			var cell = Cells[ pos.x ][ pos.y ];

			return cell;
		}
	}
}
