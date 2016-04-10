using System;
using UniverseGeneration.Range_Objects;
using UniverseGeneration.Utility;

namespace UniverseGeneration.Stellar_Bodies
{
    public partial class Star
    {
        /// <summary>
        ///     this function gets the space swallowed up when a star balloons into a giant phase
        ///     <returns>The swallowed space</returns>
        /// </summary>
        protected virtual double GetSwallowedSpace()
        {
            return .01 * Math.Sqrt(MaxLumin);
        }

        /// <summary>
        ///     This function is used to safely roll on the table (contains some error bound checking.)
        /// </summary>
        /// <param name="rollA">First roll</param>
        /// <param name="rollB">Second roll</param>
        /// <exception cref="System.ArgumentException">Throws an ArgumentException if this is out of bounds (below 0 and above 18)</exception>
        /// <returns>The table entry</returns>
        public static double GetMassByRoll( int rollA, int rollB )
        {
            if (rollA > 18 || rollA < 0 || rollB > 18 || rollB < 0)
            {
                throw new ArgumentException("One of the passed Ddice roll is beyond limits");
            }

            return StarMassTableByRoll[rollA][rollB];
        }

        /// <summary>
        ///     Returns the mass from the array <see cref="StarMassTableByIndex" /> given an index.
        /// </summary>
        /// <param name="index">The index to retrieve </param>
        /// <exception cref="System.ArgumentException">
        ///     Throws an ArgumentException if this is out of bounds (below 0 and above the
        ///     length.)
        /// </exception>
        /// <returns>The mass</returns>
        public static double GetMassByIndex( int index )
        {
            if (index < 0 && index > StarMassTableByIndex.Length)
            {
                throw new ArgumentException("The passed index is beyond limits");
            }

            return StarMassTableByIndex[index];
        }

        /// <summary>
        ///     This function gets the current index in the mass table by the current mass
        /// </summary>
        /// <param name="mass">The mass we're looking for</param>
        /// <returns>The index</returns>
        public static int GetStellarMassPos( double mass )
        {
            for (var i = 0; i < StarMassTableByIndex.Length; i++)
            {
                if (mass == StarMassTableByIndex[i])
                {
                    return i;
                }

                if (i != StarMassTableByIndex.Length - 1 && mass < StarMassTableByIndex[i] && mass > StarMassTableByIndex[i + 1])
                {
                    return i;
                }

                if (i == StarMassTableByIndex.Length - 1)
                {
                    return i;
                }
            }

            return -1;
        }

        public static string GetStellarTypeFromMass( double mass )
        {
            if (mass <= .125)
            {
                return "M7";
            }
            if (.125 < mass && mass <= .175)
            {
                return "M6";
            }
            if (.175 < mass && mass <= .225)
            {
                return "M5";
            }
            if (.225 < mass && mass <= .325)
            {
                return "M4";
            }
            if (.325 < mass && mass <= .375)
            {
                return "M3";
            }
            if (.375 < mass && mass <= .425)
            {
                return "M2";
            }
            if (.425 < mass && mass <= .475)
            {
                return "M1";
            }
            if (.475 < mass && mass <= .525)
            {
                return "M0";
            }
            if (.525 < mass && mass <= .575)
            {
                return "K8";
            }
            if (.575 < mass && mass <= .625)
            {
                return "K6";
            }
            if (.625 < mass && mass <= .675)
            {
                return "K5";
            }
            if (.675 < mass && mass <= .725)
            {
                return "K4";
            }
            if (.725 < mass && mass <= .775)
            {
                return "K2";
            }
            if (.775 < mass && mass <= .825)
            {
                return "K0";
            }
            if (.825 < mass && mass <= .875)
            {
                return "G8";
            }
            if (.875 < mass && mass <= .925)
            {
                return "G6";
            }
            if (.925 < mass && mass <= .975)
            {
                return "G4";
            }
            if (.975 < mass && mass <= 1.025)
            {
                return "G2";
            }
            if (1.025 < mass && mass <= 1.075)
            {
                return "G1";
            }
            if (1.075 < mass && mass <= 1.125)
            {
                return "G0";
            }
            if (1.175 < mass && mass <= 1.20)
            {
                return "F9";
            }
            if (1.20 < mass && mass <= 1.225)
            {
                return "F8";
            }
            if (1.225 < mass && mass <= 1.275)
            {
                return "F7";
            }
            if (1.275 < mass && mass <= 1.325)
            {
                return "F6";
            }
            if (1.325 < mass && mass <= 1.375)
            {
                return "F5";
            }
            if (1.375 < mass && mass <= 1.425)
            {
                return "F4";
            }
            if (1.425 < mass && mass <= 1.475)
            {
                return "F3";
            }
            if (1.475 < mass && mass <= 1.55)
            {
                return "F2";
            }
            if (1.55 < mass && mass <= 1.65)
            {
                return "F0";
            }
            if (1.65 < mass && mass <= 1.75)
            {
                return "A9";
            }
            if (1.75 < mass && mass <= 1.85)
            {
                return "A7";
            }
            if (1.85 < mass && mass <= 1.95)
            {
                return "A6";
            }
            if (1.95 < mass && mass <= 2.0)
            {
                return "A5";
            }

            return "X0";
        }

