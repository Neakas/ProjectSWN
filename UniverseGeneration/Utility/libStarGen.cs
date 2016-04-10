using System;
using System.Collections.Generic;
using System.Linq;
using UniverseGeneration.Range_Objects;
using UniverseGeneration.Stellar_Bodies;

namespace UniverseGeneration.Utility
{
    /// <summary>
    ///     This contains a series of helper functions to generate a Star System.
    /// </summary>
    public static class LibStarGen
    {
        /// <summary>
        ///     This generates a random name for a star system
        /// </summary>
        /// <param name="prefix">The prefix for the generator</param>
        /// <param name="ourDice">Ddice used in rolling</param>
        /// <returns>A random name for a star system</returns>
        public static string GenRandomSysName( string prefix, Dice ourDice )
        {
            return prefix + Math.Round(ourDice.RollRange(0, 1) * 1000000000, 0);
        }

        /// <summary>
        ///     This function generates a random age per GURPS Space 4e rules.
        /// </summary>
        /// <param name="ourDice">The Ddice this rolls</param>
        /// <returns>The system age</returns>
        public static double GenSystemAge( Dice ourDice )
        {
            //get first roll
            var roll = ourDice.GurpsRoll();

            if (OptionCont.GetSystemAge() != -1)
            {
                return OptionCont.GetSystemAge();
            }

            if (roll == 3)
            {
                return 0.01;
            }
            if (roll >= 4 && roll <= 6)
            {
                return .1 + ourDice.Rng(1, 6, -1) * .3 + ourDice.Rng(1, 6, -1) * .05;
            }
            if (roll >= 7 && roll <= 10)
            {
                return 2 + ourDice.Rng(1, 6, -1) * .6 + ourDice.Rng(1, 6, -1) * .1;
            }
            if (roll >= 11 && roll <= 14)
            {
                return 5.6 + ourDice.Rng(1, 6, -1) * .6 + ourDice.Rng(1, 6, -1) * .1;
            }
            if (roll >= 15 && roll <= 17)
            {
                return 8 + ourDice.Rng(1, 6, -1) * .6 + ourDice.Rng(1, 6, -1) * .1;
            }
            if (roll == 18)
            {
                return 10 + ourDice.Rng(1, 6, -1) * .6 + ourDice.Rng(1, 6, -1) * .1;
            }

            return 13.8;
        }

        /// <summary>
        ///     This function generates and populates our stars.
        /// </summary>
        /// <param name="ourBag">The Ddice object used for our PRNG</param>
        /// <param name="ourSystem">The solar system we are creating stars for</param>
        public static void CreateStars( Dice ourBag, StarSystem ourSystem )
        {
            var numStars = 0;

            //determine the number of stars
            if (OptionCont.GetNumberOfStars() != -1)
            {
                numStars = OptionCont.GetNumberOfStars();
            }
            else
            {
                // We take the roll, add 2 if it's in an open cluster,subtract 1 if not, then divide it by 5.
                // This matches the roll probablity to the table.
                numStars = (int) Math.Floor(( ourBag.GurpsRoll() + ( OptionCont.InOpenCluster ? 2 : -1 ) ) / 5.0);

                if (numStars < 1)
                {
                    numStars = 1;
                }
                if (numStars > 3)
                {
                    numStars = 3;
                }
            }

            //creating the stars.
            for (var i = 0; i < numStars; i++)
            {
                if (i == 0)
                {
                    ourSystem.AddStar(Star.IsPrimary, Star.IsPrimary, i);

                    //manually set the first star's mass and push it to the max mass setting
                    ourSystem.SysStars[0].UpdateMass(RollStellarMass(ourBag, Star.IsPrimary));
                    ourSystem.MaxMass = ourSystem.SysStars[0].CurrMass;

                    //generate the star
                    GenerateAStar(ourSystem.SysStars[i], ourBag, ourSystem.MaxMass, ourSystem.SysName);
                }
                if (i == 1)
                {
                    ourSystem.AddStar(Star.IsSecondary, Star.IsPrimary, i);
                    //generate the star
                    GenerateAStar(ourSystem.SysStars[i], ourBag, ourSystem.MaxMass, ourSystem.SysName);
                }
                if (i == 2)
                {
                    ourSystem.AddStar(Star.IsTrinary, Star.IsPrimary, i);
                    //generate the star
                    GenerateAStar(ourSystem.SysStars[i], ourBag, ourSystem.MaxMass, ourSystem.SysName);
                }

                GasGiantFlag(ourSystem.SysStars[i], ourBag.GurpsRoll());
            }

            //now generate orbitals
            if (ourSystem.CountStars() > 1)
            {
                PlaceOurStars(ourSystem, ourBag);
            }
        }

        /// <summary>
        ///     This function rolls for mass on a star.
        /// </summary>
        /// <param name="velvetBag">The Ddice object</param>
        /// <param name="orderId">The order ID of the star</param>
        /// <param name="maxMass">the maximum mass. Has a default value of 0.0, indicating no max mass (may be left out)</param>
        /// <returns>The rolled mass of a star</returns>
        public static double RollStellarMass( Dice velvetBag, int orderId, double maxMass = 0.0 )
        {
            int rollA; //roll integers

            if (Math.Abs(maxMass) < 0.0)
            {
                if (!(bool) !OptionCont.StellarMassRangeSet)
                {
                    return velvetBag.RollInRange(OptionCont.MinStellarMass, OptionCont.MaxStellarMass);
                }
                if (OptionCont.ForceGardenFavorable != null && ( orderId != Star.IsPrimary || !(bool) OptionCont.ForceGardenFavorable ))
                {
                    return Star.GetMassByRoll(velvetBag.GurpsRoll(), velvetBag.GurpsRoll());
                }
                rollA = velvetBag.Rng(6);
                if (rollA == 1)
                {
                    rollA = 5;
                }
                if (rollA == 2)
                {
                    rollA = 6;
                }
                if (rollA == 3 || rollA == 4)
                {
                    rollA = 7;
                }
                if (rollA == 5 || rollA == 6)
                {
                    rollA = 8;
                }

                return Star.GetMassByRoll(rollA, velvetBag.GurpsRoll());
            }

            var currPos = Star.GetStellarMassPos(maxMass);

            //error bound checking. The entire program is kinda predicated aroudn the idea you won't have this happen.
            //IF IT DOES, then do the simple method.
            if (currPos == -1)
            {
                double tmpRoll; //test value.
                do
                {
                    tmpRoll = Star.GetMassByRoll(velvetBag.GurpsRoll(), velvetBag.GurpsRoll());
                }
                while (tmpRoll > maxMass);

                return tmpRoll;
            }

            //else, roll for the new index.
            rollA = velvetBag.GurpsRoll();
            var rollB = velvetBag.Rng(rollA, 6); //roll integers

            //get the new index
            if (currPos - rollB <= 0)
            {
                currPos = 0;
            }
            else
            {
                currPos = currPos - rollB;
            }

            return Star.GetMassByIndex(currPos);
        }

