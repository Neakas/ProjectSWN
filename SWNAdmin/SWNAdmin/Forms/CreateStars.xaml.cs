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
using System.Windows.Shapes;
using UniverseGeneration;

namespace SWNAdmin
{
    /// <summary>
    /// Interaction logic for CreateStars.xaml
    /// </summary>
    public partial class CreateStars : Window
    {
        /// <summary>
        /// A passed StarSystem object (the one currently being used)
        /// </summary>
        public StarSystem ourSystem { get; set; }

        /// <summary>
        /// A passed Dice object
        /// </summary>
        public Dice velvetBag { get; set; }

        /// <summary>
        /// Parent object, used to pass to the main thing when we're done successfully.
        /// </summary>
        private SystemGeneration parent { get; set; }

        /// <summary>
        /// Constructor object for the Create Stars 
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
        /// This function hides or shows the number of stars you wish to create. See <see cref="OptionCont.numStars"/> for more details.
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
        /// This function hides or shows the age of the system choice control. See <see cref="OptionCont.agePreset"/> for more details.
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
        /// This function hides or shows the stellar mass choice control. See <see cref="OptionCont.stellarMassRangeSet"/>
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
        /// Saves set options to the Option Container and generates the stars. Then updates the datatable, and returns back to the
        /// main window. 
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event arguments</param>
        private void btnGenStars_Click(object sender, EventArgs e)
        {
            //save to OptionCont
            OptionCont.forceGardenFavorable = (bool)chkForceGarden.IsChecked;
            OptionCont.inOpenCluster = (bool)chkOpenCluster.IsChecked;
            OptionCont.setVerboseOutput((bool)chkVerbose.IsChecked);
            OptionCont.ensureOneOrbit = (bool)chkForceOneOrbit.IsChecked;

            //set age, or clear age. 
            if (chkAgeOverride.IsChecked == true)
                OptionCont.setSystemAge(Double.Parse(numAge.Text));

            if (chkAgeOverride.IsChecked == false)
                OptionCont.setSystemAge(-1.0);

            //set stars, or clear stars
            if (chkStarOverride.IsChecked == true)
                OptionCont.setNumberOfStars(Int32.Parse(numStars.Text));
            if (chkStarOverride.IsChecked == false)
                OptionCont.setNumberOfStars(-1);

            OptionCont.lessStellarEccent = (bool)chkLesserEccentricity.IsChecked;
            OptionCont.forceVeryLowStellarEccent = (bool)chkExtLowStellar.IsChecked;

            //set stellar mass options
            OptionCont.stellarMassRangeSet = (bool)chkStellarMass.IsChecked;
            OptionCont.minStellarMass = Double.Parse(numMinMass.Text);
            OptionCont.maxStellarMass = Double.Parse(numMaxMass.Text);

            OptionCont.fantasyColors = (bool)chkFantasyColors.IsChecked;
            OptionCont.moreFlareStarChance = (bool)chkMoreFlare.IsChecked;
            OptionCont.anyStarFlareStar = (bool)chkAnyFlareStar.IsChecked;

            //now we start setting system parameters.
            if (txtSysName.Text == "")
                this.ourSystem.sysName = libStarGen.genRandomSysName(OptionCont.sysNamePrefix, velvetBag);
            else
                this.ourSystem.sysName = txtSysName.Text;

            this.ourSystem.sysAge = libStarGen.genSystemAge(velvetBag);

            //start creating and making stars.
            libStarGen.createStars(velvetBag, ourSystem);
            parent.createStarsFinished = true;
            this.Close(); //close the form

        }

        /// <summary>
        /// Generates a random name.
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
            this.ourSystem.sysName = libStarGen.genRandomSysName(OptionCont.sysNamePrefix, velvetBag);
            this.ourSystem.sysAge = libStarGen.genSystemAge(velvetBag);
            libStarGen.createStars(velvetBag, ourSystem);
            //---------------------
            //generate the planets!
            int totalOrbCount = 0; //total orbital count

