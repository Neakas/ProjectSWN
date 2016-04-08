using System;
using UniverseGeneration.Utility;

namespace UniverseGeneration.Stellar_Bodies
{
    public partial class Star : Orbital
    {
        public virtual void sortOrbitals()
        {
            sysPlanets.Sort((x, y) => x.orbitalRadius.CompareTo(y.orbitalRadius));
        }

        public virtual void addSatellite(Satellite s)
        {
            this.sysPlanets.Add(s);
        }

        public virtual void printAllOrbitals()
        {
            Console.WriteLine("This star's orbital array contains: ");
            foreach (Orbital o in sysPlanets)
                Console.WriteLine("{0}", o);
            Console.WriteLine();

        }

        public virtual void purgeSwallowedOrbits()
        {
            if (!(this.evoLine.findCurrentAgeGroup(this.starAge) >= StarAgeLine.RET_GIANTBRANCH))
                throw new Exception("This star is not in supernova state or beyond.");

            sysPlanets.RemoveAll(orbital => orbital.orbitalRadius <= this.getSwallowedSpace());
            this.sortOrbitals();
        }

    }
}
