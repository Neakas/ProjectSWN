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
    public partial class ModifyMajorMoon
    {
        public ModifyMajorMoon()
        {
            InitializeComponent();
        }

        private void btApply_Click( object sender, RoutedEventArgs e )
        {
            MajorMoons updateplanet;
            using (var ctx = new Db1Entities())
            {
                updateplanet = ctx.MajorMoons.FirstOrDefault(s => s.Id.ToString() == TbMoonId.Text);
            }
            if (updateplanet != null)
            {
                updateplanet.name = TbMoonName.Text;
                updateplanet.atmCate = int.Parse(TbMoonatmCate.Text);
                updateplanet.atmMass = double.Parse(TbMoonatmMass.Text);
                updateplanet.axialTilt = double.Parse(TbMoonaxialTilt.Text);
                updateplanet.mass = double.Parse(TbMoonMoonMass.Text);
                updateplanet.RVM = double.Parse(TbMoonRvm.Text);
                updateplanet.orbitalPeriod = double.Parse(TbMoonorbitalPeriod.Text);
                updateplanet.orbitalRadius = double.Parse(TbMoonorbitalRadius.Text);
                updateplanet.orbitalEccent = double.Parse(TbMoonorbitalEccent.Text);
                updateplanet.moonRadius = double.Parse(TbMoonRadius.Text);
                updateplanet.baseType = int.Parse(TbMoonType.Text);
                updateplanet.SatelliteSize = int.Parse(TbMoonSite.Text);
                updateplanet.blackbodyTemp = double.Parse(TbMoonbbt.Text);
                updateplanet.dayFaceMod = int.Parse(TbMoondayFaceMod.Text);
                updateplanet.density = double.Parse(TbMoonDensity.Text);
                updateplanet.diameter = double.Parse(TbMoonDiameter.Text);
                updateplanet.gravity = double.Parse(TbMoonGravity.Text);
                updateplanet.hydCoverage = TbMoonhydCov.Text;
                updateplanet.isResonant = bool.Parse(TbMoonisRes.Text);
                updateplanet.isTideLocked = bool.Parse(TbMoonisTideLock.Text);
                updateplanet.retrogradeMotion = bool.Parse(TbMoonretrogradeMotion.Text);
                updateplanet.rotationalPeriod = double.Parse(TbMoonrotPeriod.Text);
                updateplanet.SatelliteSize = int.Parse(TbMoonSite.Text);
                updateplanet.siderealPeriod = double.Parse(TbMoonsiderealPeriod.Text);
                updateplanet.surfaceTemp = double.Parse(TbMoonsurfaceTemp.Text);
                updateplanet.tecActivity = double.Parse(TbMoontecActivity.Text);
                updateplanet.tideForce = int.Parse(TbMoontideForce.Text);
                updateplanet.tideTotal = int.Parse(TbMoontideTotal.Text);
                updateplanet.volActivity = double.Parse(TbMoonvolActivity.Text);
                updateplanet.orbitalCycle = double.Parse(TbMoonOrbitalCycle.Text);
            }
            using (var dbCtx = new Db1Entities())
            {
                dbCtx.Entry(updateplanet).State = EntityState.Modified;
                dbCtx.SaveChanges();
            }
            SystemSelector.CurrentInstance.LoadSystemsFromSql();
        }
    }
}