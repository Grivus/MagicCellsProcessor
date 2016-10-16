using MagicCellsProcessor.Entities.Logging.DataStructures;
using MagicCellsProcessor.Entities.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.Logging
{
    public class Logger
    {
        private CurrentState state = new CurrentState();

        public void NextTurn()
        {
            state.Clear();
        }

        /// <summary>
        /// В запись пойдут клетки после передвижения и до всех эффектов, и все эффекты в actions
        /// </summary>
        /// <param name="printer"></param>
        public void PrintState( ILogPrinter printer )
        {
            printer.Print( state );
        }

        /// <summary>
        /// Логирование после передвижения и до разрешения всех эффектов
        /// </summary>
        /// <param name="id"></param>
        /// <param name="playerId"></param>
        /// <param name="hp"></param>
        /// <param name="position"></param>
        public void LogSpellPart( int id, int playerId, int hp, Vector2 position )
        {
            state.spellParts.Add( new SpellPartInfo( id, playerId, hp, position ) );
        }

        public void LogAction( IActionType action )
        {
            state.actions.Add( action );
        }
    }
}
