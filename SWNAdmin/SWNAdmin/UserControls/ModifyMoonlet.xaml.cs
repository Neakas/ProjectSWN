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
    public partial class ModifyMoonlet : UserControl
    {
        public ModifyMoonlet()
        {
            InitializeComponent();
        }

        public static bool IsOuter { get; set; }

        private void btApply_Click(object sender, RoutedEventArgs e)
        {
            if (tbIsOuter.Text == "true")
            {
                OuterMoonlets updatemoonlet;
                using (var ctx = new Db1Entities())
                {
                    updatemoonlet = ctx.OuterMoonlets.Where(s => s.Id.ToString() == tbMoonletID.Text).FirstOrDefault();
                }
                if (updatemoonlet != null)
                {
                    updatemoonlet.name = tbMoonletName.Text;
                    updatemoonlet.planetRadius = double.Parse(tbMoonletRadius.Text);
                    updatemoonlet.blackbodyTemp = double.Parse(tbMoonletblackbodyTemp.Text);
                    updatemoonlet.planetRadius = double.Parse(tbMoonletRadius.Text);
                    updatemoonlet.orbitalEccent = double.Parse(tbMoonletorbitalEccent.Text);
                    updatemoonlet.orbitalPeriod = double.Parse(tbMoonletorbitalPeriod.Text);
                    updatemoonlet.orbitalRadius = double.Parse(tbMoonletorbitalRadius.Text);
                }
                using (var dbCtx = new Db1Entities())
                {
                    //3. Mark entity as modified
                    dbCtx.Entry(updatemoonlet).State = EntityState.Modified;

                    //4. call SaveChanges
                    dbCtx.SaveChanges();
                }
                SystemSelector.CurrentInstance.LoadSystemsFromSql();
            }
            else
            {
                InnerMoonlets updatemoonlet;
                using (var ctx = new Db1Entities())
                {
                    updatemoonlet = ctx.InnerMoonlets.Where(s => s.Id.ToString() == tbMoonletID.Text).FirstOrDefault();
                }
                if (updatemoonlet != null)
                {
                    if (tbMoonletName.Text != "")
                    {
                        updatemoonlet.name = tbMoonletName.Text;
                    }
                    updatemoonlet.planetRadius = double.Parse(tbMoonletRadius.Text);
                    updatemoonlet.blackbodyTemp = double.Parse(tbMoonletblackbodyTemp.Text);
                    updatemoonlet.planetRadius = double.Parse(tbMoonletRadius.Text);
                    updatemoonlet.orbitalEccent = double.Parse(tbMoonletorbitalEccent.Text);
                    updatemoonlet.orbitalPeriod = double.Parse(tbMoonletorbitalPeriod.Text);
                    updatemoonlet.orbitalRadius = double.Parse(tbMoonletorbitalRadius.Text);
                }
                using (var dbCtx = new Db1Entities())
                {
                    //3. Mark entity as modified
                    dbCtx.Entry(updatemoonlet).State = EntityState.Modified;

                    //4. call SaveChanges
                    dbCtx.SaveChanges();
                }
                SystemSelector.CurrentInstance.LoadSystemsFromSql();
            }
        }
    }
}