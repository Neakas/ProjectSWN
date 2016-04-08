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
    public partial class ModifyPlanet : UserControl
    {
        public ModifyPlanet()
        {
            InitializeComponent();
        }

        private void btApply_Click(object sender, RoutedEventArgs e)
        {
            Planets updateplanet;
            using (var ctx = new Db1Entities())
            {
                updateplanet = ctx.Planets.Where(s => s.Id.ToString() == tbPlanetID.Text).FirstOrDefault();
            }
            if (updateplanet != null)
            {
                updateplanet.name = tbPlanetName.Text;
                updateplanet.atmCate = int.Parse(tbPlanetatmCate.Text);
                updateplanet.atmMass = double.Parse(tbPlanetatmMass.Text);
                updateplanet.atmPres = tbPlanetatmPres.Text;
                updateplanet.atmnote = tbPlanetatmNote.Text;
                updateplanet.axialTilt = double.Parse(tbPlanetaxialTilt.Text);
                updateplanet.mass = double.Parse(tbPlanetPlanetMass.Text);
                updateplanet.RVM = tbPlanetRVM.Text;
                updateplanet.orbitalPeriod = double.Parse(tbPlanetorbitalPeriod.Text);
                updateplanet.orbitalRadius = double.Parse(tbPlanetorbitalRadius.Text);
                updateplanet.orbitalEccent = double.Parse(tbPlanetorbitalEccent.Text);
                updateplanet.moonRadius = double.Parse(tbPlanetRadius.Text);
                updateplanet.baseType = int.Parse(tbPlanetType.Text);
                updateplanet.SatelliteSize = int.Parse(tbPlanetSite.Text);
                updateplanet.blackbodyTemp = double.Parse(tbPlanetbbt.Text);
                updateplanet.dayFaceMod = int.Parse(tbPlanetdayFaceMod.Text);
                updateplanet.density = double.Parse(tbPlanetDensity.Text);
                updateplanet.diameter = double.Parse(tbPlanetDiameter.Text);
                updateplanet.gravity = double.Parse(tbPlanetGravity.Text);
                updateplanet.hydCoverage = tbPlanethydCov.Text;
                updateplanet.isResonant = bool.Parse(tbPlanetisRes.Text);
                updateplanet.isTideLocked = bool.Parse(tbPlanetisTideLock.Text);
                updateplanet.retrogradeMotion = bool.Parse(tbPlanetretrogradeMotion.Text);
                updateplanet.rotationalPeriod = double.Parse(tbPlanetrotPeriod.Text);
                updateplanet.SatelliteSize = int.Parse(tbPlanetSite.Text);
                updateplanet.siderealPeriod = double.Parse(tbPlanetsiderealPeriod.Text);
                updateplanet.surfaceTemp = double.Parse(tbPlanetsurfaceTemp.Text);
                updateplanet.tecActivity = double.Parse(tbPlanettecActivity.Text);
                updateplanet.tideForce = double.Parse(tbPlanettideForce.Text);
                updateplanet.volActivity = double.Parse(tbPlanetvolActivity.Text);
                updateplanet.orbitalCycle = double.Parse(tbPlanetOrbitalCycle.Text);
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