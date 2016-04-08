using System;
using System.Windows;
using System.Windows.Controls;

namespace SWNAdmin.Controller
{
    public class HexagonGrid : Grid
    {
        protected override Size MeasureOverride(Size constraint)
        {
            var side = HexagonSideLength;
            var width = 2*side;
            var height = side*Math.Sqrt(3.0);
            var rowHeight = height;

            var availableChildSize = new Size(width, height);

            foreach (FrameworkElement child in InternalChildren)
            {
                child.Measure(availableChildSize);
            }

            var totalHeight = Rows*rowHeight;
            if (Columns > 1)
                totalHeight += 0.5*rowHeight;
            var totalWidth = Columns + 0.5*side;

            var totalSize = new Size(totalWidth, totalHeight);
            return totalSize;
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var side = HexagonSideLength;
            var width = 2*side;
            var height = side*Math.Sqrt(3.0);
            var colWidth = 0.75*width;
            var rowHeight = height;

            var childSize = new Size(width, height);

            foreach (FrameworkElement child in InternalChildren)
            {
                var row = GetRow(child);
                var col = GetColumn(child);

                var left = col*colWidth;
                var top = row*rowHeight;
                var isUnevenCol = col%2 != 0;
                if (isUnevenCol)
                    top += 0.5*rowHeight;

                child.Arrange(new Rect(new Point(left, top), childSize));
            }

            return arrangeSize;
        }

        #region HexagonSideLength

        public static readonly DependencyProperty HexagonSideLengthProperty =
            DependencyProperty.Register("HexagonSideLength", typeof (double), typeof (HexagonGrid),
                new FrameworkPropertyMetadata((double) 0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double HexagonSideLength
        {
            private get { return (double) GetValue(HexagonSideLengthProperty); }
            set { SetValue(HexagonSideLengthProperty, value); }
        }

        #endregion

        #region Rows

        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof (int), typeof (HexagonGrid),
                new FrameworkPropertyMetadata(1,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public int Rows
        {
            private get { return (int) GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        #endregion

        #region Columns

        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof (int), typeof (HexagonGrid),
                new FrameworkPropertyMetadata(1,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));


        public int Columns
        {
            private get { return (int) GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        #endregion
    }
}