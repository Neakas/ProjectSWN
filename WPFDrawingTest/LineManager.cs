using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFDrawingTest
{
    internal class LineManager
    {
        private readonly Canvas _mainCanvas;
        private bool _bShiftMode;
        private bool _bDrawStarted;
        private Point _pMouseStart;
        private Point _pMouseEnd;
        private Line _previewLine;

        public LineManager(Canvas linkedCanvas)
        {
            _mainCanvas = linkedCanvas;
        }

        public void MouseDownEvent(MouseButtonEventArgs e)
        {
            if (e.ButtonState != MouseButtonState.Pressed) return;
            if (!_bDrawStarted)
            {
                Mouse.Capture(_mainCanvas);
                _pMouseStart = e.GetPosition(_mainCanvas);
                _pMouseEnd = e.GetPosition(_mainCanvas);
                _mainCanvas.MouseMove += MainCanvas_MouseMove;
                DoLinePreviewDraw();
            }
            else
            {
                if (_bShiftMode)
                {
                    DoLineDraw();
                    Mouse.Capture(_mainCanvas);
                    DoLinePreviewDraw();
                    _pMouseStart = e.GetPosition(_mainCanvas);
                }
                else
                {
                    Mouse.Capture(null);
                    _pMouseEnd = e.GetPosition(_mainCanvas);
                    DoLineDraw();
                    _mainCanvas.MouseMove -= MainCanvas_MouseMove;
                }
            }
        }

        private void DoLinePreviewDraw()
        {
            //TODO Check if Still over Canvas
            _bDrawStarted = true;
            _previewLine = new Line
            {
                X1 = _pMouseStart.X,
                Y1 = _pMouseStart.Y,
                X2 = _pMouseEnd.X,
                Y2 = _pMouseEnd.Y,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                StrokeThickness = MapController.StrokeSize,
                Stroke = Brushes.Gray
            };
            _mainCanvas.Children.Add(_previewLine);
        }

        public void DoLineDraw()
        {
            RemoveLinePreview();


            var myLine = new Line
            {
                Stroke = Brushes.Black,
                X1 = _pMouseStart.X,
                Y1 = _pMouseStart.Y,
                X2 = _pMouseEnd.X,
                Y2 = _pMouseEnd.Y,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                StrokeThickness = MapController.StrokeSize,
            };
            
            _mainCanvas.Children.Add(myLine);
       
            if (!_bShiftMode)
            {
                _bDrawStarted = false;
            }
        }

        private void PaintLineMouseMove(MouseEventArgs e)
        {
            if (_previewLine == null || !_bDrawStarted || e.LeftButton != MouseButtonState.Released) return;
            _pMouseEnd = e.GetPosition(_mainCanvas);
            RemoveLinePreview();
            DoLinePreviewDraw();
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            PaintLineMouseMove(e);
        }

        private void RemoveLinePreview()
        {
            if (!_mainCanvas.Children.Contains(_previewLine)) return;
            _mainCanvas.Children.Remove(_previewLine);
            _previewLine = null;
        }

        public void SetShiftMode(bool mode)
        {
            _bShiftMode = mode;
        }

        public void AbortDraw()
        {
            RemoveLinePreview();
            _bDrawStarted = false;
        }
    }
}
