using System;
using System.Windows;
using System.Windows.Input;
using UniverseGeneration.Range_Objects;
using UniverseGeneration.Stellar_Bodies;
using UniverseGeneration.Utility;

namespace SWNAdmin.Forms
{
    /// <summary>
    ///     Interaction logic for CreateStars.xaml
    /// </summary>
    public partial class CreateStars : Window
    {
        /// <summary>
        ///     Constructor object for the Create Stars
        /// </summary>
        /// <param name="s">Our StarSystem</param>
        /// <param name="d">The dice we use</param>
        public CreateStars(StarSystem s, Dice d, SystemGeneration p)
        {
            velvetBag = d;
            ourSystem = s;
            InitializeComponent();
            parent = p;
        }

        public CreateStars(StarSystem s, Dice d)
        {
            velvetBag = d;
            ourSystem = s;
            InitializeComponent();
        }

        /// <summary>
        ///     A passed StarSystem object (the one currently being used)
        /// </summary>
        public StarSystem ourSystem { get; set; }

        /// <summary>
        ///     A passed Dice object
        /// </summary>
        public Dice velvetBag { get; set; }

        /// <summary>
        ///     Parent object, used to pass to the main thing when we're done successfully.
        /// </summary>
        private SystemGeneration parent { get; }

        /// <summary>
        ///     This function hides or shows the number of stars you wish to create. See <see cref="OptionCont.numStars" /> for
        ///     more details.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event arguments</param>
        private void numStarOverride_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStarOverride.IsChecked == true)
                numStars.Visibility = Visibility.Visible;

