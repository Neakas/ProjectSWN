using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UniverseGeneration.Utility;

namespace UniverseGeneration.Stellar_Bodies
{
    /// <summary>
    ///     The object for satelites and moons in this object
    /// </summary>
    public class Satellite : Orbital
    {
        //flags!

        // generic error flags
        // mainly used to release error conditions from functions

        /// <summary>
        ///     FLAG: The orbit has an error (or could not be correctly added)
        /// </summary>
        public static readonly int ErrorOrbit = -1;

        /// <summary>
        ///     FLAG: The basetype has an error (or could not be correctly set)
        /// </summary>
        public static readonly int ErrorBasetype = -2;

        /// <summary>
        ///     FLAG: The subtype has an error (or could not be correctly set)
        /// </summary>
        public static readonly int ErrorSubtype = -3;

        /// <summary>
        ///     FLAG: The size has an error (or could not be correctly set)
        /// </summary>
        public static readonly int ErrorSize = -4;

        /// <summary>
        ///     FLAG: The atmosphere size has an error (or could not be correctly set)
        /// </summary>
        public static readonly int ErrorAtm = -5;

        /// <summary>
        ///     FLAG: The atmosphere condition has an error (or could not be correctly set)
        /// </summary>
        public static readonly int ErrorAtmcond = -6;

        /// <summary>
        ///     FLAG: Unknown error!
        /// </summary>
        public static readonly int ErrorGeneric = -7;

        /// <summary>
        ///     The amount of gravity on Earth.
        /// </summary>
        public static readonly double Gforce = 9.80665;

        // owner flags.
        // we use the star ids when it's just one star, though.
        public static readonly int OrbitPrisec = 9102; //primary and secondary
        public static readonly int OrbitPrisectri = 9103; //all three
        public static readonly int OrbitPritri = 9106; //shouldn't happen .(Primary and Trinary)
        public static readonly int OrbitSeccom = 9104; //secondary and companion
        public static readonly int OrbitTricom = 9105; //trinary and companion
        public static readonly int OrbitSectri = 9107; //this so shouldn't happen. Secondary and Trinary.
        public static readonly int OrbitPlanet = 9108; //for moons

        // base type flags
        // these are used because certain things need to check what the base type is.
        public static readonly int BasetypeMoon = 210;
        public static readonly int BasetypeAsteroidbelt = 211;
        public static readonly int BasetypeEmpty = 212;
        public static readonly int BasetypeGasgiant = 220;
        public static readonly int BasetypeTerrestial = 230;

        public static readonly int BasetypeUnset = 999;

        //sub type flags: i.e a Terrestial(Ice) Small, for example.
        public static readonly int SubtypeIce = 231;
        public static readonly int SubtypeRock = 232;
        public static readonly int SubtypeSulfur = 233;
        public static readonly int SubtypeHadean = 234;
        public static readonly int SubtypeAmmonia = 235;
        public static readonly int SubtypeGarden = 236;
        public static readonly int SubtypeOcean = 237;
        public static readonly int SubtypeGreenhouse = 238;
        public static readonly int SubtypeChthonian = 239;

        // size flags
        // grouped into related sizes for different base types.
        public static readonly int SizeUnset = BasetypeUnset;
        public static readonly int SizeTiny = 11;
        public static readonly int SizeSmall = 12;
        public static readonly int SizeMedium = 13;
        public static readonly int SizeLarge = 14;

        /* CHART FOR SIZES:
         * TINY : Asteroid Belt (Sparse), Terrestial (Tiny)
         * SMALL: Asteroid Belt (Light), Terrestial (Small), Gas Giant (Small)
         * MEDIUM: Asteroid Belt (Moderate), Terrestial (Standard), Gas Giant (Medium)
         * LARGE: Asteroid Belt (Dense), Terrestial (Large), Gas Giant (Large) */

        // atm flags
        // flags for type. Used to make sure we keep all refrences consistant
        public static readonly int AtmBasePres = 300;
        public static readonly int AtmPresNone = AtmBasePres + 0;
        public static readonly int AtmPresTrace = AtmBasePres + 1;
        public static readonly int AtmPresVerythin = AtmBasePres + 2;
        public static readonly int AtmPresThin = AtmBasePres + 3;
        public static readonly int AtmPresStandard = AtmBasePres + 4;
        public static readonly int AtmPresDense = AtmBasePres + 5;
        public static readonly int AtmPresVerydense = AtmBasePres + 6;
        public static readonly int AtmPresSuperdense = AtmBasePres + 7;

        // flags for badstuff
        public static readonly int AtmBaseCond = 330;
        public static readonly int AtmCondCorrosive = AtmBaseCond + 0;
        public static readonly int AtmCondSuffocating = AtmBaseCond + 1;
        public static readonly int AtmCondFlamp1 = AtmBaseCond + 2;

        //the following are [Toxic]
        public static readonly int AtmBaseToxic = 340;
        public static readonly int AtmToxicMildly = AtmBaseToxic + 0;
        public static readonly int AtmToxicHighly = AtmBaseToxic + 1;
        public static readonly int AtmToxicLethally = AtmBaseToxic + 2;

        // the following are [Marginal].
        public static readonly int AtmBaseMarginal = 350;
        public static readonly int AtmMargInert = AtmBaseMarginal + 0;
        public static readonly int AtmMargChlorine = AtmBaseMarginal + 1;
        public static readonly int AtmMargFlourine = AtmBaseMarginal + 2;
        public static readonly int AtmMargSulfur = AtmBaseMarginal + 3;
        public static readonly int AtmMargNitrogen = AtmBaseMarginal + 4;
        public static readonly int AtmMargOrganic = AtmBaseMarginal + 5;
        public static readonly int AtmMargLowoxy = AtmBaseMarginal + 6;
        public static readonly int AtmMargHighoxy = AtmBaseMarginal + 7;
        public static readonly int AtmMargPollutants = AtmBaseMarginal + 8;
        public static readonly int AtmMargHighco2 = AtmBaseMarginal + 9;

        /// <summary>
        ///     This flag stores the count of marginal flags. If you add one, please increment this.
        /// </summary>
        protected static readonly int MarginalIncrement = 10;

        /// <summary>
        ///     This flag stors the count of conditional flags. If you add one, please increment this
        /// </summary>
        protected static readonly int CondIncrement = 3;

        /// <summary>
        ///     This flag stores the count of toxicity flags.
        /// </summary>
        protected static readonly int ToxicIncrement = 3;

        // range flags
        // used to calc further down (can be altered at will)
        public static readonly int RngAtmtoxic = 3;

        // geologic flags
        // used for both tectonic and heavy actvitiy.
        public static readonly int GeologicNone = 30;
        public static readonly int GeologicLight = 31;
        public static readonly int GeologicModerate = 32;
        public static readonly int GeologicHeavy = 33;
        public static readonly int GeologicExtreme = 34;

        // climate flags
        // climates are decided via temperatures. We store them here
        // These are their stories *CLANG CLANG*
        public static readonly int ClimateFrozen = 40;
        public static readonly int ClimateVerycold = 41;
        public static readonly int ClimateCold = 42;
        public static readonly int ClimateChilly = 43;
        public static readonly int ClimateCool = 44;
        public static readonly int ClimateNormal = 45;
        public static readonly int ClimateWarm = 46;
        public static readonly int ClimateTropical = 47;
        public static readonly int ClimateHot = 48;
        public static readonly int ClimateVeryhot = 49;
        public static readonly int ClimateInfernal = 50;
        public static readonly int ClimateNone = 51;

        //description flags
        public static readonly int DescSubsurfocean = 61;
        public static readonly int DescRadHighback = 62;
        public static readonly int DescRadLethalback = 63;
        public static readonly int DescSpecringsys = 64;
        public static readonly int DescFaintringsys = 65;

        //converstion factors
        public static readonly double ConvfacDensity = 5.52;
        public static readonly double ConvfacDiameter = 12756.2;
        public static readonly double ConvfacGravity = 9.81;

        //tide flags
        public static readonly int TidePrimarystar = 101;
        public static readonly int TideSecondarystar = 102;
        public static readonly int TideTrinarystar = 103;
        public static readonly int TideSeccompstar = 104;
        public static readonly int TideTricompstar = 105;
        public static readonly int TideParplanet = 107;

        public static readonly int TideMoonBase = 110;
        public static readonly int TideMoon1 = 111;
        public static readonly int TideMoon2 = 112;
        public static readonly int TideMoon3 = 113;
        public static readonly int TideMoon4 = 114;
        public static readonly int TideMoon5 = 115;
        public static readonly int TideMoon6 = 116;
        public static readonly int TideMoon7 = 117;
        public static readonly int TideMoon8 = 118;
        public static readonly int TideMoon9 = 119;
        public static readonly int TideMoon10 = 120;

        //gas giant properties: lookup table for gas giants.

        public double[][] GasGiantTable =
        {
            new double[]
            {
                0
            }, //Index 0
            new double[]
            {
                0
            }, //Roll 1
            new double[]
            {
                0
            }, //Roll 2
            new[]
            {
                10, .42, 100, .18, 600, .31
            }, //Roll 3
            new[]
            {
                10, .42, 100, .18, 600, .31
            }, //Roll 4
            new[]
            {
                10, .42, 100, .18, 600, .31
            }, //Roll 5
            new[]
            {
                10, .42, 100, .18, 600, .31
            }, //Roll 6
            new[]
            {
                10, .42, 100, .18, 600, .31
            }, //Roll 7
            new[]
            {
                10, .42, 100, .18, 600, .31
            }, //Roll 8
            new[]
            {
                15, .26, 150, .19, 800, .35
            }, //Roll 9
            new[]
            {
                15, .26, 150, .19, 800, .35
            }, //Roll 10
            new[]
            {
                20, .22, 200, .20, 1000, .4
            }, //Roll 11
            new[]
            {
                30, .19, 250, .22, 1500, .6
            }, //Roll 12
            new[]
            {
                40, .17, 300, .24, 2000, .8
            }, //Roll 13
            new[]
            {
                50, .17, 350, .25, 2500, 1.0
            }, //Roll 14
            new[]
            {
                60, .17, 400, .26, 3000, 1.2
            }, //Roll 15
            new[]
            {
                70, .17, 450, .27, 3500, 1.4
            }, //Roll 16
            new[]
            {
                80, .17, 500, .29, 4000, 1.6
            }, //Roll 17
            new[]
            {
                80, .17, 500, .29, 4000, 1.6
            } //Roll 18
        };

        public double[][] TerrDenTable =
        {
            new double[]
            {
                0
            }, //Index 0
            new double[]
            {
                0
            }, //Roll 1
            new double[]
            {
                0
            }, //Roll 2
            new[]
            {
                .3, .6, .8
            }, //Roll 3
            new[]
            {
                .3, .6, .8
            }, //Roll 4
            new[]
            {
                .3, .6, .8
            }, //Roll 5 
            new[]
            {
                .3, .6, .8
            }, //Roll 6
            new[]
            {
                .4, .7, .9
            }, //Roll 7
            new[]
            {
                .4, .7, .9
            }, //Roll 8
            new[]
            {
                .4, .7, .9
            }, //Roll 9
            new[]
            {
                .4, .7, .9
            }, //Roll 10
            new[]
            {
                .5, .8, 1.0
            }, //Roll 11
            new[]
            {
                .5, .8, 1.0
            }, //Roll 12
            new[]
            {
                .5, .8, 1.0
            }, //Roll 13
            new[]
            {
                .5, .8, 1.0
            }, //Roll 14
            new[]
            {
                .6, .9, 1.1
            }, //Roll 15
            new[]
            {
                .6, .9, 1.1
            }, //Roll 16
            new[]
            {
                .6, .9, 1.1
            }, //Roll 17
            new[]
            {
                .7, 1.0, 1.2
            } //Roll 18
        };

        /// <summary>
        ///     Constructor object
        /// </summary>
        /// <param name="parent">The parent ID (inherited from Orbital)</param>
        /// <param name="self">Self ID (inherited from Orbital)</param>
        /// <param name="radius">Orbital Radius of the satelite</param>
        /// <param name="masterCount">In a system, the ordinal of the planet from the sun</param>
        /// <param name="satType">The Satelite Type. Default is BASETYPE_UNSET</param>
        public Satellite( int parent, int self, double radius, int masterCount, int satType = 999 ) : base(parent, self)
        {
            InitiateLists();
            UpdateType(satType); //call updateType.

            OrbitalRadius = radius;
            MasterOrderId = masterCount;
            SatelliteSize = SizeUnset;
            IsResonant = false;
        }

