using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SWN.UserControls
{
    /// <summary>
    /// Interaction logic for Notification.xaml
    /// </summary>
    public partial class Notification : UserControl
    {
        Storyboard myStoryboard;
        string Objecttype = null;
        object NotificationObject = null;

        public Notification(object notificationObject)
        {
            NotificationObject = notificationObject;
            Objecttype = NotificationObject.ToString();
            InitializeComponent();
            this.MouseEnter += onMouseEnter;
            this.MouseLeave += onMouseLeave;
            this.MouseLeftButtonDown += onMouseClick;
            SolidColorBrush animatedBrush = new SolidColorBrush();
            animatedBrush.Color = Colors.Black;
            animatedBrush.Opacity = 0;
            if (this.FindName("myanimatedbrush") == null)
            {
                this.RegisterName("myanimatedbrush", animatedBrush);
            }
            tbBorder.BorderBrush = animatedBrush;
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("NotificationText", typeof(String), typeof(Notification), new FrameworkPropertyMetadata(string.Empty));

        public String NotificationText
        {
            get { return GetValue(TextProperty).ToString(); }
            set { SetValue(TextProperty, value); }
        }

        private void onMouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = 0.0;
            myDoubleAnimation.To = 1.0;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);
            Storyboard.SetTargetName(myDoubleAnimation, "myanimatedbrush");
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(SolidColorBrush.OpacityProperty));
            myStoryboard.Begin(tbBorder);
        }

        private void onMouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = 1.0;
            myDoubleAnimation.To = 0.0;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);
            Storyboard.SetTargetName(myDoubleAnimation, "myanimatedbrush");
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(SolidColorBrush.OpacityProperty));
            myStoryboard.Begin(tbBorder);
        }

        private void onMouseClick(object sender, MouseButtonEventArgs e)
        {
            if (NotificationObject.GetType().Name == "Uri")
            {
                //We got an Uri/ImageUri...Load the Image into the ImageGrid
                MainWindow.CurrentInstance.UpdateImageWindow((Uri)NotificationObject);
                MainWindow.CurrentInstance.spNotificationPanel.Children.Remove(this);
            }
        }
    }
}
