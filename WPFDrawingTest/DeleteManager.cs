using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFDrawingTest
{
    internal class DeleteManager
    {
        private readonly Canvas _mainCanvas;
        private bool _bDrawStarted;
        private Point _pMouseStart;
        private Point _pMouseEnd;
        private Rectangle _previewRect;

        public DeleteManager(Canvas linkedCanvas)
        {
            _mainCanvas = linkedCanvas;
        }

        public void MouseDownEvent(MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
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
                    DoDeleteDraw();
                    _mainCanvas.MouseMove -= MainCanvas_MouseMove;
                }
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
                _previewRect.StrokeThickness = 2;
                _previewRect.Stroke = Brushes.Gray;
                _mainCanvas.Children.Add(_previewRect);
            }
        }

        public void DoDeleteDraw()
        {
            DeleteControls();
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
            if (!_mainCanvas.Children.Contains(_previewRect)) return;
            _mainCanvas.Children.Remove(_previewRect);
            _previewRect = null;
        }

        private void DeleteControls()
        {
            if (_mainCanvas.Children.Contains(_previewRect))
            {
                _mainCanvas.Children.Remove(_previewRect);
                
            }
            CheckLinesForDeletion();
            CheckRectsForDeletion();
            _previewRect = null;
        }

        private void CheckLinesForDeletion()
        {
            var delrec = new Rect(Canvas.GetLeft(_previewRect), Canvas.GetTop(_previewRect), _previewRect.Width, _previewRect.Height);
            var toRemove = (from Shape item in _mainCanvas.Children
                            let itemrec = new Rect(Canvas.GetLeft(item), Canvas.GetTop(item), item.Width, item.Height) where item is Line
                            let point1 = new Point(((Line) item).X1, ((Line) item).Y1) let point2 = new Point(((Line) item).X2, ((Line) item).Y2)
                            where LineIntersectsRect(point1, point2, delrec) select (Line) item).ToList();
            foreach (var item in toRemove)
            {
                _mainCanvas.Children.Remove(item);
            }


        }


        private void CheckRectsForDeletion()
        {
            var delrec = new Rect(Canvas.GetLeft(_previewRect), Canvas.GetTop(_previewRect), _previewRect.Width, _previewRect.Height);
            var toRemove = (from Shape item in _mainCanvas.Children
                            let itemrec = new Rect(Canvas.GetLeft(item), Canvas.GetTop(item), item.Width, item.Height)
                            where item is Rectangle where delrec.IntersectsWith(itemrec) select item).Cast<Rectangle>().ToList();
            foreach (var item in toRemove)
            {
                _mainCanvas.Children.Remove(item);
            }
        }

        public static bool LineIntersectsRect(Point p1, Point p2, Rect r)
        {
            return LineIntersectsLine(p1, p2, new Point(r.X, r.Y), new Point(r.X + r.Width, r.Y)) ||
                   LineIntersectsLine(p1, p2, new Point(r.X + r.Width, r.Y), new Point(r.X + r.Width, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Point(r.X + r.Width, r.Y + r.Height), new Point(r.X, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Point(r.X, r.Y + r.Height), new Point(r.X, r.Y)) ||
                   (r.Contains(p1) && r.Contains(p2));
        }

        private static bool LineIntersectsLine(Point l1P1, Point l1P2, Point l2P1, Point l2P2)
        {
            var q = (l1P1.Y - l2P1.Y) * (l2P2.X - l2P1.X) - (l1P1.X - l2P1.X) * (l2P2.Y - l2P1.Y);
            var d = (l1P2.X - l1P1.X) * (l2P2.Y - l2P1.Y) - (l1P2.Y - l1P1.Y) * (l2P2.X - l2P1.X);

            if ((int)d == 0)
            {
                return false;
            }

            var r = q / d;

            q = (l1P1.Y - l2P1.Y) * (l1P2.X - l1P1.X) - (l1P1.X - l2P1.X) * (l1P2.Y - l1P1.Y);
            var s = q / d;

            return !(r < 0) && !(r > 1) && !(s < 0) && !(s > 1);
        }

        public void AbortDraw()
        {
            RemoveRectPreview();
            _bDrawStarted = false;
        }

    }
}