        /// <summary>
        ///     The copy constructor
        /// </summary>
        /// <param name="s">The satelite object we are copying</param>
        public Satellite( Satellite s ) : base(s.ParentId, s.SelfId)
        {
            // .. it's a copy constructor.
            InitiateLists();
            UpdateType(s.SatelliteType);
            UpdateSize(s.SatelliteSize);

            foreach (var m in s.InnerMoonlets)
            {
                InnerMoonlets.Add(new Moonlet(m));
            }

            foreach (var m in s.OuterMoonlets)
            {
                OuterMoonlets.Add(new Moonlet(m));
            }

            foreach (var m in s.MajorMoons)
            {
                MajorMoons.Add(new Satellite(m));
            }

            OrbitalCycle = s.OrbitalCycle;
            Diameter = s.Diameter;
            Mass = s.Mass;
            Density = s.Density;
            AxialTilt = s.AxialTilt;
            SiderealPeriod = s.SiderealPeriod;
            RotationalPeriod = s.RotationalPeriod;
            RetrogradeMotion = s.RetrogradeMotion;

            foreach (var atmType in s.AtmCate)
            {
                AtmCate.Add(atmType);
            }

            foreach (var kvp in s.TideForce)
            {
                TideForce.Add(kvp.Key, kvp.Value);
            }

            VolActivity = s.VolActivity;
            TecActivity = s.TecActivity;
            MasterOrderId = s.MasterOrderId;

            SurfaceTemp = s.SurfaceTemp;
            DayFaceMod = s.DayFaceMod;
            NightFaceMod = s.NightFaceMod;

            HydCoverage = s.HydCoverage;
            AtmMass = s.AtmMass;
            AtmPres = s.AtmPres;
            Rvm = s.Rvm;
            IsResonant = s.IsResonant;
        }

        //general properties - Satellite size and type: these fields deteremine the base type.
        public int BaseType { get; protected set; }
        public int SatelliteType { get; protected set; }
        public int SatelliteSize { get; protected set; }

        //planet properties - moon properties
        public List<Moonlet> InnerMoonlets { get; set; }
        public List<Satellite> MajorMoons { get; set; }
        public List<Moonlet> OuterMoonlets { get; set; } //For Gas Giants: Used for outermost family.

        //moon properties
        public double OrbitalCycle { get; set; } //apparent motion of this Satellite around it's primary. Only used if it's a moon.
        public double ParentDiam { get; set; }
        public double MoonRadius { get; set; }

        //general properties - self properties
        public double Diameter { get; set; }
        public double Mass { get; set; }
        public double Density { get; set; }
        public double Gravity { get; set; } //I'd like to say this should be derived. Sigh.
        public double AxialTilt { get; set; }

        //general properties - orbital properties
        public double SiderealPeriod { get; set; } //base sidereal day
        public double RotationalPeriod { get; set; } //solar day
        public bool RetrogradeMotion { get; set; } //does this orbit in retrograde?      

        //general properties - climate and atmosphere properties
        public double HydCoverage { get; set; }
        public double SurfaceTemp { get; set; }
        public double DayFaceMod { get; set; }
        public double NightFaceMod { get; set; }

        public double AtmMass { get; set; }
        public double AtmPres { get; protected set; }
        public List<int> AtmCate { get; set; }
        protected List<int> DescListing { get; set; }

        //general properties - resources and geologic properties
        public int Rvm { get; set; }
        public int VolActivity { get; set; }
        public int TecActivity { get; set; }

        //general properties - tide data
        [XmlIgnore]
        public Dictionary<int, double> TideForce { get; set; }

        public bool IsResonant { get; set; }
        public bool IsTideLocked { get; set; }
        public double TideTotal { get; set; }

        //order properties
        public int MasterOrderId { get; set; } //used to track what planet this is in a multi-star system.

        protected void InitiateLists()
        {
            MajorMoons = new List<Satellite>();
            AtmCate = new List<int>();
            InnerMoonlets = new List<Moonlet>();
            OuterMoonlets = new List<Moonlet>();
            DescListing = new List<int>();
            TideForce = new Dictionary<int, double>();
        }

        public void UpdateAtmPres( double atmPres )
        {
            AtmPres = atmPres;
            if (!( Math.Abs(AtmPres) < 0 ))
            {
                return;
            }
            //reset mass to 0
            AtmMass = 0.0;

            //nuke it.
            if (AtmCate.Count > 0)
            {
                AtmCate.RemoveRange(0, AtmCate.Count);
            }
        }

        /// <summary>
        ///     This determines the habitability score of a satelite
        /// </summary>
        /// <returns>The habitability score of a satelite</returns>
        public virtual int GetHabitability()
        {
            var mod = 0;

            //Geologic modifiers
            if (VolActivity == GeologicHeavy)
            {
                mod = mod - 1;
            }
            if (VolActivity == GeologicExtreme)
            {
                mod = mod - 2;
            }
            if (TecActivity == GeologicExtreme)
            {
                mod = mod - 2;
            }

            var isMarginal = false;
            if (AtmCate.Count > 0)
            {
                foreach (var currAtmNote in AtmCate)
                {
                    if (currAtmNote >= AtmBaseToxic && currAtmNote < AtmBaseToxic + RngAtmtoxic)
                    {
                        mod = mod - 1;
                    }
                    if (currAtmNote == AtmCondCorrosive)
                    {
                        mod = mod - 1;
                    }

                    if (currAtmNote >= AtmBaseMarginal && currAtmNote < AtmBaseMarginal + MarginalIncrement)
                    {
                        isMarginal = true;
                    }
                }
            }

            if (GetAtmCategory() == AtmPresVerythin)
            {
                mod = mod + 1;
            }
            if (GetAtmCategory() == AtmPresThin)
            {
                mod = mod + 2;
            }
            if (GetAtmCategory() == AtmPresStandard)
            {
                mod = mod + 3;
            }
            if (GetAtmCategory() == AtmPresDense)
            {
                mod = mod + 3;
            }
            if (GetAtmCategory() == AtmPresVerydense)
            {
                mod = mod + 1;
            }
            if (GetAtmCategory() == AtmPresSuperdense)
            {
                mod = mod + 1;
            }

            if (isMarginal)
            {
                mod = mod + 1;
            }
            if (HydCoverage > .0 && HydCoverage <= .59)
            {
                mod = mod + 1;
            }
            if (HydCoverage > .59 && HydCoverage <= .90)
            {
                mod = mod + 2;
            }
            if (HydCoverage > .90 && HydCoverage <= .99)
            {
                mod = mod + 1;
            }

            if (AtmPres > 0.01 && GetClimate(SurfaceTemp) == ClimateCold)
            {
                mod = mod + 1;
            }
            if (AtmPres > 0.01 && GetClimate(SurfaceTemp) == ClimateChilly)
            {
                mod = mod + 2;
            }
            if (AtmPres > 0.01 && GetClimate(SurfaceTemp) == ClimateCool)
            {
                mod = mod + 2;
            }
            if (AtmPres > 0.01 && GetClimate(SurfaceTemp) == ClimateNormal)
            {
                mod = mod + 2;
            }
            if (AtmPres > 0.01 && GetClimate(SurfaceTemp) == ClimateWarm)
            {
                mod = mod + 2;
            }
            if (AtmPres > 0.01 && GetClimate(SurfaceTemp) == ClimateTropical)
            {
                mod = mod + 2;
            }
            if (AtmPres > 0.01 && GetClimate(SurfaceTemp) == ClimateHot)
            {
                mod = mod + 1;
            }

            if (mod >= 8 && (bool) !OptionCont.OverrideHabitability)
            {
                return 8;
            }
            return mod;
        }

        /// <summary>
        ///     This returns the description of the RVM score according to the GURPS ruleset
        /// </summary>
        /// <returns>A string describing the RVM score</returns>
        public virtual string GetRvmDesc()
        {
            if (Rvm == -5)
            {
                return "Worthless";
            }
            if (Rvm == -4)
            {
                return "Very Scant";
            }
            if (Rvm == -3)
            {
                return "Scant";
            }
            if (Rvm == -2)
            {
                return "Very Poor";
            }
            if (Rvm == -1)
            {
                return "Poor";
            }
            if (Rvm == 0)
            {
                return "Average";
            }
            if (Rvm == 1)
            {
                return "Abundant";
            }
            if (Rvm == 2)
            {
                return "Very Abundant";
            }
            if (Rvm == 3)
            {
                return "Rich";
            }
            if (Rvm == 4)
            {
                return "Very Rich";
            }
            if (Rvm == 5)
            {
                return "Motherlode";
            }

            return "Error";
        }

        //sets the total planet # in a multi star, as well as the current one around it's parent.
        // in the case of a one-star system you shouldn't assign them different numbers. 
        public virtual void UpdateOrbitalData( int masterOrbPos, int localOrbPos )
        {
            MasterOrderId = masterOrbPos;
            SelfId = localOrbPos;
        }

        /// <summary>
        ///     Adds an ATM Category Flag
        /// </summary>
        /// <param name="s">The flag to be added</param>
        public virtual void AddAtmCategory( int s )
        {
            AtmCate.Add(s);
        }

        /// <summary>
        ///     This generates the world type, given our age, blackbody temperature and mass.
        /// </summary>
        /// <param name="maxMass">The mass of the star (used for Ammonia worlds)</param>
        /// <param name="sysAge">Age of the planet</param>
        /// <param name="ourBag">Ddice object used for rolls</param>
        /// <remarks>
        ///     This is placed here so that somoene with the Satellite class can create one almost without additional logic
        ///     coding.
        /// </remarks>
        public virtual void GenWorldType( double maxMass, double sysAge, Dice ourBag )
        {
            if (( BaseType != BasetypeTerrestial ) && ( BaseType != BasetypeMoon ))
            {
                return;
            }
            if (SatelliteSize == SizeTiny)
            {
                if (BlackbodyTemp <= 140.50)
                {
                    UpdateType(SubtypeIce);
                }
                if (BlackbodyTemp > 140.50)
                {
                    UpdateType(SubtypeRock);
                }
            }

            if (SatelliteSize == SizeSmall)
            {
                if (BlackbodyTemp <= 80.50)
                {
                    UpdateType(SubtypeHadean);
                }
                if (( 80.50 < BlackbodyTemp ) && ( BlackbodyTemp <= 140.50 ))
                {
                    UpdateType(SubtypeIce);
                }
                if (BlackbodyTemp > 140.50)
                {
                    UpdateType(SubtypeRock);
                }
            }

            if (SatelliteSize == SizeMedium)
            {
                if (BlackbodyTemp <= 80.50)
                {
                    UpdateType(SubtypeHadean);
                }
                if (( BlackbodyTemp > 80.50 ) && ( BlackbodyTemp <= 150.50 ))
                {
                    UpdateType(SubtypeIce);
                }

                if (( BlackbodyTemp > 150.50 ) && ( BlackbodyTemp <= 230.50 ))
                {
                    UpdateType(maxMass <= .65 ? SubtypeAmmonia : SubtypeIce);
                }

                if (( BlackbodyTemp > 230.50 ) && ( BlackbodyTemp <= 240.50 ))
                {
                    UpdateType(SubtypeIce);
                }
                if (( BlackbodyTemp > 240.50 ) && ( BlackbodyTemp <= 320.50 ))
                {
                    var roll = ourBag.Rng(3, 6, 0);

                    var mod = (int) Math.Floor(sysAge / .5);
                    if (mod > 10)
                    {
                        mod = 10;
                    }

                    roll = roll + mod;

                    if ((bool) !OptionCont.NoOceanOnlyGarden)
                    {
                        UpdateType(roll >= 18 ? SubtypeGarden : SubtypeOcean);
                    }
                    else
                    {
                        UpdateType(SubtypeGarden);
                    }
                }

                if (( BlackbodyTemp > 321.50 ) && ( BlackbodyTemp <= 500.50 ))
                {
                    UpdateType(SubtypeGreenhouse);
                }
                if (BlackbodyTemp > 500.50)
                {
                    UpdateType(SubtypeChthonian);
                }
            }

            if (SatelliteSize != SizeLarge)
            {
                return;
            }
            {
                if (BlackbodyTemp <= 150.50)
                {
                    UpdateType(SubtypeHadean);
                }
                if (( BlackbodyTemp > 150.50 ) && ( BlackbodyTemp <= 230.50 ))
                {
                    UpdateType(maxMass <= .65 ? SubtypeAmmonia : SubtypeIce);
                }

                if (( BlackbodyTemp > 230.50 ) && ( BlackbodyTemp <= 240.50 ))
                {
                    UpdateType(SubtypeIce);
                }
                if (( BlackbodyTemp > 240.50 ) && ( BlackbodyTemp <= 320.50 ))
                {
                    ourBag.Rng(3, 6, 0);
                    int mod;

                    if (OptionCont.MoreAccurateO2Catastrophe != null && (bool) OptionCont.MoreAccurateO2Catastrophe)
                    {
                        mod = (int) Math.Floor(sysAge / .3);
                    }
                    else
                    {
                        mod = (int) Math.Floor(sysAge / .5);
                    }

                    if (OptionCont.MoreLargeGarden != null && (bool) !OptionCont.MoreLargeGarden)
                    {
                        if (mod > 5)
                        {
                            mod = 5;
                        }
                    }
                    if (OptionCont.MoreLargeGarden != null && (bool) OptionCont.MoreLargeGarden)
                    {
                        if (mod > 10)
                        {
                            mod = 10;
                        }
                    }

                    UpdateType(SubtypeGarden);
                }

                if (( BlackbodyTemp > 321.50 ) && ( BlackbodyTemp <= 500.50 ))
                {
                    UpdateType(SubtypeGreenhouse);
                }
                if (BlackbodyTemp > 500.50)
                {
                    UpdateType(SubtypeChthonian);
                }
            }
        }

        /// <summary>
        ///     Blackbody wrapper if you're invoking for the same satelite you want the temp for. Invokes the other function
        /// </summary>
        /// <param name="distChart">The distance chart of each star to each other</param>
        /// <param name="ourStars">List of our stars</param>
        public virtual void UpdateBlackBodyTemp( double[,] distChart, List<Star> ourStars )
        {
            BlackbodyTemp = CalcBlackbodyTemp(distChart, ourStars, OrbitalRadius, ParentId);
        }

