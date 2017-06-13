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
        private const double Time = 100;
        private int _indexE;
        private int _indexR;
        private readonly DispatcherTimer _timerEllipse = new DispatcherTimer();
        private readonly DispatcherTimer _timerRectangle = new DispatcherTimer();
        private ChooseStation _chooseStation = ChooseStation.Start;
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
        private readonly FrameworkElement[] _pathsR2B2;
        private readonly FrameworkElement[] _pathsR2G2;
        private readonly FrameworkElement[] _pathsB2G2;

        private Ellipse _startStation;
        private Ellipse _endStation;
        private readonly Ellipse _tempStart = new Ellipse();
        private readonly Ellipse _tempEnd = new Ellipse();
        private readonly List<Ellipse> _listEllipse = new List<Ellipse>();
        private readonly List<Rectangle> _listRectangle = new List<Rectangle>();
        private readonly List<Brush> _colors = new List<Brush>();
        //private List<Ellipse> list = new List<Ellipse>();

        public MainWindow()
        {
            InitializeComponent();

            _pathsRedLine = new FrameworkElement[]
            {
                Academmistechko, PathAcademmistechkoZhytomyrska, Zhytomyrska, PathZhytomyrskaSviatoshyn, Sviatoshyn
                , PathSviatoshynNyvky, Nyvky, PathNyvkyBeresteiska, Beresteiska,
                PathBeresteiskaShuliavska, Shuliavska, PathShuliavskaPolitekhnichnyiInstytut,
                Politekhnichnyi_Instytut, PathPolitekhnichnyiInstytutVokzalna,
                Vokzalna, PathVokzalnaUniversytet, Universytet
                , PathUniversytetTeatralna, PathTeatralnaUniversytet,  Teatralna,
                PathTeatralnaKhreshchatyk, PathKhreshchatykTeatralna,
                 Khreshchatyk , PathKhreshchatykArsenalna, Arsenalna, PathArsenalnaDnipro, Dnipro, PathDniproHidropark,
                 Hidropark, PathHidroparkLivoberezhna, Livoberezhna, PathLivoberezhnaDarnytsia, Darnytsia, PathDarnytsiaChernihivska,
                 Chernihivska, PathChernihivskaLisova, Lisova
            };
            _pathsBlueLine = new FrameworkElement[]
            {
                Heroiv_Dnipra, PathHeroivDnipraMinska, Minska, PathMinskaObolon, Obolon, PathObolonPetrivka,
                Petrivka, PathPetrivkaTarasaShevchenka, Tarasa_Shevchenka, PathTarasaShevchenkaKontraktovaPloshcha,
                Kontraktova_Ploshcha, PathKontraktovaPloshchaPochtovaPloshcha, Poshtova_Ploshcha, PathPoshtovaPloshchaMaidanNezalezhnosti,Maidan_Nezalezhnosti,
                PathMaidanNezalezhnostiPloshchaLvaTolstoho, Ploshcha_Lva_Tolstoho, PathPloshchaLvaTolstohoOlimpiiska, Olimpiiska, PathOlimpiiskaPalatsUkrayina,
                Palats_Ukrayina, PathPalatsUkrayinaLybidska, Lybidska, PathLybidskaDemiivska, Demiivska, PathDemiivskaHolosiivka, Holosiivska,
                PathHolosiivskaVasylkivska, Vasylkivska, PathVasylkivskaVystavkovyiTsentr, Vystavkovyi_Tsentr, PathVystavkovyiTsentrIpodrom,
                Ipodrom, PathIpodromTeremky, Teremky
            };

            _pathsGreenLine = new FrameworkElement[]
            {
                Syrets, PathSyretsDorohozhychi, Dorohozhychi, PathDorohozhychiLukianivska, Lukianivska, PathLukianivskaZolotiVorota,
                Zoloti_Vorota,PathZolotiVorotaPalatsSportu,PathPalatsSportuZolotiVorota, Palats_Sportu, PathPalatsSportuKlovska, PathKlovskaPalatsSportu,
                Klovska, PathKlovskaPecherska,
                Pecherska, PathPecherskaDruzhbyNarodiv, Druzhby_Narodiv, PathDruzhbyNarodivVydubychi, Vydubychi, PathVydubychiSlavutych,
                Slavutych, PathSlavutychOsokorky, Osokorky, PathOsokorkyPozniaky, Pozniaky, PathPozniakyKharkivska, Kharkivska,
                PathKharkivskaVyrlytsia, Vyrlytsia, PathVyrlytsiaBorispilska, Borispilska, PathBoryspilskaChervonyKhutir, Chervony_Khutir
            };

            _pathsR1B1 = new FrameworkElement[]
            {
                Academmistechko, PathAcademmistechkoZhytomyrska, Zhytomyrska, PathZhytomyrskaSviatoshyn, Sviatoshyn
                , PathSviatoshynNyvky, Nyvky, PathNyvkyBeresteiska, Beresteiska,
                PathBeresteiskaShuliavska, Shuliavska, PathShuliavskaPolitekhnichnyiInstytut,
                Politekhnichnyi_Instytut, PathPolitekhnichnyiInstytutVokzalna,
                Vokzalna, PathVokzalnaUniversytet, Universytet
                , PathUniversytetTeatralna, PathTeatralnaUniversytet,  Teatralna,
                PathTeatralnaKhreshchatyk, PathKhreshchatykTeatralna,
                 Khreshchatyk,Maidan_Nezalezhnosti, PathPoshtovaPloshchaMaidanNezalezhnosti,Poshtova_Ploshcha,
                 PathKontraktovaPloshchaPochtovaPloshcha,Kontraktova_Ploshcha, PathTarasaShevchenkaKontraktovaPloshcha,
                 Tarasa_Shevchenka,PathPetrivkaTarasaShevchenka,Petrivka,PathObolonPetrivka,Obolon,PathMinskaObolon,
                 Minska,PathHeroivDnipraMinska,Heroiv_Dnipra
            };

            _pathsR1G2 = new FrameworkElement[]
            {
                Academmistechko, PathAcademmistechkoZhytomyrska, Zhytomyrska, PathZhytomyrskaSviatoshyn, Sviatoshyn
                , PathSviatoshynNyvky, Nyvky, PathNyvkyBeresteiska, Beresteiska,
                PathBeresteiskaShuliavska, Shuliavska, PathShuliavskaPolitekhnichnyiInstytut,
                Politekhnichnyi_Instytut, PathPolitekhnichnyiInstytutVokzalna,
                Vokzalna, PathVokzalnaUniversytet, Universytet
                , PathUniversytetTeatralna, PathTeatralnaUniversytet,  Teatralna,Zoloti_Vorota,PathZolotiVorotaPalatsSportu,
                PathPalatsSportuZolotiVorota, Palats_Sportu, PathPalatsSportuKlovska, PathKlovskaPalatsSportu, Klovska, PathKlovskaPecherska,
                Pecherska, PathPecherskaDruzhbyNarodiv, Druzhby_Narodiv, PathDruzhbyNarodivVydubychi, Vydubychi, PathVydubychiSlavutych,
                Slavutych, PathSlavutychOsokorky, Osokorky, PathOsokorkyPozniaky, Pozniaky, PathPozniakyKharkivska, Kharkivska,
                PathKharkivskaVyrlytsia, Vyrlytsia, PathVyrlytsiaBorispilska, Borispilska, PathBoryspilskaChervonyKhutir, Chervony_Khutir
            };

            _pathsR1G1 = new FrameworkElement[]
            {
                Academmistechko, PathAcademmistechkoZhytomyrska, Zhytomyrska, PathZhytomyrskaSviatoshyn, Sviatoshyn
                , PathSviatoshynNyvky, Nyvky, PathNyvkyBeresteiska, Beresteiska,
                PathBeresteiskaShuliavska, Shuliavska, PathShuliavskaPolitekhnichnyiInstytut,
                Politekhnichnyi_Instytut, PathPolitekhnichnyiInstytutVokzalna,
                Vokzalna, PathVokzalnaUniversytet, Universytet
                , PathUniversytetTeatralna, PathTeatralnaUniversytet, Teatralna, Zoloti_Vorota,PathLukianivskaZolotiVorota,
                Lukianivska,PathDorohozhychiLukianivska,Dorohozhychi,PathSyretsDorohozhychi, Syrets
            };

            _pathsR1B2 = new FrameworkElement[]
            {
                Academmistechko, PathAcademmistechkoZhytomyrska, Zhytomyrska, PathZhytomyrskaSviatoshyn, Sviatoshyn
                , PathSviatoshynNyvky, Nyvky, PathNyvkyBeresteiska, Beresteiska,
                PathBeresteiskaShuliavska, Shuliavska, PathShuliavskaPolitekhnichnyiInstytut,
                Politekhnichnyi_Instytut, PathPolitekhnichnyiInstytutVokzalna,
                Vokzalna, PathVokzalnaUniversytet, Universytet
                , PathUniversytetTeatralna, PathTeatralnaUniversytet,  Teatralna,
                PathTeatralnaKhreshchatyk, PathKhreshchatykTeatralna,
                 Khreshchatyk,Maidan_Nezalezhnosti,PathMaidanNezalezhnostiPloshchaLvaTolstoho, Ploshcha_Lva_Tolstoho, PathPloshchaLvaTolstohoOlimpiiska, Olimpiiska, PathOlimpiiskaPalatsUkrayina,
                Palats_Ukrayina, PathPalatsUkrayinaLybidska, Lybidska, PathLybidskaDemiivska, Demiivska, PathDemiivskaHolosiivka, Holosiivska,
                PathHolosiivskaVasylkivska, Vasylkivska, PathVasylkivskaVystavkovyiTsentr, Vystavkovyi_Tsentr, PathVystavkovyiTsentrIpodrom,
                Ipodrom, PathIpodromTeremky, Teremky
            };
            _pathsB1R2 = new FrameworkElement[]
            {
                Heroiv_Dnipra, PathHeroivDnipraMinska, Minska, PathMinskaObolon, Obolon, PathObolonPetrivka,
                Petrivka, PathPetrivkaTarasaShevchenka, Tarasa_Shevchenka, PathTarasaShevchenkaKontraktovaPloshcha,
                Kontraktova_Ploshcha, PathKontraktovaPloshchaPochtovaPloshcha, Poshtova_Ploshcha, PathPoshtovaPloshchaMaidanNezalezhnosti,Maidan_Nezalezhnosti,
                 Khreshchatyk, PathKhreshchatykArsenalna, Arsenalna, PathArsenalnaDnipro, Dnipro, PathDniproHidropark,
                 Hidropark, PathHidroparkLivoberezhna, Livoberezhna, PathLivoberezhnaDarnytsia, Darnytsia, PathDarnytsiaChernihivska,
                 Chernihivska, PathChernihivskaLisova, Lisova
            };

            _pathsB1G2 = new FrameworkElement[]
            {
                Heroiv_Dnipra, PathHeroivDnipraMinska, Minska, PathMinskaObolon, Obolon, PathObolonPetrivka,
                Petrivka, PathPetrivkaTarasaShevchenka, Tarasa_Shevchenka, PathTarasaShevchenkaKontraktovaPloshcha,
                Kontraktova_Ploshcha, PathKontraktovaPloshchaPochtovaPloshcha, Poshtova_Ploshcha,PathPoshtovaPloshchaMaidanNezalezhnosti,
                Maidan_Nezalezhnosti,PathMaidanNezalezhnostiPloshchaLvaTolstoho,Ploshcha_Lva_Tolstoho
                ,Palats_Sportu, PathPalatsSportuKlovska, PathKlovskaPalatsSportu, Klovska, PathKlovskaPecherska,
                Pecherska, PathPecherskaDruzhbyNarodiv, Druzhby_Narodiv, PathDruzhbyNarodivVydubychi, Vydubychi, PathVydubychiSlavutych,
                Slavutych, PathSlavutychOsokorky, Osokorky, PathOsokorkyPozniaky, Pozniaky, PathPozniakyKharkivska, Kharkivska,
                PathKharkivskaVyrlytsia, Vyrlytsia, PathVyrlytsiaBorispilska, Borispilska, PathBoryspilskaChervonyKhutir, Chervony_Khutir
            };
            _pathsG1B1 = new FrameworkElement[]
            {
                Syrets, PathSyretsDorohozhychi, Dorohozhychi, PathDorohozhychiLukianivska, Lukianivska, PathLukianivskaZolotiVorota,
                Zoloti_Vorota,PathZolotiVorotaPalatsSportu, PathPalatsSportuZolotiVorota, Palats_Sportu,Ploshcha_Lva_Tolstoho, PathMaidanNezalezhnostiPloshchaLvaTolstoho,
                Maidan_Nezalezhnosti, PathPoshtovaPloshchaMaidanNezalezhnosti,Poshtova_Ploshcha,
                 PathKontraktovaPloshchaPochtovaPloshcha,Kontraktova_Ploshcha, PathTarasaShevchenkaKontraktovaPloshcha,
                 Tarasa_Shevchenka,PathPetrivkaTarasaShevchenka,Petrivka,PathObolonPetrivka,Obolon,PathMinskaObolon,
                 Minska,PathHeroivDnipraMinska,Heroiv_Dnipra
            };

            _pathsG1R2 = new FrameworkElement[]
            {
                Syrets, PathSyretsDorohozhychi, Dorohozhychi, PathDorohozhychiLukianivska, Lukianivska, PathLukianivskaZolotiVorota,
                Zoloti_Vorota, Teatralna,
                PathTeatralnaKhreshchatyk, PathKhreshchatykTeatralna,
                 Khreshchatyk , PathKhreshchatykArsenalna, Arsenalna, PathArsenalnaDnipro, Dnipro, PathDniproHidropark,
                 Hidropark, PathHidroparkLivoberezhna, Livoberezhna, PathLivoberezhnaDarnytsia, Darnytsia, PathDarnytsiaChernihivska,
                 Chernihivska, PathChernihivskaLisova, Lisova
            };

            _pathsG1B2 = new FrameworkElement[]
            {
                Syrets, PathSyretsDorohozhychi, Dorohozhychi, PathDorohozhychiLukianivska, Lukianivska, PathLukianivskaZolotiVorota,
                Zoloti_Vorota,PathZolotiVorotaPalatsSportu, PathPalatsSportuZolotiVorota, Palats_Sportu, Ploshcha_Lva_Tolstoho, PathPloshchaLvaTolstohoOlimpiiska, Olimpiiska, PathOlimpiiskaPalatsUkrayina,
                Palats_Ukrayina, PathPalatsUkrayinaLybidska, Lybidska, PathLybidskaDemiivska, Demiivska, PathDemiivskaHolosiivka, Holosiivska,
                PathHolosiivskaVasylkivska, Vasylkivska, PathVasylkivskaVystavkovyiTsentr, Vystavkovyi_Tsentr, PathVystavkovyiTsentrIpodrom,
                Ipodrom, PathIpodromTeremky, Teremky
            };

            _pathsR2B2 = new FrameworkElement[]
            {
                Lisova,PathChernihivskaLisova,Chernihivska,PathDarnytsiaChernihivska,Darnytsia,PathLivoberezhnaDarnytsia,Livoberezhna,PathHidroparkLivoberezhna,
                Hidropark,PathDniproHidropark,Dnipro,PathArsenalnaDnipro,Arsenalna,PathKhreshchatykArsenalna,Khreshchatyk,
                Maidan_Nezalezhnosti,PathMaidanNezalezhnostiPloshchaLvaTolstoho, Ploshcha_Lva_Tolstoho, PathPloshchaLvaTolstohoOlimpiiska, Olimpiiska, PathOlimpiiskaPalatsUkrayina,
                Palats_Ukrayina, PathPalatsUkrayinaLybidska, Lybidska, PathLybidskaDemiivska, Demiivska, PathDemiivskaHolosiivka, Holosiivska,
                PathHolosiivskaVasylkivska, Vasylkivska, PathVasylkivskaVystavkovyiTsentr, Vystavkovyi_Tsentr, PathVystavkovyiTsentrIpodrom,
                Ipodrom, PathIpodromTeremky, Teremky
            };
            _pathsR2G2 = new FrameworkElement[]
            {
                Lisova,PathChernihivskaLisova,Chernihivska,PathDarnytsiaChernihivska,Darnytsia,PathLivoberezhnaDarnytsia,Livoberezhna,PathHidroparkLivoberezhna,
                Hidropark,PathDniproHidropark,Dnipro,PathArsenalnaDnipro,Arsenalna,PathKhreshchatykArsenalna,Khreshchatyk, PathKhreshchatykTeatralna, PathTeatralnaKhreshchatyk
                ,Teatralna,Zoloti_Vorota, PathZolotiVorotaPalatsSportu, PathPalatsSportuZolotiVorota, Palats_Sportu, PathPalatsSportuKlovska, PathKlovskaPalatsSportu, Klovska, PathKlovskaPecherska,
                Pecherska, PathPecherskaDruzhbyNarodiv, Druzhby_Narodiv, PathDruzhbyNarodivVydubychi, Vydubychi, PathVydubychiSlavutych,
                Slavutych, PathSlavutychOsokorky, Osokorky, PathOsokorkyPozniaky, Pozniaky, PathPozniakyKharkivska, Kharkivska,
                PathKharkivskaVyrlytsia, Vyrlytsia, PathVyrlytsiaBorispilska, Borispilska, PathBoryspilskaChervonyKhutir, Chervony_Khutir
            };
            _pathsB2G2 = new FrameworkElement[]
            {
                Teremky,PathIpodromTeremky,Ipodrom,PathVystavkovyiTsentrIpodrom,Vystavkovyi_Tsentr,PathVasylkivskaVystavkovyiTsentr,Vasylkivska,
                PathHolosiivskaVasylkivska,Holosiivska,PathDemiivskaHolosiivka,Demiivska,PathLybidskaDemiivska,Lybidska,PathPalatsUkrayinaLybidska,
                Palats_Ukrayina,PathOlimpiiskaPalatsUkrayina,Olimpiiska,PathPloshchaLvaTolstohoOlimpiiska,Ploshcha_Lva_Tolstoho,
                Palats_Sportu, PathPalatsSportuKlovska, PathKlovskaPalatsSportu, Klovska, PathKlovskaPecherska,
                Pecherska, PathPecherskaDruzhbyNarodiv, Druzhby_Narodiv, PathDruzhbyNarodivVydubychi, Vydubychi, PathVydubychiSlavutych,
                Slavutych, PathSlavutychOsokorky, Osokorky, PathOsokorkyPozniaky, Pozniaky, PathPozniakyKharkivska, Kharkivska,
                PathKharkivskaVyrlytsia, Vyrlytsia, PathVyrlytsiaBorispilska, Borispilska, PathBoryspilskaChervonyKhutir, Chervony_Khutir
            };
            SubscribeOnEvent();
            _timerEllipse.Tick += TimerEllipseAnimated;
            _timerEllipse.Interval = TimeSpan.FromMilliseconds(Time);
            _timerRectangle.Tick += TimerAnimatedRectangle;
            _timerRectangle.Interval = TimeSpan.FromMilliseconds(Time);
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
            var point = sender as Ellipse;
            switch (_chooseStation)
            {
                case ChooseStation.Start:
                    if (point != null)
                    {
                        TextBlockWay.Text += point.Name + "\n";
                        point.MouseDown -= ClickMouseAction;
                        _tempStart.Fill = point.Fill;
                        point.Fill = Brushes.Yellow;
                        _startStation = point;
                        _chooseStation = ChooseStation.End;
                    }
                    break;
                case ChooseStation.End:
                    _startStation.MouseDown += ClickMouseAction;
                    if (point != null)
                    {
                        TextBlockWay.Text += point.Name + "\n";
                        _tempEnd.Fill = point.Fill;
                        point.Fill = Brushes.Yellow;
                        _endStation = point;
                        var way = ChoosenWay(_startStation, _endStation);
                        _chooseStation = ChooseStation.NewPath;
                        Order pathOrder;
                        switch (way)
                        {
                            case Ways.RedLine:
                                pathOrder = GetPathOrder(_pathsRedLine);
                                SeparatedPath(_startStation, _endStation, _pathsRedLine, pathOrder);
                                break;
                            case Ways.BlueLine:
                                pathOrder = GetPathOrder(_pathsBlueLine);
                                SeparatedPath(_startStation, _endStation, _pathsBlueLine, pathOrder);
                                break;
                            case Ways.GreenLine:
                                pathOrder = GetPathOrder(_pathsGreenLine);
                                SeparatedPath(_startStation, _endStation, _pathsGreenLine, pathOrder);
                                break;
                            case Ways.R1B1:
                                pathOrder = GetPathOrder(_pathsR1B1);
                                SeparatedPath(_startStation, _endStation, _pathsR1B1, pathOrder);
                                break;
                            case Ways.R1B2:
                                pathOrder = GetPathOrder(_pathsR1B2);
                                SeparatedPath(_startStation, _endStation, _pathsR1B2, pathOrder);
                                break;
                            case Ways.R1G1:
                                pathOrder = GetPathOrder(_pathsR1G1);
                                SeparatedPath(_startStation, _endStation, _pathsR1G1, pathOrder);
                                break;
                            case Ways.R1G2:
                                pathOrder = GetPathOrder(_pathsR1G2);
                                SeparatedPath(_startStation, _endStation, _pathsR1G2, pathOrder);
                                break;
                            case Ways.G1B1:
                                pathOrder = GetPathOrder(_pathsG1B1);
                                SeparatedPath(_startStation, _endStation, _pathsG1B1, pathOrder);
                                break;
                            case Ways.G1B2:
                                pathOrder = GetPathOrder(_pathsG1B2);
                                SeparatedPath(_startStation, _endStation, _pathsG1B2, pathOrder);
                                break;
                            case Ways.G1R2:
                                pathOrder = GetPathOrder(_pathsG1R2);
                                SeparatedPath(_startStation, _endStation, _pathsG1R2, pathOrder);
                                break;
                            case Ways.B1G2:
                                pathOrder = GetPathOrder(_pathsB1G2);
                                SeparatedPath(_startStation, _endStation, _pathsB1G2, pathOrder);
                                break;
                            case Ways.B1R2:
                                pathOrder = GetPathOrder(_pathsB1R2);
                                SeparatedPath(_startStation, _endStation, _pathsB1R2, pathOrder);
                                break;
                            case Ways.R2B2:
                                pathOrder = GetPathOrder(_pathsR2B2);
                                SeparatedPath(_startStation, _endStation, _pathsR2B2, pathOrder);
                                break;
                            case Ways.R2G2:
                                pathOrder = GetPathOrder(_pathsR2G2);
                                SeparatedPath(_startStation, _endStation, _pathsR2G2, pathOrder);
                                break;
                            case Ways.B2G2:
                                pathOrder = GetPathOrder(_pathsB2G2);
                                SeparatedPath(_startStation, _endStation, _pathsB2G2, pathOrder);
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

        private Order GetPathOrder(FrameworkElement[] array)
        {
            
            foreach (var path in array)
            {
                if (path.Name == _startStation.Name)
                {
                    return Order.Asc;
                    
                }
                if (path.Name == _endStation.Name)
                {
                    return Order.Desc;
                }
            }
            return Order.Asc;
        }

        private Ways ChoosenWay(Ellipse startStation, Ellipse endStation)
        {
            if (_pathsRedLine.Contains(startStation) && _pathsRedLine.Contains(endStation))
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
            if (_pathsR2B2.Contains(startStation) && _pathsR2B2.Contains(endStation))
                return Ways.R2B2;
            if (_pathsR2G2.Contains(startStation) && _pathsR2G2.Contains(endStation))
                return Ways.R2G2;
            if (_pathsB2G2.Contains(startStation) && _pathsB2G2.Contains(endStation))
                return Ways.B2G2;
            return Ways.RedLine;
        }
        #endregion
        #region SeparateArrays

        private void SeparatedPath(Ellipse startStation, Ellipse endStation, FrameworkElement[] array, Order pathOrder )
        {

            switch (pathOrder)
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
            if (_listRectangle.Count == 0)
            {
                ProgressBar.Value = 100;
            }
            else
            {
                _indexR = 0;
                _indexE = 0;
                if (!_timerEllipse.IsEnabled)
                    _timerEllipse.Start();
                if (!_timerRectangle.IsEnabled)
                    _timerRectangle.Start();
            }
        }
        #endregion
        #region AnimatedRoute
        private void TimerEllipseAnimated(object sender, EventArgs eventArgs)
        {
            var flag = true;
            if (_indexE + 1 == _listEllipse.Count)
            {
                ColorAnimated(_listEllipse[_indexE]);
                
                ProgressBar.Value = ProgressBar.Value % 2 == 0? 100 / _listEllipse.Count * (_indexE+1): 100 / _listEllipse.Count * (_indexE + 2);
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
                ProgressBar.Value = 100/_listEllipse.Count * _indexE;
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

        private void ColorAnimated(UIElement element)
        {
            var storyboard = new Storyboard();
            var colorAnimation = new ColorAnimation(Colors.Yellow, TimeSpan.FromMilliseconds(Time));
            element.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            Storyboard.SetTarget(colorAnimation, element);
            Storyboard.SetTargetProperty(colorAnimation, new PropertyPath("Fill.Color"));
            storyboard.Children.Add(colorAnimation);
            storyboard.Begin();
        }
        #endregion
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
            TextBlockWay.Text = null;
            _listRectangle.Clear();
            _listEllipse.Clear();
            _colors.Clear();
            ProgressBar.Value = 0;
            if (_timerEllipse.IsEnabled)
                _timerEllipse.Stop();
            if (_timerRectangle.IsEnabled)
                _timerRectangle.Stop();
        }

        #endregion
    }
}