        /// <summary>
        ///     This function fills in the details of the star.
        /// </summary>
        /// <param name="s">The star to be filled in</param>
        /// <param name="ourDice">The Ddice object used</param>
        /// <param name="maxMass">Max mass of the system</param>
        /// <param name="sysName">The name of the system</param>
        public static void GenerateAStar( Star s, Dice ourDice, double maxMass, string sysName )
        {
            //check mass first - if unset, set it.
            if (Math.Abs(s.CurrMass) < 0)
            {
                s.UpdateMass(s.OrderId == Star.IsPrimary ? RollStellarMass(ourDice, s.OrderId) : RollStellarMass(ourDice, s.OrderId, maxMass));
            }

            //if we are in the white dwarf branch, reroll mass.
            if (s.EvoLine.FindCurrentAgeGroup(s.StarAge) == StarAgeLine.RetCollaspedstar)
            {
                s.UpdateMass(ourDice.RollInRange(0.9, 1.4), true);
            }

            //set the generic name
            s.Name = Star.GenGenericName(sysName, s.OrderId);

            //initalize the luminosity first, then update it given the current age, status and mass.
            s.SetLumin();
            s.CurrLumin = Star.GetCurrLumin(s.EvoLine, s.StarAge, s.CurrMass);

            //determine the temperature
            s.EffTemp = Star.GetInitTemp(s.CurrMass);
            s.EffTemp = Star.GetCurrentTemp(s.EvoLine, s.CurrLumin, s.StarAge, s.CurrMass, ourDice);

            //DERIVED STATS: RADIUS, Spectral Type
            s.Radius = Star.GetRadius(s.CurrMass, s.EffTemp, s.CurrLumin, s.EvoLine.FindCurrentAgeGroup(s.StarAge));
            s.SetSpectralType();
            s.StarColor = Star.SetColor(ourDice, s.EffTemp);

            //set flare status.
            SetFlareStatus(s, ourDice);

            //end this here. We will hand orbital mechanics elsewhere.
        }

        /// <summary>
        ///     This sets the flare status of a star
        /// </summary>
        /// <param name="s">The star we're setting for</param>
        /// <param name="ourDice">The Ddice object we use.</param>
        public static void SetFlareStatus( Star s, Dice ourDice )
        {
            var roll = ourDice.GurpsRoll();
            var limit = 12;
            var massLimit = .45;

            if (OptionCont.AnyStarFlareStar)
            {
                massLimit = 11;
            }
            if (OptionCont.MoreFlareStarChance != null && (bool) OptionCont.MoreFlareStarChance)
            {
                limit = 9;
            }

            if (roll >= limit && s.CurrMass <= massLimit)
            {
                s.IsFlareStar = true;
            }
        }

        /// <summary>
        ///     A short hand copy for a comparison
        /// </summary>
        /// <param name="var">The variable being compared</param>
        /// <param name="min">Minimum threshold</param>
        /// <param name="max">Maximum Threshold</param>
        /// <param name="canEqual">If it can equal the bounds. Set to true by default.</param>
        /// <returns></returns>
        public static bool Inbetween( int var, int min, int max, bool canEqual = true )
        {
            if (canEqual)
            {
                return ( var >= min ) && ( var <= max );
            }
            return ( var > min ) && ( var < max );
        }

        /// <summary>
        ///     This picks the gas giant flag for a star.
        /// </summary>
        /// <param name="myStar">The star we're setting for</param>
        /// <param name="roll">The Ddice roll</param>
        public static void GasGiantFlag( Star myStar, int roll )
        {
            //base set.
            int noGasGiant;
            int conGaGiant;
            int eccGaGiant;
            int epiGaGiant;

            if ((bool) !OptionCont.MoreConGasGiantChances)
            {
                noGasGiant = 10;
                conGaGiant = 12;
                eccGaGiant = 14;
                epiGaGiant = 18;
            }
            else
            {
                noGasGiant = 6;
                conGaGiant = 13;
                eccGaGiant = 15;
                epiGaGiant = 18;
            }

            if (roll <= noGasGiant)
            {
                myStar.GasGiantFlag = Star.GasgiantNone;
            }
            if (roll > noGasGiant && roll <= conGaGiant)
            {
                myStar.GasGiantFlag = Star.GasgiantConventional;
            }
            if (roll > conGaGiant && roll <= eccGaGiant)
            {
                myStar.GasGiantFlag = Star.GasgiantEccentric;
            }
            if (roll > eccGaGiant && roll <= epiGaGiant)
            {
                myStar.GasGiantFlag = Star.GasgiantEpistellar;
            }
        }

        /// <summary>
        ///     deteremine the orbital ratio between planets
        /// </summary>
        /// <param name="myDice">Ddice object</param>
        /// <returns>The orbital ratio between planets</returns>
        public static double GetOrbitalRatio( Dice myDice )
        {
            double ratio = 0;

            var roll = myDice.GurpsRoll();

            if (roll == 3 || roll == 4)
            {
                ratio = 1.4 + myDice.Rng(1, 5) * .01;
            }

            if (roll == 5 || roll == 6)
            {
                ratio = 1.5 + myDice.Rng(1, 10, -5) * .01;
            }

            if (roll == 7 || roll == 8)
            {
                ratio = 1.6 + myDice.Rng(1, 10, -5) * .01;
            }

            if (roll == 9 || roll == 10 || roll == 11 || roll == 12)
            {
                ratio = 1.7 + myDice.Rng(1, 10, -5) * .01;
            }

            if (roll == 13 || roll == 14)
            {
                ratio = 1.8 + myDice.Rng(1, 10, -5) * .01;
            }

            if (roll == 15 || roll == 16)
            {
                ratio = 1.9 + myDice.Rng(1, 10, -5) * .01;
            }

            if (roll == 17 || roll == 18)
            {
                ratio = 2.0 + myDice.Rng(1, 10, -5) * .01;
            }

            return ratio;
        }

        private static double DetermineDistance( double planetDistance, double[,] distChart, int planetParent, int starId )
        {
            var parent = 0;
            var target = 0;

            //get the parent flag
            if (planetParent == Star.IsPrimary)
            {
                parent = 0;
            }
            if (planetParent == Star.IsSecondary)
            {
                parent = 1;
            }
            if (planetParent == Star.IsTrinary)
            {
                parent = 2;
            }
            if (planetParent == Star.IsSeccomp)
            {
                parent = 3;
            }
            if (planetParent == Star.IsTricomp)
            {
                parent = 4;
            }

            if (planetParent == Satellite.OrbitPrisec)
            {
                parent = 0;
            }
            if (planetParent == Satellite.OrbitSeccom)
            {
                parent = 1;
            }
            if (planetParent == Satellite.OrbitTricom)
            {
                parent = 2;
            }
            if (planetParent == Satellite.OrbitPrisectri)
            {
                parent = 0;
            }
            if (planetParent == Satellite.OrbitPritri)
            {
                parent = 0;
            }

            if (starId == Star.IsPrimary)
            {
                target = 0;
            }
            if (planetParent == Star.IsSecondary)
            {
                target = 1;
            }
            if (planetParent == Star.IsTrinary)
            {
                target = 2;
            }
            if (planetParent == Star.IsSeccomp)
            {
                target = 3;
            }
            if (planetParent == Star.IsTricomp)
            {
                target = 4;
            }

            var dist = Math.Abs(distChart[parent, target] + planetDistance);

            return dist;
        }