        //There might be a better way to calc this than I did.
        /// <summary>
        ///     Blackbody function that calcluates our temperatuer
        /// </summary>
        /// <param name="distChart">The distance chart of each star to each other</param>
        /// <param name="stars">List of our stars</param>
        /// <param name="planetRadius">Radius of this satellite</param>
        /// <param name="planetOwnership">The parent ID (what star does this satellite orbit)</param>
        /// <returns></returns>
        public virtual double CalcBlackbodyTemp( double[,] distChart, List<Star> stars, double planetRadius, int planetOwnership )
        {
            var currTemp = 0.0;
            const double blackMulti = 278.0;
            //first get distances of each star to the primary.
            new Dictionary<int, Dictionary<int, double>>();

            foreach (var star in stars)
            {
                int currStar = 0, planetOwner = 0;

                //first, determine the lookup for the planet ownership
                if (( planetOwnership == OrbitPrisec ) || ( planetOwnership == OrbitPrisectri ) || ( planetOwnership == OrbitPritri ) || ( planetOwnership == Star.IsPrimary ))
                {
                    planetOwner = 0;
                }
                if (( planetOwnership == OrbitSeccom ) || ( planetOwnership == OrbitSectri ) || ( planetOwnership == Star.IsSecondary ))
                {
                    planetOwner = 1;
                }
                if (( planetOwnership == OrbitTricom ) || ( planetOwnership == Star.IsTrinary ))
                {
                    planetOwner = 2;
                }
                if (planetOwnership == Star.IsSeccomp)
                {
                    planetOwner = 3;
                }
                if (planetOwnership == Star.IsTricomp)
                {
                    planetOwner = 4;
                }

                //second, lookup for the star we're in.
                if (star.OrderId == Star.IsPrimary)
                {
                    currStar = 0;
                }
                if (star.OrderId == Star.IsSecondary)
                {
                    currStar = 1;
                }
                if (star.OrderId == Star.IsTrinary)
                {
                    currStar = 2;
                }
                if (star.OrderId == Star.IsSeccomp)
                {
                    currStar = 3;
                }
                if (star.OrderId == Star.IsTricomp)
                {
                    currStar = 4;
                }

                var currDistance = Math.Abs(distChart[planetOwner, currStar] + planetRadius);

                //add the blackbody for this star
                currTemp = currTemp + Math.Pow(blackMulti * Math.Pow(star.CurrLumin, .25) / Math.Sqrt(currDistance), 4);
            }

            currTemp = Math.Pow(currTemp, .25);
            return currTemp;
        }

        /// <summary>
        ///     This determines what types can be added. It performs some sanity checking to make sure that invalid combinations
        ///     are not allowed.
        /// </summary>
        /// <param name="flag">The flag we are updating for</param>
        /// <exception cref="Exception">Throws an exception if you attempt to set an invalid combo.</exception>
        public virtual void UpdateType( int flag )
        {
            if (flag == BasetypeAsteroidbelt)
            {
                BaseType = flag;
            }
            if (flag == BasetypeEmpty)
            {
                BaseType = flag;
            }
            if (flag == BasetypeGasgiant)
            {
                BaseType = flag;
            }
            if (flag == BasetypeMoon)
            {
                BaseType = flag;
            }
            if (flag == BasetypeTerrestial)
            {
                BaseType = flag;
            }

            if (flag == BasetypeUnset)
            {
                BaseType = flag;
                SatelliteType = flag;
            }

            if (flag == SubtypeAmmonia)
            {
                if (!( BaseType == BasetypeTerrestial || BaseType == BasetypeMoon || BaseType == BasetypeUnset ))
                {
                    throw new Exception("Cannot set this as a type for this base type.");
                }

                if (BaseType == BasetypeUnset)
                {
                    BaseType = BasetypeTerrestial; //moons MUST set this or you'll kinda regret it.
                }

                SatelliteType = flag;
            }

            if (flag == SubtypeChthonian)
            {
                if (!( BaseType == BasetypeTerrestial || BaseType == BasetypeMoon || BaseType == BasetypeUnset ))
                {
                    throw new Exception("Cannot set this as a type for this base type.");
                }

                if (BaseType == BasetypeUnset)
                {
                    BaseType = BasetypeTerrestial; //moons MUST set this or you'll kinda regret it.
                }

                SatelliteType = flag;
            }

            if (flag == SubtypeGarden)
            {
                if (!( BaseType == BasetypeTerrestial || BaseType == BasetypeMoon || BaseType == BasetypeUnset ))
                {
                    throw new Exception("Cannot set this as a type for this base type.");
                }

                if (BaseType == BasetypeUnset)
                {
                    BaseType = BasetypeTerrestial; //moons MUST set this or you'll kinda regret it.
                }

                SatelliteType = flag;
            }

            if (flag == SubtypeGreenhouse)
            {
                if (!( BaseType == BasetypeTerrestial || BaseType == BasetypeMoon || BaseType == BasetypeUnset ))
                {
                    throw new Exception("Cannot set this as a type for this base type.");
                }

                if (BaseType == BasetypeUnset)
                {
                    BaseType = BasetypeTerrestial; //moons MUST set this or you'll kinda regret it.
                }

                SatelliteType = flag;
            }

            if (flag == SubtypeHadean)
            {
                if (!( BaseType == BasetypeTerrestial || BaseType == BasetypeMoon || BaseType == BasetypeUnset ))
                {
                    throw new Exception("Cannot set this as a type for this base type.");
                }

                if (BaseType == BasetypeUnset)
                {
                    BaseType = BasetypeTerrestial; //moons MUST set this or you'll kinda regret it.
                }

                SatelliteType = flag;
            }

            if (flag == SubtypeIce)
            {
                if (!( BaseType == BasetypeTerrestial || BaseType == BasetypeMoon || BaseType == BasetypeUnset ))
                {
                    throw new Exception("Cannot set this as a type for this base type.");
                }

                if (BaseType == BasetypeUnset)
                {
                    BaseType = BasetypeTerrestial; //moons MUST set this or you'll kinda regret it.
                }

                SatelliteType = flag;
            }

            if (flag == SubtypeRock)
            {
                if (!( BaseType == BasetypeTerrestial || BaseType == BasetypeMoon || BaseType == BasetypeUnset ))
                {
                    throw new Exception("Cannot set this as a type for this base type.");
                }

                if (BaseType == BasetypeUnset)
                {
                    BaseType = BasetypeTerrestial; //moons MUST set this or you'll kinda regret it.
                }

                SatelliteType = flag;
            }

            if (flag == SubtypeSulfur)
            {
                if (!( BaseType == BasetypeMoon || BaseType == BasetypeUnset ))
                {
                    throw new Exception("Cannot set this as a type for this base type.");
                }

                if (BaseType == BasetypeUnset)
                {
                    BaseType = BasetypeMoon; //Can only be a moon.
                }

                SatelliteType = flag;
            }
        }

        /* the following functions will create the physical properties of terrestial and gas planets. 
         * Asteroid Belts don't really have much, and empty orbitals are empty. */

        // FEW NOTES HERE: If you want to set the density of a gas giant, you'll auto set the mass. This is because the mass to generate the diameter. And for ease of use I'm 
        // letting it stand now.

        /// <summary>
        ///     This function generates the density of a sateltie. You MUST have set a basetype first.
        /// </summary>
        /// <param name="ourBag">Our Ddice object</param>
        /// <exception cref="Exception">
        ///     Throws an exception if you attempt to invoke this on an satellite with any of: UNSET, EMPTY
        ///     OR ASTEROID basetypes.
        /// </exception>
        /// <remarks>For a satellite with a basetype of GASGIANT, it will auto set the mass. </remarks>
        public virtual void GenDensity( Dice ourBag )
        {
            if (BaseType == BasetypeEmpty || BaseType == BasetypeAsteroidbelt)
            {
                throw new Exception("You cannot give these types of orbits a density.");
            }
            if (BaseType == BasetypeUnset || SatelliteSize == SizeUnset)
            {
                throw new Exception("Please give this orbit a size or basetype first.");
            }

            var roll = ourBag.Rng(3, 6, 0);
            if (BaseType == BasetypeGasgiant)
            {
                var varMass = ourBag.Rng(1, 50, 0) * .01;
                if (roll != 18)
                {
                    int massEntry = 0, densityEntry = 1;
                    if (SatelliteSize == SizeMedium)
                    {
                        massEntry = 2;
                        densityEntry = 3;
                    }

                    if (SatelliteSize == SizeLarge)
                    {
                        massEntry = 4;
                        densityEntry = 5;
                    }

                    //now we interporlate
                    Mass = GasGiantTable[roll + 1][massEntry] - GasGiantTable[roll][massEntry] * varMass + GasGiantTable[roll][massEntry];
                    Density = GasGiantTable[roll + 1][densityEntry] - GasGiantTable[roll][densityEntry] * varMass + GasGiantTable[roll][densityEntry];
                }
                else if (roll == 18)
                {
                    int massEntry = 0, densityEntry = 1;
                    if (SatelliteSize == SizeMedium)
                    {
                        massEntry = 2;
                        densityEntry = 3;
                    }

                    if (SatelliteSize == SizeLarge)
                    {
                        massEntry = 4;
                        densityEntry = 5;
                    }

                    Mass = GasGiantTable[roll][massEntry];
                    Density = GasGiantTable[roll][densityEntry];
                }
            }

            if (BaseType == BasetypeTerrestial || BaseType == BasetypeMoon)
            {
                var densityEntry = 0; //means we don't need to bother specifying for icy core!
                if (SatelliteType == SubtypeRock)
                {
                    densityEntry = 1;
                }

                if (SatelliteType == SubtypeOcean)
                {
                    densityEntry = 2;
                }
                if (SatelliteType == SubtypeGarden)
                {
                    densityEntry = 2;
                }
                if (SatelliteType == SubtypeGreenhouse)
                {
                    densityEntry = 2;
                }
                if (SatelliteType == SubtypeChthonian)
                {
                    densityEntry = 2;
                }
                if (SatelliteType == SubtypeIce && SatelliteSize == SizeLarge)
                {
                    densityEntry = 2;
                }

                Density = TerrDenTable[roll][densityEntry];
            }
        }

        /// <summary>
        ///     This function will set the diameter, mass and gravity, given the density.
        /// </summary>
        /// <param name="ourBag">Our Ddice object</param>
        /// <exception cref="Exception">Throws an exception if it's called on anything but a moon and gas giant</exception>
        /// <exception cref="Exception">Throws an exception if the density is unset.</exception>
        public virtual void GenPhysicalParameters( Dice ourBag )
        {
            if (BaseType == BasetypeAsteroidbelt || BaseType == BasetypeEmpty || BaseType == BasetypeUnset)
            {
                throw new Exception("Please only call this on a moon, terrestial planet or gas giant.");
            }
            if (Math.Abs(Density) < 0)
            {
                throw new Exception("Density unset.");
            }

            if (BaseType == BasetypeTerrestial || BaseType == BasetypeMoon)
            {
                var baseVal = Math.Sqrt(BlackbodyTemp / Density);

                //range for small is .004 to .024
                if (SatelliteSize == SizeTiny)
                {
                    Diameter = ourBag.RollRange(.004, .020) * baseVal;
                }

                //range for small is .024 to .030
                if (SatelliteSize == SizeSmall)
                {
                    Diameter = ourBag.RollRange(.024, .006) * baseVal;
                }

                //range for medium is .030 to .065
                if (SatelliteSize == SizeMedium)
                {
                    Diameter = ourBag.RollRange(.030, .035) * baseVal;
                }

                //range for large is .065 to .091
                if (SatelliteSize == SizeLarge)
                {
                    Diameter = ourBag.RollRange(.065, .026) * baseVal;
                }

                Mass = Density * Math.Pow(Diameter, 3);
                Gravity = Density * Diameter;
            }

            if (BaseType == BasetypeGasgiant)
            {
                Diameter = Math.Pow(Mass / Density, .33);
                Gravity = Density * Diameter;
            }
        }

        /// <summary>
        ///     This sets the atmospheric pressure given the table and various properties of the atmosphere.
        /// </summary>
        public virtual void CalcAtmPres()
        {
            if (!( BaseType == BasetypeMoon || BaseType == BasetypeTerrestial ))
            {
                throw new Exception("You can only invoke this on a terrestial planet or terrestial moon.");
            }

            if (Math.Abs(Gravity) < 0)
            {
                throw new Exception("You cannot calculate Atmospheric pressure until you have gravity defined.");
            }

            var presFact = 0.0;

            //pull the fact from the table. Well, from this..
            if (SatelliteSize == SizeSmall && SatelliteType == SubtypeIce)
            {
                presFact = 10.0;
            }

            if (SatelliteSize == SizeMedium && SatelliteType == SubtypeGreenhouse)
            {
                presFact = 100.0;
            }

            if (SatelliteSize == SizeLarge && SatelliteType == SubtypeGreenhouse)
            {
                presFact = 500.0;
            }

            if (SatelliteSize == SizeMedium)
            {
                if (SatelliteType == SubtypeAmmonia)
                {
                    presFact = 1.0;
                }
                if (SatelliteType == SubtypeIce)
                {
                    presFact = 1.0;
                }
                if (SatelliteType == SubtypeOcean)
                {
                    presFact = 1.0;
                }
                if (SatelliteType == SubtypeGarden)
                {
                    presFact = 1.0;
                }
            }

            if (SatelliteSize == SizeLarge)
            {
                if (SatelliteType == SubtypeAmmonia)
                {
                    presFact = 5.0;
                }
                if (SatelliteType == SubtypeIce)
                {
                    presFact = 5.0;
                }
                if (SatelliteType == SubtypeOcean)
                {
                    presFact = 5.0;
                }
                if (SatelliteType == SubtypeGarden)
                {
                    presFact = 5.0;
                }
            }

            AtmPres = AtmMass * Gravity * presFact;

            if (( SatelliteSize == SizeTiny ) || ( SatelliteType == SubtypeHadean ))
            {
                AtmPres = 0;
                AtmMass = 0;
            }

            if (( SatelliteType == SubtypeChthonian ) || ( SatelliteType == SubtypeRock && SatelliteSize == SizeSmall ))
            {
                AtmPres = .01;
                AtmMass = .01;
            }

            if (Math.Abs(OptionCont.SetAtmPressure - -1) > 0 && SatelliteType == SubtypeGarden)
            {
                AtmPres = OptionCont.SetAtmPressure;
            }
        }

