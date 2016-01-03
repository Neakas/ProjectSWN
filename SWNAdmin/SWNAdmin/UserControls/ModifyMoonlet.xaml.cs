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
    public partial class ModifyMoonlet : UserControl
    {
        public static bool IsOuter { get; set; }

        public ModifyMoonlet()
        {
            InitializeComponent();
        }

        private void btApply_Click(object sender, RoutedEventArgs e)
        {

            if (tbIsOuter.Text == "true")
            {
                Utility.OuterMoonlets updatemoonlet;
                using (var ctx = new Utility.Db1Entities())
                {
                    updatemoonlet = ctx.OuterMoonlets.Where(s => s.Id.ToString() == tbMoonletID.Text).FirstOrDefault<Utility.OuterMoonlets>();
                }
                if (updatemoonlet != null)
                {

                        updatemoonlet.name = tbMoonletName.Text;
                    updatemoonlet.planetRadius = Double.Parse(tbMoonletRadius.Text);
                    updatemoonlet.blackbodyTemp = Double.Parse(tbMoonletblackbodyTemp.Text);
                    updatemoonlet.planetRadius = Double.Parse(tbMoonletRadius.Text);
                    updatemoonlet.orbitalEccent = Double.Parse(tbMoonletorbitalEccent.Text);
                    updatemoonlet.orbitalPeriod = Double.Parse(tbMoonletorbitalPeriod.Text);
                    updatemoonlet.orbitalRadius = Double.Parse(tbMoonletorbitalRadius.Text);
                }
                using (var dbCtx = new Utility.Db1Entities())
                {
                    //3. Mark entity as modified
                    dbCtx.Entry(updatemoonlet).State = System.Data.Entity.EntityState.Modified;

                    //4. call SaveChanges
                    dbCtx.SaveChanges();
                }
                Forms.SystemSelector.CurrentInstance.LoadSystemsFromSql();
            }
            else
            {
                Utility.InnerMoonlets updatemoonlet;
                using (var ctx = new Utility.Db1Entities())
                {
                    updatemoonlet = ctx.InnerMoonlets.Where(s => s.Id.ToString() == tbMoonletID.Text).FirstOrDefault<Utility.InnerMoonlets>();
                }
                if (updatemoonlet != null)
                {
                    if (tbMoonletName.Text != "")
                    {
                        updatemoonlet.name = tbMoonletName.Text;
                    }
                    updatemoonlet.planetRadius = Double.Parse(tbMoonletRadius.Text);
                    updatemoonlet.blackbodyTemp = Double.Parse(tbMoonletblackbodyTemp.Text);
                    updatemoonlet.planetRadius = Double.Parse(tbMoonletRadius.Text);
                    updatemoonlet.orbitalEccent = Double.Parse(tbMoonletorbitalEccent.Text);
                    updatemoonlet.orbitalPeriod = Double.Parse(tbMoonletorbitalPeriod.Text);
                    updatemoonlet.orbitalRadius = Double.Parse(tbMoonletorbitalRadius.Text);
                }
                using (var dbCtx = new Utility.Db1Entities())
                {
                    //3. Mark entity as modified
                    dbCtx.Entry(updatemoonlet).State = System.Data.Entity.EntityState.Modified;

                    //4. call SaveChanges
                    dbCtx.SaveChanges();
                }
                Forms.SystemSelector.CurrentInstance.LoadSystemsFromSql();
            }
        }
    }
}
