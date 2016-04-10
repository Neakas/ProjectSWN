using System;
using System.Collections.Generic;
using System.Linq;
using UniverseGeneration.Range_Objects;
using UniverseGeneration.Utility;

namespace UniverseGeneration.Stellar_Bodies
{
    /* StarFlags.cs contains all the flags for the Star object
     * StarSatellite.cs contains all of the satellite options
     * 
     * 
     * 
     */

    /// <summary>
    ///     Star contains the flags and objects for.. a star. Also for it's subject planets, and formation and forbidden zones.
    ///     Is a child of <see cref="Orbital" />
    /// </summary>
    public partial class Star
    {
        //member arrays of the object

        /// <summary>
        ///     This member contains the table we roll on for stellar mass. Contains an array of size 19x19, mainly to make passing
        ///     dierolls easier.
        /// </summary>
        protected static double[][] StarMassTableByRoll =
        {
            new double[]
            {
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            }, //Index 0
            new double[]
            {
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            }, //Roll 1
            new double[]
            {
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            }, //Roll 2
            new[]
            {
                0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 1.9, 1.9, 1.9, 1.9, 1.9, 1.9, 1.9, 1.9
            }, //Roll: 3
            new[]
            {
                0, 0, 0, 1.8, 1.8, 1.8, 1.8, 1.8, 1.8, 1.7, 1.7, 1.7, 1.6, 1.6, 1.6, 1.6, 1.6, 1.6, 1.6
            }, //Roll: 4
            new[]
            {
                0, 0, 0, 1.5, 1.5, 1.5, 1.5, 1.5, 1.45, 1.45, 1.45, 1.4, 1.4, 1.35, 1.35, 1.35, 1.35, 1.35, 1.35, 1.35
            }, //Roll:5
            new[]
            {
                0, 0, 0, 1.3, 1.3, 1.3, 1.3, 1.3, 1.25, 1.25, 1.2, 1.15, 1.15, 1.1, 1.1, 1.1, 1.1, 1.1, 1.1
            }, //Roll: 6
            new[]
            {
                0, 0, 0, 1.05, 1.05, 1.05, 1.05, 1.05, 1, 1, .95, .9, .9, .85, .85, .85, .85, .85, .85
            }, //Roll: 7
            new[]
            {
                0, 0, 0, .8, .8, .8, .8, .8, .8, .75, .75, .7, .65, .65, .6, .6, .6, .6, .6, .6
            }, //Roll: 8
            new[]
            {
                0, 0, 0, .55, .55, .55, .55, .55, .55, .5, .5, .5, .45, .45, .45, .45, .45, .45, .45
            }, //Roll: 9
            new[]
            {
                0, 0, 0, .4, .4, .4, .4, .4, .4, .35, .35, .35, .3, .3, .3, .3, .3, .3, .3
            }, //Roll: 10
            new[]
            {
                0, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25, 0.25
            }, //Roll 11
            new[]
            {
                0, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2
            }, //Roll 12
            new[]
            {
                0, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15
            }, //Roll 13
            new[]
            {
                0, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1
            }, //Roll 14
            new[]
            {
                0, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1
            }, //Roll 15
            new[]
            {
                0, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1
            }, //Roll 16
            new[]
            {
                0, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1
            }, //Roll 17
            new[]
            {
                0, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1
            } //Roll 18
        };

        /// <summary>
        ///     This member contains the table we use to determine stellar mass by index.
        /// </summary>
        protected static double[] StarMassTableByIndex =
        {
            .1, .15, .2, .25, .3, .35, .4, .45, .5, .55, .6, .65, .7, .75, .8, .85, .9, .95, 1.0, 1.05, 1.1, 1.15, 1.2, 1.25, 1.3, 1.35, 1.4, 1.45, 1.5, 1.55, 1.6, 1.65, 1.7, 1.75, 1.8, 1.85, 1.9, 1.95, 2
        };

