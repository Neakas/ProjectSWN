using System;
using System.Collections.Generic;
using System.Linq;
using UniverseGeneration.Range_Objects;
using UniverseGeneration.Stellar_Bodies;

namespace UniverseGeneration.Utility
{
    public class FormationHelper
    {
        //flags
        public static readonly int Novalidorbit = -1;
        public static readonly int Dirleft = 1;
        public static readonly int Dirright = 2;

        public FormationHelper( int starId )
        {
            FormationZones = new List<CleanZone>();
            ForbiddenZones = new List<ForbiddenZone>();
            StarId = starId;
        }

        public List<CleanZone> FormationZones { get; protected set; }
        public List<ForbiddenZone> ForbiddenZones { get; protected set; }
        public int StarId { get; set; }
        protected Range CreationRange { get; set; }

        //member functions
        public void UpdateCreationRange( Range creationRange )
        {
            CreationRange = creationRange;
        }

        //create functions
        public void CreateForbiddenZone( double lower, double upper, int primary, int secondary )
        {
            ForbiddenZones.Add(new ForbiddenZone(lower, upper, primary, secondary));
        }

        public void CreateForbiddenZone( Range forbiddenRange, int primary, int secondary )
        {
            ForbiddenZones.Add(new ForbiddenZone(forbiddenRange, primary, secondary));
        }

        public void CreateForbiddenZone( ForbiddenZone incoming )
        {
            ForbiddenZones.Add(new ForbiddenZone(incoming));
        }

        public void CreateCleanZones( Range fullCreationRange )
        {
            UpdateCreationRange(fullCreationRange);
            var currentPos = CreationRange.LowerBound;

            //default values
            var ownershipFlag = StarId;
            var orbitDesc = StarId;

            if (ForbiddenZones.Count == 0)
            {
                FormationZones.Add(new CleanZone(CreationRange, StarId, StarId));
                return;
            }

            foreach (var o in ForbiddenZones)
            {
                if (currentPos < o.LowerBound && CreationRange.UpperBound <= o.LowerBound)
                {
                    //CASE 1: Both the current position and outer radius are before the forbidden zone
                    // This clean zone is from currentPos to the outer radius. Is the end of our generation
                    FormationZones.Add(new CleanZone(currentPos, CreationRange.UpperBound, StarId, StarId));
                    return;
                }

                if (currentPos < o.LowerBound && CreationRange.UpperBound > o.UpperBound)
                {
                    //CASE 2: The current position is below the forbidden zone, and the outer radius is beyond it
                    // This clean zone is from current position to the lower bound of the forbidden zone
                    // We then move the pointer to the end of the higher bound.
                    FormationZones.Add(new CleanZone(currentPos, o.LowerBound, ownershipFlag, orbitDesc));
                    if (o.PrimaryStar != StarId)
                    {
                        return; //return now if you lose primary status.
                    }
                    ownershipFlag = o.PrimaryStar;
                    orbitDesc = GetNewOrbitDesc(orbitDesc, o.PrimaryStar, o.SecondaryStar);
                    //OwnershipFlag = 99;
                    currentPos = o.UpperBound;
                }

                if (currentPos < o.LowerBound && o.LowerBound < CreationRange.UpperBound && CreationRange.UpperBound <= o.UpperBound)
                {
                    //CASE 3: The current position is below the forbidden zone, and the outer radius is within it.
                    // The clean zone is from the current position to the lower bound of the forbidden zone
                    // We then return, no more clear zones.
                    FormationZones.Add(new CleanZone(currentPos, o.LowerBound, ownershipFlag, orbitDesc));
                    return;
                }

                if (!( currentPos >= o.LowerBound ) || !( o.UpperBound < CreationRange.UpperBound ))
                {
                    continue;
                }
                //CASE 4: The current position is within a forbidden zone, and the outer radius is beyond it.
                //Move forward the pointers, but don't add a clean zone
                currentPos = o.UpperBound;
                ownershipFlag = o.PrimaryStar;
                orbitDesc = GetNewOrbitDesc(orbitDesc, o.PrimaryStar, o.SecondaryStar);
                //OwnershipFlag = 99;
            }

            if (!( currentPos < CreationRange.UpperBound ))
            {
                return;
            }
            // CASE 5: current position is under the upperBound. Add it, and return.
            FormationZones.Add(new CleanZone(currentPos, CreationRange.UpperBound, ownershipFlag, orbitDesc));
        }

