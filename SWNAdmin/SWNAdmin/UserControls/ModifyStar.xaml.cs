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
    public partial class ModifyStar : UserControl
    {
        public ModifyStar()
        {
            InitializeComponent();
        }

        private void btApply_Click(object sender, RoutedEventArgs e)
        {
            Utility.Stars updatestar;
            using (var ctx = new Utility.Db1Entities())
            {
                updatestar = ctx.Stars.Where(s => s.Id.ToString() == tbStarID.Text).FirstOrDefault<Utility.Stars>();
            }
            if (updatestar != null)
            {
                if (tbStarName.Text != "")
                {
                    updatestar.name = tbStarName.Text;
                }
                updatestar.currLumin = Double.Parse(tbStarLum.Text);
                updatestar.distFromPrimary = Double.Parse(tbStarDistFromPrim.Text);
                updatestar.effTemp = Double.Parse(tbStareffTemp.Text);
                updatestar.initLumin = Double.Parse(tbStarinitLum.Text);
                updatestar.initMass = Double.Parse(tbStarinitMass.Text);
                updatestar.isFlareStar = Boolean.Parse(tbStarisFlare.Text);
                updatestar.orbitalEccent = Double.Parse(tbStarorbitalEccent.Text);
                updatestar.orbitalPeriod = Double.Parse(tbStarorbitalPeriod.Text);
                updatestar.orbitalRadius = Double.Parse(tbStarorbitalRadius.Text);
                updatestar.radius = Double.Parse(tbStarRadius.Text);
                updatestar.specType = tbStarSpecType.Text;
                updatestar.starColor = tbStarDistFromPrim.Text;

            }
            using (var dbCtx = new Utility.Db1Entities())
            {
                //3. Mark entity as modified
                dbCtx.Entry(updatestar).State = System.Data.Entity.EntityState.Modified;

                //4. call SaveChanges
                dbCtx.SaveChanges();
            }
            Forms.SystemSelector.CurrentInstance.LoadSystemsFromSql();
        }
    }
}