        /// <summary>
        ///     Determines RVM, and geologic values for a satelite
        /// </summary>
        /// <param name="s">The satelite</param>
        /// <param name="ourBag">Ddice object</param>
        /// <param name="sysAge">System Age</param>
        /// <param name="isGasGiantMoon">Is this a moon of a gas giant?</param>
        private static void DetermineGeologicValues( Satellite s, Dice ourBag, double sysAge, bool isGasGiantMoon )
        {
            //volcanic set first.
            var addVal = s.Gravity / sysAge * 40;

            if (s.MajorMoons.Count == 1)
            {
                addVal = addVal + 5;
            }
            if (s.MajorMoons.Count == 2)
            {
                addVal = addVal + 10;
            }

            if (s.SatelliteType == Satellite.SubtypeSulfur)
            {
                addVal = addVal + 60;
            }
            if (isGasGiantMoon)
            {
                addVal = addVal + 5;
            }

            var roll = ourBag.GurpsRoll();

            addVal = addVal + roll;

            if (addVal <= 16.5)
            {
                s.VolActivity = Satellite.GeologicNone;
            }
            if (addVal > 16.5 && addVal <= 20.5)
            {
                s.VolActivity = Satellite.GeologicLight;
            }
            if (addVal > 20.5 && addVal <= 26.5)
            {
                s.VolActivity = Satellite.GeologicModerate;
            }
            if (addVal > 26.5 && addVal <= 70.5)
            {
                s.VolActivity = Satellite.GeologicHeavy;
            }
            if (addVal > 70.5)
            {
                s.VolActivity = Satellite.GeologicExtreme;
            }

            roll = ourBag.GurpsRoll();
            if (s.VolActivity == Satellite.GeologicHeavy && s.SatelliteType == Satellite.SubtypeGarden && roll <= 8)
            {
                roll = ourBag.Rng(6);
                if (roll <= 3)
                {
                    s.AddAtmCategory(Satellite.AtmMargPollutants);
                }
                if (roll >= 4)
                {
                    s.AddAtmCategory(Satellite.AtmMargSulfur);
                }
            }

            roll = ourBag.GurpsRoll();
            if (s.VolActivity == Satellite.GeologicExtreme && s.SatelliteType == Satellite.SubtypeGarden && roll <= 14)
            {
                roll = ourBag.Rng(6);
                if (roll <= 3)
                {
                    s.AddAtmCategory(Satellite.AtmMargPollutants);
                }
                if (roll >= 4)
                {
                    s.AddAtmCategory(Satellite.AtmMargSulfur);
                }
            }

            //tectonic next
            roll = ourBag.GurpsRoll();

            //negative mods
            if (Math.Abs(s.HydCoverage) < 0)
            {
                roll = roll - 4;
            }
            if (s.HydCoverage > 0 && s.HydCoverage < .5)
            {
                roll = roll - 2;
            }
            if (s.VolActivity == Satellite.GeologicNone)
            {
                roll = roll - 8;
            }
            if (s.VolActivity == Satellite.GeologicLight)
            {
                roll = roll - 4;
            }

            //postive mods
            if (s.VolActivity == Satellite.GeologicHeavy)
            {
                roll = roll + 4;
            }
            if (s.VolActivity == Satellite.GeologicExtreme)
            {
                roll = roll + 8;
            }
            if (s.MajorMoons.Count == 1)
            {
                roll = roll + 2;
            }
            if (s.MajorMoons.Count > 1)
            {
                roll = roll + 4;
            }

            //nullers.
            if (s.SatelliteSize == Satellite.SizeTiny)
            {
                roll = 0;
            }
            if (s.SatelliteSize == Satellite.SizeSmall)
            {
                roll = 0;
            }

            if (roll <= 6.5)
            {
                s.TecActivity = Satellite.GeologicNone;
            }
            if (roll > 6.5 && roll <= 10.5)
            {
                s.TecActivity = Satellite.GeologicLight;
            }
            if (roll > 10.5 && roll <= 14.5)
            {
                s.TecActivity = Satellite.GeologicModerate;
            }
            if (roll > 14.5 && roll <= 18.5)
            {
                s.TecActivity = Satellite.GeologicHeavy;
            }
            if (roll > 18.5)
            {
                s.TecActivity = Satellite.GeologicExtreme;
            }

            //update RVM
            if ((bool) !OptionCont.HighRvmVal)
            {
                roll = ourBag.GurpsRoll();
            }
            if (OptionCont.HighRvmVal != null && (bool) OptionCont.HighRvmVal)
            {
                roll = ourBag.Rng(1, 6, 10);
            }

            if (s.VolActivity == Satellite.GeologicNone)
            {
                roll = roll - 2;
            }
            if (s.VolActivity == Satellite.GeologicLight)
            {
                roll = roll - 1;
            }
            if (s.VolActivity == Satellite.GeologicHeavy)
            {
                roll = roll + 1;
            }
            if (s.VolActivity == Satellite.GeologicExtreme)
            {
                roll = roll + 2;
            }

            if (s.BaseType == Satellite.BasetypeAsteroidbelt)
            {
                if (s.SatelliteSize == Satellite.SizeTiny)
                {
                    roll = roll - 1;
                }
                if (s.SatelliteSize == Satellite.SizeMedium)
                {
                    roll = roll + 2;
                }
                if (s.SatelliteSize == Satellite.SizeLarge)
                {
                    roll = roll + 4;
                }
            }

            //set stable activity here:
            if (OptionCont.StableActivity != null && ( (bool) OptionCont.StableActivity && s.SatelliteSize >= Satellite.SizeSmall && ( s.BaseType == Satellite.BasetypeMoon || s.BaseType == Satellite.BasetypeTerrestial ) ))
            {
                s.VolActivity = Satellite.GeologicModerate;
                s.TecActivity = Satellite.GeologicModerate;
            }

            s.PopulateRvm(roll);
        }

