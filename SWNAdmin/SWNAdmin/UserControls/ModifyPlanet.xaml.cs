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
    public partial class ModifyPlanet : UserControl
    {
        public ModifyPlanet()
        {
            InitializeComponent();
        }

        private void btApply_Click(object sender, RoutedEventArgs e)
        {
            Utility.Planets updateplanet;
            using (var ctx = new Utility.Db1Entities())
            {
                updateplanet = ctx.Planets.Where(s => s.Id.ToString() == tbPlanetID.Text).FirstOrDefault<Utility.Planets>();
            }
            if (updateplanet != null)
            {
                updateplanet.name = tbPlanetName.Text;
                updateplanet.atmCate = Int32.Parse(tbPlanetatmCate.Text);
                updateplanet.atmMass = Double.Parse(tbPlanetatmMass.Text);
                updateplanet.atmPres = tbPlanetatmPres.Text;
                updateplanet.atmnote = tbPlanetatmNote.Text;
                updateplanet.axialTilt = Double.Parse(tbPlanetaxialTilt.Text);
                updateplanet.mass = Double.Parse(tbPlanetPlanetMass.Text);
                updateplanet.RVM = tbPlanetRVM.Text;
                updateplanet.orbitalPeriod = Double.Parse(tbPlanetorbitalPeriod.Text);
                updateplanet.orbitalRadius = Double.Parse(tbPlanetorbitalRadius.Text);
                updateplanet.orbitalEccent = Double.Parse(tbPlanetorbitalEccent.Text);
                updateplanet.moonRadius = Double.Parse(tbPlanetRadius.Text);
                updateplanet.baseType = Int32.Parse(tbPlanetType.Text);
                updateplanet.SatelliteSize = Int32.Parse(tbPlanetSite.Text);
                updateplanet.blackbodyTemp = Double.Parse(tbPlanetbbt.Text);
                updateplanet.dayFaceMod = Int32.Parse(tbPlanetdayFaceMod.Text);
                updateplanet.density = Double.Parse(tbPlanetDensity.Text);
                updateplanet.diameter = Double.Parse(tbPlanetDiameter.Text);
                updateplanet.gravity = Double.Parse(tbPlanetGravity.Text);
                updateplanet.hydCoverage = tbPlanethydCov.Text;
                updateplanet.isResonant = Boolean.Parse(tbPlanetisRes.Text);
                updateplanet.isTideLocked = Boolean.Parse(tbPlanetisTideLock.Text);
                updateplanet.retrogradeMotion = Boolean.Parse(tbPlanetretrogradeMotion.Text);
                updateplanet.rotationalPeriod = Double.Parse(tbPlanetrotPeriod.Text);
                updateplanet.SatelliteSize = Int32.Parse(tbPlanetSite.Text);
                updateplanet.siderealPeriod = Double.Parse(tbPlanetsiderealPeriod.Text);
                updateplanet.surfaceTemp = Double.Parse(tbPlanetsurfaceTemp.Text);
                updateplanet.tecActivity = Double.Parse(tbPlanettecActivity.Text);
                updateplanet.tideForce = Double.Parse(tbPlanettideForce.Text);
                updateplanet.volActivity = Double.Parse(tbPlanetvolActivity.Text);
                updateplanet.orbitalCycle = Double.Parse(tbPlanetOrbitalCycle.Text);
            }
            using (var dbCtx = new Utility.Db1Entities())
            {
                //3. Mark entity as modified
                dbCtx.Entry(updateplanet).State = System.Data.Entity.EntityState.Modified;

                //4. call SaveChanges
                dbCtx.SaveChanges();
            }
            Forms.SystemSelector.CurrentInstance.LoadSystemsFromSql();
        }
    }
}
