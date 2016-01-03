using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniverseGeneration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace SWNAdmin
{
    public class SqlManager
    {
        static string projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

        public static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + projectDir + @"\Utility\Db1.mdf;Integrated Security=True;Connect Timeout=30");

        public static bool Connect()
        {
            //Neue SQL Verbindung
            //Öffne die Verbindung
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
           
            return true;
        }

        public static bool Disconnect()
        {
            con.Close();
            return true;
        }

        public static void InsertSystem(StarSystem system)
        {
            using (var context = new Utility.Db1Entities())
            {
                Utility.StarSystems NewSystem = new Utility.StarSystems();
                NewSystem.sysName = system.sysName;
                NewSystem.habitableZones = system.habitableZones.Count;
                NewSystem.maxMass = system.maxMass;
                NewSystem.numDwarfPlanets = system.numDwarfPlanets;
                NewSystem.star2index = system.star2index;
                NewSystem.star3index = system.star3index;
                NewSystem.subCompanionStar2index = system.subCompanionStar2index;
                NewSystem.subCompanionStar3index = system.subCompanionStar3index;
                NewSystem.sysAge = system.sysAge;
                NewSystem.sysStars = system.sysStars.Count;
                foreach (Star star in system.sysStars)
                {
                    Utility.Stars NewStar = new Utility.Stars();
                    NewStar.currLumin = Math.Round(star.currLumin, 4);
                    NewStar.distFromPrimary = star.distFromPrimary;
                    NewStar.effTemp = Math.Round(star.effTemp,4);
                    NewStar.gasGiantFlag = star.gasGiantFlag;
                    NewStar.initLumin = star.initLumin;
                    NewStar.initMass = star.initMass;
                    NewStar.isFlareStar = star.isFlareStar;
                    NewStar.name = star.name;
                    NewStar.orbitalEccent = star.orbitalEccent;
                    NewStar.orbitalPeriod = Math.Round(star.orbitalPeriod,4);
                    NewStar.orbitalRadius = star.orbitalRadius;
                    NewStar.orbitalSep = star.orbitalSep;
                    NewStar.orderID = star.orderID;
                    NewStar.parentID = star.parentID;
                    NewStar.parentName = star.parentName;
                    NewStar.radius = Math.Round(star.getRadiusAU(), 4);//radius;
                    NewStar.selfID = star.selfID;
                    NewStar.specType = star.specType;
                    NewStar.starAge = star.starAge;
                    NewStar.starColor = star.starColor;
                    NewStar.sysPlanets = star.sysPlanets.Count();
                    NewStar.StarOrder = Star.getDescFromFlag(star.selfID);
                    NewStar.StarOrder = star.returnCurrentBranchDesc();
                    NewStar.OrbitalDetails = star.printOrbitalDetails();
                    NewStar.StarString = star.ToString();
                    

                    foreach (Satellite Sat in star.sysPlanets)
                    {
                        Utility.Planets NewSatellite = new Utility.Planets();
                        string type = "";
                        string Pressure = "";
                        if (Sat.baseType != Satellite.BASETYPE_ASTEROIDBELT || OptionCont.expandAsteroidBelt)
                        {
                            type = Sat.descSizeType();
                        }
                        if (Sat.baseType == Satellite.BASETYPE_ASTEROIDBELT)
                        {
                            type = "Asteroid Belt";
                        }

                        if (Sat.baseType == Satellite.BASETYPE_ASTEROIDBELT)
                            Pressure = "None.";

                        if (Sat.baseType == Satellite.BASETYPE_GASGIANT)
                            Pressure = "Superdense Atmosphere.";

                        if (Sat.baseType == Satellite.BASETYPE_MOON || Sat.baseType == Satellite.BASETYPE_TERRESTIAL)
                            Pressure = Sat.getDescAtmCategory() + "(" + Math.Round(Sat.atmPres, 2) + ")";

                        NewSatellite.SatelliteSize = Sat.SatelliteSize;
                        NewSatellite.SatelliteType = Sat.SatelliteType;
                        NewSatellite.atmCate = Sat.atmCate.Count;
                        NewSatellite.atmMass = Sat.atmMass;
                        NewSatellite.atmPres = Pressure;
                        NewSatellite.axialTilt = Sat.axialTilt;
                        NewSatellite.baseType = Sat.baseType;
                        NewSatellite.blackbodyTemp = Sat.blackbodyTemp;
                        NewSatellite.dayFaceMod = Convert.ToInt32(Sat.dayFaceMod);
                        NewSatellite.density = Sat.density;
                        NewSatellite.diameter = Math.Round(Sat.diameterInKM(), 2);
                        NewSatellite.gravity = Math.Round(Sat.gravity * Satellite.GFORCE, 2);
                        NewSatellite.hydCoverage = ((Sat.hydCoverage * 100) + "%");
                        NewSatellite.innerMoonlets = Sat.innerMoonlets.Count;
                        NewSatellite.isResonant = Sat.isResonant;
                        NewSatellite.isTideLocked = Sat.isTideLocked;
                        NewSatellite.majorMoons = Sat.majorMoons.Count;
                        NewSatellite.mass = Sat.mass;
                        NewSatellite.masterOrderID = Sat.masterOrderID;
                        NewSatellite.moonRadius = Sat.moonRadius;
                        NewSatellite.name = Sat.name;
                        NewSatellite.nightFaceMod = Convert.ToInt32(Sat.nightFaceMod);
                        NewSatellite.orbitalCycle = Sat.orbitalCycle;
                        NewSatellite.orbitalEccent = Sat.orbitalEccent;
                        NewSatellite.orbitalPeriod = Sat.orbitalPeriod;
                        NewSatellite.orbitalRadius = Math.Round(Sat.orbitalRadius, 2);
                        NewSatellite.outerMoonlets = Sat.outerMoonlets.Count;
                        NewSatellite.parentDiam = Sat.parentDiam;
                        NewSatellite.parentID = Sat.parentID;
                        NewSatellite.parentName = Sat.parentName;
                        NewSatellite.retrogradeMotion = Sat.retrogradeMotion;
                        NewSatellite.rotationalPeriod = Sat.rotationalPeriod;
                        NewSatellite.selfID = Sat.selfID;
                        NewSatellite.siderealPeriod = Sat.siderealPeriod;
                        NewSatellite.surfaceTemp = Sat.surfaceTemp;
                        NewSatellite.tecActivity = Sat.tecActivity;
                        NewSatellite.tideTotal = Sat.tideTotal;
                        NewSatellite.volActivity = Sat.volActivity;
                        NewSatellite.sattype = type;
                        NewSatellite.atmnote = Sat.descAtm();
                        NewSatellite.RVM = Sat.getRVMDesc();
                        NewSatellite.planetString = Sat.ToString();

                        foreach (Moonlet InnerMoonlet in Sat.innerMoonlets)
                        {
                            Utility.InnerMoonlets NewInnerMoonlet = new Utility.InnerMoonlets();
                            NewInnerMoonlet.blackbodyTemp = InnerMoonlet.blackbodyTemp;
                            NewInnerMoonlet.name = InnerMoonlet.name;
                            NewInnerMoonlet.orbitalEccent = InnerMoonlet.orbitalEccent;
                            NewInnerMoonlet.orbitalPeriod = InnerMoonlet.orbitalPeriod;
                            NewInnerMoonlet.orbitalRadius = Math.Round(InnerMoonlet.orbitalRadius,2);
                            NewInnerMoonlet.parentID = InnerMoonlet.parentID;
                            NewInnerMoonlet.parentName = InnerMoonlet.parentName;
                            NewInnerMoonlet.planetRadius = InnerMoonlet.planetRadius;
                            NewInnerMoonlet.selfID = InnerMoonlet.selfID;
                            NewInnerMoonlet.innerMoonString = InnerMoonlet.ToString();
                            NewSatellite.InnerMoonlets1.Add(NewInnerMoonlet);
                        }
                        foreach (Moonlet OuterMoonlet in Sat.outerMoonlets)
                        {
                            Utility.OuterMoonlets NewOuterMoonlet = new Utility.OuterMoonlets();
                            NewOuterMoonlet.blackbodyTemp = OuterMoonlet.blackbodyTemp;
                            NewOuterMoonlet.name = OuterMoonlet.name;
                            NewOuterMoonlet.orbitalEccent = OuterMoonlet.orbitalEccent;
                            NewOuterMoonlet.orbitalPeriod = OuterMoonlet.orbitalPeriod;
                            NewOuterMoonlet.orbitalRadius = Math.Round(OuterMoonlet.orbitalRadius,2);
                            NewOuterMoonlet.parentID = OuterMoonlet.parentID;
                            NewOuterMoonlet.parentName = OuterMoonlet.parentName;
                            NewOuterMoonlet.planetRadius = OuterMoonlet.planetRadius;
                            NewOuterMoonlet.selfID = OuterMoonlet.selfID;
                            NewOuterMoonlet.outerMoonString = OuterMoonlet.ToString();
                            NewSatellite.OuterMoonlets1.Add(NewOuterMoonlet);
                        }
                        foreach (Satellite MajorMoon in Sat.majorMoons)
                        {
                            Utility.MajorMoons NewMajorMoon = new Utility.MajorMoons();
                            NewMajorMoon.RVM = MajorMoon.RVM;
                            NewMajorMoon.SatelliteSize = MajorMoon.SatelliteSize;
                            NewMajorMoon.SatelliteType = MajorMoon.SatelliteType;
                            NewMajorMoon.atmCate = MajorMoon.atmCate.Count;
                            NewMajorMoon.atmMass = MajorMoon.atmMass;
                            NewMajorMoon.axialTilt = MajorMoon.axialTilt;
                            NewMajorMoon.baseType = MajorMoon.baseType;
                            NewMajorMoon.blackbodyTemp = MajorMoon.blackbodyTemp;
                            NewMajorMoon.dayFaceMod = Convert.ToInt32(MajorMoon.dayFaceMod);
                            NewMajorMoon.density = MajorMoon.density;
                            NewMajorMoon.diameter = Math.Round(MajorMoon.diameterInKM(), 2);
                            NewMajorMoon.gravity = Math.Round(MajorMoon.gravity * Satellite.GFORCE, 2);
                            NewMajorMoon.hydCoverage = ((Sat.hydCoverage * 100) + "%");
                            NewMajorMoon.innerMoonlets = MajorMoon.innerMoonlets.Count;
                            NewMajorMoon.isResonant = MajorMoon.isResonant;
                            NewMajorMoon.isTideLocked = MajorMoon.isTideLocked;
                            NewMajorMoon.majorMoons1 = MajorMoon.majorMoons.Count;
                            NewMajorMoon.mass = MajorMoon.mass;
                            NewMajorMoon.moonRadius = MajorMoon.moonRadius;
                            NewMajorMoon.name = MajorMoon.name;
                            NewMajorMoon.nightFaceMod = Convert.ToInt32(MajorMoon.nightFaceMod);
                            NewMajorMoon.orbitalCycle = MajorMoon.orbitalCycle;
                            NewMajorMoon.orbitalEccent = MajorMoon.orbitalEccent;
                            NewMajorMoon.orbitalPeriod = MajorMoon.orbitalPeriod;
                            NewMajorMoon.orbitalRadius = Math.Round(MajorMoon.orbitalRadius, 2);
                            NewMajorMoon.outerMoonlets = MajorMoon.outerMoonlets.Count;
                            NewMajorMoon.parentDiam = MajorMoon.parentDiam;
                            NewMajorMoon.parentID = MajorMoon.parentID;
                            NewMajorMoon.parentName = MajorMoon.parentName;
                            NewMajorMoon.retrogradeMotion = MajorMoon.retrogradeMotion;
                            NewMajorMoon.rotationalPeriod = MajorMoon.rotationalPeriod;
                            NewMajorMoon.selfID = MajorMoon.selfID;
                            NewMajorMoon.siderealPeriod = MajorMoon.siderealPeriod;
                            NewMajorMoon.surfaceTemp = MajorMoon.surfaceTemp;
                            NewMajorMoon.tecActivity = MajorMoon.tecActivity;
                            NewMajorMoon.tideForce = MajorMoon.tideForce.Count;
                            NewMajorMoon.tideTotal = MajorMoon.tideTotal;
                            NewMajorMoon.volActivity = MajorMoon.volActivity;
                            NewMajorMoon.MajorMoonString = MajorMoon.ToString();
                            NewSatellite.MajorMoons1.Add(NewMajorMoon);
                        }
                        NewStar.Planets.Add(NewSatellite);
                    }
                    context.Stars.Add(NewStar);
                }
                context.StarSystems.Add(NewSystem);
                context.SaveChanges();
            }
        }

        public static bool InsertCharacter(CharacterController NewChar, string UserName)
        {
            var regcontext = new Utility.Db1Entities();
            var query = from c in regcontext.Registration where c.Username == UserName select c;
            var UserID = query.FirstOrDefault().Id;

            using (var context = new Utility.Db1Entities())
            {
                Utility.Characters NewCharacter = new Utility.Characters();
                NewCharacter.Name = NewChar.Name;
                NewCharacter.PlayerName = UserName;
                NewCharacter.PlayerId = UserID;
                NewCharacter.Height = NewChar.Height;
                NewCharacter.Weight = NewChar.Weight;
                NewCharacter.SizeModifier = NewChar.SizeModifier;
                NewCharacter.Age = NewChar.Age;
                NewCharacter.UnspendPoints = NewChar.UnspendPoints;
                NewCharacter.Strenght = NewChar.Strenght;
                NewCharacter.Dexterity = NewChar.Dexterity;
                NewCharacter.Intelligence = NewChar.Intelligence;
                NewCharacter.Health = NewChar.Health;
                NewCharacter.HitPoints = NewChar.HitPoints;
                NewCharacter.WillPower = NewChar.WillPower;
                NewCharacter.Perception = NewChar.Perception;
                NewCharacter.BasicLift = NewChar.BasicLift;
                NewCharacter.BasicSpeed = NewChar.BasicSpeed;
                NewCharacter.BasicMove = NewChar.BasicMove;
                NewCharacter.PersonalTechnologyLevel = NewChar.PersonalTechnologyLevel;
                NewCharacter.ParryValue = NewChar.ParryValue;
                context.Characters.Add(NewCharacter);
                context.SaveChanges();
                return true;
                
            }
           
        }

        public static List<Utility.StarSystems> LoadAllSystems()
        {
            List<Utility.StarSystems> AllSystems = new List<Utility.StarSystems>();

            var context = new Utility.Db1Entities();
            var query = from c in context.StarSystems select c;
            AllSystems = query.ToList();

            return AllSystems;
        }

        public static void QuerySystem()
        {
            var context = new Utility.Db1Entities();
            var query = from c in context.StarSystems where c.Id == 5 select c ;
            var sysNames = query.ToList();
        }

        public static void QueryAdvantage()
        {
            var context = new Utility.Db1Entities();
            var query = from c in context.Advantages where c.Id == 7 select c;
            var adv = query.FirstOrDefault();
        }

    }
}
