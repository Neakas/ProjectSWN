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
        public CreatePlanets(StarSystem o, Dice d, SystemGeneration p)
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
        ///     A passed Dice object
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
        private void btnGenPlanets_Click(object sender, EventArgs e)
        {
            //save options
            OptionCont.MoreConGasGiantChances = ChkConGasGiant.IsChecked != null && (bool) ChkConGasGiant.IsChecked;
            OptionCont.noOceanOnlyGarden = OnlyGarden.IsChecked != null && (bool) OnlyGarden.IsChecked;
            OptionCont.moreAccurateO2Catastrophe = ChkMoreAccurateO2Catastrophe.IsChecked != null && (bool) ChkMoreAccurateO2Catastrophe.IsChecked;
            OptionCont.stableActivity = FrcStableActivity.IsChecked != null && (bool) FrcStableActivity.IsChecked;
            OptionCont.noMarginalAtm = NoMarginAtm.IsChecked != null && (bool) NoMarginAtm.IsChecked;
            OptionCont.highRVMVal = HighRvm.IsChecked != null && (bool) HighRvm.IsChecked;
            OptionCont.overrideHabitability = ChkHigherHabitability.IsChecked != null && (bool) ChkHigherHabitability.IsChecked;
            OptionCont.ignoreLunarTidesOnGardenWorlds = IgnoreTides.IsChecked != null && (bool) IgnoreTides.IsChecked;
            OptionCont.rerollAxialTiltOver45 = ChkKeepAxialTiltUnder45.IsChecked != null && (bool) ChkKeepAxialTiltUnder45.IsChecked;
            OptionCont.alwaysDisplayTidalData = ChkDisplayTidalData.IsChecked != null && (bool) ChkDisplayTidalData.IsChecked;
            OptionCont.expandAsteroidBelt = ChkExpandAsteroidBelt.IsChecked != null && (bool) ChkExpandAsteroidBelt.IsChecked;

            if (OverrideMoons.IsChecked == true) OptionCont.setNumberOfMoonsOverGarden(int.Parse(NumMoons.Text));
            if (OverridePressure.IsChecked == true) OptionCont.setAtmPressure = double.Parse(NumAtmPressure.Text);
            if (ChkOverrideTilt.IsChecked == true) OptionCont.setAxialTilt(int.Parse(NumTilt.Text));

            //set the moon option.
            if (BookHigh.IsChecked == true) OptionCont.moonOrbitFlag = OptionCont.MOON_BOOKHIGH;
            if (BookMoon.IsChecked == true) OptionCont.moonOrbitFlag = OptionCont.MOON_BOOK;
            if (ExtendHigh.IsChecked == true) OptionCont.moonOrbitFlag = OptionCont.MOON_EXPANDHIGH;
            if (ExtendNorm.IsChecked == true) OptionCont.moonOrbitFlag = OptionCont.MOON_EXPAND;

            //generate the planets!
            var totalOrbCount = 0; //total orbital count

            //first off, master loop. 
            foreach (var star in OurSystem.sysStars)
            {
                if (!star.testInitlizationZones())
                    star.initalizeZonesOfInterest();
                for (var i = 1; i < OurSystem.sysStars.Count; i++)
                {
                    Range temp;
                    if (OurSystem.sysStars[i].parentID == star.selfID)
                    {
                        temp = new Range(OurSystem.sysStars[i].getInnerForbiddenZone(),
                            OurSystem.sysStars[i].getOuterForbiddenZone());
                        star.createForbiddenZone(temp, star.selfID,
                            OurSystem.sysStars[i].selfID);
                    }
                    if (OurSystem.sysStars[i].selfID != star.selfID) continue;
                    temp = new Range(OurSystem.sysStars[i].getInnerForbiddenZone(),
                        OurSystem.sysStars[i].getOuterForbiddenZone());
                    star.createForbiddenZone(temp, star.parentID,
                        star.selfID);
                }

                star.sortForbidden();
                star.createCleanZones();
                var placeHolder = new Satellite(0, 0, 0, 0);
                int ownership;
                if (star.gasGiantFlag != Star.GASGIANT_NONE)
                {
                    double rangeAvail = 0, lowerBound = 0, diffRange = 0;
                    var spawnRange = new Range(0, 1);

                    //get range availability and spawn range

                    //CONVENTIONAL
                    if (star.gasGiantFlag == Star.GASGIANT_CONVENTIONAL)
                    {
                        rangeAvail = star.checkConRange();
                        lowerBound = Star.snowLine(star.initLumin)*1;
                        diffRange = Star.snowLine(star.initLumin)*1.5 - lowerBound;
                        spawnRange = star.getConventionalRange();
                    }

                    //ECCENTRIC
                    if (star.gasGiantFlag == Star.GASGIANT_ECCENTRIC)
                    {
                        rangeAvail = star.checkEccRange();
                        lowerBound = Star.snowLine(star.initLumin)*.125;
                        diffRange = Star.snowLine(star.initLumin)*.75 - lowerBound;
                        spawnRange = star.getEccentricRange();
                    }

                    //EPISTELLAR 
                    if (star.gasGiantFlag == Star.GASGIANT_EPISTELLAR)
                    {
                        rangeAvail = star.checkEpiRange();
                        lowerBound =
                            Star.innerRadius(star.initLumin,
                                star.initMass)*.1;
                        diffRange =
                            Star.innerRadius(star.initLumin,
                                star.initMass)*1.8 - lowerBound;
                        spawnRange = star.getEpistellarRange();
                    }

                    int roll;
                    double orbit;
                    if (rangeAvail >= .25)
                    {
                        do
                        {
                            orbit = VelvetBag.rollRange(lowerBound, diffRange);
                        } while (!star.verifyCleanOrbit(orbit));

                        ownership = star.getOwnership(orbit);

                        if (star.gasGiantFlag == Star.GASGIANT_EPISTELLAR)
                            ownership = star.selfID;

                        placeHolder = new Satellite(ownership, 0, orbit, 0, Satellite.BASETYPE_GASGIANT);

                        roll = VelvetBag.gurpsRoll() + 4;
                        libStarGen.updateGasGiantSize(placeHolder, roll);
                    }

                    if (rangeAvail >= .005 && rangeAvail < .25)
                    {
                        orbit = star.pickInRange(spawnRange);
                        ownership = star.getOwnership(orbit);
                        if (star.gasGiantFlag == Star.GASGIANT_EPISTELLAR)
                            ownership = star.selfID;

                        placeHolder = new Satellite(ownership, 0, orbit, 0, Satellite.BASETYPE_GASGIANT);

                        roll = VelvetBag.gurpsRoll() + 4;
                        libStarGen.updateGasGiantSize(placeHolder, roll);
                    }
                }

                //now we've determined our placeholdr, let's start working on our orbitals.

                double currOrbit = Star.innerRadius(star.initLumin,
                    star.initMass),
                    nextOrbit;
                const double distance = .15;

                //now we have our placeholder.
                if (Math.Abs(placeHolder.orbitalRadius) > 0)
                {
                    star.addSatellite(placeHolder);
                    currOrbit = placeHolder.orbitalRadius;
                }

                if (star.gasGiantFlag != Star.GASGIANT_EPISTELLAR &&
                    Math.Abs(placeHolder.orbitalRadius) > 0)
                {
                    //we're moving left.
                    //LEFT RIGHT LEFT
                    //.. sorry about that
                    var innerRadius = Star.innerRadius(star.initLumin,
                        star.initMass);
                    do
                    {
                        //as we're moving left, divide.
                        nextOrbit = currOrbit/libStarGen.getOrbitalRatio(VelvetBag);

                        if (nextOrbit > currOrbit - distance)
                            nextOrbit = currOrbit - distance;

                        if (star.verifyCleanOrbit(nextOrbit) &&
                            star.withinCreationRange(nextOrbit))
                        {
                            ownership = star.getOwnership(nextOrbit);
                            star.addSatellite(new Satellite(ownership, 0, nextOrbit, 0));
                        }

                        currOrbit = nextOrbit;

                        //now let's check on 
                    } while (currOrbit > innerRadius);
                }

                //MOVE RIGHT!
                //now we have our placeholder.
                if (star.gasGiantFlag == Star.GASGIANT_EPISTELLAR ||
                    Math.Abs(placeHolder.orbitalRadius) < 0)
                {
                    var outerRadius = Star.outerRadius(star.initMass);
                    do
                    {
                        //as we're moving right, multiply.
                        nextOrbit = currOrbit*libStarGen.getOrbitalRatio(VelvetBag);

                        if (nextOrbit < currOrbit + distance)
                            nextOrbit = currOrbit + distance;

                        if (star.verifyCleanOrbit(nextOrbit) &&
                            star.withinCreationRange(nextOrbit))
                        {
                            ownership = star.getOwnership(nextOrbit);
                            star.addSatellite(new Satellite(ownership, 0, nextOrbit, 0));
                        }

                        currOrbit = nextOrbit;

                        if (currOrbit < 0)
                            currOrbit = outerRadius + 10;
                        //now let's check on 
                    } while (currOrbit < outerRadius);
                }

                //if a clean zone has 0 planets, add one.
                foreach (var c in star.zonesOfInterest.formationZones)
                {
                    if (!star.cleanZoneHasOrbits(c))
                    {
                        nextOrbit = star.pickInRange(c.getRange());
                        ownership = star.getOwnership(nextOrbit);
                        star.addSatellite(new Satellite(ownership, 0, nextOrbit, 0));
                    }
                }

                //sort orbitals
                star.sortOrbitals();
                star.giveOrbitalsOrder(ref totalOrbCount);

                //now we get orbital contents, then fill in details
                libStarGen.populateOrbits(star, VelvetBag);

                //set any star with all empty orbits to have one planet
                if (star.isAllEmptyOrbits() && OptionCont.ensureOneOrbit)
                {
                    var newPlanet = VelvetBag.rng(1, star.sysPlanets.Count, -1);
                    star.sysPlanets[newPlanet].updateTypeSize(Satellite.BASETYPE_TERRESTIAL,
                        Satellite.SIZE_MEDIUM);
                }
            }

            foreach (var star in OurSystem.sysStars)
            {
                var distChart = libStarGen.genDistChart(OurSystem.sysStars);
                foreach (var sat in star.sysPlanets)
                {
                    sat.updateBlackBodyTemp(distChart, OurSystem.sysStars);
                }
                libStarGen.createPlanets(OurSystem, star.sysPlanets, VelvetBag);
            }

            OParent.createPlanetsFinished = true;
            Close(); //close the form
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnGenPlanets_Click(this, null);
            }
        }
    }
}