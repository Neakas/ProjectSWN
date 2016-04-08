using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SWNAdmin.Forms;
using SWNAdmin.Utility;

namespace SWNAdmin.UserControls
{
    /// <summary>
    ///     Interaction logic for ModifyStarSystem.xaml
    /// </summary>
    public partial class ModifyStarSystem : UserControl
    {
        public ModifyStarSystem()
        {
            InitializeComponent();
        }

        public StarSystems conObject { get; set; }

        private void btApply_Click(object sender, RoutedEventArgs e)
        {
            StarSystems updatesystem;
            using (var ctx = new Db1Entities())
            {
                updatesystem = ctx.StarSystems.Where(s => s.Id.ToString() == tbSysID.Text).FirstOrDefault();
            }
            if (updatesystem != null)
            {
                updatesystem.sysName = tbSysName.Text;
                updatesystem.sysAge = double.Parse(tbSysAge.Text);
            }
            using (var dbCtx = new Db1Entities())
            {
                //3. Mark entity as modified
                dbCtx.Entry(updatesystem).State = EntityState.Modified;

                //4. call SaveChanges
                dbCtx.SaveChanges();
            }
            SystemSelector.CurrentInstance.LoadSystemsFromSql();
        }
    }
}