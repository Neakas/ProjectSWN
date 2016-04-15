using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using UniverseGeneration.Stellar_Bodies;
using UniverseGeneration.Utility;

namespace SWNAdmin.Utility
{
    public class SqlManager
    {
        private static readonly string ProjectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.FullName;

        public static SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + ProjectDir + @"\Utility\Db1.mdf;Integrated Security=True;Connect Timeout=30");

        public static bool Connect()
        {
            //Neue SQL Verbindung
            //Öffne die Verbindung
            if (Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }

            return true;
        }

        public static bool Disconnect()
        {
            Con.Close();
            return true;
        }

        public static void InsertSystem( StarSystem system )
        {
            using (var context = new Db1Entities())
            {
                var newSystem = new StarSystems
                {
                    sysName = system.SysName,
                    habitableZones = system.HabitableZones.Count,
                    maxMass = system.MaxMass,
                    numDwarfPlanets = system.NumDwarfPlanets,
                    star2index = system.Star2Index,
                    star3index = system.Star3Index,
                    subCompanionStar2index = system.SubCompanionStar2Index,
                    subCompanionStar3index = system.SubCompanionStar3Index,
                    sysAge = system.SysAge,
                    sysStars = system.SysStars.Count
                };
                foreach (var star in system.SysStars)
                {
                    var newStar = new Stars
                    {
                        currLumin = Math.Round(star.CurrLumin, 4),
                        distFromPrimary = star.DistFromPrimary,
                        effTemp = Math.Round(star.EffTemp, 4),
                        gasGiantFlag = star.GasGiantFlag,
                        initLumin = star.InitLumin,
                        initMass = star.InitMass,
                        isFlareStar = star.IsFlareStar,
                        name = star.Name,
                        orbitalEccent = star.OrbitalEccent,
                        orbitalPeriod = Math.Round(star.OrbitalPeriod, 4),
                        orbitalRadius = star.OrbitalRadius,
                        orbitalSep = star.OrbitalSep,
                        orderID = star.OrderId,
                        parentID = star.ParentId,
                        parentName = star.ParentName,
                        radius = Math.Round(star.GetRadiusAu(), 4),
                        selfID = star.SelfId,
                        specType = star.SpecType,
                        starAge = star.StarAge,
                        starColor = star.StarColor,
                        sysPlanets = star.SysPlanets.Count,
                        StarOrder = Star.GetDescFromFlag(star.SelfId)
                    };
                    newStar.StarOrder = star.ReturnCurrentBranchDesc();
                    newStar.OrbitalDetails = star.PrintOrbitalDetails();
                    newStar.StarString = star.ToString();

                    foreach (var sat in star.SysPlanets)
                    {
                        var newSatellite = new Planets();
                        var type = "";
                        var pressure = "";
                        if (OptionCont.ExpandAsteroidBelt != null && ( sat.BaseType != Satellite.BasetypeAsteroidbelt || (bool) OptionCont.ExpandAsteroidBelt ))
                        {
                            type = sat.DescSizeType();
                        }
                        if (sat.BaseType == Satellite.BasetypeAsteroidbelt)
                        {
                            type = "Asteroid Belt";
                        }

                        if (sat.BaseType == Satellite.BasetypeAsteroidbelt)
                        {
                            pressure = "None.";
                        }

                        if (sat.BaseType == Satellite.BasetypeGasgiant)
                        {
                            pressure = "Superdense Atmosphere.";
                        }

                        if (sat.BaseType == Satellite.BasetypeMoon || sat.BaseType == Satellite.BasetypeTerrestial)
                        {
                            pressure = sat.GetDescAtmCategory() + "(" + Math.Round(sat.AtmPres, 2) + ")";
                        }

                        newSatellite.SatelliteSize = sat.SatelliteSize;
                        newSatellite.SatelliteType = sat.SatelliteType;
                        newSatellite.atmCate = sat.AtmCate.Count;
                        newSatellite.atmMass = sat.AtmMass;
                        newSatellite.atmPres = pressure;
                        newSatellite.axialTilt = sat.AxialTilt;
                        newSatellite.baseType = sat.BaseType;
                        newSatellite.blackbodyTemp = sat.BlackbodyTemp;
                        newSatellite.dayFaceMod = Convert.ToInt32(sat.DayFaceMod);
                        newSatellite.density = sat.Density;
                        newSatellite.diameter = Math.Round(sat.DiameterInKm(), 2);
                        newSatellite.gravity = Math.Round(sat.Gravity * Satellite.Gforce, 2);
                        newSatellite.hydCoverage = sat.HydCoverage * 100 + "%";
                        newSatellite.innerMoonlets = sat.InnerMoonlets.Count;
                        newSatellite.isResonant = sat.IsResonant;
                        newSatellite.isTideLocked = sat.IsTideLocked;
                        newSatellite.majorMoons = sat.MajorMoons.Count;
                        newSatellite.mass = sat.Mass;
                        newSatellite.masterOrderID = sat.MasterOrderId;
                        newSatellite.moonRadius = sat.MoonRadius;
                        newSatellite.name = sat.Name;
                        newSatellite.nightFaceMod = Convert.ToInt32(sat.NightFaceMod);
                        newSatellite.orbitalCycle = sat.OrbitalCycle;
                        newSatellite.orbitalEccent = sat.OrbitalEccent;
                        newSatellite.orbitalPeriod = sat.OrbitalPeriod;
                        newSatellite.orbitalRadius = Math.Round(sat.OrbitalRadius, 2);
                        newSatellite.outerMoonlets = sat.OuterMoonlets.Count;
                        newSatellite.parentDiam = sat.ParentDiam;
                        newSatellite.parentID = sat.ParentId;
                        newSatellite.parentName = sat.ParentName;
                        newSatellite.retrogradeMotion = sat.RetrogradeMotion;
                        newSatellite.rotationalPeriod = sat.RotationalPeriod;
                        newSatellite.selfID = sat.SelfId;
                        newSatellite.siderealPeriod = sat.SiderealPeriod;
                        newSatellite.surfaceTemp = sat.SurfaceTemp;
                        newSatellite.tecActivity = sat.TecActivity;
                        newSatellite.tideTotal = sat.TideTotal;
                        newSatellite.volActivity = sat.VolActivity;
                        newSatellite.sattype = type;
                        newSatellite.atmnote = sat.DescAtm();
                        newSatellite.RVM = sat.GetRvmDesc();
                        newSatellite.planetString = sat.ToString();

                        foreach (var innerMoonlet in sat.InnerMoonlets)
                        {
                            var newInnerMoonlet = new InnerMoonlets
                            {
                                blackbodyTemp = innerMoonlet.BlackbodyTemp,
                                name = innerMoonlet.Name,
                                orbitalEccent = innerMoonlet.OrbitalEccent,
                                orbitalPeriod = innerMoonlet.OrbitalPeriod,
                                orbitalRadius = Math.Round(innerMoonlet.OrbitalRadius, 2),
                                parentID = innerMoonlet.ParentId,
                                parentName = innerMoonlet.ParentName,
                                planetRadius = innerMoonlet.PlanetRadius,
                                selfID = innerMoonlet.SelfId,
                                innerMoonString = innerMoonlet.ToString()
                            };
                            newSatellite.InnerMoonlets1.Add(newInnerMoonlet);
                        }
                        foreach (var outerMoonlet in sat.OuterMoonlets)
                        {
                            var newOuterMoonlet = new OuterMoonlets
                            {
                                blackbodyTemp = outerMoonlet.BlackbodyTemp,
                                name = outerMoonlet.Name,
                                orbitalEccent = outerMoonlet.OrbitalEccent,
                                orbitalPeriod = outerMoonlet.OrbitalPeriod,
                                orbitalRadius = Math.Round(outerMoonlet.OrbitalRadius, 2),
                                parentID = outerMoonlet.ParentId,
                                parentName = outerMoonlet.ParentName,
                                planetRadius = outerMoonlet.PlanetRadius,
                                selfID = outerMoonlet.SelfId,
                                outerMoonString = outerMoonlet.ToString()
                            };
                            newSatellite.OuterMoonlets1.Add(newOuterMoonlet);
                        }
                        foreach (var majorMoon in sat.MajorMoons)
                        {
                            var newMajorMoon = new MajorMoons
                            {
                                RVM = majorMoon.Rvm,
                                SatelliteSize = majorMoon.SatelliteSize,
                                SatelliteType = majorMoon.SatelliteType,
                                atmCate = majorMoon.AtmCate.Count,
                                atmMass = majorMoon.AtmMass,
                                axialTilt = majorMoon.AxialTilt,
                                baseType = majorMoon.BaseType,
                                blackbodyTemp = majorMoon.BlackbodyTemp,
                                dayFaceMod = Convert.ToInt32(majorMoon.DayFaceMod),
                                density = majorMoon.Density,
                                diameter = Math.Round(majorMoon.DiameterInKm(), 2),
                                gravity = Math.Round(majorMoon.Gravity * Satellite.Gforce, 2),
                                hydCoverage = sat.HydCoverage * 100 + "%",
                                innerMoonlets = majorMoon.InnerMoonlets.Count,
                                isResonant = majorMoon.IsResonant,
                                isTideLocked = majorMoon.IsTideLocked,
                                majorMoons1 = majorMoon.MajorMoons.Count,
                                mass = majorMoon.Mass,
                                moonRadius = majorMoon.MoonRadius,
                                name = majorMoon.Name,
                                nightFaceMod = Convert.ToInt32(majorMoon.NightFaceMod),
                                orbitalCycle = majorMoon.OrbitalCycle,
                                orbitalEccent = majorMoon.OrbitalEccent,
                                orbitalPeriod = majorMoon.OrbitalPeriod,
                                orbitalRadius = Math.Round(majorMoon.OrbitalRadius, 2),
                                outerMoonlets = majorMoon.OuterMoonlets.Count,
                                parentDiam = majorMoon.ParentDiam,
                                parentID = majorMoon.ParentId,
                                parentName = majorMoon.ParentName,
                                retrogradeMotion = majorMoon.RetrogradeMotion,
                                rotationalPeriod = majorMoon.RotationalPeriod,
                                selfID = majorMoon.SelfId,
                                siderealPeriod = majorMoon.SiderealPeriod,
                                surfaceTemp = majorMoon.SurfaceTemp,
                                tecActivity = majorMoon.TecActivity,
                                tideForce = majorMoon.TideForce.Count,
                                tideTotal = majorMoon.TideTotal,
                                volActivity = majorMoon.VolActivity,
                                MajorMoonString = majorMoon.ToString()
                            };
                            newSatellite.MajorMoons1.Add(newMajorMoon);
                        }
                        newStar.Planets.Add(newSatellite);
                    }
                    context.Stars.Add(newStar);
                }
                context.StarSystems.Add(newSystem);
                context.SaveChanges();
            }
        }

        public static List<StarSystems> LoadAllSystems()
        {
            var context = new Db1Entities();
            var query = from c in context.StarSystems select c;
            var allSystems = query.ToList();

            return allSystems;
        }
    }
}