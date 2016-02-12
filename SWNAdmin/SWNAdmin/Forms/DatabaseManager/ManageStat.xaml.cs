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
            var query = from c in context.Attribute select c;
            var stats = query.ToList();
            lbStats.ItemsSource = stats;
            lbStats.DisplayMemberPath = "StatName";
            lbStats.SelectedValuePath = "Id";
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            var findcontext = new Utility.Db1Entities();
            var query = from c in findcontext.Attribute where c.Name == tbStat.Text select c;
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
                        Utility.Attribute newStat = new Utility.Attribute();
                        newStat.Name = tbStat.Text;
                        context.Attribute.Add(newStat);
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
            var query = from c in context.Attribute where c.Name == tbStat.Text select c;
            var foundstat = query.FirstOrDefault();
            if (tbStat.Text == "")
            {
                MessageBox.Show("Please input Stat!");
            }
            else
            {
                if (foundstat.Id != 0)
                {
                    context.Attribute.Remove(foundstat);
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
                Utility.Attribute selectedStat = new Utility.Attribute();
                selectedStat = lbStats.SelectedItem as Utility.Attribute;
                tbStat.Text = selectedStat.Name;
            }
        }
    }
}
