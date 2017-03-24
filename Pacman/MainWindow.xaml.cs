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
            Pac.Speed = 1500;
            Pacman.RenderTransform = Pac.Trans;
            Wall.Xleft = 50;
            Wall.Xright = 110;
        }

        #region Движение пакмена по событию (нажатие на клавиши - стрелочки)
        private void Moving(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    Pac.Speed = 1500;
                    Pac.X = Pac.Trans.X;
                    if (Pac.X > Pacman.Margin.Left / 2)
                    {
                        Pac.Speed = 500;
                    }
                    else if (Pac.X > Pacman.Margin.Left / 4)
                    {
                        Pac.Speed = 1000;
                    }
                    else if (Pac.X < -Pacman.Margin.Left / 2)
                    {
                        Pac.Speed = 2500;
                    }
                    else if (Pac.X < -Pacman.Margin.Left / 4)
                    {
                        Pac.Speed = 1000;
                    }
                    while (Pac.X <= Width - Pacman.Margin.Left - Pacman.Width - Pacman.RadiusX / 2)
                    {
                        var animationX = new DoubleAnimation(Pac.X++, TimeSpan.FromMilliseconds(Pac.Speed));
                        label.Content = Pac.Speed;
                        Pac.Trans.BeginAnimation(TranslateTransform.XProperty, animationX);
                        var animationY = new DoubleAnimation(Pac.Trans.Y, TimeSpan.FromMilliseconds(Pac.Speed));
                        Pac.Trans.BeginAnimation(TranslateTransform.YProperty, animationY);
                    }
                    labelX.Content = Pac.Trans.X;
                    break;
                case Key.Left:
                    Pac.Speed = 1500;
                    Pac.X = Pac.Trans.X;
                    if (Pac.X < -Pacman.Margin.Left / 2)
                    {
                        Pac.Speed = 500;
                    }
                    else if (Pac.X < -Pacman.Margin.Left/4)
                    {
                        Pac.Speed = 1000;
                    }
                    else if (Pac.X > Pacman.Margin.Left / 2)
                    {
                        Pac.Speed = 2500;
                    }
                    else if (Pac.X > Pacman.Margin.Left / 4)
                    {
                        Pac.Speed = 2000;
                    }
                    while (Pac.X >= -Pacman.Margin.Left)
                    {
                        var animationX = new DoubleAnimation(Pac.X--, TimeSpan.FromMilliseconds(Pac.Speed));
                        Pac.Trans.BeginAnimation(TranslateTransform.XProperty, animationX);
                        var animationY = new DoubleAnimation(Pac.Trans.Y, TimeSpan.FromMilliseconds(Pac.Speed));
                        Pac.Trans.BeginAnimation(TranslateTransform.YProperty, animationY);
                        label.Content = Pac.Speed;
                    }
                    labelX.Content = Pac.Trans.X;
                    break;
                case Key.Up:
                    Pac.Speed = 1500;
                    Pac.Y = Pac.Trans.Y;
                    if (Pac.Y < -Pacman.Margin.Top / 2)
                    {
                        Pac.Speed = 1000;
                    }
                    else if (Pac.Y < -Pacman.Margin.Left / 4)
                    {
                        Pac.Speed = 1500;
                    }
                    else if (Pac.Y > -Pacman.Margin.Left / 2)
                    {
                        Pac.Speed = 2000;
                    }
                    else if (Pac.Y > -Pacman.Margin.Left / 4)
                    {
                        Pac.Speed = 2000;
                    }
                    while (Pac.Y >= -Pacman.Margin.Top)
                    {
                        var animationX = new DoubleAnimation(Pac.Trans.X, TimeSpan.FromMilliseconds(Pac.Speed));
                        Pac.Trans.BeginAnimation(TranslateTransform.XProperty, animationX);
                        var animationY = new DoubleAnimation(Pac.Y--, TimeSpan.FromMilliseconds(Pac.Speed));
                        Pac.Trans.BeginAnimation(TranslateTransform.YProperty, animationY);
                        label1.Content = Pac.Speed;
                    }
                    labelY.Content = Pac.Trans.Y;
                    break;
                case Key.Down:
                    Pac.Speed = 1500;
                    Pac.Y = Pac.Trans.Y;
                    if (Pac.Y > -Pacman.Margin.Left / 2)
                    {
                        Pac.Speed = 500;
                    }
                    else if (Pac.Y > -Pacman.Margin.Left / 4)
                    {
                        Pac.Speed = 1500;
                    }
                    else if (Pac.Y < -Pacman.Margin.Top / 2)
                    {
                        Pac.Speed = 2000;
                    }
                    else if (Pac.Y < -Pacman.Margin.Left / 4)
                    {
                        Pac.Speed = 1000;
                    }
                    while (Pac.Y <= Height - 25 - Pacman.Height - Pacman.RadiusY / 2 - Pacman.Margin.Top)
                    {
                        var animationX = new DoubleAnimation(Pac.Trans.X, TimeSpan.FromMilliseconds(Pac.Speed));
                        Pac.Trans.BeginAnimation(TranslateTransform.XProperty, animationX);
                        var animationY = new DoubleAnimation(Pac.Y++, TimeSpan.FromMilliseconds(Pac.Speed));
                        Pac.Trans.BeginAnimation(TranslateTransform.YProperty, animationY);
                        label1.Content = Pac.Speed;
                    }
                    labelY.Content = Pac.Trans.Y;
                    break;
                case Key.Space:
                break;                
            }
        }
        #endregion

    }
}