        public static string GetStellarTypeFromTemp( double temp )
        {
            if (temp < 3150)
            {
                return "M7";
            }
            if (3150 <= temp && temp < 3175)
            {
                return "M6";
            }
            if (3175 <= temp && temp < 3250)
            {
                return "M5";
            }
            if (3250 <= temp && temp < 3350)
            {
                return "M4";
            }
            if (3350 <= temp && temp < 3450)
            {
                return "M3";
            }
            if (3450 <= temp && temp < 3550)
            {
                return "M2";
            }
            if (3550 <= temp && temp < 3700)
            {
                return "M1";
            }
            if (3700 <= temp && temp < 3900)
            {
                return "M0";
            }
            if (3900 <= temp && temp < 4100)
            {
                return "K8";
            }
            if (4100 <= temp && temp < 4300)
            {
                return "K6";
            }
            if (4300 <= temp && temp < 4500)
            {
                return "K5";
            }
            if (4500 <= temp && temp < 4750)
            {
                return "K4";
            }
            if (4750 <= temp && temp < 5050)
            {
                return "K2";
            }
            if (5050 <= temp && temp < 5300)
            {
                return "K0";
            }
            if (5300 <= temp && temp < 5450)
            {
                return "G8";
            }
            if (5450 <= temp && temp < 5600)
            {
                return "G6";
            }
            if (5600 <= temp && temp < 5750)
            {
                return "G4";
            }
            if (5750 <= temp && temp < 5850)
            {
                return "G2";
            }
            if (5850 <= temp && temp < 5950)
            {
                return "G1";
            }
            if (5950 <= temp && temp < 6050)
            {
                return "G0";
            }
            if (6050 <= temp && temp < 6150)
            {
                return "F9";
            }
            if (6150 <= temp && temp < 6350)
            {
                return "F8";
            }
            if (6350 <= temp && temp < 6450)
            {
                return "F7";
            }
            if (6450 <= temp && temp < 6550)
            {
                return "F6";
            }
            if (6550 <= temp && temp < 6650)
            {
                return "F5";
            }
            if (6650 <= temp && temp < 6750)
            {
                return "F4";
            }
            if (6750 <= temp && temp < 6950)
            {
                return "F3";
            }
            if (6950 <= temp && temp < 7150)
            {
                return "F2";
            }
            if (7150 <= temp && temp < 7400)
            {
                return "F0";
            }
            if (7400 <= temp && temp < 7650)
            {
                return "A9";
            }
            if (7650 <= temp && temp < 7900)
            {
                return "A7";
            }
            if (7900 <= temp && temp < 8100)
            {
                return "A6";
            }
            if (8100 <= temp && temp < 8300)
            {
                return "A5";
            }

            return "X0";
        }

        protected virtual double GetMinLumin()
        {
            const double tmpDbl = 0.0;

            if (CurrMass >= MinLuminTable[33][0])
            {
                return 16;
            }
            //basic change: if it's above the mass, just throw a high mass star

            for (var i = 0; i < MinLuminTable.Length; i++)
            {
                if (!( CurrMass >= MinLuminTable[i][0] ) || !( CurrMass < MinLuminTable[i + 1][0] ))
                {
                    continue;
                }
                //get the minimum mass and mass Range
                var minMass = MinLuminTable[i][0];
                var massRange = CurrMass - minMass;

                //get the minimum lumin and range
                var minLumin = MinLuminTable[i][1];
                var luminRange = MinLuminTable[i + 1][1] - MinLuminTable[i][1];

                return minLumin + massRange / ( MinLuminTable[i + 1][0] - minMass ) * luminRange;
            }
            return tmpDbl;
        }

