using System;

namespace UniverseGeneration.Stellar_Bodies
{
    public class Moonlet : Orbital
    {
        public double planetRadius { get; set; }
        public string moonName { get; set; }

        public Moonlet(int parent, int self, double planetRadius, string name) : base(parent, self)
        {
            this.planetRadius = planetRadius;
            this.moonName = name;
        }

        //copy constructor
        public Moonlet(Moonlet inBound) : base(inBound.parentID, inBound.selfID)
        {
            this.planetRadius = inBound.planetRadius;
            this.moonName = inBound.moonName;
            this.orbitalRadius = inBound.orbitalRadius;
            this.orbitalPeriod = inBound.orbitalPeriod;
        }
        private Moonlet() { }

        public override string ToString()
        {
            string ret = "";
            ret = this.moonName + " orbiting at " + Math.Round(this.orbitalRadius, 3) + " Earth diameters and ";
            ret = ret + this.planetRadius + " planetary radii";
            return ret;
        }
    }
}
