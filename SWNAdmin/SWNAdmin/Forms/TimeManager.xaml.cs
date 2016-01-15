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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SWNAdmin.Forms
{
    /// <summary>
    /// Interaction logic for TimeManager.xaml
    /// </summary>
    public partial class TimeManager : Window
    {
        public TimeManager()
        {
            InitializeComponent();
            UpdateDateTimeDisplay();
        }

        public void UpdateDateTimeDisplay()
        {
            DateTime DT = TimeHandler.GetCurrentDateTime();
            MainCalendar.SelectedDate = DT;
            tbClock.Text = DT.TimeOfDay.ToString();
            tbDate.Text = DT.ToShortDateString();
        }

        private void _1MButton_Click(object sender, RoutedEventArgs e)
        {
            TimeHandler.IncrementMinute(1);
            UpdateDateTimeDisplay();
        }

        private void _5MButton_Click(object sender, RoutedEventArgs e)
        {
            TimeHandler.IncrementMinute(5);
            UpdateDateTimeDisplay();
        }

        private void TenMButton_Click(object sender, RoutedEventArgs e)
        {
            TimeHandler.IncrementMinute(10);
            UpdateDateTimeDisplay();
        }

        private void ThirtyMButton_Click(object sender, RoutedEventArgs e)
        {
            TimeHandler.IncrementMinute(30);
            UpdateDateTimeDisplay();
        }

        private void OneHButton_Click(object sender, RoutedEventArgs e)
        {
            TimeHandler.IncrementHour(1);
            UpdateDateTimeDisplay();
        }

        private void SixHButton_Click(object sender, RoutedEventArgs e)
        {
            TimeHandler.IncrementHour(6);
            UpdateDateTimeDisplay();
        }

        private void TwelveHButton_Click(object sender, RoutedEventArgs e)
        {
            TimeHandler.IncrementHour(12);
            UpdateDateTimeDisplay();
        }

        private void OneDButton_Click(object sender, RoutedEventArgs e)
        {
            TimeHandler.IncrementDay(1);
            UpdateDateTimeDisplay();
        }

        private void sevenDButton_click(object sender, RoutedEventArgs e)
        {
            TimeHandler.IncrementDay(7);
            UpdateDateTimeDisplay();
        }

        private void thirthyDButton_Click(object sender, RoutedEventArgs e)
        {
            TimeHandler.IncrementDay(30);
            UpdateDateTimeDisplay();
        }

        private void tbDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ModifyDate();
            }
        }

        private void tbClock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ModifyDate();
            }
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            tbDate.IsEnabled = true;
            tbClock.IsEnabled = true;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            tbDate.IsEnabled = false;
            tbClock.IsEnabled = false;
        }

        private void btUndo_Click(object sender, RoutedEventArgs e)
        {
            //TODOLOW DT Undo reimplement
            //TimeHandler.SetCurrentDateTime(UndoHandler.getUndo(), true);
            //UpdateDateTimeDisplay();
        }

        public void ModifyDate()
        {
            DateTime DT = TimeHandler.GetCurrentDateTime();
            DateTime.TryParse(String.Concat(tbDate.Text, " ", tbClock.Text), out DT);
            TimeHandler.SetCurrentDateTime(DT);
            UpdateDateTimeDisplay();
        }

    }
}
