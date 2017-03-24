using System;
using System.Windows;
using System.Windows.Media;

namespace Pacman
{
    internal static class Pac
    {
        public static double X { get; set; }
        public static double Y { get; set; }
        public static double Speed { get; set; }
        public static TranslateTransform Trans = new TranslateTransform();
        public static double Time { get; set; }
    }

}
