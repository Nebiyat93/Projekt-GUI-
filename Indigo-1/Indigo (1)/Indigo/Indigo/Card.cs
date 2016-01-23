using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows;

namespace Indigo
{
    public class Card : INotifyPropertyChanged
    {

        private readonly BitmapImage frontImage;
        private readonly BitmapImage backImage;
        private readonly string name;


        public Card(string name, BitmapImage frontImage, BitmapImage backImage)
        {
            this.name = name;
            this.frontImage = frontImage;
            this.backImage = backImage;

            this.Status = CardState.Covered;
        }

        public string Name
        {
            get { return name; }
        }

        public CardState Status { get; set; }

        public Brush ActiveImage
        {
            get
            {
                if (this.Status == CardState.Covered)
                {
                    var brush = new ImageBrush(backImage);
                    brush.Stretch = Stretch.Uniform;
                    return brush;
                }

                if (this.Status == CardState.Uncovered)
                {
                    var brush = new ImageBrush(frontImage);
                    brush.Stretch = Stretch.Uniform;
                    return brush;
                }

                if (this.Status == CardState.Matched)
                {
                    return new SolidColorBrush(Colors.Transparent);
                }

                throw new InvalidOperationException("Invalid Card State.");
            }
        }

        public void Uncover()
        {
            this.Status = CardState.Uncovered;
            RaiseNotifyChanged("ActiveImage");
        }

        public void Cover()
        {
            this.Status = CardState.Covered;
            RaiseNotifyChanged("ActiveImage");
        }

        public void Match()
        {
            this.Status = CardState.Matched;
            RaiseNotifyChanged("ActiveImage");
        }

        private void RaiseNotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public enum CardState
    {
        Covered,
        Uncovered,
        Matched
    }
}
