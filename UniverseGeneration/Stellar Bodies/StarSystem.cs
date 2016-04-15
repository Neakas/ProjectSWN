using System;
using System.Collections.Generic;
using System.Linq;
using UniverseGeneration.Range_Objects;
using UniverseGeneration.Utility;

namespace UniverseGeneration.Stellar_Bodies
{
    public class StarSystem
    {
        public StarSystem()
        {
            InitateLists();
        }

        //fields
        public string SysName { get; set; }
        public double SysAge { get; set; }
        public List<Star> SysStars { get; set; }
        public List<Range> HabitableZones { get; set; }
        //derived field
        public double MaxMass { get; set; }

        //outer system details
        public int NumDwarfPlanets { get; set; }
        public double HelioPause { get; set; }

        public int SubCompanionStar2Index { get; set; }
        public int SubCompanionStar3Index { get; set; }
        public int Star2Index { get; set; }
        public int Star3Index { get; set; }

        public void InitateLists()
        {
            SysStars = new List<Star>();
            HabitableZones = new List<Range>();
        }

        public void ResetSystem()
        {
            SysAge = 0.0;
            MaxMass = 0.0;
            NumDwarfPlanets = 0;
            HelioPause = 0.0;
            SubCompanionStar2Index = 0;
            SubCompanionStar3Index = 0;
            Star2Index = 0;
            Star3Index = 0;
            SysStars.Clear();
            HabitableZones.Clear();
        }

        public void AddStar( Star newStar )
        {
            SysStars.Add(newStar);
        }

        public void AddStar( int selfId, int parent, int order )
        {
            if (Math.Abs(SysAge) < 0)
            {
                throw new Exception("This star system needs an age.");
            }

            //Set flags here.
            var curPos = SysStars.Count;

            if (selfId == Star.IsSecondary)
            {
                Star2Index = curPos;
            }

            if (selfId == Star.IsSeccomp)
            {
                SubCompanionStar2Index = curPos;
            }

            if (selfId == Star.IsTrinary)
            {
                Star3Index = curPos;
            }

            if (selfId == Star.IsTricomp)
            {
                SubCompanionStar3Index = curPos;
            }

            //add it now

            SysStars.Add(new Star(SysAge, parent, selfId, order, SysName));
        }

        public int GetPositionById( int selfId )
        {
            for (var i = 0; i < SysStars.Count; i++)
            {
                if (SysStars[i].SelfId == selfId)
                {
                    return i;
                }
            }

            return 0;
        }

        public int GetValidParent( int parentFlag )
        {
            var planetOwner = 0;

            if (( parentFlag == Satellite.OrbitPrisec ) || ( parentFlag == Satellite.OrbitPrisectri ) || ( parentFlag == Satellite.OrbitPritri ) || ( parentFlag == Star.IsPrimary ))
            {
                planetOwner = 0;
            }
            if (( parentFlag == Satellite.OrbitSeccom ) || ( parentFlag == Satellite.OrbitSectri ) || ( parentFlag == Star.IsSecondary ))
            {
                planetOwner = Star2Index;
            }
            if (( parentFlag == Satellite.OrbitTricom ) || ( parentFlag == Star.IsTrinary ))
            {
                planetOwner = Star3Index;
            }
            if (parentFlag == Star.IsSeccomp)
            {
                planetOwner = SubCompanionStar2Index;
            }
            if (parentFlag == Star.IsTricomp)
            {
                planetOwner = SubCompanionStar3Index;
            }

            return planetOwner;
        }

        public int GetStellarParentId( int id )
        {
            if (id == Star.IsPrimary)
            {
                return 0;
            }
            if (id == Star.IsSecondary)
            {
                return Star2Index;
            }
            if (id == Star.IsTrinary)
            {
                return Star3Index;
            }
            if (id == Star.IsSeccomp)
            {
                return SubCompanionStar2Index;
            }
            return id == Star.IsTricomp ? SubCompanionStar3Index : 0;
        }

        public static int GenNumOfStars( Dice ourDice )
        {
            //if there's an override in, autoreturn it.
            //if (OptionCont.numStarOverride) return OptionCont.numStars;

            var roll = ourDice.GurpsRoll();

            if (OptionCont.InOpenCluster)
            {
                roll = roll + 3;
            }

            roll = (int) Math.Floor(( roll - 1 ) / 5.0);

            //logic bugs.
            if (roll < 1)
            {
                roll = 1;
            }
            if (roll > 3)
            {
                roll = 3;
            }

            return roll;
        }

        /// <summary>
        ///     Gets the population from the age
        /// </summary>
        /// <param name="age">The age</param>
        /// <returns>The age description</returns>
        public static string GetPopulationFromAge( double age )
        {
            if (age >= .01 && age < .1)
            {
                return "Extreme Population I";
            }
            if (age >= .1 && age < 2)
            {
                return "Young Population I";
            }
            if (age >= 2 && age < 5.6)
            {
                return "Intermediate Population I";
            }
            if (age >= 5.6 && age < 8.2)
            {
                return "Old Population I";
            }
            if (age >= 8.2 && age < 10.4)
            {
                return "Intermediate Population II";
            }
            return age >= 10.4 ? "Extreme Population II" : "???";
        }

        /// <summary>
        ///     This counts the number of stars in this solar system.
        /// </summary>
        /// <returns>Returns an integer of the number of stars</returns>
        public int CountStars()
        {
            return SysStars.Count;
        }

        /// <summary>
        ///     This counts the total number of planets in this solar system
        /// </summary>
        /// <returns>Returns an integer of the number of planets</returns>
        public int CountPlanets()
        {
            return SysStars.Sum(t => t.SysPlanets.Count);
        }

        /// <summary>
        ///     Clears all planets without removing the stars.
        /// </summary>
        public void ClearPlanets()
        {
            foreach (var s in SysStars)
            {
                s.SysPlanets.Clear();
            }
        }
    }
}