using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wpf_ChangeImage
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
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