        public int GetNewOrbitDesc( int prevOrbit, int primaryFz, int secondaryFz )
        {
            if (prevOrbit == Star.IsPrimary)
            {
                if (primaryFz == Star.IsPrimary && secondaryFz == Star.IsSecondary)
                {
                    return Satellite.OrbitPrisec;
                }
            }

            if (prevOrbit == Star.IsSecondary)
            {
                if (primaryFz == Star.IsSecondary && secondaryFz == Star.IsSeccomp)
                {
                    return Satellite.OrbitSeccom;
                }

                if (primaryFz == Star.IsSeccomp && secondaryFz == Star.IsSecondary)
                {
                    return Satellite.OrbitSeccom;
                }

                if (primaryFz == Star.IsSecondary && secondaryFz == Star.IsPrimary)
                {
                    return Satellite.OrbitPrisec;
                }
            }

            Console.WriteLine("PrevOrbit: {0} , Primary Star: {1}, Secondary Star: {2}", prevOrbit, primaryFz, secondaryFz);

            if (prevOrbit == Star.IsTrinary)
            {
                if (primaryFz == Star.IsTrinary && secondaryFz == Star.IsTricomp)
                {
                    return Satellite.OrbitTricom;
                }
            }

            if (prevOrbit == Satellite.OrbitPrisec || prevOrbit == Satellite.OrbitSeccom || prevOrbit == Satellite.OrbitTricom)
            {
                return prevOrbit;
            }

            return Satellite.ErrorOrbit;
        }

        public void CreateCleanZones( double inner, double outer )
        {
            CreateCleanZones(new Range(inner, outer));
        }

        //get minimal and maximal functions.
        public double GetMinimalCleanZone()
        {
            return FormationZones.Count == 0 ? Novalidorbit : FormationZones[0].LowerBound;
        }

        public double GetMaximalCleanZone()
        {
            return FormationZones.Count == 0 ? Novalidorbit : FormationZones[FormationZones.Count - 1].UpperBound;
        }

        //gets the range for this
        public Range GetRange( double orbit )
        {
            foreach (var o in FormationZones.Where(o => o.WithinRange(orbit)))
            {
                return new Range(o.LowerBound, o.UpperBound);
            }

            foreach (var o in ForbiddenZones.Where(o => o.WithinRange(orbit)))
            {
                return new Range(o.LowerBound, o.UpperBound);
            }

            return new Range(0, 0);
        }

        public int GetOwnership( double orbital )
        {
            foreach (var o in FormationZones.Where(o => o.WithinRange(orbital)))
            {
                return o.OrbitDesc;
            }

            return -9999; //INVALID DATA.
        }

        //check within range functions. 
        public bool IsWithinForbiddenZone( double orbit )
        {
            return ForbiddenZones.Any(o => o.WithinRange(orbit));
        }

        public bool IsWithinCleanZone( double orbit )
        {
            if (FormationZones.Any(o => o.WithinRange(orbit)))
            {
                return true;
            }

            //Epistellar check.
            return orbit < CreationRange.LowerBound && !IsWithinForbiddenZone(orbit);
        }

        //check range width
        public double GetRangeWidth( double orbit )
        {
            foreach (var o in FormationZones.Where(o => o.WithinRange(orbit)))
            {
                return o.Length;
            }

            return ( from o in ForbiddenZones where o.WithinRange(orbit) select o.Length ).FirstOrDefault();
        }

