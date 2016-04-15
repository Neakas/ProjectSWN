using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SWNAdmin.Networking;
using SWNAdmin.UserControls;
using SWNAdmin.Utility;

namespace SWNAdmin.Forms
{
    /// <summary>
    ///     Interaction logic for SystemSelector.xaml
    /// </summary>
    public partial class SystemSelector
    {
        public static SystemSelector CurrentInstance;
        private StarSystems _loadedSystem;
        private List<StarSystems> _systemList = new List<StarSystems>();

        public SystemSelector()
        {
            CurrentInstance = this;
            InitializeComponent();
            LoadSystemsFromSql();
        }

        public void LoadSystemsFromSql()
        {
            _systemList = SqlManager.LoadAllSystems();
            CbSelectSystem.ItemsSource = _systemList;
            CbSelectSystem.DisplayMemberPath = "sysName";
            CbSelectSystem.SelectedValuePath = "Id";
        }

        private void cbSelectSystem_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            _loadedSystem = null;
            TvSystem.Items.Clear();
            LoadCompleteSystem();
            SetupTreeView();
            //TODOLOW Fix the Display of Empty Orbitals
        }

        public void UpdateTv()
        {
            TvSystem.Items.Clear();
        }

        private void SetupTreeView()
        {
            if (_loadedSystem != null)
            {
                var item = new TreeViewItem
                {
                    ToolTip = "system Item",
                    Header = _loadedSystem.sysName,
                    Tag = _loadedSystem.Id
                };
                foreach (var star in _loadedSystem.Stars)
                {
                    var item2 = new TreeViewItem
                    {
                        Header = star.StarOrder,
                        ToolTip = "Star Item",
                        Tag = star.Id
                    };
                    foreach (var planet in star.Planets)
                    {
                        var item3 = new TreeViewItem
                        {
                            ToolTip = "Planet Item",
                            Tag = planet.Id
                        };
                        var majorMoon = new TreeViewItem();
                        var innerMoonlet = new TreeViewItem();
                        var outerMoonlet = new TreeViewItem();
                        majorMoon.Header = "Major Moons";
                        innerMoonlet.Header = "Inner Moonlets";
                        outerMoonlet.Header = "Outer Moonlet";

                        foreach (var item4 in planet.MajorMoons1.Select(mm => new TreeViewItem
                        {
                            Tag = mm.Id,
                            ToolTip = "MajorMoon Item",
                            Header = mm.name
                        }))
                        {
                            majorMoon.Items.Add(item4);
                        }

                        foreach (var item5 in planet.InnerMoonlets1.Select(im => new TreeViewItem
                        {
                            ToolTip = "InnerMoonlet Item",
                            Tag = im.Id,
                            Header = im.Id.ToString()
                        }))
                        {
                            innerMoonlet.Items.Add(item5);
                        }

                        foreach (var item6 in planet.OuterMoonlets1.Select(om => new TreeViewItem
                        {
                            Tag = om.Id,
                            ToolTip = "OuterMoonlet Item",
                            Header = om.Id.ToString()
                        }))
                        {
                            outerMoonlet.Items.Add(item6);
                        }
                        item3.Header = !string.IsNullOrEmpty(planet.name) ? planet.name : planet.sattype;
                        if (planet.MajorMoons1.Count > 0)
                        {
                            item3.Items.Add(majorMoon);
                        }
                        if (planet.InnerMoonlets1.Count > 0)
                        {
                            item3.Items.Add(innerMoonlet);
                        }
                        if (planet.OuterMoonlets1.Count > 0)
                        {
                            item3.Items.Add(outerMoonlet);
                        }
                        item2.Items.Add(item3);
                    }
                    item.Items.Add(item2);
                }

                TvSystem.Items.Add(item);
            }
        }

        private void LoadCompleteSystem()
        {
            if (CbSelectSystem.SelectedItem == null)
            {
            }
            else
            {
                var context = new Db1Entities();
                context.Configuration.ProxyCreationEnabled = false;
                var query = ( from p in context.StarSystems.Include("Stars").Include("Stars").Include("Stars.Planets").Include("Stars.Planets.InnerMoonlets1").Include("Stars.Planets.OuterMoonlets1").Include("Stars.Planets.MajorMoons1")
                    where p.Id == (int) CbSelectSystem.SelectedValue
                    select p ).FirstOrDefault();
                _loadedSystem = query;
            }
        }

