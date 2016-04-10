using System;
using System.Linq;
using System.Windows.Input;
using UniverseGeneration.Range_Objects;
using UniverseGeneration.Stellar_Bodies;
using UniverseGeneration.Utility;

namespace SWNAdmin.Forms
{
    /// <summary>
    ///     Interaction logic for CreateStars.xaml
    /// </summary>
    public partial class CreateStars
    {
        /// <summary>
        ///     Constructor object for the Create Stars
        /// </summary>
        /// <param name="s">Our StarSystem</param>
        /// <param name="d">The Ddice we use</param>
        /// <param name="p">System Generation</param>
        public CreateStars( StarSystem s, Dice d, SystemGeneration p )
        {
            VelvetBag = d;
            OurSystem = s;
            InitializeComponent();
            SystemParent = p;
        }

        public CreateStars( StarSystem s, Dice d )
        {
            VelvetBag = d;
            OurSystem = s;
            InitializeComponent();
        }

        /// <summary>
        ///     A passed StarSystem object (the one currently being used)
        /// </summary>
        public StarSystem OurSystem { get; set; }

        /// <summary>
        ///     A passed Ddice object
        /// </summary>
        public Dice VelvetBag { get; set; }

        /// <summary>
        ///     SystemParent object, used to pass to the main thing when we're done successfully.
        /// </summary>
        private SystemGeneration SystemParent { get; }

        /// <summary>
        ///     Saves set options to the Option Container and generates the stars. Then updates the datatable, and returns back to
        ///     the
        ///     main window.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event arguments</param>
        private void btnGenStars_Click( object sender, EventArgs e )
        {
            //save to OptionCont
            OptionCont.ForceGardenFavorable = ChkForceGarden.IsChecked != null && (bool) ChkForceGarden.IsChecked;
            OptionCont.InOpenCluster = ChkOpenCluster.IsChecked != null && (bool) ChkOpenCluster.IsChecked;
            OptionCont.SetVerboseOutput(ChkVerbose.IsChecked != null && (bool) ChkVerbose.IsChecked);
            OptionCont.EnsureOneOrbit = ChkForceOneOrbit.IsChecked != null && (bool) ChkForceOneOrbit.IsChecked;

            //set age, or clear age. 
            if (ChkAgeOverride.IsChecked == true)
            {
                OptionCont.SetSystemAge(double.Parse(NumAge.Text));
            }

            if (ChkAgeOverride.IsChecked == false)
            {
                OptionCont.SetSystemAge(-1.0);
            }

            //set stars, or clear stars
            if (ChkStarOverride.IsChecked == true)
            {
                OptionCont.SetNumberOfStars(int.Parse(NumStars.Text));
            }
            if (ChkStarOverride.IsChecked == false)
            {
                OptionCont.SetNumberOfStars(-1);
            }

            OptionCont.LessStellarEccent = ChkLesserEccentricity.IsChecked != null && (bool) ChkLesserEccentricity.IsChecked;
            OptionCont.ForceVeryLowStellarEccent = ChkExtLowStellar.IsChecked != null && (bool) ChkExtLowStellar.IsChecked;

            //set stellar mass options
            OptionCont.StellarMassRangeSet = ChkStellarMass.IsChecked != null && (bool) ChkStellarMass.IsChecked;
            OptionCont.MinStellarMass = double.Parse(NumMinMass.Text);
            OptionCont.MaxStellarMass = double.Parse(NumMaxMass.Text);

            OptionCont.FantasyColors = ChkFantasyColors.IsChecked != null && (bool) ChkFantasyColors.IsChecked;
            OptionCont.MoreFlareStarChance = ChkMoreFlare.IsChecked != null && (bool) ChkMoreFlare.IsChecked;
            OptionCont.AnyStarFlareStar = (bool) ChkAnyFlareStar.IsChecked;

            //now we start setting system parameters.
            OurSystem.SysName = TxtSysName.Text == "" ? LibStarGen.GenRandomSysName(OptionCont.SysNamePrefix, VelvetBag) : TxtSysName.Text;

            OurSystem.SysAge = LibStarGen.GenSystemAge(VelvetBag);

            //start creating and making stars.
            LibStarGen.CreateStars(VelvetBag, OurSystem);
            SystemParent.CreateStarsFinished = true;
            Close(); //close the form
        }

        private void Window_KeyDown( object sender, KeyEventArgs e )
        {
            if (e.Key == Key.Enter)
            {
                btnGenStars_Click(this, null);
            }
        }

        public StarSystem CreateNewSystem()
        {
            OurSystem.SysName = LibStarGen.GenRandomSysName(OptionCont.SysNamePrefix, VelvetBag);
            OurSystem.SysAge = LibStarGen.GenSystemAge(VelvetBag);
            LibStarGen.CreateStars(VelvetBag, OurSystem);
            //---------------------
            //generate the planets!
            var totalOrbCount = 0; //total orbital count

            //first off, master loop. 
            foreach (var star in OurSystem.SysStars)
            {
                //draw up forbidden zones.
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
                //gas giant flag
                //                LibStarGen.gasGiantFlag(this.OurSystem.sysStars[currStar], VelvetBag.gurpsRoll());

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
                foreach (var c in star.ZonesOfInterest.FormationZones.Where(c => !star.CleanZoneHasOrbits(c)))
                {
                    nextOrbit = star.PickInRange(c.GetRange());
                    ownership = star.GetOwnership(nextOrbit);
                    star.AddSatellite(new Satellite(ownership, 0, nextOrbit, 0));
                }

                //sort orbitals
                star.SortOrbitals();
                star.GiveOrbitalsOrder(ref totalOrbCount);

                //now we get orbital contents, then fill in details
                LibStarGen.PopulateOrbits(star, VelvetBag);

                //set any star with all empty orbits to have one planet
                if (OptionCont.EnsureOneOrbit != null && ( !star.IsAllEmptyOrbits() || !(bool) OptionCont.EnsureOneOrbit ))
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
            //-----------------------
            return OurSystem;
        }
    }
}