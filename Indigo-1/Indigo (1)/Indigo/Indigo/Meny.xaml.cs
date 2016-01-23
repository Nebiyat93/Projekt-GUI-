using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Indigo
{
    /// <summary>
    /// Interaction logic for Meny.xaml
    /// </summary>
    public partial class Meny : Window
    {
        MainWindow win = new MainWindow();
        public Meny()
        {
            
            win.Show();
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e) // stäng knappen.
        {
            this.Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // starta knappen
        {
            this.Close();
            
            
            
        }

      
    }
}
