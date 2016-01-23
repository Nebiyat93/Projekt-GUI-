using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indigo
{
    public class GameOptions
    {

        private string selectedIconSet = "misc";

        public string SelectedIconSet
        {
            get { return selectedIconSet; }
            set { selectedIconSet = value; }
        }

        private List<string> availableIconSets = new List<string>()
        {
            "misc",
            "monster"
        };

        public List<string> AvailableIconSets
        {
            get { return availableIconSets; }
        }


        public GameOptions()
        {

        }
    }
}
