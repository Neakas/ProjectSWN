using System.Collections.Generic;
using System.Linq;
using UniverseGeneration.Range_Objects;

namespace UniverseGeneration.Utility
{
    internal class FormationZone
    {
        //flags
        public static int FzValidorbit = 610;
        public static int FzTooclose = 611;
        public static int FzForbidden = 612;
        public static int FzOutbounds = 613;
        public static int FzBadparent = 9999;

        //constants
        public static double MinDistance = .15;
        public static double MinOrbitalRatio = 1.4;
        public static double MaxOrbitalRatio = 2.1;

        public FormationZone( double lower, double upper, int parentId )
        {
            Segments = new List<FormationSegment>
            {
                new FormationSegment(parentId, lower, upper)
            };
            ParentId = parentId;
            OurOrbits = new List<double>();
        }

        public FormationZone( Range incoming, int parentId )
        {
            Segments = new List<FormationSegment>
            {
                new FormationSegment(parentId, incoming)
            };
            ParentId = parentId;
            OurOrbits = new List<double>();
        }

        public List<FormationSegment> Segments { get; set; }
        public int ParentId { get; set; }
        public List<double> OurOrbits { get; set; }

        public int CheckOrbit( double orbit )
        {
            foreach (var l in Segments.Where(l => l.WithinRange(orbit)))
            {
                if (l.ParentId == FzBadparent)
                {
                    return FzForbidden;
                }
                foreach (var d in OurOrbits)
                {
                    if (d - .15 < orbit && orbit > d + .15)
                    {
                        return FzTooclose;
                    }
                    if (d / 1.4 < orbit && orbit > d * 1.4)
                    {
                        return FzTooclose;
                    }
                }

                return FzValidorbit;
            }

            return FzOutbounds;
        }

        public double NextCleanOrbit( double orbit )
        {
            var nextSegment = false;
            foreach (var l in Segments)
            {
                if (l.WithinRange(orbit))
                {
                    nextSegment = true;
                }

                if (!nextSegment)
                {
                    continue;
                }
                //skip if it's a bad parent.
                //else return the start of this segment.
                if (l.ParentId == FzBadparent)
                {
                }
                else
                {
                    return l.LowerBound;
                }
            }

            return FzOutbounds;
        }
    }
}