        /// <summary>
        ///     The minimum luminosity of the star, given its' mass
        /// </summary>
        public static double[][] MinLuminTable =
        {
            new[]
            {
                .1, .0012
            },
            new[]
            {
                .15, .0036
            },
            new[]
            {
                .2, .0079
            },
            new[]
            {
                .25, .015
            },
            new[]
            {
                .3, .024
            },
            new[]
            {
                .35, .037
            },
            new[]
            {
                .4, .054
            },
            new[]
            {
                .45, .07
            },
            new[]
            {
                .5, .09
            },
            new[]
            {
                .55, .11
            },
            new[]
            {
                .6, .13
            },
            new[]
            {
                .65, .15
            },
            new[]
            {
                .7, .12
            },
            new[]
            {
                .75, .23
            },
            new[]
            {
                .8, .28
            },
            new[]
            {
                .85, .36
            },
            new[]
            {
                .9, .45
            },
            new[]
            {
                .95, .56
            },
            new[]
            {
                1, .68
            },
            new[]
            {
                1.05, .87
            },
            new[]
            {
                1.1, 1.1
            },
            new[]
            {
                1.15, 1.4
            },
            new[]
            {
                1.2, 1.7
            },
            new[]
            {
                1.25, 2.1
            },
            new[]
            {
                1.3, 2.5
            },
            new[]
            {
                1.35, 3.1
            },
            new[]
            {
                1.4, 3.7
            },
            new[]
            {
                1.45, 4.3
            },
            new[]
            {
                1.5, 5.1
            },
            new[]
            {
                1.6, 6.7
            },
            new[]
            {
                1.7, 8.6
            },
            new[]
            {
                1.8, 11
            },
            new[]
            {
                1.9, 13
            },
            new double[]
            {
                2, 16
            }
        };

        /// <summary>
        ///     Base constructor, given no details.
        /// </summary>
        /// <param name="parent">The parent this star belongs to. (IsPrimary for a primary star)</param>
        /// <param name="self">The ID of this star</param>
        public Star( int parent, int self ) : base(parent, self)
        {
            OrbitalRadius = 0.0;
            GasGiantFlag = GasgiantNone; //set to none automatically. We will set it correctly later.
            EvoLine = new StarAgeLine();
            SysPlanets = new List<Satellite>();
        }

        /// <summary>
        ///     A full constructor.
        /// </summary>
        /// <param name="age">The age of the star</param>
        /// <param name="parent">The parent this star belongs to. (IsPrimary for a primary star)</param>
        /// <param name="self">The ID of this star</param>
        /// <param name="order">Where the star is in the sequence</param>
        /// <param name="baseName">The name of the system</param>
        public Star( double age, int parent, int self, int order, string baseName ) : base(parent, self)
        {
            StarAge = age;
            OrbitalRadius = 0.0;
            GasGiantFlag = GasgiantNone; //set to none automatically. We will set it correctly later.
            OrderId = order;
            Name = GenGenericName(baseName, order);
            EvoLine = new StarAgeLine();
            SysPlanets = new List<Satellite>();
        }

        /// <summary>
        ///     Constructor given the age, parent, self and Order.
        /// </summary>
        /// <param name="age">Age of the star</param>
        /// <param name="parent">The star this belongs to (for a priamry star, put IsPrimary here)</param>
        /// <param name="self">The star's ID</param>
        /// <param name="order">Where is it in the system?</param>
        public Star( double age, int parent, int self, int order ) : base(parent, self)
        {
            StarAge = age;
            OrbitalRadius = 0.0;
            GasGiantFlag = GasgiantNone;
            EvoLine = new StarAgeLine();
            OrderId = order;
            SysPlanets = new List<Satellite>();
        }

        //properties of the star

        /// <summary>
        ///     currMass is the current Mass of the star
        /// </summary>
        public double CurrMass { get; protected set; }

        /// <summary>
        ///     initMass is the initial mass of the star
        ///     <remarks>
        ///         This is one of the things placed here for white dwarves,
        ///         since they have an arbitrary changed mass.
        ///     </remarks>
        /// </summary>
        public double InitMass { get; protected set; }

        /// <summary>
        ///     The radius of the star, stored in __AU__.
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        ///     The current luminosity. Stored in __solar luminosities__
        /// </summary>
        public double CurrLumin { get; set; }

        /// <summary>
        ///     The initial luminosity. Needed to determine various elements of formation.
        /// </summary>
        public double InitLumin { get; protected set; }

        /// <summary>
        ///     The maximum luminsoity. It's used for internal factors, but generally only relevant for giant and white dwarf phase
        /// </summary>
        protected double MaxLumin { get; set; }

        /// <summary>
        ///     The effective temperature of the surface of the star.
        /// </summary>
        public double EffTemp { get; set; }

        /// <summary>
        ///     The spectral type of the star.
        /// </summary>
        public string SpecType { get; set; }

        /// <summary>
        ///     This determiners if it's a flare star
        /// </summary>
        public bool IsFlareStar { get; set; }

        /// <summary>
        ///     The age of the star. I don't LIKE storing it here, but the star needs to know it's own age in order to know where
        ///     it is.
        /// </summary>
        public double StarAge { get; set; }

