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
        private double _speed = 10;
        private int _index;
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private ChooseStation _chooseStation = ChooseStation.Start;
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
                , PathSviatoshynNyvky, Nyvky, PathNyvkyBeresteiska, Beresteiska, PathBeresteiskaShuliavska, Shuliavska, PathShuliavskaPolitekhnichnyiInstytut,
                PolitekhnichnyiInstytut, PathPolitekhnichnyiInstytutVokzalna, Vokzalna, PathVokzalnaUniversytet, Universytet
                , PathUniversytetTeatralna,  Teatralna, PathCrossRoadTeatralnaKhreshchatyk, 
                 Khreshchatyk , PathCrossRoadKhreshchatykArsenalna, Arsenalna, PathArsenalnaDnipro, Dnepr, PathDniproHidropark,
                 Hidropark, PathHidroparkLivoberezhna, Livoberezhna, PathLivoberezhnaDarnytsia, Darnytsia, PathDarnytsiaChernihivska,
                 Chernihivska, PathChernihivskaLisova, Lisova
            };
            SubscribeOnEvent();
            _timer.Tick += TimerAnimatedRoute;
            _timer.Interval = TimeSpan.FromMilliseconds(_speed);
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
                                AnimatedPath(_startStation, _endStation);
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
            _index = 0;
            if (_timer.IsEnabled)
            {
                _timer.Stop();
            }
        }

        #endregion


        #region SeparateArrays

        private void AnimatedPath(Ellipse startStation, Ellipse endStation)
        {
            var start = int.MaxValue;
            for (var i = 0; i < _paths.Length; i++)
            {
                if (_paths[i].Name == startStation.Name)
                {
                    start = i;
                    _listEllipse.Add(_paths[i] as Ellipse);
                }

                if (start < i && i%2 != 0)
                    _listRectangle.Add(_paths[i] as Rectangle);

                if (_paths[i].Name == endStation.Name)
                {
                    _listEllipse.Add(_paths[i] as Ellipse);
                    if(_paths[i].Name != Lisova.Name)
                        _listRectangle.Add(_paths[i + 1] as Rectangle);
                    break;
                }
                if (start < i && i%2 == 0)
                    _listEllipse.Add(_paths[i] as Ellipse);
            }

            if (!_timer.IsEnabled)
                _timer.Start();
        }

        #endregion

        #region AnimatedRoute
        private void TimerAnimatedRoute(object sender, EventArgs eventArgs)
        {
            var flag = true;

            if (_index + 1 == _listEllipse.Count)
            {
                _index = 0;
                _timer.Stop();
                flag = false;
            }

            ColorAnimated(_listRectangle[_index]);
            if (_listRectangle[_index].Name == "PathUniversytetTeatralna")
            {
                ColorAnimated(PathCrossRoadTeatralnaUniversytet);
            }
            if (_listRectangle[_index].Name == "PathCrossRoadTeatralnaKhreshchatyk")
            {
                ColorAnimated(PathCrossRoadKhreshchatykTeatralna);
            }
            if (_index < _listEllipse.Count)
                _listEllipse[_index].Fill = Brushes.Yellow;
            if (flag)
                _index++;
        }
        #endregion


        private void ColorAnimated(FrameworkElement element)
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
