namespace UniverseGeneration.Utility
{
    /// <summary>
    ///     This class is an container for the generator status. Accessed by both thing setting it (Front-end) and the
    ///     generator using them.
    /// </summary>
    public static class OptionCont
    {
        /// <summary>
        ///     Exposed so that people may set it later via an option menu. Controls displayed figures in various outputs
        /// </summary>
        public static int NumberOfDecimal = 4;

        /// <summary>
        ///     This flag sets the moon orbit status to : By The Book. (see the setting for details.)
        /// </summary>
        public static readonly int MoonBook = 0;

        /// <summary>
        ///     This flag sets the moon orbit status to : By the Book, High Only. (see the setting for details.)
        /// </summary>
        public static readonly int MoonBookhigh = 1;

        /// <summary>
        ///     This flag sets the moon orbit status to: Extended. (see the setting for details.)
        /// </summary>
        public static readonly int MoonExpand = 2;

        /// <summary>
        ///     This flag sets the moon orbit status to: Extended, High Only. (see the setting for details)
        /// </summary>
        public static readonly int MoonExpandhigh = 3;

        /// <summary>
        ///     This setting is intended to make a garden world during generation much more likely. (affecting star size and
        ///     stellar spacing)
        /// </summary>
        /// <remarks>
        ///     This setting is per GURPS Space (4e) pg 101. Also, pg 105.
        /// </remarks>
        public static bool? ForceGardenFavorable = false;

        /// <summary>
        ///     This setting sets the system in an open cluster or not. In an open cluster, stars are more common (The roll
        ///     modifier is +3)
        /// </summary>
        /// <remarks>
        ///     This setting is per GURPS Space (4e) pg 70, 100.
        /// </remarks>
        public static bool InOpenCluster = false;

        /// <summary>
        ///     This setting enables much more verbose output (exposes details normaly used during system generation.)
        ///     Has it's own accessors.
        /// </summary>
        private static bool? _verboseOutput = false;

        /// <summary>
        ///     The manually set number of stars.
        /// </summary>
        /// <remarks>The value of -1 means unset. Please keep this in mind when accessing this option</remarks>
        private static int _numStars = -1;

        /// <summary>
        ///     This setting enables a preset age. Used because the stage a star is in depends on the age of a system. (Also, some
        ///     plans for later.)
        /// </summary>
        /// <remarks>The value of -1 means unset. Please keep this in mind when accessing this option</remarks>
        private static double _presetAge = -1.0;

        /// <summary>
        ///     Override detector for the stellar mass range indicator.
        /// </summary>
        public static bool? StellarMassRangeSet = false;

        /// <summary>
        ///     The minimum stellar mass for the generator
        /// </summary>
        public static double MinStellarMass = .1;

        /// <summary>
        ///     The maximum stellar mass for the generator.
        /// </summary>
        public static double MaxStellarMass = 2;

        /// <summary>
        ///     Caps Stellar Eccentricity at .5
        /// </summary>
        public static bool LessStellarEccent = false;

        /// <summary>
        ///     Caps Stellar Eccentricity at .1
        /// </summary>
        public static bool ForceVeryLowStellarEccent = false;

        /// <summary>
        ///     Overrides the current method of determining color (based on effective surface temperature) for a random color.
        /// </summary>
        public static bool? FantasyColors = false;

        /// <summary>
        ///     Flare star variability (base is 12+) is lengthened to 9+
        /// </summary>
        public static bool? MoreFlareStarChance = false;

        /// <summary>
        ///     By GURPS Space (p128) only M-class stars (under .45 solar masses) can be flare stars. This setting makes ANY star
        ///     be a flare star
        /// </summary>
        /// <remarks>
        ///     It is not recommended you enable this and the other flare star option.
        /// </remarks>
        public static bool AnyStarFlareStar = false;

        /// <summary>
        ///     The prefix used by the basic random name generator.
        /// </summary>
        public static string SysNamePrefix = "XSC";

        /// <summary>
        ///     This tells it to roll rather than use the index array for secondary star mass
        /// </summary>
        public static bool? UseStraightRoll = false;

        /// <summary>
        ///     This gives more conventional gas giant chances
        /// </summary>
        public static bool? MoreConGasGiantChances = false;

        /// <summary>
        ///     This option forces only garden no ocean worlds.
        /// </summary>
        public static bool? NoOceanOnlyGarden = false;

        /// <summary>
        ///     More chances of a large garden world.
        /// </summary>
        public static bool? MoreLargeGarden = false;

        /// <summary>
        ///     More accurate timing of an oxygen catastrophe
        /// </summary>
        public static bool? MoreAccurateO2Catastrophe = false;

        /// <summary>
        ///     Prefer High RVM values
        /// </summary>
        public static bool? HighRvmVal = false;

        /// <summary>
        ///     No Marginal Atmosphere conditions
        /// </summary>
        public static bool? NoMarginalAtm = false;

        /// <summary>
        ///     Forces stable activity (volcanic, activity) to Moderate.
        /// </summary>
        public static bool? StableActivity = false;

        /// <summary>
        ///     This sets the atmospheric pressure via override (For Garden Worlds only)
        /// </summary>
        /// <remarks>The value of -1 means unset. Please keep this in mind when accessing this option</remarks>
        public static double SetAtmPressure = -1.0;

