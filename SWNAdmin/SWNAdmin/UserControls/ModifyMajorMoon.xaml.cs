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
    public partial class ModifyMajorMoon : UserControl
    {
        public ModifyMajorMoon()
        {
            InitializeComponent();
        }

        private void btApply_Click(object sender, RoutedEventArgs e)
        {
            Utility.MajorMoons updateplanet;
            using (var ctx = new Utility.Db1Entities())
            {
                updateplanet = ctx.MajorMoons.Where(s => s.Id.ToString() == tbMoonID.Text).FirstOrDefault<Utility.MajorMoons>();
            }
            if (updateplanet != null)
            {

                    updateplanet.name = tbMoonName.Text;
                updateplanet.atmCate = Int32.Parse(tbMoonatmCate.Text);
                updateplanet.atmMass = Double.Parse(tbMoonatmMass.Text);
                updateplanet.axialTilt = Double.Parse(tbMoonaxialTilt.Text);
                updateplanet.mass = Double.Parse(tbMoonMoonMass.Text);
                updateplanet.RVM = Double.Parse(tbMoonRVM.Text);
                updateplanet.orbitalPeriod = Double.Parse(tbMoonorbitalPeriod.Text);
                updateplanet.orbitalRadius = Double.Parse(tbMoonorbitalRadius.Text);
                updateplanet.orbitalEccent = Double.Parse(tbMoonorbitalEccent.Text);
                updateplanet.moonRadius = Double.Parse(tbMoonRadius.Text);
                updateplanet.baseType = Int32.Parse(tbMoonType.Text);
                updateplanet.SatelliteSize = Int32.Parse(tbMoonSite.Text);
                updateplanet.blackbodyTemp = Double.Parse(tbMoonbbt.Text);
                updateplanet.dayFaceMod = Int32.Parse(tbMoondayFaceMod.Text);
                updateplanet.density = Double.Parse(tbMoonDensity.Text);
                updateplanet.diameter = Double.Parse(tbMoonDiameter.Text);
                updateplanet.gravity = Double.Parse(tbMoonGravity.Text);
                updateplanet.hydCoverage = tbMoonhydCov.Text;
                updateplanet.isResonant = Boolean.Parse(tbMoonisRes.Text);
                updateplanet.isTideLocked = Boolean.Parse(tbMoonisTideLock.Text);
                updateplanet.retrogradeMotion = Boolean.Parse(tbMoonretrogradeMotion.Text);
                updateplanet.rotationalPeriod = Double.Parse(tbMoonrotPeriod.Text);
                updateplanet.SatelliteSize = Int32.Parse(tbMoonSite.Text);
                updateplanet.siderealPeriod = Double.Parse(tbMoonsiderealPeriod.Text);
                updateplanet.surfaceTemp = Double.Parse(tbMoonsurfaceTemp.Text);
                updateplanet.tecActivity = Double.Parse(tbMoontecActivity.Text);
                updateplanet.tideForce = Int32.Parse(tbMoontideForce.Text);
                updateplanet.tideTotal = Int32.Parse(tbMoontideTotal.Text);
                updateplanet.volActivity = Double.Parse(tbMoonvolActivity.Text);
                updateplanet.orbitalCycle = Double.Parse(tbMoonOrbitalCycle.Text);


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
