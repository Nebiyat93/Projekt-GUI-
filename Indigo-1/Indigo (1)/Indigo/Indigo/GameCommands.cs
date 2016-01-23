using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Indigo
{
    public static class GameCommands
    {
        private static RoutedUICommand newGameSinglePlayer;
        private static RoutedUICommand options;


        static GameCommands()
        {
            CreateNewGameSinglePlayerCommand();
            CreateOptionsCommand();
        }

        private static void CreateOptionsCommand()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.F5));
            options = new RoutedUICommand("Options", "Options", typeof(GameCommands), inputs);
        }

        private static void CreateNewGameSinglePlayerCommand()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.F3));
            newGameSinglePlayer = new RoutedUICommand("New Game (Single Player)", "NewGameSinglePlayer", typeof(GameCommands), inputs);
        }

        public static RoutedUICommand Options
        {
            get
            {
                return options;
            }
        }

        public static RoutedUICommand NewGameSinglePlayer
        {
            get
            {
                return newGameSinglePlayer;
            }
        }     
    }
}
