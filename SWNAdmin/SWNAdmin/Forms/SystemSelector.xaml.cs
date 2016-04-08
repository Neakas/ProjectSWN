using System;
using System.Collections.Generic;
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
    public partial class SystemSelector : Window
    {
        public static SystemSelector CurrentInstance;
        private StarSystems LoadedSystem;
        private List<StarSystems> SystemList = new List<StarSystems>();

        public SystemSelector()
        {
            CurrentInstance = this;
            InitializeComponent();
            LoadSystemsFromSql();
            SqlManager.QuerySystem();
        }

        public void LoadSystemsFromSql()
        {
            SystemList = SqlManager.LoadAllSystems();
            cbSelectSystem.ItemsSource = SystemList;
            cbSelectSystem.DisplayMemberPath = "sysName";
            cbSelectSystem.SelectedValuePath = "Id";
        }

        private void cbSelectSystem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadedSystem = null;
            tVSystem.Items.Clear();
            var selectedSystem = cbSelectSystem.SelectedItem as StarSystems;
            LoadCompleteSystem(selectedSystem);
            SetupTreeView();
            //TODOLOW Fix the Display of Empty Orbitals
        }

        public void UpdateTV()
        {
            tVSystem.Items.Clear();
        }

        private void SetupTreeView()
        {
            if (LoadedSystem != null)
            {
                var item = new TreeViewItem();
                item.ToolTip = "System Item";
                item.Header = LoadedSystem.sysName;
                item.Tag = LoadedSystem.Id;
                foreach (var star in LoadedSystem.Stars)
                {
                    var item2 = new TreeViewItem();
                    item2.Header = star.StarOrder;
                    item2.ToolTip = "Star Item";
                    item2.Tag = star.Id;
                    foreach (var planet in star.Planets)
                    {
                        var item3 = new TreeViewItem();
                        item3.ToolTip = "Planet Item";
                        item3.Tag = planet.Id;
                        var MajorMoon = new TreeViewItem();
                        var InnerMoonlet = new TreeViewItem();
                        var OuterMoonlet = new TreeViewItem();
                        MajorMoon.Header = "Major Moons";
                        InnerMoonlet.Header = "Inner Moonlets";
                        OuterMoonlet.Header = "Outer Moonlet";

                        foreach (var mm in planet.MajorMoons1)
                        {
                            var item4 = new TreeViewItem();
                            item4.Tag = mm.Id;
                            item4.ToolTip = "MajorMoon Item";
                            item4.Header = mm.name;
                            MajorMoon.Items.Add(item4);
                        }

                        foreach (var im in planet.InnerMoonlets1)
                        {
                            var item5 = new TreeViewItem();
                            item5.ToolTip = "InnerMoonlet Item";
                            item5.Tag = im.Id;
                            item5.Header = im.Id.ToString();
                            InnerMoonlet.Items.Add(item5);
                        }

                        foreach (var om in planet.OuterMoonlets1)
                        {
                            var item6 = new TreeViewItem();
                            item6.Tag = om.Id;
                            item6.ToolTip = "OuterMoonlet Item";
                            item6.Header = om.Id.ToString();
                            OuterMoonlet.Items.Add(item6);
                        }
                        if (planet.name != "" && planet.name != null)
                        {
                            item3.Header = planet.name;
                        }
                        else
                        {
                            item3.Header = planet.sattype;
                        }
                        if (planet.MajorMoons1.Count > 0)
                        {
                            item3.Items.Add(MajorMoon);
                        }
                        if (planet.InnerMoonlets1.Count > 0)
                        {
                            item3.Items.Add(InnerMoonlet);
                        }
                        if (planet.OuterMoonlets1.Count > 0)
                        {
                            item3.Items.Add(OuterMoonlet);
                        }
                        item2.Items.Add(item3);
                    }
                    item.Items.Add(item2);
                }
                //item.ItemsSource = LoadedSystem.Stars;

                tVSystem.Items.Add(item);
            }
        }

        private void LoadCompleteSystem(StarSystems System)
        {
            if (cbSelectSystem.SelectedItem == null)
            {
            }
            else
            {
                var context = new Db1Entities();
                context.Configuration.ProxyCreationEnabled = false;
                var query =
                    (from p in
                        context.StarSystems.Include("Stars")
                            .Include("Stars")
                            .Include("Stars.Planets")
                            .Include("Stars.Planets.InnerMoonlets1")
                            .Include("Stars.Planets.OuterMoonlets1")
                            .Include("Stars.Planets.MajorMoons1")
                        where p.Id == (int) cbSelectSystem.SelectedValue
                        select p).FirstOrDefault();
                LoadedSystem = query;
            }


            //CalcLocalDay(selectedSystem);
        }


        private void btSendSystem_Click(object sender, RoutedEventArgs e)
        {
            SWNService.CurrentService.SendSystem(LoadedSystem);
        }

        private void CalcLocalDay(StarSystems System)
        {
            foreach (var Star in System.Stars)
            {
                foreach (var Planet in Star.Planets)
                {
                    var SiderealPeriod = Convert.ToDouble(Planet.siderealPeriod);
                    var RotationalPeriod = Convert.ToDouble(Planet.rotationalPeriod);
                    var ApparentLength = 0.00;
                    if (SiderealPeriod != RotationalPeriod)
                    {
                        ApparentLength = SiderealPeriod*RotationalPeriod/(SiderealPeriod - RotationalPeriod);
                    }
                    else
                    {
                        ApparentLength = 0.00;
                    }
                }
            }
        }

        private void tVSystem_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
