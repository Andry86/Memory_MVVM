using System.Windows;
using MemoNoteMvvm.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using MemoNoteMvvm.Helpers;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Command;
using System.Timers;
using NuGet.Commands;
using System.Windows.Input;

namespace MemoNoteMvvm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
      //  public ICommand vedi { get { return new DelegateCommand(vedit); } }
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();


           Messenger.Default.Register<OpenWindowMessage>(this,message => {
           var noteWindows = SimpleIoc.Default.GetInstance<ViewModelNote>();
           noteWindows.question = message.Argument;
           noteWindows.note = message.ArgumentSecond;
           var noteWindowView = new ViewNote()
           {
               DataContext = noteWindows
           };
           var result = noteWindowView.ShowDialog() ?? false;
        }); 
           
        //    RelayCommand OpenModalDialog ;
            

        }

        private void NotificationMessageReceived(NotificationMessage msg)
        {
            if (msg.Notification == "ShowView2")
            {
                var view = new ViewNote();
                view.Show();
            }
        }
     
    }
}