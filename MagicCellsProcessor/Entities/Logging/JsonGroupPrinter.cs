using MagicCellsProcessor.Entities.Logging.DataStructures;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace MagicCellsProcessor.Entities.Logging
{
    public class JsonGroupPrinter : ILogPrinter
    {
        public string FileNameToSave
        {
            get
            {
                return fileNameToSave;
            }
        }

        private string fileNameToSave;

        private List<object> steps = new List<object>();

        public JsonGroupPrinter()
        {
            fileNameToSave = ConfigurationManager.AppSettings[ "outputFileForPlayer" ];
        }

        public void Print( CurrentState state )
        {
            steps.Add( new CurrentState( state ) );

            //JavaScriptSerializer ser = new JavaScriptSerializer();

            //var seriliazed = ser.Serialize( state );

            //StreamWriter wr = new StreamWriter( fileNameToSave, true );

            //wr.WriteLine( seriliazed );

            //wr.Close();
        }

        public void PrintAll()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();

            var serialized = ser.Serialize( steps );

            StreamWriter wr = new StreamWriter( fileNameToSave );

            wr.WriteLine( serialized );

            wr.Close();
        }
    }
}