        /// <summary>
        ///     Calculates an axial tilt of a satellite given the ruleset
        /// </summary>
        /// <param name="ourBag"></param>
        public virtual void CreateAxialTilt( Dice ourBag )
        {
            do
            {
                switch (ourBag.GurpsRoll())
                {
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        AxialTilt = ourBag.Rng(2, 6, -2);
                        break;
                    case 7:
                    case 8:
                    case 9:
                        AxialTilt = 10 + ourBag.Rng(2, 6, -2);
                        break;
                    case 10:
                    case 11:
                    case 12:
                        AxialTilt = 20 + ourBag.Rng(2, 6, -2);
                        break;
                    case 13:
                    case 14:
                        AxialTilt = 30 + ourBag.Rng(2, 6, -2);
                        break;
                    case 15:
                    case 16:
                        AxialTilt = 30 + ourBag.Rng(2, 6, -2);
                        break;
                    case 17:
                    case 18:
                        switch (ourBag.Rng(1, 6, 0))
                        {
                            case 1:
                            case 2:
                                AxialTilt = 50 + ourBag.Rng(2, 6, -2);
                                break;
                            case 3:
                            case 4:
                                AxialTilt = 60 + ourBag.Rng(2, 6, -2);
                                break;
                            case 5:
                                AxialTilt = 70 + ourBag.Rng(2, 6, -2);
                                break;
                            case 6:
                                AxialTilt = 80 + ourBag.Rng(2, 6, -2);
                                break;
                        }
                        break;
                }
            }
            while (OptionCont.RerollAxialTiltOver45 != null && (bool) OptionCont.RerollAxialTiltOver45 && AxialTilt > 45);

            if (OptionCont.GetAxialTilt() != -1)
            {
                AxialTilt = OptionCont.GetAxialTilt();
            }
        }

        /// <summary>
        ///     Sets the Resource Value Modifier for this satelite
        /// </summary>
        /// <param name="roll">The Ddice roll</param>
        public virtual void PopulateRvm( int roll )
        {
            if (BaseType == BasetypeAsteroidbelt)
            {
                if (roll == 3)
                {
                    Rvm = -5;
                }
                if (roll == 4)
                {
                    Rvm = -4;
                }
                if (roll == 5)
                {
                    Rvm = -3;
                }
                if (roll == 6 || roll == 7)
                {
                    Rvm = -2;
                }
                if (roll == 8 || roll == 9)
                {
                    Rvm = -1;
                }
                if (roll == 10 || roll == 11)
                {
                    Rvm = 0;
                }
                if (roll == 12 || roll == 13)
                {
                    Rvm = 1;
                }
                if (roll == 14 || roll == 15)
                {
                    Rvm = 2;
                }
                if (roll == 16)
                {
                    Rvm = 3;
                }
                if (roll == 17)
                {
                    Rvm = 4;
                }
                if (roll == 18)
                {
                    Rvm = 5;
                }
            }

            else
            {
                if (roll <= 2)
                {
                    Rvm = -3;
                }
                if (roll == 3 || roll == 4)
                {
                    Rvm = -2;
                }
                if (roll >= 5 && roll <= 7)
                {
                    Rvm = -1;
                }
                if (roll >= 8 && roll <= 13)
                {
                    Rvm = 0;
                }
                if (roll >= 14 && roll <= 16)
                {
                    Rvm = 1;
                }
                if (roll >= 17 && roll <= 18)
                {
                    Rvm = 2;
                }
                if (roll >= 19)
                {
                    Rvm = 3;
                }
            }
        }

        /// <summary>
        ///     Sets climate and atmospheric data for this satelite.
        /// </summary>
        /// <param name="maxMass">The maximum mass of the star</param>
        /// <param name="ourBag">The Ddice object</param>
        public void SetClimateData( double maxMass, Dice ourBag )
        {
            int roll;
            //atm mass first.
            AtmMass = 0.01; //default case

            //ice case
            if (SatelliteType == SubtypeIce)
            {
                AtmMass = ourBag.Rng(3, 6, 0) / 10.0 + ourBag.Rng(1, 6, -1) / 100.0;
                AtmCate.Add(AtmCondSuffocating);

                if (SatelliteSize == SizeSmall)
                {
                    roll = ourBag.Rng(3, 6, 0);
                    AtmCate.Add(roll >= 15 ? AtmToxicHighly : AtmToxicMildly);
                }

                else if (SatelliteSize == SizeMedium)
                {
                    roll = ourBag.Rng(3, 6, 0);
                    if (roll >= 13)
                    {
                        AtmCate.Add(AtmToxicMildly);
                    }
                }

                else if (SatelliteSize == SizeLarge)
                {
                    AtmCate.Add(AtmToxicHighly);
                }
            }

            //Either ammonia or greenhouse
            if (SatelliteType == SubtypeAmmonia || SatelliteType == SubtypeGreenhouse)
            {
                AtmMass = ourBag.Rng(3, 6, 0) / 10.0 + ourBag.Rng(1, 6, -1) / 100.0;
                AtmCate.Add(AtmCondSuffocating);
                AtmCate.Add(AtmToxicLethally);
                AtmCate.Add(AtmCondCorrosive);
            }

            //ocean case
            if (SatelliteType == SubtypeOcean)
            {
                AtmMass = ourBag.Rng(3, 6, 0) / 10.0 + ourBag.Rng(1, 6, -1) / 100.0;
                AtmCate.Add(AtmCondSuffocating);

                if (SatelliteSize == SizeMedium)
                {
                    roll = ourBag.Rng(3, 6, 0);
                    if (roll >= 13)
                    {
                        AtmCate.Add(AtmToxicMildly);
                    }
                }

                else if (SatelliteSize == SizeLarge)
                {
                    AtmCate.Add(AtmCondSuffocating);
                    AtmCate.Add(AtmToxicHighly);
                }
            }

            //either garden
            if (SatelliteType == SubtypeGarden)
            {
                AtmMass = ourBag.Rng(3, 6, 0) / 10.0 + ourBag.Rng(1, 6, -1) / 100.0;
                roll = ourBag.Rng(3, 6, 0);
                if (roll >= 12 && (bool) !OptionCont.NoMarginalAtm)
                {
                    //add marginal code here
                    foreach (var i in GenMarginal(ourBag))
                    {
                        AtmCate.Add(i);
                    }
                }
            }

            //NOW, for hydrographic coverage
            HydCoverage = 0.0; //default.
            if (SatelliteType == SubtypeIce)
            {
                if (SatelliteSize == SizeSmall)
                {
                    roll = ourBag.Rng(1, 6, 2);
                    HydCoverage = roll * .1;
                }

                if (SatelliteSize == SizeMedium || SatelliteSize == SizeLarge)
                {
                    roll = ourBag.Rng(2, 6, -2);
                    if (roll < 0)
                    {
                        roll = 0;
                    }
                    HydCoverage = roll * .1;
                }
            }

            if (SatelliteType == SubtypeAmmonia)
            {
                roll = ourBag.Rng(2, 6, 0);
                if (roll > 10)
                {
                    roll = 10;
                }
                HydCoverage = roll * .1;
            }

            if (SatelliteType == SubtypeOcean || SatelliteType == SubtypeGarden)
            {
                if (SatelliteSize == SizeMedium)
                {
                    roll = ourBag.Rng(1, 6, 4);
                    HydCoverage = roll * .1;
                }

                if (SatelliteSize == SizeLarge)
                {
                    roll = ourBag.Rng(1, 6, 6);
                    if (roll > 10)
                    {
                        roll = 10;
                    }
                    HydCoverage = roll * .1;
                }
            }

            if (SatelliteType == SubtypeGreenhouse)
            {
                roll = ourBag.Rng(2, 6, -7);
                if (roll < 0)
                {
                    roll = 0;
                }
                HydCoverage = roll * .1;
            }

            //randomize coverage!
            if (HydCoverage >= .1)
            {
                roll = ourBag.Rng(1, 10, -5);
                HydCoverage = HydCoverage + roll * .01;
            }
        }

        public virtual void DetSurfaceTemp( double modifiers )
        {
            double absorptionFactor = 0.0, greenHouseFactor = 0.0;

            if (BaseType == BasetypeAsteroidbelt)
            {
                absorptionFactor = GetAbsorptionFactor(modifiers);
            }
            if (SatelliteType == SubtypeIce && SatelliteSize == SizeTiny)
            {
                absorptionFactor = GetAbsorptionFactor(modifiers);
            }
            if (SatelliteType == SubtypeRock && SatelliteSize == SizeTiny)
            {
                absorptionFactor = GetAbsorptionFactor(modifiers);
            }
            if (SatelliteType == SubtypeSulfur && SatelliteSize == SizeTiny)
            {
                absorptionFactor = GetAbsorptionFactor(modifiers);
            }
            if (SatelliteType == SubtypeHadean && SatelliteSize == SizeSmall)
            {
                absorptionFactor = GetAbsorptionFactor(modifiers);
            }
            if (SatelliteType == SubtypeIce && SatelliteSize == SizeSmall)
            {
                absorptionFactor = GetAbsorptionFactor(modifiers);
                greenHouseFactor = .10;
            }
            if (SatelliteType == SubtypeRock && SatelliteSize == SizeSmall)
            {
                absorptionFactor = GetAbsorptionFactor(modifiers);
            }
            if (SatelliteType == SubtypeHadean && SatelliteSize == SizeMedium)
            {
                absorptionFactor = GetAbsorptionFactor(modifiers);
            }
            if (SatelliteType == SubtypeAmmonia)
            {
                absorptionFactor = GetAbsorptionFactor(modifiers);
                greenHouseFactor = .20;
            }
            if (SatelliteType == SubtypeIce && ( SatelliteSize == SizeMedium || SatelliteSize == SizeLarge ))
            {
                absorptionFactor = GetAbsorptionFactor(modifiers);
                greenHouseFactor = .20;
            }

            if (SatelliteType == SubtypeOcean || SatelliteType == SubtypeGarden)
            {
                if (HydCoverage <= .20)
                {
                    absorptionFactor = GetAbsorptionFactor(modifiers);
                    greenHouseFactor = .16;
                }

                if (HydCoverage > .20 && HydCoverage <= .50)
                {
                    absorptionFactor = GetAbsorptionFactor(modifiers);
                    greenHouseFactor = .16;
                }

                if (HydCoverage > .50 && HydCoverage <= .90)
                {
                    absorptionFactor = GetAbsorptionFactor(modifiers);
                    greenHouseFactor = .16;
                }

                if (HydCoverage > .90)
                {
                    absorptionFactor = GetAbsorptionFactor(modifiers);
                    greenHouseFactor = .16;
                }
            }

            if (SatelliteType == SubtypeGreenhouse)
            {
                absorptionFactor = GetAbsorptionFactor(modifiers);
                greenHouseFactor = 2.0;
            }

            if (SatelliteType == SubtypeChthonian)
            {
                absorptionFactor = GetAbsorptionFactor(modifiers);
            }

            //get the surface temp.
            SurfaceTemp = BlackbodyTemp * ( absorptionFactor * ( 1 + AtmMass * greenHouseFactor ) );
        }

        public virtual double GetAbsorptionFactor( double modifiers )
        {
            double absorptionFactor = 0;

            if (BaseType == BasetypeAsteroidbelt)
            {
                absorptionFactor = .97;
            }
            if (SatelliteType == SubtypeIce && SatelliteSize == SizeTiny)
            {
                absorptionFactor = .86;
            }
            if (SatelliteType == SubtypeRock && SatelliteSize == SizeTiny)
            {
                absorptionFactor = .97;
            }
            if (SatelliteType == SubtypeSulfur && SatelliteSize == SizeTiny)
            {
                absorptionFactor = .77;
            }
            if (SatelliteType == SubtypeHadean && SatelliteSize == SizeSmall)
            {
                absorptionFactor = .67;
            }
            if (SatelliteType == SubtypeIce && SatelliteSize == SizeSmall)
            {
                absorptionFactor = .93;
            }
            if (SatelliteType == SubtypeRock && SatelliteSize == SizeSmall)
            {
                absorptionFactor = .96;
            }
            if (SatelliteType == SubtypeHadean && SatelliteSize == SizeMedium)
            {
                absorptionFactor = .67;
            }
            if (SatelliteType == SubtypeAmmonia)
            {
                absorptionFactor = .84;
            }
            if (SatelliteType == SubtypeIce && ( SatelliteSize == SizeMedium || SatelliteSize == SizeLarge ))
            {
                absorptionFactor = .86;
            }

            if (SatelliteType == SubtypeOcean || SatelliteType == SubtypeGarden)
            {
                if (HydCoverage <= .20)
                {
                    absorptionFactor = .95;
                }
                if (HydCoverage > .20 && HydCoverage <= .50)
                {
                    absorptionFactor = .92;
                }

                if (HydCoverage > .50 && HydCoverage <= .90)
                {
                    absorptionFactor = .88;
                }

                if (HydCoverage > .90)
                {
                    absorptionFactor = .84;
                }
            }

            if (SatelliteType == SubtypeGreenhouse)
            {
                absorptionFactor = .77;
            }

            if (SatelliteType == SubtypeChthonian)
            {
                absorptionFactor = .97;
            }

            return absorptionFactor + modifiers;
        }

