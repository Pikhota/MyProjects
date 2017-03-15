using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Wpf_ChangeImage
{

    public partial class MainWindow : Window
    {
        readonly Random _ran = new Random();
        readonly BitmapImage _x;   //игрок
        readonly BitmapImage _zero; //язва
        readonly BitmapImage _yazvaDade;
        readonly BitmapImage _yazva;
        private int _course;
        private bool _start;
        private bool _lou;
        private readonly int[] _field;
        private readonly Button[] _btns;
        private readonly Image[] _imgBtns;
        private bool _pleerWinStupidYazva;
        public MainWindow()
        {
            InitializeComponent();
            Uri uriImgBell = new Uri(@"pack://application:,,,/Resources/11111.png", UriKind.Absolute);
            Uri uriImgGiftbox = new Uri(@"pack://application:,,,/Resources/язва2.png", UriKind.Absolute);
            Uri uriImgyazvaDade = new Uri(@"pack://application:,,,/Resources/Язва 4.png", UriKind.Absolute);
            Uri uriImgyazva = new Uri(@"pack://application:,,,/Resources/Язва1.png", UriKind.Absolute);
            _x = GetBitmapImage(uriImgBell);
            _zero = GetBitmapImage(uriImgGiftbox);
            _yazvaDade = GetBitmapImage(uriImgyazvaDade);
            _yazva = GetBitmapImage(uriImgyazva);
            // выставляем изначальные флаги и значения картинок кнопок.
            _btns = new[] { btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8 };
            foreach (var btn in _btns)
            {
                btn.Tag = false;
            }

            _imgBtns = new[] { imgBtn0, imgBtn1, imgBtn2, imgBtn3, imgBtn4, imgBtn5, imgBtn6, imgBtn7, imgBtn8 };
            foreach (var imgBtn in _imgBtns)
            {
                imgBtn.Source = null;
            }
            _start = false;
            _lou = false;
            _field = new int[9];
            _pleerWinStupidYazva = false;
            _course = 0;
            textBox.Text = "Ну что, сыграем?!";
        }

        //присваиваем переменным картинки
        BitmapImage GetBitmapImage(Uri uri)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = uri;
            bitmapImage.EndInit();
            return bitmapImage;
        }
        //метод проверки выиграл ли игрок (перебор выиграшных комбинаций)
        private void WinPayer()
        {
            var row1 = (_field[1] == 2 && _field[2] == 2 && _field[3] == 2);
            var row2 = (_field[8] == 2 && _field[0] == 2 && _field[4] == 2);
            var row3 = (_field[7] == 2 && _field[6] == 2 && _field[5] == 2);
            var col1 = (_field[1] == 2 && _field[8] == 2 && _field[7] == 2);
            var col2 = (_field[2] == 2 && _field[0] == 2 && _field[6] == 2);
            var col3 = (_field[3] == 2 && _field[4] == 2 && _field[5] == 2);
            // Диагональ с верхнего левого угла в нижний правый
            var diag1 = (_field[1] == 2 && _field[0] == 2 && _field[5] == 2);
            // Диагональ с нижнего левого угла в верхний правый
            var diag2 = (_field[7] == 2 && _field[0] == 2 && _field[3] == 2);
            if (row1 || row2 || row3 || col1 || col2 || col3 || diag1 || diag2)
                PlayerWin();

        }
        //метод проверки выиграла ли язва (перебор выиграшных комбинаций)
        private void WinYazva()
        {
            var row1 = (_field[1] == 1 && _field[2] == 1 && _field[3] == 1);
            var row2 = (_field[8] == 1 && _field[0] == 1 && _field[4] == 1);
            var row3 = (_field[7] == 1 && _field[6] == 1 && _field[5] == 1);
            var col1 = (_field[1] == 1 && _field[8] == 1 && _field[7] == 1);
            var col2 = (_field[2] == 1 && _field[0] == 1 && _field[6] == 1);
            var col3 = (_field[3] == 1 && _field[4] == 1 && _field[5] == 1);
            // Диагональ с верхнего левого угла в нижний правый
            var diag1 = (_field[1] == 1 && _field[0] == 1 && _field[5] == 1);
            // Диагональ с нижнего левого угла в верхний правый
            var diag2 = (_field[7] == 1 && _field[0] == 1 && _field[3] == 1);
            if (row1 || row2 || row3 || col1 || col2 || col3 || diag1 || diag2)
                YazvaWin();
           }
        //проверка на ничью
        private void PossiblyDraw()
        {
            if (_field[0] != 0 && _field[1] != 0 && _field[2] != 0 &&
                _field[3] != 0 && _field[4] != 0 && _field[5] != 0 &&
                _field[6] != 0 && _field[7] != 0 && _field[8] != 0)
                Draw();
        }

        private void PlayerWin()
        {
            textBox.Text = " :( Ты победил";
            var player1 = new SoundPlayer();
            player1.Stream = Properties.Resources.no;
            player1.Play();
            //todo
            //стопим игру
            _start = false;
        }
        private void YazvaWin()
        {
            
            textBox.Text = "Ха-ха! Ты проиграл";
            var player = new SoundPlayer();
            player.Stream = Properties.Resources.alienlaf;
            player.Play();
            
            //todo
            //стопим игру
            _start = false;
        }
        private void Draw()
        {
            textBox.Text = "Меня не победить";
            //todo
            //стопим игру
            _pleerWinStupidYazva = true;
            _start = false;
        }
        //чистим все до состояния запуска программы
        private void Clean_Click(object sender, RoutedEventArgs e)
        {
            foreach (var btn in _btns)
            {
                btn.Tag = false;
            }

            foreach (var imgBtn in _imgBtns)
            {
                imgBtn.Source = null;
            }
            for (int key = 0; key < _field.Length; key++)
            {
                _field[key] = 0;
            }
            _course = 0;
            _pleerWinStupidYazva = false;
            _start = false;
            _lou = false;
            image.Source = _yazva;
            var player2 = new SoundPlayer();
            player2.Stream = Properties.Resources.smyv;
            player2.Play();
            textBox.Text = "Ну что, сыграем?!";
        }
        private void Start_Click(object sender, RoutedEventArgs e) //старт
        {
            //Обнуление игры, и разрешение на нажатие кнопок.
            InitializeComponent();
            // выставляем изначальные флаги и значения картинок кнопок.
            //_btns = new[] { btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8 };
            foreach (var btn in _btns)
            {
                btn.Tag = false;
            }

            // _imgBtns = new[] { imgBtn0, imgBtn1, imgBtn2, imgBtn3, imgBtn4, imgBtn5, imgBtn6, imgBtn7, imgBtn8 };
            foreach (var imgBtn in _imgBtns)
            {
                imgBtn.Source = null;
            }
            _start = true;
            for (int key = 0; key < _field.Length; key++)
            {
                _field[key] = 0;
            }

            _pleerWinStupidYazva = false;
            _course = 0;
            textBox.Text = "";
            GoYazva();
        }
        //выбор легкой игры - таблЭтка
        private void Lou_Click(object sender, RoutedEventArgs e)
        {
            image.Source = _yazvaDade;
            _lou = true;
            textBox.Text = "Я умираю :(";
            var player1 = new SoundPlayer();
            player1.Stream = Properties.Resources.no;
            player1.Play();
        }


        #region Логика язвы

        public void Yazva()
        {
            switch (_course)
            {
                case 0:
                    GoYazvaFirst();
                    break;
                case 1:
                    GoYazvaTwo();
                    break;
                case 2:
                    GoYazvaThree();
                    break;
                case 3:
                    GoYazvaFour();
                    break;
            }
        }

        public void GoYazva()
        {
            if (_lou)
            {
                //StupidYazva();
                StupidYazva();
            }
            else
            {
                //язва забивает центр
                _field[0] = 1;
                btn0.Tag = true;
                imgBtn0.Source = _zero;
            }
        }

        public void StupidYazva()
        {
            WinPayer();
            if (_start)
            {
                var flag = true;
                do
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        var temp = _ran.Next(0, 9);
                        if (_field[temp] == 0)
                        {
                            _field[temp] = 1;
                            _btns[temp].Tag = true;
                            _imgBtns[temp].Source = _zero;
                            flag = false;
                            break;
                        }
                    }
                    flag = false;
                    //var temp = _ran.Next(0, 8);
                    //if (_field[temp] == 0)
                    //{
                    //    _field[temp] = 1;
                    //    _btns[temp].Tag = true;
                    //    _imgBtns[temp].Source = _zero;
                    //    flag = false;
                    //}

                } while (flag);
            }
            //WinYazva();
            PossiblyDraw();
            if (_pleerWinStupidYazva)
                textBox.Text = ":( Ты победил";
        }

        //первый ответный ход язвы
        public void GoYazvaFirst()
        {
            //if (_lou)
            //{
            //    StupidYazva();
            //}
            //else
            //{
            //первый ход язвы 
            if (_field[5] == 2)
            {
                _field[3] = 1;
                btn3.Tag = true;
                imgBtn3.Source = _zero;
            }
            else if (_field[1] == 0)
            {
                _field[1] = 1;
                btn1.Tag = true;
                imgBtn1.Source = _zero;
            }
            else
            {
                _field[3] = 1;
                btn3.Tag = true;
                imgBtn3.Source = _zero;
            }

            _course = 1;
        }
        //второй ответный ход язвы
        public void GoYazvaTwo()
        {
            //WinPayer();
            //if (_lou)
            //{
            //    StupidYazva();
            //}
            //else 
            if (_field[1] == 1 && _field[5] == 0)
            {
                _field[5] = 1;
                btn5.Tag = true;
                imgBtn5.Source = _zero;
            }
            else if (_field[1] == 2 && _field[7] == 0 && _field[3] == 1)
            {
                _field[7] = 1;
                btn7.Tag = true;
                imgBtn7.Source = _zero;
            }
            else if (_field[1] == 2 && _field[7] == 2 && _field[3] == 1)
            {
                _field[8] = 1;
                btn8.Tag = true;
                imgBtn8.Source = _zero;
            }
            else if (_field[1] == 1 && _field[5] == 2 && _field[3] == 2)
            {
                _field[4] = 1;
                btn4.Tag = true;
                imgBtn4.Source = _zero;
            }
            else if (_field[1] == 1 && _field[2] == 2 && _field[5] == 2)
            {
                _field[7] = 1;
                btn7.Tag = true;
                imgBtn7.Source = _zero;
            }
            else if (_field[1] == 0 && _field[3] == 1 && _field[7] == 0)
            {
                _field[7] = 1;
                btn7.Tag = true;
                imgBtn7.Source = _zero;
            }
            else if (_field[1] == 0 && _field[3] == 1 && _field[7] == 2 && _field[5] != 2)
            {
                _field[8] = 1;
                btn8.Tag = true;
                imgBtn8.Source = _zero;
            }
            else if (_field[1] == 1 && _field[5] == 2 && _field[8] == 2)
            {
                _field[3] = 1;
                btn3.Tag = true;
                imgBtn3.Source = _zero;
            }
            else if (_field[1] == 1 && _field[5] == 2 && _field[7] == 2)
            {
                _field[6] = 1;
                btn6.Tag = true;
                imgBtn6.Source = _zero;
            }
            else if (_field[1] == 1 && _field[5] == 2 && _field[6] == 2)
            {
                _field[7] = 1;
                btn7.Tag = true;
                imgBtn7.Source = _zero;
            }
            else if (_field[3] == 1 && _field[5] == 2 && _field[7] == 0)
            {
                _field[7] = 1;
                btn7.Tag = true;
                imgBtn7.Source = _zero;
            }
            else if (_field[3] == 1 && _field[5] == 2 && _field[7] == 2)
            {
                _field[6] = 1;
                btn6.Tag = true;
                imgBtn6.Source = _zero;
            }
            else if (_field[1] == 1 && _field[5] == 2 && _field[4] == 2)
            {
                _field[3] = 1;
                btn3.Tag = true;
                imgBtn3.Source = _zero;
            }
            _course = 2;
            WinYazva();
            PossiblyDraw();
        }
        //третий ответный ход язвы
        public void GoYazvaThree()
        {
            //WinPayer();
            //if (_lou)
            //{
            //    StupidYazva();
            //}
            //else 
            if (_field[1] == 1 && _field[3] == 2 && _field[4] == 1 && _field[5] == 2 && _field[8] == 0)
            {
                _field[8] = 1;
                btn8.Tag = true;
                imgBtn8.Source = _zero;
            }
            else if (_field[1] == 1 && _field[3] == 2 && _field[4] == 1 && _field[5] == 2 && _field[8] == 2)
            {
                _field[2] = 1;
                btn2.Tag = true;
                imgBtn2.Source = _zero;
            }
            else if (_field[1] == 1 && _field[2] == 2 && _field[5] == 2 && _field[7] == 1 && _field[8] == 0)
            {
                _field[8] = 1;
                btn8.Tag = true;
                imgBtn8.Source = _zero;
            }
            else if (_field[1] == 1 && _field[2] == 2 && _field[5] == 2 && _field[7] == 1 && _field[8] == 2)
            {
                _field[3] = 1;
                btn3.Tag = true;
                imgBtn3.Source = _zero;
            }
            else if (_field[1] == 2 && _field[3] == 1 && _field[8] == 1 && _field[7] == 2 && _field[4] == 0)
            {
                _field[4] = 1;
                btn4.Tag = true;
                imgBtn4.Source = _zero;
            }
            else if (_field[1] == 2 && _field[3] == 1 && _field[8] == 1 && _field[7] == 2 && _field[4] == 2)
            {
                _field[2] = 1;
                btn2.Tag = true;
                imgBtn2.Source = _zero;
            }
            else if (_field[1] == 1 && _field[3] == 1 && _field[8] == 2 && _field[5] == 2 && _field[7] == 0)
            {
                _field[7] = 1;
                btn7.Tag = true;
                imgBtn7.Source = _zero;
            }
            else if (_field[1] == 1 && _field[3] == 1 && _field[8] == 2 && _field[5] == 2 && _field[7] == 2)
            {
                _field[2] = 1;
                btn2.Tag = true;
                imgBtn2.Source = _zero;
            }
            else if (_field[1] == 1 && _field[7] == 2 && _field[6] == 1 && _field[5] == 2 && _field[2] == 0)
            {
                _field[2] = 1;
                btn2.Tag = true;
                imgBtn2.Source = _zero;
            }
            else if (_field[1] == 1 && _field[7] == 2 && _field[6] == 1 && _field[5] == 2 && _field[2] == 2)
            {
                _field[8] = 1;
                btn8.Tag = true;
                imgBtn8.Source = _zero;
            }
            else if (_field[1] == 1 && _field[7] == 1 && _field[6] == 2 && _field[5] == 2 && _field[3] == 0)
            {
                _field[3] = 1;
                btn3.Tag = true;
                imgBtn3.Source = _zero;
            }
            else if (_field[1] == 1 && _field[7] == 1 && _field[6] == 2 && _field[5] == 2 && _field[3] == 2)
            {
                _field[8] = 1;
                btn8.Tag = true;
                imgBtn8.Source = _zero;
            }
            else if (_field[3] == 1 && _field[7] == 2 && _field[6] == 1 && _field[5] == 2 && _field[2] == 0)
            {
                _field[2] = 1;
                btn2.Tag = true;
                imgBtn2.Source = _zero;
            }
            else if (_field[3] == 1 && _field[7] == 2 && _field[6] == 1 && _field[5] == 2 && _field[2] == 2)
            {
                _field[4] = 1;
                btn4.Tag = true;
                imgBtn4.Source = _zero;
            }
            else if (_field[1] == 1 && _field[3] == 1 && _field[4] == 2 && _field[5] == 2 && _field[2] == 0)
            {
                _field[2] = 1;
                btn2.Tag = true;
                imgBtn2.Source = _zero;
            }
            else if (_field[1] == 1 && _field[3] == 1 && _field[4] == 2 && _field[5] == 2 && _field[2] == 2)
            {
                _field[7] = 1;
                btn7.Tag = true;
                imgBtn7.Source = _zero;
            }
            _course = 3;
            WinYazva();
            PossiblyDraw();
        }
        //четвертый ответный ход язвы
        public void GoYazvaFour()
        {
            WinPayer();
            //находим оставшуюся пустую клетку
            if (_field[1] == 0)
            {
                _field[1] = 1;
                btn1.Tag = true;
                imgBtn1.Source = _zero;
            }
            else if (_field[2] == 0)
            {
                _field[2] = 1;
                btn2.Tag = true;
                imgBtn2.Source = _zero;
            }
            else if (_field[3] == 0)
            {
                _field[3] = 1;
                btn3.Tag = true;
                imgBtn3.Source = _zero;
            }
            else if (_field[4] == 0)
            {
                _field[4] = 1;
                btn4.Tag = true;
                imgBtn4.Source = _zero;
            }
            else if (_field[5] == 0)
            {
                _field[5] = 1;
                btn5.Tag = true;
                imgBtn5.Source = _zero;
            }
            else if (_field[6] == 0)
            {
                _field[6] = 1;
                btn6.Tag = true;
                imgBtn6.Source = _zero;
            }
            else if (_field[7] == 0)
            {
                _field[7] = 1;
                btn7.Tag = true;
                imgBtn7.Source = _zero;
            }
            else if (_field[8] == 0)
            {
                _field[8] = 1;
                btn8.Tag = true;
                imgBtn8.Source = _zero;
            }

            WinYazva();
            if (_start)
                PossiblyDraw();
        }


        #endregion



        #region Клики
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (_start)
            {
                if (!(bool)btn1.Tag)
                {
                    imgBtn1.Source = _x;
                    btn1.Tag = true;
                    _field[1] = 2;
                    if (_lou)
                        StupidYazva();
                    else
                        Yazva();
                }

            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (_start)
                if (!(bool)btn2.Tag)
                {
                    imgBtn2.Source = _x;
                    btn2.Tag = true;
                    _field[2] = 2;
                    if (_lou)
                        StupidYazva();
                    else
                        Yazva();
                }
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if (_start)
                if (!(bool)btn3.Tag)
                {
                    imgBtn3.Source = _x;
                    btn3.Tag = true;
                    _field[3] = 2;
                    if (_lou)
                        StupidYazva();
                    else
                        Yazva();
                }
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            if (_start)
                if (!(bool)btn4.Tag)
                {
                    imgBtn4.Source = _x;
                    btn4.Tag = true;
                    _field[4] = 2;
                    if (_lou)
                        StupidYazva();
                    else
                        Yazva();
                }
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            if (_start)
                if (!(bool)btn5.Tag)
                {
                    imgBtn5.Source = _x;
                    btn5.Tag = true;
                    _field[5] = 2;
                    if (_lou)
                        StupidYazva();
                    else
                        Yazva();
                }
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            if (_start)
                if (!(bool)btn6.Tag)
                {
                    imgBtn6.Source = _x;
                    btn6.Tag = true;
                    _field[6] = 2;
                    if (_lou)
                        StupidYazva();
                    else
                        Yazva();
                }
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            if (_start)
                if (!(bool)btn7.Tag)
                {
                    imgBtn7.Source = _x;
                    btn7.Tag = true;
                    _field[7] = 2;
                    if (_lou)
                        StupidYazva();
                    else
                        Yazva();
                }
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            if (_start)
                if (!(bool)btn8.Tag)
                {
                    imgBtn8.Source = _x;
                    btn8.Tag = true;
                    _field[8] = 2;
                    if (_lou)
                        StupidYazva();
                    else
                        Yazva();
                }
        }

        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            if (_start)
                if (!(bool)btn0.Tag)
                {
                    imgBtn0.Source = _x;
                    btn0.Tag = true;
                    _field[0] = 2;
                    if (_lou)
                        StupidYazva();
                    else
                        Yazva();
                }
        }

        #endregion
        // двигаем игру по рабочему столу
        private void MouseLeftButtonDownDragMove(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
        // закрыть игру
        private void CloseCommand(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }
        //свернуть игру
        private void MinimizedCommand(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
       }
}
