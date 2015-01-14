using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; // per INotifyPropertyChanged 
using System.Windows.Input;
using System.Data.SqlClient;
using System.Windows;
using System.Data;
using MemoNoteMvvm.Helpers;
using System.Timers;
using System.Threading; // per ICommand
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.IO;

public delegate void mostraDel(object source);

namespace MemoNoteMvvm.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Properties
        string note, result;
        string question;
        System.Timers.Timer tem = new System.Timers.Timer(2000);
        public RelayCommand ShowView2Command { private set; get; }

        public string Note
        {
            get { return note; }
            set
            {
                note = value;
                RaisePropertyChanged(() => Note);
            }
        }
        public string Question
        {
            get { return question; }
            set
            {
                question = value;
                RaisePropertyChanged(() => Question);
            }
        }

        IModel model;
        #endregion

        #region Command
        public ICommand AddMemo { get { return new DelegateCommand(add); } }
        public RelayCommand OpenModalDialog { get; private set; }

        #endregion

        //Costruttore
        public MainViewModel()
        {
            ShowView2Command = new RelayCommand(ShowView2CommandExecute);

            Messenger.Default.Register<string>(this, s => result = s);
            model = new ModelMemo();
            model.vediMemoEvent += printNote;
            tem.Start();
            tem.Elapsed += mostraEvent;

            OpenModalDialog = new RelayCommand(() => Messenger.Default.Send<OpenWindowMessage>
                (new OpenWindowMessage() { Type = WindowType.kModal, Argument = model.Question, ArgumentSecond = model.Note }));

        }

        void add()
        {
            if ((this.note != null) && (question != null))
            {
                model.addMemo(note, question);
            }
            else
            {
                MessageBox.Show("errore");
            }
        }

        bool CanAumentaAnnoExecute()
        { return true; }


        public void mostraEvent(object source, ElapsedEventArgs e)
        {
            model.vediMemo();


        }

        public void ShowView2CommandExecute()
        {
            Messenger.Default.Send(new NotificationMessage("ShowView2"));
        }

        [STAThread]
        public void printNote(object sender)
        {
            var thread = new Thread(new ThreadStart(DisplayNote));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();


        }
        public void DisplayNote()
        {
            OpenModalDialog.Execute(this);
            //   thread.Abort();
        }


        public void startTime(object source)
        {
            tem.Start();

        }

        //public event PropertyChangedEventHandler PropertyChanged;
    }
}