        public virtual List<int> GenMarginal( Dice ourBag )
        {
            var ret = new List<int>();
            var roll = ourBag.Rng(3, 6, 0);
            if (roll == 3 || roll == 4)
            {
                roll = ourBag.Rng(3, 6, 0);
                //for the difference
                ret.Add(roll >= 16 ? AtmMargFlourine : AtmMargChlorine);

                //always true
                ret.Add(AtmToxicHighly);
            }

            if (roll == 5 || roll == 6)
            {
                ret.Add(AtmMargSulfur);
                ret.Add(AtmToxicMildly);
            }

            if (roll == 7)
            {
                ret.Add(AtmMargNitrogen);
                ret.Add(AtmToxicMildly);
            }

            if (roll == 8 || roll == 9)
            {
                ret.Add(AtmMargOrganic);
                roll = ourBag.Rng(3, 6, 0);
                if (roll >= 17)
                {
                    ret.Add(AtmToxicHighly);
                }
                else if (roll >= 12 && roll <= 16)
                {
                    ret.Add(AtmToxicMildly);
                }
            }

            if (roll == 10 || roll == 11)
            {
                ret.Add(AtmMargLowoxy);
            }

            if (roll == 12 || roll == 13)
            {
                ret.Add(AtmMargPollutants);
                roll = ourBag.Rng(3, 6, 0);
                if (roll >= 9 && roll <= 11)
                {
                    ret.Add(AtmToxicMildly);
                }
                else if (roll >= 17)
                {
                    ret.Add(AtmToxicHighly);
                }
            }

            if (roll == 14)
            {
                ret.Add(AtmMargHighco2);
                roll = ourBag.Rng(3, 6, 0);
                if (roll >= 15)
                {
                    ret.Add(AtmToxicMildly);
                }
            }

            if (roll == 15 || roll == 16)
            {
                ret.Add(AtmMargHighoxy);
                roll = ourBag.Rng(3, 6, 0);
                if (roll >= 15)
                {
                    ret.Add(AtmToxicMildly);
                    ret.Add(AtmCondFlamp1);
                }
            }
            if (roll == 17 || roll == 18)
            {
                ret.Add(AtmMargInert);
            }

            return ret;
        }

        public virtual void GenerateOrbitalPeriod( double parentMass )
        {
            if (BaseType == BasetypeTerrestial || BaseType == BasetypeGasgiant)
            {
                OrbitalPeriod = Math.Sqrt(Math.Pow(OrbitalRadius, 3) / ( Mass * .0000030024584 + parentMass )) * 365.25;
            }
            if (BaseType == BasetypeMoon)
            {
                OrbitalPeriod = .166 * Math.Sqrt(Math.Pow(OrbitalRadius, 3) / ( Mass + parentMass ));
            }
        }

        /// <summary>
        ///     This function sets the size of a satelite.
        /// </summary>
        /// <param name="flag">The size to set to</param>
        /// <exception cref="Exception">It will throw an exception if you attempt to set Tiny on a Gas Giant</exception>
        public virtual void UpdateSize( int flag )
        {
            if (BaseType == BasetypeGasgiant && flag == SizeTiny)
            {
                throw new Exception("You cannot have Tiny Gas Giants");
            }

            SatelliteSize = flag;
            SatelliteSize = SatelliteSize;
        }

        /// <summary>
        ///     A shortcut function to update both the type and size of a satellite
        /// </summary>
        /// <param name="typeFlag">The type of a satellite</param>
        /// <param name="sizeFlag">The size of a satellite</param>
        public virtual void UpdateTypeSize( int typeFlag, int sizeFlag )
        {
            UpdateType(typeFlag);
            UpdateSize(sizeFlag);
        }

        /// <summary>
        ///     Returns the affinity of the planet
        /// </summary>
        /// <returns>The affinity (RVM + Habitability)</returns>
        public virtual int GetAffinity()
        {
            return GetHabitability() + Rvm;
        }

        /// <summary>
        ///     This factor calculates the differentation on a moon. Should only be invoked on a gas giant moon
        /// </summary>
        /// <param name="parentMass">The parent mass of the moon</param>
        /// <param name="ourBag">Ddice object used for rolls</param>
        /// <returns>The differentation factor</returns>
        /// <exception cref="Exception">Will throw an exception if you invoke this on a satellite that isn't a moon</exception>
        public double GetDifferentationFactor( double parentMass, Dice ourBag )
        {
            if (BaseType != BasetypeMoon)
            {
                throw new Exception("Cannot call this except on a moon.");
            }

            double factor = 0;

            if (parentMass > 3900)
            {
                factor = .7;
            }

            if (parentMass > 2500 && parentMass < 3900)
            {
                factor = .6;
            }
            if (parentMass > 1000 && parentMass < 2500)
            {
                factor = .5;
            }
            if (parentMass > 300 && parentMass < 1000)
            {
                factor = .4;
            }
            if (parentMass < 300)
            {
                factor = .3;
            }

            var value = 1 / Math.Sqrt(MoonRadius * factor) * 100;

            return value;
        }

        /// <summary>
        ///     This function calculates the category given the atmospheric pressure
        /// </summary>
        /// <returns>The flag describing the pressure category</returns>
        public virtual int GetAtmCategory()
        {
            if (AtmPres < 0.01)
            {
                return AtmPresNone;
            }
            if (Math.Abs(AtmPres - 0.01) < 0)
            {
                return AtmPresTrace;
            }
            if (0.01 < AtmPres && AtmPres <= 0.5)
            {
                return AtmPresVerythin;
            }
            if (0.5 < AtmPres && AtmPres <= 0.8)
            {
                return AtmPresThin;
            }
            if (0.8 < AtmPres && AtmPres <= 1.2)
            {
                return AtmPresStandard;
            }
            if (1.2 < AtmPres && AtmPres <= 1.5)
            {
                return AtmPresDense;
            }
            if (1.5 < AtmPres && AtmPres <= 10)
            {
                return AtmPresVerydense;
            }
            if (AtmPres > 10)
            {
                return AtmPresSuperdense;
            }

            return ErrorAtm;
        }

        /// <summary>
        ///     Describes the volcanic activity of this satelite
        /// </summary>
        /// <returns>The description (a string)</returns>
        public virtual string GetVolDesc()
        {
            if (VolActivity == GeologicNone)
            {
                return "None";
            }
            if (VolActivity == GeologicLight)
            {
                return "Light";
            }
            if (VolActivity == GeologicModerate)
            {
                return "Moderate";
            }
            if (VolActivity == GeologicHeavy)
            {
                return "Heavy";
            }
            if (VolActivity == GeologicExtreme)
            {
                return "Extreme";
            }

            return "Error";
        }

        /// <summary>
        ///     Describes the tectonic activity of this satelite
        /// </summary>
        /// <returns>The description (a string)</returns>
        public virtual string GetTecDesc()
        {
            if (TecActivity == GeologicNone)
            {
                return "None";
            }
            if (TecActivity == GeologicLight)
            {
                return "Light";
            }
            if (TecActivity == GeologicModerate)
            {
                return "Moderate";
            }
            if (TecActivity == GeologicHeavy)
            {
                return "Heavy";
            }
            if (TecActivity == GeologicExtreme)
            {
                return "Extreme";
            }

            return "Error";
        }

        /// <summary>
        ///     Used to describe the satellite type within the object
        /// </summary>
        /// <returns>The description</returns>
        protected virtual string ConvSatelliteTypeToString()
        {
            if (SatelliteType == SubtypeAmmonia)
            {
                return "Ammonia";
            }
            if (SatelliteType == SubtypeChthonian)
            {
                return "Chthonian";
            }
            if (SatelliteType == SubtypeGarden)
            {
                if (HydCoverage < 1)
                {
                    return "Garden";
                }
                return "Oceanic Garden";
            }
            if (SatelliteType == SubtypeGreenhouse)
            {
                if (HydCoverage > 0)
                {
                    return "Wet Greenhouse";
                }
                return "Dry Greenhouse";
            }
            if (SatelliteType == SubtypeHadean)
            {
                return "Hadean";
            }
            if (SatelliteType == SubtypeIce)
            {
                return "Ice";
            }
            if (SatelliteType == SubtypeOcean)
            {
                return "Ocean";
            }
            if (SatelliteType == SubtypeRock)
            {
                return "Rock";
            }
            if (SatelliteType == SubtypeSulfur)
            {
                return "Sulfur";
            }

            //base types
            if (BaseType == BasetypeAsteroidbelt)
            {
                return "Asteroid Belt";
            }
            if (BaseType == BasetypeEmpty)
            {
                return "Empty";
            }
            if (BaseType == BasetypeTerrestial)
            {
                return "Terrestial";
            }
            if (BaseType == BasetypeMoon)
            {
                return "Moon";
            }
            if (BaseType == BasetypeUnset)
            {
                return "Unset";
            }
            if (BaseType == BasetypeGasgiant)
            {
                return "Gas Giant";
            }

            return "???";
        }

        /// <summary>
        ///     Describes the satelite type, given the subtype.
        /// </summary>
        /// <param name="flag">Subtype flag</param>
        /// <param name="hydCoverage">The hydrographic coverage of the planet</param>
        /// <returns>description of the satellite</returns>
        public virtual string ConvSatelliteTypeToString( double hydCoverage, int flag )
        {
            if (flag == SubtypeAmmonia)
            {
                return "Ammonia";
            }
            if (flag == SubtypeChthonian)
            {
                return "Chthonian";
            }
            if (flag == SubtypeGarden)
            {
                if (hydCoverage < 1)
                {
                    return "Garden";
                }
                return "Oceanic Garden";
            }
            if (flag == SubtypeGreenhouse)
            {
                if (hydCoverage > 0)
                {
                    return "Wet Greenhouse";
                }
                return "Dry Greenhouse";
            }
            if (flag == SubtypeHadean)
            {
                return "Hadean";
            }
            if (flag == SubtypeIce)
            {
                return "Ice";
            }
            if (flag == SubtypeOcean)
            {
                return "Ocean";
            }
            if (flag == SubtypeRock)
            {
                return "Rock";
            }
            if (flag == SubtypeSulfur)
            {
                return "Sulfur";
            }

            //base types
            if (flag == BasetypeAsteroidbelt)
            {
                return "Asteroid Belt";
            }
            if (flag == BasetypeEmpty)
            {
                return "Empty";
            }
            if (flag == BasetypeTerrestial)
            {
                return "Terrestial";
            }
            if (flag == BasetypeMoon)
            {
                return "Moon";
            }
            if (flag == BasetypeUnset)
            {
                return "Unset";
            }
            return "???";
        }

        /// <summary>
        ///     Describe the size of the satelite of the self object
        /// </summary>
        /// <returns>Return the description (string)</returns>
        protected virtual string DescribeSatelliteSize()
        {
            if (BaseType == BasetypeMoon || BaseType == BasetypeTerrestial)
            {
                if (SatelliteSize == SizeTiny)
                {
                    return "Tiny";
                }
                if (SatelliteSize == SizeSmall)
                {
                    return "Small";
                }
                if (SatelliteSize == SizeMedium)
                {
                    return "Standard";
                }
                if (SatelliteSize == SizeLarge)
                {
                    return "Large";
                }
                return "???";
            }

            if (BaseType == BasetypeGasgiant)
            {
                if (SatelliteSize == SizeSmall)
                {
                    return "Small";
                }
                if (SatelliteSize == SizeMedium)
                {
                    return "Medium";
                }
                if (SatelliteSize == SizeLarge)
                {
                    return "Large";
                }
                return "???";
            }

            if (BaseType == BasetypeAsteroidbelt)
            {
                if (SatelliteSize == SizeTiny)
                {
                    return "Sparse";
                }
                if (SatelliteSize == SizeSmall)
                {
                    return "Light";
                }
                if (SatelliteSize == SizeMedium)
                {
                    return "Moderate";
                }
                if (SatelliteSize == SizeLarge)
                {
                    return "Dense";
                }
                return "???";
            }

            if (BaseType == BasetypeEmpty)
            {
                return "";
            }

            return "???";
        }