        //labeling and order properties
        /// <summary>
        ///     Order ID: this stores a number from (0,9) which is used to determine the name.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        ///     Orbital Serpation Flag (Very Close, etc.)
        /// </summary>
        public int OrbitalSep { get; set; }

        //flags for planetary creation
        /// <summary>
        ///     In GURPS, whether or not you have any gas giants, and where they are, is dependent on this flag
        /// </summary>
        public int GasGiantFlag { get; set; }

        /// <summary>
        ///     This contains every orbital that planets can form in the star.
        /// </summary>
        public List<Satellite> SysPlanets { get; set; }

        /// <summary>
        ///     Soon to be removed.
        /// </summary>
        public FormationHelper ZonesOfInterest { get; set; }

        /// <summary>
        ///     Contains the segments that make up the lifespan of the star.
        /// </summary>
        public StarAgeLine EvoLine { get; set; }

        /// <summary>
        ///     This stores the distance of each star from the primary. It's needed for blackbody calculations.
        /// </summary>
        public double DistFromPrimary { get; set; }

        /// <summary>
        ///     This is the stellar color. Not always used.
        /// </summary>
        public string StarColor { get; set; }

        /// <summary>
        ///     This function returns a generic name on the scheme [SYSTEMNAME-ID]
        /// </summary>
        /// <param name="sysName">The system name</param>
        /// <param name="id">Where the star is in the system (number)</param>
        /// <returns>The generic name</returns>
        public static string GenGenericName( string sysName, int id )
        {
            char[] starNames =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I'
            };
            return sysName + "-" + starNames[id];
        }

        //zones of interest functions - both to see if it's initated and to create it.

        //init formulas
        //passthrough functions
        public void CreateForbiddenZone( Range incoming, int primary, int secondary )
        {
            ZonesOfInterest.CreateForbiddenZone(incoming, primary, secondary);
        }

        public void CreateCleanZones()
        {
            ZonesOfInterest.CreateCleanZones(InnerRadius(InitLumin, InitMass), OuterRadius(InitMass));
        }

        public void SortForbidden()
        {
            ZonesOfInterest.SortForbiddenZones();
        }

        public void SortClean()
        {
            ZonesOfInterest.SortCleanZones();
        }

        public double VerifyRange( Range incoming )
        {
            return ZonesOfInterest.VerifyRange(incoming);
        }

        public double PickInRange( Range incoming )
        {
            return ZonesOfInterest.PickInRange(incoming);
        }

        public bool VerifyCleanOrbit( double incoming )
        {
            return ZonesOfInterest.IsWithinCleanZone(incoming);
        }

        public bool WithinCreationRange( double incoming )
        {
            if (incoming >= InnerRadius(InitLumin, InitMass) && incoming <= OuterRadius(InitMass))
            {
                return true;
            }

            return false;
        }

        public List<CleanZone> GetCleanZones()
        {
            return ZonesOfInterest.FormationZones;
        }

        public bool CleanZoneHasOrbits( CleanZone clear )
        {
            return SysPlanets.Any(s => clear.WithinRange(s.OrbitalRadius));
        }

        public bool VerifyForbiddenOrbit( double incoming )
        {
            return ZonesOfInterest.IsWithinForbiddenZone(incoming);
        }

        public double GetNextCleanOrbit( double orbit, int flag )
        {
            return ZonesOfInterest.GetNextCleanOrbit(orbit, flag);
        }

        public double GetMinCleanOrbit()
        {
            return ZonesOfInterest.GetMinimalCleanZone();
        }

        public double GetMaxCleanOrbit()
        {
            return ZonesOfInterest.GetMaximalCleanZone();
        }

        public double GetRangeWidth( double orbit )
        {
            return ZonesOfInterest.GetRangeWidth(orbit);
        }

        public int GetOwnership( double orbital )
        {
            return ZonesOfInterest.GetOwnership(orbital);
        }

        public int GetAdjacencyMod( double orbital )
        {
            return ZonesOfInterest.GetAdjacencyMod(orbital);
        }

        public double PickInCurrentRange( double orbit, double minLimit )
        {
            double retValue;

            if (ZonesOfInterest.GetRangeWidth(orbit) < minLimit)
            {
                do
                {
                    retValue = ZonesOfInterest.PickInRange(ZonesOfInterest.GetRange(orbit));
                }
                while (Math.Abs(retValue - orbit) < 0.01);
            }
            else
            {
                do
                {
                    retValue = ZonesOfInterest.PickInRange(ZonesOfInterest.GetRange(orbit));
                }
                while (retValue < orbit + minLimit);
            }

            return retValue;
            //return this.zonesOfInterest.pickInRange(this.zonesOfInterest.getRange(orbit));
        }