        /// <summary>
        ///     Updates a satellite for tidal lock
        /// </summary>
        /// <param name="s">The satelite </param>
        /// <param name="ourBag">Our Ddice object</param>
        private static void UpdateTidalLock( Satellite s, Dice ourBag )
        {
            var atmDesc = s.GetAtmCategory();

            if (atmDesc == Satellite.AtmPresNone || atmDesc == Satellite.AtmPresTrace)
            {
                s.UpdateAtmPres(0.00);
                s.HydCoverage = 0.0;
                s.DayFaceMod = 1.2;
                s.NightFaceMod = .1;
            }

            if (atmDesc == Satellite.AtmPresVerythin)
            {
                s.UpdateAtmPres(0.01);
                s.HydCoverage = 0.0;
                s.DayFaceMod = 1.2;
                s.NightFaceMod = .1;
            }

            if (atmDesc == Satellite.AtmPresThin)
            {
                s.UpdateAtmPres(ourBag.RollRange(.01, .49));

                s.HydCoverage = s.HydCoverage - .5;
                if (s.HydCoverage < 0)
                {
                    s.HydCoverage = 0.0;
                }

                s.DayFaceMod = 1.16;
                s.NightFaceMod = .67;
            }

            if (atmDesc == Satellite.AtmPresStandard)
            {
                s.HydCoverage = s.HydCoverage - .25;
                if (s.HydCoverage < 0)
                {
                    s.HydCoverage = 0.0;
                }

                s.DayFaceMod = 1.12;
                s.NightFaceMod = .80;
            }

            if (atmDesc == Satellite.AtmPresDense)
            {
                s.HydCoverage = s.HydCoverage - .1;
                if (s.HydCoverage < 0)
                {
                    s.HydCoverage = 0.0;
                }
                s.DayFaceMod = 1.09;
                s.NightFaceMod = .88;
            }

            if (atmDesc == Satellite.AtmPresVerydense)
            {
                s.DayFaceMod = 1.05;
                s.NightFaceMod = .95;
            }

            if (atmDesc != Satellite.AtmPresSuperdense)
            {
                return;
            }
            s.DayFaceMod = 1.0;
            s.NightFaceMod = 1.0;
        }

        private static void CalcEccentricity( Dice ourDice, Star s )
        {
            var modifiers = 0; //reset the thing.

            if (OptionCont.LessStellarEccent)
            {
                //now we generate eccentricities
                if (s.OrbitalSep == Star.OrbsepVeryclose)
                {
                    modifiers = modifiers - 10; //Very Close
                }
                if (s.OrbitalSep == Star.OrbsepClose)
                {
                    modifiers = modifiers - 6; //Close
                }
                if (s.OrbitalSep == Star.OrbsepModerate)
                {
                    modifiers = modifiers - 2; //Moderate  
                }
            }
            else
            {
                if (s.OrbitalSep == Star.OrbsepVeryclose)
                {
                    modifiers = modifiers - 6; //Very Close
                }
                if (s.OrbitalSep == Star.OrbsepClose)
                {
                    modifiers = modifiers - 4; //Close
                }
                if (s.OrbitalSep == Star.OrbsepModerate)
                {
                    modifiers = modifiers - 2; //Moderate  
                }
            }

            var roll = ourDice.GurpsRoll(modifiers);
            Star.GenerateEccentricity(roll, s);

            if (OptionCont.ForceVeryLowStellarEccent)
            {
                if (s.OrbitalEccent > .2)
                {
                    s.OrbitalEccent = .1;
                }
                if (s.OrbitalEccent > .1 && s.OrbitalEccent < .2)
                {
                    s.OrbitalEccent = .05;
                }
            }
        }

        /// <summary>
        ///     Rolls the gas giant size and updates it.
        /// </summary>
        /// <param name="s">The gas gaint we are editing</param>
        /// <param name="roll">The Ddice roll</param>
        public static void UpdateGasGiantSize( Satellite s, int roll )
        {
            if (roll <= 10)
            {
                s.UpdateSize(Satellite.SizeSmall);
            }
            if (roll >= 11 && roll <= 16)
            {
                s.UpdateSize(Satellite.SizeMedium);
            }
            if (roll >= 17)
            {
                s.UpdateSize(Satellite.SizeLarge);
            }
        }

        /// <summary>
        ///     Populates orbits around a star, according to GURPS 4e rules. (Does not create them)
        /// </summary>
        /// <param name="s">The star we're populating around</param>
        /// <param name="myDice">Our Ddice object.</param>
        public static void PopulateOrbits( Star s, Dice myDice )
        {
            const double maxRatio = 2.0;
            const double minRatio = 1.4;
            const double minDistance = .15;
            var firstGasGiant = !s.ContainsGasGiants();

            foreach (var sat in s.SysPlanets)
            {
                var roll = myDice.GurpsRoll();

                //set gas giants first.
                if (s.GasGiantFlag != Star.GasgiantNone)
                {
                    //BEFORE SNOW LINE: Only Eccentric, Epistellar
                    if (sat.OrbitalRadius < Star.SnowLine(s.InitLumin))
                    {
                        if (roll <= 8 && s.GasGiantFlag == Star.GasgiantEccentric)
                        {
                            sat.UpdateType(Satellite.BasetypeGasgiant);
                            UpdateGasGiantSize(sat, myDice.GurpsRoll() + 4);
                        }

                        if (roll <= 6 && s.GasGiantFlag == Star.GasgiantEpistellar)
                        {
                            sat.UpdateType(Satellite.BasetypeGasgiant);
                            UpdateGasGiantSize(sat, myDice.GurpsRoll() + 4);
                        }
                    }

                    //AFTER SNOW LINE: All three
                    if (sat.OrbitalRadius >= Star.SnowLine(s.InitLumin))
                    {
                        if (roll <= 15 && s.GasGiantFlag == Star.GasgiantConventional)
                        {
                            sat.UpdateType(Satellite.BasetypeGasgiant);
                            if (firstGasGiant)
                            {
                                UpdateGasGiantSize(sat, myDice.GurpsRoll() + 4);
                                firstGasGiant = false;
                            }
                            else
                            {
                                UpdateGasGiantSize(sat, myDice.GurpsRoll());
                            }
                        }

                        if (roll <= 14 && ( s.GasGiantFlag == Star.GasgiantEccentric || s.GasGiantFlag == Star.GasgiantEpistellar ))
                        {
                            sat.UpdateType(Satellite.BasetypeGasgiant);
                            if (firstGasGiant)
                            {
                                UpdateGasGiantSize(sat, myDice.GurpsRoll() + 4);
                                firstGasGiant = false;
                            }
                            else
                            {
                                UpdateGasGiantSize(sat, myDice.GurpsRoll());
                            }
                        }
                    }
                }

                //Done with the gas giant. Let's go start seeign what else it could be.

                //We can get mods now.
                if (sat.BaseType != Satellite.BasetypeGasgiant)
                {
                    //INNER AND OUTER RADIUS
                    var mod = 0;

                    if (sat.OrbitalRadius - minDistance <= Star.InnerRadius(s.InitLumin, s.InitMass) || sat.OrbitalRadius / Star.InnerRadius(s.InitLumin, s.InitMass) <= maxRatio)
                    {
                        mod = mod - 3;
                    }

                    if (sat.OrbitalRadius + minDistance >= Star.OuterRadius(s.InitMass) || Star.OuterRadius(s.InitMass) / sat.OrbitalRadius <= maxRatio)
                    {
                        mod = mod - 3;
                    }

                    //FORBIDDDEN ZONE
                    if (s.GetClosestDistToForbiddenZone(sat.OrbitalRadius) <= minDistance || ( s.GetClosestForbiddenZoneRatio(sat.OrbitalRadius) < maxRatio && s.GetClosestForbiddenZoneRatio(sat.OrbitalRadius) > minRatio ))
                    {
                        //MessageBox.Show("THE FORBIDDEN ZONE!!!!");
                        mod = mod - 6;
                    }

                    //GAS GIANT LOCATION
                    if (s.IsPrevSatelliteGasGiant(sat.OrbitalRadius))
                    {
                        mod = mod - 6;
                    }
                    if (s.IsNextSatelliteGasGiant(sat.OrbitalRadius))
                    {
                        mod = mod - 3;
                    }

                    //now let's get the orbit type.
                    // MessageBox.Show("Mod is " + mod);
                    mod = mod + myDice.GurpsRoll();
                    //MessageBox.Show("Mod + Roll is " + mod);
                    if (mod <= 3)
                    {
                        sat.UpdateType(Satellite.BasetypeEmpty);
                    }

                    if (mod >= 4 && mod <= 6)
                    {
                        sat.UpdateType(Satellite.BasetypeAsteroidbelt);

                        //Expanded Asteroid Belt options
                        if (OptionCont.ExpandAsteroidBelt != null && (bool) OptionCont.ExpandAsteroidBelt)
                        {
                            roll = myDice.GurpsRoll();
                            if (roll <= 6)
                            {
                                sat.UpdateSize(Satellite.SizeTiny);
                            }
                            if (roll >= 7 && roll <= 13)
                            {
                                sat.UpdateSize(Satellite.SizeSmall);
                            }
                            if (roll >= 14 && roll <= 15)
                            {
                                sat.UpdateSize(Satellite.SizeMedium);
                            }
                            if (roll >= 16)
                            {
                                sat.UpdateSize(Satellite.SizeLarge);
                            }
                        }

                        else
                        {
                            sat.UpdateSize(Satellite.SizeSmall);
                        }
                    }

                    if (mod >= 7 && mod <= 8)
                    {
                        sat.UpdateTypeSize(Satellite.BasetypeTerrestial, Satellite.SizeTiny);
                    }

                    if (mod >= 9 && mod <= 11)
                    {
                        sat.UpdateTypeSize(Satellite.BasetypeTerrestial, Satellite.SizeSmall);
                    }

                    if (mod >= 12 && mod <= 15)
                    {
                        sat.UpdateTypeSize(Satellite.BasetypeTerrestial, Satellite.SizeMedium);
                    }

                    if (mod >= 16)
                    {
                        sat.UpdateTypeSize(Satellite.BasetypeTerrestial, Satellite.SizeLarge);
                    }
                }
            }
        }

