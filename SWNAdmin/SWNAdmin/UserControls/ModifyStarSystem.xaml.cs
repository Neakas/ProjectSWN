using System.Data.Entity;
using System.Linq;
using System.Windows;
using SWNAdmin.Forms;
using SWNAdmin.Utility;

namespace SWNAdmin.UserControls
{
    /// <summary>
    ///     Interaction logic for ModifyStarSystem.xaml
    /// </summary>
    public partial class ModifyStarSystem
    {
        public ModifyStarSystem()
        {
            InitializeComponent();
        }

        public StarSystems ConObject { get; set; }

        private void btApply_Click( object sender, RoutedEventArgs e )
        {
            StarSystems updatesystem;
            using (var ctx = new Db1Entities())
            {
                updatesystem = ctx.StarSystems.FirstOrDefault(s => s.Id.ToString() == TbSysId.Text);
            }
            if (updatesystem != null)
            {
                updatesystem.sysName = TbSysName.Text;
                updatesystem.sysAge = double.Parse(TbSysAge.Text);
            }
            using (var dbCtx = new Db1Entities())
            {
                dbCtx.Entry(updatesystem).State = EntityState.Modified;
                dbCtx.SaveChanges();
            }
            SystemSelector.CurrentInstance.LoadSystemsFromSql();
        }
    }
}