        /// <summary>
        ///     Describe the satellite size given a flag (used for access outside the object)
        /// </summary>
        /// <param name="flag">The flag describing the size of the satellite</param>
        /// <param name="baseType">The flag describing the base type of the satellite</param>
        /// <returns>The description</returns>
        public static string DescribeSatelliteSize( int baseType, int flag )
        {
            if (baseType == BasetypeMoon || baseType == BasetypeTerrestial)
            {
                if (flag == SizeTiny)
                {
                    return "Tiny";
                }
                if (flag == SizeSmall)
                {
                    return "Small";
                }
                if (flag == SizeMedium)
                {
                    return "Standard";
                }
                if (flag == SizeLarge)
                {
                    return "Large";
                }
                return "???";
            }

            if (baseType == BasetypeGasgiant)
            {
                if (flag == SizeSmall)
                {
                    return "Small";
                }
                if (flag == SizeMedium)
                {
                    return "Medium";
                }
                if (flag == SizeLarge)
                {
                    return "Large";
                }
                return "???";
            }

            if (baseType == BasetypeAsteroidbelt)
            {
                if (flag == SizeTiny)
                {
                    return "Sparse";
                }
                if (flag == SizeSmall)
                {
                    return "Light";
                }
                if (flag == SizeMedium)
                {
                    return "Moderate";
                }
                if (flag == SizeLarge)
                {
                    return "Dense";
                }
                return "???";
            }

            return "???";
        }

        /// <summary>
        ///     Returns the planetary diameter in KM, rather than Earth diameters
        /// </summary>
        /// <returns>double diameter in KM</returns>
        public virtual double DiameterInKm()
        {
            return Diameter * 12756;
        }

        public virtual string DescCurrentClimate()
        {
            return GetClimateDesc(GetClimate(SurfaceTemp));
        }

        /// <summary>
        ///     This describes the climate given a flag.
        /// </summary>
        /// <param name="climate">The climate flag</param>
        /// <returns>The description (a string) of this climate</returns>
        public virtual string GetClimateDesc( int climate )
        {
            if (climate == ClimateNone)
            {
                return "Climate: No atmosphere";
            }

            if (climate == ClimateFrozen)
            {
                return "Climate: Frozen (Below -20° F)";
            }
            if (climate == ClimateVerycold)
            {
                return "Climate: Very Cold (Between -20°F and 0°F)";
            }

            if (climate == ClimateCold)
            {
                return "Climate: Cold (Between 0°F and 20°F)";
            }

            if (climate == ClimateChilly)
            {
                return "Climate: Chilly (Between 20°F and 40°F)";
            }

            if (climate == ClimateCool)
            {
                return "Climate: Cool (Between 40°F and 60°F)";
            }

            if (climate == ClimateNormal)
            {
                return "Climate: Normal (Between 60°F and 80°F)";
            }

            if (climate == ClimateWarm)
            {
                return "Climate: Warm (Between 80°F and 100°F)";
            }

            if (climate == ClimateTropical)
            {
                return "Climate: Tropical (Between 100°F and 120°F)";
            }

            if (climate == ClimateHot)
            {
                return "Climate: Hot (Between 120°F and 140°F)";
            }

            if (climate == ClimateVeryhot)
            {
                return "Climate: Very Hot (Between 140°F and 160°F)";
            }

            if (climate == ClimateInfernal)
            {
                return "Climate: Infernal (Above 160°F)";
            }

            return "Climate: ????";
        }

        //calculates the climate given a temperature.
        // like the others, this is overrridable, if you wish to use a different table than the GURPS 4e one. 
        public virtual int GetClimate( double temp )
        {
            if (AtmMass <= 0.01)
            {
                return ClimateNone;
            }
            if (temp <= 244.50)
            {
                return ClimateFrozen;
            }
            if (temp > 244.50 && temp <= 255.50)
            {
                return ClimateVerycold;
            }
            if (temp > 255.50 && temp <= 266.50)
            {
                return ClimateCold;
            }
            if (temp > 266.50 && temp <= 278.50)
            {
                return ClimateChilly;
            }
            if (temp > 278.50 && temp <= 289.50)
            {
                return ClimateCool;
            }
            if (temp > 289.50 && temp <= 300.50)
            {
                return ClimateNormal;
            }
            if (temp > 300.50 && temp <= 311.50)
            {
                return ClimateWarm;
            }
            if (temp > 311.50 && temp <= 322.50)
            {
                return ClimateTropical;
            }
            if (temp > 322.50 && temp <= 333.50)
            {
                return ClimateHot;
            }
            if (temp > 333.50 && temp <= 344.50)
            {
                return ClimateVeryhot;
            }
            if (temp > 344.50)
            {
                return ClimateInfernal;
            }

            return ErrorGeneric;
        }

        /// <summary>
        ///     This describes the current atmospheric category
        /// </summary>
        /// <returns>A string description of the category</returns>
        public virtual string GetDescAtmCategory()
        {
            if (GetAtmCategory() == AtmPresNone)
            {
                return "None";
            }
            if (GetAtmCategory() == AtmPresTrace)
            {
                return "Trace";
            }
            if (GetAtmCategory() == AtmPresVerythin)
            {
                return "Very Thin";
            }
            if (GetAtmCategory() == AtmPresThin)
            {
                return "Thin";
            }
            if (GetAtmCategory() == AtmPresStandard)
            {
                return "Standard";
            }
            if (GetAtmCategory() == AtmPresDense)
            {
                return "Dense";
            }
            if (GetAtmCategory() == AtmPresVerydense)
            {
                return "Very Dense";
            }
            if (GetAtmCategory() == AtmPresSuperdense)
            {
                return "Superdense";
            }

            return "???";
        }

        /// <summary>
        ///     Converting the flags describing various atmospheric conditions
        /// </summary>
        /// <param name="code">The code to be described</param>
        /// <returns>A description of the code</returns>
        public virtual string ConvAtmCodeToString( int code )
        {
            var ret = "";

            //condtionals
            if (code == AtmCondCorrosive)
            {
                return "Corrosive";
            }
            if (code == AtmCondFlamp1)
            {
                return "Flammability Class +1";
            }
            if (code == AtmCondSuffocating)
            {
                return "Suffocating";
            }

            //marginal
            if (code == AtmMargChlorine)
            {
                return "Marginal: Chlorine";
            }
            if (code == AtmMargFlourine)
            {
                return "Marginal: Flourine";
            }
            if (code == AtmMargHighco2)
            {
                return "Marginal: High Carbon Dioxide";
            }
            if (code == AtmMargHighoxy)
            {
                return "Marginal: High Oxygen";
            }
            if (code == AtmMargInert)
            {
                return "Marginal: Inert Gases";
            }
            if (code == AtmMargLowoxy)
            {
                return "Marginal: Low Oxygen";
            }
            if (code == AtmMargNitrogen)
            {
                return "Marginal: Nitrogen";
            }
            if (code == AtmMargOrganic)
            {
                return "Marginal: Organic Toxins";
            }
            if (code == AtmMargPollutants)
            {
                return "Marginal: Pollutants";
            }
            if (code == AtmMargSulfur)
            {
                return "Marginal: Sulfur";
            }

            //toxic
            if (code == AtmToxicHighly)
            {
                return "Highly Toxic";
            }
            if (code == AtmToxicLethally)
            {
                return "Lethally Toxic";
            }
            if (code == AtmToxicMildly)
            {
                return "Mildly Toxic";
            }

            return ret;
        }

        /// <summary>
        ///     Convert the description flag to a string for description
        /// </summary>
        /// <param name="i">The flag</param>
        /// <returns>The string describing the flag</returns>
        public virtual string ConvDescCodeToString( int i )
        {
            if (i == DescFaintringsys)
            {
                return "Faint Ring System";
            }
            if (i == DescRadHighback)
            {
                return "High Background Radiation";
            }
            if (i == DescRadLethalback)
            {
                return "Lethal Background Radiation";
            }
            if (i == DescSpecringsys)
            {
                return "Spectular Ring System";
            }
            if (i == DescSubsurfocean)
            {
                return "Subsurface Ocean";
            }

            return "???";
        }

        /// <summary>
        ///     Generates the eccentricity of the planet's orbit around it's primary.
        /// </summary>
        /// <param name="flag">The gas giant flag</param>
        /// <param name="snowLine">Location of the snow line</param>
        /// <param name="ourDice">Our Ddice object</param>
        public virtual void GetPlanetEccentricity( int flag, double snowLine, Dice ourDice )
        {
            var roll = ourDice.GurpsRoll();
            double mod = 0;

            if (SelfId == 0 && BaseType == BasetypeGasgiant && flag == Star.GasgiantEpistellar)
            {
                roll = roll - 6;
            }
            if (BaseType == BasetypeGasgiant && flag == Star.GasgiantEccentric && OrbitalRadius < snowLine)
            {
                roll = roll + 4;
            }
            if (flag == Star.GasgiantConventional)
            {
                roll = roll - 6;
            }
            if (roll <= 3)
            {
                OrbitalEccent = .0;
            }
            else if (roll >= 4 && roll <= 6)
            {
                OrbitalEccent = .05;
            }
            else if (roll >= 7 && roll <= 9)
            {
                OrbitalEccent = .1;
            }
            else if (roll >= 10 && roll <= 11)
            {
                OrbitalEccent = .15;
            }
            else if (roll == 12)
            {
                OrbitalEccent = .2;
            }
            else if (roll == 13)
            {
                OrbitalEccent = .3;
            }
            else if (roll == 14)
            {
                OrbitalEccent = .4;
            }
            else if (roll == 15)
            {
                OrbitalEccent = .5;
            }
            else if (roll == 16)
            {
                OrbitalEccent = .6;
            }
            else if (roll == 17)
            {
                OrbitalEccent = .7;
            }
            else if (roll >= 18)
            {
                OrbitalEccent = .8;
            }

            if (roll <= 11 && roll != 3)
            {
                mod = ourDice.Rng(1, 5, -2) * .01;
            }
            else if (roll >= 12)
            {
                mod = ourDice.Rng(1, 10, -5) * .01;
            }

            OrbitalEccent = OrbitalEccent + mod;
        }

        /// <summary>
        ///     Generate (and store) the orbital velocity for an object (sidereal period)
        /// </summary>
        /// <param name="ourBag">Ddice object</param>
        public void GenerateOrbitalVelocity( Dice ourBag )
        {
            if (TideTotal < 50)
            {
                var roll = ourBag.GurpsRoll();
                var temp = (int) Math.Floor(roll + TideTotal);

                if (SatelliteSize == SizeTiny)
                {
                    temp = temp + 18;
                }
                if (SatelliteSize == SizeSmall)
                {
                    temp = temp + 14;
                }
                if (SatelliteSize == SizeMedium)
                {
                    temp = temp + 10;
                }
                if (SatelliteSize == SizeLarge)
                {
                    temp = temp + 6;
                }

                if (( roll >= 16 ) || ( temp >= 36 ))
                {
                    switch (ourBag.Rng(2, 6, 0))
                    {
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            SiderealPeriod = temp / 24.0;
                            break;
                        case 7:
                            SiderealPeriod = ourBag.Rng(1, 6) * 2;
                            break;
                        case 8:
                            SiderealPeriod = ourBag.Rng(1, 6) * 5;
                            break;
                        case 9:
                            SiderealPeriod = ourBag.Rng(1, 6) * 10;
                            break;
                        case 10:
                            SiderealPeriod = ourBag.Rng(1, 6) * 20;
                            break;
                        case 11:
                            SiderealPeriod = ourBag.Rng(1, 6) * 50;
                            break;
                        case 12:
                            SiderealPeriod = ourBag.Rng(1, 6) * 100;
                            break;
                    }
                }
                else if (!IsTideLocked)
                {
                    SiderealPeriod = temp / 24.0;
                }

                if (IsTideLocked)
                {
                    SiderealPeriod = OrbitalPeriod;
                }
            }
            if (SiderealPeriod >= OrbitalPeriod)
            {
                IsTideLocked = true;
            }
        }

        /// <summary>
        ///     This function creates a generic name for satellites and moons
        /// </summary>
        /// <param name="parentName">The star's name</param>
        /// <param name="systemName">The system's name</param>
        public void GenGenericName( string parentName, string systemName )
        {
            GenGenericName();
            if (BaseType == BasetypeTerrestial || BaseType == BasetypeGasgiant)
            {
                if (ParentId >= 9000 && ParentId <= 9050)
                {
                    Name = parentName + ( SelfId + 1 );
                }
                else
                {
                    if (ParentId == OrbitPrisec)
                    {
                        Name = systemName + "-AB" + ( SelfId + 1 );
                    }
                    if (ParentId == OrbitPrisectri)
                    {
                        Name = systemName + "-ABC" + ( SelfId + 1 );
                    }
                    if (ParentId == OrbitPritri)
                    {
                        Name = systemName + "-AC" + ( SelfId + 1 );
                    }
                    if (ParentId == OrbitSeccom)
                    {
                        Name = systemName + "-BD" + ( SelfId + 1 );
                    }
                    if (ParentId == OrbitSectri)
                    {
                        Name = systemName + "-BC" + ( SelfId + 1 );
                    }
                    if (ParentId == OrbitTricom)
                    {
                        Name = systemName + "-CE" + ( SelfId + 1 );
                    }
                }
            }
            if (BaseType == BasetypeMoon)
            {
                string[] moonStr =
                {
                    "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII", "XIII", "XIV", "XV"
                };
                Name = parentName + moonStr[SelfId];
            }
        }

