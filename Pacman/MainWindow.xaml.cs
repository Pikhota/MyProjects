using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Pacman
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Pac Pac = new Pac();
        public DoubleAnimation AnimationX = new DoubleAnimation();
        public DoubleAnimation AnimationY = new DoubleAnimation();
        public RectangleGeometry GeometryPac = new RectangleGeometry();
        private const double Step = 5;

        public MainWindow()
        {

            InitializeComponent();

            KeyDown += Moving;

            #region Создание пакмена
            Pac.PacPath.Stroke = Brushes.Yellow;
            Pac.PacPath.StrokeThickness = 1;
            Pac.PacPath.Fill = Pac.SolidColorBrush;
            GeometryPac.RadiusX = 26;
            GeometryPac.RadiusY = 26;
            GeometryPac.Rect = Pac.Pacman;
            Pac.PacPath.Data = GeometryPac;
            Win.Children.Add(Pac.PacPath);
            #endregion
            Pac.PacPath.RenderTransform = Pac.Trans;
        }

        #region Проверка на столкновение со стеной
        private bool CollisionWall()
        {
            var flag = false;
            Rectangle []walls = new Rectangle[]{ WallA1, WallA2, WallA3, WallA4,WallA5,WallA6,WallA7,WallA8,WallC1,WallC2,WallC3
            , WallM1,WallM2,WallM3,WallM4,WallM5,WallN1,WallN2,WallN3,WallP1,WallP2,WallP3,WallP4};
            foreach(var wall in walls)
            {
                Rect rect = GetWall(wall);
                if(Pac.Pacman.IntersectsWith(rect))
                {
                    flag = true;
                }
            }
            return flag;            
        }
        #endregion

        #region Движение пакмена по событию (нажатие на клавиши - стрелочки)
        private void Moving(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    Pac.Pacman.X = Pac.Trans.X;
                    if (Pac.Pacman.X < Win.Width - Pac.Pacman.Width - GeometryPac.RadiusX / 2 - Step)
                    {
                        if (CollisionWall())
                        {
                            AnimationX = new DoubleAnimation(Pac.Pacman.X - Step, TimeSpan.FromMilliseconds(Pac.Speed));
                            Pac.Trans.BeginAnimation(TranslateTransform.XProperty, AnimationX);
                            AnimateY();
                        }
                        else
                        {
                            AnimationX = new DoubleAnimation(Pac.Pacman.X + Step, TimeSpan.FromMilliseconds(Pac.Speed));
                            Pac.Trans.BeginAnimation(TranslateTransform.XProperty, AnimationX);
                            AnimateY();
                        }
                    }
                    break;
                case Key.Left:
                    Pac.Pacman.X = Pac.Trans.X;
                    if (Pac.Pacman.X > 0)
                    {
                        if (CollisionWall())
                        {
                            AnimationX = new DoubleAnimation(Pac.Pacman.X + Step, TimeSpan.FromMilliseconds(Pac.Speed));
                            Pac.Trans.BeginAnimation(TranslateTransform.XProperty, AnimationX);
                            AnimateY();
                        }
                        else
                        {
                            AnimationX = new DoubleAnimation(Pac.Pacman.X - Step, TimeSpan.FromMilliseconds(Pac.Speed));
                            Pac.Trans.BeginAnimation(TranslateTransform.XProperty, AnimationX);
                            AnimateY();
                        }
                    }
                    break;
                case Key.Up:
                    Pac.Pacman.Y = Pac.Trans.Y;
                    if (Pac.Pacman.Y > 0)
                    {
                        if (CollisionWall())
                        {
                            AnimateX();
                            AnimationY = new DoubleAnimation(Pac.Pacman.Y + Step, TimeSpan.FromMilliseconds(Pac.Speed));
                            Pac.Trans.BeginAnimation(TranslateTransform.YProperty, AnimationY);
                        }
                        else
                        {
                            AnimationX = new DoubleAnimation(Pac.Trans.X, TimeSpan.FromMilliseconds(Pac.Speed));
                            Pac.Trans.BeginAnimation(TranslateTransform.XProperty, AnimationX);
                            AnimationY = new DoubleAnimation(Pac.Pacman.Y - Step, TimeSpan.FromMilliseconds(Pac.Speed));
                            Pac.Trans.BeginAnimation(TranslateTransform.YProperty, AnimationY);
                        }
                    }
                    break;
                case Key.Down:
                    Pac.Pacman.Y = Pac.Trans.Y;
                    if (Pac.Pacman.Y < Win.Height - Pac.Pacman.Height - GeometryPac.RadiusX / 2 - Step)
                    {
                        if (CollisionWall())
                        {
                            AnimateX();
                            AnimationY = new DoubleAnimation(Pac.Pacman.Y - Step, TimeSpan.FromMilliseconds(Pac.Speed));
                            Pac.Trans.BeginAnimation(TranslateTransform.YProperty, AnimationY);
                        }
                        else
                        {
                            AnimateX();
                            AnimationY = new DoubleAnimation(Pac.Pacman.Y + Step, TimeSpan.FromMilliseconds(Pac.Speed));
                            Pac.Trans.BeginAnimation(TranslateTransform.YProperty, AnimationY);
                        }
                    }
                    break;
            }
        }
        #endregion
        #region Вспомогающие методы (уменьшение дублированого кода и т.п.)
        private void AnimateX()
        {
            AnimationX = new DoubleAnimation(Pac.Trans.X, TimeSpan.FromMilliseconds(Pac.Speed));
            Pac.Trans.BeginAnimation(TranslateTransform.XProperty, AnimationX);
        }

        private void AnimateY()
        {
            AnimationY = new DoubleAnimation(Pac.Trans.Y, TimeSpan.FromMilliseconds(Pac.Speed));
            Pac.Trans.BeginAnimation(TranslateTransform.YProperty, AnimationY);
        }

        private Rect GetWall(Rectangle wall)
        {
            GeneralTransform TransWall = wall.TransformToVisual(this);
            return TransWall.TransformBounds(new Rect(new Size(wall.ActualWidth, wall.ActualHeight)));
        }
        #endregion
    }
}
