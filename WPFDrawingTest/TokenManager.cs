using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFDrawingTest
{
    internal class TokenManager
    {
        private TranslateTransform _tt;
        public Point DragOffset;
        private bool _bIsDragging;

        public Token CreateToken(double top = 50, double left = 50 , Token.TokenSize size = Token.TokenSize.Normal)
        {
            var newToken = new Token
            {
                Name = "New_Token",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = Token.SizeToIntDict[size],
                Width = Token.SizeToIntDict[size],
                Margin = new Thickness(top, left, 0, 0),
                Stroke = Brushes.DarkGray,
                StrokeThickness = 1,
                Fill = Brushes.Black
            };
            newToken.MouseLeftButtonDown += Token_MouseLeftButtonDown;
            newToken.MouseLeftButtonUp += Token_MouseLeftButtonUp;
            newToken.MouseMove += Token_MouseMove;
            _tt = new TranslateTransform();
            newToken.RenderTransform = _tt;
            return newToken;
        }

        public void Token_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _bIsDragging = true;
            DragOffset = e.GetPosition((UIElement)sender);
            var dragToken = (Token)e.Source;
            dragToken.CaptureMouse();
        }

        public void Token_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _bIsDragging = false;
            var dragToken = (Token)e.Source;
            dragToken.ReleaseMouseCapture();
            SnapToGrid(dragToken);
        }

        public void Token_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_bIsDragging) return;
            var mousePos = e.GetPosition((UIElement)sender);
            var dragToken = (Token)e.Source;
            var localTranslateTransform = (TranslateTransform)dragToken.RenderTransform;
            var newX = localTranslateTransform.X + (mousePos.X - DragOffset.X);
            var newY = localTranslateTransform.Y + (mousePos.Y - DragOffset.Y);
            localTranslateTransform.X = newX;
            localTranslateTransform.Y = newY;
        }

        private static void SnapToGrid(UIElement token)
        {
            var localTranslateTransform = (TranslateTransform)token.RenderTransform;
            var newx = Math.Round(localTranslateTransform.X / 50, MidpointRounding.ToEven) * 50;
            var newy = Math.Round(localTranslateTransform.Y / 50, MidpointRounding.ToEven) * 50;
            localTranslateTransform.X = newx;
            localTranslateTransform.Y = newy;
        }
    }
}
