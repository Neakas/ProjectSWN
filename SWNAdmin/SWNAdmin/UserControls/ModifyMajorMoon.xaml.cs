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
    public partial class ModifyMajorMoon : UserControl
    {
        public ModifyMajorMoon()
        {
            InitializeComponent();
        }

        private void btApply_Click(object sender, RoutedEventArgs e)
        {
            MajorMoons updateplanet;
            using (var ctx = new Db1Entities())
            {
                updateplanet = ctx.MajorMoons.Where(s => s.Id.ToString() == tbMoonID.Text).FirstOrDefault();
            }
            if (updateplanet != null)
            {
                updateplanet.name = tbMoonName.Text;
                updateplanet.atmCate = int.Parse(tbMoonatmCate.Text);
                updateplanet.atmMass = double.Parse(tbMoonatmMass.Text);
                updateplanet.axialTilt = double.Parse(tbMoonaxialTilt.Text);
                updateplanet.mass = double.Parse(tbMoonMoonMass.Text);
                updateplanet.RVM = double.Parse(tbMoonRVM.Text);
                updateplanet.orbitalPeriod = double.Parse(tbMoonorbitalPeriod.Text);
                updateplanet.orbitalRadius = double.Parse(tbMoonorbitalRadius.Text);
                updateplanet.orbitalEccent = double.Parse(tbMoonorbitalEccent.Text);
                updateplanet.moonRadius = double.Parse(tbMoonRadius.Text);
                updateplanet.baseType = int.Parse(tbMoonType.Text);
                updateplanet.SatelliteSize = int.Parse(tbMoonSite.Text);
                updateplanet.blackbodyTemp = double.Parse(tbMoonbbt.Text);
                updateplanet.dayFaceMod = int.Parse(tbMoondayFaceMod.Text);
                updateplanet.density = double.Parse(tbMoonDensity.Text);
                updateplanet.diameter = double.Parse(tbMoonDiameter.Text);
                updateplanet.gravity = double.Parse(tbMoonGravity.Text);
                updateplanet.hydCoverage = tbMoonhydCov.Text;
                updateplanet.isResonant = bool.Parse(tbMoonisRes.Text);
                updateplanet.isTideLocked = bool.Parse(tbMoonisTideLock.Text);
                updateplanet.retrogradeMotion = bool.Parse(tbMoonretrogradeMotion.Text);
                updateplanet.rotationalPeriod = double.Parse(tbMoonrotPeriod.Text);
                updateplanet.SatelliteSize = int.Parse(tbMoonSite.Text);
                updateplanet.siderealPeriod = double.Parse(tbMoonsiderealPeriod.Text);
                updateplanet.surfaceTemp = double.Parse(tbMoonsurfaceTemp.Text);
                updateplanet.tecActivity = double.Parse(tbMoontecActivity.Text);
                updateplanet.tideForce = int.Parse(tbMoontideForce.Text);
                updateplanet.tideTotal = int.Parse(tbMoontideTotal.Text);
                updateplanet.volActivity = double.Parse(tbMoonvolActivity.Text);
                updateplanet.orbitalCycle = double.Parse(tbMoonOrbitalCycle.Text);
            }
            using (var dbCtx = new Db1Entities())
            {
                //3. Mark entity as modified
                dbCtx.Entry(updateplanet).State = EntityState.Modified;

                //4. call SaveChanges
                dbCtx.SaveChanges();
            }
            SystemSelector.CurrentInstance.LoadSystemsFromSql();
        }
    }
}