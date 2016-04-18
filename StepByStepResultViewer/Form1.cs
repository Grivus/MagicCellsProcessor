using MagicCellsProcessor.Entities.utils;
using StepByStepResultViewer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StepByStepResultViewer
{
	public partial class Form1 : Form
	{
		List<StepInfo> steps = new List<StepInfo>();
		private int fieldSizeX;
		private int fieldSizeY;

		private int stepNumber = 0;

		private Dictionary<int, Color> colors = new Dictionary<int, Color>()
		{
			{ 1, Color.Red },
			{ 2, Color.Blue }
		};

		private void ShowStep( int stepNumber )
		{
			for ( int i = 0; i < fieldSizeX; ++i )
			{
				for ( int j = 0; j < fieldSizeY; ++j )
				{
					var some = tableView[ i, j ];
					some.Value = "";
					some.Style.BackColor = Color.White;
				}
			}

			foreach ( var element in steps[ stepNumber ].hpSpellPartsPositions )
			{
				tableView[ element.pos.x, element.pos.y ].Value = element.hp;
				tableView[ element.pos.x, element.pos.y ].Style.BackColor = colors[ element.playerId ];
			}

		}

		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click( object sender, EventArgs e )
		{
			var filename = ConfigurationManager.AppSettings[ "workFile" ];
			StreamReader sr = new StreamReader( filename );
			
			fieldSizeX = Int32.Parse( sr.ReadLine() );
			fieldSizeY = Int32.Parse( sr.ReadLine() );

			while ( !sr.EndOfStream )
			{
				int spellPartsOnStep = Int32.Parse( sr.ReadLine() );

				List<SpellPartInfo> hpPos = new List<SpellPartInfo>();

				for ( int i = 0; i < spellPartsOnStep; ++i )
				{
					int playerId = Int32.Parse( sr.ReadLine() );

					string rawPos = sr.ReadLine();

					var rawPositions = rawPos.Split( ' ' );

					int posX = Int32.Parse( rawPositions[ 0 ] );
					int posY = Int32.Parse( rawPositions[ 1 ] );

					int hp = Int32.Parse( sr.ReadLine() );

					hpPos.Add( new SpellPartInfo( playerId, hp, new Vector2( posX, posY ) ) );
				}

				steps.Add( new StepInfo( hpPos ) );
			}

			sr.Close();

			tableView.Rows.Clear();
			tableView.Columns.Clear();

			for ( int i = 0; i < fieldSizeX; ++i )
			{
				tableView.Columns.Add( new DataGridViewTextBoxColumn() );
			}

			for ( int i = 0; i < fieldSizeY; ++i )
			{
				tableView.Rows.Add();
			}

			for ( int i = 0; i < fieldSizeX; ++i )
			{
				tableView.Columns[ i ].Width = 20;
				tableView.Columns[ i ].HeaderText = (i ).ToString();
			}

			for ( int i = 0; i < fieldSizeY; ++i )
			{
				tableView.Rows[ i ].Height = 20;
				tableView.Rows[ i ].HeaderCell.Value = ( i  ).ToString();
			}

			ShowStep( stepNumber );
		}

		private void button2_Click( object sender, EventArgs e )
		{
			if ( stepNumber > 0 )
			{
				ShowStep( --stepNumber );
			} 
		}

		private void button3_Click( object sender, EventArgs e )
		{
			if ( stepNumber < steps.Count - 1 )
			{
				ShowStep( ++stepNumber );
			}
		}
	}
}
