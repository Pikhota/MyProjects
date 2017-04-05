using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pacman
{
    public class Pac
    {
        public  double Speed = 0;
        public  Rect Pacman = new Rect(0, 0, 50, 50);
        public  TranslateTransform Trans = new TranslateTransform();
        public  SolidColorBrush SolidColorBrush = new SolidColorBrush { Color = Color.FromRgb(255,250,0) };
        public  Path PacPath =new Path();
    }

}
