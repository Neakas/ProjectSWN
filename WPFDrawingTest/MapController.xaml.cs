using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFDrawingTest
{
    /// <summary>
    /// Interaction logic for MapController.xaml
    /// </summary>
    public partial class MapController
    {
        public enum PaintMode { None, Line, Rect, Delete, Brush }
        public static int StrokeSize = 5;
        private readonly LineManager _linemanager;
        private readonly RectManager _rectmanager;
        private readonly DeleteManager _deletemanager;
        private readonly BrushManager _brushmanager;
        private readonly Dictionary<string, PaintMode> _paintModeDict = new Dictionary<string, PaintMode>()
            {
                { "None",PaintMode.None },
                { "Line",PaintMode.Line },
                { "Rect",PaintMode.Rect },
                { "Brush",PaintMode.Brush },
                { "Delete",PaintMode.Delete }
            };

        private PaintMode _currentPaintMode;

        public MapController()
        {
            InitializeComponent();
            new GridManager(MainGrid);
            _linemanager = new LineManager(MainCanvas);
            _rectmanager = new RectManager(MainCanvas);
            _brushmanager = new BrushManager(MainCanvas);
            _deletemanager = new DeleteManager(MainCanvas);
            var tokenmanager = new TokenManager();
            MainGrid.Children.Add(tokenmanager.CreateToken());
        }

        private void cbPaintModes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbPaintModes.SelectedItem != null)
            {
                _currentPaintMode = _paintModeDict[((ComboBoxItem)CbPaintModes.SelectedItem).Content.ToString()];
            }
        }

        private void MainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_currentPaintMode == PaintMode.Line)
            {
                _linemanager.MouseDownEvent(e);
            }
            if (_currentPaintMode == PaintMode.Rect)
            {
                _rectmanager.MouseDownEvent(e);
            }
            if (_currentPaintMode == PaintMode.Delete)
            {
                _deletemanager.MouseDownEvent(e);
            }
            if (_currentPaintMode == PaintMode.Brush)
            {
                _brushmanager.MouseDownEvent(e);
            }
            if (_currentPaintMode == PaintMode.None)
            {
                //if (((Canvas)sender).IsMouseCaptured) return;
                //((Canvas)sender).CaptureMouse();

                //start = e.GetPosition(MainGrid);
                //origin.X = MainCanvas.RenderTransform.Value.OffsetX;
                //origin.Y = MainCanvas.RenderTransform.Value.OffsetY;
            }
        }

        public void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (_currentPaintMode == PaintMode.Line)
            {
                if (e.Key == Key.LeftShift)
                {
                    _linemanager.SetShiftMode(true);
                }
                if (e.Key == Key.Escape)
                {
                    _linemanager.AbortDraw();
                }
                FocusManager.SetFocusedElement(this, this);
            }
            if (_currentPaintMode == PaintMode.Rect)
            {
                if (e.Key == Key.Escape)
                {
                    _rectmanager.AbortDraw();
                }
            }
            if (_currentPaintMode != PaintMode.Delete) return;
            if (e.Key == Key.Escape)
            {
                _deletemanager.AbortDraw();
            }
        }

        public void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (_currentPaintMode != PaintMode.Line) return;
            if (e.Key == Key.LeftShift)
            {
                _linemanager.SetShiftMode(false);
            }
        }

        private void iudStrokeSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            StrokeSize = (int)e.NewValue;
        }

        private void MainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //((Canvas)sender).ReleaseMouseCapture();
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            //if (!((Canvas)sender).IsMouseCaptured) return;
            //Point p = e.MouseDevice.GetPosition(MainGrid);

            //Matrix m = MainCanvas.RenderTransform.Value;
            //m.OffsetX = _origin.X + (p.X - _start.X);
            //m.OffsetY = _origin.Y + (p.Y - _start.Y);

            //MainCanvas.RenderTransform = new MatrixTransform(m);
        }
    }
}