            //first off, master loop. 
            for (int currStar = 0; currStar < this.ourSystem.sysStars.Count; currStar++)
            {
                Range temp;
                //draw up forbidden zones.
                if (!this.ourSystem.sysStars[currStar].testInitlizationZones()) this.ourSystem.sysStars[currStar].initalizeZonesOfInterest();
                for (int i = 1; i < this.ourSystem.sysStars.Count; i++)
                {
                    if (this.ourSystem.sysStars[i].parentID == this.ourSystem.sysStars[currStar].selfID)
                    {
                        temp = new Range(this.ourSystem.sysStars[i].getInnerForbiddenZone(), this.ourSystem.sysStars[i].getOuterForbiddenZone());
                        this.ourSystem.sysStars[currStar].createForbiddenZone(temp, this.ourSystem.sysStars[currStar].selfID, this.ourSystem.sysStars[i].selfID);
                    }
                    if (this.ourSystem.sysStars[i].selfID == this.ourSystem.sysStars[currStar].selfID)
                    {
                        temp = new Range(this.ourSystem.sysStars[i].getInnerForbiddenZone(), this.ourSystem.sysStars[i].getOuterForbiddenZone());
                        this.ourSystem.sysStars[currStar].createForbiddenZone(temp, this.ourSystem.sysStars[currStar].parentID, this.ourSystem.sysStars[currStar].selfID);
                    }
                }

                this.ourSystem.sysStars[currStar].sortForbidden();
                this.ourSystem.sysStars[currStar].createCleanZones();
                //gas giant flag
                //                libStarGen.gasGiantFlag(this.ourSystem.sysStars[currStar], velvetBag.gurpsRoll());

                Satellite placeHolder = new Satellite(0, 0, 0, 0);
                int ownership, roll;
                double orbit = 0;
                if (this.ourSystem.sysStars[currStar].gasGiantFlag != Star.GASGIANT_NONE)
                {
                    double rangeAvail = 0, lowerBound = 0, diffRange = 0;
                    Range spawnRange = new Range(0, 1);

                    //get range availability and spawn range

                    //CONVENTIONAL
                    if (this.ourSystem.sysStars[currStar].gasGiantFlag == Star.GASGIANT_CONVENTIONAL)
                    {
                        rangeAvail = this.ourSystem.sysStars[currStar].checkConRange();
                        lowerBound = Star.snowLine(this.ourSystem.sysStars[currStar].initLumin) * 1;
                        diffRange = (Star.snowLine(this.ourSystem.sysStars[currStar].initLumin) * 1.5) - lowerBound;
                        spawnRange = this.ourSystem.sysStars[currStar].getConventionalRange();
                    }

                    //ECCENTRIC
                    if (this.ourSystem.sysStars[currStar].gasGiantFlag == Star.GASGIANT_ECCENTRIC)
                    {
                        rangeAvail = this.ourSystem.sysStars[currStar].checkEccRange();
                        lowerBound = Star.snowLine(this.ourSystem.sysStars[currStar].initLumin) * .125;
                        diffRange = (Star.snowLine(this.ourSystem.sysStars[currStar].initLumin) * .75) - lowerBound;
                        spawnRange = this.ourSystem.sysStars[currStar].getEccentricRange();
                    }

                    //EPISTELLAR 
                    if (this.ourSystem.sysStars[currStar].gasGiantFlag == Star.GASGIANT_EPISTELLAR)
                    {
                        rangeAvail = this.ourSystem.sysStars[currStar].checkEpiRange();
                        lowerBound = Star.innerRadius(this.ourSystem.sysStars[currStar].initLumin, this.ourSystem.sysStars[currStar].initMass) * .1;
                        diffRange = (Star.innerRadius(this.ourSystem.sysStars[currStar].initLumin, this.ourSystem.sysStars[currStar].initMass) * 1.8) - lowerBound;
                        spawnRange = this.ourSystem.sysStars[currStar].getEpistellarRange();
                    }

                    if (rangeAvail >= .25)
                    {
                        do
                        {
                            orbit = velvetBag.rollRange(lowerBound, diffRange);
                        } while (!this.ourSystem.sysStars[currStar].verifyCleanOrbit(orbit));

                        ownership = this.ourSystem.sysStars[currStar].getOwnership(orbit);

                        if (this.ourSystem.sysStars[currStar].gasGiantFlag == Star.GASGIANT_EPISTELLAR)
                            ownership = this.ourSystem.sysStars[currStar].selfID;

                        placeHolder = new Satellite(ownership, 0, orbit, 0, Satellite.BASETYPE_GASGIANT);

                        roll = velvetBag.gurpsRoll() + 4;
                        libStarGen.updateGasGiantSize(placeHolder, roll);
                    }

                    if (rangeAvail >= .005 && rangeAvail < .25)
                    {
                        orbit = this.ourSystem.sysStars[currStar].pickInRange(spawnRange);
                        ownership = this.ourSystem.sysStars[currStar].getOwnership(orbit);
                        if (this.ourSystem.sysStars[currStar].gasGiantFlag == Star.GASGIANT_EPISTELLAR)
                            ownership = this.ourSystem.sysStars[currStar].selfID;

                        placeHolder = new Satellite(ownership, 0, orbit, 0, Satellite.BASETYPE_GASGIANT);

                        roll = velvetBag.gurpsRoll() + 4;
                        libStarGen.updateGasGiantSize(placeHolder, roll);
                    }
                }

                //now we've determined our placeholdr, let's start working on our orbitals.

                double currOrbit = Star.innerRadius(this.ourSystem.sysStars[currStar].initLumin, this.ourSystem.sysStars[currStar].initMass), nextOrbit = 0;
                double distance = .15;

                //now we have our placeholder.
                if (placeHolder.orbitalRadius != 0)
                {
                    this.ourSystem.sysStars[currStar].addSatellite(placeHolder);
                    currOrbit = placeHolder.orbitalRadius;
                }

                if (this.ourSystem.sysStars[currStar].gasGiantFlag != Star.GASGIANT_EPISTELLAR && placeHolder.orbitalRadius != 0)
                {
                    //we're moving left.
                    //LEFT RIGHT LEFT
                    //.. sorry about that
                    double innerRadius = Star.innerRadius(this.ourSystem.sysStars[currStar].initLumin, this.ourSystem.sysStars[currStar].initMass);
                    do
                    {
                        //as we're moving left, divide.
                        nextOrbit = currOrbit / libStarGen.getOrbitalRatio(velvetBag);

                        if (nextOrbit > currOrbit - distance)
                            nextOrbit = currOrbit - distance;

                        if (this.ourSystem.sysStars[currStar].verifyCleanOrbit(nextOrbit) && this.ourSystem.sysStars[currStar].withinCreationRange(nextOrbit))
                        {
                            ownership = this.ourSystem.sysStars[currStar].getOwnership(nextOrbit);
                            this.ourSystem.sysStars[currStar].addSatellite(new Satellite(ownership, 0, nextOrbit, 0));
                        }

                        currOrbit = nextOrbit;

                        //now let's check on 
                    } while (currOrbit > innerRadius);

                }

                //MOVE RIGHT!
                //now we have our placeholder.
                if (this.ourSystem.sysStars[currStar].gasGiantFlag == Star.GASGIANT_EPISTELLAR || placeHolder.orbitalRadius == 0)
                {
                    double outerRadius = Star.outerRadius(this.ourSystem.sysStars[currStar].initMass);
                    do
                    {
                        //as we're moving right, multiply.
                        nextOrbit = currOrbit * libStarGen.getOrbitalRatio(velvetBag);

                        if (nextOrbit < currOrbit + distance)
                            nextOrbit = currOrbit + distance;

                        if (this.ourSystem.sysStars[currStar].verifyCleanOrbit(nextOrbit) && this.ourSystem.sysStars[currStar].withinCreationRange(nextOrbit))
                        {
                            ownership = this.ourSystem.sysStars[currStar].getOwnership(nextOrbit);
                            this.ourSystem.sysStars[currStar].addSatellite(new Satellite(ownership, 0, nextOrbit, 0));
                        }

                        currOrbit = nextOrbit;

                        if (currOrbit < 0)
                            currOrbit = outerRadius + 10;
                        //now let's check on 
                    } while (currOrbit < outerRadius);
                }

                //if a clean zone has 0 planets, add one.
                foreach (cleanZone c in this.ourSystem.sysStars[currStar].zonesOfInterest.formationZones)
                {
                    if (!this.ourSystem.sysStars[currStar].cleanZoneHasOrbits(c))
                    {
                        nextOrbit = this.ourSystem.sysStars[currStar].pickInRange(c.getRange());
                        ownership = this.ourSystem.sysStars[currStar].getOwnership(nextOrbit);
                        this.ourSystem.sysStars[currStar].addSatellite(new Satellite(ownership, 0, nextOrbit, 0));
                    }
                }

                //sort orbitals
                this.ourSystem.sysStars[currStar].sortOrbitals();
                this.ourSystem.sysStars[currStar].giveOrbitalsOrder(ref totalOrbCount);

                //now we get orbital contents, then fill in details
                libStarGen.populateOrbits(this.ourSystem.sysStars[currStar], velvetBag);

                //set any star with all empty orbits to have one planet
                if (this.ourSystem.sysStars[currStar].isAllEmptyOrbits() && OptionCont.ensureOneOrbit)
                {
                    int newPlanet = velvetBag.rng(1, this.ourSystem.sysStars[currStar].sysPlanets.Count, -1);
                    this.ourSystem.sysStars[currStar].sysPlanets[newPlanet].updateTypeSize(Satellite.BASETYPE_TERRESTIAL, Satellite.SIZE_MEDIUM);
                }
            }

            for (int currStar = 0; currStar < this.ourSystem.sysStars.Count; currStar++)
            {
                double[,] distChart = libStarGen.genDistChart(this.ourSystem.sysStars);
                for (int i = 0; i < this.ourSystem.sysStars[currStar].sysPlanets.Count; i++)
                {
                    this.ourSystem.sysStars[currStar].sysPlanets[i].updateBlackBodyTemp(distChart, this.ourSystem.sysStars);
                }
                libStarGen.createPlanets(this.ourSystem, this.ourSystem.sysStars[currStar].sysPlanets, velvetBag);
            }
            //-----------------------
            return ourSystem;
        }
    }
}