        //LibStarGen.convertTemp("kelvin", "celsius", pl.surfaceTemp) + "C)";

        /// <summary>
        ///     Generic convert temperature function
        /// </summary>
        /// <param name="source">The temperature type ("kelvin", "celsius", "farenheit")</param>
        /// <param name="destination">The temperature type ("kelvin", "celsius", "farenheit")</param>
        /// <param name="sourceTemp">The temperature</param>
        /// <returns></returns>
        public static double ConvertTemp( string source, string destination, double sourceTemp )
        {
            if (source == "kelvin")
            {
                if (destination == "celsius")
                {
                    return sourceTemp - 273.15;
                }

                if (destination == "farenheit")
                {
                    return ( sourceTemp - 273.15 ) * 1.8 + 32.0;
                }
            }

            if (source == "celsius")
            {
                if (destination == "kelvin")
                {
                    return sourceTemp + 273.15;
                }

                if (destination == "farenheit")
                {
                    return sourceTemp * 1.8 + 32.0;
                }
            }

            if (source == "farenheit")
            {
                if (destination == "celsius")
                {
                    return ( sourceTemp - 32.0 ) / 1.8;
                }

                if (destination == "kelvin")
                {
                    return ( sourceTemp - 32.0 ) / 1.8 + 273.15;
                }
            }

            return -9999.9;
        }

