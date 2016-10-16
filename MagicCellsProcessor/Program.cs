using MagicCellsProcessor.Entities;
using MagicCellsProcessor.Entities.FieldParts;
using MagicCellsProcessor.Entities.SpellParts;
using MagicCellsProcessor.Entities.utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MagicCellsProcessor
{
    class MyTest
    {
        public List<int> list = new List<int>();
        public int a = 2;

        public string A
        {
            get
            {
                return "Aaaa";
            }
        }
    }

	class Program
	{
		static void Main( string[] args )
		{
			var filename = ConfigurationManager.AppSettings[ "inputFile" ];

			StreamReader reader = new StreamReader( filename );

			int fieldSizeX = Int32.Parse( reader.ReadLine() );
			int fieldSizeY = Int32.Parse( reader.ReadLine() );
			int count = Int32.Parse( reader.ReadLine() );


			Player firstPlayer = new Player( 1 );
			Player secondPlayer = new Player( 2 );

			var firstPlayerCells = new List<KeyValuePair<TestSpellPart, Entities.utils.Vector2>>();
			var secondPlayerCells = new List<KeyValuePair<TestSpellPart, Entities.utils.Vector2>>();




			while ( !reader.EndOfStream )
			{
				string cellInfo = reader.ReadLine();
				var info = cellInfo.Split( ' ' );

				int playerId = Int32.Parse( info[ 0 ] );
				
				int row = Int32.Parse( info[ 1 ] );
				int col = Int32.Parse( info[ 2 ] );
				int life = Int32.Parse( info[ 3 ] );

				var cells = playerId == 1 ? firstPlayerCells : secondPlayerCells;
				var player = playerId == 1 ? firstPlayer : secondPlayer;
				cells.Add( new KeyValuePair<Entities.SpellParts.TestSpellPart, Entities.utils.Vector2>(
														new TestSpellPart( player, life, life / 2  ),
														new Entities.utils.Vector2( col, row )
														) );

			}
			reader.Close();

			firstPlayer.InitSpellPartsAndPositions( firstPlayerCells );
			secondPlayer.InitSpellPartsAndPositions( secondPlayerCells );


			//firstPlayer.InitSpellPartsAndPositions( new List<KeyValuePair<TestSpellPart, Entities.utils.Vector2>>()
			//{
			//	new KeyValuePair<Entities.SpellParts.TestSpellPart, Entities.utils.Vector2>(
			//											new TestSpellPart( firstPlayer, 5, 3 ),
			//											new Entities.utils.Vector2( 10, 10 )
			//											),
			//	new KeyValuePair<Entities.SpellParts.TestSpellPart, Entities.utils.Vector2>(
			//											new TestSpellPart( firstPlayer, 5, 3 ),
			//											new Entities.utils.Vector2( 10, 11 )
			//											),
			//	new KeyValuePair<Entities.SpellParts.TestSpellPart, Entities.utils.Vector2>(
			//											new TestSpellPart( firstPlayer, 5, 3 ),
			//											new Entities.utils.Vector2( 9, 10 )
			//											),
			//	new KeyValuePair<Entities.SpellParts.TestSpellPart, Entities.utils.Vector2>(
			//											new TestSpellPart( firstPlayer, 5, 3 ),
			//											new Entities.utils.Vector2( 9, 11 )
			//											)
			//} );

			//secondPlayer.InitSpellPartsAndPositions( new List<KeyValuePair<TestSpellPart, Entities.utils.Vector2>>()
			//{
			//	new KeyValuePair<TestSpellPart, Entities.utils.Vector2>(
			//											new TestSpellPart( secondPlayer, 10, 4 ),
			//											new Entities.utils.Vector2( 3, 10 )
			//										),
			//	new KeyValuePair<TestSpellPart, Entities.utils.Vector2>(
			//											new TestSpellPart( secondPlayer, 10, 4 ),
			//											new Entities.utils.Vector2( 3, 11 )
			//										),
			//	new KeyValuePair<TestSpellPart, Entities.utils.Vector2>(
			//											new TestSpellPart( secondPlayer, 10, 4 ),
			//											new Entities.utils.Vector2( 2, 10 )
			//										)
			//} );

			Field field = new Field( new List<Player> { firstPlayer, secondPlayer }, fieldSizeX, fieldSizeY );

			GameProcessor.Instance.Run( field );
		}
	}
}
