using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MemoNoteMvvm.Helpers
{
    class fileLog
    {
        public static void Log(string Message)
        {
            // Creiamo la stringa che verrà visualizzata nel log
            // [04/02/2012 16:59]: Messaggio
            string Line = "[" + DateTime.Now + "]: " + Message + Environment.NewLine;

            // Aggiungiamo la linea al nostro log (chiamato log.txt)
            File.AppendAllText("log.txt", Line);
        }
    }
}
