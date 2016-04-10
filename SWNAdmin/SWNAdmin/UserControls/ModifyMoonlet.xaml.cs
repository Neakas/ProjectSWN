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
    public partial class ModifyMoonlet
    {
        public ModifyMoonlet()
        {
            InitializeComponent();
        }

        public static bool IsOuter { get; set; }

        private void btApply_Click( object sender, RoutedEventArgs e )
        {
            if (TbIsOuter.Text == "true")
            {
                OuterMoonlets updatemoonlet;
                using (var ctx = new Db1Entities())
                {
                    updatemoonlet = ctx.OuterMoonlets.FirstOrDefault(s => s.Id.ToString() == TbMoonletId.Text);
                }
                if (updatemoonlet != null)
                {
                    updatemoonlet.name = TbMoonletName.Text;
                    updatemoonlet.planetRadius = double.Parse(TbMoonletRadius.Text);
                    updatemoonlet.blackbodyTemp = double.Parse(TbMoonletblackbodyTemp.Text);
                    updatemoonlet.planetRadius = double.Parse(TbMoonletRadius.Text);
                    updatemoonlet.orbitalEccent = double.Parse(TbMoonletorbitalEccent.Text);
                    updatemoonlet.orbitalPeriod = double.Parse(TbMoonletorbitalPeriod.Text);
                    updatemoonlet.orbitalRadius = double.Parse(TbMoonletorbitalRadius.Text);
                }
                using (var dbCtx = new Db1Entities())
                {
                    dbCtx.Entry(updatemoonlet).State = EntityState.Modified;
                    dbCtx.SaveChanges();
                }
                SystemSelector.CurrentInstance.LoadSystemsFromSql();
            }
            else
            {
                InnerMoonlets updatemoonlet;
                using (var ctx = new Db1Entities())
                {
                    updatemoonlet = ctx.InnerMoonlets.FirstOrDefault(s => s.Id.ToString() == TbMoonletId.Text);
                }
                if (updatemoonlet != null)
                {
                    if (TbMoonletName.Text != "")
                    {
                        updatemoonlet.name = TbMoonletName.Text;
                    }
                    updatemoonlet.planetRadius = double.Parse(TbMoonletRadius.Text);
                    updatemoonlet.blackbodyTemp = double.Parse(TbMoonletblackbodyTemp.Text);
                    updatemoonlet.planetRadius = double.Parse(TbMoonletRadius.Text);
                    updatemoonlet.orbitalEccent = double.Parse(TbMoonletorbitalEccent.Text);
                    updatemoonlet.orbitalPeriod = double.Parse(TbMoonletorbitalPeriod.Text);
                    updatemoonlet.orbitalRadius = double.Parse(TbMoonletorbitalRadius.Text);
                }
                using (var dbCtx = new Db1Entities())
                {
                    dbCtx.Entry(updatemoonlet).State = EntityState.Modified;
                    dbCtx.SaveChanges();
                }
                SystemSelector.CurrentInstance.LoadSystemsFromSql();
            }
        }
    }
}