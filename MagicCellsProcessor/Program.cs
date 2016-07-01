﻿using MagicCellsProcessor.Entities;
using MagicCellsProcessor.Entities.FieldParts;
using MagicCellsProcessor.Entities.SpellParts;
using MagicCellsProcessor.Entities.utils;
using System;
using System.Collections.Generic;
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

            //MyTest test = new MyTest();
            //test.list.Add( 2 );
            //test.list.Add( 3 );

            //List<MyTest> listTest = new List<MyTest>();
            //listTest.Add( test );
            //listTest.Add( new MyTest() );

            //JavaScriptSerializer ser = new JavaScriptSerializer();
            //Console.WriteLine( ser.Serialize( listTest ) );

            //return;
            Player firstPlayer = new Player( 1 );
			firstPlayer.InitSpellPartsAndPositions( new List<KeyValuePair<TestSpellPart, Entities.utils.Vector2>>()
			{
				new KeyValuePair<Entities.SpellParts.TestSpellPart, Entities.utils.Vector2>(
														new TestSpellPart( firstPlayer, 5, 3 ),
														new Entities.utils.Vector2( 10, 10 )
														),
				new KeyValuePair<Entities.SpellParts.TestSpellPart, Entities.utils.Vector2>(
														new TestSpellPart( firstPlayer, 5, 3 ),
														new Entities.utils.Vector2( 10, 11 )
														),
				new KeyValuePair<Entities.SpellParts.TestSpellPart, Entities.utils.Vector2>(
														new TestSpellPart( firstPlayer, 5, 3 ),
														new Entities.utils.Vector2( 9, 10 )
														),
				new KeyValuePair<Entities.SpellParts.TestSpellPart, Entities.utils.Vector2>(
														new TestSpellPart( firstPlayer, 5, 3 ),
														new Entities.utils.Vector2( 9, 11 )
														)
			} );

			Player secondPlayer = new Player( 2 );
			secondPlayer.InitSpellPartsAndPositions( new List<KeyValuePair<TestSpellPart, Entities.utils.Vector2>>()
			{
				new KeyValuePair<TestSpellPart, Entities.utils.Vector2>(
														new TestSpellPart( secondPlayer, 10, 4 ),
														new Entities.utils.Vector2( 3, 10 )
													),
				new KeyValuePair<TestSpellPart, Entities.utils.Vector2>(
														new TestSpellPart( secondPlayer, 10, 4 ),
														new Entities.utils.Vector2( 3, 11 )
													),
				new KeyValuePair<TestSpellPart, Entities.utils.Vector2>(
														new TestSpellPart( secondPlayer, 10, 4 ),
														new Entities.utils.Vector2( 2, 10 )
													)
			} );

			Field field = new Field( new List<Player> { firstPlayer, secondPlayer }, 20, 20 );

			GameProcessor.Instance.Run( field );
		}
	}
}
