using System;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
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
        private const double Speed = 0;
        private const double Step = 5;
        public Rect Pacman;
        public TranslateTransform Trans = new TranslateTransform();
        public DoubleAnimation AnimationX = new DoubleAnimation();
        public DoubleAnimation AnimationY = new DoubleAnimation();
        public string SideCollision;
        public int LifeCount = 100;

        public MainWindow()
        {
            InitializeComponent();
            KeyDown += Moving;
            Pacman = new Rect(0, 0, ImgPacman.Width, ImgPacman.Height);
            ImgPacman.RenderTransform = Trans;

        }

        #region Проверка на столкновение со стеной

        private bool CollisionWall()
        {
            var flag = false;
            var walls = new[]{ WallA1, WallA2, WallA3, WallA4,WallA5,WallA6,WallA7,WallA8,WallC1,WallC2,WallC3
            , WallM1,WallM2,WallM3,WallM4,WallM5,WallN1,WallN2,WallN3,WallP1,WallP2,WallP3,WallP4};
            foreach (var wall in walls)
            {
                var rectWall = GetRect(wall);
                if (Pacman.IntersectsWith(rectWall))
                {
                    flag = true;
                }
            }
            return flag;
        }

        private bool CollisionGhost()
        {
            var flag = false;
            var ghosts = new[] { ImgGhostWhite, ImgGhostRed, ImgGhostYellow, ImgGhostViolet };
            foreach (var ghost in ghosts)
            {
                var rectGhost = GetRect(ghost);
                if (Pacman.IntersectsWith(rectGhost))
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
                    CurrentLocation(e);
                    if (Pacman.Right < Win.Width - Step * 2)
                    {
                        
                        if (CollisionWall() && SideCollision != "left")
                        {
                            SideCollision = "right";
                            if (SideCollision == "right")
                            {
                                while (CollisionWall())
                                {
                                    AnimationX = new DoubleAnimation(Pacman.X--, TimeSpan.FromMilliseconds(Speed));
                                    Trans.BeginAnimation(TranslateTransform.XProperty, AnimationX);
                                    AnimateY();
                                }
                            }
                        }
                        else
                        {
                            AnimationX = new DoubleAnimation(Pacman.X + Step, TimeSpan.FromMilliseconds(Speed));
                            Trans.BeginAnimation(TranslateTransform.XProperty, AnimationX);
                            AnimateY();
                            SideCollision = string.Empty;
                        }
                    }
                    break;
                case Key.Left:
                    CurrentLocation(e);
                    if (Pacman.Left > 0)
                    {

                        if (CollisionWall() && SideCollision !="right")
                        {
                            SideCollision = "left";
                            if (SideCollision == "left")
                            {
                                while (CollisionWall())
                                {
                                    AnimationX = new DoubleAnimation(Pacman.X++, TimeSpan.FromMilliseconds(Speed));
                                    Trans.BeginAnimation(TranslateTransform.XProperty, AnimationX);
                                    AnimateY();
                                }
                            }
                        }
                        else
                        {
                            AnimationX = new DoubleAnimation(Pacman.X - Step, TimeSpan.FromMilliseconds(Speed));
                            Trans.BeginAnimation(TranslateTransform.XProperty, AnimationX);
                            AnimateY();
                            SideCollision = string.Empty;
                        }
                    }
                    break;
                case Key.Up:
                    CurrentLocation(e);
                    if (Pacman.Top > 0)
                    {
                        if (CollisionWall() && SideCollision != "bottom")
                        {
                            SideCollision = "top";
                            if (SideCollision == "top")
                            {
                                while (CollisionWall())
                                {
                                    AnimateX();
                                    AnimationY = new DoubleAnimation(Pacman.Y++, TimeSpan.FromMilliseconds(Speed));
                                    Trans.BeginAnimation(TranslateTransform.YProperty, AnimationY);
                                }
                            }
                        }
                        else
                        {
                            AnimateX();
                            AnimationY = new DoubleAnimation(Pacman.Y - Step, TimeSpan.FromMilliseconds(Speed));
                            Trans.BeginAnimation(TranslateTransform.YProperty, AnimationY);
                            SideCollision = string.Empty;
                        }

                    }
                    break;
                case Key.Down:
                    CurrentLocation(e);
                    if (Pacman.Bottom < Win.Height - Step * 2)
                    {
                        if (CollisionWall() && SideCollision != "top")
                        {
                            SideCollision = "bottom";
                            if (SideCollision == "bottom")
                                while (CollisionWall())
                                {
                                    AnimateX();
                                    AnimationY = new DoubleAnimation(Pacman.Y--, TimeSpan.FromMilliseconds(Speed));
                                    Trans.BeginAnimation(TranslateTransform.YProperty, AnimationY);
                                }
                        }
                        else
                        {
                            AnimateX();
                            AnimationY = new DoubleAnimation(Pacman.Y + Step, TimeSpan.FromMilliseconds(Speed));
                            Trans.BeginAnimation(TranslateTransform.YProperty, AnimationY);
                            SideCollision = string.Empty;
                        }
                    }
                    break;
            }
            if (CollisionGhost())
            {
                Life.Content = LifeCount--;
            }
        }
        #endregion
        #region Вспомогающие методы (уменьшение дублированого кода и т.п.)
        private void AnimateX()
        {
            AnimationX = new DoubleAnimation(Pacman.X, TimeSpan.FromMilliseconds(Speed));
            Trans.BeginAnimation(TranslateTransform.XProperty, AnimationX);
        }

        private void AnimateY()
        {
            AnimationY = new DoubleAnimation(Pacman.Y, TimeSpan.FromMilliseconds(Speed));
            Trans.BeginAnimation(TranslateTransform.YProperty, AnimationY);
        }

        private Rect GetRect(Rectangle wall)
        {
            var transWall = wall.TransformToVisual(this);
            return transWall.TransformBounds(new Rect(new Size(wall.ActualWidth, wall.ActualHeight)));
        }

        private Rect GetRect(Image ghost)
        {
            var transGhost = ghost.TransformToVisual(this);
            return transGhost.TransformBounds(new Rect(new Size(ghost.ActualWidth, ghost.ActualHeight)));
        }

        private void CurrentLocation(KeyEventArgs e)
        {
            if (Key.Right == e.Key || Key.Left == e.Key)
                Pacman.X = Trans.X;
            if (Key.Up == e.Key || Key.Down == e.Key)
                Pacman.Y = Trans.Y;
        }
        #endregion
    }
}
