using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Collections;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Media;
using System.Diagnostics;
using System.Windows.Media.Effects;

namespace Indigo
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameOptions gameOptions = new GameOptions();
        private GameController gameController;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            gameController = new SinglePlayerGameController(this.gameGrid, gameOptions.SelectedIconSet);
        }


        private void rectangleLeftMouseButtonUp(object sender, MouseButtonEventArgs e)
        {

            Rectangle cardRectangle = e.Source as Rectangle;
            if (cardRectangle == null) return;

            gameController.PickCard(cardRectangle);


        }

        private void OptionsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Meny meny = new Meny();
            meny.Show();
            this.Close();
        }

        private void NewGameSinglePlayerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
            gameController = new SinglePlayerGameController(gameGrid, gameOptions.SelectedIconSet);
            gameController.StartGame();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    

    }
}
