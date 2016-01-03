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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SWNAdmin.UserControls
{
    /// <summary>
    /// Interaction logic for ModifyStarSystem.xaml
    /// </summary>
    public partial class ModifyStarSystem : UserControl
    {
        public Utility.StarSystems conObject { get; set; }
        public ModifyStarSystem()
        {
            InitializeComponent();
        }

        private void btApply_Click(object sender, RoutedEventArgs e)
        {
            Utility.StarSystems updatesystem;
            using (var ctx = new Utility.Db1Entities())
            {
                updatesystem = ctx.StarSystems.Where(s => s.Id.ToString() == tbSysID.Text).FirstOrDefault<Utility.StarSystems>();
            }
            if (updatesystem != null)
            {
                updatesystem.sysName = tbSysName.Text;
                updatesystem.sysAge = Double.Parse(tbSysAge.Text);
            }
            using (var dbCtx = new Utility.Db1Entities())
            {
                //3. Mark entity as modified
                dbCtx.Entry(updatesystem).State = System.Data.Entity.EntityState.Modified;

                //4. call SaveChanges
                dbCtx.SaveChanges();
            }
            Forms.SystemSelector.CurrentInstance.LoadSystemsFromSql();
        }
    }
}
