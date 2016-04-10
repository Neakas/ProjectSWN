namespace UniverseGeneration.Stellar_Bodies
{
    public partial class Star
    {
        //flags        
        //description types
        public static readonly int IsPrimary = 9000; //flag for orbitals
        public static readonly int IsSecondary = 9001;
        public static readonly int IsTrinary = 9002;
        public static readonly int IsSeccomp = 9005;
        public static readonly int IsTricomp = 9006;

        //gas giant flags.
        public static readonly int GasgiantNone = 700;
        public static readonly int GasgiantConventional = 701;
        public static readonly int GasgiantEccentric = 702;
        public static readonly int GasgiantEpistellar = 703;

        //orbital seperation flags
        public static readonly int OrbsepNone = 501;
        public static readonly int OrbsepVeryclose = 502;
        public static readonly int OrbsepClose = 503;
        public static readonly int OrbsepModerate = 504;
        public static readonly int OrbsepWide = 505;
        public static readonly int OrbsepDistant = 506;
        public static readonly int OrbsepContact = 507;
    }
}