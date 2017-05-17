using System;
using System.Collections.Generic;
using System.Linq;
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
        private double _speed = 500;
        private int _indexE;
        private int _indexR;
        private readonly DispatcherTimer _timerEllipse = new DispatcherTimer();
        private readonly DispatcherTimer _timerRectangle = new DispatcherTimer();
        private ChooseStation _chooseStation = ChooseStation.Start;
        private Order _order;
        private readonly FrameworkElement[] _pathsRedLine;
        private readonly FrameworkElement[] _pathsBlueLine;
        private readonly FrameworkElement[] _pathsGreenLine;

        private readonly FrameworkElement[] _pathsR1B1;
        private readonly FrameworkElement[] _pathsR1B2;
        private readonly FrameworkElement[] _pathsR1G1;
        private readonly FrameworkElement[] _pathsR1G2;
        private readonly FrameworkElement[] _pathsB1R2;
        private readonly FrameworkElement[] _pathsB1G2;
        private readonly FrameworkElement[] _pathsG1B1;
        private readonly FrameworkElement[] _pathsG1R2;
        private readonly FrameworkElement[] _pathsG1B2;

        private Ellipse _startStation;
        private Ellipse _endStation;
        private readonly Ellipse _tempStart = new Ellipse();
        private readonly Ellipse _tempEnd = new Ellipse();
        private readonly List<Ellipse> _listEllipse = new List<Ellipse>();
        private readonly List<Rectangle> _listRectangle = new List<Rectangle>();
        private readonly List<Brush> _colors = new List<Brush>();

        public MainWindow()
        {
            InitializeComponent();

            _pathsRedLine = new FrameworkElement[]
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
            _pathsBlueLine = new FrameworkElement[]
            {
                HeroivDnipra, PathHeroivDnipraMinska, Minska, PathMinskaObolon, Obolon, PathObolonPetrivka,
                Petrivka, PathPetrivkaTarasaShevchenka, TarasaShevchenka, PathTarasaShevchenkaKontraktovaPloshcha,
                KontraktovaPloshcha, PathKontraktovaPloshchaPochtovaPloshcha, PoshtovaPloshcha, CrossRoadPoshtovaPloshchaMaidanNezalezhnosti,MaidanNezalezhnosti,
                CrossRoadMaidanNezalezhnostiPloshchaLvaTolstoho, PloshchaLvaTolstoho, PathPloshchaLvaTolstohoOlimpiiska, Olimpiiska, PathOlimpiiskaPalatsUkrayina,
                PalatsUkrayina, PathPalatsUkrayinaLybidska, Lybidska, PathLybidskaDemiivska, Demiivska, PathDemiivskaHolosiivka, Holosiivska,
                PathHolosiivskaVasylkivska, Vasylkivska, PathVasylkivskaVystavkovyiTsentr, VystavkovyiTsentr, PathVystavkovyiTsentrIpodrom,
                Ipodrom, PathIpodromTeremky, Teremky
            };

            _pathsGreenLine = new FrameworkElement[]
            {
                Syrets, PathSyretsDorohozhychi, Dorohozhychi, PathDorohozhychiLukianivska, Lukianivska, PathLukianivskaZolotiVorota,
                ZolotiVorota,PathZolotiVorotaPalatsSportu,PathPalatsSportuTeatralna, PalatsSportu, PathPalatsSportuKlovska, PathKlovskaPalatsSportu,
                Klovska, PathKlovskaPecherska,
                Pecherska, PathPecherskaDruzhbyNarodiv, DruzhbyNarodiv, PathDruzhbyNarodivVydubychi, Vydubychi, PathVydubychiSlavutych,
                Slavutych, PathSlavutychOsokorky, Osokorky, PathOsokorkyPozniaky, Pozniaky, PathPozniakyKharkivska, Kharkivska,
                PathKharkivskaVyrlytsia, Vyrlytsia, PathVyrlytsiaBorispilska, Borispilska, PathBoryspilskaChervonyKhutir, ChervonyKhutir
            };

            _pathsR1B1 = new FrameworkElement[]
            {
                Academmistechko, PathAcademmistechkoZhytomyrska, Zhytomyrska, PathZhytomyrskaSviatoshyn, Sviatoshyn
                , PathSviatoshynNyvky, Nyvky, PathNyvkyBeresteiska, Beresteiska,
                PathBeresteiskaShuliavska, Shuliavska, PathShuliavskaPolitekhnichnyiInstytut,
                PolitekhnichnyiInstytut, PathPolitekhnichnyiInstytutVokzalna,
                Vokzalna, PathVokzalnaUniversytet, Universytet
                , PathUniversytetTeatralna, PathCrossRoadTeatralnaUniversytet,  Teatralna,
                PathCrossRoadTeatralnaKhreshchatyk, PathCrossRoadKhreshchatykTeatralna,
                 Khreshchatyk,MaidanNezalezhnosti, CrossRoadPoshtovaPloshchaMaidanNezalezhnosti,PoshtovaPloshcha,
                 PathKontraktovaPloshchaPochtovaPloshcha,KontraktovaPloshcha, PathTarasaShevchenkaKontraktovaPloshcha,
                 TarasaShevchenka,PathPetrivkaTarasaShevchenka,Petrivka,PathObolonPetrivka,Obolon,PathMinskaObolon,
                 Minska,PathHeroivDnipraMinska,HeroivDnipra
            };

            _pathsR1G2 = new FrameworkElement[]
            {
                Academmistechko, PathAcademmistechkoZhytomyrska, Zhytomyrska, PathZhytomyrskaSviatoshyn, Sviatoshyn
                , PathSviatoshynNyvky, Nyvky, PathNyvkyBeresteiska, Beresteiska,
                PathBeresteiskaShuliavska, Shuliavska, PathShuliavskaPolitekhnichnyiInstytut,
                PolitekhnichnyiInstytut, PathPolitekhnichnyiInstytutVokzalna,
                Vokzalna, PathVokzalnaUniversytet, Universytet
                , PathUniversytetTeatralna, PathCrossRoadTeatralnaUniversytet,  Teatralna,ZolotiVorota,PathZolotiVorotaPalatsSportu, PalatsSportu, PathPalatsSportuKlovska, Klovska, PathKlovskaPecherska,
                Pecherska, PathPecherskaDruzhbyNarodiv, DruzhbyNarodiv, PathDruzhbyNarodivVydubychi, Vydubychi, PathVydubychiSlavutych,
                Slavutych, PathSlavutychOsokorky, Osokorky, PathOsokorkyPozniaky, Pozniaky, PathPozniakyKharkivska, Kharkivska,
                PathKharkivskaVyrlytsia, Vyrlytsia, PathVyrlytsiaBorispilska, Borispilska, PathBoryspilskaChervonyKhutir, ChervonyKhutir
            };

            _pathsR1G1 = new FrameworkElement[]
            {
                Academmistechko, PathAcademmistechkoZhytomyrska, Zhytomyrska, PathZhytomyrskaSviatoshyn, Sviatoshyn
                , PathSviatoshynNyvky, Nyvky, PathNyvkyBeresteiska, Beresteiska,
                PathBeresteiskaShuliavska, Shuliavska, PathShuliavskaPolitekhnichnyiInstytut,
                PolitekhnichnyiInstytut, PathPolitekhnichnyiInstytutVokzalna,
                Vokzalna, PathVokzalnaUniversytet, Universytet
                , PathUniversytetTeatralna, PathCrossRoadTeatralnaUniversytet, Teatralna, ZolotiVorota,PathLukianivskaZolotiVorota,
                Lukianivska,PathDorohozhychiLukianivska,Dorohozhychi,PathSyretsDorohozhychi, Syrets   
            };

            _pathsR1B2 = new FrameworkElement[]
            {
                Academmistechko, PathAcademmistechkoZhytomyrska, Zhytomyrska, PathZhytomyrskaSviatoshyn, Sviatoshyn
                , PathSviatoshynNyvky, Nyvky, PathNyvkyBeresteiska, Beresteiska,
                PathBeresteiskaShuliavska, Shuliavska, PathShuliavskaPolitekhnichnyiInstytut,
                PolitekhnichnyiInstytut, PathPolitekhnichnyiInstytutVokzalna,
                Vokzalna, PathVokzalnaUniversytet, Universytet
                , PathUniversytetTeatralna, PathCrossRoadTeatralnaUniversytet,  Teatralna,
                PathCrossRoadTeatralnaKhreshchatyk, PathCrossRoadKhreshchatykTeatralna,
                 Khreshchatyk,MaidanNezalezhnosti,CrossRoadMaidanNezalezhnostiPloshchaLvaTolstoho, PloshchaLvaTolstoho, PathPloshchaLvaTolstohoOlimpiiska, Olimpiiska, PathOlimpiiskaPalatsUkrayina,
                PalatsUkrayina, PathPalatsUkrayinaLybidska, Lybidska, PathLybidskaDemiivska, Demiivska, PathDemiivskaHolosiivka, Holosiivska,
                PathHolosiivskaVasylkivska, Vasylkivska, PathVasylkivskaVystavkovyiTsentr, VystavkovyiTsentr, PathVystavkovyiTsentrIpodrom,
                Ipodrom, PathIpodromTeremky, Teremky
            };
            _pathsB1R2 = new FrameworkElement[]
            {
                HeroivDnipra, PathHeroivDnipraMinska, Minska, PathMinskaObolon, Obolon, PathObolonPetrivka,
                Petrivka, PathPetrivkaTarasaShevchenka, TarasaShevchenka, PathTarasaShevchenkaKontraktovaPloshcha,
                KontraktovaPloshcha, PathKontraktovaPloshchaPochtovaPloshcha, PoshtovaPloshcha, CrossRoadPoshtovaPloshchaMaidanNezalezhnosti,MaidanNezalezhnosti,
                 Khreshchatyk, PathCrossRoadKhreshchatykArsenalna, Arsenalna, PathArsenalnaDnipro, Dnepr, PathDniproHidropark,
                 Hidropark, PathHidroparkLivoberezhna, Livoberezhna, PathLivoberezhnaDarnytsia, Darnytsia, PathDarnytsiaChernihivska,
                 Chernihivska, PathChernihivskaLisova, Lisova
            };

            _pathsB1G2 = new FrameworkElement[]
            {
                HeroivDnipra, PathHeroivDnipraMinska, Minska, PathMinskaObolon, Obolon, PathObolonPetrivka,
                Petrivka, PathPetrivkaTarasaShevchenka, TarasaShevchenka, PathTarasaShevchenkaKontraktovaPloshcha,
                KontraktovaPloshcha, PathKontraktovaPloshchaPochtovaPloshcha, PoshtovaPloshcha,PalatsSportu, PathPalatsSportuKlovska, Klovska, PathKlovskaPecherska,
                Pecherska, PathPecherskaDruzhbyNarodiv, DruzhbyNarodiv, PathDruzhbyNarodivVydubychi, Vydubychi, PathVydubychiSlavutych,
                Slavutych, PathSlavutychOsokorky, Osokorky, PathOsokorkyPozniaky, Pozniaky, PathPozniakyKharkivska, Kharkivska,
                PathKharkivskaVyrlytsia, Vyrlytsia, PathVyrlytsiaBorispilska, Borispilska, PathBoryspilskaChervonyKhutir, ChervonyKhutir
            };
            _pathsG1B1 = new FrameworkElement[]
            {
                Syrets, PathSyretsDorohozhychi, Dorohozhychi, PathDorohozhychiLukianivska, Lukianivska, PathLukianivskaZolotiVorota,
                ZolotiVorota,PathZolotiVorotaPalatsSportu, PalatsSportu,PloshchaLvaTolstoho, CrossRoadMaidanNezalezhnostiPloshchaLvaTolstoho,
                MaidanNezalezhnosti, CrossRoadPoshtovaPloshchaMaidanNezalezhnosti,PoshtovaPloshcha,
                 PathKontraktovaPloshchaPochtovaPloshcha,KontraktovaPloshcha, PathTarasaShevchenkaKontraktovaPloshcha,
                 TarasaShevchenka,PathPetrivkaTarasaShevchenka,Petrivka,PathObolonPetrivka,Obolon,PathMinskaObolon,
                 Minska,PathHeroivDnipraMinska,HeroivDnipra
            };
            
            _pathsG1R2 = new FrameworkElement[]
            {
                Syrets, PathSyretsDorohozhychi, Dorohozhychi, PathDorohozhychiLukianivska, Lukianivska, PathLukianivskaZolotiVorota,
                ZolotiVorota, Teatralna,
                PathCrossRoadTeatralnaKhreshchatyk, PathCrossRoadKhreshchatykTeatralna,
                 Khreshchatyk , PathCrossRoadKhreshchatykArsenalna, Arsenalna, PathArsenalnaDnipro, Dnepr, PathDniproHidropark,
                 Hidropark, PathHidroparkLivoberezhna, Livoberezhna, PathLivoberezhnaDarnytsia, Darnytsia, PathDarnytsiaChernihivska,
                 Chernihivska, PathChernihivskaLisova, Lisova
            };

            _pathsG1B2 = new FrameworkElement[]
            {
                Syrets, PathSyretsDorohozhychi, Dorohozhychi, PathDorohozhychiLukianivska, Lukianivska, PathLukianivskaZolotiVorota,
                ZolotiVorota,PathZolotiVorotaPalatsSportu, PalatsSportu, PloshchaLvaTolstoho, PathPloshchaLvaTolstohoOlimpiiska, Olimpiiska, PathOlimpiiskaPalatsUkrayina,
                PalatsUkrayina, PathPalatsUkrayinaLybidska, Lybidska, PathLybidskaDemiivska, Demiivska, PathDemiivskaHolosiivka, Holosiivska,
                PathHolosiivskaVasylkivska, Vasylkivska, PathVasylkivskaVystavkovyiTsentr, VystavkovyiTsentr, PathVystavkovyiTsentrIpodrom,
                Ipodrom, PathIpodromTeremky, Teremky
            };

            SubscribeOnEvent();
            _timerEllipse.Tick += TimerEllipseAnimatedEllipse;
            _timerEllipse.Interval = TimeSpan.FromMilliseconds(_speed);
            _timerRectangle.Tick += TimerAnimatedRectangle;
            _timerRectangle.Interval = TimeSpan.FromMilliseconds(_speed);
        }

        


        private void SubscribeOnEvent()
        {
            foreach (var path in _pathsRedLine)
            {
                path.MouseDown += ClickMouseAction;
            }
            foreach (var path in _pathsBlueLine)
            {
                path.MouseDown += ClickMouseAction;
            }
            foreach (var path in _pathsGreenLine)
            {
                path.MouseDown += ClickMouseAction;
            }
        }

        #region ClickMouseAction

        private void ClickMouseAction(object sender, MouseButtonEventArgs e)
        {
            Ways way;
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
                        way = ChoosenWay(_startStation, _endStation);
                        _chooseStation = ChooseStation.NewPath;
                        switch (way)
                        {
                            case Ways.RedLine:
                                foreach (var path in _pathsRedLine)
                                {
                                    if (path.Name == _startStation.Name)
                                    {
                                        _order = Order.Asc;
                                        SeparatedPath(_startStation, _endStation, _pathsRedLine);
                                        break;
                                    }
                                    if (path.Name == _endStation.Name)
                                    {
                                        _order = Order.Desc;
                                        SeparatedPath(_startStation, _endStation, _pathsRedLine);
                                        break;
                                    }
                                }
                                break;
                                case Ways.BlueLine:
                                foreach (var path in _pathsBlueLine)
                                {
                                    if (path.Name == _startStation.Name)
                                    {
                                        _order = Order.Asc;
                                        SeparatedPath(_startStation, _endStation, _pathsBlueLine);
                                        break;
                                    }
                                    if (path.Name == _endStation.Name)
                                    {
                                        _order = Order.Desc;
                                        SeparatedPath(_startStation, _endStation, _pathsBlueLine);
                                        break;
                                    }
                                }
                                break;
                            case Ways.GreenLine:
                                foreach (var path in _pathsGreenLine)
                                {
                                    if (path.Name == _startStation.Name)
                                    {
                                        _order = Order.Asc;
                                        SeparatedPath(_startStation, _endStation, _pathsGreenLine);
                                        break;
                                    }
                                    if (path.Name == _endStation.Name)
                                    {
                                        _order = Order.Desc;
                                        SeparatedPath(_startStation, _endStation, _pathsGreenLine);
                                        break;
                                    }
                                }
                                break;
                            case Ways.R1B1:
                                foreach (var path in _pathsR1B1)
                                {
                                    if (path.Name == _startStation.Name)
                                    {
                                        _order = Order.Asc;
                                        SeparatedPath(_startStation, _endStation, _pathsR1B1);
                                        break;
                                    }
                                    if (path.Name == _endStation.Name)
                                    {
                                        _order = Order.Desc;
                                        SeparatedPath(_startStation, _endStation, _pathsR1B1);
                                        break;
                                    }
                                }
                                break;
                            case Ways.R1B2:
                                foreach (var path in _pathsR1B2)
                                {
                                    if (path.Name == _startStation.Name)
                                    {
                                        _order = Order.Asc;
                                        SeparatedPath(_startStation, _endStation, _pathsR1B2);
                                        break;
                                    }
                                    if (path.Name == _endStation.Name)
                                    {
                                        _order = Order.Desc;
                                        SeparatedPath(_startStation, _endStation, _pathsR1B2);
                                        break;
                                    }
                                }
                                break;
                            case Ways.R1G1:
                                foreach (var path in _pathsR1G1)
                                {
                                    if (path.Name == _startStation.Name)
                                    {
                                        _order = Order.Asc;
                                        SeparatedPath(_startStation, _endStation, _pathsR1G1);
                                        break;
                                    }
                                    if (path.Name == _endStation.Name)
                                    {
                                        _order = Order.Desc;
                                        SeparatedPath(_startStation, _endStation, _pathsR1G1);
                                        break;
                                    }
                                }
                                break;
                            case Ways.R1G2:
                                foreach (var path in _pathsR1G2)
                                {
                                    if (path.Name == _startStation.Name)
                                    {
                                        _order = Order.Asc;
                                        SeparatedPath(_startStation, _endStation, _pathsR1G2);
                                        break;
                                    }
                                    if (path.Name == _endStation.Name)
                                    {
                                        _order = Order.Desc;
                                        SeparatedPath(_startStation, _endStation, _pathsR1G2);
                                        break;
                                    }
                                }
                                break;
                            case Ways.G1B1:
                                foreach (var path in _pathsG1B1)
                                {
                                    if (path.Name == _startStation.Name)
                                    {
                                        _order = Order.Asc;
                                        SeparatedPath(_startStation, _endStation, _pathsG1B1);
                                        break;
                                    }
                                    if (path.Name == _endStation.Name)
                                    {
                                        _order = Order.Desc;
                                        SeparatedPath(_startStation, _endStation, _pathsG1B1);
                                        break;
                                    }
                                }
                                break;
                            case Ways.G1B2:
                                foreach (var path in _pathsG1B2)
                                {
                                    if (path.Name == _startStation.Name)
                                    {
                                        _order = Order.Asc;
                                        SeparatedPath(_startStation, _endStation, _pathsB1G2);
                                        break;
                                    }
                                    if (path.Name == _endStation.Name)
                                    {
                                        _order = Order.Desc;
                                        SeparatedPath(_startStation, _endStation, _pathsB1G2);
                                        break;
                                    }
                                }
                                break;
                            case Ways.G1R2:
                                foreach (var path in _pathsG1R2)
                                {
                                    if (path.Name == _startStation.Name)
                                    {
                                        _order = Order.Asc;
                                        SeparatedPath(_startStation, _endStation, _pathsG1R2);
                                        break;
                                    }
                                    if (path.Name == _endStation.Name)
                                    {
                                        _order = Order.Desc;
                                        SeparatedPath(_startStation, _endStation, _pathsG1R2);
                                        break;
                                    }
                                }
                                break;
                            case Ways.B1G2:
                                foreach (var path in _pathsB1G2)
                                {
                                    if (path.Name == _startStation.Name)
                                    {
                                        _order = Order.Asc;
                                        SeparatedPath(_startStation, _endStation, _pathsB1G2);
                                        break;
                                    }
                                    if (path.Name == _endStation.Name)
                                    {
                                        _order = Order.Desc;
                                        SeparatedPath(_startStation, _endStation, _pathsB1G2);
                                        break;
                                    }
                                }
                                break;
                            case Ways.B1R2:
                                foreach (var path in _pathsB1R2)
                                {
                                    if (path.Name == _startStation.Name)
                                    {
                                        _order = Order.Asc;
                                        SeparatedPath(_startStation, _endStation, _pathsB1R2);
                                        break;
                                    }
                                    if (path.Name == _endStation.Name)
                                    {
                                        _order = Order.Desc;
                                        SeparatedPath(_startStation, _endStation, _pathsB1R2);
                                        break;
                                    }
                                }
                                break;
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

        private Ways ChoosenWay(Ellipse startStation, Ellipse endStation)
        {
            if(_pathsRedLine.Contains(startStation) && _pathsRedLine.Contains(endStation))
                return Ways.RedLine;
            if (_pathsBlueLine.Contains(startStation) && _pathsBlueLine.Contains(endStation))
                return Ways.BlueLine;
            if (_pathsGreenLine.Contains(startStation) && _pathsGreenLine.Contains(endStation))
                return Ways.GreenLine;
            if (_pathsR1B1.Contains(startStation) && _pathsR1B1.Contains(endStation))
                return Ways.R1B1;
            if (_pathsR1B2.Contains(startStation) && _pathsR1B2.Contains(endStation))
                return Ways.R1B2;
            if (_pathsR1G1.Contains(startStation) && _pathsR1G1.Contains(endStation))
                return Ways.R1G1;
            if (_pathsR1G2.Contains(startStation) && _pathsR1G2.Contains(endStation))
                return Ways.R1G2;
            if (_pathsG1B1.Contains(startStation) && _pathsG1B1.Contains(endStation))
                return Ways.G1B1;
            if (_pathsG1B2.Contains(startStation) && _pathsG1B2.Contains(endStation))
                return Ways.G1B2;
            if (_pathsG1R2.Contains(startStation) && _pathsG1R2.Contains(endStation))
                return Ways.G1R2;
            if (_pathsB1G2.Contains(startStation) && _pathsB1G2.Contains(endStation))
                return Ways.B1G2;
            if (_pathsB1R2.Contains(startStation) && _pathsB1R2.Contains(endStation))
                return Ways.B1R2;
            return Ways.Default;
        }
        #region Cleaner

        private void Cleaner()
        {
            var i = 0;
            _startStation.Fill = _tempStart.Fill;
            _endStation.Fill = _tempEnd.Fill;

            foreach (var ellipse in _listEllipse)
            {
                ellipse.Fill = Brushes.White;
            }
            foreach (var rect in _listRectangle)
            {
                rect.Fill = _colors[i];
                i++;
            }
            _listRectangle.Clear();
            _listEllipse.Clear();
            _colors.Clear();
            if (_timerEllipse.IsEnabled)
                _timerEllipse.Stop();
            if (_timerRectangle.IsEnabled)
                _timerRectangle.Stop();
        }

        #endregion


        #region SeparateArrays

        private void SeparatedPath(Ellipse startStation, Ellipse endStation, FrameworkElement[] array)
        {
            
            switch (_order)
            {
                case Order.Asc:
                {
                        var start = int.MaxValue;
                        for (var i = 0; i < array.Length; i++)
                        {
                            if (array[i].Name == startStation.Name)
                                start = i;

                            if (array[i].Name == endStation.Name)
                                break;

                            if (start < i && array[i] is Ellipse)
                                _listEllipse.Add((Ellipse)array[i]);

                            if (start < i && array[i] is Rectangle)
                            {
                                _listRectangle.Add((Rectangle)array[i]);
                                var rect = (Rectangle)array[i];
                                _colors.Add(rect.Fill);
                            }

                        }
                        break;
                }
                case Order.Desc:
                {
                    var start = int.MinValue;
                        for (var i = array.Length - 1; i >= 0; i--)
                        {
                            if (array[i].Name == startStation.Name)
                                start = i;

                            if (array[i].Name == endStation.Name)
                                break;

                            if (start > i && array[i] is Ellipse)
                                _listEllipse.Add((Ellipse)array[i]);

                            if (start > i && array[i] is Rectangle)
                            {
                                _listRectangle.Add((Rectangle)array[i]);
                                var rect = (Rectangle)array[i];
                                _colors.Add(rect.Fill);
                            }
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
