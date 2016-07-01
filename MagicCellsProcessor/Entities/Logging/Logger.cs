using MagicCellsProcessor.Entities.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.Logging
{
    public struct SpellPartInfo
    {
        public Vector2 position;
        public int playerId;
        public int id;
        public int hp;

        public SpellPartInfo(int id, int playerId, int hp, Vector2 position)
        {
            this.id = id;
            this.playerId = playerId;
            this.hp = hp;
            this.position = position;
        }

        public SpellPartInfo( SpellPartInfo info )
        {
            id = info.id;
            playerId = info.playerId;
            hp = info.hp;
            position = info.position;
        }
    }

    public class CurrentState
    {
        public List<SpellPartInfo> spellParts = new List<SpellPartInfo>();

        public List<IActionType> actions = new List<IActionType>();

        public CurrentState() { }

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