        /// <summary>
        ///     This sets the number of moons via override. (For Garden Worlds only)
        /// </summary>
        /// <remarks>The value of -1 means unset. Please keep this in mind when accessing this option</remarks>
        private static int _numMoonsOverGarden = -1;

        /// <summary>
        ///     This overrides GURPS's default limit of +8 Habitablility
        /// </summary>
        public static bool? OverrideHabitability = false;

        /// <summary>
        ///     This overrides generated axial tilt.
        /// </summary>
        /// <remarks>The value of -1 means unset. Please keep this in mind when accessing this option</remarks>
        private static int _forceAxialTilt = -1;

        /// <summary>
        ///     Reroll the axial tilt over 45 degrees.
        /// </summary>
        public static bool? RerollAxialTiltOver45 = false;

        /// <summary>
        ///     This option ignores lunar tides on garden worlds for purposes of determining tidal force.
        /// </summary>
        public static bool? IgnoreLunarTidesOnGardenWorlds = false;

        /// <summary>
        ///     This option forces to display all tidal data no matter what on the output.
        /// </summary>
        public static bool? AlwaysDisplayTidalData = false;

        /// <summary>
        ///     This option expands the asteroid belt size/RVM.
        /// </summary>
        public static bool? ExpandAsteroidBelt = false;

        /// <summary>
        ///     This option sets the moon orbit flag during generation
        /// </summary>
        public static int MoonOrbitFlag = MoonBook;

        /// <summary>
        ///     This option forces at least one terrestial planet to be created during orbital generation.
        ///     (Will only be invoked if none are generated)
        /// </summary>
        public static bool? EnsureOneOrbit = false;

        //unverified options.
        public static bool? ReplaceLowRedWithBrown = false; //OPTION FORM OK, WITHHELD - SEE CHANGELOG
        public static bool? AltMassRollMethod = false; //SEE CHANGELOG.

        //accessors

        /// <summary>
        ///     This function is an accessor for the property <see cref="_forceAxialTilt" />
        /// </summary>
        /// <param name="tilt">The tilt to be set</param>
        /// <returns>True if valid, false if invalid</returns>
        public static bool SetAxialTilt( int tilt )
        {
            if (tilt < 1 || tilt > 90)
            {
                return false;
            }
            _forceAxialTilt = tilt;
            return true;
        }

        /// <summary>
        ///     This function is an accessor for the property <see cref="_forceAxialTilt" />
        /// </summary>
        /// <returns>The current tilt</returns>
        public static int GetAxialTilt()
        {
            return _forceAxialTilt;
        }

        /// <summary>
        ///     This function is an accessor for the property <see cref="_verboseOutput" />
        /// </summary>
        /// <returns>The property</returns>
        public static bool GetVerboseOutput()
        {
            return _verboseOutput != null && (bool) _verboseOutput;
        }

        /// <summary>
        ///     This function is an accessor for the property <see cref="_verboseOutput" />.
        ///     Used to ensure that if you're enabling verbose output you display tidal data as well.
        /// </summary>
        /// <param name="setting">The value you are setting verbose output to</param>
        public static void SetVerboseOutput( bool setting )
        {
            _verboseOutput = setting;
            if (setting)
            {
                AlwaysDisplayTidalData = true;
            }
        }

        /// <summary>
        ///     This function is an accessor for the property <see cref="_numStars" />
        ///     Used to do error checking (i.e no setting for 4 or more stars.
        /// </summary>
        /// <param name="num">This is an integer representing the number of stars</param>
        /// <returns>If the property was succesfully set (will return false if it's over 3 or under 1)</returns>
        public static bool SetNumberOfStars( int num )
        {
            if (( num == -1 ) || ( num >= 1 && num <= 3 ))
            {
                _numStars = num;
                return true;
            }

            return false;
        }

        /// <summary>
        ///     This returns the property <see cref="_numStars" />
        /// </summary>
        /// <returns>The property</returns>
        public static int GetNumberOfStars()
        {
            return _numStars;
        }

        /// <summary>
        ///     This sets the property <see cref="_numMoonsOverGarden" />
        /// </summary>
        /// <param name="num">This is an double representing the number of moons</param>
        /// <returns>If the property was succesfully set (will return false if it's over 3 or under 1)</returns>
        public static bool SetNumberOfMoonsOverGarden( int num )
        {
            if (( num == -1 ) || ( num >= 1 && num <= 3 ))
            {
                _numMoonsOverGarden = num;
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Returns the number of moons.
        /// </summary>
        /// <returns>The number of moons</returns>
        public static int GetNumberOfMoonsOverGarden()
        {
            return _numMoonsOverGarden;
        }

        /// <summary>
        ///     This function is an accessor for the property <see cref="_presetAge" />
        ///     There may be bound checking later.
        /// </summary>
        /// <param name="age">A double representing the age to be set</param>
        public static void SetSystemAge( double age )
        {
            _presetAge = age;
        }

        /// <summary>
        ///     This returns the property <see cref="_presetAge" />
        /// </summary>
        /// <returns>The property</returns>
        public static double GetSystemAge()
        {
            return _presetAge;
        }
    }
}