        /// <summary>
        ///     Creaates moons around sateliets according to GURPS 4e rules.
        /// </summary>
        /// <param name="sysName">The system name</param>
        /// <param name="ourBag">Ddice object used in rolling</param>
        /// <param name="flag">The OptionCont flag describing where we put moon orbits</param>
        public void CreateMoons( string sysName, Dice ourBag, int flag = 0 )
        {
            string[] moonletNames =
            {
                "Alpha", "Beta", "Gamma", "Delta", "Epsilon", "Zeta", "Eta", "Theta", "Iota", "Kappa", "Lambda", "Mu", "Nu", "Ksi", "Omicron", "Pi", "Rho", "Sigma", "Tau", "Upsilon", "Phi", "Chi", "Psi", "Omega"
            };

            //initiate objects
            var currMoonlet = 0;
            var occupiedOrbits = new List<double>();
            double currOrbit;

            int numRoll, roll; //roll variables

            //terrestial
            if (BaseType == BasetypeTerrestial)
            {
                //moonlets
                numRoll = ourBag.Rng(1, 6, -2);

                //modifiers for moonlets
                if (SatelliteSize == SizeTiny)
                {
                    numRoll = numRoll - 2;
                }
                if (SatelliteSize == SizeSmall)
                {
                    numRoll = numRoll - 1;
                }
                if (SatelliteSize == SizeLarge)
                {
                    numRoll = numRoll + 1;
                }
                if (OrbitalRadius <= 1.5 && .75 < OrbitalRadius)
                {
                    numRoll = numRoll - 1;
                }
                if (OrbitalRadius <= .75 && .5 < OrbitalRadius)
                {
                    numRoll = numRoll - 3;
                }

                if (OrbitalRadius <= .5)
                {
                    numRoll = 0; //set to 0, since we cannot have any in this range
                }

                if (numRoll > 0)
                {
                    for (var i = 0; i < numRoll; i++)
                    {
                        do
                        {
                            roll = ourBag.Rng(1, 6, 4);
                            currOrbit = roll / 4.0;
                        }
                        while (ScanOccupiedOrbits(occupiedOrbits, currOrbit));

                        occupiedOrbits.Add(currOrbit);
                        InnerMoonlets.Add(new Moonlet(SelfId, i, currOrbit, moonletNames[currMoonlet]));
                        InnerMoonlets[i].OrbitalRadius = InnerMoonlets[i].PlanetRadius * Diameter;
                        currMoonlet++;
                    }
                }

                //major moons
                numRoll = ourBag.Rng(1, 6, -4);

                if (SatelliteSize == SizeTiny)
                {
                    numRoll = numRoll - 2;
                }
                if (SatelliteSize == SizeSmall)
                {
                    numRoll = numRoll + 1;
                }
                if (SatelliteSize == SizeLarge)
                {
                    numRoll = numRoll - 1;
                }
                if (OrbitalRadius <= 1.5 && .75 < OrbitalRadius)
                {
                    numRoll = numRoll - 1;
                }

                if (OrbitalRadius <= .75)
                {
                    numRoll = 0;
                }

                if (OptionCont.GetNumberOfMoonsOverGarden() != -1 && SatelliteType == SubtypeGarden)
                {
                    numRoll = OptionCont.GetNumberOfMoonsOverGarden();
                }

                if (numRoll > 0)
                {
                    for (var i = 0; i < numRoll; i++)
                    {
                        var size = SizeMedium;

                        roll = ourBag.Rng(3, 6, 0);
                        if (SatelliteSize == SizeTiny)
                        {
                            size = SizeTiny;
                        }

                        if (SatelliteSize == SizeSmall)
                        {
                            size = SizeTiny;
                        }

                        if (SatelliteSize == SizeMedium)
                        {
                            size = roll >= 10 ? SizeSmall : SizeTiny;
                        }

                        if (SatelliteSize == SizeLarge)
                        {
                            if (roll >= 15)
                            {
                                size = SizeMedium;
                            }
                            if (roll >= 12 && roll <= 14)
                            {
                                size = SizeSmall;
                            }
                            size = SizeTiny;
                        }

                        do
                        {
                            var mods = 0; //roll variables
                            if (SatelliteType - size == 2)
                            {
                                mods = 2;
                            }
                            if (SatelliteType - size == 1)
                            {
                                mods = 4;
                            }

                            if (flag == OptionCont.MoonBook)
                            {
                                roll = ourBag.Rng(2, 6, mods);
                            }
                            if (flag == OptionCont.MoonBookhigh)
                            {
                                roll = ourBag.Rng(1, 6, mods + 6);
                            }
                            if (flag == OptionCont.MoonExpand)
                            {
                                roll = ourBag.Rng(2, 10, mods);
                            }
                            if (flag == OptionCont.MoonExpandhigh)
                            {
                                roll = ourBag.Rng(2, 6, mods + 12);
                            }

                            currOrbit = roll * 2.5;
                        }
                        while (ScanOccupiedOrbits(occupiedOrbits, currOrbit) && !WithinOtherOrbits(occupiedOrbits, currOrbit, 5.0));

                        occupiedOrbits.Add(currOrbit);
                        MajorMoons.Add(new Satellite(OrbitPlanet, i, currOrbit * Diameter, i, BasetypeMoon));
                        MajorMoons[i].UpdateSize(size);
                        MajorMoons[i].MoonRadius = currOrbit;
                        MajorMoons[i].BlackbodyTemp = BlackbodyTemp;
                        MajorMoons[i].ParentDiam = Diameter * 12756.2;
                    }
                }
            }

            //gas giant
            if (BaseType == BasetypeGasgiant)
            {
                //moonlets (inner)
                numRoll = ourBag.Rng(2, 6, 0);
                if (OrbitalRadius <= .1)
                {
                    numRoll = numRoll - 10;
                }
                else if (OrbitalRadius > .1 && .5 >= OrbitalRadius)
                {
                    numRoll = numRoll - 8;
                }
                else if (OrbitalRadius > .5 && .75 >= OrbitalRadius)
                {
                    numRoll = numRoll - 6;
                }
                else if (OrbitalRadius > .75 && 1.5 >= OrbitalRadius)
                {
                    numRoll = numRoll - 3;
                }

                if (numRoll > 0)
                {
                    for (var i = 0; i < numRoll; i++)
                    {
                        currOrbit = ourBag.Rng(1, 6, 4) * .25;
                        InnerMoonlets.Add(new Moonlet(SelfId, currMoonlet, currOrbit, moonletNames[currMoonlet]));
                        InnerMoonlets[i].OrbitalRadius = currOrbit * Diameter;
                        currMoonlet++;
                    }
                }

                //major moons
                numRoll = ourBag.Rng(1, 6, 0);
                if (OrbitalRadius <= .1)
                {
                    numRoll = numRoll - 6;
                }
                else if (OrbitalRadius > .1 && .5 >= OrbitalRadius)
                {
                    numRoll = numRoll - 5;
                }
                else if (OrbitalRadius > .5 && .75 >= OrbitalRadius)
                {
                    numRoll = numRoll - 4;
                }
                else if (OrbitalRadius > .75 && 1.5 >= OrbitalRadius)
                {
                    numRoll = numRoll - 1;
                }

                if (numRoll > 0)
                {
                    for (var i = 0; i < numRoll; i++)
                    {
                        var size = SizeMedium;

                        roll = ourBag.GurpsRoll();
                        if (roll >= 15)
                        {
                            size = SizeMedium;
                        }
                        if (roll >= 12 && roll <= 14)
                        {
                            size = SizeSmall;
                        }
                        if (roll < 12)
                        {
                            size = SizeTiny;
                        }

                        do
                        {
                            roll = ourBag.Rng(3, 6, 3);
                            if (roll >= 15)
                            {
                                roll = roll + ourBag.Rng(2, 6, 0);
                            }
                            currOrbit = roll / 2.0;
                        }
                        while (ScanOccupiedOrbits(occupiedOrbits, currOrbit) && !WithinOtherOrbits(occupiedOrbits, currOrbit, 1.0));

                        occupiedOrbits.Add(currOrbit);
                        MajorMoons.Add(new Satellite(OrbitPlanet, i, currOrbit * Diameter, i, BasetypeMoon));
                        MajorMoons[i].UpdateSize(size);
                        MajorMoons[i].MoonRadius = currOrbit;
                        MajorMoons[i].BlackbodyTemp = BlackbodyTemp;
                        MajorMoons[i].ParentDiam = Diameter * 12756.2;
                    }
                }

                //Captured Moons
                numRoll = ourBag.Rng(1, 6, 0);
                if (OrbitalRadius <= .5)
                {
                    numRoll = numRoll - 6;
                }
                else if (OrbitalRadius > .5 && .75 >= OrbitalRadius)
                {
                    numRoll = numRoll - 5;
                }
                else if (OrbitalRadius > .75 && 1.5 >= OrbitalRadius)
                {
                    numRoll = numRoll - 4;
                }
                else if (OrbitalRadius > 1.5 && 3 >= OrbitalRadius)
                {
                    numRoll = numRoll - 1;
                }

                if (numRoll > 0)
                {
                    for (var i = 0; i < numRoll; i++)
                    {
                        do
                        {
                            roll = ourBag.Rng(1, 280, 20);
                            currOrbit = roll;
                        }
                        while (ScanOccupiedOrbits(occupiedOrbits, currOrbit));
                        occupiedOrbits.Add(currOrbit);

                        OuterMoonlets.Add(new Moonlet(SelfId, currMoonlet, currOrbit, moonletNames[currMoonlet]));
                        OuterMoonlets[i].OrbitalRadius = currMoonlet * Diameter;
                        currMoonlet++;
                    }
                }
            }

            if (BaseType == BasetypeGasgiant)
            {
                if (InnerMoonlets.Count >= 10)
                {
                    UpdateDescListing(DescSpecringsys);
                }
                if (InnerMoonlets.Count >= 6 && InnerMoonlets.Count < 9)
                {
                    UpdateDescListing(DescFaintringsys);
                }
            }
        }

        /// <summary>
        ///     This function describes the atm in a string.
        /// </summary>
        /// <returns></returns>
        public string DescAtm()
        {
            var dA = new List<string>();

            //find conditions
            foreach (var i in AtmCate)
            {
                if (i > AtmBaseCond && i < CondIncrement + AtmBaseCond)
                {
                    dA.Add("Special Condition");
                }
                if (i > AtmBaseMarginal && i < MarginalIncrement + AtmBaseMarginal)
                {
                    dA.Add("Marginal");
                }
                if (i > AtmBaseToxic && i < ToxicIncrement + AtmBaseToxic)
                {
                    dA.Add("Toxic");
                }
            }

            var desc = "";
            for (var i = 0; i < dA.Count; i++)
            {
                desc += dA[i];
                if (i + 1 < dA.Count)
                {
                    desc += " & ";
                }
            }

            return desc;
        }

        /// <summary>
        ///     Updates the description of the atmosphere
        /// </summary>
        /// <param name="flag">The flag we're adding to the atmosphere</param>
        public void UpdateDescListing( int flag )
        {
            DescListing.Add(flag);
        }

        /// <summary>
        ///     Describes a planet in the format of Large(Rock) for example
        /// </summary>
        /// <returns>string describing size(type)</returns>
        public string DescSizeType()
        {
            return DescribeSatelliteSize() + "(" + ConvSatelliteTypeToString() + ")";
        }

        /// <summary>
        ///     A helper function to scan for occupied orbits (during moon generation)
        /// </summary>
        /// <param name="occuOrbits">The list of occupied orbits</param>
        /// <param name="current">The orbit to add</param>
        /// <returns>True if there is an orbit conflict, false otherwise</returns>
        protected static bool ScanOccupiedOrbits( List<double> occuOrbits, double current )
        {
            return occuOrbits.Any(orbit => Math.Abs(orbit - current) < 0);
        }

        /// <summary>
        ///     Another helper function: makes sure that the orbit is not within the safety margin
        /// </summary>
        /// <param name="occuOrbits">The list of current objects</param>
        /// <param name="current">the orbit to be added</param>
        /// <param name="margin">The margin of safety</param>
        /// <returns></returns>
        protected static bool WithinOtherOrbits( List<double> occuOrbits, double current, double margin )
        {
            return occuOrbits.Any(orbit => orbit + margin <= current);
        }

