using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SWN.UserControls
{
    /// <summary>
    ///     Interaction logic for Notification.xaml
    /// </summary>
    public partial class Notification
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("NotificationText", typeof (string), typeof (Notification), new FrameworkPropertyMetadata(string.Empty));

        private readonly object _notificationObject;

        private Storyboard _myStoryboard;

        public Notification( object notificationObject )
        {
            _notificationObject = notificationObject;
            InitializeComponent();
            MouseEnter += OnMouseEnter;
            MouseLeave += OnMouseLeave;
            MouseLeftButtonDown += OnMouseClick;
            var animatedBrush = new SolidColorBrush
            {
                Color = Colors.Black,
                Opacity = 0
            };
            if (FindName("myanimatedbrush") == null)
            {
                RegisterName("myanimatedbrush", animatedBrush);
            }
            TbBorder.BorderBrush = animatedBrush;
        }

        public string NotificationText
        {
            get
            {
                return GetValue(TextProperty).ToString();
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        private void OnMouseEnter( object sender, MouseEventArgs e )
        {
            var myDoubleAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = new Duration(TimeSpan.FromSeconds(0.5))
            };
            _myStoryboard = new Storyboard();
            _myStoryboard.Children.Add(myDoubleAnimation);
            Storyboard.SetTargetName(myDoubleAnimation, "myanimatedbrush");
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Brush.OpacityProperty));
            _myStoryboard.Begin(TbBorder);
        }

        private void OnMouseLeave( object sender, MouseEventArgs e )
        {
            var myDoubleAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(TimeSpan.FromSeconds(0.5))
            };
            _myStoryboard = new Storyboard();
            _myStoryboard.Children.Add(myDoubleAnimation);
            Storyboard.SetTargetName(myDoubleAnimation, "myanimatedbrush");
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Brush.OpacityProperty));
            _myStoryboard.Begin(TbBorder);
        }

        private void OnMouseClick( object sender, MouseButtonEventArgs e )
        {
            if (_notificationObject.GetType().Name != "Uri")
            {
                return;
            }
            //We got an Uri/ImageUri...Load the Image into the ImageGrid
            MainWindow.CurrentInstance.SetImageGridBusy();
            MainWindow.CurrentInstance.UpdateImageWindow((Uri) _notificationObject);
            RemoveNotification();
        }

        private void RemoveNotification()
        {
            MainWindow.CurrentInstance.UnregisterName(MainWindow.CurrentInstance.NotificationDict[this]);
            MainWindow.CurrentInstance.NotificationDict.Remove(this);
            MainWindow.CurrentInstance.SpNotificationPanel.Children.Remove(this);
        }
    }
}