        public static double GetMinLumin( double mass )
        {
            const double tmpDbl = 0.0;

            if (mass >= MinLuminTable[33][0])
            {
                return 16;
            }
            //basic change: if it's above the mass, just throw a high mass star

            for (var i = 0; i < MinLuminTable.Length; i++)
            {
                if (!( mass >= MinLuminTable[i][0] ) || !( mass < MinLuminTable[i + 1][0] ))
                {
                    continue;
                }
                //get the minimum mass and mass Range
                var minMass = MinLuminTable[i][0];
                var massRange = mass - minMass;

                //get the minimum lumin and range
                var minLumin = MinLuminTable[i][1];
                var luminRange = MinLuminTable[i + 1][1] - MinLuminTable[i][1];

                return minLumin + massRange / ( MinLuminTable[i + 1][0] - minMass ) * luminRange;
            }
            return tmpDbl;
        }

        protected static double GetMaxLumin( double currMass )
        {
            //this will determine the maximum luminosity for a star
            if (currMass < .45 || currMass > 2.0)
            {
                return -1;
            }
            var tmpDbl = 4.989914231 * Math.Pow(currMass, 4) - 17.79087942 * Math.Pow(currMass, 3) + 28.85126179 * Math.Pow(currMass, 2);
            tmpDbl = tmpDbl - 18.49923946 * currMass + 4.052052039;
            tmpDbl = Math.Round(tmpDbl, 4);
            return tmpDbl;
        }

        /// <summary>
        ///     Calculates the inital temperature of a star, given it's mass
        /// </summary>
        /// <param name="currMass">The mass of a star being calculated</param>
        /// <returns>The initial effective temperature</returns>
        public static double GetInitTemp( double currMass )
        {
            //corrected 12 Dec 2012: Missing sign. Not the most accurate, but the table isn't super accurate either.
            var tmpDbl = -2604.2 * Math.Pow(currMass, 6) + 14710 * Math.Pow(currMass, 5) - 29246 * Math.Pow(currMass, 4);
            tmpDbl += 22255 * Math.Pow(currMass, 3) - 2083 * Math.Pow(currMass, 2) - 449.86 * currMass + 3214.2;
            tmpDbl = Math.Round(tmpDbl, 2);

            return tmpDbl;
        }

        public static double GetRadius( double mass, double effTemp, double currLumin, int currentAgeGroup )
        {
            if (currentAgeGroup != StarAgeLine.RetCollaspedstar)
            {
                return 155000 * Math.Sqrt(currLumin) / Math.Pow(effTemp, 2);
            }
            return 0.0000425875 * Math.Pow(mass, -0.33);
        }

        //these set age markers
        public static double FindMainLimit( double currMass )
        {
            //determines the main sequence limit
            if (currMass < .45)
            {
                return 1300.0; //set for an extremely high number.
            }
            var tmpDbl = 39.5535698038 * Math.Pow(currMass, 4) - 247.56796104217 * Math.Pow(currMass, 3);
            tmpDbl += 580.40142164746 * Math.Pow(currMass, 2) - 610.07037160674 * currMass + 247.75169730288;
            tmpDbl = Math.Round(tmpDbl, 3);

            return tmpDbl;
        }

        public static double FindSubLimit( double currMass )
        {
            //determines the sub span limit
            var tmpDbl = 1.9998950042437 * Math.Pow(currMass, 4) - 14.088628035713 * Math.Pow(currMass, 3) + 37.565459826918 * Math.Pow(currMass, 2) - 45.463378074726 * currMass + 21.565798170783;
            tmpDbl = Math.Round(tmpDbl, 3);

            return tmpDbl;
        }

        public static double CalcOrbitalPeriod( double orbitMass, double srcMass, double orbitalRadius )
        {
            return Math.Sqrt(Math.Pow(orbitalRadius, 3) / ( orbitMass + srcMass ));
        }

        /**
         */

