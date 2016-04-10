using System;
using UniverseGeneration.Stellar_Bodies;

namespace UniverseGeneration.Range_Objects
{
    public class ForbiddenZone : Range
    {
        public ForbiddenZone( double lower, double upper, int primary, int secondary ) : base(lower, upper)
        {
            PrimaryStar = primary;
            SecondaryStar = secondary;
        }

        public ForbiddenZone( Range incoming, int primary, int secondary ) : base(incoming)
        {
            PrimaryStar = primary;
            SecondaryStar = secondary;
        }

        //copy constructor
        public ForbiddenZone( ForbiddenZone r ) : base(r.LowerBound, r.UpperBound)
        {
            PrimaryStar = r.PrimaryStar;
            SecondaryStar = r.SecondaryStar;
        }

        public int PrimaryStar { get; set; }
        public int SecondaryStar { get; set; }

        public Range GetRange()
        {
            return new Range(LowerBound, UpperBound);
        }

        public override bool WithinRange( double number )
        {
            return LowerBound < number && number < UpperBound;
        }

        public override string ToString()
        {
            var ret = "This forbidden zone is from " + LowerBound + " to " + UpperBound + " AU";
            ret = ret + Environment.NewLine + "      " + "From star " + Star.GetDescSelfFlag(PrimaryStar) + " to " + Star.GetDescSelfFlag(SecondaryStar);

            return ret;
        }
    }
}