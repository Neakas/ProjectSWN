namespace UniverseGeneration.Utility
{
    class StarStringBuilder
    {
        public static string GenerateString(bool isPrimary,string Starname,string StarType,string LuminocityClass,float mass, float SystemAge, float StarTemperature, float luminosity, float Radius)
        {
            string StarString;
            string PartType = "Primary";
            string grabbedStarname = Starname;
            if (!isPrimary)
            {
                PartType = "Companion";
            }
            StarString = PartType + " Star (" + grabbedStarname + "):\nSpectral type: " + StarType + " " + LuminocityClass + "\nMass: " + mass + " solar masses\nAge: " + SystemAge + " billion years\nEffective temperature: " +
                StarTemperature + " Celcius\nLuminosity: " + luminosity + " solar luminosities\nRadius: " + Radius + " AU.";
            return StarString;
        }
    }
}