        /// <summary>
        ///     The ToString Object for our planet or moon
        /// </summary>
        /// <returns>A description of the object</returns>
        public override string ToString()
        {
            string ret;
            var nL = Environment.NewLine;
            const string spacing = "    ";
            var numOfDigits = OptionCont.NumberOfDecimal;
            const int numOfSmallDigits = 2;

            if (RotationalPeriod < 0)
            {
                RotationalPeriod = RotationalPeriod * -1;
                RetrogradeMotion = true;
            }

            //short cut.
            if (BaseType == BasetypeUnset)
            {
                ret = "[ORBIT " + ( SelfId + 1 ) + "] Unset Orbital at " + Math.Round(OrbitalRadius, numOfDigits) + " AU.";
                return ret;
            }

            if (BaseType == BasetypeEmpty)
            {
                ret = "[ORBIT " + ( SelfId + 1 ) + "] Empty Orbital at " + Math.Round(OrbitalRadius, numOfDigits) + " AU.";
                return ret;
            }

            if (BaseType == BasetypeAsteroidbelt)
            {
                ret = "[ORBIT " + ( SelfId + 1 ) + "]";

                if (OptionCont.ExpandAsteroidBelt != null && (bool) OptionCont.ExpandAsteroidBelt)
                {
                    ret = ret + nL + spacing + "Asteroid Belt (" + DescribeSatelliteSize() + ")";
                }
                else
                {
                    ret = ret + nL + spacing + "Asteroid Belt";
                }

                ret = ret + " and orbits at " + Math.Round(OrbitalRadius, numOfDigits) + " AU.";
                ret = ret + nL + spacing + "Blackbody Temperature is " + Math.Round(BlackbodyTemp, numOfSmallDigits) + "K";
                ret = ret + nL + spacing + "RVM: " + Rvm + " (" + GetRvmDesc() + ")";
                return ret;
            }

            //main block
            if (BaseType == BasetypeMoon)
            {
                ret = "[MOON " + ( SelfId + 1 ) + "]";
                ret = ret + nL + spacing + Name + " is a ";
            }
            else
            {
                ret = "[ORBIT " + ( SelfId + 1 ) + "]";
                ret = ret + nL + spacing + Name + " is a ";
            }

            if (BaseType != BasetypeGasgiant)
            {
                ret = ret + DescribeSatelliteSize() + " (" + ConvSatelliteTypeToString() + ")";
            }
            else
            {
                ret = ret + "Gas Giant (" + DescribeSatelliteSize() + ")";
            }

            if (BaseType != BasetypeMoon)
            {
                ret = ret + " and orbits at " + Math.Round(OrbitalRadius, numOfDigits) + " AU";
                if (RetrogradeMotion)
                {
                    ret = ret + " in a retrograde manner.";
                }
                else
                {
                    ret = ret + ".";
                }
            }
            else
            {
                ret = ret + " and orbits at " + Math.Round(OrbitalRadius, numOfDigits) + " earth diameters.";
                ret = ret + nL + spacing + "Planetary Diameters: " + MoonRadius;
                if (RetrogradeMotion)
                {
                    ret = ret + nL + spacing + "Orbits in a retrograde manner.";
                }
            }

            ret = ret + nL + spacing + "Blackbody Temperature is " + Math.Round(BlackbodyTemp, numOfSmallDigits) + "K";

            ret = ret + nL + spacing + "Orbital Parent: " + Star.GetDescSelfFlag(ParentId) + ".";

            ret = ret + nL;

            //atmospheric data
            if (BaseType == BasetypeMoon || BaseType == BasetypeTerrestial)
            {
                ret = ret + nL + spacing + "Atmospheric Data:";
                ret = ret + nL + spacing + "Pressure: " + GetDescAtmCategory() + " (" + Math.Round(AtmPres, numOfSmallDigits + 1) + " atm).";
                ret = ret + nL + spacing + "Atmospheric Mass: " + AtmMass + " standard atmospheric mass";

                if (AtmCate.Count > 0)
                {
                    ret = ret + nL + spacing + "Atm Notes: ";
                    for (var i = 0; i < AtmCate.Count; i++)
                    {
                        if (i != AtmCate.Count - 1)
                        {
                            ret = ret + ConvAtmCodeToString(AtmCate[i]) + ", ";
                        }
                        else
                        {
                            ret = ret + ConvAtmCodeToString(AtmCate[i]);
                        }
                    }
                }

                ret = ret + nL;

                //climate data.
                ret = ret + nL + spacing + "Climate Data:";

                if (IsTideLocked)
                {
                    var temp = SurfaceTemp * DayFaceMod;
                    ret = ret + nL + spacing + "Day Side Surface Temperature: " + TempInKelFarCel(temp);
                    if (AtmPres > 0)
                    {
                        ret = ret + nL + spacing + "Day Side Climate: " + GetClimateDesc(GetClimate(temp));
                    }

                    temp = SurfaceTemp * NightFaceMod;
                    ret = ret + nL + spacing + "Night Side Surface Temperature: " + TempInKelFarCel(temp);
                    if (AtmPres > 0)
                    {
                        ret = ret + nL + spacing + "Night Side Climate: " + GetClimateDesc(GetClimate(temp));
                    }
                }
                else
                {
                    ret = ret + nL + spacing + "Surface Temperature: " + TempInKelFarCel(SurfaceTemp);
                    if (AtmPres > 0)
                    {
                        ret = ret + nL + spacing + GetClimateDesc(GetClimate(SurfaceTemp));
                    }
                }
                ret = ret + nL;
            }

            //physical properties
            ret = ret + nL + spacing + "Physical Properties:";
            ret = ret + nL + spacing + "Density: " + Density + " Earth densities (" + Density * ConvfacDensity + " g/cc)";
            ret = ret + nL + spacing + "Diameter: " + Math.Round(Diameter, 3) + " Earth diameters (" + Math.Round(Diameter * ConvfacDiameter, 3) + " km)";
            ret = ret + nL + spacing + "Mass: " + Math.Round(Mass, 3) + " Earth masses";
            ret = ret + nL + spacing + "Axial Tilt: " + AxialTilt + "°";
            ret = ret + nL + spacing + "Gravity: " + Math.Round(Gravity, 3) + " Earth gravities (" + Math.Round(Gravity * ConvfacGravity, 3) + " m/s²)";
            ret = ret + nL + spacing + "RVM: " + Rvm + " (" + GetRvmDesc() + "), Tectonic: " + GetTecDesc() + ", Volcanic: " + GetVolDesc();
            ret = ret + nL + spacing + "Hydrographic Coverage: " + $"{HydCoverage:P}";

            //orbital properties
            ret = ret + nL;
            ret = ret + nL + spacing + "Orbital Properties:";
            ret = ret + nL + spacing + "Orbital Period: " + Math.Round(OrbitalPeriod, 3) + "d (" + Math.Round(OrbitalPeriod / 365.25, 3) + "y).";

            if (!IsTideLocked)
            {
                ret = ret + nL + spacing + "Sidereal Period: " + Math.Round(SiderealPeriod, 3) + "d.";
                ret = ret + " Solar Day: " + Math.Round(RotationalPeriod, 3) + "d.";
            }

            //tide locked.
            if (IsTideLocked && !IsResonant)
            {
                ret = ret + nL + spacing + "This satellite is tide locked.";
            }
            else if (IsResonant)
            {
                ret = ret + nL + spacing + "This satellite is locked in a resonant pattern.";
            }

            //tide data
            if (OptionCont.AlwaysDisplayTidalData != null && ( HydCoverage > 0 || (bool) OptionCont.AlwaysDisplayTidalData ))
            {
                ret = ret + nL;
                ret = ret + nL + spacing + "Tidal Data:";
                if (OptionCont.GetVerboseOutput() || (bool) OptionCont.AlwaysDisplayTidalData)
                {
                    ret = ret + nL + spacing + "Total tidal force: " + Math.Round(TideTotal, 3) + " units";
                }
                if (OptionCont.GetVerboseOutput() || (bool) OptionCont.AlwaysDisplayTidalData)
                {
                    ret = ret + nL;
                }

                ret = ret + DisplayTidalData() + nL;
            }

            //description block

            if (DescListing.Count > 0)
            {
                ret = ret + nL;
                ret = ret + nL + spacing + "Special Description Notes: ";
                ret = ret + " ";
                for (var i = 0; i < DescListing.Count; i++)
                {
                    if (i == DescListing.Count - 1)
                    {
                        ret = ret + ConvDescCodeToString(DescListing[i]) + " ";
                    }
                    else
                    {
                        ret = ret + ConvDescCodeToString(DescListing[i]) + ", ";
                    }
                }
            }

            ////moon block
            //if (this.baseType == Satellite.BASETYPE_GASGIANT)
            //{
            //    ret = ret + nL;
            //    if (this.innerMoonlets.Count > 0)
            //    {
            //        ret = ret + nL + spacing + "Inner Moonlets:";
            //        ret = ret + nL + spacing;
            //        foreach (Moonlet m in this.innerMoonlets)
            //        {
            //            ret = ret + m + nL + spacing;
            //        }
            //    }
            //    else
            //    {
            //        ret = ret + nL + spacing + "Inner Moonlets: 0";
            //        ret = ret + nL;
            //    }

            //    if (this.outerMoonlets.Count > 0)
            //    {
            //        ret = ret + nL + spacing + "Outer Moonlets:";
            //        ret = ret + nL + spacing;
            //        foreach (Moonlet m in this.outerMoonlets)
            //        {
            //            ret = ret + m + " " + nL + spacing;
            //        }
            //    }
            //    else
            //    {
            //        ret = ret + nL + spacing + "Outer Moonlets: 0";
            //        ret = ret + nL;
            //    }

            //    //major moons
            //    if (this.majorMoons.Count > 0)
            //    {
            //        ret = ret + nL + spacing + "Major Moons" + nL;
            //        foreach (Satellite s in this.majorMoons)
            //            ret = ret + spacing + s + nL;
            //    }
            //    else
            //    {
            //        ret = ret + nL + spacing + "Major Moons: 0";
            //        ret = ret + nL;
            //    }
            //}

            //if (this.baseType == Satellite.BASETYPE_TERRESTIAL)
            //{
            //    if (this.innerMoonlets.Count > 0)
            //    {
            //        ret = ret + nL + spacing + "Inner Moonlets:";
            //        ret = ret + nL + spacing;
            //        foreach (Moonlet m in this.innerMoonlets)
            //        {
            //            ret = ret + m + " " + nL + spacing;
            //        }
            //    }
            //    else
            //    {
            //        ret = ret + nL + spacing + "Inner Moonlets: 0";
            //        ret = ret + nL;
            //    }

            //    //major moons
            //    if (this.majorMoons.Count > 0)
            //    {
            //        ret = ret + nL + spacing + "Major Moons" + nL;
            //        foreach (Satellite s in this.majorMoons)
            //            ret = ret + nL + s + nL;
            //    }
            //    else
            //    {
            //        ret = ret + nL + spacing + "Major Moons: 0";
            //        ret = ret + nL;
            //    }
            //}

            return ret;
        }

        /// <summary>
        ///     This lists Kelvin, Farenheit and Celsius temperatures.
        /// </summary>
        /// <param name="temp">The temperature in Kelvin</param>
        /// <returns>A string describing all three</returns>
        protected string TempInKelFarCel( double temp )
        {
            var ret = "";
            ret = ret + Math.Round(temp, 3) + "K, ";
            ret = ret + Math.Round(( temp - 273.15 ) * 1.8 + 32, 3) + "°F, ";
            ret = ret + Math.Round(temp - 273.15, 3) + "°C";
            return ret;
        }

        /// <summary>
        ///     Calculates the total tidal force acting on an object
        /// </summary>
        /// <param name="sysAge">The age of the system</param>
        /// <returns>The Tidal Force</returns>
        public double TotalTidalForce( double sysAge )
        {
            var val = TideForce.Where(tideData => OptionCont.IgnoreLunarTidesOnGardenWorlds != null && !( tideData.Key >= TideMoon1 && tideData.Key <= TideMoon10 && (bool) OptionCont.IgnoreLunarTidesOnGardenWorlds ))
                .Aggregate(0.0, ( current, tideData ) => current + tideData.Value);

            val = val * sysAge / Mass;
            return val;
        }

        /// <summary>
        ///     Output formatted tidal data
        /// </summary>
        /// <returns>Formatted Tidal Data</returns>
        public string DisplayTidalData()
        {
            var ret = "";
            var nL = Environment.NewLine;
            const string spacing = "    ";
            var ourFlag = " ";

            foreach (var tideData in TideForce)
            {
                var addStr = true;
                if (tideData.Key == TideMoon1)
                {
                    ourFlag = "moon one";
                }
                if (tideData.Key == TideMoon2)
                {
                    ourFlag = "moon two";
                }
                if (tideData.Key == TideMoon3)
                {
                    ourFlag = "moon three";
                }
                if (tideData.Key == TideMoon4)
                {
                    ourFlag = "moon four";
                }
                if (tideData.Key == TideMoon5)
                {
                    ourFlag = "moon five";
                }
                if (tideData.Key == TideMoon6)
                {
                    ourFlag = "moon six";
                }
                if (tideData.Key == TideMoon7)
                {
                    ourFlag = "moon seven";
                }
                if (tideData.Key == TideMoon8)
                {
                    ourFlag = "moon eight";
                }
                if (tideData.Key == TideMoon9)
                {
                    ourFlag = "moon nine";
                }
                if (tideData.Key == TideMoon10)
                {
                    ourFlag = "moon ten";
                }

                if (tideData.Key == TidePrimarystar)
                {
                    ourFlag = "the primary star";
                }
                if (tideData.Key == TideSecondarystar)
                {
                    ourFlag = "the secondary star";
                }
                if (tideData.Key == TideTrinarystar)
                {
                    ourFlag = "the trinary star";
                }
                if (tideData.Key == TideSeccompstar)
                {
                    ourFlag = "the secondary companion star";
                }
                if (tideData.Key == TideTricompstar)
                {
                    ourFlag = "the trinary companion star";
                }
                if (tideData.Key == TideParplanet)
                {
                    ourFlag = "parent planet";
                }

                var tideVal = tideData.Value;
                var toBeAdded = nL + spacing + "Tidal Force generated by " + ourFlag + " is " + $"{tideVal:N2}" + "ft amplitude";

                if (tideData.Key >= TideMoonBase && tideData.Key <= TideMoonBase + 10)
                {
                    if (OptionCont.IgnoreLunarTidesOnGardenWorlds != null && (bool) OptionCont.IgnoreLunarTidesOnGardenWorlds && SatelliteType == SubtypeGarden)
                    {
                        addStr = false;
                    }
                }

                if (addStr)
                {
                    ret = ret + toBeAdded;
                }
            }

            return ret;
        }
    }
}