        /// <summary>
        ///     This places our stars around the primary, as well as creating the secondary stars if called for
        /// </summary>
        /// <param name="ourSystem">The star system to be added to.</param>
        /// <param name="velvetBag">Our Ddice object.</param>
        private static void PlaceOurStars( StarSystem ourSystem, Dice velvetBag )
        {
            //initiate the variables we need to ensure distances are kept
            var maxOrbitalDistance = 600.0;
            var starLimit = ourSystem.SysStars.Count;
            for (var i = 1; i < starLimit; i++)
            {
                var modifiers = 0;
                var minOrbitalDistance = ourSystem.SysStars[i - 1].OrbitalRadius;

                //set the min and max conditions for the first star here.
                int roll;
                double tempVal;
                if (ourSystem.SysStars[i].ParentId == 0 || ourSystem.SysStars[i].ParentId == Star.IsPrimary)
                {
                    //apply modifiers
                    if (ourSystem.SysStars[i].SelfId == Star.IsTrinary)
                    {
                        modifiers = modifiers + 6;
                    }
                    if (OptionCont.ForceGardenFavorable != null)
                    {
                        if ((bool) OptionCont.ForceGardenFavorable && ourSystem.SysStars[i].ParentId == Star.IsPrimary)
                        {
                            modifiers = modifiers + 4;
                        }
                    }

                    if (minOrbitalDistance == 600.0)
                    {
                        //in this situation, orbital 3 or so can't be safely placed because the range is 0. 
                        // so we autogenerate it.
                        tempVal = velvetBag.RollRange(25, 25);
                        ourSystem.SysStars[i].OrbitalSep = 5;
                        ourSystem.SysStars[ourSystem.Star2Index].OrbitalRadius = ourSystem.SysStars[ourSystem.Star2Index].OrbitalRadius - tempVal;
                        ourSystem.SysStars[i].OrbitalRadius = 600 + tempVal;
                        ourSystem.SysStars[i].DistFromPrimary = ourSystem.SysStars[i].OrbitalRadius;
                    }
                    else
                    {
                        do
                        {
                            //roll the Ddice and generate the orbital radius
                            do
                            {
                                roll = velvetBag.GurpsRoll(modifiers);
                                if (roll <= 6)
                                {
                                    ourSystem.SysStars[i].OrbitalSep = Star.OrbsepVeryclose;
                                }
                                if (roll >= 7 && roll <= 9)
                                {
                                    ourSystem.SysStars[i].OrbitalSep = Star.OrbsepClose;
                                }
                                if (roll >= 10 && roll <= 11)
                                {
                                    ourSystem.SysStars[i].OrbitalSep = Star.OrbsepModerate;
                                }
                                if (roll >= 12 && roll <= 14)
                                {
                                    ourSystem.SysStars[i].OrbitalSep = Star.OrbsepWide;
                                }
                                if (roll >= 15)
                                {
                                    ourSystem.SysStars[i].OrbitalSep = Star.OrbsepDistant;
                                }
                                tempVal = velvetBag.Rng(2, 6) * GetSepModifier(ourSystem.SysStars[i].OrbitalSep);
                            }
                            while (tempVal <= minOrbitalDistance);

                            //if (ourSystem.sysStars[i].selfID == 2) tempVal = this.velvetBag.six(1, 7) * ourSystem.sysStars[i].getSepModifier(); 
                            var lowerBound = tempVal - .5 * GetSepModifier(ourSystem.SysStars[i].OrbitalSep);
                            var higherBound = .5 * GetSepModifier(ourSystem.SysStars[i].OrbitalSep) + tempVal;

                            //set for constraints
                            if (lowerBound < minOrbitalDistance)
                            {
                                lowerBound = minOrbitalDistance;
                            }
                            if (higherBound > maxOrbitalDistance)
                            {
                                higherBound = maxOrbitalDistance;
                            }

                            ourSystem.SysStars[i].OrbitalRadius = tempVal;
                            ourSystem.SysStars[i].DistFromPrimary = ourSystem.SysStars[i].OrbitalRadius;
                        }
                        while (ourSystem.SysStars[i].OrbitalRadius <= minOrbitalDistance);

                        //let's see if it has a subcompanion
                        if (ourSystem.SysStars[i].OrbitalSep == Star.OrbsepDistant)
                        {
                            roll = velvetBag.GurpsRoll();
                            if (roll >= 11)
                            {
                                //generate the subcompanion
                                var order = 0;

                                if (ourSystem.SysStars[i].SelfId == Star.IsSecondary)
                                {
                                    order = Star.IsSeccomp;
                                }
                                if (ourSystem.SysStars[i].SelfId == Star.IsTrinary)
                                {
                                    order = Star.IsTricomp;
                                }

                                //add the star
                                ourSystem.AddStar(order, ourSystem.SysStars[i].SelfId, i + 1);

                                ourSystem.SysStars[starLimit].Name = Star.GenGenericName(ourSystem.SysName, i + 1);

                                //set the name, then generate the star
                                ourSystem.SysStars[starLimit].ParentName = ourSystem.SysStars[i].Name;
                                GenerateAStar(ourSystem.SysStars[starLimit], velvetBag, ourSystem.SysStars[i].CurrMass, ourSystem.SysName);
                                starLimit++; //increment the total number of stars we have generated
                            }
                        }
                    }
                }
                else
                {
                    maxOrbitalDistance = ourSystem.SysStars[ourSystem.GetStellarParentId(ourSystem.SysStars[i].ParentId)].OrbitalRadius;
                    //roll for seperation
                    do
                    {
                        //roll the Ddice

                        roll = velvetBag.GurpsRoll(-6);
                        if (roll <= 6)
                        {
                            ourSystem.SysStars[i].OrbitalSep = Star.OrbsepVeryclose;
                        }
                        if (roll >= 7 && roll <= 9)
                        {
                            ourSystem.SysStars[i].OrbitalSep = Star.OrbsepClose;
                        }
                        if (roll >= 10 && roll <= 11)
                        {
                            ourSystem.SysStars[i].OrbitalSep = Star.OrbsepModerate;
                        }
                        if (roll >= 12 && roll <= 14)
                        {
                            ourSystem.SysStars[i].OrbitalSep = Star.OrbsepWide;
                        }
                        if (roll >= 15)
                        {
                            ourSystem.SysStars[i].OrbitalSep = Star.OrbsepDistant;
                        }

                        //set the subcompanion orbital
                        tempVal = velvetBag.Rng(2, 6) * GetSepModifier(ourSystem.SysStars[i].OrbitalSep);
                        tempVal -= 0.5 * GetSepModifier(ourSystem.SysStars[i].OrbitalSep);
                        var higherBound = .5 * GetSepModifier(ourSystem.SysStars[i].OrbitalSep) + tempVal;

                        if (higherBound > maxOrbitalDistance)
                        {
                            higherBound = maxOrbitalDistance;
                        }

                        ourSystem.SysStars[i].OrbitalRadius = tempVal;
                        ourSystem.SysStars[i].DistFromPrimary = ourSystem.SysStars[i].OrbitalRadius + maxOrbitalDistance;
                    }
                    while (ourSystem.SysStars[i].OrbitalRadius > maxOrbitalDistance);
                }

                CalcEccentricity(velvetBag, ourSystem.SysStars[i]);

                var parent = ourSystem.GetStellarParentId(ourSystem.SysStars[i].ParentId);
                ourSystem.SysStars[i].OrbitalPeriod = Star.CalcOrbitalPeriod(ourSystem.SysStars[parent].CurrMass, ourSystem.SysStars[i].CurrMass, ourSystem.SysStars[i].OrbitalRadius);
            }
        }

        /// <summary>
        ///     This function draws a table up of each star's distance from each other and from the primary
        /// </summary>
        /// <param name="stars">The stars in this solar system</param>
        /// <returns>Returns a multidimensional array of distances (in doubles)</returns>
        public static double[,] GenDistChart( List<Star> stars )
        {
            //first get distances of each star to the primary.
            //Dictionary<int, Dictionary<int,double> distChart = new Dictionary<int, Dictionary<int,double> = new Dictionary<int,double>>();
            new Dictionary<int, Dictionary<int, double>>();
            var satIdFlags = new int[]
            {
                Star.IsPrimary, Star.IsSecondary, Star.IsTrinary, Star.IsSeccomp, Star.IsTricomp
            };
            var distChart = new double[5, 5];

            //INDEX CHART
            /* 0 is Primary
             * 1 is Secondary
             * 2 is Trinary
             * 3 is Secondary-Companion
             * 4 is Trinary-Companion */

            //ROW ONE: Star from which you are comparing [Current]
            //ROW TWO: Star for which you want. [Target]
            for (var i = 0; i < stars.Count; i++)
            {
                for (var j = 0; j < stars.Count; j++)
                {
                    distChart[i, j] = 0.0;

                    if (i == 0)
                    {
                        if (stars[j].OrderId == Star.IsSecondary)
                        {
                            distChart[i, 1] = stars[j].DistFromPrimary * -1.0;
                        }
                        if (stars[j].OrderId == Star.IsTrinary)
                        {
                            distChart[i, 2] = stars[j].DistFromPrimary * -1.0;
                        }
                        if (stars[j].OrderId == Star.IsSeccomp)
                        {
                            distChart[i, 3] = stars[j].DistFromPrimary * -1.0;
                        }
                        if (stars[j].OrderId == Star.IsTricomp)
                        {
                            distChart[i, 4] = stars[j].DistFromPrimary * -1.0;
                        }
                    }

                    if (stars[i].OrderId == Star.IsSecondary)
                    {
                        distChart[1, 0] = distChart[0, 0] + stars[i].DistFromPrimary;
                        if (stars[j].OrderId == Star.IsSecondary)
                        {
                            distChart[1, 1] = distChart[0, 1] + stars[i].DistFromPrimary;
                        }
                        if (stars[j].OrderId == Star.IsTrinary)
                        {
                            distChart[1, 2] = distChart[0, 2] + stars[i].DistFromPrimary;
                        }
                        if (stars[j].OrderId == Star.IsSeccomp)
                        {
                            distChart[1, 3] = distChart[0, 3] + stars[i].DistFromPrimary;
                        }
                        if (stars[j].OrderId == Star.IsTricomp)
                        {
                            distChart[1, 4] = distChart[0, 4] + stars[i].DistFromPrimary;
                        }
                    }

                    if (stars[i].OrderId == Star.IsTrinary)
                    {
                        distChart[2, 0] = distChart[0, 0] + stars[i].DistFromPrimary;
                        if (stars[j].OrderId == Star.IsSecondary)
                        {
                            distChart[2, 1] = distChart[0, 1] + stars[i].DistFromPrimary;
                        }
                        if (stars[j].OrderId == Star.IsTrinary)
                        {
                            distChart[2, 2] = distChart[0, 2] + stars[i].DistFromPrimary;
                        }
                        if (stars[j].OrderId == Star.IsSeccomp)
                        {
                            distChart[2, 3] = distChart[0, 3] + stars[i].DistFromPrimary;
                        }
                        if (stars[j].OrderId == Star.IsTricomp)
                        {
                            distChart[2, 4] = distChart[0, 4] + stars[i].DistFromPrimary;
                        }
                    }

                    if (stars[i].OrderId == Star.IsSeccomp)
                    {
                        distChart[3, 0] = distChart[0, 0] + stars[i].DistFromPrimary;
                        if (stars[j].OrderId == Star.IsSecondary)
                        {
                            distChart[3, 1] = distChart[0, 1] + stars[i].DistFromPrimary;
                        }
                        if (stars[j].OrderId == Star.IsTrinary)
                        {
                            distChart[3, 2] = distChart[0, 2] + stars[i].DistFromPrimary;
                        }
                        if (stars[j].OrderId == Star.IsSeccomp)
                        {
                            distChart[3, 3] = distChart[0, 3] + stars[i].DistFromPrimary;
                        }
                        if (stars[j].OrderId == Star.IsTricomp)
                        {
                            distChart[3, 4] = distChart[0, 4] + stars[i].DistFromPrimary;
                        }
                    }

                    if (stars[i].OrderId == Star.IsTricomp)
                    {
                        distChart[4, 0] = distChart[0, 0] + stars[i].DistFromPrimary;
                        if (stars[j].OrderId == Star.IsSecondary)
                        {
                            distChart[4, 1] = distChart[0, 1] + stars[i].DistFromPrimary;
                        }
                        if (stars[j].OrderId == Star.IsTrinary)
                        {
                            distChart[4, 2] = distChart[0, 2] + stars[i].DistFromPrimary;
                        }
                        if (stars[j].OrderId == Star.IsSeccomp)
                        {
                            distChart[4, 3] = distChart[0, 3] + stars[i].DistFromPrimary;
                        }
                        if (stars[j].OrderId == Star.IsTricomp)
                        {
                            distChart[4, 4] = distChart[0, 4] + stars[i].DistFromPrimary;
                        }
                    }
                }
            }

            return distChart;
        }