        //check -range- function.
        public double VerifyRange( Range checkRange )
        {
            double rangeAvail = 1;
            var currentPos = checkRange.LowerBound;

            //escape hatch for this condition

            if (ForbiddenZones.Count == 0)
            {
                return 1;
            }

            //Which is.. wat? Still, fixed.  
            if (FormationZones.Any(o => checkRange.LowerBound >= o.LowerBound && checkRange.UpperBound <= o.UpperBound))
            {
                return 1;
            }

            foreach (var o in ForbiddenZones)
            {
                //CASE 1: The forbidden zone is between here and the end
                if (( currentPos < checkRange.UpperBound ) && ( currentPos < o.LowerBound ) && ( o.UpperBound <= checkRange.UpperBound ))
                {
                    rangeAvail = rangeAvail - ( o.UpperBound - o.LowerBound ) / checkRange.Length;
                    if (Math.Abs(rangeAvail) < 0)
                    {
                        return rangeAvail;
                    }

                    currentPos = o.UpperBound;
                }

                //CASE 2: This is within a forbidden zone
                if (( checkRange.LowerBound >= o.LowerBound ) && ( checkRange.UpperBound <= o.UpperBound ))
                {
                    return 0;
                }

                //CASE 3: Current Position is within a forbidden zone and it cotnains the end.
                if (( currentPos < checkRange.UpperBound ) && ( currentPos > o.LowerBound ) && ( checkRange.UpperBound <= o.UpperBound ))
                {
                    rangeAvail = rangeAvail - ( o.UpperBound - currentPos ) / checkRange.Length;
                    if (Math.Abs(rangeAvail) < 0)
                    {
                        return rangeAvail;
                    }

                    currentPos = checkRange.UpperBound;
                }

                //CASE 4: Current Position is within a forbidden zone and it does not contain the end.
                if (( currentPos < checkRange.UpperBound ) && ( currentPos > o.LowerBound ) && ( checkRange.UpperBound > o.UpperBound ))
                {
                    rangeAvail = rangeAvail - ( o.UpperBound - currentPos ) / checkRange.Length;
                    if (rangeAvail == 0)
                    {
                        return rangeAvail;
                    }

                    currentPos = o.UpperBound;
                }

                //CASE 5: The end is within a forbidden zone but the current position is not
                if (!( currentPos < checkRange.UpperBound ) || !( currentPos < o.LowerBound ) || !( checkRange.UpperBound >= o.LowerBound ) || !( checkRange.UpperBound < o.UpperBound ))
                {
                    continue;
                }
                rangeAvail = rangeAvail - ( checkRange.UpperBound - o.LowerBound ) / checkRange.Length;
                if (rangeAvail == 0)
                {
                    return rangeAvail;
                }

                currentPos = checkRange.UpperBound;

                //CASE 6: No IF condition if the forbidden zone is entirely to the left or right.
            }

            if (rangeAvail >= 0 && rangeAvail <= 1)
            {
                return rangeAvail;
            }
            throw new Exception("RangeAvail is " + rangeAvail + " and exceeds 0-100%.");
        }

        public double VerifyRange( double lower, double upper )
        {
            return VerifyRange(new Range(lower, upper));
        }

        public double GetClosestDistFromForbiddenZone( double orbit )
        {
            double dist = 9990;

            if (ForbiddenZones.Count == 0)
            {
                return -1;
            }

            foreach (var o in ForbiddenZones)
            {
                var var = Math.Abs(orbit - o.LowerBound);
                if (var < dist)
                {
                    dist = var;
                }

                var = Math.Abs(orbit - o.UpperBound);
                if (var < dist)
                {
                    dist = var;
                }
            }

            return dist;
        }

