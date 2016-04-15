using System.Windows;
using System.Windows.Controls;

namespace SWNAdmin.Forms
{
    /// <summary>
    ///     Interaction logic for Map.xaml
    /// </summary>
    public partial class Map
    {
        public int MapColumns = 10;
        //Definiere Spalten und Reihen
        public int MapRows = 10;

        public Map()
        {
            InitializeComponent();
        }

        private void Window_Loaded( object sender, RoutedEventArgs e )
        {
            //bei Fenster Laden wird die Karte Generiert
            GenerateMap();
        }

        private void GenerateMap()
        {
            //Generiert die Karte
            var r = 0;
            int c;
            for (c = 0; 0 < MapRows; c++)
            {
                var columnButton = new Button();
                Grid.SetRow(columnButton, r);
                Grid.SetColumn(columnButton, 0);
                columnButton.Name = "Button" + r + 0;
                columnButton.Content = r + "," + 0;
                HexGrid1.Children.Add(columnButton);
                for (r = 0; r < MapRows; r++)
                {
                    var rowButton = new Button();
                    Grid.SetRow(rowButton, r);
                    Grid.SetColumn(rowButton, 0);
                    rowButton.Name = "Button" + r + 0;
                    rowButton.Content = r + "," + 0;
                    HexGrid1.Children.Add(rowButton);
                }
            }
        }
    }
}