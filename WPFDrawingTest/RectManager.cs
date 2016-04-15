using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFDrawingTest
{
    internal class RectManager
    {
        private readonly Canvas _mainCanvas;
        private bool _bDrawStarted;
        private Point _pMouseStart;
        private Point _pMouseEnd;
        private Rectangle _previewRect;

        public RectManager(Canvas linkedCanvas)
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
                DoRectPreviewDraw();
            }
            else
            {
                Mouse.Capture(null);
                _pMouseEnd = e.GetPosition(_mainCanvas);
                DoRectDraw();
                _mainCanvas.MouseMove -= MainCanvas_MouseMove;
            }
        }

        private void DoRectPreviewDraw()
        {
            _bDrawStarted = true;
            {
                _previewRect = new Rectangle();

                var newWidth = _pMouseEnd.X - _pMouseStart.X;
                var newHeight = _pMouseEnd.Y - _pMouseStart.Y;
                if (_pMouseStart.Y >= _pMouseEnd.Y || _pMouseStart.X >= _pMouseEnd.X)
                {
                    ScaleTransform st;
                    if (_pMouseStart.Y >= _pMouseEnd.Y && _pMouseStart.X >= _pMouseEnd.X)
                    {
                        _previewRect.RenderTransformOrigin = new Point(0.5, 0.5);
                        newWidth = _pMouseStart.X - _pMouseEnd.X;
                        newHeight = _pMouseStart.Y - _pMouseEnd.Y;
                        if (newWidth < 0)
                        {
                            newWidth *= -1;
                        }
                        if (newHeight < 0)
                        {
                            newHeight *= -1;
                        }
                        st = new ScaleTransform
                        {
                            ScaleY = -1,
                            ScaleX = -1
                        };
                        _previewRect.RenderTransform = st;
                        _previewRect.Width = newWidth;
                        _previewRect.Height = newHeight;
                        Canvas.SetTop(_previewRect, _pMouseStart.Y - newHeight);
                        Canvas.SetLeft(_previewRect, _pMouseStart.X - newWidth);
                    }
                    else
                    {
                        if (_pMouseStart.Y >= _pMouseEnd.Y)
                        {
                            _previewRect.RenderTransformOrigin = new Point(0.5, 0.5);
                            newHeight = _pMouseStart.Y - _pMouseEnd.Y;
                            if (newHeight < 0)
                            {
                                newHeight *= -1;
                            }
                            st = new ScaleTransform {ScaleY = -1};
                            _previewRect.RenderTransform = st;
                            _previewRect.Width = newWidth;
                            _previewRect.Height = newHeight;
                            Canvas.SetTop(_previewRect, _pMouseStart.Y - newHeight);
                            Canvas.SetLeft(_previewRect, _pMouseStart.X);
                        }
                        if (_pMouseStart.X >= _pMouseEnd.X)
                        {
                            _previewRect.RenderTransformOrigin = new Point(0.5, 0.5);
                            newWidth = _pMouseStart.X - _pMouseEnd.X;
                            if (newWidth < 0)
                            {
                                newWidth *= -1;
                            }
                            st = new ScaleTransform {ScaleX = -1};
                            _previewRect.RenderTransform = st;
                            _previewRect.Width = newWidth;
                            _previewRect.Height = newHeight;
                            Canvas.SetTop(_previewRect, _pMouseStart.Y);
                            Canvas.SetLeft(_previewRect, _pMouseStart.X - newWidth);
                        }
                    }
                }
                else
                {
                    _previewRect.Width = newWidth;
                    _previewRect.Height = newHeight;
                    Canvas.SetTop(_previewRect, _pMouseStart.Y);
                    Canvas.SetLeft(_previewRect, _pMouseStart.X);
                }

                _previewRect.HorizontalAlignment = HorizontalAlignment.Left;
                _previewRect.VerticalAlignment = VerticalAlignment.Center;
                _previewRect.StrokeThickness = MapController.StrokeSize;
                _previewRect.Stroke = Brushes.Gray;
                _mainCanvas.Children.Add(_previewRect);
            }
        }

        public void DoRectDraw()
        {
            TransformRectPreview();
            _bDrawStarted = false;
        }

        private void PaintRectMouseMove(MouseEventArgs e)
        {
            if (_previewRect == null || !_bDrawStarted || e.LeftButton != MouseButtonState.Released) return;
            _pMouseEnd = e.GetPosition(_mainCanvas);
            RemoveRectPreview();
            DoRectPreviewDraw();
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            PaintRectMouseMove(e);
        }

        private void RemoveRectPreview()
        {
            if (_mainCanvas.Children.Contains(_previewRect))
            {
                _mainCanvas.Children.Remove(_previewRect);
                _previewRect = null;
            }
        }

        private void TransformRectPreview()
        {
            if (_mainCanvas.Children.Contains(_previewRect))
            {
                _mainCanvas.Children.Remove(_previewRect);
                var myRect = _previewRect;
                myRect.Stroke = Brushes.Black;
                myRect.StrokeThickness = MapController.StrokeSize;
                _mainCanvas.Children.Add(myRect);
                _previewRect = null;
            }
        }

        public void AbortDraw()
        {
            RemoveRectPreview();
            _bDrawStarted = false;
        }

    }
}
