using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.utils
{
	public class Vector2
	{
		public int x;
		public int y;

		public Vector2( int x, int y )
		{
			this.x = x;
			this.y = y;
		}

		public override string ToString()
		{
			return x.ToString() + " " + y.ToString();
		}
	}
}
