namespace UniverseGeneration.Utility
{
    internal class StarStringBuilder
    {
        public static string GenerateString( bool isPrimary, string starname, string starType, string luminocityClass, float mass, float systemAge, float starTemperature, float luminosity, float radius )
        {
            var partType = "Primary";
            var grabbedStarname = starname;
            if (!isPrimary)
            {
                partType = "Companion";
            }
            var starString = partType + " Star (" + grabbedStarname + "):\nSpectral type: " + starType + " " + luminocityClass + "\nMass: " + mass + " solar masses\nAge: " + systemAge + " billion years\nEffective temperature: " +
                             starTemperature + " Celcius\nLuminosity: " + luminosity + " solar luminosities\nRadius: " + radius + " AU.";
            return starString;
        }
    }
}