using System.Windows;
using System.Windows.Controls;

namespace SWN.Forms
{
    /// <summary>
    ///     Interaction logic for Map.xaml
    /// </summary>
    public partial class Map : Window
    {
        public int MapColumns = 10;
        //Definiere Spalten und Reihen
        public int MapRows = 10;

        public Map()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //bei Fenster Laden wird die Karte Generiert
            GenerateMap();
        }

        private void GenerateMap()
        {
            //Generiert die Karte
            var R = 0;
            var C = 0;
            for (C = 0; C < MapRows; C++)
            {
                var ColumnButton = new Button();
                Grid.SetRow(ColumnButton, R);
                Grid.SetColumn(ColumnButton, C);
                ColumnButton.Name = "Button" + R + C;
                ColumnButton.Content = R + "," + C;
                HexGrid1.Children.Add(ColumnButton);
                for (R = 0; R < MapRows; R++)
                {
                    var RowButton = new Button();
                    Grid.SetRow(RowButton, R);
                    Grid.SetColumn(RowButton, C);
                    RowButton.Name = "Button" + R + C;
                    RowButton.Content = R + "," + C;
                    HexGrid1.Children.Add(RowButton);
                }
            }
        }
    }
}