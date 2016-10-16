using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.Logging.DataStructures
{
	public class CurrentState
	{
		public List<SpellPartInfo> spellParts = new List<SpellPartInfo>();

		public List<IActionType> actions = new List<IActionType>();

		public CurrentState()
		{
		}

		public CurrentState( CurrentState state )
		{
			spellParts.Clear();

			foreach ( var spellPart in state.spellParts )
				spellParts.Add( new SpellPartInfo( spellPart ) );

			actions.Clear();

			foreach ( var action in state.actions )
				actions.Add( action.Copy() );
		}

		public void Clear()
		{
			spellParts.Clear();
			actions.Clear();
		}
	}
}
