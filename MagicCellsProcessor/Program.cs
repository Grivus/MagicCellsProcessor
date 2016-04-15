using MagicCellsProcessor.Entities;
using MagicCellsProcessor.Entities.FieldParts;
using MagicCellsProcessor.Entities.SpellParts;
using MagicCellsProcessor.Entities.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor
{
	class Program
	{
		static void Main( string[] args )
		{
			Player firstPlayer = new Player( 1 );
			firstPlayer.InitSpellPartsAndPositions( new List<KeyValuePair<ISpellPart, Entities.utils.Vector2>>()
			{
				new KeyValuePair<Entities.SpellParts.ISpellPart, Entities.utils.Vector2>(
														new TestSpellPart( firstPlayer, 2, 1 ),
														new Entities.utils.Vector2( 10, 10 )
														)
			} );

			Player secondPlayer = new Player( 2 );
			secondPlayer.InitSpellPartsAndPositions( new List<KeyValuePair<ISpellPart, Entities.utils.Vector2>>()
			{
				new KeyValuePair<ISpellPart, Entities.utils.Vector2>(
														new TestSpellPart( secondPlayer, 5, 1 ),
														new Entities.utils.Vector2( 3, 10 )
													)
			} );

			Field field = new Field( new List<Player> { firstPlayer, secondPlayer }, 20, 20 );

			GameProcessor.Instance.Run( field );
		}
	}
}
