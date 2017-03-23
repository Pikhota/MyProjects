using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Pacman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            KeyDown += Moving;
            Pac.Trans = new TranslateTransform();
            Pac.Speed = 1500;
            Pacman.RenderTransform = Pac.Trans;
        }

        #region Движение пакмена по событию (нажатие на клавиши - стрелочки)
        private void Moving(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    Pac.X = Pac.Trans.X;
                    while (Pac.X <= Width - Pacman.Width - Pacman.RadiusX / 2)
                    {
                        var animationX = new DoubleAnimation(Pac.X++, TimeSpan.FromMilliseconds(Pac.Speed));
                        Pac.Trans.BeginAnimation(TranslateTransform.XProperty, animationX);
                        var animationY = new DoubleAnimation(Pac.Trans.Y, TimeSpan.FromMilliseconds(Pac.Speed));
                        Pac.Trans.BeginAnimation(TranslateTransform.YProperty, animationY);
                    }
                    break;
                case Key.Left:
                    Pac.X = Pac.Trans.X;
                    while (Pac.X >= 0)
                    {
                        var animationX = new DoubleAnimation(Pac.X--, TimeSpan.FromMilliseconds(Pac.Speed));
                        Pac.Trans.BeginAnimation(TranslateTransform.XProperty, animationX);
                        var animationY = new DoubleAnimation(Pac.Trans.Y, TimeSpan.FromMilliseconds(Pac.Speed));
                        Pac.Trans.BeginAnimation(TranslateTransform.YProperty, animationY);
                    }
                    break;
                case Key.Up:
                    Pac.Y = Pac.Trans.Y;
                    while (Pac.Y >= 0)
                    {
                        var animationX = new DoubleAnimation(Pac.Trans.X, TimeSpan.FromMilliseconds(Pac.Speed));
                        Pac.Trans.BeginAnimation(TranslateTransform.XProperty, animationX);
                        var animationY = new DoubleAnimation(Pac.Y--, TimeSpan.FromMilliseconds(Pac.Speed));
                        Pac.Trans.BeginAnimation(TranslateTransform.YProperty, animationY);
                    }
                    break;
                case Key.Down:
                    Pac.Y = Pac.Trans.Y;
                    while (Pac.Y <= (Height - 25) - Pacman.Height - Pacman.RadiusY / 2)
                    {
                        var animationX = new DoubleAnimation(Pac.Trans.X, TimeSpan.FromMilliseconds(Pac.Speed));
                        Pac.Trans.BeginAnimation(TranslateTransform.XProperty, animationX);
                        var animationY = new DoubleAnimation(Pac.Y++, TimeSpan.FromMilliseconds(Pac.Speed));
                        Pac.Trans.BeginAnimation(TranslateTransform.YProperty, animationY);
                    }
                    break;
            }
        }
        #endregion

    }
}