//Update UI
            ControlCanvas.Children.Clear();

            var selectedtv = (TreeViewItem) tVSystem.SelectedItem;
            if (tVSystem.SelectedItem != null)
            {
                if ((string) selectedtv.ToolTip == "System Item")
                {
                    var mss = new ModifyStarSystem();
                    var conObject = LoadedSystem;
                    mss.tbSysAge.Text = conObject.sysAge.ToString();
                    mss.tbSyshab.Text = conObject.habitableZones.ToString();
                    mss.tbSysID.Text = conObject.Id.ToString();
                    mss.tbSysName.Text = conObject.sysName;
                    mss.tbSysStars.Text = conObject.sysStars.ToString();
                    ControlCanvas.Children.Add(mss);
                }
                if ((string) selectedtv.ToolTip == "Star Item")
                {
                    var ms = new ModifyStar();
                    Stars conObject = null;
                    foreach (var star in LoadedSystem.Stars)
                    {
                        if ((int) selectedtv.Tag == star.Id)
                        {
                            conObject = star;
                            ms.tbStarAge.Text = conObject.starAge.ToString();
                            ms.tbStarColor.Text = conObject.starColor;
                            ms.tbStarDistFromPrim.Text = conObject.distFromPrimary.ToString();
                            ms.tbStareffTemp.Text = conObject.effTemp.ToString();
                            ms.tbStarID.Text = conObject.Id.ToString();
                            ms.tbStarinitLum.Text = conObject.initLumin.ToString();
                            ms.tbStarinitMass.Text = conObject.initMass.ToString();
                            ms.tbStarisFlare.Text = conObject.isFlareStar.ToString();
                            ms.tbStarLum.Text = conObject.currLumin.ToString();
                            ms.tbStarName.Text = conObject.name;
                            ms.tbStarorbitalEccent.Text = conObject.orbitalEccent.ToString();
                            ms.tbStarorbitalPeriod.Text = conObject.orbitalPeriod.ToString();
                            ms.tbStarorbitalRadius.Text = conObject.orbitalRadius.ToString();
                            ms.tbStarOrder.Text = conObject.StarOrder;
                            ms.tbStarPlanets.Text = conObject.sysPlanets.ToString();
                            ms.tbStarRadius.Text = conObject.radius.ToString();
                            ms.tbStarSpecType.Text = conObject.specType;
                            tbText.Text = conObject.StarString;
                        }
                    }
                    ControlCanvas.Children.Add(ms);
                }
                if ((string) selectedtv.ToolTip == "Planet Item")
                {
                    var mp = new ModifyPlanet();

                    Planets conObject = null;
                    foreach (var star in LoadedSystem.Stars)
                    {
                        foreach (var planet in star.Planets)
                        {
                            if ((int) selectedtv.Tag == planet.Id)
                            {
                                conObject = planet;
                                if (conObject.name != null)
                                {
                                    mp.tbPlanetName.Text = conObject.name;
                                }
                                else
                                {
                                    mp.tbPlanetName.Text = conObject.sattype;
                                }

                                mp.tbPlanetAge.Text = "N/A";
                                mp.tbPlanetatmCate.Text = conObject.atmCate.ToString();
                                mp.tbPlanetatmMass.Text = conObject.atmMass.ToString();
                                mp.tbPlanetatmNote.Text = conObject.atmnote;
                                mp.tbPlanetatmPres.Text = conObject.atmPres;
                                mp.tbPlanetaxialTilt.Text = conObject.axialTilt.ToString();
                                mp.tbPlanetbbt.Text = conObject.blackbodyTemp.ToString();
                                mp.tbPlanetdayFaceMod.Text = conObject.dayFaceMod.ToString();
                                mp.tbPlanetDensity.Text = conObject.density.ToString();
                                mp.tbPlanetDiameter.Text = conObject.diameter.ToString();
                                mp.tbPlanetGravity.Text = conObject.gravity.ToString();
                                mp.tbPlanethydCov.Text = conObject.hydCoverage;
                                mp.tbPlanetID.Text = conObject.Id.ToString();
                                mp.tbPlanetisRes.Text = conObject.isResonant.ToString();
                                mp.tbPlanetisTideLock.Text = conObject.isTideLocked.ToString();
                                mp.tbPlanetNightFaceMod.Text = conObject.nightFaceMod.ToString();
                                mp.tbPlanetOrbitalCycle.Text = conObject.orbitalCycle.ToString();
                                mp.tbPlanetorbitalEccent.Text = conObject.orbitalEccent.ToString();
                                mp.tbPlanetorbitalPeriod.Text = conObject.orbitalPeriod.ToString();
                                mp.tbPlanetorbitalRadius.Text = conObject.orbitalRadius.ToString();
                                mp.tbPlanetOrder.Text = conObject.masterOrderID.ToString();
                                mp.tbPlanetPlanetMass.Text = conObject.mass.ToString();
                                mp.tbPlanetRadius.Text = conObject.moonRadius.ToString();
                                mp.tbPlanetretrogradeMotion.Text = conObject.retrogradeMotion.ToString();
                                mp.tbPlanetrotPeriod.Text = conObject.rotationalPeriod.ToString();
                                mp.tbPlanetRVM.Text = conObject.RVM;
                                mp.tbPlanetsatType.Text = conObject.sattype;
                                mp.tbPlanetsiderealPeriod.Text = conObject.siderealPeriod.ToString();
                                mp.tbPlanetSite.Text = conObject.SatelliteSize.ToString();
                                mp.tbPlanetsurfaceTemp.Text = conObject.surfaceTemp.ToString();
                                mp.tbPlanettecActivity.Text = conObject.tecActivity.ToString();
                                mp.tbPlanettideForce.Text = "0.00";
                                mp.tbPlanettideTotal.Text = conObject.tideTotal.ToString();
                                mp.tbPlanetType.Text = conObject.baseType.ToString();
                                mp.tbPlanetvolActivity.Text = conObject.volActivity.ToString();
                                tbText.Text = conObject.planetString;
                            }
                        }
                    }
                    ControlCanvas.Children.Add(mp);
                }
                if ((string) selectedtv.ToolTip == "MajorMoon Item")
                {
                    var mmm = new ModifyMajorMoon();

                    MajorMoons conObject = null;
                    foreach (var star in LoadedSystem.Stars)
                    {
                        foreach (var planet in star.Planets)
                        {
                            foreach (var majormoon in planet.MajorMoons1)
                            {
                                if ((int) selectedtv.Tag == majormoon.Id)
                                {
                                    conObject = majormoon;
                                    mmm.tbMoonAge.Text = "N/A";
                                    mmm.tbMoonatmCate.Text = conObject.atmCate.ToString();
                                    mmm.tbMoonatmMass.Text = conObject.mass.ToString();
                                    mmm.tbMoonatmNote.Text = "N/A";
                                    mmm.tbMoonatmPres.Text = "N/A";
                                    mmm.tbMoonaxialTilt.Text = conObject.axialTilt.ToString();
                                    mmm.tbMoonbbt.Text = conObject.blackbodyTemp.ToString();
                                    mmm.tbMoondayFaceMod.Text = conObject.dayFaceMod.ToString();
                                    mmm.tbMoonDensity.Text = conObject.density.ToString();
                                    mmm.tbMoonDiameter.Text = conObject.diameter.ToString();
                                    mmm.tbMoonGravity.Text = conObject.gravity.ToString();
                                    mmm.tbMoonhydCov.Text = conObject.hydCoverage;
                                    mmm.tbMoonID.Text = conObject.Id.ToString();
                                    mmm.tbMoonisRes.Text = conObject.isResonant.ToString();
                                    mmm.tbMoonisTideLock.Text = conObject.isTideLocked.ToString();
                                    mmm.tbMoonMoonMass.Text = conObject.mass.ToString();
                                    mmm.tbMoonName.Text = conObject.name;
                                    mmm.tbMoonNightFaceMod.Text = conObject.nightFaceMod.ToString();
                                    mmm.tbMoonOrbitalCycle.Text = conObject.orbitalCycle.ToString();
                                    mmm.tbMoonorbitalEccent.Text = conObject.orbitalEccent.ToString();
                                    mmm.tbMoonorbitalPeriod.Text = conObject.orbitalPeriod.ToString();
                                    mmm.tbMoonorbitalRadius.Text = conObject.orbitalRadius.ToString();
                                    mmm.tbMoonOrder.Text = conObject.masterOrderId.ToString();
                                    mmm.tbMoonRadius.Text = conObject.moonRadius.ToString();
                                    mmm.tbMoonretrogradeMotion.Text = conObject.retrogradeMotion.ToString();
                                    mmm.tbMoonrotPeriod.Text = conObject.rotationalPeriod.ToString();
                                    mmm.tbMoonRVM.Text = conObject.RVM.ToString();
                                    mmm.tbMoonsatType.Text = conObject.SatelliteType.ToString();
                                    mmm.tbMoonsiderealPeriod.Text = conObject.siderealPeriod.ToString();
                                    mmm.tbMoonSite.Text = conObject.SatelliteSize.ToString();
                                    mmm.tbMoonsurfaceTemp.Text = conObject.surfaceTemp.ToString();
                                    mmm.tbMoontecActivity.Text = conObject.tecActivity.ToString();
                                    mmm.tbMoontideForce.Text = conObject.tideForce.ToString();
                                    mmm.tbMoontideTotal.Text = conObject.tideTotal.ToString();
                                    mmm.tbMoonType.Text = conObject.baseType.ToString();
                                    mmm.tbMoonvolActivity.Text = conObject.volActivity.ToString();
                                    tbText.Text = conObject.MajorMoonString;
                                }
                            }
                        }
                    }
                    ControlCanvas.Children.Add(mmm);
                }
                if ((string) selectedtv.ToolTip == "OuterMoonlet Item" ||
                    (string) selectedtv.ToolTip == "InnerMoonlet Item")
                {
                    var modm = new ModifyMoonlet();

                    OuterMoonlets conObjectOuter = null;
                    InnerMoonlets conObjectInner = null;
                    if ((string) selectedtv.ToolTip == "OuterMoonlet Item")
                    {
                        modm.tbIsOuter.Text = "true";
                        foreach (var star in LoadedSystem.Stars)
                        {
                            foreach (var planet in star.Planets)
                            {
                                foreach (var outermoonlet in planet.OuterMoonlets1)
                                {
                                    if ((int) selectedtv.Tag == outermoonlet.Id)
                                    {
                                        conObjectOuter = outermoonlet;
                                        modm.tbMoonletblackbodyTemp.Text = conObjectOuter.blackbodyTemp.ToString();
                                        modm.tbMoonletID.Text = conObjectOuter.Id.ToString();
                                        modm.tbMoonletName.Text = "";
                                        modm.tbMoonletorbitalEccent.Text = conObjectOuter.orbitalEccent.ToString();
                                        modm.tbMoonletorbitalPeriod.Text = conObjectOuter.orbitalPeriod.ToString();
                                        modm.tbMoonletorbitalRadius.Text = conObjectOuter.orbitalRadius.ToString();
                                        modm.tbMoonletRadius.Text = conObjectOuter.planetRadius.ToString();
                                        tbText.Text = conObjectOuter.outerMoonString;
                                    }
                                }
                            }
                        }
                    }
                    if ((string) selectedtv.ToolTip == "InnerMoonlet Item")
                    {
                        modm.tbIsOuter.Text = "false";
                        foreach (var star in LoadedSystem.Stars)
                        {
                            foreach (var planet in star.Planets)
                            {
                                foreach (var innermoonlet in planet.InnerMoonlets1)
                                {
                                    if ((int) selectedtv.Tag == innermoonlet.Id)
                                    {
                                        conObjectInner = innermoonlet;
                                        modm.tbMoonletblackbodyTemp.Text = conObjectInner.blackbodyTemp.ToString();
                                        modm.tbMoonletID.Text = conObjectInner.Id.ToString();
                                        modm.tbMoonletName.Text = "";
                                        modm.tbMoonletorbitalEccent.Text = conObjectInner.orbitalEccent.ToString();
                                        modm.tbMoonletorbitalPeriod.Text = conObjectInner.orbitalPeriod.ToString();
                                        modm.tbMoonletorbitalRadius.Text = conObjectInner.orbitalRadius.ToString();
                                        modm.tbMoonletRadius.Text = conObjectInner.planetRadius.ToString();
                                        tbText.Text = conObjectInner.innerMoonString;
                                    }
                                }
                            }
                        }
                    }
                    ControlCanvas.Children.Add(modm);
                }
            }
        }
    }
}