using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFDrawingTest
{
    internal class Token : Shape
    {

        public enum TokenSize { Small, Normal, Large }
        public static Dictionary<TokenSize, int> SizeToIntDict = new Dictionary<TokenSize, int>()
        {
            { TokenSize.Small, 25 },
            { TokenSize.Normal, 50 },
            { TokenSize.Large, 100 }

        }
        ;
        protected override Geometry DefiningGeometry
        {
            get
            {
                Geometry g = new EllipseGeometry(new Rect(new Size(Width,Height)));
                return g;
            }
        }

        public Token()
        {
            SetupContextMenu();
        }

        private void SetupContextMenu()
        {
            var ctm = new ContextMenu {Background = Brushes.DarkGray};
            var miSmall = new MenuItem();
            miSmall.Click += ChangetoSmall;
            miSmall.Header = "Small";
            miSmall.Foreground = Brushes.White;
            var miNormal = new MenuItem();
            miNormal.Click += ChangetoNormal;
            miNormal.Header = "Normal";
            miNormal.Foreground = Brushes.White;
            var miLarge = new MenuItem();
            miLarge.Click += ChangetoLarge;
            miLarge.Header = "Large";
            miLarge.Foreground = Brushes.White;
            var miChangeSize = new MenuItem
            {
                Header = "Change Size",
                Foreground = Brushes.White
            };
            miChangeSize.Items.Add(miSmall);
            miChangeSize.Items.Add(miNormal);
            miChangeSize.Items.Add(miLarge);
            ctm.Items.Add(miChangeSize);
            
            ContextMenu = ctm;
        }

        private void ChangetoSmall(object sender, RoutedEventArgs e)
        {
            ChangeSize(TokenSize.Small);
        }
        private void ChangetoNormal(object sender, RoutedEventArgs e)
        {
            ChangeSize(TokenSize.Normal);
        }
        private void ChangetoLarge(object sender, RoutedEventArgs e)
        {
            ChangeSize(TokenSize.Large);
        }

        private void ChangeSize(TokenSize size)
        {
            Width = SizeToIntDict[size];
            Height = SizeToIntDict[size];
        }
    }
}
