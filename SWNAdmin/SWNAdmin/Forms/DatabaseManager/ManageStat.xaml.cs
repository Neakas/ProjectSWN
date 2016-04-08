using System.Windows;
using System.Windows.Input;
using SWNAdmin.Utility;
using System.Linq;

namespace SWNAdmin.Forms.DatabaseManager
{
    /// <summary>
    ///     Interaction logic for AddStat.xaml
    /// </summary>
    public partial class ManageStat
    {
        public ManageStat()
        {
            InitializeComponent();
            FillListbox();
        }

        private void FillListbox()
        {
            var context = new Db1Entities();
            var query = from c in context.Attribute select c;
            var stats = query.ToList();
            LbStats.ItemsSource = stats;
            LbStats.DisplayMemberPath = "StatName";
            LbStats.SelectedValuePath = "Id";
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            var findcontext = new Db1Entities();
            var query = from c in findcontext.Attribute where c.Name == TbStat.Text select c;
            var foundstat = query.FirstOrDefault();
            if (foundstat == null)
            {
                if (TbStat.Text == "")
                {
                    MessageBox.Show("Please input Stat!");
                }
                else
                {
                    using (var context = new Db1Entities())
                    {
                        var newStat = new Attribute {Name = TbStat.Text};
                        context.Attribute.Add(newStat);
                        context.SaveChanges();
                    }
                    MessageBox.Show("'" + TbStat.Text + "' added to the Database");
                    FillListbox();
                    TbStat.Text = "";
                }
            }
            else
            {
                MessageBox.Show("That Stat allready exists in the Database!");
            }
        }

        private void btDel_Click(object sender, RoutedEventArgs e)
        {
            var context = new Db1Entities();
            var query = from c in context.Attribute where c.Name == TbStat.Text select c;
            var foundstat = query.FirstOrDefault();
            if (TbStat.Text == "")
            {
                MessageBox.Show("Please input Stat!");
            }
            else
            {
                if (foundstat != null && foundstat.Id != 0)
                {
                    context.Attribute.Remove(foundstat);
                    context.SaveChanges();
                    FillListbox();
                    MessageBox.Show("'" + TbStat.Text + "' deleted from the Database");
                    TbStat.Text = "";
                }
                else
                {
                    MessageBox.Show("No Stat with that Name found in the Database");
                }
            }
        }

        private void lbStats_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedStat = LbStats.SelectedItem as Attribute;
            if (selectedStat != null) TbStat.Text = selectedStat.Name;
        }
    }
}