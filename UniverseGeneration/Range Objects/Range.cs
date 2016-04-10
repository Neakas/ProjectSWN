namespace UniverseGeneration.Range_Objects
{
    public class Range
    {
        public Range( double low, double high )
        {
            /* if (high < low)
                throw new Exception("Invalid argument: higher bound is lower than the lower bound");
            if (high == low)
                throw new Exception("Invalid argument: Both range endpoints are the same number"); */

            LowerBound = low;
            UpperBound = high;
            Length = UpperBound - LowerBound;
        }

        //copy constructor
        public Range( Range incoming )
        {
            /* if (incoming.upperBound < this.lowerBound)
                throw new Exception("Invalid argument: higher bound is lower than the lower bound");
            if (this.upperBound == this.lowerBound) 
                throw new Exception("Invalid argument: Both range endpoints are the same number"); */

            LowerBound = incoming.LowerBound;
            UpperBound = incoming.UpperBound;
            Length = incoming.Length;
        }

        public Range()
        {
        }

        public double LowerBound { get; protected set; }
        public double UpperBound { get; protected set; }
        public double Length { get; protected set; }

        public void SetUpperBound( double bound )
        {
            UpperBound = bound;
            Length = UpperBound - LowerBound;
        }

        public void SetLowerBound( double bound )
        {
            LowerBound = bound;
            Length = UpperBound - LowerBound;
        }

        public virtual bool WithinRange( double number )
        {
            if (LowerBound <= number && number <= UpperBound)
            {
                return true;
            }

            return false;
        }

        public virtual double PosWithinRange( double number )
        {
            if (WithinRange(number))
            {
                return ( number - LowerBound ) / Length;
            }

            return 0.0;
        }

        public override string ToString()
        {
            return "This range is from " + LowerBound + " to " + UpperBound;
        }
    }
}