        public static void InitateHabitableZones( double[,] distanceChart, List<Star> stars, List<Range> habitableZone )
        {
            double upperBound = 0;
            const double maxDistance = 755;
            double currDistance = 0;

            do
            {
                double currTemp = 0;
                foreach (var s in stars)
                {
                    var currStar = 0;
                    //first, find the current star.
                    if (s.OrderId == Star.IsPrimary)
                    {
                        currStar = 0;
                    }
                    if (s.OrderId == Star.IsSecondary)
                    {
                        currStar = 1;
                    }
                    if (s.OrderId == Star.IsTrinary)
                    {
                        currStar = 2;
                    }
                    if (s.OrderId == Star.IsSeccomp)
                    {
                        currStar = 3;
                    }
                    if (s.OrderId == Star.IsTricomp)
                    {
                        currStar = 4;
                    }

                    var detDst = Math.Abs(distanceChart[0, currStar] + currDistance);
                    currTemp = currTemp + Math.Pow(278.0 * Math.Pow(s.CurrLumin, .25) / Math.Sqrt(detDst), 4);
                }
                currTemp = Math.Pow(currTemp, .25);

                if (currTemp == 320)
                {
                    upperBound = currDistance;
                }

                if (currTemp == 240)
                {
                    var lowerBound = currDistance;
                    habitableZone.Add(new Range(lowerBound, upperBound));
                    upperBound = 0;
                }
                currDistance = currDistance + .00001;
            }
            while (currDistance <= maxDistance);
        }

