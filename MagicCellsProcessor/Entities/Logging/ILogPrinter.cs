using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCellsProcessor.Entities.Logging
{
    public interface ILogPrinter
    {
        string FileNameToSave
        {
            get;
        }

        void Print( CurrentState state );
    }
}