        public virtual bool IsAllEmptyOrbits()
        {
            return SysPlanets.All(s => s.BaseType == Satellite.BasetypeEmpty);
        }

        //mass functionality
        public virtual void UpdateMass( double mass, bool isWhiteDwarf = false )
        {
            if (mass == 0.00)
            {
                throw new Exception("Mass is 0 solar masses.");
            }

            CurrMass = mass;

            EvoLine.AddMainLimit(FindMainLimit(CurrMass));
            EvoLine.AddSubLimit(FindSubLimit(CurrMass));
            EvoLine.AddGiantLimit(FindGiantLimit(CurrMass));

            if (!isWhiteDwarf)
            {
                InitMass = mass;
            }
        }

        /// <summary>
        ///     Sets the order of this star
        /// </summary>
        /// <param name="orderId">The order of this star in the system</param>
        public virtual void AddOrder( int orderId )
        {
            OrderId = orderId;
        }

        /// <summary>
        ///     This updates the star to what the current should be given no alterations.
        /// </summary>
        /// <param name="ageL">the age line of the star</param>
        /// <param name="age">The age of the star</param>
        /// <param name="mass">The current mass of the star</param>
        /// <returns>The current lumonsity of the star</returns>
        public static double GetCurrLumin( StarAgeLine ageL, double age, double mass )
        {
            var ageGroup = ageL.FindCurrentAgeGroup(age);

            if (ageGroup == StarAgeLine.RetMainbranch && mass < .45) //if it's under .45 solar masses, it'll always be the minimum luminosity.
            {
                return GetMinLumin(mass);
            }
            if (ageGroup == StarAgeLine.RetMainbranch && mass >= .45) // now it's going to be somewhere between the minimum and maximum, given it's age.
            {
                return GetMinLumin(mass) + age / ageL.GetMainLimit() * ( GetMaxLumin(mass) - GetMinLumin(mass) );
            }
            if (ageGroup == StarAgeLine.RetSubbranch) //simply maxmium luminsoity
            {
                return GetMaxLumin(mass);
            }
            if (ageGroup == StarAgeLine.RetGiantbranch)
            {
                return GetMaxLumin(mass) * 10000; //IMPLEMENTED HOUSE RULE. Yeah. Uh.. Yeah.
            }
            if (ageGroup == StarAgeLine.RetCollaspedstar)
            {
                return 1611047115.0 * mass * Math.Pow(ageL.GetAgeFromCollapse(age) * 100000000, -7.0 / 5.0); //corrected from report.
            }

            return 0;
        }

        /// <summary>
        ///     This updates the star to the current surface temperature given no alterations
        /// </summary>
        /// <param name="ageL">the age line of the star</param>
        /// <param name="lumin">The current luminosity of the star (used for White Dwarfs)</param>
        /// <param name="age">The age of the star</param>
        /// <param name="mass">The current mass of the star</param>
        /// <param name="ourDice">Ddice (due to randomization of the temperature)</param>
        /// <returns>The current temperature of the star</returns>
        public static double GetCurrentTemp( StarAgeLine ageL, double lumin, double age, double mass, Dice ourDice )
        {
            if (ageL.FindCurrentAgeGroup(age) == StarAgeLine.RetMainbranch)
            {
                return GetInitTemp(mass);
            }
            if (ageL.FindCurrentAgeGroup(age) == StarAgeLine.RetSubbranch)
            {
                return GetInitTemp(mass) - ageL.CalcWithInSubLimit(age) * ( GetInitTemp(mass) - 4800 );
            }
            if (ageL.FindCurrentAgeGroup(age) == StarAgeLine.RetGiantbranch)
            {
                return 3000 + ourDice.Rng(2, 6, -2) * 200;
            }
            return ageL.FindCurrentAgeGroup(age) == StarAgeLine.RetCollaspedstar ? Math.Pow(lumin / Math.Pow(GetRadius(mass, 0, lumin, StarAgeLine.RetCollaspedstar), 2) * ( 5.38937375 * Math.Pow(10, 26) ), 1.00 / 4) : 0;
        }

