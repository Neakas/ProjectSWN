using System.Windows;
using System.Windows.Controls;

namespace SWNAdmin
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class Map : Window
    {
        //Definiere Spalten und Reihen
        public int MapRows = 10;
        public int MapColumns = 10;

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
            int R = 0;
            int C = 0;
            for (C = 0; C < MapRows; C++)
            {
                Button ColumnButton = new Button();
                Grid.SetRow(ColumnButton, R);
                Grid.SetColumn(ColumnButton, C);
                ColumnButton.Name = "Button" + R + C;
                ColumnButton.Content = R + "," + C;
                HexGrid1.Children.Add(ColumnButton);
                for (R = 0; R < MapRows; R++)
                {
                    Button RowButton = new Button();
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