            if (chkStarOverride.IsChecked == false)
                numStars.Visibility = Visibility.Hidden;
        }

        /// <summary>
        ///     This function hides or shows the age of the system choice control. See <see cref="OptionCont.agePreset" /> for more
        ///     details.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event arguments</param>
        private void chkAgeOverride_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAgeOverride.IsChecked == true)
            {
                numAge.Visibility = Visibility.Visible;
                lblAgeYear.Visibility = Visibility.Visible;
            }

            if (chkAgeOverride.IsChecked == true)
            {
                numAge.Visibility = Visibility.Hidden;
                lblAgeYear.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        ///     This function hides or shows the stellar mass choice control. See <see cref="OptionCont.stellarMassRangeSet" />
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event arguments</param>
        private void chkStellarMass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStellarMass.IsChecked == true)
            {
                lblMass.Visibility = Visibility.Visible;
                lblMassB.Visibility = Visibility.Visible;
                numMinMass.Visibility = Visibility.Visible;
                numMaxMass.Visibility = Visibility.Visible;
            }

            if (chkStellarMass.IsChecked == false)
            {
                lblMass.Visibility = Visibility.Hidden;
                lblMassB.Visibility = Visibility.Hidden;
                numMinMass.Visibility = Visibility.Hidden;
                numMaxMass.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        ///     Saves set options to the Option Container and generates the stars. Then updates the datatable, and returns back to
        ///     the
        ///     main window.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event arguments</param>
        private void btnGenStars_Click(object sender, EventArgs e)
        {
            //save to OptionCont
            OptionCont.forceGardenFavorable = (bool) chkForceGarden.IsChecked;
            OptionCont.inOpenCluster = (bool) chkOpenCluster.IsChecked;
            OptionCont.setVerboseOutput((bool) chkVerbose.IsChecked);
            OptionCont.ensureOneOrbit = (bool) chkForceOneOrbit.IsChecked;

            //set age, or clear age. 
            if (chkAgeOverride.IsChecked == true)
                OptionCont.setSystemAge(double.Parse(numAge.Text));

            if (chkAgeOverride.IsChecked == false)
                OptionCont.setSystemAge(-1.0);

            //set stars, or clear stars
            if (chkStarOverride.IsChecked == true)
                OptionCont.setNumberOfStars(int.Parse(numStars.Text));
            if (chkStarOverride.IsChecked == false)
                OptionCont.setNumberOfStars(-1);

            OptionCont.lessStellarEccent = (bool) chkLesserEccentricity.IsChecked;
            OptionCont.forceVeryLowStellarEccent = (bool) chkExtLowStellar.IsChecked;

            //set stellar mass options
            OptionCont.stellarMassRangeSet = (bool) chkStellarMass.IsChecked;
            OptionCont.minStellarMass = double.Parse(numMinMass.Text);
            OptionCont.maxStellarMass = double.Parse(numMaxMass.Text);

            OptionCont.fantasyColors = (bool) chkFantasyColors.IsChecked;
            OptionCont.moreFlareStarChance = (bool) chkMoreFlare.IsChecked;
            OptionCont.anyStarFlareStar = (bool) chkAnyFlareStar.IsChecked;

            //now we start setting system parameters.
            if (txtSysName.Text == "")
                ourSystem.sysName = libStarGen.genRandomSysName(OptionCont.sysNamePrefix, velvetBag);
            else
                ourSystem.sysName = txtSysName.Text;

            ourSystem.sysAge = libStarGen.genSystemAge(velvetBag);

            //start creating and making stars.
            libStarGen.createStars(velvetBag, ourSystem);
            parent.createStarsFinished = true;
            Close(); //close the form
        }

        /// <summary>
        ///     Generates a random name.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event arguments</param>
        private void btnRandomName_Click(object sender, EventArgs e)
        {
            txtSysName.Text = libStarGen.genRandomSysName(OptionCont.sysNamePrefix, velvetBag);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnGenStars_Click(this, null);
            }
        }

        public StarSystem CreateNewSystem()
        {
            ourSystem.sysName = libStarGen.genRandomSysName(OptionCont.sysNamePrefix, velvetBag);
            ourSystem.sysAge = libStarGen.genSystemAge(velvetBag);
            libStarGen.createStars(velvetBag, ourSystem);
            //---------------------
            //generate the planets!
            var totalOrbCount = 0; //total orbital count

            //first off, master loop. 
            for (var currStar = 0; currStar < ourSystem.sysStars.Count; currStar++)
            {
                Range temp;
                //draw up forbidden zones.
                if (!ourSystem.sysStars[currStar].testInitlizationZones())
                    ourSystem.sysStars[currStar].initalizeZonesOfInterest();
                for (var i = 1; i < ourSystem.sysStars.Count; i++)
                {
                    if (ourSystem.sysStars[i].parentID == ourSystem.sysStars[currStar].selfID)
                    {
                        temp = new Range(ourSystem.sysStars[i].getInnerForbiddenZone(),
                            ourSystem.sysStars[i].getOuterForbiddenZone());
                        ourSystem.sysStars[currStar].createForbiddenZone(temp, ourSystem.sysStars[currStar].selfID,
                            ourSystem.sysStars[i].selfID);
                    }
                    if (ourSystem.sysStars[i].selfID == ourSystem.sysStars[currStar].selfID)
                    {
                        temp = new Range(ourSystem.sysStars[i].getInnerForbiddenZone(),
                            ourSystem.sysStars[i].getOuterForbiddenZone());
                        ourSystem.sysStars[currStar].createForbiddenZone(temp, ourSystem.sysStars[currStar].parentID,
                            ourSystem.sysStars[currStar].selfID);
                    }
                }

                ourSystem.sysStars[currStar].sortForbidden();
                ourSystem.sysStars[currStar].createCleanZones();
                //gas giant flag
                //                libStarGen.gasGiantFlag(this.ourSystem.sysStars[currStar], velvetBag.gurpsRoll());

                var placeHolder = new Satellite(0, 0, 0, 0);
                int ownership, roll;
                double orbit = 0;
                if (ourSystem.sysStars[currStar].gasGiantFlag != Star.GASGIANT_NONE)
                {
                    double rangeAvail = 0, lowerBound = 0, diffRange = 0;
                    var spawnRange = new Range(0, 1);

                    //get range availability and spawn range

                    //CONVENTIONAL
                    if (ourSystem.sysStars[currStar].gasGiantFlag == Star.GASGIANT_CONVENTIONAL)
                    {
                        rangeAvail = ourSystem.sysStars[currStar].checkConRange();
                        lowerBound = Star.snowLine(ourSystem.sysStars[currStar].initLumin)*1;
                        diffRange = Star.snowLine(ourSystem.sysStars[currStar].initLumin)*1.5 - lowerBound;
                        spawnRange = ourSystem.sysStars[currStar].getConventionalRange();
                    }

                    //ECCENTRIC
                    if (ourSystem.sysStars[currStar].gasGiantFlag == Star.GASGIANT_ECCENTRIC)
                    {
                        rangeAvail = ourSystem.sysStars[currStar].checkEccRange();
                        lowerBound = Star.snowLine(ourSystem.sysStars[currStar].initLumin)*.125;
                        diffRange = Star.snowLine(ourSystem.sysStars[currStar].initLumin)*.75 - lowerBound;
                        spawnRange = ourSystem.sysStars[currStar].getEccentricRange();
                    }

                    //EPISTELLAR 
                    if (ourSystem.sysStars[currStar].gasGiantFlag == Star.GASGIANT_EPISTELLAR)
                    {
                        rangeAvail = ourSystem.sysStars[currStar].checkEpiRange();
                        lowerBound =
                            Star.innerRadius(ourSystem.sysStars[currStar].initLumin,
                                ourSystem.sysStars[currStar].initMass)*.1;
                        diffRange =
                            Star.innerRadius(ourSystem.sysStars[currStar].initLumin,
                                ourSystem.sysStars[currStar].initMass)*1.8 - lowerBound;
                        spawnRange = ourSystem.sysStars[currStar].getEpistellarRange();
                    }

                    if (rangeAvail >= .25)
                    {
                        do
                        {
                            orbit = velvetBag.rollRange(lowerBound, diffRange);
                        } while (!ourSystem.sysStars[currStar].verifyCleanOrbit(orbit));

                        ownership = ourSystem.sysStars[currStar].getOwnership(orbit);

                        if (ourSystem.sysStars[currStar].gasGiantFlag == Star.GASGIANT_EPISTELLAR)
                            ownership = ourSystem.sysStars[currStar].selfID;

                        placeHolder = new Satellite(ownership, 0, orbit, 0, Satellite.BASETYPE_GASGIANT);

                        roll = velvetBag.gurpsRoll() + 4;
                        libStarGen.updateGasGiantSize(placeHolder, roll);
                    }

                    if (rangeAvail >= .005 && rangeAvail < .25)
                    {
                        orbit = ourSystem.sysStars[currStar].pickInRange(spawnRange);
                        ownership = ourSystem.sysStars[currStar].getOwnership(orbit);
                        if (ourSystem.sysStars[currStar].gasGiantFlag == Star.GASGIANT_EPISTELLAR)
                            ownership = ourSystem.sysStars[currStar].selfID;

                        placeHolder = new Satellite(ownership, 0, orbit, 0, Satellite.BASETYPE_GASGIANT);

                        roll = velvetBag.gurpsRoll() + 4;
                        libStarGen.updateGasGiantSize(placeHolder, roll);
                    }
                }

                //now we've determined our placeholdr, let's start working on our orbitals.

                double currOrbit = Star.innerRadius(ourSystem.sysStars[currStar].initLumin,
                    ourSystem.sysStars[currStar].initMass),
                    nextOrbit = 0;
                var distance = .15;

                //now we have our placeholder.
                if (placeHolder.orbitalRadius != 0)
                {
                    ourSystem.sysStars[currStar].addSatellite(placeHolder);
                    currOrbit = placeHolder.orbitalRadius;
                }

                if (ourSystem.sysStars[currStar].gasGiantFlag != Star.GASGIANT_EPISTELLAR &&
                    placeHolder.orbitalRadius != 0)
                {
                    //we're moving left.
                    //LEFT RIGHT LEFT
                    //.. sorry about that
                    var innerRadius = Star.innerRadius(ourSystem.sysStars[currStar].initLumin,
                        ourSystem.sysStars[currStar].initMass);
                    do
                    {
                        //as we're moving left, divide.
                        nextOrbit = currOrbit/libStarGen.getOrbitalRatio(velvetBag);

                        if (nextOrbit > currOrbit - distance)
                            nextOrbit = currOrbit - distance;

                        if (ourSystem.sysStars[currStar].verifyCleanOrbit(nextOrbit) &&
                            ourSystem.sysStars[currStar].withinCreationRange(nextOrbit))
                        {
                            ownership = ourSystem.sysStars[currStar].getOwnership(nextOrbit);
                            ourSystem.sysStars[currStar].addSatellite(new Satellite(ownership, 0, nextOrbit, 0));
                        }

                        currOrbit = nextOrbit;

                        //now let's check on 
                    } while (currOrbit > innerRadius);
                }

                //MOVE RIGHT!
                //now we have our placeholder.
                if (ourSystem.sysStars[currStar].gasGiantFlag == Star.GASGIANT_EPISTELLAR ||
                    placeHolder.orbitalRadius == 0)
                {
                    var outerRadius = Star.outerRadius(ourSystem.sysStars[currStar].initMass);
                    do
                    {
                        //as we're moving right, multiply.
                        nextOrbit = currOrbit*libStarGen.getOrbitalRatio(velvetBag);

                        if (nextOrbit < currOrbit + distance)
                            nextOrbit = currOrbit + distance;

                        if (ourSystem.sysStars[currStar].verifyCleanOrbit(nextOrbit) &&
                            ourSystem.sysStars[currStar].withinCreationRange(nextOrbit))
                        {
                            ownership = ourSystem.sysStars[currStar].getOwnership(nextOrbit);
                            ourSystem.sysStars[currStar].addSatellite(new Satellite(ownership, 0, nextOrbit, 0));
                        }

                        currOrbit = nextOrbit;

                        if (currOrbit < 0)
                            currOrbit = outerRadius + 10;
                        //now let's check on 
                    } while (currOrbit < outerRadius);
                }

                //if a clean zone has 0 planets, add one.
                foreach (var c in ourSystem.sysStars[currStar].zonesOfInterest.formationZones)
                {
                    if (!ourSystem.sysStars[currStar].cleanZoneHasOrbits(c))
                    {
                        nextOrbit = ourSystem.sysStars[currStar].pickInRange(c.getRange());
                        ownership = ourSystem.sysStars[currStar].getOwnership(nextOrbit);
                        ourSystem.sysStars[currStar].addSatellite(new Satellite(ownership, 0, nextOrbit, 0));
                    }
                }

                //sort orbitals
                ourSystem.sysStars[currStar].sortOrbitals();
                ourSystem.sysStars[currStar].giveOrbitalsOrder(ref totalOrbCount);

                //now we get orbital contents, then fill in details
                libStarGen.populateOrbits(ourSystem.sysStars[currStar], velvetBag);

                //set any star with all empty orbits to have one planet
                if (ourSystem.sysStars[currStar].isAllEmptyOrbits() && OptionCont.ensureOneOrbit)
                {
                    var newPlanet = velvetBag.rng(1, ourSystem.sysStars[currStar].sysPlanets.Count, -1);
                    ourSystem.sysStars[currStar].sysPlanets[newPlanet].updateTypeSize(Satellite.BASETYPE_TERRESTIAL,
                        Satellite.SIZE_MEDIUM);
                }
            }

            for (var currStar = 0; currStar < ourSystem.sysStars.Count; currStar++)
            {
                var distChart = libStarGen.genDistChart(ourSystem.sysStars);
                for (var i = 0; i < ourSystem.sysStars[currStar].sysPlanets.Count; i++)
                {
                    ourSystem.sysStars[currStar].sysPlanets[i].updateBlackBodyTemp(distChart, ourSystem.sysStars);
                }
                libStarGen.createPlanets(ourSystem, ourSystem.sysStars[currStar].sysPlanets, velvetBag);
            }
            //-----------------------
            return ourSystem;
        }
    }
}