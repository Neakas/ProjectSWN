using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SWN.UserControls
{
    /// <summary>
    ///     Interaction logic for Notification.xaml
    /// </summary>
    public partial class Notification : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("NotificationText",
            typeof (string), typeof (Notification), new FrameworkPropertyMetadata(string.Empty));

        private readonly object NotificationObject;

        private Storyboard myStoryboard;
        private string Objecttype;

        public Notification(object notificationObject)
        {
            NotificationObject = notificationObject;
            Objecttype = NotificationObject.ToString();
            InitializeComponent();
            MouseEnter += onMouseEnter;
            MouseLeave += onMouseLeave;
            MouseLeftButtonDown += onMouseClick;
            var animatedBrush = new SolidColorBrush();
            animatedBrush.Color = Colors.Black;
            animatedBrush.Opacity = 0;
            if (FindName("myanimatedbrush") == null)
            {
                RegisterName("myanimatedbrush", animatedBrush);
            }
            tbBorder.BorderBrush = animatedBrush;
        }

        public string NotificationText
        {
            get { return GetValue(TextProperty).ToString(); }
            set { SetValue(TextProperty, value); }
        }

        private void onMouseEnter(object sender, MouseEventArgs e)
        {
            var myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = 0.0;
            myDoubleAnimation.To = 1.0;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);
            Storyboard.SetTargetName(myDoubleAnimation, "myanimatedbrush");
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Brush.OpacityProperty));
            myStoryboard.Begin(tbBorder);
        }

        private void onMouseLeave(object sender, MouseEventArgs e)
        {
            var myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = 1.0;
            myDoubleAnimation.To = 0.0;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);
            Storyboard.SetTargetName(myDoubleAnimation, "myanimatedbrush");
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Brush.OpacityProperty));
            myStoryboard.Begin(tbBorder);
        }

        private void onMouseClick(object sender, MouseButtonEventArgs e)
        {
            if (NotificationObject.GetType().Name == "Uri")
            {
                //We got an Uri/ImageUri...Load the Image into the ImageGrid
                MainWindow.CurrentInstance.SetImageGridBusy();
                MainWindow.CurrentInstance.UpdateImageWindow((Uri) NotificationObject);
                RemoveNotification();
            }
        }

        private void RemoveNotification()
        {
            MainWindow.CurrentInstance.UnregisterName(MainWindow.CurrentInstance.NotificationDict[this]);
            MainWindow.CurrentInstance.NotificationDict.Remove(this);
            MainWindow.CurrentInstance.spNotificationPanel.Children.Remove(this);
        }
    }
}