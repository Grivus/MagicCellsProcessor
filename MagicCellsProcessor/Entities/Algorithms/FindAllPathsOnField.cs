using MagicCellsProcessor.Entities.FieldParts;
using MagicCellsProcessor.Entities.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.Algorithms
{
	public class DistancesInfoStorage
	{
		public List<List<float>> distances;
		public List<List<int>> paths;

		public DistancesInfoStorage( List<List<float>> dist, List<List<int>> paths )
		{
			this.distances = new List<List<float>>( dist );
			this.paths = new List<List<int>>( paths );
		}
	}

	public static class FindAllPathsOnField
	{

		public static int GetFirstWayIndex( int indexFrom, int indexTo, List<List<int>> paths )
		{
			if ( paths[ indexFrom ][ indexTo ] == indexTo )
				return indexTo;
			else
				return GetFirstWayIndex( indexFrom, paths[ indexFrom ][ indexTo ], paths );

			throw new Exception( "fuck you" );
		}

		public static DistancesInfoStorage GetAllPaths( Field field )
		{
			var distances = new List<List<float>>();
			var paths = new List<List<int>>();
			// init with zeros
			
			for ( int i = 0; i < field.CellsCount; ++i )
			{
				distances.Add( new List<float>() );
				paths.Add( new List<int>() );
				for ( int j = 0; j < field.CellsCount; ++j )
				{
					distances[ i ].Add( CellsConstants.CELL_WEIGHT_INFINITY );
					paths[ i ].Add( -1 );
				}
			}

			for ( int i = 0; i < field.Cells.Count; ++i )
				for ( int j = 0; j < field.Cells.Count; ++j )
				{
					var cell = field.Cells[ i ][ j ];
					var cellIndex = Utils.ConvertVector2ToOne( cell.Position, field.Cells.Count );//cell.Position.x + cell.Position.y * ( field.CellsCount - 1 );

					foreach ( var neighbour in cell.GetNeighborsCells() )
					{
						var neighbourIndex = Utils.ConvertTwoIndexesToOne( neighbour.Position.x, neighbour.Position.y, field.Cells.Count );//neighbour.Position.x + neighbour.Position.y * ( field.Cells[ i ].Count - 1 );
						distances[ cellIndex ][ neighbourIndex ] = CellsConstants.CELL_STANDART_WEIGHT;
						distances[ neighbourIndex ][ cellIndex ] = CellsConstants.CELL_STANDART_WEIGHT;

						paths[ cellIndex ][ neighbourIndex ] = neighbourIndex;
						paths[ neighbourIndex ][ cellIndex ] = cellIndex;
					}

					distances[ cellIndex ][ cellIndex ] = CellsConstants.CELL_WEIGHT_INFINITY;
					
				}

			// floyd-warshall find all paths 
			for ( int i = 0; i < field.CellsCount; ++i )
			{
				for ( int j = 0; j < field.CellsCount; ++j )
				{
					for ( int k = 0; k < field.CellsCount; ++k )
					{
						if ( distances[ j ][ i ] + distances[ i ][ k ] < distances[ j ][ k ] )
						{
							distances[ j ][ k ] = distances[ j ][ i ] + distances[ i ][ k ];
							paths[ j ][ k ] = i;
						}
					}
				}
			}

			return new DistancesInfoStorage( distances, paths );

		}
	}
}
