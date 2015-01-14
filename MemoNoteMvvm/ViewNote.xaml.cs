using MemoNoteMvvm.ViewModel;
using System;
using System.Threading;
using System.Windows;

namespace MemoNoteMvvm
{
    /// <summary>
    /// Description for ViewNote.
    /// </summary>
    public partial class ViewNote : Window
    {
        /// <summary>
        /// Initializes a new instance of the ViewNote class.
        /// </summary>
        public ViewNote()
        {
            
           InitializeComponent();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        //    MessageBox.Show("Bravo!");
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        }
}