using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFDrawingTest
{
    internal class BrushManager
    {
        private readonly Canvas _mainCanvas;
        private Point _currentPoint;
        private bool _bDrawStarted;
        private Polyline polyline;
        private List<Line> delList;

        public BrushManager(Canvas linkedCanvas)
        {
            _mainCanvas = linkedCanvas;
        }

        public void MouseDownEvent(MouseButtonEventArgs e)
        {
            polyline = null;
            delList = null;
            polyline = new Polyline
            {
                Stroke = Brushes.Black,
                StrokeThickness = MapController.StrokeSize
            };
            polyline.Points.Clear();
            delList = new List<Line>();
            _mainCanvas.MouseUp += MouseUpEvent;
            if (!_bDrawStarted)
            {
                if (e.ButtonState != MouseButtonState.Pressed) return;
                _currentPoint = e.GetPosition(_mainCanvas);
                _mainCanvas.MouseMove += MainCanvas_MouseMove;
                _bDrawStarted = true;
            }
            else
            {
                _mainCanvas.MouseMove -= MainCanvas_MouseMove;
            }
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (delList == null)
            {
                delList = new List<Line>();
            }
            if (e.LeftButton != MouseButtonState.Pressed) return;
            var line = new Line
            {
                Stroke = SystemColors.WindowFrameBrush,
                StrokeThickness = MapController.StrokeSize,
                X1 = _currentPoint.X,
                Y1 = _currentPoint.Y,
                X2 = e.GetPosition(_mainCanvas).X,
                Y2 = e.GetPosition(_mainCanvas).Y
            };
            
            _currentPoint = e.GetPosition(_mainCanvas);
            polyline.Points.Add(_currentPoint);

            _mainCanvas.Children.Add(line);
            delList.Add(line);
        }

        private void MouseUpEvent(object sender,MouseButtonEventArgs e)
        {
            _bDrawStarted = false;
            _mainCanvas.MouseMove -= MainCanvas_MouseMove;

            foreach (var delitem in delList)
            {
                _mainCanvas.Children.Remove(delitem);
            }
            _mainCanvas.Children.Add(polyline);
            _mainCanvas.MouseUp -= MouseUpEvent;
        }

    }
}
