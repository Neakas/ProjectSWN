using System;

namespace UniverseGeneration.Stellar_Bodies
{
    public class Moonlet : Orbital
    {
        public Moonlet( int parent, int self, double planetRadius, string name ) : base(parent, self)
        {
            PlanetRadius = planetRadius;
            MoonName = name;
        }

        //copy constructor
        public Moonlet( Moonlet inBound ) : base(inBound.ParentId, inBound.SelfId)
        {
            PlanetRadius = inBound.PlanetRadius;
            MoonName = inBound.MoonName;
            OrbitalRadius = inBound.OrbitalRadius;
            OrbitalPeriod = inBound.OrbitalPeriod;
        }

        public double PlanetRadius { get; set; }
        public string MoonName { get; set; }

        public override string ToString()
        {
            var ret = "";
            ret = MoonName + " orbiting at " + Math.Round(OrbitalRadius, 3) + " Earth diameters and ";
            ret = ret + PlanetRadius + " planetary radii";
            return ret;
        }
    }
}