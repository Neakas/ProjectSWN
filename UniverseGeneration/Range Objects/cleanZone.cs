using System;
using UniverseGeneration.Stellar_Bodies;

namespace UniverseGeneration.Range_Objects
{
    public class CleanZone : Range
    {
        public CleanZone( double lower, double upper, int ownership, int orbitDesc ) : base(lower, upper)
        {
            OwnershipFlag = ownership;
            OrbitDesc = orbitDesc;
        }

        public CleanZone( Range incoming, int ownership, int orbitDesc ) : base(incoming)
        {
            OwnershipFlag = ownership;
            OrbitDesc = orbitDesc;
        }

        //copy constructor
        public CleanZone( CleanZone c ) : base(c.LowerBound, c.UpperBound)
        {
            OwnershipFlag = c.OwnershipFlag;
            OrbitDesc = c.OrbitDesc;
        }

        public int OwnershipFlag { get; set; }
        public int OrbitDesc { get; set; }

        public Range GetRange()
        {
            return new Range(LowerBound, UpperBound);
        }

        public override string ToString()
        {
            var ret = "This clean zone is from " + LowerBound + " to " + UpperBound + " AU";
            ret = ret + Environment.NewLine + "    " + " with ownership " + Star.GetDescSelfFlag(OwnershipFlag) + " and ";
            ret = ret + "orbit desc of " + Star.GetDescSelfFlag(OrbitDesc);

            return ret;
        }
    }
}