        private void btSendSystem_Click( object sender, RoutedEventArgs e )
        {
            SwnService.CurrentService.SendSystem(_loadedSystem);
        }

        private void tVSystem_SelectedItemChanged( object sender, RoutedPropertyChangedEventArgs<object> e )
        {
//Update UI
            ControlCanvas.Children.Clear();

            var selectedtv = (TreeViewItem) TvSystem.SelectedItem;
            if (TvSystem.SelectedItem == null)
            {
                return;
            }
            if ((string) selectedtv.ToolTip == "system Item")
            {
                var mss = new ModifyStarSystem();
                var conObject = _loadedSystem;
                mss.TbSysAge.Text = conObject.sysAge.ToString(CultureInfo.InvariantCulture);
                mss.TbSyshab.Text = conObject.habitableZones.ToString();
                mss.TbSysId.Text = conObject.Id.ToString();
                mss.TbSysName.Text = conObject.sysName;
                mss.TbSysStars.Text = conObject.sysStars.ToString();
                ControlCanvas.Children.Add(mss);
            }
            if ((string) selectedtv.ToolTip == "Star Item")
            {
                var ms = new ModifyStar();
                foreach (var conObject in _loadedSystem.Stars.Where(star => (int) selectedtv.Tag == star.Id))
                {
                    ms.TbStarAge.Text = conObject.starAge.ToString(CultureInfo.InvariantCulture);
                    ms.TbStarColor.Text = conObject.starColor;
                    ms.TbStarDistFromPrim.Text = conObject.distFromPrimary.ToString(CultureInfo.InvariantCulture);
                    ms.TbStareffTemp.Text = conObject.effTemp.ToString(CultureInfo.InvariantCulture);
                    ms.TbStarId.Text = conObject.Id.ToString();
                    ms.TbStarinitLum.Text = conObject.initLumin.ToString(CultureInfo.InvariantCulture);
                    ms.TbStarinitMass.Text = conObject.initMass.ToString(CultureInfo.InvariantCulture);
                    ms.TbStarisFlare.Text = conObject.isFlareStar.ToString();
                    ms.TbStarLum.Text = conObject.currLumin.ToString(CultureInfo.InvariantCulture);
                    ms.TbStarName.Text = conObject.name;
                    ms.TbStarorbitalEccent.Text = conObject.orbitalEccent.ToString();
                    ms.TbStarorbitalPeriod.Text = conObject.orbitalPeriod.ToString();
                    ms.TbStarorbitalRadius.Text = conObject.orbitalRadius.ToString();
                    ms.TbStarOrder.Text = conObject.StarOrder;
                    ms.TbStarPlanets.Text = conObject.sysPlanets.ToString();
                    ms.TbStarRadius.Text = conObject.radius.ToString(CultureInfo.InvariantCulture);
                    ms.TbStarSpecType.Text = conObject.specType;
                    TbText.Text = conObject.StarString;
                }
                ControlCanvas.Children.Add(ms);
            }
            if ((string) selectedtv.ToolTip == "Planet Item")
            {
                var mp = new ModifyPlanet();

                foreach (var conObject in _loadedSystem.Stars.SelectMany(star => star.Planets.Where(planet => (int) selectedtv.Tag == planet.Id)))
                {
                    mp.TbPlanetName.Text = conObject.name ?? conObject.sattype;

                    mp.TbPlanetAge.Text = "N/A";
                    mp.TbPlanetatmCate.Text = conObject.atmCate.ToString();
                    mp.TbPlanetatmMass.Text = conObject.atmMass.ToString();
                    mp.TbPlanetatmNote.Text = conObject.atmnote;
                    mp.TbPlanetatmPres.Text = conObject.atmPres;
                    mp.TbPlanetaxialTilt.Text = conObject.axialTilt.ToString();
                    mp.TbPlanetbbt.Text = conObject.blackbodyTemp.ToString();
                    mp.TbPlanetdayFaceMod.Text = conObject.dayFaceMod.ToString();
                    mp.TbPlanetDensity.Text = conObject.density.ToString();
                    mp.TbPlanetDiameter.Text = conObject.diameter.ToString();
                    mp.TbPlanetGravity.Text = conObject.gravity.ToString();
                    mp.TbPlanethydCov.Text = conObject.hydCoverage;
                    mp.TbPlanetId.Text = conObject.Id.ToString();
                    mp.TbPlanetisRes.Text = conObject.isResonant.ToString();
                    mp.TbPlanetisTideLock.Text = conObject.isTideLocked.ToString();
                    mp.TbPlanetNightFaceMod.Text = conObject.nightFaceMod.ToString();
                    mp.TbPlanetOrbitalCycle.Text = conObject.orbitalCycle.ToString();
                    mp.TbPlanetorbitalEccent.Text = conObject.orbitalEccent.ToString();
                    mp.TbPlanetorbitalPeriod.Text = conObject.orbitalPeriod.ToString();
                    mp.TbPlanetorbitalRadius.Text = conObject.orbitalRadius.ToString();
                    mp.TbPlanetOrder.Text = conObject.masterOrderID.ToString();
                    mp.TbPlanetPlanetMass.Text = conObject.mass.ToString();
                    mp.TbPlanetRadius.Text = conObject.moonRadius.ToString();
                    mp.TbPlanetretrogradeMotion.Text = conObject.retrogradeMotion.ToString();
                    mp.TbPlanetrotPeriod.Text = conObject.rotationalPeriod.ToString();
                    mp.TbPlanetRvm.Text = conObject.RVM;
                    mp.TbPlanetsatType.Text = conObject.sattype;
                    mp.TbPlanetsiderealPeriod.Text = conObject.siderealPeriod.ToString();
                    mp.TbPlanetSite.Text = conObject.SatelliteSize.ToString();
                    mp.TbPlanetsurfaceTemp.Text = conObject.surfaceTemp.ToString();
                    mp.TbPlanettecActivity.Text = conObject.tecActivity.ToString();
                    mp.TbPlanettideForce.Text = "0.00";
                    mp.TbPlanettideTotal.Text = conObject.tideTotal.ToString();
                    mp.TbPlanetType.Text = conObject.baseType.ToString();
                    mp.TbPlanetvolActivity.Text = conObject.volActivity.ToString();
                    TbText.Text = conObject.planetString;
                }
                ControlCanvas.Children.Add(mp);
            }
            if ((string) selectedtv.ToolTip == "MajorMoon Item")
            {
                var mmm = new ModifyMajorMoon();

                foreach (var majormoon in _loadedSystem.Stars.SelectMany(star => star.Planets.SelectMany(planet => planet.MajorMoons1.Where(majormoon => (int) selectedtv.Tag == majormoon.Id))))
                {
                    var conObject = majormoon;
                    mmm.TbMoonAge.Text = "N/A";
                    mmm.TbMoonatmCate.Text = conObject.atmCate.ToString();
                    mmm.TbMoonatmMass.Text = conObject.mass.ToString();
                    mmm.TbMoonatmNote.Text = "N/A";
                    mmm.TbMoonatmPres.Text = "N/A";
                    mmm.TbMoonaxialTilt.Text = conObject.axialTilt.ToString();
                    mmm.TbMoonbbt.Text = conObject.blackbodyTemp.ToString();
                    mmm.TbMoondayFaceMod.Text = conObject.dayFaceMod.ToString();
                    mmm.TbMoonDensity.Text = conObject.density.ToString();
                    mmm.TbMoonDiameter.Text = conObject.diameter.ToString();
                    mmm.TbMoonGravity.Text = conObject.gravity.ToString();
                    mmm.TbMoonhydCov.Text = conObject.hydCoverage;
                    mmm.TbMoonId.Text = conObject.Id.ToString();
                    mmm.TbMoonisRes.Text = conObject.isResonant.ToString();
                    mmm.TbMoonisTideLock.Text = conObject.isTideLocked.ToString();
                    mmm.TbMoonMoonMass.Text = conObject.mass.ToString();
                    mmm.TbMoonName.Text = conObject.name;
                    mmm.TbMoonNightFaceMod.Text = conObject.nightFaceMod.ToString();
                    mmm.TbMoonOrbitalCycle.Text = conObject.orbitalCycle.ToString();
                    mmm.TbMoonorbitalEccent.Text = conObject.orbitalEccent.ToString();
                    mmm.TbMoonorbitalPeriod.Text = conObject.orbitalPeriod.ToString();
                    mmm.TbMoonorbitalRadius.Text = conObject.orbitalRadius.ToString();
                    mmm.TbMoonOrder.Text = conObject.masterOrderId.ToString();
                    mmm.TbMoonRadius.Text = conObject.moonRadius.ToString();
                    mmm.TbMoonretrogradeMotion.Text = conObject.retrogradeMotion.ToString();
                    mmm.TbMoonrotPeriod.Text = conObject.rotationalPeriod.ToString();
                    mmm.TbMoonRvm.Text = conObject.RVM.ToString();
                    mmm.TbMoonsatType.Text = conObject.SatelliteType.ToString();
                    mmm.TbMoonsiderealPeriod.Text = conObject.siderealPeriod.ToString();
                    mmm.TbMoonSite.Text = conObject.SatelliteSize.ToString();
                    mmm.TbMoonsurfaceTemp.Text = conObject.surfaceTemp.ToString();
                    mmm.TbMoontecActivity.Text = conObject.tecActivity.ToString();
                    mmm.TbMoontideForce.Text = conObject.tideForce.ToString();
                    mmm.TbMoontideTotal.Text = conObject.tideTotal.ToString();
                    mmm.TbMoonType.Text = conObject.baseType.ToString();
                    mmm.TbMoonvolActivity.Text = conObject.volActivity.ToString();
                    TbText.Text = conObject.MajorMoonString;
                }
                ControlCanvas.Children.Add(mmm);
            }
            if ((string) selectedtv.ToolTip != "OuterMoonlet Item" && (string) selectedtv.ToolTip != "InnerMoonlet Item")
            {
                return;
            }
            {
                var modm = new ModifyMoonlet();
                if ((string) selectedtv.ToolTip == "OuterMoonlet Item")
                {
                    modm.TbIsOuter.Text = "true";
                    foreach (var conObjectOuter in _loadedSystem.Stars.SelectMany(star => star.Planets.SelectMany(planet => planet.OuterMoonlets1.Where(outermoonlet => (int) selectedtv.Tag == outermoonlet.Id))))
                    {
                        modm.TbMoonletblackbodyTemp.Text = conObjectOuter.blackbodyTemp.ToString();
                        modm.TbMoonletId.Text = conObjectOuter.Id.ToString();
                        modm.TbMoonletName.Text = "";
                        modm.TbMoonletorbitalEccent.Text = conObjectOuter.orbitalEccent.ToString();
                        modm.TbMoonletorbitalPeriod.Text = conObjectOuter.orbitalPeriod.ToString();
                        modm.TbMoonletorbitalRadius.Text = conObjectOuter.orbitalRadius.ToString();
                        modm.TbMoonletRadius.Text = conObjectOuter.planetRadius.ToString();
                        TbText.Text = conObjectOuter.outerMoonString;
                    }
                }
                if ((string) selectedtv.ToolTip == "InnerMoonlet Item")
                {
                    modm.TbIsOuter.Text = "false";
                    foreach (var conObjectInner in _loadedSystem.Stars.SelectMany(star => star.Planets.SelectMany(planet => planet.InnerMoonlets1.Where(innermoonlet => (int) selectedtv.Tag == innermoonlet.Id))))
                    {
                        modm.TbMoonletblackbodyTemp.Text = conObjectInner.blackbodyTemp.ToString();
                        modm.TbMoonletId.Text = conObjectInner.Id.ToString();
                        modm.TbMoonletName.Text = "";
                        modm.TbMoonletorbitalEccent.Text = conObjectInner.orbitalEccent.ToString();
                        modm.TbMoonletorbitalPeriod.Text = conObjectInner.orbitalPeriod.ToString();
                        modm.TbMoonletorbitalRadius.Text = conObjectInner.orbitalRadius.ToString();
                        modm.TbMoonletRadius.Text = conObjectInner.planetRadius.ToString();
                        TbText.Text = conObjectInner.innerMoonString;
                    }
                }
                ControlCanvas.Children.Add(modm);
            }
        }
    }
}