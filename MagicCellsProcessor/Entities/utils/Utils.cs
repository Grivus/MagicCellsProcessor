using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.utils
{
	public static class Utils
	{
		public static T Clamp<T>( T val, T min, T max ) where T : IComparable<T>
		{
			if ( val.CompareTo( min ) < 0 )
				return min;
			else if ( val.CompareTo( max ) > 0 )
				return max;
			else
				return val;
		}

		public static int ConvertTwoIndexesToOne( int x, int y, int count )
		{
				return x + ( y ) * count;

		}

		public static int ConvertVector2ToOne( Vector2 pos, int count )
		{
				return pos.x + ( pos.y ) * count;

		}

		public static Vector2 ConvertOneIndexToVector2( int index, int count )
		{
			return new Vector2( index % count, index / count );
		}
	}
}
