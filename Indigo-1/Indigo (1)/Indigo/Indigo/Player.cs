using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Indigo
{
    public class Player : INotifyPropertyChanged
    {
        private int score;
        private bool active;
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public int Score
        {
            get { return score; }
            set
            {
                score = value;
                RaiseNotifyPropertyChanged("Score");
            }
        }

        /// <summary>
        /// Indicates whether this player is currently allowed to draw cards.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
                RaiseNotifyPropertyChanged("IsActive");
            }

        }


        private void RaiseNotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
