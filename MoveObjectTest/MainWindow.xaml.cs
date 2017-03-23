using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MoveObjectTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Point Point { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            

            KeyDown += Moving;
            Pos.Trans = new TranslateTransform();
            Pos.Speed = 2000;
            Pacman.RenderTransform = Pos.Trans;
        }

        private void Moving(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    Pos.X = Pos.Trans.X;
                    while (Pos.X <= Width - Pacman.Width - Pacman.RadiusX / 2)
                    {
                        var animationX = new DoubleAnimation(Pos.X++, TimeSpan.FromMilliseconds(Pos.Speed));
                        Pos.Trans.BeginAnimation(TranslateTransform.XProperty, animationX);
                        var animationY = new DoubleAnimation(Pos.Trans.Y, TimeSpan.FromMilliseconds(Pos.Speed));
                        Pos.Trans.BeginAnimation(TranslateTransform.YProperty, animationY);
                    }
                    break;
                case Key.Left:
                    /*Pos.Vector = VisualTreeHelper.GetOffset(Pacman);
                    Point = new Point(Pos.Vector.X, Pos.Vector.Y);*/
                    Pos.X = Pos.Trans.X;
                    while (Pos.X >= 0)
                    {
                        var animationX = new DoubleAnimation(Pos.X--, TimeSpan.FromMilliseconds(Pos.Speed));
                        Pos.Trans.BeginAnimation(TranslateTransform.XProperty, animationX);
                        var animationY = new DoubleAnimation(Pos.Trans.Y, TimeSpan.FromMilliseconds(Pos.Speed));
                        Pos.Trans.BeginAnimation(TranslateTransform.YProperty, animationY);
                    }
                    break;
                case Key.Up:
                    Pos.Y = Pos.Trans.Y;
                    while(Pos.Y >= 0)
                    {
                        var animationX = new DoubleAnimation(Pos.Trans.X, TimeSpan.FromMilliseconds(Pos.Speed));
                        Pos.Trans.BeginAnimation(TranslateTransform.XProperty, animationX);
                        var animationY = new DoubleAnimation(Pos.Y--, TimeSpan.FromMilliseconds(Pos.Speed));
                        Pos.Trans.BeginAnimation(TranslateTransform.YProperty, animationY);
                    }
                    break;
                case Key.Down:

                    Pos.Y = Pos.Trans.Y;
                    while(Pos.Y <= (Height - 25) - Pacman.Height - Pacman.RadiusY / 2)
                    {
                        var animationX = new DoubleAnimation(Pos.Trans.X, TimeSpan.FromMilliseconds(Pos.Speed));
                        Pos.Trans.BeginAnimation(TranslateTransform.XProperty, animationX);
                        var animationY = new DoubleAnimation(Pos.Y++, TimeSpan.FromMilliseconds(Pos.Speed));
                        Pos.Trans.BeginAnimation(TranslateTransform.YProperty, animationY);
                    }
                    break;
            }
        }
    }
}