        public double GetClosestForbiddenZoneRatio( double orbit )
        {
            double ratio = 9999;

            if (ForbiddenZones.Count == 0)
            {
                return -1;
            }

            foreach (var o in ForbiddenZones)
            {
                //lower Bound check
                double var = 0;
                if (o.LowerBound > orbit)
                {
                    var = o.LowerBound / orbit;
                }
                else
                {
                    var = orbit / o.LowerBound;
                }

                if (var > ratio)
                {
                    ratio = var;
                }

                //upper Bound check
                if (o.UpperBound > orbit)
                {
                    var = o.UpperBound / orbit;
                }
                else
                {
                    var = orbit / o.UpperBound;
                }

                if (var > ratio)
                {
                    ratio = var;
                }
            }

            return ratio;
        }

        //range pick equations
        public double PickInRange( Range validRange )
        {
            var rangeIncrement = Math.Pow(10, 6);
            var currentPos = validRange.LowerBound;
            do
            {
                if (!IsWithinForbiddenZone(currentPos))
                {
                    return currentPos;
                }
                currentPos += validRange.Length / rangeIncrement;
            }
            while (currentPos <= validRange.UpperBound);

            return Novalidorbit;
        }

        public double PickInRange( double lower, double higher )
        {
            return PickInRange(new Range(lower, higher));
        }

        //orbital help functionality
        //only call if you are about to enter a FZ. Or this will return nonsense.
        public double GetNextCleanOrbit( double orbit, int flag )
        {
            foreach (var o in FormationZones)
            {
                if (o.WithinRange(orbit))
                {
                    return orbit;
                }

                if (o.LowerBound > orbit && flag == Dirright)
                {
                    return o.LowerBound;
                }
                if (o.UpperBound < orbit && flag == Dirleft)
                {
                    return o.UpperBound;
                }
            }

            return Novalidorbit;
        }

        //get adjacency functions
        public int GetAdjacencyMod( double orbital )
        {
            var mod = 0;
            var rorbitChecked = false;
            var oRorbitChecked = false;
            foreach (var o in ForbiddenZones)
            {
                //forbidden zone left.
                if (( orbital / 1.4 <= o.UpperBound && orbital / 2.0 >= o.UpperBound ) || o.UpperBound <= orbital - .15)
                {
                    mod = mod - 6;
                }

                //inner radius
                if (( orbital / 1.4 <= CreationRange.LowerBound && orbital / 2.0 >= CreationRange.LowerBound ) || ( orbital - .15 <= CreationRange.LowerBound ) && !rorbitChecked)
                {
                    mod = mod - 3;
                    rorbitChecked = true;
                }

                //outer radius
                if (orbital * 1.4 >= CreationRange.UpperBound && orbital * 2.0 <= CreationRange.UpperBound && !oRorbitChecked)
                {
                    mod = mod - 3;
                    oRorbitChecked = true;
                }

                //forbidden zone right
                if (orbital * 1.4 >= o.LowerBound && orbital * 2.0 <= o.LowerBound)
                {
                    mod = mod - 6;
                }
            }

            return mod;
        }

        //sorting functions
        public void SortForbiddenZones()
        {
            ForbiddenZones.Sort(( x, y ) => x.LowerBound.CompareTo(y.LowerBound));
        }

        public void SortCleanZones()
        {
            FormationZones.Sort(( x, y ) => x.LowerBound.CompareTo(y.LowerBound));
        }

        //determination functions
        public bool IsAnyValidFormationZone()
        {
            if (FormationZones.Count > 0)
            {
                return true;
            }
            return false;
        }

        //helpful functions
        protected int UpdateOwnership( int current, int primary, int secondary )
        {
            return Satellite.ErrorOrbit;
        }

        public override string ToString()
        {
            var nL = Environment.NewLine + "    ";
            var desc = ForbiddenZones.Aggregate("These system zones contain: ", ( current, o ) => current + ( nL + "" + o ));
            return FormationZones.Aggregate(desc, ( current, o ) => current + ( nL + "" + o ));
        }
    }
}