using System;
using System.Windows.Input;
using UniverseGeneration.Range_Objects;
using UniverseGeneration.Stellar_Bodies;
using UniverseGeneration.Utility;

namespace SWNAdmin.Forms
{
    /// <summary>
    ///     Interaction logic for CreatePlanets.xaml
    /// </summary>
    public partial class CreatePlanets
    {
        /// <summary>
        ///     The constructor object for this form
        /// </summary>
        public CreatePlanets( StarSystem o, Dice d, SystemGeneration p )
        {
            OParent = p;
            VelvetBag = d;
            OurSystem = o;

            InitializeComponent();
        }

        /// <summary>
        ///     A passed StarSystem object (the one currently being used)
        /// </summary>
        private StarSystem OurSystem { get; }

        /// <summary>
        ///     A passed Ddice object
        /// </summary>
        private Dice VelvetBag { get; }

        /// <summary>
        ///     OParent object, used to pass to the main thing when we're done successfully.
        /// </summary>
        private SystemGeneration OParent { get; }

        /// <summary>
        ///     Sends the completed status and begins generating the planets
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">EventArgs object</param>
        private void btnGenPlanets_Click( object sender, EventArgs e )
        {
            //save options
            OptionCont.MoreConGasGiantChances = ChkConGasGiant.IsChecked != null && (bool) ChkConGasGiant.IsChecked;
            OptionCont.NoOceanOnlyGarden = OnlyGarden.IsChecked != null && (bool) OnlyGarden.IsChecked;
            OptionCont.MoreAccurateO2Catastrophe = ChkMoreAccurateO2Catastrophe.IsChecked != null && (bool) ChkMoreAccurateO2Catastrophe.IsChecked;
            OptionCont.StableActivity = FrcStableActivity.IsChecked != null && (bool) FrcStableActivity.IsChecked;
            OptionCont.NoMarginalAtm = NoMarginAtm.IsChecked != null && (bool) NoMarginAtm.IsChecked;
            OptionCont.HighRvmVal = HighRvm.IsChecked != null && (bool) HighRvm.IsChecked;
            OptionCont.OverrideHabitability = ChkHigherHabitability.IsChecked != null && (bool) ChkHigherHabitability.IsChecked;
            OptionCont.IgnoreLunarTidesOnGardenWorlds = IgnoreTides.IsChecked != null && (bool) IgnoreTides.IsChecked;
            OptionCont.RerollAxialTiltOver45 = ChkKeepAxialTiltUnder45.IsChecked != null && (bool) ChkKeepAxialTiltUnder45.IsChecked;
            OptionCont.AlwaysDisplayTidalData = ChkDisplayTidalData.IsChecked != null && (bool) ChkDisplayTidalData.IsChecked;
            OptionCont.ExpandAsteroidBelt = ChkExpandAsteroidBelt.IsChecked != null && (bool) ChkExpandAsteroidBelt.IsChecked;

            if (OverrideMoons.IsChecked == true)
            {
                OptionCont.SetNumberOfMoonsOverGarden(int.Parse(NumMoons.Text));
            }
            if (OverridePressure.IsChecked == true)
            {
                OptionCont.SetAtmPressure = double.Parse(NumAtmPressure.Text);
            }
            if (ChkOverrideTilt.IsChecked == true)
            {
                OptionCont.SetAxialTilt(int.Parse(NumTilt.Text));
            }

            //set the moon option.
            if (BookHigh.IsChecked == true)
            {
                OptionCont.MoonOrbitFlag = OptionCont.MoonBookhigh;
            }
            if (BookMoon.IsChecked == true)
            {
                OptionCont.MoonOrbitFlag = OptionCont.MoonBook;
            }
            if (ExtendHigh.IsChecked == true)
            {
                OptionCont.MoonOrbitFlag = OptionCont.MoonExpandhigh;
            }
            if (ExtendNorm.IsChecked == true)
            {
                OptionCont.MoonOrbitFlag = OptionCont.MoonExpand;
            }

            //generate the planets!
            var totalOrbCount = 0; //total orbital count

            //first off, master loop. 
            foreach (var star in OurSystem.SysStars)
            {
                if (!star.TestInitlizationZones())
                {
                    star.InitalizeZonesOfInterest();
                }
                for (var i = 1; i < OurSystem.SysStars.Count; i++)
                {
                    Range temp;
                    if (OurSystem.SysStars[i].ParentId == star.SelfId)
                    {
                        temp = new Range(OurSystem.SysStars[i].GetInnerForbiddenZone(), OurSystem.SysStars[i].GetOuterForbiddenZone());
                        star.CreateForbiddenZone(temp, star.SelfId, OurSystem.SysStars[i].SelfId);
                    }
                    if (OurSystem.SysStars[i].SelfId != star.SelfId)
                    {
                        continue;
                    }
                    temp = new Range(OurSystem.SysStars[i].GetInnerForbiddenZone(), OurSystem.SysStars[i].GetOuterForbiddenZone());
                    star.CreateForbiddenZone(temp, star.ParentId, star.SelfId);
                }

                star.SortForbidden();
                star.CreateCleanZones();
                var placeHolder = new Satellite(0, 0, 0, 0);
                int ownership;
                if (star.GasGiantFlag != Star.GasgiantNone)
                {
                    double rangeAvail = 0, lowerBound = 0, diffRange = 0;
                    var spawnRange = new Range(0, 1);

                    //get range availability and spawn range

                    //CONVENTIONAL
                    if (star.GasGiantFlag == Star.GasgiantConventional)
                    {
                        rangeAvail = star.CheckConRange();
                        lowerBound = Star.SnowLine(star.InitLumin) * 1;
                        diffRange = Star.SnowLine(star.InitLumin) * 1.5 - lowerBound;
                        spawnRange = star.GetConventionalRange();
                    }

                    //ECCENTRIC
                    if (star.GasGiantFlag == Star.GasgiantEccentric)
                    {
                        rangeAvail = star.CheckEccRange();
                        lowerBound = Star.SnowLine(star.InitLumin) * .125;
                        diffRange = Star.SnowLine(star.InitLumin) * .75 - lowerBound;
                        spawnRange = star.GetEccentricRange();
                    }

                    //EPISTELLAR 
                    if (star.GasGiantFlag == Star.GasgiantEpistellar)
                    {
                        rangeAvail = star.CheckEpiRange();
                        lowerBound = Star.InnerRadius(star.InitLumin, star.InitMass) * .1;
                        diffRange = Star.InnerRadius(star.InitLumin, star.InitMass) * 1.8 - lowerBound;
                        spawnRange = star.GetEpistellarRange();
                    }

                    int roll;
                    double orbit;
                    if (rangeAvail >= .25)
                    {
                        do
                        {
                            orbit = VelvetBag.RollRange(lowerBound, diffRange);
                        }
                        while (!star.VerifyCleanOrbit(orbit));

                        ownership = star.GetOwnership(orbit);

                        if (star.GasGiantFlag == Star.GasgiantEpistellar)
                        {
                            ownership = star.SelfId;
                        }

                        placeHolder = new Satellite(ownership, 0, orbit, 0, Satellite.BasetypeGasgiant);

                        roll = VelvetBag.GurpsRoll() + 4;
                        LibStarGen.UpdateGasGiantSize(placeHolder, roll);
                    }

                    if (rangeAvail >= .005 && rangeAvail < .25)
                    {
                        orbit = star.PickInRange(spawnRange);
                        ownership = star.GetOwnership(orbit);
                        if (star.GasGiantFlag == Star.GasgiantEpistellar)
                        {
                            ownership = star.SelfId;
                        }

                        placeHolder = new Satellite(ownership, 0, orbit, 0, Satellite.BasetypeGasgiant);

                        roll = VelvetBag.GurpsRoll() + 4;
                        LibStarGen.UpdateGasGiantSize(placeHolder, roll);
                    }
                }

                //now we've determined our placeholdr, let's start working on our orbitals.

                double currOrbit = Star.InnerRadius(star.InitLumin, star.InitMass), nextOrbit;
                const double distance = .15;

                //now we have our placeholder.
                if (Math.Abs(placeHolder.OrbitalRadius) > 0)
                {
                    star.AddSatellite(placeHolder);
                    currOrbit = placeHolder.OrbitalRadius;
                }

                if (star.GasGiantFlag != Star.GasgiantEpistellar && Math.Abs(placeHolder.OrbitalRadius) > 0)
                {
                    //we're moving left.
                    //LEFT RIGHT LEFT
                    //.. sorry about that
                    var innerRadius = Star.InnerRadius(star.InitLumin, star.InitMass);
                    do
                    {
                        //as we're moving left, divide.
                        nextOrbit = currOrbit / LibStarGen.GetOrbitalRatio(VelvetBag);

                        if (nextOrbit > currOrbit - distance)
                        {
                            nextOrbit = currOrbit - distance;
                        }

                        if (star.VerifyCleanOrbit(nextOrbit) && star.WithinCreationRange(nextOrbit))
                        {
                            ownership = star.GetOwnership(nextOrbit);
                            star.AddSatellite(new Satellite(ownership, 0, nextOrbit, 0));
                        }

                        currOrbit = nextOrbit;

                        //now let's check on 
                    }
                    while (currOrbit > innerRadius);
                }

                //MOVE RIGHT!
                //now we have our placeholder.
                if (star.GasGiantFlag == Star.GasgiantEpistellar || Math.Abs(placeHolder.OrbitalRadius) < 0)
                {
                    var outerRadius = Star.OuterRadius(star.InitMass);
                    do
                    {
                        //as we're moving right, multiply.
                        nextOrbit = currOrbit * LibStarGen.GetOrbitalRatio(VelvetBag);

                        if (nextOrbit < currOrbit + distance)
                        {
                            nextOrbit = currOrbit + distance;
                        }

                        if (star.VerifyCleanOrbit(nextOrbit) && star.WithinCreationRange(nextOrbit))
                        {
                            ownership = star.GetOwnership(nextOrbit);
                            star.AddSatellite(new Satellite(ownership, 0, nextOrbit, 0));
                        }

                        currOrbit = nextOrbit;

                        if (currOrbit < 0)
                        {
                            currOrbit = outerRadius + 10;
                        }
                        //now let's check on 
                    }
                    while (currOrbit < outerRadius);
                }

                //if a clean zone has 0 planets, add one.
                foreach (var c in star.ZonesOfInterest.FormationZones)
                {
                    if (!star.CleanZoneHasOrbits(c))
                    {
                        nextOrbit = star.PickInRange(c.GetRange());
                        ownership = star.GetOwnership(nextOrbit);
                        star.AddSatellite(new Satellite(ownership, 0, nextOrbit, 0));
                    }
                }

                //sort orbitals
                star.SortOrbitals();
                star.GiveOrbitalsOrder(ref totalOrbCount);

                //now we get orbital contents, then fill in details
                LibStarGen.PopulateOrbits(star, VelvetBag);

                //set any star with all empty orbits to have one planet
                if (!star.IsAllEmptyOrbits() || !(bool) OptionCont.EnsureOneOrbit)
                {
                    continue;
                }
                var newPlanet = VelvetBag.Rng(1, star.SysPlanets.Count, -1);
                star.SysPlanets[newPlanet].UpdateTypeSize(Satellite.BasetypeTerrestial, Satellite.SizeMedium);
            }

            foreach (var star in OurSystem.SysStars)
            {
                var distChart = LibStarGen.GenDistChart(OurSystem.SysStars);
                foreach (var sat in star.SysPlanets)
                {
                    sat.UpdateBlackBodyTemp(distChart, OurSystem.SysStars);
                }
                LibStarGen.CreatePlanets(OurSystem, star.SysPlanets, VelvetBag);
            }

            OParent.CreatePlanetsFinished = true;
            Close(); //close the form
        }

        private void Window_KeyDown( object sender, KeyEventArgs e )
        {
            if (e.Key == Key.Enter)
            {
                btnGenPlanets_Click(this, null);
            }
        }
    }
}