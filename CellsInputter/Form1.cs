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

namespace CellsInputter
{
	public partial class Form1 : Form
	{
		int fieldSizeX;
		int fieldSizeY;

		private Dictionary<int, Color> colors = new Dictionary<int, Color>()
		{
			{ 1, Color.Red },
			{ 2, Color.Blue }
		};


		public Form1()
		{
			InitializeComponent();
		}

		private void label4_Click( object sender, EventArgs e )
		{

		}

		private void Form1_Load( object sender, EventArgs e )
		{
			cbCellType.Items.Add( "Default" );
			cbCellType.SelectedItem = cbCellType.Items[ 0 ];

			cbPlayerId.Items.Add( 1 );
			cbPlayerId.Items.Add( 2 );
			cbPlayerId.SelectedItem = cbPlayerId.Items[ 0 ];

			fieldSizeX = Convert.ToInt32( tbFieldSize.Text );
			fieldSizeY = Convert.ToInt32( tbFieldSize.Text );

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
				tableView.Columns[ i ].HeaderText = ( i ).ToString();
			}

			for ( int i = 0; i < fieldSizeY; ++i )
			{
				tableView.Rows[ i ].Height = 20;
				tableView.Rows[ i ].HeaderCell.Value = ( i ).ToString();
			}
		}

		private void tableView_SelectionChanged( object sender, EventArgs e )
		{
			var selected = tableView.SelectedCells;

			if ( Control.ModifierKeys == Keys.Control )
			{
				for ( int i = 0; i < selected.Count; ++i )
				{
					selected[ i ].Value = Convert.ToInt32( tbCellLife.Text );
					selected[ i ].Style.BackColor = colors[ Convert.ToInt32( cbPlayerId.SelectedItem ) ];
				}
			}
			else
			{
				for ( int i = 0; i < selected.Count; ++i )
				{
					selected[ i ].Value = "";
					selected[ i ].Style.BackColor = Color.White;
				}
			}
		}

		private void button1_Click( object sender, EventArgs e )
		{
			List<DataGridViewCell> filled = new List<DataGridViewCell>();
			
			for ( int i = 0; i < fieldSizeX; ++i )
			{
				for ( int j = 0; j < fieldSizeY;  ++j )
				{
					if ( tableView[ i, j ].Value != null && tableView[ i, j ].Value.ToString() != "" )
					{
						filled.Add( tableView[ i, j ] );
					}
				}
			}

			var filename = ConfigurationManager.AppSettings[ "workFile" ];

			StreamWriter writer = new StreamWriter( filename );

			writer.WriteLine( fieldSizeX );
			writer.WriteLine( fieldSizeY );

			writer.WriteLine( filled.Count );

			for ( int i = 0; i < filled.Count; ++i )
			{
				// playerId
				writer.Write( filled[i].Style.BackColor == colors[1] ? 1 : 2);
				writer.Write( " " );
				//cell pos
				writer.Write( filled[i].RowIndex );
				writer.Write( " " );
				writer.Write( filled[ i ].ColumnIndex );
				writer.Write( " " );
				// cells life
				writer.WriteLine( filled[ i ].Value );

			}

			writer.Close();
		}

		private void button2_Click( object sender, EventArgs e )
		{
			var filename = ConfigurationManager.AppSettings[ "workFile" ];

			StreamReader reader = new StreamReader( filename );

			fieldSizeX = Int32.Parse( reader.ReadLine() );
			fieldSizeY = Int32.Parse( reader.ReadLine() );
			int count = Int32.Parse( reader.ReadLine() );

			while ( !reader.EndOfStream )
			{
				string cellInfo = reader.ReadLine();
				var info = cellInfo.Split( ' ' );

				int playerId = Int32.Parse( info[ 0 ] );
				int row = Int32.Parse( info[ 1 ] );
				int col = Int32.Parse( info[ 2 ] );
				int life = Int32.Parse( info[ 3 ] );

				tableView[ col, row ].Value = life;
				tableView[ col, row ].Style.BackColor = colors[ playerId ];

			}
			reader.Close();
		}

		private void button3_Click( object sender, EventArgs e )
		{
			for ( int i = 0; i < fieldSizeX; ++i )
			{
				for ( int j = 0; j < fieldSizeY; ++j )
				{
					tableView[ i, j ].Value = "";
					tableView[ i, j ].Style.BackColor = Color.White;

				}
			}
		}
	}
}
