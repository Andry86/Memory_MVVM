using GalaSoft.MvvmLight;
using MemoNoteMvvm.Helpers;
using MemoNoteMvvm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace MemoNoteMvvm.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelNote : ViewModelBase
    {
        public ICommand No { get { return new DelegateCommand(butNo); } }
        public ICommand Si { get { return new DelegateCommand(butSi); } }

        private readonly IDataService _dataService;
        public string _note;
        private string _question;

        public string question{       
            get
            {
                return _question;
            }

            set
            {
                if (_question == value)
                {
                    return;
                }

                _question = value;
                RaisePropertyChanged(() => question);
            }
        }

        public string note
        {
            get
            {
                return _note;
            }

            set
            {
                if (_note == value)
                {
                    return;
                }

                _note = value;
                RaisePropertyChanged(() => note);
            }
        }

        
       public ViewModelNote(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                });
            RaisePropertyChanged(() =>note);

        }

       public void butNo()
       {

          MessageBox.Show(note);

       }

       public void butSi()
       {

           MessageBox.Show("Bravo!!!");

       }
    }
}

