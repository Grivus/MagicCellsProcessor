using MagicCellsProcessor.Entities.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.FieldParts
{
	public static class FieldConstants
	{
		public const int FIELD_SIZE_X = 50;
		public const int FIELD_SIZE_Y = 50;

		public static List<Vector2> playersOffsetsOnField = new List<Vector2>() { new Vector2(0, 0),
																				 new Vector2( 0, 0),
																				 new Vector2( 0, 0)
																			    };
	}
}
