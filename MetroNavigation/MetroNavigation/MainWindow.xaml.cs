using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MetroNavigation
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _speed = 1000;
        private int _indexE;
        private int _indexR;
        private readonly DispatcherTimer _timerEllipse = new DispatcherTimer();
        private readonly DispatcherTimer _timerRectangle = new DispatcherTimer();
        private ChooseStation _chooseStation = ChooseStation.Start;
        private Order _order;
        private readonly FrameworkElement[] _paths;
        private Ellipse _startStation;
        private Ellipse _endStation;
        private readonly Ellipse _tempStart = new Ellipse();
        private readonly Ellipse _tempEnd = new Ellipse();
        private readonly List<Ellipse> _listEllipse = new List<Ellipse>();
        private readonly List<Rectangle> _listRectangle = new List<Rectangle>();

        public MainWindow()
        {
            InitializeComponent();

            _paths = new FrameworkElement[]
            {
                Academmistechko, PathAcademmistechkoZhytomyrska, Zhytomyrska, PathZhytomyrskaSviatoshyn, Sviatoshyn
                , PathSviatoshynNyvky, Nyvky, PathNyvkyBeresteiska, Beresteiska,
                PathBeresteiskaShuliavska, Shuliavska, PathShuliavskaPolitekhnichnyiInstytut,
                PolitekhnichnyiInstytut, PathPolitekhnichnyiInstytutVokzalna,
                Vokzalna, PathVokzalnaUniversytet, Universytet
                , PathUniversytetTeatralna, PathCrossRoadTeatralnaUniversytet,  Teatralna,
                PathCrossRoadTeatralnaKhreshchatyk, PathCrossRoadKhreshchatykTeatralna,
                 Khreshchatyk , PathCrossRoadKhreshchatykArsenalna, Arsenalna, PathArsenalnaDnipro, Dnepr, PathDniproHidropark,
                 Hidropark, PathHidroparkLivoberezhna, Livoberezhna, PathLivoberezhnaDarnytsia, Darnytsia, PathDarnytsiaChernihivska,
                 Chernihivska, PathChernihivskaLisova, Lisova
            };
            SubscribeOnEvent();
            _timerEllipse.Tick += TimerEllipseAnimatedEllipse;
            _timerEllipse.Interval = TimeSpan.FromMilliseconds(_speed);
            _timerRectangle.Tick += TimerAnimatedRectangle;
            _timerRectangle.Interval = TimeSpan.FromMilliseconds(_speed);
        }

        


        private void SubscribeOnEvent()
        {
            foreach (var path in _paths)
            {
                path.MouseDown += ClickMouseAction;
            }
        }

        #region ClickMouseAction

        private void ClickMouseAction(object sender, MouseButtonEventArgs e)
        {

            var point = sender as Ellipse;
            switch (_chooseStation)
            {
                case ChooseStation.Start:
                    if (point != null)
                    {
                        _tempStart.Fill = point.Fill;
                        point.Fill = Brushes.Yellow;
                        _startStation = point;
                        _chooseStation = ChooseStation.End;
                    }
                    break;
                case ChooseStation.End:
                    if (point != null)
                    {
                        _tempEnd.Fill = point.Fill;
                        point.Fill = Brushes.Yellow;
                        _endStation = point;
                        _chooseStation = ChooseStation.NewPath;
                        foreach (var path in _paths)
                        {
                            if (path.Name == _startStation.Name)
                            {
                                _order = Order.Asc;
                                SeparatedPath(_startStation, _endStation);
                                break;
                            }
                            if (path.Name == _endStation.Name)
                            {
                                _order = Order.Desc;
                                SeparatedPath(_startStation, _endStation);
                                break;
                            }
                        }
                    }
                    break;
                case ChooseStation.NewPath:
                    _chooseStation = ChooseStation.Start;
                    Cleaner();
                    ClickMouseAction(sender, e);
                    break;
            }
        }


        #endregion

        #region Cleaner

        private void Cleaner()
        {
            _startStation.Fill = _tempStart.Fill;
            _endStation.Fill = _tempEnd.Fill;

            foreach (var ellipse in _listEllipse)
            {
                ellipse.Fill = Brushes.White;
            }
            foreach (var rect in _listRectangle)
            {
                rect.Fill = Brushes.Red;
            }
            _listRectangle.Clear();
            _listEllipse.Clear();
            _indexE = 0;
            if (_timerEllipse.IsEnabled)
                _timerEllipse.Stop();
            if (_timerRectangle.IsEnabled)
                _timerRectangle.Stop();
        }

        #endregion


        #region SeparateArrays

        private void SeparatedPath(Ellipse startStation, Ellipse endStation)
        {

            switch (_order)
            {
                case Order.Asc:
                {
                        var start = int.MaxValue;
                        for (var i = 0; i < _paths.Length; i++)
                        {
                            if (_paths[i].Name == startStation.Name)
                                start = i;

                            if (_paths[i].Name == endStation.Name)
                                break;

                            if (start < i && _paths[i] is Ellipse)
                                _listEllipse.Add((Ellipse)_paths[i]);

                            if (start < i && _paths[i] is Rectangle)
                                _listRectangle.Add((Rectangle)_paths[i]);

                        }
                        break;
                }
                case Order.Desc:
                {
                    var start = int.MinValue;
                        for (var i = _paths.Length - 1; i >= 0; i--)
                        {
                            if (_paths[i].Name == startStation.Name)
                                start = i;

                            if (_paths[i].Name == endStation.Name)
                                break;

                            if (start > i && _paths[i] is Ellipse)
                                _listEllipse.Add((Ellipse)_paths[i]);

                            if (start > i && _paths[i] is Rectangle)
                                _listRectangle.Add((Rectangle)_paths[i]);

                        }
                        break;
                }

            }

            _indexR = 0;
            _indexE = 0;

            if (!_timerEllipse.IsEnabled)
                _timerEllipse.Start();
            if (!_timerRectangle.IsEnabled)
                _timerRectangle.Start();
        }
        #endregion

        #region AnimatedRoute
        private void TimerEllipseAnimatedEllipse(object sender, EventArgs eventArgs)
        {
            var flag = true;

            if (_indexE + 1 == _listEllipse.Count)
            {
                ColorAnimated(_listEllipse[_indexE]);
                _timerEllipse.Stop();
                flag = false;
            }
            else if (_listEllipse.Count == 0)
            {
                _timerEllipse.Stop();
                flag = false;
            }

            if (flag)
            {
                ColorAnimated(_listEllipse[_indexE]);
                _indexE++;
            }
        }

        private void TimerAnimatedRectangle(object sender, EventArgs e)
        {
            var flag = true;

            if (_indexR + 1 == _listRectangle.Count)
            {
                ColorAnimated(_listRectangle[_indexR]);
                _timerRectangle.Stop();
                flag = false;
            }
            if (flag)
            {
                ColorAnimated(_listRectangle[_indexR]);
                _indexR++;
            }
        }
        #endregion


        private void ColorAnimated(UIElement element)
        {
            var storyboard = new Storyboard();
            var colorAnimation = new ColorAnimation(Colors.Yellow, TimeSpan.FromMilliseconds(_speed));
            element.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            Storyboard.SetTarget(colorAnimation, element);
            Storyboard.SetTargetProperty(colorAnimation, new PropertyPath("Fill.Color"));
            storyboard.Children.Add(colorAnimation);
            storyboard.Begin();
        }
    }
}
