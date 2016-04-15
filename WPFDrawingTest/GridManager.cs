using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFDrawingTest
{
    internal class GridManager
    {
        public readonly Grid MainGrid;
        public GridManager(Grid targetGridControl,int cellWidth = 50, int cellHeight = 50)
        {
            MainGrid = targetGridControl;
            BuildGrid(cellWidth,cellHeight);
            MainGrid.Children.Insert(0,BuildGrid(cellWidth, cellHeight));
        }

        private static Border BuildGrid(int cellWidth = 50, int cellHeight = 50)
        {
            var gridBorder = new Border();

            var cellRect = new Rectangle
            {
                Stroke = Brushes.DarkGray,
                StrokeThickness = 1,
                Height = 50,
                Width = 50,
                StrokeDashArray = new DoubleCollection(new double[] {5, 3})
            };

            var vb = new VisualBrush(cellRect)
            {
                TileMode = TileMode.Tile,
                Viewport = new System.Windows.Rect(0, 0, cellHeight, cellWidth),
                ViewportUnits = BrushMappingMode.Absolute,
                Viewbox = new System.Windows.Rect(0, 0, cellHeight, cellWidth),
                ViewboxUnits = BrushMappingMode.Absolute
            };
            gridBorder.Background = vb;

            return gridBorder;
        }
    }
}
