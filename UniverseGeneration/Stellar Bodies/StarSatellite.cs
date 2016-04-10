using System;
using UniverseGeneration.Utility;

namespace UniverseGeneration.Stellar_Bodies
{
    public partial class Star : Orbital
    {
        public virtual void SortOrbitals()
        {
            SysPlanets.Sort(( x, y ) => x.OrbitalRadius.CompareTo(y.OrbitalRadius));
        }

        public virtual void AddSatellite( Satellite s )
        {
            SysPlanets.Add(s);
        }

        public virtual void PrintAllOrbitals()
        {
            Console.WriteLine("This star's orbital array contains: ");
            foreach (var o in SysPlanets)
            {
                Console.WriteLine("{0}", o);
            }
            Console.WriteLine();
        }

        public virtual void PurgeSwallowedOrbits()
        {
            if (!( EvoLine.FindCurrentAgeGroup(StarAge) >= StarAgeLine.RetGiantbranch ))
            {
                throw new Exception("This star is not in supernova state or beyond.");
            }

            SysPlanets.RemoveAll(orbital => orbital.OrbitalRadius <= GetSwallowedSpace());
            SortOrbitals();
        }
    }
}