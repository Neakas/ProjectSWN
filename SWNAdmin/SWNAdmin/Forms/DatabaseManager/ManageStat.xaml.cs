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
    /// Interaction logic for AddStat.xaml
    /// </summary>
    public partial class ManageStat : Window
    {
        public ManageStat()
        {
            InitializeComponent();
            FillListbox();
        }

        private void FillListbox()
        {
            var context = new Utility.Db1Entities();
            var query = from c in context.Stats select c;
            var stats = query.ToList();
            lbStats.ItemsSource = stats;
            lbStats.DisplayMemberPath = "StatName";
            lbStats.SelectedValuePath = "Id";
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            var findcontext = new Utility.Db1Entities();
            var query = from c in findcontext.Stats where c.StatName == tbStat.Text select c;
            var foundstat = query.FirstOrDefault();
            if (foundstat == null)
            {
                if (tbStat.Text == "")
                {
                    MessageBox.Show("Please input Stat!");
                }
                else
                {
                    using (var context = new Utility.Db1Entities())
                    {
                        Utility.Stats newStat = new Utility.Stats();
                        newStat.StatName = tbStat.Text;
                        context.Stats.Add(newStat);
                        context.SaveChanges();
                    }
                    MessageBox.Show("'" + tbStat.Text + "' added to the Database");
                    FillListbox();
                    tbStat.Text = "";
                }
            }
            else
            {
                MessageBox.Show("That Stat allready exists in the Database!");
            }
        }

        private void btDel_Click(object sender, RoutedEventArgs e)
        {
            var context = new Utility.Db1Entities();
            var query = from c in context.Stats where c.StatName == tbStat.Text select c;
            var foundstat = query.FirstOrDefault();
            if (tbStat.Text == "")
            {
                MessageBox.Show("Please input Stat!");
            }
            else
            {
                if (foundstat.Id != 0)
                {
                    context.Stats.Remove(foundstat);
                    context.SaveChanges();
                    FillListbox();
                    MessageBox.Show("'" + tbStat.Text + "' deleted from the Database");
                    tbStat.Text = "";
                }
                else
                {
                    MessageBox.Show("No Stat with that Name found in the Database");
                }
            }
        }

        private void lbStats_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbStats.SelectedItem != null)
            {
                Utility.Stats selectedStat = new Utility.Stats();
                selectedStat = lbStats.SelectedItem as Utility.Stats;
                tbStat.Text = selectedStat.StatName;
            }
        }
    }
}
