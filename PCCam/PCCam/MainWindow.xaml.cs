using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Media.

namespace PCCam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += Cam;


        }

        private void Cam(object sender, RoutedEventArgs routedEventArgs)
        {
            CameraCaptureUI camUI = new CameraCaptureUI(); 
            VideoSources.ItemsSource = CaptureDeviceConfiguration.GetAvailableVideoCaptureDevices();
        }
    }
}
