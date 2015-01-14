using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoNoteMvvm.Helpers
{
    public class OpenWindowMessage
    {
        public WindowType Type { get; set; }
        public string Argument { get; set; }
        public string ArgumentSecond { get; set; }

    }
}
