using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Pacman
{
    public delegate void CollisionDelegate();

    public delegate void MoveDelegate();
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double Speed = 0;
        private const double Step = 5;
        public Rect Pacman;
        public TranslateTransform TransPacman = new TranslateTransform();
        public DoubleAnimation AnimationX = new DoubleAnimation();
        public DoubleAnimation AnimationY = new DoubleAnimation();
        public string SideCollision;
        public Point StartPosition = new Point(0,0); 
        public int Life = 3;
        public int CountApple = 0;
        public List<Image> Apples;
        private Storyboard _storyboardGhost;
        public event CollisionDelegate CollisionEvent;
        //public Thread th;

        public MainWindow()
        {
            InitializeComponent();
            KeyDown += Moving;
            CollisionEvent += IsCollisionGhost;
            CollisionEvent += IsEatApple;
            Pacman = new Rect(StartPosition.X, StartPosition.Y, ImgPacman.Width, ImgPacman.Height);
            ImgPacman.RenderTransform = TransPacman;
            Apples = new List<Image>{ ImgApple1, ImgApple2, ImgApple3, ImgApple4, ImgApple5, ImgApple6, ImgApple7, ImgApple8, ImgApple9, ImgApple10 };
            GhostStartMove();
        }



        #region Движение пакмена по событию (нажатие на клавиши - стрелочки)

        private void Moving(object sender, KeyEventArgs e)
        {
            CurrentLocation(e);
            OnCollisionEvent();
            switch (e.Key)
            {
                case Key.Right:
                    SideCollision = "right";
                    if (Pacman.Right < Win.Width)
                    {
                        AnimationX = new DoubleAnimation(Pacman.X + Step, TimeSpan.FromMilliseconds(Speed));
                        TransPacman.BeginAnimation(TranslateTransform.XProperty, AnimationX);
                        AnimateY();
                        while (IsCollisionWall() && SideCollision == "right")
                        {
                            AnimationX = new DoubleAnimation(Pacman.X--, TimeSpan.FromMilliseconds(Speed));
                            TransPacman.BeginAnimation(TranslateTransform.XProperty, AnimationX);
                            AnimateY();
                        }
                    }
                    break;
                case Key.Left:
                    SideCollision = "left";
                    if (Pacman.Left > 0)
                    {

                        AnimationX = new DoubleAnimation(Pacman.X - Step, TimeSpan.FromMilliseconds(Speed));
                        TransPacman.BeginAnimation(TranslateTransform.XProperty, AnimationX);
                        AnimateY();

                        while (IsCollisionWall() && SideCollision == "left")
                        {
                            AnimationX = new DoubleAnimation(Pacman.X++, TimeSpan.FromMilliseconds(Speed));
                            TransPacman.BeginAnimation(TranslateTransform.XProperty, AnimationX);
                            AnimateY();
                        }
                    }
                    break;
                case Key.Up:
                    SideCollision = "top";
                    if (Pacman.Top > 0)
                    {
                        AnimateX();
                        AnimationY = new DoubleAnimation(Pacman.Y - Step, TimeSpan.FromMilliseconds(Speed));
                        TransPacman.BeginAnimation(TranslateTransform.YProperty, AnimationY);

                        while (IsCollisionWall() && SideCollision == "top")
                        {
                            AnimateX();
                            AnimationY = new DoubleAnimation(Pacman.Y++, TimeSpan.FromMilliseconds(Speed));
                            TransPacman.BeginAnimation(TranslateTransform.YProperty, AnimationY);
                        }


                    }
                    break;
                case Key.Down:
                    SideCollision = "bottom";
                    if (Pacman.Bottom < Win.Height - Step * 2)
                    {
                        AnimateX();
                        AnimationY = new DoubleAnimation(Pacman.Y + Step, TimeSpan.FromMilliseconds(Speed));
                        TransPacman.BeginAnimation(TranslateTransform.YProperty, AnimationY);
                        while (IsCollisionWall() && SideCollision == "bottom")
                        {
                            AnimationY = new DoubleAnimation(Pacman.Y--, TimeSpan.FromMilliseconds(Speed));
                            TransPacman.BeginAnimation(TranslateTransform.YProperty, AnimationY);
                        }
                    }
                    break;
            }
        }
        #endregion

        #region Проверки на столкновение со стеной, привидениями и на съедение яблока
        //private double CurLoc()
        //{
        //    var result = 0.0;
        //    var walls = new[]{ WallA1, WallA2, WallA3, WallA4,WallA5,WallA6,WallA7,WallA8,WallC1,WallC2,WallC3
        //    , WallM1,WallM2,WallM3,WallM4,WallM5,WallN1,WallN2,WallN3,WallP1,WallP2,WallP3,WallP4};
        //    foreach (var wall in walls)
        //    {
        //        var rectWall = GetRect(wall);
        //        if (Pacman.IntersectsWith(rectWall))
        //        {
        //            var rect = Rect.Intersect(Pacman, rectWall);
        //            result = rectWall.Width - rect.Width;
        //        }
        //    }
        //    return result;
        //}

        private bool IsCollisionWall()
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

        private void IsCollisionGhost()
        {
            var ghosts = new[] { ImgGhostWhite, ImgGhostRed, ImgGhostYellow, ImgGhostViolet };
            foreach (var ghost in ghosts)
            {
                var rectGhost = GetRect(ghost);
                if (!Pacman.IntersectsWith(rectGhost)) continue;
                GoToStart();
                MissLife();
            }
        }

        private void IsEatApple()
        {
            var flag = false;
            for (var apple = 0; apple < Apples.Count; apple++)
            {
                var rectApple = GetRect(Apples[apple]);
                if (Pacman.IntersectsWith(rectApple))
                {
                    SetCollapsedImage(Apples[apple]);
                    Apples.RemoveAt(apple);
                    Victory.Visibility = Apples.Count == 0 ? Visibility.Visible : Visibility.Hidden;
                    PacmanScore.Content = ++CountApple;
                    
                }
            }
            if (Victory.IsVisible)
            {
                GhostStop();
            }
        }
        #endregion

        #region Потеря жизни

        private void MissLife()
        {
            switch (Life)
            {
                case 1:
                    SetCollapsedImage(Life1);
                    Life--;
                    SetVisibleImage(GameOver);
                    break;
                case 2:
                    SetCollapsedImage(Life2);
                    Life--;
                    break;
                case 3:
                    Life--;
                    SetCollapsedImage(Life3);
                    break;
            }
        }
        #endregion

        #region Вспомогающие методы (уменьшение дублированого кода etc.)

        private void AnimateX()
        {
            AnimationX = new DoubleAnimation(Pacman.X, TimeSpan.FromMilliseconds(Speed));
            TransPacman.BeginAnimation(TranslateTransform.XProperty, AnimationX);
        }

        private void AnimateY()
        {
            AnimationY = new DoubleAnimation(Pacman.Y, TimeSpan.FromMilliseconds(Speed));
            TransPacman.BeginAnimation(TranslateTransform.YProperty, AnimationY);
        }

        private Rect GetRect(FrameworkElement element)
        {
            var transElement = element.TransformToVisual(this);
            return transElement.TransformBounds(new Rect(new Size(element.ActualWidth, element.ActualHeight)));
        }


        private void CurrentLocation(KeyEventArgs e)
        {
            if (Key.Right == e.Key || Key.Left == e.Key)
                Pacman.X = TransPacman.X;
            if (Key.Up == e.Key || Key.Down == e.Key)
                Pacman.Y = TransPacman.Y;
        }

        private void GoToStart()
        {
            Pacman.X = 0;
            Pacman.Y = 0;
        }

        private static void SetCollapsedImage(UIElement img)
        {
            if (img.Visibility == Visibility.Visible || img.Visibility == Visibility.Hidden)
                img.Visibility = Visibility.Collapsed;
        }

        private static void SetVisibleImage(UIElement img)
        {
            if (img.Visibility == Visibility.Hidden)
                img.Visibility = Visibility.Visible;
        }
        #endregion

        #region Старт движение привидений
        private void GhostStartMove()
        {
            _storyboardGhost = FindResource("MoveGhost") as Storyboard;
            _storyboardGhost?.Begin();
        }
        #endregion

        

        #region Остановка движения привидений
        private void GhostStop()
        {
            _storyboardGhost.Pause();
        }
        #endregion

        public virtual void OnCollisionEvent()
        {
            CollisionEvent?.Invoke();
        }
    }
}

