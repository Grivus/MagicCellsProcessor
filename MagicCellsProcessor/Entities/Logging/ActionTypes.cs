using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.Logging
{
    public static class ActionsNames
    {
        public static Dictionary<Type, string> name = new Dictionary<Type, string>()
        {
            { typeof( ActionTypeDealDamage ), "dealDamage" },
            { typeof( ActionTypeDie ), "die" }
        };
    }

    public interface IActionType
    {
        IActionType Copy();

        string TypeName
        {
            get;
        }
    }

    public struct ActionTypeDealDamage : IActionType
    {
        public int idFrom;
        public int idTo;
        public int damage;

        public string TypeName
        {
            get
            {
                return ActionsNames.name[ this.GetType() ];
            }
        }

        public ActionTypeDealDamage( int idFrom, int idTo, int damage )
        {
            this.idFrom = idFrom;
            this.idTo = idTo;
            this.damage = damage;
        }

        public IActionType Copy()
        {
            return new ActionTypeDealDamage( idFrom, idTo, damage );
        }
    }

    public struct ActionTypeDie : IActionType
    {
        public int id;

        public ActionTypeDie( int id )
        {
            this.id = id;
        }

        public string TypeName
        {
            get
            {
                return ActionsNames.name[ this.GetType() ];
            }
        }

        public IActionType Copy()
        {
            return new ActionTypeDie( this.id );
        }
    }
}
