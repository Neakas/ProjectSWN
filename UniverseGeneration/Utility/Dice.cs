using System;

namespace UniverseGeneration.Utility
{
    public class Dice
    {
        protected MersenneTwister Ddice = new MersenneTwister((int) DateTime.Now.Ticks / 10);

        public int Probablity( int probSize = 100 )
        {
            return (int) ( probSize * Ddice.NextDoublePositive() + 1 );
        }

        public int GurpsRoll()
        {
            return Rng(3, 6);
        }

        public int GurpsRoll( int mod )
        {
            return Rng(3, 6, mod);
        }

        public int Rng( int size )
        {
            return (int) ( size * Ddice.NextDoublePositive() + 1 );
        }

        public int Rng( int num, int size )
        {
            var total = 0;
            for (var i = 0; i < num; i++)
            {
                total = total + Rng(size);
            }

            return total;
        }

        public int Rng( int num, int size, int mod )
        {
            var total = Rng(num, size) + mod;
            return total;
        }

        public double RollRange( double startVal, double range )
        {
            return Ddice.NextDoublePositive() * range + startVal;
        }

        public double RollInRange( double startVal, double endVal )
        {
            var range = endVal - startVal;
            return Ddice.NextDoublePositive() * range + startVal;
        }
    }
}