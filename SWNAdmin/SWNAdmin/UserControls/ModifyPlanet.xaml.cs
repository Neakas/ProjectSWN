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
    public partial class ModifyPlanet
    {
        public ModifyPlanet()
        {
            InitializeComponent();
        }

        private void btApply_Click( object sender, RoutedEventArgs e )
        {
            Planets updateplanet;
            using (var ctx = new Db1Entities())
            {
                updateplanet = ctx.Planets.FirstOrDefault(s => s.Id.ToString() == TbPlanetId.Text);
            }
            if (updateplanet != null)
            {
                updateplanet.name = TbPlanetName.Text;
                updateplanet.atmCate = int.Parse(TbPlanetatmCate.Text);
                updateplanet.atmMass = double.Parse(TbPlanetatmMass.Text);
                updateplanet.atmPres = TbPlanetatmPres.Text;
                updateplanet.atmnote = TbPlanetatmNote.Text;
                updateplanet.axialTilt = double.Parse(TbPlanetaxialTilt.Text);
                updateplanet.mass = double.Parse(TbPlanetPlanetMass.Text);
                updateplanet.RVM = TbPlanetRvm.Text;
                updateplanet.orbitalPeriod = double.Parse(TbPlanetorbitalPeriod.Text);
                updateplanet.orbitalRadius = double.Parse(TbPlanetorbitalRadius.Text);
                updateplanet.orbitalEccent = double.Parse(TbPlanetorbitalEccent.Text);
                updateplanet.moonRadius = double.Parse(TbPlanetRadius.Text);
                updateplanet.baseType = int.Parse(TbPlanetType.Text);
                updateplanet.SatelliteSize = int.Parse(TbPlanetSite.Text);
                updateplanet.blackbodyTemp = double.Parse(TbPlanetbbt.Text);
                updateplanet.dayFaceMod = int.Parse(TbPlanetdayFaceMod.Text);
                updateplanet.density = double.Parse(TbPlanetDensity.Text);
                updateplanet.diameter = double.Parse(TbPlanetDiameter.Text);
                updateplanet.gravity = double.Parse(TbPlanetGravity.Text);
                updateplanet.hydCoverage = TbPlanethydCov.Text;
                updateplanet.isResonant = bool.Parse(TbPlanetisRes.Text);
                updateplanet.isTideLocked = bool.Parse(TbPlanetisTideLock.Text);
                updateplanet.retrogradeMotion = bool.Parse(TbPlanetretrogradeMotion.Text);
                updateplanet.rotationalPeriod = double.Parse(TbPlanetrotPeriod.Text);
                updateplanet.SatelliteSize = int.Parse(TbPlanetSite.Text);
                updateplanet.siderealPeriod = double.Parse(TbPlanetsiderealPeriod.Text);
                updateplanet.surfaceTemp = double.Parse(TbPlanetsurfaceTemp.Text);
                updateplanet.tecActivity = double.Parse(TbPlanettecActivity.Text);
                updateplanet.tideForce = double.Parse(TbPlanettideForce.Text);
                updateplanet.volActivity = double.Parse(TbPlanetvolActivity.Text);
                updateplanet.orbitalCycle = double.Parse(TbPlanetOrbitalCycle.Text);
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