        /// <summary>
        ///     This function creates planets. (durr hurr). Only invoke after you've deteremined the orbitals.
        /// </summary>
        /// <param name="ourSystem">The star system we are creating for</param>
        /// <param name="ourPlanets">The orbitals we've created</param>
        /// <param name="velvetBag">Our Ddice object</param>
        public static void CreatePlanets( StarSystem ourSystem, List<Satellite> ourPlanets, Dice velvetBag )
        {
            var distanceTable = GenDistChart(ourSystem.SysStars);

            foreach (var s in ourPlanets)
            {
                if (s.BaseType == Satellite.BasetypeAsteroidbelt || s.BaseType == Satellite.BasetypeEmpty)
                {
                    if (s.BaseType == Satellite.BasetypeAsteroidbelt)
                    {
                        DetermineGeologicValues(s, velvetBag, ourSystem.SysAge, false);
                    }

                    continue;
                }

                double temp;
                var parent = ourSystem.GetValidParent(s.ParentId);

                //set physical properties
                s.GenGenericName(ourSystem.SysStars[parent].Name, ourSystem.SysName);
                s.GenWorldType(ourSystem.MaxMass, ourSystem.SysAge, velvetBag);

                s.GenDensity(velvetBag);
                s.GenPhysicalParameters(velvetBag);
                s.SetClimateData(ourSystem.MaxMass, velvetBag);
                s.DetSurfaceTemp(0);
                if (s.BaseType != Satellite.BasetypeGasgiant)
                {
                    s.CalcAtmPres();
                }

                s.CreateMoons(ourSystem.SysName, velvetBag, OptionCont.MoonOrbitFlag);

                s.GetPlanetEccentricity(ourSystem.SysStars[parent].GasGiantFlag, Star.SnowLine(ourSystem.SysStars[parent].InitLumin), velvetBag);
                s.GenerateOrbitalPeriod(ourSystem.SysStars[parent].CurrMass);
                s.CreateAxialTilt(velvetBag);

                foreach (var sun in from sun in ourSystem.SysStars let dist = DetermineDistance(s.OrbitalRadius, distanceTable, s.ParentId, sun.SelfId) select sun)
                {
                    temp = .46 * sun.CurrMass * s.Diameter / Math.Pow(s.OrbitalRadius, 3);
                    var tide = 0;

                    //add the correct flag.
                    if (sun.SelfId == Star.IsPrimary)
                    {
                        tide = Satellite.TidePrimarystar;
                    }
                    if (sun.SelfId == Star.IsSecondary)
                    {
                        tide = Satellite.TideSecondarystar;
                    }
                    if (sun.SelfId == Star.IsTrinary)
                    {
                        tide = Satellite.TideTrinarystar;
                    }
                    if (sun.SelfId == Star.IsSeccomp)
                    {
                        tide = Satellite.TideSeccompstar;
                    }
                    if (sun.SelfId == Star.IsTricomp)
                    {
                        tide = Satellite.TideTricompstar;
                    }

                    s.TideForce.Add(tide, temp);
                }

                if (s.MajorMoons.Count > 0)
                {
                    foreach (var moon in s.MajorMoons)
                    {
                        double lunarTides;
                        double different = 0;

                        moon.GenGenericName(s.Name, ourSystem.SysName);
                        //establish physical properties
                        moon.GenWorldType(ourSystem.MaxMass, ourSystem.SysAge, velvetBag);
                        if (s.BaseType == Satellite.BasetypeGasgiant)
                        {
                            //first, differentation test.
                            var dFactor = moon.GetDifferentationFactor(s.Mass, velvetBag);
                            if (dFactor > 100)
                            {
                                if (moon.SatelliteType == Satellite.SubtypeIce)
                                {
                                    moon.UpdateType(Satellite.SubtypeSulfur);
                                }
                                different = -.15;
                            }
                            if (dFactor > 80 && dFactor <= 100)
                            {
                                if (moon.SatelliteType == Satellite.SubtypeIce)
                                {
                                    moon.UpdateType(Satellite.SubtypeSulfur);
                                }
                                different = -.1;
                            }
                            if (dFactor > 50 && dFactor <= 80)
                            {
                                different = -.05;
                                moon.UpdateDescListing(Satellite.DescSubsurfocean);
                            }
                            if (dFactor > 30 && dFactor <= 50)
                            {
                                moon.UpdateDescListing(Satellite.DescSubsurfocean);
                            }
                        }

                        moon.GenDensity(velvetBag);
                        moon.GenPhysicalParameters(velvetBag);
                        moon.SetClimateData(ourSystem.MaxMass, velvetBag);
                        moon.DetSurfaceTemp(different);
                        moon.CalcAtmPres();

                        if (s.BaseType == Satellite.BasetypeGasgiant)
                        {
                            //radiation test
                            moon.UpdateDescListing(moon.AtmPres > .2 ? Satellite.DescRadHighback : Satellite.DescRadLethalback);
                        }

                        //orbital period
                        moon.GenerateOrbitalPeriod(s.Mass);

                        //update parent. 
                        temp = 2230000 * moon.Mass * s.Diameter / Math.Pow(moon.OrbitalRadius, 3);
                        s.TideForce.Add(Satellite.TideMoonBase + moon.SelfId + 1, temp);

                        //moon tides
                        lunarTides = 2230000 * s.Mass * moon.Diameter / Math.Pow(moon.OrbitalRadius, 3);

                        lunarTides = lunarTides * ourSystem.SysAge / moon.Mass;
                        moon.TideForce.Add(Satellite.TideParplanet, lunarTides);
                        moon.TideTotal = moon.TotalTidalForce(ourSystem.SysAge);

                        if (moon.TideTotal >= 50 && velvetBag.GurpsRoll() > 17)
                        {
                            moon.IsResonant = true;
                        }
                        else if (moon.TideTotal >= 50)
                        {
                            moon.IsTideLocked = true;
                        }

                        moon.GenerateOrbitalVelocity(velvetBag);
                        if (moon.IsTideLocked && !moon.IsResonant)
                        {
                            UpdateTidalLock(moon, velvetBag);
                        }
                        if (moon.IsResonant)
                        {
                            moon.SiderealPeriod = moon.OrbitalPeriod * 2.0 / 3.0;
                            moon.RotationalPeriod = moon.SiderealPeriod;
                        }

                        if (velvetBag.GurpsRoll() >= 17)
                        {
                            moon.RetrogradeMotion = true;
                        }

                        if (moon.OrbitalPeriod == moon.SiderealPeriod)
                        {
                            moon.RotationalPeriod = 0;
                        }

                        else //calculate solar day from sidereal
                        {
                            double sidereal;
                            if (moon.RetrogradeMotion)
                            {
                                sidereal = -1 * moon.SiderealPeriod;
                            }
                            else
                            {
                                sidereal = moon.SiderealPeriod;
                            }

                            moon.RotationalPeriod = s.OrbitalPeriod * sidereal / ( s.OrbitalPeriod - sidereal );
                            moon.OrbitalCycle = moon.OrbitalPeriod * s.RotationalPeriod / ( moon.OrbitalPeriod - s.RotationalPeriod );
                        }
                        moon.CreateAxialTilt(velvetBag);
                        DetermineGeologicValues(moon, velvetBag, ourSystem.SysAge, s.BaseType == Satellite.BasetypeGasgiant);
                    }
                }

                //tides calculated already.
                s.TideTotal = s.TotalTidalForce(ourSystem.SysAge);
                if (s.TideTotal >= 50 && s.OrbitalEccent > .1)
                {
                    s.IsResonant = true;
                }
                else if (s.TideTotal >= 50)
                {
                    s.IsTideLocked = true;
                }

                s.GenerateOrbitalVelocity(velvetBag);

                if (s.IsTideLocked && !s.IsResonant)
                {
                    UpdateTidalLock(s, velvetBag);
                }
                if (s.IsResonant)
                {
                    s.SiderealPeriod = s.OrbitalPeriod * 2.0 / 3.0;
                    s.RotationalPeriod = s.SiderealPeriod;
                }

                if (velvetBag.GurpsRoll() >= 13)
                {
                    s.RetrogradeMotion = true;
                }
                if (s.OrbitalPeriod == s.SiderealPeriod)
                {
                    s.RotationalPeriod = 0;
                }

                else
                {
                    double sidereal;
                    if (s.RetrogradeMotion)
                    {
                        sidereal = -1 * s.SiderealPeriod;
                    }
                    else
                    {
                        sidereal = s.SiderealPeriod;
                    }

                    s.RotationalPeriod = s.OrbitalPeriod * sidereal / ( s.OrbitalPeriod - sidereal );
                }
                s.CreateAxialTilt(velvetBag);
                DetermineGeologicValues(s, velvetBag, ourSystem.SysAge, false);
            }
        }

        //describes the seperation flag. Used to describe it.
        public static string GetSeperationStr( int orbitalSep )
        {
            if (orbitalSep == Star.OrbsepNone)
            {
                return "None";
            }
            if (orbitalSep == Star.OrbsepContact)
            {
                return "Contact";
            }
            if (orbitalSep == Star.OrbsepVeryclose)
            {
                return "Very Close";
            }
            if (orbitalSep == Star.OrbsepClose)
            {
                return "Close";
            }
            if (orbitalSep == Star.OrbsepModerate)
            {
                return "Moderate";
            }
            if (orbitalSep == Star.OrbsepDistant)
            {
                return "Distant";
            }
            if (orbitalSep == Star.OrbsepWide)
            {
                return "Wide";
            }

            return "ERROR";
        }

        public static double GetSepModifier( int flag )
        {
            if (flag == Star.OrbsepNone)
            {
                return 0;
            }
            if (flag == Star.OrbsepContact)
            {
                return 0.00001;
            }
            if (flag == Star.OrbsepVeryclose)
            {
                return 0.05;
            }
            if (flag == Star.OrbsepClose)
            {
                return 0.5;
            }
            if (flag == Star.OrbsepModerate)
            {
                return 2;
            }
            if (flag == Star.OrbsepDistant)
            {
                return 10;
            }
            if (flag == Star.OrbsepWide)
            {
                return 50;
            }

            return -1;
        }
    }
}