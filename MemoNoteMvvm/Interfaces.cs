using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoNoteMvvm
{
    interface IView
    {

        string vNote { get; set; }
        string vQuestion { get; set; }
        event viewEventHandler ricorda;
    }

    interface IModel
    {
        string Note { get; set; }
        string Question { get; set; }
        event VediMemoEventHandler vediMemoEvent;
        void vediMemo();
        void addMemo(string memo, string question);
    }


}
