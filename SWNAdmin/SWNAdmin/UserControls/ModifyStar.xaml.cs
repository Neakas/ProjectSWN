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
    public partial class ModifyStar : UserControl
    {
        public ModifyStar()
        {
            InitializeComponent();
        }

        private void btApply_Click(object sender, RoutedEventArgs e)
        {
            Stars updatestar;
            using (var ctx = new Db1Entities())
            {
                updatestar = ctx.Stars.Where(s => s.Id.ToString() == tbStarID.Text).FirstOrDefault();
            }
            if (updatestar != null)
            {
                if (tbStarName.Text != "")
                {
                    updatestar.name = tbStarName.Text;
                }
                updatestar.currLumin = double.Parse(tbStarLum.Text);
                updatestar.distFromPrimary = double.Parse(tbStarDistFromPrim.Text);
                updatestar.effTemp = double.Parse(tbStareffTemp.Text);
                updatestar.initLumin = double.Parse(tbStarinitLum.Text);
                updatestar.initMass = double.Parse(tbStarinitMass.Text);
                updatestar.isFlareStar = bool.Parse(tbStarisFlare.Text);
                updatestar.orbitalEccent = double.Parse(tbStarorbitalEccent.Text);
                updatestar.orbitalPeriod = double.Parse(tbStarorbitalPeriod.Text);
                updatestar.orbitalRadius = double.Parse(tbStarorbitalRadius.Text);
                updatestar.radius = double.Parse(tbStarRadius.Text);
                updatestar.specType = tbStarSpecType.Text;
                updatestar.starColor = tbStarDistFromPrim.Text;
            }
            using (var dbCtx = new Db1Entities())
            {
                //3. Mark entity as modified
                dbCtx.Entry(updatestar).State = EntityState.Modified;

                //4. call SaveChanges
                dbCtx.SaveChanges();
            }
            SystemSelector.CurrentInstance.LoadSystemsFromSql();
        }
    }
}