        public static double FindGiantLimit( double currMass )
        {
            //determines the giant limit
            var tmpDbl = 4.533 * Math.Pow(currMass, 6) - 42.472 * Math.Pow(currMass, 5) + 164.88 * Math.Pow(currMass, 4) - 340.31 * Math.Pow(currMass, 3) + 395.58 * Math.Pow(currMass, 2) - 247.54 * currMass + 66.283;

            tmpDbl = Math.Round(tmpDbl, 3);

            return tmpDbl;
        }

        //generates the formation numbers
        public static double InnerRadius( double initLumin, double initMass )
        {
            var lumFactor = Math.Sqrt(initLumin);
            if (.1 * initMass > .01 * lumFactor)
            {
                return .1 * initMass;
            }
            return Math.Round(.01 * lumFactor, 3);
        }

        public static double OuterRadius( double initMass )
        {
            return Math.Round(40 * initMass, 3);
        }

        public static double SnowLine( double initLumin )
        {
            return 4.85 * Math.Sqrt(initLumin);
        }

        public virtual Range GenerateFormationRange()
        {
            return new Range(InnerRadius(InitLumin, InitMass), OuterRadius(InitMass));
        }

        //forbidden zone calcs
        public virtual double GetInnerForbiddenZone()
        {
            return Math.Round(GetPeriapsis(OrbitalEccent, OrbitalRadius) / 3, 3);
        }

        public virtual double GetOuterForbiddenZone()
        {
            return Math.Round(3 * GetApapsis(OrbitalEccent, OrbitalRadius), 3);
        }

        public virtual Range GetEpistellarRange()
        {
            return new Range(.1 * InnerRadius(InitLumin, InitMass), 1.8 * InnerRadius(InitLumin, InitMass));
        }

        public virtual Range GetEccentricRange()
        {
            return new Range(.125 * SnowLine(InitLumin), .75 * SnowLine(InitLumin));
        }

        public virtual Range GetConventionalRange()
        {
            return new Range(SnowLine(InitLumin), 1.5 * SnowLine(InitLumin));
        }

        //gas giant checks (all passthrough, but simplify  the call.
        public double CheckEpiRange()
        {
            return ZonesOfInterest.VerifyRange(GetEpistellarRange());
        }

        public double CheckEccRange()
        {
            return ZonesOfInterest.VerifyRange(GetEccentricRange());
        }

        public double CheckConRange()
        {
            return ZonesOfInterest.VerifyRange(GetConventionalRange());
        }

        /// <summary>
        ///     This describes the gas giant flag.
        /// </summary>
        /// <param name="flag">The gas giant flag</param>
        /// <returns>A string containing the description of the flag</returns>
        public static string DescGasGiantFlag( int flag )
        {
            if (flag == GasgiantConventional)
            {
                return "Conventional Gas Giant";
            }
            if (flag == GasgiantEccentric)
            {
                return "Eccentric Gas Giant";
            }
            if (flag == GasgiantEpistellar)
            {
                return "Epistellar Gas Giant";
            }
            if (flag == GasgiantNone)
            {
                return "No Gas Giant";
            }

            return "GAS GIANT ERROR";
        }

        /// <summary>
        ///     Describes the self flag.
        /// </summary>
        /// <param name="flag">The self ID of the star</param>
        /// <returns>The string of the self ID</returns>
        public static string GetDescSelfFlag( int flag )
        {
            if (flag == IsPrimary)
            {
                return "Primary star";
            }
            if (flag == IsSecondary)
            {
                return "Secondary star";
            }
            if (flag == IsTrinary)
            {
                return "Trinary star";
            }
            if (flag == IsSeccomp)
            {
                return "Secondary Companion star";
            }
            if (flag == IsTricomp)
            {
                return "Trinary Companion star";
            }
            if (flag == Satellite.OrbitPrisec)
            {
                return "Primary and Secondary stars";
            }
            if (flag == Satellite.OrbitSeccom)
            {
                return "Secondary and Companion stars";
            }
            if (flag == Satellite.OrbitTricom)
            {
                return "Trinary and Companion stars";
            }
            if (flag == Satellite.OrbitPlanet)
            {
                return "Parent Planet";
            }

            return "???";
        }

        //general function
        public static double GetRatio( double x, double y )
        {
            return y / x;
        }
    }
}