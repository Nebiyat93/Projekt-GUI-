using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;

namespace Indigo
{
    public class SinglePlayerGameController : GameController
    {


        public SinglePlayerGameController(Grid gameGrid, string iconSet)
            : base(gameGrid, iconSet)
        {

        }
    }
}
