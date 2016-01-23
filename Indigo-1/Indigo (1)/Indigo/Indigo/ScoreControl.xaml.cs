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

namespace Indigo
{
    /// <summary>
    /// Interaction logic for ScoreControl.xaml
    /// </summary>
    public partial class ScoreControl : UserControl
    {

        public static DependencyProperty ScoreProperty;


        public ScoreControl()
        {
            InitializeComponent();

            //Create Rows
            for (int x = 0; x < 16; x++)
            {
                this.grid.RowDefinitions.Add(new RowDefinition());
            }

            this.DataContextChanged += new DependencyPropertyChangedEventHandler(ScoreControl_DataContextChanged);
        }

        void ScoreControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.grid.Children.Clear();
        }

        static ScoreControl()
        {
            ScoreProperty = DependencyProperty.Register("Score", typeof(int), typeof(ScoreControl),
                            new FrameworkPropertyMetadata(new PropertyChangedCallback(OnScoreChanged)));

        }

        public static void OnScoreChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ScoreControl scoreControl = o as ScoreControl;
            if ((int)e.NewValue == 0) return;
            Image i = new Image();
            i.Source = new BitmapImage(new Uri("images/star.png", UriKind.Relative));
            i.SetValue(Grid.RowProperty, (int)e.NewValue);
            scoreControl.grid.Children.Add(i);
        }

        public int Score
        {
            get
            {
                return (int)GetValue(ScoreProperty);
            }
            set
            {
                SetValue(ScoreProperty, value);
            }

        }
    }
}

