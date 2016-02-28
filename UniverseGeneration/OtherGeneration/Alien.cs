using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniverseGeneration
{
    public class Alien
    {
        public string ChemicalBasis { get; set; }
        public List<string> TypesofLife = new List<string>
        {
            "Hydrogen-Based Life (Frozen worlds below -250° F or Gas Giants)",
            "Ammonia-Based Life (Frozen worlds between - 100° F and - 30° F)",
            "Hydrocarbon-Based Life (Cold to Cool worlds)",
            "Water-Based Life (Cold to Hot worlds)",
            "Chlorine - Based Life (Cold to Tropical worlds",
            "Silicon/Sulfuric Acid Life (Warm to Infernal worlds between 50° F and 600° F)",
            "Silicon/Liquid Sulfur Life (Infernal worlds between 250° F and 750° F)",
            "Silicon/Liquid Rock Life (Infernal worlds above 2500° F, or mantle)",
            "Plasma Life (Infernal worlds or stars above 4000° F)",
            "Exotica (Nebula - dwelling life, Machine life, Magnetic life)"
        };

        public List<string> TypesofLandHabitats = new List<string>
        {
            "Plains",
            "Desert",
            "Island/Beach",
            "Woodlands",
            "Swampland",
            "Mountain",
            "Arctic",
            "Jungle",
        };

        public List<string> TypesofWaterHabitats = new List<string>
        {
            "Banks",
            "Open Ocean",
            "Fresh-Water Lakes",
            "River/Stream",
            "Tropical Lagoon",
            "Deep-Ocean Vents",
            "Salt-Water Sea",
            "Reef"
        };

        public string LandWater { get; set; }
        public string Habitat { get; set; }

        /// <summary>
        /// Creates a New Alien Race
        /// </summary>
        /// <param name="WaterModPercent">"Base Value is 0, set to -1 if you Have no Water on the Planet, else Input the Percentage of Water on the Planet"</param>
        public Alien(int WaterModPercent = 0)
        {
            ChemicalBasis = DetermineChemicalBasis();
            LandWater = DetermineLandWater(WaterModPercent);
            Habitat = DetermineHabitat(LandWater);
        }
        
        private string DetermineChemicalBasis()
        {
            string cb = "";
            Dice dice = new Dice();
            int roll = dice.gurpsRoll();
            switch (roll)
            {
                case 3:
                case 4:
                case 5:
                    cb = TypesofLife[0];
                    break;
                case 6:
                case 7:
                    cb = TypesofLife[1];
                    break;
                case 8:
                    cb = TypesofLife[2];
                    break;
                case 9:
                case 10:
                case 11:
                    cb = TypesofLife[3];
                    break;
                case 12:
                    cb = TypesofLife[4];
                    break;
                case 13:
                    cb = TypesofLife[5];
                    break;
                case 14:
                    cb = TypesofLife[6];
                    break;
                case 15:
                    cb = TypesofLife[7];
                    break;
                case 16:
                    cb = TypesofLife[8];
                    break;
                case 17:
                case 18:
                    cb = TypesofLife[9];
                    break;
            }
            return cb;
        }

        private string DetermineLandWater(int WaterModPercent)
        {
            string lw = "";
            Dice dice = new Dice();
            int roll = dice.rng(6);

            int Mod = 0;
            if (WaterModPercent == 0)
            {
                Mod = 0;
            }
            if (WaterModPercent <= 50 && WaterModPercent > 0)
            {
                Mod = -1;
            }
            if (WaterModPercent >= 80 && WaterModPercent < 90)
            {
                Mod = 1;
            }
            if (WaterModPercent >= 90)
            {
                Mod = 2;
            }

            roll += Mod;

            switch (roll)
            {
                case 1:
                case 2:
                case 3:
                    lw = "Land";
                    break;
                case 4:
                case 5:
                case 6:
                    lw = "Water";
                    break;
            }
            return lw;
        }

        private string DetermineHabitat(string LandWater)
        {
            string hab = "";
            Dice dice = new Dice();
            int roll = dice.gurpsRoll();

            if (LandWater == "Land")
            {
                switch (roll)
                {
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        hab = TypesofLandHabitats[0];
                        break;
                    case 8:
                        hab = TypesofLandHabitats[1];
                        break;
                    case 9:
                        hab = TypesofLandHabitats[2];
                        break;
                    case 10:
                        hab = TypesofLandHabitats[3];
                        break;
                    case 11:
                        hab = TypesofLandHabitats[4];
                        break;
                    case 12:
                        hab = TypesofLandHabitats[5];
                        break;
                    case 13:
                        hab = TypesofLandHabitats[6];
                        break;
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                        hab = TypesofLandHabitats[7];
                        break;
                }
            }
            if (LandWater == "Water")
            {
                switch (roll)
                {
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        hab = TypesofWaterHabitats[0];
                        break;
                    case 8:
                        hab = TypesofWaterHabitats[1];
                        break;
                    case 9:
                        hab = TypesofWaterHabitats[2];
                        break;
                    case 10:
                        hab = TypesofWaterHabitats[3];
                        break;
                    case 11:
                        hab = TypesofWaterHabitats[4];
                        break;
                    case 12:
                        hab = TypesofWaterHabitats[5];
                        break;
                    case 13:
                        hab = TypesofWaterHabitats[6];
                        break;
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                        hab = TypesofWaterHabitats[7];
                        break;
                }
            }
            return hab;
        }

    }
}
