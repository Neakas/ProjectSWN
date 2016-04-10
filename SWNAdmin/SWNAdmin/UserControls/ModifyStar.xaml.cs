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

        private void btApply_Click( object sender, RoutedEventArgs e )
        {
            Stars updatestar;
            using (var ctx = new Db1Entities())
            {
                updatestar = ctx.Stars.Where(s => s.Id.ToString() == TbStarId.Text).FirstOrDefault();
            }
            if (updatestar != null)
            {
                if (TbStarName.Text != "")
                {
                    updatestar.name = TbStarName.Text;
                }
                updatestar.currLumin = double.Parse(TbStarLum.Text);
                updatestar.distFromPrimary = double.Parse(TbStarDistFromPrim.Text);
                updatestar.effTemp = double.Parse(TbStareffTemp.Text);
                updatestar.initLumin = double.Parse(TbStarinitLum.Text);
                updatestar.initMass = double.Parse(TbStarinitMass.Text);
                updatestar.isFlareStar = bool.Parse(TbStarisFlare.Text);
                updatestar.orbitalEccent = double.Parse(TbStarorbitalEccent.Text);
                updatestar.orbitalPeriod = double.Parse(TbStarorbitalPeriod.Text);
                updatestar.orbitalRadius = double.Parse(TbStarorbitalRadius.Text);
                updatestar.radius = double.Parse(TbStarRadius.Text);
                updatestar.specType = TbStarSpecType.Text;
                updatestar.starColor = TbStarDistFromPrim.Text;
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