        /// <summary>
        ///     This function sets the inital luminosity of a Star object
        /// </summary>
        public virtual void SetLumin()
        {
            var currAgeGroup = EvoLine.FindCurrentAgeGroup(StarAge);
            InitLumin = GetMinLumin(); //this applies for most stars.

            CurrLumin = GetCurrLumin(EvoLine, StarAge, CurrMass);

            //set the maximum luminosity. Used for determining the formation zones if the star is in a few phases.
            if (currAgeGroup == StarAgeLine.RetGiantbranch)
            {
                MaxLumin = CurrLumin;
            }

            if (currAgeGroup == StarAgeLine.RetCollaspedstar)
            {
                MaxLumin = GetMinLumin(CurrMass) * 10000;
            }
        }

        public virtual void UpdateLumin( double lumin )
        {
            var currStatus = EvoLine.FindCurrentAgeGroup(StarAge);

            if (currStatus == StarAgeLine.RetMainbranch && CurrMass < .45)
            {
                CurrLumin = lumin;
                InitLumin = lumin;
            }

            if (( currStatus == StarAgeLine.RetMainbranch && CurrMass >= .45 ) || currStatus == StarAgeLine.RetSubbranch || currStatus == StarAgeLine.RetGiantbranch || currStatus == StarAgeLine.RetGiantbranch)
            {
                CurrLumin = lumin;
            }
        }

        //returns the radius in terms of either KM or AU.
        public virtual double GetRadiusAu()
        {
            return Radius;
        }

        public virtual double GetRadiusKm()
        {
            return Radius * AUtoKm;
        }

        //update temp.
        public virtual void UpdateTemp( double effTemp )
        {
            if (EvoLine.FindCurrentAgeGroup(StarAge) == StarAgeLine.RetSubbranch || EvoLine.FindCurrentAgeGroup(StarAge) == StarAgeLine.RetGiantbranch)
            {
                EffTemp = effTemp;
                SpecType = GetStellarTypeFromTemp(EffTemp);
            }
            if (EvoLine.FindCurrentAgeGroup(StarAge) == StarAgeLine.RetMainbranch)
            {
                EffTemp = effTemp;
            }
        }

        //describes the age status.
        public virtual string GetStatusDesc()
        {
            if (EvoLine.FindCurrentAgeGroup(StarAge) == StarAgeLine.RetMainbranch)
            {
                return "Main Sequence";
            }
            if (EvoLine.FindCurrentAgeGroup(StarAge) == StarAgeLine.RetSubbranch)
            {
                return "Subgiant Branch";
            }
            if (EvoLine.FindCurrentAgeGroup(StarAge) == StarAgeLine.RetGiantbranch)
            {
                return "Asymptotic Giant Branch";
            }
            if (EvoLine.FindCurrentAgeGroup(StarAge) == StarAgeLine.RetCollaspedstar)
            {
                return "White Dwarf";
            }

            return "INVALID STATUS";
        }

        /// <summary>
        ///     This sets the spectral type of our star.
        /// </summary>
        public virtual void SetSpectralType()
        {
            if (EvoLine.FindCurrentAgeGroup(StarAge) == StarAgeLine.RetMainbranch)
            {
                SpecType = GetStellarTypeFromMass(CurrMass) + " V";
            }

            if (EvoLine.FindCurrentAgeGroup(StarAge) == StarAgeLine.RetSubbranch)
            {
                SpecType = GetStellarTypeFromTemp(CurrMass) + " IV";
            }

            if (EvoLine.FindCurrentAgeGroup(StarAge) == StarAgeLine.RetGiantbranch)
            {
                SpecType = GetStellarTypeFromTemp(CurrMass) + " II";
            }

            //the fun one - white dwarf
            if (EvoLine.FindCurrentAgeGroup(StarAge) == StarAgeLine.RetCollaspedstar)
            {
                if (EffTemp >= 1000)
                {
                    SpecType = "DA";
                }
                if (EffTemp >= 300 && EffTemp < 1000)
                {
                    SpecType = "DB";
                }
                if (EffTemp < 300)
                {
                    SpecType = "DC";
                }
            }
        }

