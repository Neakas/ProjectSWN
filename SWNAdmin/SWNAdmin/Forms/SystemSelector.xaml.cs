using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Windows.Shapes;
using UniverseGeneration;


namespace SWNAdmin.Forms
{
    /// <summary>
    /// Interaction logic for SystemSelector.xaml
    /// </summary>
    ///
    public partial class SystemSelector : Window
    {
        public static SystemSelector CurrentInstance;
        List<Utility.StarSystems> SystemList = new List<Utility.StarSystems>();
        Utility.StarSystems LoadedSystem;

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
            Utility.StarSystems selectedSystem = cbSelectSystem.SelectedItem as Utility.StarSystems;
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
            TreeViewItem item = new TreeViewItem();
            item.ToolTip = "System Item";
            item.Header = LoadedSystem.sysName;
            item.Tag = LoadedSystem.Id;
            foreach (Utility.Stars star in LoadedSystem.Stars)
            {
                TreeViewItem item2 = new TreeViewItem();
                item2.Header = star.StarOrder;
                item2.ToolTip = "Star Item";
                item2.Tag = star.Id;
                foreach (Utility.Planets planet in star.Planets)
                {
                    TreeViewItem item3 = new TreeViewItem();
                    item3.ToolTip = "Planet Item";
                    item3.Tag = planet.Id;
                    TreeViewItem MajorMoon = new TreeViewItem();
                    TreeViewItem InnerMoonlet = new TreeViewItem();
                    TreeViewItem OuterMoonlet = new TreeViewItem();
                    MajorMoon.Header = "Major Moons";
                    InnerMoonlet.Header = "Inner Moonlets";
                    OuterMoonlet.Header = "Outer Moonlet";

                    foreach (Utility.MajorMoons mm in planet.MajorMoons1)
                    {
                        TreeViewItem item4 = new TreeViewItem();
                        item4.Tag = mm.Id;
                        item4.ToolTip = "MajorMoon Item";
                        item4.Header = mm.name;
                        MajorMoon.Items.Add(item4);
                    }

                    foreach (Utility.InnerMoonlets im in planet.InnerMoonlets1)
                    {
                        TreeViewItem item5 = new TreeViewItem();
                        item5.ToolTip = "InnerMoonlet Item";
                        item5.Tag = im.Id;
                        item5.Header = im.Id.ToString();
                        InnerMoonlet.Items.Add(item5);
                    }

                    foreach (Utility.OuterMoonlets om in planet.OuterMoonlets1)
                    {
                        TreeViewItem item6 = new TreeViewItem();
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

        private void LoadCompleteSystem(Utility.StarSystems System)
        {
            if (cbSelectSystem.SelectedItem == null)
            {
                return;
            }
            else
            {
                var context = new Utility.Db1Entities();
                context.Configuration.ProxyCreationEnabled = false;
                var query = (from p in context.StarSystems.Include("Stars").Include("Stars").Include("Stars.Planets").Include("Stars.Planets.InnerMoonlets1").Include("Stars.Planets.OuterMoonlets1").Include("Stars.Planets.MajorMoons1")
                             where p.Id == (int)cbSelectSystem.SelectedValue
                             select p).FirstOrDefault();
                LoadedSystem = query;
            }


         
            
            //CalcLocalDay(selectedSystem);
        }


        private void btSendSystem_Click(object sender, RoutedEventArgs e)
        {
            SWNService.CurrentService.SendSystem(LoadedSystem as Utility.StarSystems);
        }

        private void CalcLocalDay(Utility.StarSystems System)
        {
            foreach (Utility.Stars Star in System.Stars)
            {
                foreach (Utility.Planets Planet in Star.Planets)
                {
                    Double SiderealPeriod = Convert.ToDouble(Planet.siderealPeriod);
                    Double RotationalPeriod = Convert.ToDouble(Planet.rotationalPeriod);
                    Double ApparentLength = 0.00;
                    if (SiderealPeriod != RotationalPeriod)
                    {
                        ApparentLength = (SiderealPeriod * RotationalPeriod) / (SiderealPeriod - RotationalPeriod);
                    }
                    else
                    {
                        ApparentLength = 0.00;
                    }
                    
                }
            }

            
        }

        private void tVSystem_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {//Update UI
            ControlCanvas.Children.Clear();
            
            TreeViewItem selectedtv = (TreeViewItem)tVSystem.SelectedItem;
            if (tVSystem.SelectedItem != null)
            {
                if ((string)selectedtv.ToolTip == "System Item")
                {
                    UserControls.ModifyStarSystem mss = new UserControls.ModifyStarSystem();                   
                    Utility.StarSystems conObject = LoadedSystem;
                    mss.tbSysAge.Text = conObject.sysAge.ToString();
                    mss.tbSyshab.Text = conObject.habitableZones.ToString();
                    mss.tbSysID.Text = conObject.Id.ToString();
                    mss.tbSysName.Text = conObject.sysName.ToString();
                    mss.tbSysStars.Text = conObject.sysStars.ToString();
                    ControlCanvas.Children.Add(mss);

                }
                if ((string)selectedtv.ToolTip == "Star Item")
                {
                    UserControls.ModifyStar ms = new UserControls.ModifyStar();
                    Utility.Stars conObject = null;
                    foreach (Utility.Stars star in LoadedSystem.Stars)
                    {
                        if ((Int32)selectedtv.Tag == star.Id)
                        {
                            conObject = star;
                            ms.tbStarAge.Text = conObject.starAge.ToString();
                            ms.tbStarColor.Text = conObject.starColor.ToString();
                            ms.tbStarDistFromPrim.Text = conObject.distFromPrimary.ToString();
                            ms.tbStareffTemp.Text = conObject.effTemp.ToString();
                            ms.tbStarID.Text = conObject.Id.ToString();
                            ms.tbStarinitLum.Text = conObject.initLumin.ToString();
                            ms.tbStarinitMass.Text = conObject.initMass.ToString();
                            ms.tbStarisFlare.Text = conObject.isFlareStar.ToString();
                            ms.tbStarLum.Text = conObject.currLumin.ToString();
                            ms.tbStarName.Text = conObject.name.ToString();
                            ms.tbStarorbitalEccent.Text = conObject.orbitalEccent.ToString();
                            ms.tbStarorbitalPeriod.Text = conObject.orbitalPeriod.ToString();
                            ms.tbStarorbitalRadius.Text = conObject.orbitalRadius.ToString();
                            ms.tbStarOrder.Text = conObject.StarOrder.ToString();
                            ms.tbStarPlanets.Text = conObject.sysPlanets.ToString();
                            ms.tbStarRadius.Text = conObject.radius.ToString();
                            ms.tbStarSpecType.Text = conObject.specType.ToString();
                            tbText.Text = conObject.StarString;
                        }
                    }
                    ControlCanvas.Children.Add(ms);
                }
                if ((string)selectedtv.ToolTip == "Planet Item")
                {
                    UserControls.ModifyPlanet mp = new UserControls.ModifyPlanet();
                    
                    Utility.Planets conObject = null;
                    foreach (Utility.Stars star in LoadedSystem.Stars)
                    {
                        foreach (Utility.Planets planet in star.Planets)
                        {
                            if ((Int32)selectedtv.Tag == planet.Id)
                            {
                                conObject = planet;
                                mp.tbPlanetName.Text = conObject.name.ToString();
                                mp.tbPlanetAge.Text = "N/A";
                                mp.tbPlanetatmCate.Text = conObject.atmCate.ToString();
                                mp.tbPlanetatmMass.Text = conObject.atmMass.ToString();
                                mp.tbPlanetatmNote.Text = conObject.atmnote.ToString();
                                mp.tbPlanetatmPres.Text = conObject.atmPres.ToString();
                                mp.tbPlanetaxialTilt.Text = conObject.axialTilt.ToString();
                                mp.tbPlanetbbt.Text = conObject.blackbodyTemp.ToString();
                                mp.tbPlanetdayFaceMod.Text = conObject.dayFaceMod.ToString();
                                mp.tbPlanetDensity.Text = conObject.density.ToString();
                                mp.tbPlanetDiameter.Text = conObject.diameter.ToString();
                                mp.tbPlanetGravity.Text = conObject.gravity.ToString();
                                mp.tbPlanethydCov.Text = conObject.hydCoverage.ToString();
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
                                mp.tbPlanetRVM.Text = conObject.RVM.ToString();
                                mp.tbPlanetsatType.Text = conObject.sattype.ToString();
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
                if ((string)selectedtv.ToolTip == "MajorMoon Item")
                {
                    UserControls.ModifyMajorMoon mmm = new UserControls.ModifyMajorMoon();
                   
                    Utility.MajorMoons conObject = null;
                    foreach (Utility.Stars star in LoadedSystem.Stars)
                    {
                        foreach (Utility.Planets planet in star.Planets)
                        {
                            foreach (Utility.MajorMoons majormoon in planet.MajorMoons1)
                            {
                                if ((Int32)selectedtv.Tag == majormoon.Id)
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
                                    mmm.tbMoonhydCov.Text = conObject.hydCoverage.ToString();
                                    mmm.tbMoonID.Text = conObject.Id.ToString();
                                    mmm.tbMoonisRes.Text = conObject.isResonant.ToString();
                                    mmm.tbMoonisTideLock.Text = conObject.isTideLocked.ToString();
                                    mmm.tbMoonMoonMass.Text = conObject.mass.ToString();
                                    mmm.tbMoonName.Text = conObject.name.ToString();
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
                if ((string)selectedtv.ToolTip == "OuterMoonlet Item" || (string)selectedtv.ToolTip == "InnerMoonlet Item")
                {
                    UserControls.ModifyMoonlet modm = new UserControls.ModifyMoonlet();
                    
                    Utility.OuterMoonlets conObjectOuter = null;
                    Utility.InnerMoonlets conObjectInner = null;
                    if ((string)selectedtv.ToolTip == "OuterMoonlet Item")
                    {
                        modm.tbIsOuter.Text = "true";
                        foreach (Utility.Stars star in LoadedSystem.Stars)
                        {
                            foreach (Utility.Planets planet in star.Planets)
                            {
                                foreach (Utility.OuterMoonlets outermoonlet in planet.OuterMoonlets1)
                                {
                                    if ((Int32)selectedtv.Tag == outermoonlet.Id)
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
                    if ((string)selectedtv.ToolTip == "InnerMoonlet Item")
                    {
                        modm.tbIsOuter.Text = "false";
                        foreach (Utility.Stars star in LoadedSystem.Stars)
                        {
                            foreach (Utility.Planets planet in star.Planets)
                            {
                                foreach (Utility.InnerMoonlets innermoonlet in planet.InnerMoonlets1)
                                {
                                    if ((Int32)selectedtv.Tag == innermoonlet.Id)
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
