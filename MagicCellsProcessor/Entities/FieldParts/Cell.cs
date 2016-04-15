using MagicCellsProcessor.Entities.SpellParts;
using MagicCellsProcessor.Entities.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.FieldParts
{
	/// <summary>
	/// Represents field cell
	/// </summary>
	public class Cell
	{
		public int Index
		{
			get
			{
				return Utils.ConvertVector2ToOne( position, field.Cells.Count );
			}
		}
		/// <summary>
		/// position of that cell in the field
		/// You should not modify me out of the field.
		/// </summary>
		public Vector2 Position
		{
			get
			{
				return position;
			}
		}
		private Vector2 position;

		/// <summary>
		/// spell part in that cell
		/// </summary>
		public ISpellPart SpellPart
		{
			get
			{
				return spellPart;
			}
		}
		private ISpellPart spellPart;

		/// <summary>
		/// weight for distance calculations
		/// </summary>
		public float Weight
		{
			get
			{
				float spellPartWeight = spellPart == null ? 0 : CellsConstants.CELL_WEIGHT_WITH_SPELLPART;
				return weight + spellPartWeight;
			}
		}

		public Field Field
		{
			get
			{
				return field;
			}
		}

		private float weight;

		private Field field;

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="owner"> field owner of cell </param>
		/// <param name="xPos"> x coordinate in field </param>
		/// <param name="yPos"> y coordinate in field </param>
		/// <param name="weight"> weight for distance calculations </param>
		public Cell( Field owner, int xPos, int yPos, float weight = CellsConstants.CELL_STANDART_WEIGHT )
		{
			this.field = owner;
			this.position = new Vector2( xPos, yPos );
			this.weight = weight;
		}

		/// <summary>
		/// locates spellPart in that cell
		/// </summary>
		/// <param name="spellPart"> spell part to locate in that cell </param>
		public void SetSpellPart( ISpellPart spellPart )
		{
			this.spellPart = spellPart;
		}

		/// <summary>
		/// removes spellPart from that cell
		/// </summary>
		public void RemoveSpellPart()
		{
			spellPart = null;
		}

		/// <summary>
		/// 8 neighbors for middle cell, and so on
		/// </summary>
		/// <returns></returns>
		public List<Cell> GetNeighborsCells()
		{
			List<Cell> result = new List<Cell>();

			int sizeX = field.Cells.Count;
			int sizeY = field.Cells[ 0 ].Count;
			
			// left
			if ( position.x >= 1 )
			{
				result.Add( field.Cells[ position.x - 1 ][ position.y ] );
			}

			// left-bottom
			if ( position.x >= 1 && position.y < sizeY - 1 )
			{
				result.Add( field.Cells[ position.x - 1 ][ position.y + 1 ] );
			}

			// left-top
			if ( position.x >= 1 && position.y >= 1 )
			{
				result.Add( field.Cells[ position.x - 1 ][ position.y - 1 ] );
			}

			// right
			if ( position.x < sizeX - 1 )
			{
				result.Add( field.Cells[ position.x + 1 ][ position.y ] );
			}
			
			// right-top
			if ( position.x < sizeX - 1 && position.y < sizeY - 1 )
			{
				result.Add( field.Cells[ position.x + 1 ][ position.y + 1 ] );
			}

			// right-bottom
			if ( position.x < sizeX - 1 && position.y >= 1 )
			{
				result.Add( field.Cells[ position.x + 1 ][ position.y - 1 ] );
			}

			//top
			if ( position.y >= 1 )
			{
				result.Add( field.Cells[ position.x ][ position.y - 1 ] );
			}

			//top
			if ( position.y < sizeY - 1 )
			{
				result.Add( field.Cells[ position.x ][ position.y + 1 ] );
			}


			return result;
		}
	}
}