        public static string SetColor( Dice ourDice, double effTemp )
        {
            if ((bool) !OptionCont.FantasyColors)
            {
                if (effTemp >= 33000)
                {
                    return "Blue";
                }
                if (effTemp >= 10000 && effTemp < 33000)
                {
                    return "Blue-White";
                }
                if (effTemp >= 7500 && effTemp < 10000)
                {
                    return "Whitish Blue";
                }
                if (effTemp >= 6000 && effTemp < 7500)
                {
                    return "White";
                }
                if (effTemp >= 5200 && effTemp < 6000)
                {
                    return "Yellow";
                }
                if (effTemp >= 4250 && effTemp < 5200)
                {
                    return "Yellowish Orange";
                }
                if (effTemp >= 3700 && effTemp < 4250)
                {
                    return "Orange";
                }
                if (effTemp >= 2000 && effTemp < 3700)
                {
                    return "Orangish Red";
                }
                if (effTemp >= 1300 && effTemp < 2000)
                {
                    return "Red";
                }
                if (effTemp >= 700 && effTemp < 1300)
                {
                    return "Purplish Red";
                }
                if (effTemp >= 100 && effTemp < 700)
                {
                    return "Brown";
                }
                if (effTemp < 100)
                {
                    return "Black";
                }
            }
            else
            {
                var roll = ourDice.Rng(100019);
                if (LibStarGen.Inbetween(roll, 0, 10))
                {
                    return "Black";
                }
                if (LibStarGen.Inbetween(roll, 11, 531))
                {
                    return "Green";
                }
                if (LibStarGen.Inbetween(roll, 532, 952))
                {
                    return "Yellow-Green";
                }
                if (LibStarGen.Inbetween(roll, 953, 6057))
                {
                    return "Red-Orange";
                }
                if (LibStarGen.Inbetween(roll, 6058, 6835))
                {
                    return "Blue";
                }
                if (LibStarGen.Inbetween(roll, 6836, 11940))
                {
                    return "Purple-Red";
                }
                if (LibStarGen.Inbetween(roll, 11941, 23948))
                {
                    return "Red";
                }
                if (LibStarGen.Inbetween(roll, 23949, 49960))
                {
                    return "Yellow";
                }
                if (LibStarGen.Inbetween(roll, 49961, 75972))
                {
                    return "Orange";
                }
                if (LibStarGen.Inbetween(roll, 75973, 87980))
                {
                    return "Yellow-Orange";
                }
                if (LibStarGen.Inbetween(roll, 87981, 93085))
                {
                    return "Blue-White";
                }
                if (LibStarGen.Inbetween(roll, 93086, 93763))
                {
                    return "White";
                }
                if (LibStarGen.Inbetween(roll, 93764, 98868))
                {
                    return "White-Blue";
                }
                if (LibStarGen.Inbetween(roll, 98869, 99289))
                {
                    return "Green-Blue";
                }
                if (LibStarGen.Inbetween(roll, 99290, 99710))
                {
                    return "Blue-Violet";
                }
                if (LibStarGen.Inbetween(roll, 99711, 100019))
                {
                    return "Purple";
                }
            }

            return "ERROR";
        }

        public virtual bool TestInitlizationZones()
        {
            if (ZonesOfInterest == null)
            {
                return false;
            }
            return true;
        }

        public virtual void InitalizeZonesOfInterest()
        {
            ZonesOfInterest = new FormationHelper(SelfId);
        }

        public void SortForbiddenZones()
        {
            ZonesOfInterest.SortForbiddenZones();
        }

        public void SortCleanZones()
        {
            ZonesOfInterest.SortCleanZones();
        }

        public double GetClosestDistToForbiddenZone( double orbit )
        {
            return ZonesOfInterest.GetClosestDistFromForbiddenZone(orbit);
        }

        public double GetClosestForbiddenZoneRatio( double orbit )
        {
            return ZonesOfInterest.GetClosestForbiddenZoneRatio(orbit);
        }

        public bool ContainsGasGiants( bool pastSnowLine = true )
        {
            foreach (var s in SysPlanets)
            {
                if (!pastSnowLine)
                {
                    if (s.SatelliteType == Satellite.BasetypeGasgiant)
                    {
                        return true;
                    }
                    else if (s.SatelliteType == Satellite.BasetypeGasgiant && s.OrbitalRadius > SnowLine(InitLumin))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //checks the previous satellite to see if it's a gas giant
        public bool IsPrevSatelliteGasGiant( double orbitalRadius )
        {
            for (var i = 0; i < SysPlanets.Count; i++)
            {
                if (SysPlanets[i].OrbitalRadius != orbitalRadius)
                {
                    continue;
                }
                if (i == 0)
                {
                    return false;
                }
                if (SysPlanets[i - 1].BaseType == Satellite.BasetypeGasgiant)
                {
                    return true;
                }
            }

            return false;
        }

        //checks the next satellite to see if it's a gas giant
        public bool IsNextSatelliteGasGiant( double orbitalRadius )
        {
            for (var i = 0; i < SysPlanets.Count; i++)
            {
                if (SysPlanets[i].OrbitalRadius != orbitalRadius)
                {
                    continue;
                }
                if (i == SysPlanets.Count - 1)
                {
                    return false;
                }
                if (SysPlanets[i + 1].BaseType == Satellite.BasetypeGasgiant)
                {
                    return true;
                }
            }

            return false;
        }

        public string PrintSummaryLine( string intro = "" )
        {
            string desc;

            if (SelfId != IsPrimary)
            {
                desc = intro + " " + CurrMass + " solar masses, " + Math.Round(CurrLumin, OptionCont.NumberOfDecimal) + " solar luminosities. Eff Temp: " + EffTemp + "K, apparent color ";
                desc += StarColor + ". This star orbits " + GetDescSelfFlag(ParentId) + " at " + OrbitalRadius + "AU out with an eccentricity of " + OrbitalEccent;
            }
            else
            {
                desc = intro + " " + CurrMass + " solar masses, " + Math.Round(CurrLumin, OptionCont.NumberOfDecimal) + " solar luminosities. Eff Temp: " + EffTemp + "K, apparent color ";
                desc += StarColor;
            }

            return desc;
        }

        /// <summary>
        ///     This function prints the Periapsis and Apapsis of stars around an orbiting primary.
        /// </summary>
        /// <returns></returns>
        public string PrintOrbitalDetails()
        {
            if (SelfId == IsPrimary)
            {
                return "N/A";
            }
            return "Periapsis - " + Math.Round(GetPeriapsis(OrbitalEccent, OrbitalRadius), OptionCont.NumberOfDecimal) + " AU. Apapsis - " + Math.Round(GetApapsis(OrbitalEccent, OrbitalRadius), OptionCont.NumberOfDecimal);
        }

        public override string ToString()
        {
            var nL = Environment.NewLine + "    ";

            var ret = Name + " is a " + GetStatusDesc() + " star with spectral type " + SpecType;
            ret = ret + nL + "This star has " + CurrMass + " solar masses, and a current luminosity of " + Math.Round(CurrLumin, OptionCont.NumberOfDecimal);
            ret = ret + nL + "solar luminosities. It has a surface temperature of " + Math.Round(EffTemp, OptionCont.NumberOfDecimal) + "K.";
            ret = ret + nL + "This star's radius is " + Math.Round(GetRadiusAu(), OptionCont.NumberOfDecimal) + " AU.";
            ret = ret + nL + "Apparent Color : " + StarColor;

            if (OptionCont.GetVerboseOutput())
            {
                ret = ret + Environment.NewLine;
                ret = ret + nL + "Initial Luminosity: " + InitLumin + " solar luminosities.";
                ret = ret + nL + "Initial Mass: " + InitMass + " solar masses";
                ret = ret + nL + "Formation Zones: " + InnerRadius(InitLumin, InitMass) + " AU to " + Math.Round(OuterRadius(InitMass), OptionCont.NumberOfDecimal) + " AU";
                ret = ret + nL + "Snow Line: " + Math.Round(SnowLine(InitLumin), OptionCont.NumberOfDecimal) + " AU.";
            }

            ret = ret + Environment.NewLine;
            if (IsFlareStar)
            {
                ret = ret + nL + "This star is a flare star.";
            }

            ret = ret + Environment.NewLine;

            if (OptionCont.GetVerboseOutput())
            {
                ret = ret + nL + "Self ID: " + GetDescSelfFlag(SelfId) + " and Parent ID: " + GetDescSelfFlag(ParentId);
                ret = ret + nL;
            }

            //printing out age details
            ret = ret + nL + "Evolution Data";
            ret = ret + Environment.NewLine;

            if (EvoLine.GetGiantLimit() < 1000)
            {
                ret = ret + nL + "Main Sequence Ends: " + EvoLine.GetMainLimit() + " Gyr,";
                ret = ret + " Subgiant Ends: " + EvoLine.GetSubLimit() + " Gyr";
                ret = ret + nL + "Giant Stage Ends: " + EvoLine.GetGiantLimit() + " Gyr";

                if (StarAge < EvoLine.GetMainLimit())
                {
                    ret = ret + nL + "This star will exit the main sequence phase in: " + ( EvoLine.GetMainLimit() - StarAge ) + " Gyr";
                }
                if (StarAge >= EvoLine.GetMainLimit() && StarAge < EvoLine.GetSubLimit())
                {
                    ret = ret + nL + "This star will exit the subgiant phase in: " + ( EvoLine.GetSubLimit() - StarAge ) + " Gyr";
                }
                if (StarAge >= EvoLine.GetSubLimit() && StarAge < EvoLine.GetGiantLimit())
                {
                    ret = ret + nL + "This star will exit the giant phase in: " + ( EvoLine.GetGiantLimit() - StarAge ) + " Gyr";
                }
                if (StarAge >= EvoLine.GetGiantLimit())
                {
                    ret = ret + nL + "This star has been a white dwarf for: " + ( StarAge - EvoLine.GetGiantLimit() ) + " Gyr";
                }
            }

            else
            {
                ret = ret + nL + "This star will burn out sometime well after the galaxy disappears.";
            }

            if (SelfId != IsPrimary)
            {
                ret = ret + Environment.NewLine;
                ret = ret + nL + "Orbital Details";
                ret = ret + nL + "This orbits " + ParentName + " at " + OrbitalRadius + " AU.";

                if (OrbitalEccent > 0)
                {
                    ret = ret + nL + "Eccentricity: " + OrbitalEccent + ".";
                    ret = ret + nL + "Periapsis: " + GetPeriapsis(OrbitalEccent, OrbitalRadius) + " AU and Apapasis: " + GetApapsis(OrbitalEccent, OrbitalRadius) + " AU.";
                }

                ret = ret + nL + "Orbital period is " + Math.Round(OrbitalPeriod, 2) + " years (" + Math.Round(OrbitalPeriod * 365.25, 2);
                ret = ret + " days)";
                ret = ret + nL + "This has a seperation of " + LibStarGen.GetSeperationStr(OrbitalSep);
            }

            //ret = ret + nL;
            //ret = ret + nL + "Orbital Details";
            //foreach (Satellite s in this.sysPlanets)
            //{
            //    ret = ret + nL + s;
            //    ret = ret + nL;
            //}
            //ret = ret + nL;

            if (!OptionCont.GetVerboseOutput())
            {
                return ret;
            }
            ret = ret + nL;
            ret = ret + nL + "Formation Zone Details";
            ret = ret + nL;
            ret = ZonesOfInterest.ForbiddenZones.Aggregate(ret, ( current, r ) => current + nL + r);
            ret = ret + nL;
            ret = ZonesOfInterest.FormationZones.Aggregate(ret, ( current, r ) => current + nL + r);
            ret = ret + nL;
            ret = ret + nL + "Gas Giant Flag: " + DescGasGiantFlag(GasGiantFlag);
            ret = ret + nL;

            return ret;
        }

        public static void GenerateEccentricity( int roll, Star s )
        {
            //set the eccentricity
            if (roll <= 3)
            {
                s.OrbitalEccent = 0;
            }
            if (roll == 4)
            {
                s.OrbitalEccent = .1;
            }
            if (roll == 5)
            {
                s.OrbitalEccent = .2;
            }
            if (roll == 6)
            {
                s.OrbitalEccent = .3;
            }
            if (roll == 7 || roll == 8)
            {
                s.OrbitalEccent = .4;
            }
            if (roll >= 9 && roll <= 11)
            {
                s.OrbitalEccent = .5;
            }
            if (roll == 12 || roll == 13)
            {
                s.OrbitalEccent = .6;
            }
            if (roll == 14 || roll == 15)
            {
                s.OrbitalEccent = .7;
            }
            if (roll == 16)
            {
                s.OrbitalEccent = .8;
            }
            if (roll == 17)
            {
                s.OrbitalEccent = .9;
            }
            if (roll >= 18)
            {
                s.OrbitalEccent = .95;
            }
        }

        public static string GetDescFromFlag( int flag )
        {
            if (flag == IsPrimary)
            {
                return "Primary Star";
            }
            if (flag == IsSecondary)
            {
                return "Secondary Star";
            }
            if (flag == IsSeccomp)
            {
                return "Secondary Companion";
            }
            if (flag == IsTrinary)
            {
                return "Trinary Star";
            }
            if (flag == IsTricomp)
            {
                return "Trinary Companion";
            }

            return "[ERROR]";
        }

        /// <summary>
        ///     This returns the current branch description for this star
        /// </summary>
        /// <returns>A string containing the branch description</returns>
        public string ReturnCurrentBranchDesc()
        {
            return StarAgeLine.DescBranch(EvoLine.FindCurrentAgeGroup(StarAge));
        }

        public void GiveOrbitalsOrder( ref int totalOrbitals )
        {
            var currOrb = 0;
            foreach (var s in SysPlanets)
            {
                s.SelfId = currOrb;
                s.MasterOrderId = totalOrbitals;

                totalOrbitals++;
                currOrb++;
            }
        }
    }
}