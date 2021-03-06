﻿using System.Collections.Generic;
using UniverseGeneration.Utility;

namespace UniverseGeneration.OtherGeneration
{
    public class Alien
    {
        #region EnumsAndDicts

        public enum ChemicalBasis
        {
            None,
            Hydrogen,
            Ammonia,
            Hydrocarbon,
            Water,
            Chlorine,
            SulfuricAcid,
            LiquidSulfur,
            LiquidRock,
            Plasma,
            Exotic
        }

        public enum LandWater
        {
            None,
            Land,
            Water
        }

        public enum LandHabitats
        {
            None,
            Plains,
            Desert,
            IslandBeach,
            Woodlands,
            Swampland,
            Mountain,
            Arctic,
            Jungle
        }

        public enum WaterHabitats
        {
            None,
            Banks,
            OpenOcean,
            FreshWaterLakes,
            RiverStream,
            TropicalLagoon,
            DeepOceanVents,
            SaltWaterSea,
            Reef
        }

        public enum TrophicDiets
        {
            None,
            CombinedMethod,
            ParasiteSymbiont,
            FilterFeeder,
            PouncingCarnivore,
            ChasingCarnivore,
            HijackingCarnivore,
            TrappingCarnivore,
            Scavenger,
            GatheringHerbivore,
            GrazingHerbivore,
            Omnivore,
            Decomposer,
            AutothrophPhotosynthetic,
            AutothrophChemosynthetic,
            AutothrophOther
        }

        public enum Locomotions
        {
            None,
            Immobile,
            Slithering,
            Swimming,
            Digging,
            Walking,
            Wingedflight,
            Floating,
            Sailing,
            Bouyantflight,
            Climbing,
            Special
        }

        public enum SizeClasses
        {
            None,
            Small,
            HumanScale,
            Large
        }

        public enum Symmetries
        {
            None,
            Bilateral,
            Trilateral,
            Radial,
            Spherical,
            Asymmetric
        }

        public enum Tails
        {
            None,
            FeatureLess,
            Striker,
            Long,
            Constricting,
            Barbed,
            Gripping,
            Branching,
            Combination
        }

        public enum Skeletons
        {
            None,
            Hydrostatic,
            External,
            Internal,
            Combination
        }

        public enum SkinClasses
        {
            None,
            Skin,
            Scales,
            Fur,
            Feathers,
            Exeskeleton
        }

        public enum Skins
        {
            None,
            SoftSkin,
            NormalSkin,
            Hide,
            ThickHide,
            ArmorShell,
            Blubber,
            Scales,
            HeavyScales,
            Fur,
            ThickFur,
            ThickFurOverHide,
            Spines,
            Feathers,
            ThickFeather,
            FeathersOverHide,
            LightExoskeleton,
            ToughExoskeleton,
            HeavyExoskeleton
        }

        public enum BreathingMethods
        {
            None,
            Gills,
            LungsOxygenStorage,
            GillsAndLungs,
            Lungs
        }

        public enum Temperatures
        {
            None,
            ColdBloodedwithDis,
            ColdBlooded,
            PartialRegulations,
            WarmBlooded,
            WarmBloodedMetabolism
        }

        public enum GrowthRates
        {
            None,
            Metamorphosis,
            Molting,
            ContinuousGrowth,
            UnusualGrowthPattern
        }

        public enum Sexes
        {
            None,
            Asexual,
            Hermaphrodite,
            TwoSexes,
            Switching,
            ThreeOrMore,
            Alternating
        }

        public enum Gestations
        {
            None,
            SpawningPollinating,
            EggLaying,
            LiveBearing,
            LiveBearingWithPouch,
            BroodParasite,
            ParasiticYoung,
            CannibalisticEatParent,
            CannibalisticEatEachOther
        }

        public enum Strategies
        {
            None,
            StrongK,
            ModerateK,
            MedianStrategy,
            ModerateR,
            StrongR
        }

        public enum PrimarySenses
        {
            None,
            Vision,
            Hearing,
            TouchTaste
        }

        public enum Visions
        {
            None,
            Blindness,
            BlindnessSoft,
            BadSightColorblind,
            BadSight,
            Normal,
            TelescopicVision
        }

        public enum Hearings
        {
            None,
            Deafness,
            HardOfhearing,
            Normal,
            NormalPlusRange,
            Acute,
            AcutePlusSubSonic,
            AcutePlusSubSonicAndSonar
        }

        public enum Touches
        {
            None,
            Numb,
            Minus2PoorSense,
            Minus1PoorSense,
            Normal,
            Acute,
            AcutePlusSensitive
        }

        public enum TasteSmells
        {
            None,
            NoTasteSmell,
            NoSmell,
            Normal,
            Acute,
            AcutePlusDisciminatory
        }

        public enum SpecialSenses
        {
            None,
            ThreeSixtyVision,
            AbsoluteDirection,
            DisciminatoryHearing,
            PeripheralVision,
            NightVision,
            UltraVision,
            DetectHeat,
            DetectElectricFields,
            PerfectBalance,
            ScanningSense
        }

        public enum Intelligences
        {
            None,
            Mindless,
            Preprogrammed,
            LowIntelligence,
            HighIntelligence,
            Presapient,
            Sapient
        }

        public enum MatingBehaviours
        {
            None,
            NoPair,
            TempPair,
            PermanentPair,
            Harem,
            Hive
        }

        public enum SocialOrganizations
        {
            None,
            Solitary,
            PairBonded,
            SmallGroup,
            MediumGroup,
            LargeHerd
        }

        public enum Chauvinisms
        {
            None,
            RacialIntolerance,
            ExtremlyChauvinistic,
            VeryChauvinistic,
            Chauvinistic,
            Xenophobia,
            Normal,
            BroadMinded,
            VeryBroadMinded,
            Xenophilia,
            Undisciminating
        }

        public enum Concentrations
        {
            None,
            VerySingleMinded,
            SingleMinded,
            Attentive,
            Normal,
            Distractible,
            ShortAttentionSpan,
            VeryShortAttentionSpan
        }

        public enum Curiosities
        {
            None,
            VeryCurious,
            Curious,
            Nosy,
            Normal,
            Staid,
            Incurious,
            VeryIncurious
        }

        public enum Egoisms
        {
            None,
            VerySelfish,
            Selfish,
            Proud,
            Normal,
            Humble,
            Selfless,
            VerySelfless
        }

        public enum Empathies
        {
            None,
            VeryEmpathic,
            Empathy,
            Responsive,
            Normal,
            Oblivious,
            Callous,
            LowEmpathy
        }

        public enum Gegariousnesses
        {
            None,
            Gregarious,
            Chummy,
            Congenial,
            Normal,
            Uncongenial,
            Loner,
            ExtremeLoner
        }

        public enum Imaginations
        {
            None,
            ExtremlyImaginative,
            VeryImaginative,
            Imaginative,
            Normal,
            Dull,
            Hidebound,
            VeryHidebound
        }

        public enum Suspicions
        {
            None,
            VeryFearfull,
            Fearfulness,
            Careful,
            Normal,
            Fearlessness,
            VeryFearless,
            ExtremlyFearless
        }

        public enum Playfulnessess
        {
            None,
            ExtremeCompulsivePlayfulness,
            CompulsivePlayfulness,
            Playful,
            Normal,
            Serious,
            OdiousRacialHabit,
            NoSenseofHumor
        }

        public enum Communications
        {
            None,
            Chemical,
            Sound,
            Visual,
            Gestures,
            Expressions,
            Language
        }

        public Dictionary<ChemicalBasis, string> TypesofLifeDict = new Dictionary<ChemicalBasis, string>
        {
            {
                ChemicalBasis.None, "None"
            },
            {
                ChemicalBasis.Hydrogen, "Hydrogen-Based Life (Frozen worlds below -250° F or Gas Giants)"
            },
            {
                ChemicalBasis.Ammonia, "Ammonia-Based Life (Frozen worlds between - 100° F and - 30° F)"
            },
            {
                ChemicalBasis.Hydrocarbon, "Hydrocarbon-Based Life (Cold to Cool worlds)"
            },
            {
                ChemicalBasis.Water, "Water-Based Life (Cold to Hot worlds)"
            },
            {
                ChemicalBasis.Chlorine, "Chlorine - Based Life (Cold to Tropical worlds"
            },
            {
                ChemicalBasis.SulfuricAcid, "Silicon/Sulfuric Acid Life (Warm to Infernal worlds between 50° F and 600° F)"
            },
            {
                ChemicalBasis.LiquidSulfur, "Silicon/Liquid Sulfur Life (Infernal worlds between 250° F and 750° F)"
            },
            {
                ChemicalBasis.LiquidRock, "Silicon/Liquid Rock Life (Infernal worlds above 2500° F, or mantle)"
            },
            {
                ChemicalBasis.Plasma, "Plasma Life (Infernal worlds or stars above 4000° F)"
            },
            {
                ChemicalBasis.Exotic, "Exotica (Nebula - dwelling life, Machine life, Magnetic life)"
            }
        };

        public Dictionary<LandWater, string> LandWaterDict = new Dictionary<LandWater, string>
        {
            {
                LandWater.None, "None"
            },
            {
                LandWater.Land, "Mostly Landmass"
            },
            {
                LandWater.Water, "Mostly Watermass"
            }
        };

        public Dictionary<LandHabitats, string> TypesofLandHabitatsDict = new Dictionary<LandHabitats, string>
        {
            {
                LandHabitats.None, "None"
            },
            {
                LandHabitats.Plains, "Plains"
            },
            {
                LandHabitats.Desert, "Desert"
            },
            {
                LandHabitats.IslandBeach, "Island/Beach"
            },
            {
                LandHabitats.Woodlands, "Woodlands"
            },
            {
                LandHabitats.Swampland, "Swampland"
            },
            {
                LandHabitats.Mountain, "Mountain"
            },
            {
                LandHabitats.Arctic, "Arctic"
            },
            {
                LandHabitats.Jungle, "Jungle"
            }
        };

        public Dictionary<WaterHabitats, string> TypesofWaterHabitatsDict = new Dictionary<WaterHabitats, string>
        {
            {
                WaterHabitats.None, "None"
            },
            {
                WaterHabitats.Banks, "Banks"
            },
            {
                WaterHabitats.OpenOcean, "Open Ocean"
            },
            {
                WaterHabitats.FreshWaterLakes, "FreshWaterLakes"
            },
            {
                WaterHabitats.RiverStream, "River/Stream"
            },
            {
                WaterHabitats.TropicalLagoon, "TropicalLagoon"
            },
            {
                WaterHabitats.DeepOceanVents, "Deep-Ocean Vents"
            },
            {
                WaterHabitats.SaltWaterSea, "Salt-Water Sea"
            },
            {
                WaterHabitats.Reef, "Reef"
            }
        };

        public Dictionary<TrophicDiets, string> TrophicDietDict = new Dictionary<TrophicDiets, string>
        {
            {
                TrophicDiets.None, "None"
            },
            {
                TrophicDiets.CombinedMethod, "Combined Method"
            },
            {
                TrophicDiets.ParasiteSymbiont, "Parasite/Symbiont"
            },
            {
                TrophicDiets.FilterFeeder, "Filter-Feeder"
            },
            {
                TrophicDiets.PouncingCarnivore, "Pouncing Carnivore"
            },
            {
                TrophicDiets.ChasingCarnivore, "Chasing Carnivore"
            },
            {
                TrophicDiets.HijackingCarnivore, "Hijacking Carnivore"
            },
            {
                TrophicDiets.TrappingCarnivore, "Trapping Carnivore"
            },
            {
                TrophicDiets.Scavenger, "Scavenger"
            },
            {
                TrophicDiets.GatheringHerbivore, "Gathering Herbivore"
            },
            {
                TrophicDiets.GrazingHerbivore, "Grazing Herbivore"
            },
            {
                TrophicDiets.Omnivore, "Omnivore"
            },
            {
                TrophicDiets.Decomposer, "Decomposer"
            },
            {
                TrophicDiets.AutothrophPhotosynthetic, "Autothroph - Photosynthetic"
            },
            {
                TrophicDiets.AutothrophChemosynthetic, "Autothroph - Chemosynthetic"
            },
            {
                TrophicDiets.AutothrophOther, "Autothroph - Other"
            }
        };

        public Dictionary<Locomotions, string> LocomotionDict = new Dictionary<Locomotions, string>
        {
            {
                Locomotions.None, "None"
            },
            {
                Locomotions.Immobile, "Immobile"
            },
            {
                Locomotions.Bouyantflight, "Bouyant Flight"
            },
            {
                Locomotions.Climbing, "Climbing"
            },
            {
                Locomotions.Digging, "Digging"
            },
            {
                Locomotions.Floating, "Floating"
            },
            {
                Locomotions.Sailing, "Sailing"
            },
            {
                Locomotions.Slithering, "Slithering"
            },
            {
                Locomotions.Special, "Special"
            },
            {
                Locomotions.Swimming, "Swimming"
            },
            {
                Locomotions.Walking, "Walking"
            },
            {
                Locomotions.Wingedflight, "Winged Flight"
            }
        };

        public Dictionary<SizeClasses, string> SizeClassDict = new Dictionary<SizeClasses, string>
        {
            {
                SizeClasses.None, "None"
            },
            {
                SizeClasses.HumanScale, "Human-Scale"
            },
            {
                SizeClasses.Large, "Large"
            },
            {
                SizeClasses.Small, "Small"
            }
        };

        public Dictionary<Symmetries, string> SymmetryDict = new Dictionary<Symmetries, string>
        {
            {
                Symmetries.None, "None"
            },
            {
                Symmetries.Asymmetric, "Asymmetric"
            },
            {
                Symmetries.Bilateral, "Bilateral"
            },
            {
                Symmetries.Radial, "Radial"
            },
            {
                Symmetries.Spherical, "Spherical"
            },
            {
                Symmetries.Trilateral, "Trilateral"
            }
        };

        public Dictionary<Tails, string> TailsDict = new Dictionary<Tails, string>
        {
            {
                Tails.None, "None"
            },
            {
                Tails.FeatureLess, "No features (tail is a 0-point advantage)"
            },
            {
                Tails.Striker, "Striker tail (Striker doing crushing damage)"
            },
            {
                Tails.Long, "Long tail (Long enhancement)"
            },
            {
                Tails.Constricting, "Constricting tail (Constriction Attack)"
            },
            {
                Tails.Barbed, "Barbed striker tail (Striker doing cutting or piercing damage)"
            },
            {
                Tails.Gripping, "Gripping tail (counts as an Extra Arm with Bad Grip)"
            },
            {
                Tails.Branching, "Branching tail (tail splits according to body symmetry)"
            },
            {
                Tails.Combination, "Combination (roll 1d+5 twice and combine results)"
            }
        };

        public Dictionary<Skeletons, string> SkeletonDict = new Dictionary<Skeletons, string>
        {
            {
                Skeletons.None, "No Skeleton"
            },
            {
                Skeletons.Hydrostatic, "Hydrostatic skeleton"
            },
            {
                Skeletons.External, "External skeleton"
            },
            {
                Skeletons.Internal, "Internal skeleton"
            },
            {
                Skeletons.Combination, "Combination"
            }
        };

        public Dictionary<SkinClasses, string> SkinTypeDict = new Dictionary<SkinClasses, string>
        {
            {
                SkinClasses.None, "None"
            },
            {
                SkinClasses.Skin, "Skin"
            },
            {
                SkinClasses.Scales, "Scales"
            },
            {
                SkinClasses.Fur, "Fur"
            },
            {
                SkinClasses.Feathers, "Feathers"
            },
            {
                SkinClasses.Exeskeleton, "Exoskeleton (external-skeleton organisms always have Exoskeleton)"
            }
        };

        public Dictionary<Skins, string> SkinDict = new Dictionary<Skins, string>
        {
            {
                Skins.None, "None"
            },
            {
                Skins.ArmorShell, "Armor Shell"
            },
            {
                Skins.Blubber, "Blubber"
            },
            {
                Skins.Feathers, "Feathers"
            },
            {
                Skins.FeathersOverHide, "Feathers over Hide"
            },
            {
                Skins.Fur, "Fur"
            },
            {
                Skins.HeavyExoskeleton, "Heavy Exoskeleton"
            },
            {
                Skins.HeavyScales, "Heavy Scales"
            },
            {
                Skins.Hide, "Hide"
            },
            {
                Skins.LightExoskeleton, "Light Exoskeleton"
            },
            {
                Skins.NormalSkin, "Normal Skin"
            },
            {
                Skins.Scales, "Scales"
            },
            {
                Skins.SoftSkin, "Soft Skin"
            },
            {
                Skins.Spines, "Spines"
            },
            {
                Skins.ThickFeather, "Thick Feathers"
            },
            {
                Skins.ThickFur, "Thick Fur"
            },
            {
                Skins.ThickFurOverHide, "Thick Fur over Hide"
            },
            {
                Skins.ThickHide, "Thick Hide"
            },
            {
                Skins.ToughExoskeleton, "Tough Exoskeleton"
            }
        };

        public Dictionary<BreathingMethods, string> BreathingMethodDict = new Dictionary<BreathingMethods, string>
        {
            {
                BreathingMethods.None, "None"
            },
            {
                BreathingMethods.Gills, "Doesnt Breathe (Gills)"
            },
            {
                BreathingMethods.LungsOxygenStorage, "Lungs (air-breathing) with Doesn’t Breathe (Oxygen Storage))"
            },
            {
                BreathingMethods.GillsAndLungs, "Doesn’t Breathe (Gills) and Lungs (or convertible organ)"
            },
            {
                BreathingMethods.Lungs, "Lungs"
            }
        };

        public Dictionary<Temperatures, string> TemperatureDict = new Dictionary<Temperatures, string>
        {
            {
                Temperatures.None, "None"
            },
            {
                Temperatures.ColdBloodedwithDis, "Cold-blooded (with Cold-Blooded disadvantage)"
            },
            {
                Temperatures.ColdBlooded, "Cold-blooded (no disadvantage)Cold-blooded (no disadvantage)"
            },
            {
                Temperatures.PartialRegulations, "Partial regulation (temperature varies within limits)"
            },
            {
                Temperatures.WarmBlooded, "Warm-blooded"
            },
            {
                Temperatures.WarmBloodedMetabolism, "Warm-blooded (with Metabolism Control 2)"
            }
        };

        public Dictionary<GrowthRates, string> GrowthDict = new Dictionary<GrowthRates, string>
        {
            {
                GrowthRates.None, "None"
            },
            {
                GrowthRates.ContinuousGrowth, "Continuous Growth"
            },
            {
                GrowthRates.Metamorphosis, "Metamorphosis"
            },
            {
                GrowthRates.Molting, "Molting"
            },
            {
                GrowthRates.UnusualGrowthPattern, "Unusual Growth Patterns"
            }
        };

        public Dictionary<Sexes, string> SexesDict = new Dictionary<Sexes, string>
        {
            {
                Sexes.None, "None"
            },
            {
                Sexes.Asexual, "Asexual reproduction or Parthenogenesis"
            },
            {
                Sexes.Hermaphrodite, "Hermaphodite"
            },
            {
                Sexes.TwoSexes, "Two Sexes"
            },
            {
                Sexes.Switching, "Switching between male and female"
            },
            {
                Sexes.ThreeOrMore, "Three or more Sexes (1d: 1-3: three sexes, 4-5: four sexes, 6: 2d sexes)"
            },
            {
                Sexes.Alternating, "Roll twice and combine; types either alternate or are triggered by conditions"
            }
        };

        public Dictionary<Gestations, string> GestationDict = new Dictionary<Gestations, string>
        {
            {
                Gestations.None, "None"
            },
            {
                Gestations.SpawningPollinating, "Spawning/Pollinating"
            },
            {
                Gestations.EggLaying, "Egg-Laying"
            },
            {
                Gestations.LiveBearing, "Live-Bearing"
            },
            {
                Gestations.LiveBearingWithPouch, "Live-Bearing with Pouch"
            },
            {
                Gestations.BroodParasite, "Brood Parasite (young raised by another species)"
            },
            {
                Gestations.ParasiticYoung, "Parasitic Young (young implanted in a host)"
            },
            {
                Gestations.CannibalisticEatParent, "Cannibalistic Young (young implanted in parent, fatal to parent)"
            },
            {
                Gestations.CannibalisticEatEachOther, "Cannibalistic Young (young consume each other)"
            }
        };

        public Dictionary<Strategies, string> StrategyDict = new Dictionary<Strategies, string>
        {
            {
                Strategies.None, "None"
            },
            {
                Strategies.StrongK, "Strong K-Strategy: one offspring, extensive care after birth"
            },
            {
                Strategies.ModerateK, "Moderate K-Strategy: one to two offspring per litter, extensive care after birth"
            },
            {
                Strategies.MedianStrategy, "Median Strategy: 1d offspring per litter, moderate care after birth"
            },
            {
                Strategies.ModerateR, "Moderate r-Strategy: 1d+1 offspring per litter, some care after birth"
            },
            {
                Strategies.StrongR, "Strong r-Strategy: 2d offspring per litter, no care, +1 level of Short Lifespan"
            }
        };

        public Dictionary<PrimarySenses, string> PrimarySenseDict = new Dictionary<PrimarySenses, string>
        {
            {
                PrimarySenses.None, "None"
            },
            {
                PrimarySenses.Vision, "Vision"
            },
            {
                PrimarySenses.Hearing, "Hearing"
            },
            {
                PrimarySenses.TouchTaste, "Touch and Taste"
            }
        };

        public Dictionary<Visions, string> VisionDict = new Dictionary<Visions, string>
        {
            {
                Visions.None, "None"
            },
            {
                Visions.Blindness, "Blindness"
            },
            {
                Visions.BlindnessSoft, "Blindness (Can sense light and dark, -10%) [-45]"
            },
            {
                Visions.BadSightColorblind, "Bad Sight and Colorblindness"
            },
            {
                Visions.BadSight, "Bad Sight or Colorblindness*"
            },
            {
                Visions.Normal, "Normal Vision*"
            },
            {
                Visions.TelescopicVision, "Telescopic Vision 4*"
            }
        };

        public Dictionary<Hearings, string> HearingDict = new Dictionary<Hearings, string>
        {
            {
                Hearings.None, "None"
            },
            {
                Hearings.Deafness, "Deafness"
            },
            {
                Hearings.HardOfhearing, "Hard of Hearing"
            },
            {
                Hearings.Normal, "Normal Hearing*"
            },
            {
                Hearings.NormalPlusRange, "Normal Hearing with extended range (Subsonic Hearing if Large, Ultrahearing otherwise)*"
            },
            {
                Hearings.Acute, "Acute Hearing 4**"
            },
            {
                Hearings.AcutePlusSubSonic, "Acute Hearing 4 and either Subsonic Hearing or Ultrahearing**"
            },
            {
                Hearings.AcutePlusSubSonicAndSonar, "Acute Hearing 4 with Ultrasonic Hearing and Sonar**"
            }
        };

        public Dictionary<Touches, string> TouchDict = new Dictionary<Touches, string>
        {
            {
                Touches.None, "None"
            },
            {
                Touches.Numb, "Numb"
            },
            {
                Touches.Minus2PoorSense, "-2 DX from poor sense of touch"
            },
            {
                Touches.Minus1PoorSense, "-1 DX from poor sense of touch"
            },
            {
                Touches.Normal, "Human-level touch"
            },
            {
                Touches.Acute, "Acute Touch 4*"
            },
            {
                Touches.AcutePlusSensitive, "Acute Touch 4 and either Sensitive Touch or Vibration Sense*"
            }
        };

        public Dictionary<TasteSmells, string> TasteSmellDict = new Dictionary<TasteSmells, string>
        {
            {
                TasteSmells.None, "None"
            },
            {
                TasteSmells.NoTasteSmell, "No Sense of Smell/Taste"
            },
            {
                TasteSmells.NoSmell, "No Sense of Smell (can taste, -50%) [-2 points]"
            },
            {
                TasteSmells.Normal, "Normal taste/smell"
            },
            {
                TasteSmells.Acute, "Acute Taste/Smell 4 (aquatic organisms use Acute Taste only)*"
            },
            {
                TasteSmells.AcutePlusDisciminatory, "Acute Taste/Smell 4 and Discriminatory Smell (aquatic organisms use Discriminatory Taste)*"
            }
        };

        public Dictionary<SpecialSenses, string> SpecialSenseDict = new Dictionary<SpecialSenses, string>
        {
            {
                SpecialSenses.None, "None"
            },
            {
                SpecialSenses.ThreeSixtyVision, "360° Vision*"
            },
            {
                SpecialSenses.AbsoluteDirection, "Absolute Direction"
            },
            {
                SpecialSenses.DisciminatoryHearing, "Discriminatory Hearing"
            },
            {
                SpecialSenses.PeripheralVision, "Peripheral Vision"
            },
            {
                SpecialSenses.NightVision, "Night Vision"
            },
            {
                SpecialSenses.UltraVision, "Ultravision"
            },
            {
                SpecialSenses.DetectHeat, "Detect (Heat)"
            },
            {
                SpecialSenses.DetectElectricFields, "Detect (Electric Fields)"
            },
            {
                SpecialSenses.PerfectBalance, "Perfect Balance"
            },
            {
                SpecialSenses.ScanningSense, "Scanning Sense (Radar)"
            }
        };

        public Dictionary<Intelligences, string> IntelligenceDict = new Dictionary<Intelligences, string>
        {
            {
                Intelligences.None, "None"
            },
            {
                Intelligences.Mindless, "Mindless (IQ 0)"
            },
            {
                Intelligences.Preprogrammed, "Preprogrammed (IQ 1 and Cannot Learn)"
            },
            {
                Intelligences.LowIntelligence, "Low Intelligence (IQ 1-3 and Bestial)"
            },
            {
                Intelligences.HighIntelligence, "High Intelligence (IQ 3-5 and Bestial)"
            },
            {
                Intelligences.Presapient, "Presapient (IQ 5)"
            },
            {
                Intelligences.Sapient, "Sapient"
            }
        };

        public Dictionary<MatingBehaviours, string> MatingBahaviourDict = new Dictionary<MatingBehaviours, string>
        {
            {
                MatingBehaviours.None, "None"
            },
            {
                MatingBehaviours.NoPair, "Mating only, no pair bond"
            },
            {
                MatingBehaviours.TempPair, "Temporary pair bond"
            },
            {
                MatingBehaviours.PermanentPair, "Permanent pair bond"
            },
            {
                MatingBehaviours.Harem, "Harem"
            },
            {
                MatingBehaviours.Hive, "Hive"
            }
        };

        public Dictionary<SocialOrganizations, string> SocialOrganizationDict = new Dictionary<SocialOrganizations, string>
        {
            {
                SocialOrganizations.None, "None"
            },
            {
                SocialOrganizations.LargeHerd, "Large Herd"
            },
            {
                SocialOrganizations.MediumGroup, "Medium Group"
            },
            {
                SocialOrganizations.PairBonded, "Pair Bonded"
            },
            {
                SocialOrganizations.SmallGroup, "Small Group"
            },
            {
                SocialOrganizations.Solitary, "Solitary"
            }
        };

        public Dictionary<Chauvinisms, string> ChauvinismDict = new Dictionary<Chauvinisms, string>
        {
            {
                Chauvinisms.None, "None"
            },
            {
                Chauvinisms.ExtremlyChauvinistic, "Chauvinistic (quirk) (becomes Racial Intolerance if Empathy is less than +1 or Suspicion greater than -1; becomes Xenophobia if Suspicion is greater than +1)"
            },
            {
                Chauvinisms.VeryChauvinistic, "Chauvinistic (quirk) (becomes Racial Intolerance if Empathy is less than +1 or Suspicion greater than -1)"
            },
            {
                Chauvinisms.Chauvinistic, "Chauvinistic (quirk) (becomes Racial Intolerance if Empathy is less than 0 or Suspicion greater than 0)"
            },
            {
                Chauvinisms.Normal, "Normal"
            },
            {
                Chauvinisms.BroadMinded, "Broad-Minded (quirk)"
            },
            {
                Chauvinisms.VeryBroadMinded, "Broad-Minded (quirk) (becomes Xenophilia at 15 if Suspicion is less than 0 and Empathy is greater than 0)"
            },
            {
                Chauvinisms.Undisciminating, "Undiscriminating (quirk – considers all intelligence to be one “species”) (becomes Xenophilia at 12 if Suspicion is less than 0 or Empathy is greater than 0; Xenophilia at 9 if both are true)"
            }
        };

        public Dictionary<Concentrations, string> ConcentrationDict = new Dictionary<Concentrations, string>
        {
            {
                Concentrations.None, "None"
            },
            {
                Concentrations.VerySingleMinded, "Single-Minded and either High Pain Threshold or one 5-point Talent"
            },
            {
                Concentrations.SingleMinded, "Single-Minded"
            },
            {
                Concentrations.Attentive, "Attentive (quirk)"
            },
            {
                Concentrations.Normal, "Normal"
            },
            {
                Concentrations.Distractible, "Distractible (quirk)"
            },
            {
                Concentrations.ShortAttentionSpan, "Short Attention Span (12)"
            },
            {
                Concentrations.VeryShortAttentionSpan, "Short Attention Span (9)"
            }
        };

        public Dictionary<Curiosities, string> CuriosityDict = new Dictionary<Curiosities, string>
        {
            {
                Curiosities.None, "None"
            },
            {
                Curiosities.VeryCurious, "Curious (9) (if Concentration or Suspicion is 0 or less, reduce selfcontrol number to 6)"
            },
            {
                Curiosities.Curious, "Curious (12) (if Concentration is 0 or less, change self-control number to 9)"
            },
            {
                Curiosities.Nosy, "Nosy (quirk) (becomes Curious at 12 if Concentration is 0 or less)"
            },
            {
                Curiosities.Normal, "Normal"
            },
            {
                Curiosities.Staid, "Staid (quirk)"
            },
            {
                Curiosities.Incurious, "Incurious (12) (self-control number becomes 9 if Suspicion is less than 0)"
            },
            {
                Curiosities.VeryIncurious, "Incurious (9)"
            }
        };

        public Dictionary<Egoisms, string> EgoismDict = new Dictionary<Egoisms, string>
        {
            {
                Egoisms.None, "None"
            },
            {
                Egoisms.VerySelfish, "Selfish (9)"
            },
            {
                Egoisms.Selfish, "Selfish (12) (control number becomes 9 if Suspicion is more than 0 or Empathy is less than 0)"
            },
            {
                Egoisms.Proud, "Proud (quirk) (Selfish at 12 if Suspicion is more than 0; Selfish at 9 if Suspicion is +2 or more or if Empathy is -2 or less)"
            },
            {
                Egoisms.Normal, "Normal"
            },
            {
                Egoisms.Humble, "Humble (quirk)"
            },
            {
                Egoisms.Selfless, "Selfless (12) (control number becomes 9 if Chauvinism is +2 or more)"
            },
            {
                Egoisms.VerySelfless, "Selfless (6)"
            }
        };

        public Dictionary<Empathies, string> EmpathyDict = new Dictionary<Empathies, string>
        {
            {
                Empathies.VeryEmpathic, "Empathy (if Gregariousness is more than 0 add Charitable at 12)"
            },
            {
                Empathies.Empathy, "Empathy (Sensitive)"
            },
            {
                Empathies.Responsive, "Responsive (quirk) (becomes Sensitive if Gregariousness is more than 0 and Suspicion is less than 0)"
            },
            {
                Empathies.Normal, "Normal"
            },
            {
                Empathies.Oblivious, "Oblivious"
            },
            {
                Empathies.Callous, "Callous"
            },
            {
                Empathies.LowEmpathy, "Low Empathy (carnivores add Bloodlust at 12)"
            }
        };

        public Dictionary<Gegariousnesses, string> GegariousnessnessDict = new Dictionary<Gegariousnesses, string>
        {
            {
                Gegariousnesses.None, "None"
            },
            {
                Gegariousnesses.Gregarious, "Gregarious"
            },
            {
                Gegariousnesses.Chummy, "Chummy"
            },
            {
                Gegariousnesses.Congenial, "Congenial (quirk)"
            },
            {
                Gegariousnesses.Normal, "Normal"
            },
            {
                Gegariousnesses.Uncongenial, "Uncongenial (quirk)"
            },
            {
                Gegariousnesses.Loner, "Loner (12)"
            },
            {
                Gegariousnesses.ExtremeLoner, "Loner (9)"
            }
        };

        public Dictionary<Imaginations, string> ImaginationDict = new Dictionary<Imaginations, string>
        {
            {
                Imaginations.None, "None"
            },
            {
                Imaginations.ExtremlyImaginative, "Imaginative (quirk) (as with +2 below, but if Empathy is less than +1 add the Odious Racial Habit (Nonstop Idea Factory) [-5])"
            },
            {
                Imaginations.VeryImaginative, "Imaginative (quirk) (as with +1 below, but adds the quirk Dreamer if Egoism is greater than 0 or if Concentration is less than +1)"
            },
            {
                Imaginations.Imaginative, "Imaginative (quirk) (becomes Versatile if Concentration is 0 or more and Egoism is less than +2)"
            },
            {
                Imaginations.Normal, "Normal"
            },
            {
                Imaginations.Dull, "Dull (quirk)"
            },
            {
                Imaginations.Hidebound, "Hidebound"
            },
            {
                Imaginations.VeryHidebound, "Hidebound and -1 IQ"
            }
        };

        public Dictionary<Suspicions, string> SuspicionDict = new Dictionary<Suspicions, string>
        {
            {
                Suspicions.None, "None"
            },
            {
                Suspicions.VeryFearfull, "Fearfulness 2, and Cowardice (if herbivore) or Paranoia (if carnivore)"
            },
            {
                Suspicions.Fearfulness, "Fearfulness 1 (becomes Careful quirk if Curiosity is -3)"
            },
            {
                Suspicions.Careful, "Careful (quirk) (ignore if Curiosity is -2 or less)"
            },
            {
                Suspicions.Normal, "Normal"
            },
            {
                Suspicions.Fearlessness, "Fearlessness 1"
            },
            {
                Suspicions.VeryFearless, "Fearlessness 2 (add Overconfidence if Egoism is +2 or more)"
            },
            {
                Suspicions.ExtremlyFearless, "Fearlessness 3 (add Overconfidence if Egoism is +1 or more; Fearlessness becomes Unfazeable if Chauvinism is -3 or less)"
            }
        };

        public Dictionary<Playfulnessess, string> PlayfulnessDict = new Dictionary<Playfulnessess, string>
        {
            {
                Playfulnessess.None, "None"
            },
            {
                Playfulnessess.ExtremeCompulsivePlayfulness, "Compulsive Playfulness (9) (becomes Trickster at 12 if species is Overconfident)"
            },
            {
                Playfulnessess.CompulsivePlayfulness, "Compulsive Playfulness (12) [-5*]"
            },
            {
                Playfulnessess.Playful, "Playful (quirk)"
            },
            {
                Playfulnessess.Normal, "Normal (occasionally playful)"
            },
            {
                Playfulnessess.Serious, "Serious (quirk)"
            },
            {
                Playfulnessess.OdiousRacialHabit, "Odious Racial Habit (Wet Blanket) [-5]"
            },
            {
                Playfulnessess.NoSenseofHumor, "No Sense of Humor"
            }
        };

        #endregion

        #region PropsAndConstructor

        public ChemicalBasis ChemicalBase { get; private set; }
        public LandWater LandOrWater { get; private set; }
        public LandHabitats LandHabitat { get; private set; }
        public WaterHabitats WaterHabitat { get; private set; }
        public TrophicDiets TrophicDiet { get; private set; }
        public Locomotions PrimaryLocomotion { get; private set; }
        public Locomotions SecondaryLocomotion { get; private set; }
        public bool HasSecondaryLocomotion { get; private set; }
        private double Gravity { get; set; }
        public SizeClasses SizeClass { get; private set; }
        public double Size { get; private set; }
        public Symmetries Symmetry { get; private set; }
        public int Sides { get; private set; }
        public int LimbSegments { get; private set; }
        public Tails Tail { get; private set; }
        public int Manipulators { get; private set; }
        public Skeletons Skeleton { get; private set; }
        public SkinClasses SkinClass { get; private set; }
        public Skins Skin { get; private set; }
        public BreathingMethods Breathing { get; private set; }
        public Temperatures Temperture { get; private set; }
        public GrowthRates Growth { get; private set; }
        public Sexes Sex { get; private set; }
        public Gestations Gestation { get; private set; }
        public Strategies Strategy { get; private set; }
        public int OffspringCount { get; private set; }
        public PrimarySenses PrimarySense { get; private set; }
        public Visions Vision { get; private set; }
        public Hearings Hearing { get; private set; }
        public Touches Touch { get; private set; }
        public TasteSmells TasteSmell { get; private set; }
        private List<SpecialSenses> SpecialSense { get; set; }
        public Intelligences Intelligence { get; private set; }
        public int IntelligenceValue { get; private set; }
        public MatingBehaviours MatingBahavior { get; private set; }
        public SocialOrganizations SocialOrganization { get; private set; }
        public int SocialGroupSize { get; private set; }
        public Concentrations Concentration { get; private set; }
        public Curiosities Curiosity { get; private set; }
        public Egoisms Egoism { get; private set; }
        public Empathies Empathy { get; private set; }
        public Gegariousnesses Gegariousness { get; private set; }
        public Imaginations Imagination { get; private set; }
        public Chauvinisms Chauvinism { get; private set; }
        public Suspicions Suspicion { get; private set; }
        public Playfulnessess Playfulness { get; private set; }

        /// <summary>
        ///     Creates a New Alien Race
        /// </summary>
        /// <param name="waterModPercent">
        ///     "Base Value is 0, set to -1 if you Have no Water on the Planet, else Input the Percentage
        ///     of Water on the Planet"
        /// </param>
        /// <param name="isSapient"> If the Alien is Suppose to be Sapient or not</param>
        /// <param name="gravity"> The Gravit of the Home Planet</param>
        public Alien( int waterModPercent = 0, bool isSapient = true, double gravity = 1.00 )
        {
            ChemicalBase = DetermineChemicalBasis();
            LandOrWater = DetermineLandWater(waterModPercent);
            if (LandOrWater == LandWater.Land)
            {
                WaterHabitat = WaterHabitats.None;
                LandHabitat = DetermineLandHabitat();
            }
            if (LandOrWater == LandWater.Water)
            {
                LandHabitat = LandHabitats.None;
                WaterHabitat = DetermineWaterHabitat();
            }

            TrophicDiet = DetermineDiet(isSapient);
            PrimaryLocomotion = DeterminePrimaryLocomotion();
            if (HasSecondaryLocomotion)
            {
                SecondaryLocomotion = DetermineSecondaryLocomotion();
            }
            Gravity = gravity;
            SizeClass = DetermineSizeClass();
            Size = DetermineSize();
            Symmetry = DetermineSymmetry();
            Sides = DetermineSides();
            LimbSegments = DetermineLimbSegments();
            Tail = DetermineTail();
            Manipulators = DetermineManipulators() * 2;
            Skeleton = DetermineSkeleton();
            SkinClass = DetermineSkinClass();
            Skin = DetermineSkin();
            Breathing = DetermineBreathing();
            Temperture = DetermineTemperature();
            Growth = DetermineGrowth();
            Sex = DetermineSexes();
            Gestation = DetermineGestation();
            Strategy = DetermineStrategy();
            PrimarySense = DeterminePrimarySense();
            Vision = DetermineVision();
            Hearing = DetermineHearing();
            Touch = DetermineTouch();
            TasteSmell = DetermineTasteSmell();
            SpecialSense = DetermineSpecialSenses();
            Intelligence = DetermineIntelligence(isSapient);
            MatingBahavior = DetermineMatingBehaviour();
            SocialOrganization = DetermineSocialOrganization();
            Concentration = DetermineConcentration();
            Curiosity = DetermineCuriosity();
            Egoism = DetermineEgoism();
            Empathy = DetermineEmpathy();
            Gegariousness = DetermineGegariousness();
            Imagination = DetermineImagination();
            Suspicion = DetermineSuspicion();
            Playfulness = DeterminePlayfulness();

            Chauvinism = DetermineChauvinism();
        }

        #endregion

        #region GenerationMethods

        private ChemicalBasis DetermineChemicalBasis()
        {
            var cb = ChemicalBasis.None;
            var dice = new Dice();
            var roll = dice.GurpsRoll();
            switch (roll)
            {
                case 3:
                case 4:
                case 5:
                    cb = ChemicalBasis.Hydrogen;
                    break;
                case 6:
                case 7:
                    cb = ChemicalBasis.Ammonia;
                    break;
                case 8:
                    cb = ChemicalBasis.Hydrocarbon;
                    break;
                case 9:
                case 10:
                case 11:
                    cb = ChemicalBasis.Water;
                    break;
                case 12:
                    cb = ChemicalBasis.Chlorine;
                    break;
                case 13:
                    cb = ChemicalBasis.SulfuricAcid;
                    break;
                case 14:
                    cb = ChemicalBasis.LiquidSulfur;
                    break;
                case 15:
                    cb = ChemicalBasis.LiquidRock;
                    break;
                case 16:
                    cb = ChemicalBasis.Plasma;
                    break;
                case 17:
                case 18:
                    cb = ChemicalBasis.Exotic;
                    break;
            }
            return cb;
        }

        private LandWater DetermineLandWater( int waterModPercent )
        {
            var lw = LandWater.None;
            var dice = new Dice();
            var roll = dice.Rng(6);

            var mod = 0;
            if (waterModPercent == 0)
            {
                mod = 0;
            }
            if (waterModPercent <= 50 && waterModPercent > 0)
            {
                mod = -1;
            }
            if (waterModPercent >= 80 && waterModPercent < 90)
            {
                mod = 1;
            }
            if (waterModPercent >= 90)
            {
                mod = 2;
            }

            roll += mod;

            switch (roll)
            {
                case 1:
                case 2:
                case 3:
                    lw = LandWater.Land;
                    break;
                case 4:
                case 5:
                case 6:
                    lw = LandWater.Water;
                    break;
            }
            return lw;
        }

        private LandHabitats DetermineLandHabitat()
        {
            var dice = new Dice();
            var roll = dice.GurpsRoll();
            var hab = LandHabitats.None;
            switch (roll)
            {
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    hab = LandHabitats.Plains;
                    break;
                case 8:
                    hab = LandHabitats.Desert;
                    break;
                case 9:
                    hab = LandHabitats.IslandBeach;
                    break;
                case 10:
                    hab = LandHabitats.Woodlands;
                    break;
                case 11:
                    hab = LandHabitats.Swampland;
                    break;
                case 12:
                    hab = LandHabitats.Mountain;
                    break;
                case 13:
                    hab = LandHabitats.Arctic;
                    break;
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                    hab = LandHabitats.Jungle;
                    break;
            }
            return hab;
        }

        private WaterHabitats DetermineWaterHabitat()
        {
            var dice = new Dice();
            var roll = dice.GurpsRoll();
            var hab = WaterHabitats.None;
            switch (roll)
            {
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    hab = WaterHabitats.Banks;
                    break;
                case 8:
                    hab = WaterHabitats.OpenOcean;
                    break;
                case 9:
                    hab = WaterHabitats.FreshWaterLakes;
                    break;
                case 10:
                    hab = WaterHabitats.RiverStream;
                    break;
                case 11:
                    hab = WaterHabitats.TropicalLagoon;
                    break;
                case 12:
                    hab = WaterHabitats.DeepOceanVents;
                    break;
                case 13:
                    hab = WaterHabitats.SaltWaterSea;
                    break;
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                    hab = WaterHabitats.Reef;
                    break;
            }
            return hab;
        }

        private TrophicDiets DetermineDiet( bool issapient )
        {
            var diet = TrophicDiets.None;
            var dice = new Dice();
            var roll = dice.GurpsRoll();
            if (issapient)
            {
                switch (roll)
                {
                    case 3:
                        diet = TrophicDiets.CombinedMethod;
                        break;
                    case 4:
                        diet = TrophicDiets.ParasiteSymbiont;
                        break;
                    case 5:
                        diet = TrophicDiets.FilterFeeder;
                        break;
                    case 6:
                        diet = TrophicDiets.PouncingCarnivore;
                        break;
                    case 7:
                        diet = TrophicDiets.Scavenger;
                        break;
                    case 8:
                    case 9:
                        diet = TrophicDiets.GatheringHerbivore;
                        break;
                    case 10:
                        diet = TrophicDiets.Omnivore;
                        break;
                    case 11:
                    case 12:
                        diet = TrophicDiets.ChasingCarnivore;
                        break;
                    case 13:
                        diet = TrophicDiets.GrazingHerbivore;
                        break;
                    case 14:
                        diet = TrophicDiets.HijackingCarnivore;
                        break;
                    case 15:
                    case 16:
                        diet = TrophicDiets.TrappingCarnivore;
                        break;
                    case 17:
                        diet = TrophicDiets.Decomposer;
                        break;
                    case 18:
                        var newdice = new Dice();
                        var autothrophroll = newdice.Rng(6);
                        switch (autothrophroll)
                        {
                            case 1:
                            case 2:
                            case 3:
                                diet = TrophicDiets.AutothrophPhotosynthetic;
                                break;
                            case 4:
                            case 5:
                                diet = TrophicDiets.AutothrophChemosynthetic;
                                break;
                            case 6:
                                diet = TrophicDiets.AutothrophOther;
                                break;
                        }
                        break;
                }
            }
            else
            {
                switch (roll)
                {
                    case 3:
                        diet = TrophicDiets.CombinedMethod;
                        break;
                    case 4:
                        var newdice = new Dice();
                        var autothrophroll = newdice.Rng(6);
                        switch (autothrophroll)
                        {
                            case 1:
                            case 2:
                            case 3:
                                diet = TrophicDiets.AutothrophPhotosynthetic;
                                break;
                            case 4:
                            case 5:
                                diet = TrophicDiets.AutothrophChemosynthetic;
                                break;
                            case 6:
                                diet = TrophicDiets.AutothrophOther;
                                break;
                        }
                        break;
                    case 5:
                        diet = TrophicDiets.Decomposer;
                        break;
                    case 6:
                        diet = TrophicDiets.Scavenger;
                        break;
                    case 7:
                        diet = TrophicDiets.Omnivore;
                        break;
                    case 8:
                    case 9:
                        diet = TrophicDiets.GatheringHerbivore;
                        break;
                    case 10:
                    case 11:
                        diet = TrophicDiets.GrazingHerbivore;
                        break;
                    case 12:
                        diet = TrophicDiets.PouncingCarnivore;
                        break;
                    case 13:
                        diet = TrophicDiets.PouncingCarnivore;
                        break;
                    case 14:
                        diet = TrophicDiets.TrappingCarnivore;
                        break;
                    case 15:
                        diet = TrophicDiets.HijackingCarnivore;
                        break;
                    case 16:
                        diet = TrophicDiets.FilterFeeder;
                        break;
                    case 17:
                    case 18:
                        diet = TrophicDiets.ParasiteSymbiont;
                        break;
                }
            }
            return diet;
        }

        private Locomotions DeterminePrimaryLocomotion()
        {
            var primaryLocomotion = Locomotions.None;
            var dice = new Dice();
            var roll = dice.Rng(2, 6);
            if (TrophicDiet == TrophicDiets.PouncingCarnivore || TrophicDiet == TrophicDiets.ChasingCarnivore || TrophicDiet == TrophicDiets.Omnivore || TrophicDiet == TrophicDiets.GatheringHerbivore || TrophicDiet == TrophicDiets.Scavenger)
            {
                roll += 1;
            }
            if (LandHabitat == LandHabitats.Arctic)
            {
                switch (roll)
                {
                    case 2:
                        primaryLocomotion = Locomotions.Immobile;
                        break;
                    case 3:
                    case 4:
                        primaryLocomotion = Locomotions.Slithering;
                        break;
                    case 5:
                    case 6:
                        primaryLocomotion = Locomotions.Swimming;
                        HasSecondaryLocomotion = true;
                        break;
                    case 7:
                        primaryLocomotion = Locomotions.Digging;
                        HasSecondaryLocomotion = true;
                        break;
                    case 8:
                    case 9:
                        primaryLocomotion = Locomotions.Walking;
                        break;
                    case 10:
                    case 11:
                        primaryLocomotion = Locomotions.Wingedflight;
                        HasSecondaryLocomotion = true;
                        break;
                    case 12:
                    case 13:
                        primaryLocomotion = Locomotions.Special;
                        break;
                }
            }
            if (WaterHabitat == WaterHabitats.Banks || WaterHabitat == WaterHabitats.OpenOcean)
            {
                switch (roll)
                {
                    case 2:
                    case 3:
                        primaryLocomotion = Locomotions.Immobile;
                        break;
                    case 4:
                        primaryLocomotion = Locomotions.Floating;
                        break;
                    case 5:
                        primaryLocomotion = Locomotions.Sailing;
                        break;
                    case 6:
                    case 7:
                    case 8:
                        primaryLocomotion = Locomotions.Swimming;
                        break;
                    case 9:
                    case 10:
                    case 11:
                        primaryLocomotion = Locomotions.Wingedflight;
                        HasSecondaryLocomotion = true;
                        break;
                    case 12:
                    case 13:
                        primaryLocomotion = Locomotions.Special;
                        break;
                }
            }
            if (WaterHabitat == WaterHabitats.DeepOceanVents || WaterHabitat == WaterHabitats.Reef)
            {
                switch (roll)
                {
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        primaryLocomotion = Locomotions.Immobile;
                        break;
                    case 6:
                        primaryLocomotion = Locomotions.Floating;
                        break;
                    case 7:
                        primaryLocomotion = Locomotions.Digging;
                        HasSecondaryLocomotion = true;
                        break;
                    case 8:
                    case 9:
                        primaryLocomotion = Locomotions.Walking;
                        HasSecondaryLocomotion = true;
                        break;
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                        primaryLocomotion = Locomotions.Swimming;
                        break;
                }
            }
            if (LandHabitat == LandHabitats.Desert)
            {
                switch (roll)
                {
                    case 2:
                        primaryLocomotion = Locomotions.Immobile;
                        break;
                    case 3:
                    case 4:
                        primaryLocomotion = Locomotions.Slithering;
                        break;
                    case 5:
                        primaryLocomotion = Locomotions.Digging;
                        HasSecondaryLocomotion = true;
                        break;
                    case 6:
                    case 7:
                    case 8:
                        primaryLocomotion = Locomotions.Walking;
                        break;
                    case 9:
                    case 10:
                    case 11:
                        primaryLocomotion = Locomotions.Wingedflight;
                        HasSecondaryLocomotion = true;
                        break;
                    case 12:
                    case 13:
                        primaryLocomotion = Locomotions.Special;
                        break;
                }
            }
            if (LandHabitat == LandHabitats.IslandBeach)
            {
                switch (roll)
                {
                    case 2:
                        primaryLocomotion = Locomotions.Immobile;
                        break;
                    case 3:
                    case 4:
                        primaryLocomotion = Locomotions.Slithering;
                        break;
                    case 5:
                        primaryLocomotion = Locomotions.Digging;
                        HasSecondaryLocomotion = true;
                        break;
                    case 6:
                    case 7:
                        primaryLocomotion = Locomotions.Walking;
                        break;
                    case 8:
                        primaryLocomotion = Locomotions.Climbing;
                        HasSecondaryLocomotion = true;
                        break;
                    case 9:
                        primaryLocomotion = Locomotions.Swimming;
                        HasSecondaryLocomotion = true;
                        break;
                    case 10:
                    case 11:
                        primaryLocomotion = Locomotions.Wingedflight;
                        HasSecondaryLocomotion = true;
                        break;
                    case 12:
                    case 13:
                        primaryLocomotion = Locomotions.Special;
                        break;
                }
            }
            if (WaterHabitat == WaterHabitats.TropicalLagoon)
            {
                switch (roll)
                {
                    case 2:
                    case 3:
                    case 4:
                        primaryLocomotion = Locomotions.Immobile;
                        break;
                    case 5:
                        primaryLocomotion = Locomotions.Floating;
                        break;
                    case 6:
                        primaryLocomotion = Locomotions.Slithering;
                        HasSecondaryLocomotion = true;
                        break;
                    case 7:
                        primaryLocomotion = Locomotions.Walking;
                        HasSecondaryLocomotion = true;
                        break;
                    case 8:
                        primaryLocomotion = Locomotions.Digging;
                        HasSecondaryLocomotion = true;
                        break;
                    case 9:
                        primaryLocomotion = Locomotions.Swimming;
                        break;
                    case 10:
                    case 11:
                        primaryLocomotion = Locomotions.Wingedflight;
                        break;
                    case 12:
                    case 13:
                        primaryLocomotion = Locomotions.Special;
                        break;
                }
            }
            if (WaterHabitat == WaterHabitats.SaltWaterSea || WaterHabitat == WaterHabitats.FreshWaterLakes)
            {
                switch (roll)
                {
                    case 2:
                    case 3:
                        primaryLocomotion = Locomotions.Immobile;
                        break;
                    case 4:
                        primaryLocomotion = Locomotions.Floating;
                        break;
                    case 5:
                        primaryLocomotion = Locomotions.Walking;
                        HasSecondaryLocomotion = true;
                        break;
                    case 6:
                        primaryLocomotion = Locomotions.Slithering;
                        HasSecondaryLocomotion = true;
                        break;
                    case 7:
                    case 8:
                    case 9:
                        primaryLocomotion = Locomotions.Swimming;
                        break;
                    case 10:
                    case 11:
                        primaryLocomotion = Locomotions.Wingedflight;
                        HasSecondaryLocomotion = true;
                        break;
                    case 12:
                    case 13:
                        primaryLocomotion = Locomotions.Special;
                        break;
                }
            }
            if (LandHabitat == LandHabitats.Mountain)
            {
                switch (roll)
                {
                    case 2:
                        primaryLocomotion = Locomotions.Immobile;
                        break;
                    case 3:
                    case 4:
                        primaryLocomotion = Locomotions.Slithering;
                        break;
                    case 5:
                        primaryLocomotion = Locomotions.Digging;
                        HasSecondaryLocomotion = true;
                        break;
                    case 6:
                    case 7:
                        primaryLocomotion = Locomotions.Walking;
                        HasSecondaryLocomotion = true;
                        break;
                    case 8:
                        primaryLocomotion = Locomotions.Climbing;
                        HasSecondaryLocomotion = true;
                        break;
                    case 9:
                    case 10:
                    case 11:
                        primaryLocomotion = Locomotions.Wingedflight;
                        HasSecondaryLocomotion = true;
                        break;
                    case 12:
                    case 13:
                        primaryLocomotion = Locomotions.Special;
                        break;
                }
            }
            if (LandHabitat == LandHabitats.Plains)
            {
                switch (roll)
                {
                    case 2:
                        primaryLocomotion = Locomotions.Immobile;
                        break;
                    case 3:
                    case 4:
                        primaryLocomotion = Locomotions.Slithering;
                        break;
                    case 5:
                        primaryLocomotion = Locomotions.Digging;
                        HasSecondaryLocomotion = true;
                        break;
                    case 6:
                    case 7:
                    case 8:
                        primaryLocomotion = Locomotions.Walking;
                        break;
                    case 9:
                    case 10:
                    case 11:
                        primaryLocomotion = Locomotions.Wingedflight;
                        HasSecondaryLocomotion = true;
                        break;
                    case 12:
                    case 13:
                        primaryLocomotion = Locomotions.Special;
                        break;
                }
            }
            if (WaterHabitat == WaterHabitats.RiverStream)
            {
                switch (roll)
                {
                    case 2:
                    case 3:
                        primaryLocomotion = Locomotions.Immobile;
                        break;
                    case 4:
                        primaryLocomotion = Locomotions.Floating;
                        break;
                    case 5:
                        primaryLocomotion = Locomotions.Slithering;
                        HasSecondaryLocomotion = true;
                        break;
                    case 6:
                        primaryLocomotion = Locomotions.Digging;
                        HasSecondaryLocomotion = true;
                        break;
                    case 7:
                        primaryLocomotion = Locomotions.Walking;
                        HasSecondaryLocomotion = true;
                        break;
                    case 8:
                    case 9:
                        primaryLocomotion = Locomotions.Swimming;
                        break;
                    case 10:
                    case 11:
                        primaryLocomotion = Locomotions.Wingedflight;
                        HasSecondaryLocomotion = true;
                        break;
                    case 12:
                    case 13:
                        primaryLocomotion = Locomotions.Special;
                        break;
                }
            }
            if (LandHabitat == LandHabitats.Swampland)
            {
                switch (roll)
                {
                    case 2:
                        primaryLocomotion = Locomotions.Immobile;
                        break;
                    case 3:
                    case 4:
                    case 5:
                        primaryLocomotion = Locomotions.Swimming;
                        HasSecondaryLocomotion = true;
                        break;
                    case 6:
                        primaryLocomotion = Locomotions.Slithering;
                        break;
                    case 7:
                        primaryLocomotion = Locomotions.Digging;
                        HasSecondaryLocomotion = true;
                        break;
                    case 8:
                        primaryLocomotion = Locomotions.Walking;
                        break;
                    case 9:
                        primaryLocomotion = Locomotions.Climbing;
                        HasSecondaryLocomotion = true;
                        break;
                    case 10:
                    case 11:
                        primaryLocomotion = Locomotions.Wingedflight;
                        HasSecondaryLocomotion = true;
                        break;
                    case 12:
                    case 13:
                        primaryLocomotion = Locomotions.Special;
                        break;
                }
            }
            if (LandHabitat == LandHabitats.Woodlands || LandHabitat == LandHabitats.Jungle)
            {
                switch (roll)
                {
                    case 2:
                        primaryLocomotion = Locomotions.Immobile;
                        break;
                    case 3:
                    case 4:
                        primaryLocomotion = Locomotions.Slithering;
                        break;
                    case 5:
                        primaryLocomotion = Locomotions.Digging;
                        HasSecondaryLocomotion = true;
                        break;
                    case 6:
                    case 7:
                        primaryLocomotion = Locomotions.Walking;
                        break;
                    case 8:
                    case 9:
                        primaryLocomotion = Locomotions.Climbing;
                        HasSecondaryLocomotion = true;
                        break;
                    case 10:
                    case 11:
                        primaryLocomotion = Locomotions.Wingedflight;
                        HasSecondaryLocomotion = true;
                        break;
                    case 12:
                    case 13:
                        primaryLocomotion = Locomotions.Special;
                        break;
                }
            }
            return primaryLocomotion;
        }

        private Locomotions DetermineSecondaryLocomotion()
        {
            var secondaryLocomotion = Locomotions.None;
            var dice = new Dice();
            var roll = dice.Rng(2, 6);
            if (PrimaryLocomotion == Locomotions.Climbing)
            {
                switch (roll)
                {
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        secondaryLocomotion = Locomotions.Slithering;
                        break;
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                        secondaryLocomotion = Locomotions.Walking;
                        break;
                    case 12:
                        secondaryLocomotion = Locomotions.None;
                        break;
                }
            }
            if (PrimaryLocomotion == Locomotions.Digging)
            {
                if (LandOrWater == LandWater.Land)
                {
                    switch (roll)
                    {
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            secondaryLocomotion = Locomotions.Slithering;
                            break;
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                            secondaryLocomotion = Locomotions.Walking;
                            break;
                        case 12:
                            secondaryLocomotion = Locomotions.None;
                            break;
                    }
                }
                if (LandOrWater == LandWater.Water)
                {
                    switch (roll)
                    {
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            secondaryLocomotion = Locomotions.Slithering;
                            break;
                        case 6:
                        case 7:
                            secondaryLocomotion = Locomotions.Walking;
                            break;
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                            secondaryLocomotion = Locomotions.Swimming;
                            break;
                        case 12:
                            secondaryLocomotion = Locomotions.None;
                            break;
                    }
                }
            }
            if (PrimaryLocomotion == Locomotions.Slithering)
            {
                switch (roll)
                {
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        secondaryLocomotion = Locomotions.Swimming;
                        break;
                    case 11:
                    case 12:
                        secondaryLocomotion = Locomotions.None;
                        break;
                }
            }
            if (PrimaryLocomotion == Locomotions.Walking)
            {
                switch (roll)
                {
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        secondaryLocomotion = Locomotions.Swimming;
                        break;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                        secondaryLocomotion = Locomotions.None;
                        break;
                }
            }
            if (PrimaryLocomotion == Locomotions.Wingedflight)
            {
                switch (roll)
                {
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        secondaryLocomotion = Locomotions.Climbing;
                        break;
                    case 6:
                    case 7:
                        secondaryLocomotion = Locomotions.Swimming;
                        break;
                    case 8:
                    case 9:
                    case 10:
                        secondaryLocomotion = Locomotions.Walking;
                        break;
                    case 11:
                        secondaryLocomotion = Locomotions.Slithering;
                        break;
                    case 12:
                        secondaryLocomotion = Locomotions.None;
                        break;
                }
            }
            return secondaryLocomotion;
        }

        private SizeClasses DetermineSizeClass()
        {
            var sizeclass = SizeClasses.None;
            var dice = new Dice();
            var roll = dice.Rng(6);
            if (Gravity <= 0.4)
            {
                roll += 2;
            }
            if (Gravity >= 0.5 && Gravity <= 0.75)
            {
                roll += 1;
            }
            if (Gravity >= 1.5 && Gravity <= 2.00)
            {
                roll -= 1;
            }
            if (Gravity > 2)
            {
                roll -= 2;
            }
            if (LandOrWater == LandWater.Water)
            {
                roll += 1;
            }
            if (WaterHabitat == WaterHabitats.Banks || WaterHabitat == WaterHabitats.OpenOcean)
            {
                roll += 1;
            }
            if (WaterHabitat == WaterHabitats.TropicalLagoon || WaterHabitat == WaterHabitats.RiverStream)
            {
                roll -= 1;
            }
            if (LandHabitat == LandHabitats.Plains)
            {
                roll += 1;
            }
            if (LandHabitat == LandHabitats.IslandBeach || LandHabitat == LandHabitats.Desert || LandHabitat == LandHabitats.Mountain)
            {
                roll -= 1;
            }
            if (TrophicDiet == TrophicDiets.GrazingHerbivore)
            {
                roll += 1;
            }
            if (TrophicDiet == TrophicDiets.ParasiteSymbiont)
            {
                roll -= 4;
            }
            if (PrimaryLocomotion == Locomotions.Slithering || SecondaryLocomotion == Locomotions.Slithering)
            {
                roll -= 1;
            }
            if (PrimaryLocomotion == Locomotions.Wingedflight || SecondaryLocomotion == Locomotions.Wingedflight)
            {
                roll -= 3;
            }

            /////////////////////////
            if (roll <= 2)
            {
                sizeclass = SizeClasses.Small;
            }
            if (roll == 3 || roll == 4)
            {
                sizeclass = SizeClasses.HumanScale;
            }
            if (roll >= 5)
            {
                sizeclass = SizeClasses.Large;
            }
            return sizeclass;
        }

        private double DetermineSize()
        {
            var size = 0.00;
            var dice = new Dice();
            var roll = dice.Rng(6);
            if (SizeClass == SizeClasses.Small)
            {
                switch (roll)
                {
                    case 1:
                        size = 0.05;
                        break;
                    case 2:
                        size = 0.07;
                        break;
                    case 3:
                        size = 0.10;
                        break;
                    case 4:
                        size = 0.15;
                        break;
                    case 5:
                        size = 0.20;
                        break;
                    case 6:
                        size = 0.30;
                        break;
                }
            }
            if (SizeClass == SizeClasses.HumanScale)
            {
                switch (roll)
                {
                    case 1:
                        size = 0.50;
                        break;
                    case 2:
                        size = 0.70;
                        break;
                    case 3:
                        size = 1.00;
                        break;
                    case 4:
                        size = 1.50;
                        break;
                    case 5:
                        size = 2.00;
                        break;
                    case 6:
                        size = 3.00;
                        break;
                }
            }
            if (SizeClass == SizeClasses.Large)
            {
                switch (roll)
                {
                    case 1:
                        size = 5.00;
                        break;
                    case 2:
                        size = 7.00;
                        break;
                    case 3:
                        size = 10.00;
                        break;
                    case 4:
                        size = 15.00;
                        break;
                    case 5:
                        size = 20.00;
                        break;
                    case 6:
                        size = 50.00;
                        break;
                }
            }
            if (Gravity >= 3.50)
            {
                size *= 0.30;
            }
            if (Gravity < 3.50 && Gravity >= 2.50)
            {
                size *= 0.40;
            }
            if (Gravity < 2.50 && Gravity >= 2.00)
            {
                size *= 0.50;
            }
            if (Gravity < 2.00 && Gravity >= 1.50)
            {
                size *= 0.60;
            }
            if (Gravity < 1.50 && Gravity >= 1.25)
            {
                size *= 0.75;
            }
            if (Gravity < 1.25 && Gravity >= 1.00)
            {
                size *= 0.90;
            }
            if (Gravity < 1.00 && Gravity >= 0.90)
            {
                size *= 1.00;
            }
            if (Gravity < 0.90 && Gravity >= 0.80)
            {
                size *= 1.10;
            }
            if (Gravity < 0.80 && Gravity >= 0.70)
            {
                size *= 1.20;
            }
            if (Gravity < 0.70 && Gravity >= 0.60)
            {
                size *= 1.30;
            }
            if (Gravity < 0.60 && Gravity >= 0.50)
            {
                size *= 1.40;
            }
            if (Gravity < 0.50 && Gravity >= 0.40)
            {
                size *= 1.60;
            }
            if (Gravity < 0.40 && Gravity >= 0.30)
            {
                size *= 1.80;
            }
            if (Gravity < 0.30 && Gravity >= 0.20)
            {
                size *= 2.20;
            }
            if (Gravity < 0.20 && Gravity >= 0.10)
            {
                size *= 2.90;
            }
            if (Gravity >= 0.10)
            {
                size *= 4.60;
            }
            return size;
        }

        private Symmetries DetermineSymmetry()
        {
            var symmetry = Symmetries.None;
            var dice = new Dice();
            var roll = dice.Rng(2, 6);

            if (PrimaryLocomotion == Locomotions.Immobile || SecondaryLocomotion == Locomotions.Immobile)
            {
                roll += 1;
            }

            if (PrimaryLocomotion == Locomotions.Floating || SecondaryLocomotion == Locomotions.Floating)
            {
                roll += 1;
            }

            switch (roll)
            {
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    symmetry = Symmetries.Bilateral;
                    break;
                case 8:
                    symmetry = Symmetries.Trilateral;
                    break;
                case 9:
                    symmetry = Symmetries.Radial;
                    break;
                case 10:
                    symmetry = Symmetries.Spherical;
                    break;
                case 11:
                case 12:
                case 13:
                case 14:
                    symmetry = Symmetries.Asymmetric;
                    break;
            }
            return symmetry;
        }

        private int DetermineSides()
        {
            var sides = 0;
            var dice = new Dice();
            var roll = dice.Rng(6);
            if (Symmetry == Symmetries.Bilateral)
            {
                sides = 2;
            }
            if (Symmetry == Symmetries.Trilateral)
            {
                sides = 3;
            }
            if (Symmetry == Symmetries.Radial)
            {
                sides = roll + 3;
            }
            if (Symmetry == Symmetries.Spherical)
            {
                switch (roll)
                {
                    case 1:
                        sides = 4;
                        break;
                    case 2:
                    case 3:
                        sides = 6;
                        break;
                    case 4:
                        sides = 8;
                        break;
                    case 5:
                        sides = 12;
                        break;
                    case 6:
                        sides = 20;
                        break;
                }
            }
            return sides;
        }

        private int DetermineLimbSegments()
        {
            var limbseg = 0;
            var dice = new Dice();
            var roll = dice.Rng(6);
            if (Symmetry == Symmetries.Trilateral)
            {
                roll -= 1;
            }
            if (Symmetry == Symmetries.Radial)
            {
                roll -= 2;
            }
            if (Symmetry == Symmetries.Spherical)
            {
                return 1;
            }
            if (Symmetry == Symmetries.Asymmetric)
            {
                var d1 = new Dice();
                return d1.Rng(2, 6) - 2;
            }
            ///////////
            if (roll <= 1)
            {
                return 0;
            }
            switch (roll)
            {
                case 2:
                    limbseg = 1;
                    break;
                case 3:
                    limbseg = 2;
                    break;
                case 4:
                    var d2 = new Dice();
                    limbseg = d2.Rng(6);
                    break;
                case 5:
                    var d3 = new Dice();
                    limbseg = d3.Rng(2, 6);
                    break;
                case 6:
                    var d4 = new Dice();
                    limbseg = d4.Rng(3, 6);
                    break;
            }

            return limbseg;
        }

        private Tails DetermineTail()
        {
            var tail = Tails.None;
            var taildice = new Dice();
            var roll = taildice.Rng(6);
            if (Symmetry == Symmetries.Spherical)
            {
                return Tails.None;
            }
            if (PrimaryLocomotion == Locomotions.Swimming || SecondaryLocomotion == Locomotions.Swimming)
            {
                roll += 1;
            }
            if (roll >= 5)
            {
                var dice = new Dice();
                roll = dice.Rng(2, 6);
                switch (roll)
                {
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        tail = Tails.FeatureLess;
                        //TODO: Add 0-Point Advantage/Feature.(Has no Gameplay Effect)
                        break;
                    case 6:
                        tail = Tails.Striker;
                        break;
                    case 7:
                        tail = Tails.Long;
                        //TODO: Long enhancement
                        break;
                    case 8:
                        tail = Tails.Constricting;
                        break;
                    case 9:
                        tail = Tails.Barbed;
                        break;
                    case 10:
                        tail = Tails.Gripping;
                        break;
                    case 11:
                        tail = Tails.Branching;
                        break;
                    case 12:
                        tail = Tails.Combination;
                        break;
                }
            }
            else
            {
                tail = Tails.None;
            }
            return tail;
        }

        private int DetermineManipulators()
        {
            var manipulators = 0;
            var dice = new Dice();
            var roll = dice.Rng(2, 6);
            if (LimbSegments == 1)
            {
                roll -= 1;
            }
            if (LimbSegments > 4 && LimbSegments <= 6)
            {
                roll += 1;
            }
            if (LimbSegments > 6)
            {
                roll += 2;
            }
            if (PrimaryLocomotion == Locomotions.Wingedflight || SecondaryLocomotion == Locomotions.Wingedflight)
            {
                roll -= 1;
            }
            if (PrimaryLocomotion == Locomotions.Swimming && WaterHabitat == WaterHabitats.OpenOcean)
            {
                roll -= 2;
            }
            if (TrophicDiet == TrophicDiets.GatheringHerbivore)
            {
                roll += 1;
            }

            if (roll <= 6)
            {
                manipulators = 0;
            }

            switch (roll)
            {
                case 7:
                case 8:
                case 9:
                    manipulators = 1;
                    break;
                case 10:
                    manipulators = 2;
                    break;
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                    var d1 = new Dice();
                    manipulators = d1.Rng(6);
                    break;
            }
            return manipulators;
        }

        private Skeletons DetermineSkeleton()
        {
            var skeleton = Skeletons.None;
            var dice = new Dice();
            var roll = dice.Rng(2, 6);
            if (SizeClass == SizeClasses.HumanScale)
            {
                roll += 1;
            }
            if (SizeClass == SizeClasses.Large)
            {
                roll += 2;
            }
            if (LandOrWater == LandWater.Land)
            {
                roll += 1;
            }
            if (PrimaryLocomotion == Locomotions.Slithering || SecondaryLocomotion == Locomotions.Slithering)
            {
                roll -= 1;
            }
            if (PrimaryLocomotion == Locomotions.Immobile || SecondaryLocomotion == Locomotions.Immobile)
            {
                roll -= 1;
            }
            if (Symmetry == Symmetries.Asymmetric)
            {
                roll -= 1;
            }
            if (Gravity <= 0.50)
            {
                roll -= 1;
            }
            if (Gravity >= 1.25)
            {
                roll += 1;
            }

            if (roll <= 3)
            {
                skeleton = Skeletons.None;
            }

            switch (roll)
            {
                case 4:
                case 5:
                    skeleton = Skeletons.Hydrostatic;
                    break;
                case 6:
                case 7:
                    skeleton = Skeletons.External;
                    break;
                case 8:
                case 9:
                case 10:
                    skeleton = Skeletons.Internal;
                    break;
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    skeleton = Skeletons.Combination;
                    break;
            }
            return skeleton;
        }

        private SkinClasses DetermineSkinClass()
        {
            var skinclass = SkinClasses.None;
            var dice = new Dice();
            var roll = dice.Rng(6);
            if (Skeleton == Skeletons.External)
            {
                return SkinClasses.Exeskeleton;
            }
            switch (roll)
            {
                case 1:
                case 2:
                    skinclass = SkinClasses.Skin;
                    break;
                case 3:
                    skinclass = SkinClasses.Scales;
                    break;
                case 4:
                    skinclass = SkinClasses.Fur;
                    break;
                case 5:
                    skinclass = SkinClasses.Feathers;
                    break;
                case 6:
                    skinclass = SkinClasses.Exeskeleton;
                    break;
            }
            return skinclass;
        }

        private Skins DetermineSkin()
        {
            var skin = Skins.None;
            if (SkinClass == SkinClasses.Skin)
            {
                var dice = new Dice();
                var roll = dice.Rng(2, 6);
                if (LandHabitat == LandHabitats.Arctic)
                {
                    roll += 1;
                }
                if (LandOrWater == LandWater.Water)
                {
                    roll += 1;
                }
                if (LandHabitat == LandHabitats.Desert)
                {
                    roll -= 1;
                }
                if (TrophicDiet == TrophicDiets.GatheringHerbivore || TrophicDiet == TrophicDiets.GrazingHerbivore)
                {
                    roll += 1;
                }
                if (PrimaryLocomotion == Locomotions.Wingedflight || SecondaryLocomotion == Locomotions.Wingedflight)
                {
                    roll -= 5;
                }

                if (roll <= 4)
                {
                    skin = Skins.SoftSkin;
                }
                switch (roll)
                {
                    case 5:
                        skin = Skins.NormalSkin;
                        break;
                    case 6:
                    case 7:
                        skin = Skins.Hide;
                        break;
                    case 8:
                        skin = Skins.ThickHide;
                        break;
                    case 9:
                        skin = Skins.ArmorShell;
                        break;
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                        skin = Skins.Blubber;
                        //TODO: Add Temperature Tolerance
                        break;
                }
            }
            if (SkinClass == SkinClasses.Scales)
            {
                var dice = new Dice();
                var roll = dice.Rng(2, 6);
                if (LandHabitat == LandHabitats.Desert)
                {
                    roll += 1;
                }
                if (TrophicDiet == TrophicDiets.GatheringHerbivore || TrophicDiet == TrophicDiets.GrazingHerbivore)
                {
                    roll += 1;
                }
                if (PrimaryLocomotion == Locomotions.Wingedflight || SecondaryLocomotion == Locomotions.Wingedflight)
                {
                    roll -= 2;
                }
                if (PrimaryLocomotion == Locomotions.Digging || SecondaryLocomotion == Locomotions.Digging)
                {
                    roll -= 1;
                }
                if (roll <= 3)
                {
                    skin = Skins.NormalSkin;
                }

                switch (roll)
                {
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        skin = Skins.Scales;
                        break;
                    case 9:
                    case 10:
                        skin = Skins.HeavyScales;
                        break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                        skin = Skins.ArmorShell;
                        break;
                }
            }
            if (SkinClass == SkinClasses.Fur)
            {
                var dice = new Dice();
                var roll = dice.Rng(2, 6);
                if (LandHabitat == LandHabitats.Desert)
                {
                    roll -= 1;
                }
                if (LandHabitat == LandHabitats.Arctic)
                {
                    roll += 1;
                }
                if (PrimaryLocomotion == Locomotions.Wingedflight || SecondaryLocomotion == Locomotions.Wingedflight)
                {
                    roll -= 1;
                }
                if (TrophicDiet == TrophicDiets.GatheringHerbivore || TrophicDiet == TrophicDiets.GrazingHerbivore)
                {
                    roll += 1;
                }

                if (roll <= 5)
                {
                    skin = Skins.NormalSkin;
                }

                switch (roll)
                {
                    case 6:
                    case 7:
                        skin = Skins.Fur;
                        break;
                    case 8:
                    case 9:
                        skin = Skins.ThickFur;
                        //TODO: Add Temperature Tolerance
                        break;
                    case 10:
                    case 11:
                        skin = Skins.ThickFurOverHide;
                        break;
                    case 12:
                    case 13:
                    case 14:
                        skin = Skins.Spines;
                        break;
                }
            }
            if (SkinClass == SkinClasses.Feathers)
            {
                var dice = new Dice();
                var roll = dice.Rng(2, 6);
                if (LandHabitat == LandHabitats.Desert)
                {
                    roll -= 1;
                }
                if (LandHabitat == LandHabitats.Arctic)
                {
                    roll += 1;
                }
                if (PrimaryLocomotion == Locomotions.Wingedflight || SecondaryLocomotion == Locomotions.Wingedflight)
                {
                    roll += 1;
                }

                switch (roll)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        skin = Skins.NormalSkin;
                        break;
                    case 6:
                    case 7:
                    case 8:
                        skin = Skins.Feathers;
                        break;
                    case 9:
                    case 10:
                        skin = Skins.ThickFeather;
                        //TODO: Add Temperature Tolerance
                        break;
                    case 11:
                        skin = Skins.FeathersOverHide;
                        //TODO: Add Temperature Tolerance
                        break;
                    case 12:
                    case 13:
                    case 14:
                        skin = Skins.Spines;
                        break;
                }
            }

            if (SkinClass == SkinClasses.Exeskeleton)
            {
                var dice = new Dice();
                var roll = dice.Rng(6);

                if (LandOrWater == LandWater.Water)
                {
                    roll += 1;
                }
                if (PrimaryLocomotion == Locomotions.Immobile || SecondaryLocomotion == Locomotions.Immobile)
                {
                    roll += 1;
                }
                if (PrimaryLocomotion == Locomotions.Wingedflight || SecondaryLocomotion == Locomotions.Wingedflight)
                {
                    roll -= 2;
                }

                if (roll <= 2)
                {
                    skin = Skins.LightExoskeleton;
                }
                switch (roll)
                {
                    case 3:
                    case 4:
                        skin = Skins.ToughExoskeleton;
                        break;
                    case 5:
                        skin = Skins.HeavyExoskeleton;
                        break;
                    case 6:
                    case 7:
                    case 8:
                        skin = Skins.HeavyExoskeleton;
                        break;
                }
            }
            return skin;
        }

        private BreathingMethods DetermineBreathing()
        {
            var breathing = BreathingMethods.None;
            var dice = new Dice();
            var roll = dice.Rng(2, 6);
            if (LandOrWater == LandWater.Land)
            {
                return BreathingMethods.Lungs;
            }
            if (PrimaryLocomotion == Locomotions.Wingedflight || SecondaryLocomotion == Locomotions.Wingedflight)
            {
                return BreathingMethods.Lungs;
            }
            if (WaterHabitat == WaterHabitats.DeepOceanVents)
            {
                return BreathingMethods.Gills;
            }

            switch (roll)
            {
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    breathing = BreathingMethods.Gills;
                    break;
                case 7:
                case 8:
                    breathing = BreathingMethods.LungsOxygenStorage;
                    break;
                case 9:
                case 10:
                    breathing = BreathingMethods.GillsAndLungs;
                    break;
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    breathing = BreathingMethods.Lungs;
                    break;
            }
            return breathing;
        }

        private Temperatures DetermineTemperature()
        {
            var temperature = Temperatures.None;
            var dice = new Dice();
            var roll = dice.Rng(2, 6);

            if (Breathing == BreathingMethods.Lungs)
            {
                roll += 1;
            }
            if (Breathing == BreathingMethods.Gills)
            {
                roll -= 1;
            }
            if (SizeClass == SizeClasses.HumanScale || SizeClass == SizeClasses.Large)
            {
                roll += 1;
            }
            if (LandOrWater == LandWater.Land)
            {
                roll += 1;
            }
            if (LandHabitat == LandHabitats.Woodlands || LandHabitat == LandHabitats.Mountain)
            {
                roll += 1;
            }
            if (LandHabitat == LandHabitats.Arctic)
            {
                roll += 2;
            }

            switch (roll)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    temperature = Temperatures.ColdBloodedwithDis;
                    //TODO: Add Cold-Blooded Disadvantage
                    break;
                case 5:
                case 6:
                    temperature = Temperatures.ColdBlooded;
                    break;
                case 7:
                    temperature = Temperatures.PartialRegulations;
                    break;
                case 8:
                case 9:
                    temperature = Temperatures.WarmBlooded;
                    break;
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                    temperature = Temperatures.WarmBloodedMetabolism;
                    //TODO: Add Metabolism Control
                    break;
            }
            return temperature;
        }

        private GrowthRates DetermineGrowth()
        {
            var growth = GrowthRates.None;
            var dice = new Dice();
            var roll = dice.Rng(2, 6);
            if (Skeleton == Skeletons.External)
            {
                roll -= 1;
            }
            if (SizeClass == SizeClasses.Large)
            {
                roll += 1;
            }
            if (PrimaryLocomotion == Locomotions.Immobile || SecondaryLocomotion == Locomotions.Immobile)
            {
                roll += 1;
            }

            switch (roll)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    growth = GrowthRates.Metamorphosis;
                    break;
                case 5:
                case 6:
                    growth = GrowthRates.Molting;
                    break;
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                    growth = GrowthRates.ContinuousGrowth;
                    break;
                case 12:
                case 13:
                case 14:
                    growth = GrowthRates.UnusualGrowthPattern;
                    break;
            }
            return growth;
        }

        private Sexes DetermineSexes()
        {
            var sexes = Sexes.None;
            var dice = new Dice();
            var roll = dice.Rng(2, 6);
            if (PrimaryLocomotion == Locomotions.Immobile || SecondaryLocomotion == Locomotions.Immobile)
            {
                roll -= 1;
            }
            if (Symmetry == Symmetries.Asymmetric)
            {
                roll -= 1;
            }
            if (TrophicDiet == TrophicDiets.AutothrophPhotosynthetic || TrophicDiet == TrophicDiets.AutothrophChemosynthetic || TrophicDiet == TrophicDiets.AutothrophOther)
            {
                roll -= 1;
            }

            if (roll <= 4)
            {
                sexes = Sexes.Asexual;
            }

            switch (roll)
            {
                case 5:
                case 6:
                    sexes = Sexes.Hermaphrodite;
                    break;
                case 7:
                case 8:
                case 9:
                    sexes = Sexes.TwoSexes;
                    break;
                case 10:
                    sexes = Sexes.Switching;
                    break;
                case 11:
                    sexes = Sexes.ThreeOrMore;
                    break;
                case 12:
                    sexes = Sexes.Alternating;
                    break;
            }
            return sexes;
        }

        private Gestations DetermineGestation()
        {
            var gestation = Gestations.None;
            var dice = new Dice();
            var roll = dice.Rng(2, 6);
            if (LandOrWater == LandWater.Land)
            {
                roll -= 1;
            }
            if (PrimaryLocomotion == Locomotions.Immobile || SecondaryLocomotion == Locomotions.Immobile)
            {
                roll -= 2;
            }
            if (Temperture == Temperatures.WarmBlooded)
            {
                roll += 1;
            }

            if (roll <= 6)
            {
                gestation = Gestations.SpawningPollinating;
            }

            switch (roll)
            {
                case 7:
                case 8:
                    gestation = Gestations.EggLaying;
                    break;
                case 9:
                case 10:
                    gestation = Gestations.LiveBearing;
                    break;
                case 11:
                case 12:
                case 13:
                    gestation = Gestations.LiveBearingWithPouch;
                    break;
            }

            var specialDice = new Dice();
            roll = specialDice.Rng(2, 6);
            if (roll == 12)
            {
                var d1 = new Dice();
                roll = d1.Rng(6);
                switch (roll)
                {
                    case 1:
                        gestation = Gestations.BroodParasite;
                        break;
                    case 2:
                    case 3:
                        gestation = Gestations.ParasiticYoung;
                        break;
                    case 4:
                    case 5:
                        gestation = Gestations.CannibalisticEatParent;
                        break;
                    case 6:
                        gestation = Gestations.CannibalisticEatEachOther;
                        break;
                }
            }
            return gestation;
        }

        private Strategies DetermineStrategy()
        {
            var strategy = Strategies.None;
            var dice = new Dice();
            var roll = dice.Rng(2, 6);

            if (SizeClass == SizeClasses.Large)
            {
                roll -= 2;
            }
            if (SizeClass == SizeClasses.Small)
            {
                roll += 1;
            }
            if (Gestation == Gestations.SpawningPollinating)
            {
                roll += 2;
            }

            if (roll <= 4)
            {
                strategy = Strategies.StrongK;
                OffspringCount = 1;
            }

            switch (roll)
            {
                case 5:
                case 6:
                    strategy = Strategies.ModerateK;
                    OffspringCount = 1;
                    break;
                case 7:
                    var d2 = new Dice();
                    var spawn = d2.Rng(6);
                    strategy = Strategies.MedianStrategy;
                    OffspringCount = spawn;
                    break;
                case 8:
                case 9:
                    var d3 = new Dice();
                    var spawn3 = d3.Rng(6) + 1;
                    strategy = Strategies.ModerateR;
                    OffspringCount = spawn3;
                    break;
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    var d4 = new Dice();
                    var spawn4 = d4.Rng(2, 6);
                    strategy = Strategies.StrongR;
                    OffspringCount = spawn4;
                    break;
            }

            if (Gestation == Gestations.SpawningPollinating)
            {
                var d1 = new Dice();
                var spawn = d1.Rng(2, 6) * 10 * roll;
                OffspringCount = spawn;
            }
            return strategy;
        }

        private PrimarySenses DeterminePrimarySense()
        {
            var primarysense = PrimarySenses.None;
            var dice = new Dice();
            var roll = dice.Rng(3, 6);

            if (LandOrWater == LandWater.Water)
            {
                roll -= 2;
            }

            if (TrophicDiet == TrophicDiets.AutothrophPhotosynthetic || TrophicDiet == TrophicDiets.AutothrophChemosynthetic || TrophicDiet == TrophicDiets.AutothrophOther)
            {
                roll += 2;
            }

            if (roll >= 1 && roll <= 7)
            {
                primarysense = PrimarySenses.Hearing;
            }
            if (roll >= 8 && roll <= 12)
            {
                primarysense = PrimarySenses.Vision;
            }
            if (roll >= 13 && roll <= 20)
            {
                primarysense = PrimarySenses.TouchTaste;
            }
            return primarysense;
        }

        private Visions DetermineVision()
        {
            var vision = Visions.None;
            var dice = new Dice();
            var roll = dice.GurpsRoll();
            if (PrimarySense == PrimarySenses.Vision)
            {
                roll += 4;
            }
            if (PrimaryLocomotion == Locomotions.Digging)
            {
                roll -= 4;
            }
            if (PrimaryLocomotion == Locomotions.Climbing || SecondaryLocomotion == Locomotions.Climbing)
            {
                roll += 2;
            }
            if (PrimaryLocomotion == Locomotions.Wingedflight || SecondaryLocomotion == Locomotions.Wingedflight)
            {
                roll += 3;
            }
            if (PrimaryLocomotion == Locomotions.Immobile || SecondaryLocomotion == Locomotions.Immobile)
            {
                roll -= 4;
            }
            if (WaterHabitat == WaterHabitats.DeepOceanVents)
            {
                roll -= 4;
            }
            if (TrophicDiet == TrophicDiets.FilterFeeder)
            {
                roll -= 2;
            }
            if (TrophicDiet == TrophicDiets.ChasingCarnivore || TrophicDiet == TrophicDiets.HijackingCarnivore || TrophicDiet == TrophicDiets.TrappingCarnivore || TrophicDiet == TrophicDiets.PouncingCarnivore ||
                TrophicDiet == TrophicDiets.GatheringHerbivore)
            {
                roll += 2;
            }

            if (roll <= 6)
            {
                vision = Visions.Blindness;
            }
            switch (roll)
            {
                case 7:
                    vision = Visions.BlindnessSoft;
                    break;
                case 8:
                case 9:
                    vision = Visions.BadSightColorblind;
                    break;
                case 10:
                case 11:
                    vision = Visions.BadSight;
                    break;
                case 12:
                case 13:
                case 14:
                    vision = Visions.Normal;
                    break;
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                    vision = Visions.TelescopicVision;
                    break;
            }
            return vision;
        }

        private Hearings DetermineHearing()
        {
            var hearing = Hearings.None;
            var dice = new Dice();
            var roll = dice.GurpsRoll();
            if (PrimarySense == PrimarySenses.Hearing)
            {
                roll += 4;
            }
            if (Vision == Visions.Blindness || Vision == Visions.BlindnessSoft)
            {
                roll += 2;
            }
            if (Vision == Visions.BadSight || Vision == Visions.BadSightColorblind)
            {
                roll += 1;
            }
            if (LandOrWater == LandWater.Water)
            {
                roll += 1;
            }
            if (PrimaryLocomotion == Locomotions.Immobile || SecondaryLocomotion == Locomotions.Immobile)
            {
                roll -= 4;
            }
            if (roll <= 6)
            {
                hearing = Hearings.Deafness;
            }

            switch (roll)
            {
                case 7:
                case 8:
                    hearing = Hearings.HardOfhearing;
                    break;
                case 9:
                case 10:
                    hearing = Hearings.Normal;
                    break;
                case 11:
                    hearing = Hearings.NormalPlusRange;
                    break;
                case 12:
                    hearing = Hearings.Acute;
                    break;
                case 13:
                    hearing = Hearings.AcutePlusSubSonic;
                    break;
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                    hearing = Hearings.AcutePlusSubSonicAndSonar;
                    break;
            }
            return hearing;
        }

        private Touches DetermineTouch()
        {
            var touch = Touches.None;
            var dice = new Dice();
            var roll = dice.Rng(2, 6);
            if (PrimarySense == PrimarySenses.TouchTaste)
            {
                roll += 4;
            }
            if (Skeleton == Skeletons.External)
            {
                roll -= 2;
            }
            if (LandOrWater == LandWater.Water)
            {
                roll += 2;
            }
            if (PrimaryLocomotion == Locomotions.Digging || SecondaryLocomotion == Locomotions.Digging)
            {
                roll += 2;
            }
            if (PrimaryLocomotion == Locomotions.Wingedflight || SecondaryLocomotion == Locomotions.Wingedflight)
            {
                roll -= 2;
            }
            if (Vision == Visions.Blindness || Vision == Visions.BlindnessSoft)
            {
                roll += 2;
            }
            if (TrophicDiet == TrophicDiets.TrappingCarnivore)
            {
                roll += 1;
            }
            if (SizeClass == SizeClasses.Small)
            {
                roll += 1;
            }
            if (roll <= 2)
            {
                touch = Touches.Numb;
            }
            switch (roll)
            {
                case 3:
                case 4:
                    touch = Touches.Minus2PoorSense;
                    break;
                case 5:
                case 6:
                    touch = Touches.Minus1PoorSense;
                    break;
                case 7:
                case 8:
                    touch = Touches.Normal;
                    break;
                case 9:
                case 10:
                    touch = Touches.Acute;
                    break;
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                    touch = Touches.AcutePlusSensitive;
                    break;
            }
            return touch;
        }

        private TasteSmells DetermineTasteSmell()
        {
            var tastesmell = TasteSmells.None;
            var dice = new Dice();
            var roll = dice.Rng(2, 6) + 2;
            if (PrimarySense == PrimarySenses.TouchTaste)
            {
                roll += 4;
            }
            if (TrophicDiet == TrophicDiets.ChasingCarnivore || TrophicDiet == TrophicDiets.GatheringHerbivore)
            {
                roll += 2;
            }
            if (TrophicDiet == TrophicDiets.FilterFeeder || TrophicDiet == TrophicDiets.TrappingCarnivore || TrophicDiet == TrophicDiets.AutothrophPhotosynthetic || TrophicDiet == TrophicDiets.AutothrophChemosynthetic ||
                TrophicDiet == TrophicDiets.AutothrophOther)
            {
                roll -= 2;
            }
            if (PrimaryLocomotion == Locomotions.Immobile || SecondaryLocomotion == Locomotions.Immobile)
            {
                roll -= 4;
            }
            if (roll <= 3)
            {
                tastesmell = TasteSmells.NoTasteSmell;
            }
            switch (roll)
            {
                case 4:
                case 5:
                    tastesmell = TasteSmells.NoSmell;
                    break;
                case 6:
                case 7:
                case 8:
                    tastesmell = TasteSmells.Normal;
                    break;
                case 9:
                case 10:
                    tastesmell = TasteSmells.Acute;
                    break;
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    tastesmell = TasteSmells.AcutePlusDisciminatory;
                    break;
            }
            return tastesmell;
        }

        private List<SpecialSenses> DetermineSpecialSenses()
        {
            var specialsenses = new List<SpecialSenses>();
            var dice = new Dice();
            var roll = dice.Rng(2, 6);
            //TODO: Add All the Advantages
            //360 Vision
            if (LandHabitat == LandHabitats.Plains || LandHabitat == LandHabitats.Desert)
            {
                roll += 1;
            }
            if (Symmetry == Symmetries.Radial || Symmetry == Symmetries.Spherical)
            {
                roll += 1;
            }
            if (roll >= 11)
            {
                specialsenses.Add(SpecialSenses.ThreeSixtyVision);
            }

            //Absolute Direction
            roll = dice.Rng(2, 6);
            if (WaterHabitat == WaterHabitats.OpenOcean)
            {
                roll += 1;
            }
            if (PrimaryLocomotion == Locomotions.Wingedflight || SecondaryLocomotion == Locomotions.Wingedflight)
            {
                roll += 1;
            }
            if (PrimaryLocomotion == Locomotions.Digging || SecondaryLocomotion == Locomotions.Digging)
            {
                roll += 1;
            }
            if (roll >= 11)
            {
                specialsenses.Add(SpecialSenses.AbsoluteDirection);
            }

            //Discriminatory Hearing
            roll = dice.Rng(2, 6);
            if (Hearing == Hearings.AcutePlusSubSonicAndSonar)
            {
                roll += 2;
            }
            if (roll >= 11)
            {
                specialsenses.Add(SpecialSenses.DisciminatoryHearing);
            }

            //Peripheral Vision
            roll = dice.Rng(2, 6);
            if (LandHabitat == LandHabitats.Plains || LandHabitat == LandHabitats.Desert)
            {
                roll += 1;
            }
            if (TrophicDiet == TrophicDiets.GatheringHerbivore || TrophicDiet == TrophicDiets.GrazingHerbivore)
            {
                roll += 2;
            }
            if (roll >= 10 && roll <= 12)
            {
                specialsenses.Add(SpecialSenses.PeripheralVision);
            }

            //Night Vision
            roll = dice.Rng(2, 6);
            if (LandOrWater == LandWater.Water)
            {
                roll += 2;
            }
            if (TrophicDiet == TrophicDiets.PouncingCarnivore || TrophicDiet == TrophicDiets.ChasingCarnivore || TrophicDiet == TrophicDiets.TrappingCarnivore || TrophicDiet == TrophicDiets.HijackingCarnivore)
            {
                roll += 2;
            }
            if (roll >= 11)
            {
                specialsenses.Add(SpecialSenses.NightVision);
            }

            //Ultravision
            roll = dice.Rng(2, 6);
            if (roll >= 11 && LandOrWater != LandWater.Water && ChemicalBase != ChemicalBasis.Ammonia)
            {
                specialsenses.Add(SpecialSenses.UltraVision);
            }

            //Detect (Heat)
            roll = dice.Rng(2, 6);
            if (TrophicDiet == TrophicDiets.PouncingCarnivore || TrophicDiet == TrophicDiets.ChasingCarnivore || TrophicDiet == TrophicDiets.TrappingCarnivore || TrophicDiet == TrophicDiets.HijackingCarnivore)
            {
                roll += 1;
            }
            if (LandHabitat == LandHabitats.Arctic)
            {
                roll += 1;
            }
            if (roll >= 11 && LandOrWater != LandWater.Water)
            {
                specialsenses.Add(SpecialSenses.DetectHeat);
            }

            //Detect (Electric Fields)
            roll = dice.Rng(2, 6);
            if (TrophicDiet == TrophicDiets.PouncingCarnivore || TrophicDiet == TrophicDiets.ChasingCarnivore || TrophicDiet == TrophicDiets.TrappingCarnivore || TrophicDiet == TrophicDiets.HijackingCarnivore)
            {
                roll += 1;
            }
            if (roll >= 11 && LandOrWater != LandWater.Land)
            {
                specialsenses.Add(SpecialSenses.DetectElectricFields);
            }

            //Perfect Balance
            roll = dice.Rng(2, 6);
            if (PrimaryLocomotion == Locomotions.Climbing || SecondaryLocomotion == Locomotions.Climbing)
            {
                roll += 2;
            }
            if (LandHabitat == LandHabitats.Mountain)
            {
                roll += 1;
            }
            if (Gravity <= 0.50)
            {
                roll -= 1;
            }
            if (Gravity >= 1.50)
            {
                roll += 1;
            }
            if (roll >= 11 && LandOrWater == LandWater.Land)
            {
                specialsenses.Add(SpecialSenses.PerfectBalance);
            }
            //Scanning Sense (Radar)
            roll = dice.Rng(2, 6);
            if (roll >= 11 && SizeClass != SizeClasses.Small && LandOrWater != LandWater.Water)
            {
                specialsenses.Add(SpecialSenses.ScanningSense);
            }
            return specialsenses;
        }

        private Intelligences DetermineIntelligence( bool issapient )
        {
            var intelligence = Intelligences.None;
            if (!issapient)
            {
                var dice = new Dice();
                var roll = dice.Rng(2, 6);
                if (TrophicDiet == TrophicDiets.AutothrophPhotosynthetic || TrophicDiet == TrophicDiets.AutothrophChemosynthetic || TrophicDiet == TrophicDiets.AutothrophOther || TrophicDiet == TrophicDiets.FilterFeeder ||
                    TrophicDiet == TrophicDiets.GrazingHerbivore)
                {
                    roll -= 1;
                }
                if (TrophicDiet == TrophicDiets.GatheringHerbivore || TrophicDiet == TrophicDiets.Omnivore)
                {
                    roll += 1;
                }
                if (SizeClass == SizeClasses.Small)
                {
                    roll -= 1;
                }
                if (Strategy == Strategies.StrongR)
                {
                    roll -= 1;
                }
                if (Strategy == Strategies.StrongK)
                {
                    roll += 1;
                }
                if (roll <= 3)
                {
                    intelligence = Intelligences.Mindless;
                    IntelligenceValue = 0;
                }
                switch (roll)
                {
                    case 4:
                    case 5:
                        intelligence = Intelligences.Preprogrammed;
                        //TODO: Add Cannot Learn Disadvantage
                        IntelligenceValue = 1;
                        break;
                    case 6:
                    case 7:
                    case 8:
                        intelligence = Intelligences.LowIntelligence;
                        //TODO: Add Bestial Disadvantage
                        IntelligenceValue = 2;
                        break;
                    case 9:
                    case 10:
                        intelligence = Intelligences.HighIntelligence;
                        //TODO: Add Bestial Disadvantage
                        IntelligenceValue = 4;
                        break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                        intelligence = Intelligences.Presapient;
                        IntelligenceValue = 5;
                        break;
                }
            }
            else
            {
                var dice = new Dice();
                var roll = dice.Rng(6) + 5;
                if (TrophicDiet == TrophicDiets.AutothrophPhotosynthetic || TrophicDiet == TrophicDiets.AutothrophChemosynthetic || TrophicDiet == TrophicDiets.AutothrophOther || TrophicDiet == TrophicDiets.FilterFeeder ||
                    TrophicDiet == TrophicDiets.GrazingHerbivore)
                {
                    roll -= 1;
                }
                if (TrophicDiet == TrophicDiets.GatheringHerbivore || TrophicDiet == TrophicDiets.Omnivore)
                {
                    roll += 1;
                }
                if (SizeClass == SizeClasses.Small)
                {
                    roll -= 1;
                }
                if (Strategy == Strategies.StrongR)
                {
                    roll -= 1;
                }
                if (Strategy == Strategies.StrongK)
                {
                    roll += 1;
                }
                if (roll <= 5)
                {
                    roll = 6;
                }
                intelligence = Intelligences.Sapient;
                IntelligenceValue = roll;
            }
            return intelligence;
        }

        private MatingBehaviours DetermineMatingBehaviour()
        {
            var matingbehaviour = MatingBehaviours.None;
            var dice = new Dice();
            var roll = dice.Rng(2, 6);
            if (Sex == Sexes.Hermaphrodite)
            {
                roll -= 2;
            }
            if (Gestation == Gestations.SpawningPollinating)
            {
                roll -= 1;
            }
            if (Gestation == Gestations.LiveBearing || Gestation == Gestations.LiveBearingWithPouch)
            {
                roll += 1;
            }
            if (Strategy == Strategies.StrongR)
            {
                roll -= 1;
            }
            if (Strategy == Strategies.StrongK)
            {
                roll += 1;
            }

            if (roll <= 5)
            {
                matingbehaviour = MatingBehaviours.NoPair;
            }

            switch (roll)
            {
                case 6:
                case 7:
                    matingbehaviour = MatingBehaviours.TempPair;
                    break;
                case 8:
                    matingbehaviour = MatingBehaviours.PermanentPair;
                    break;
                case 9:
                case 10:
                    matingbehaviour = MatingBehaviours.Harem;
                    break;
                case 11:
                case 12:
                case 13:
                case 14:
                    matingbehaviour = MatingBehaviours.Hive;
                    break;
            }

            return matingbehaviour;
        }

        private SocialOrganizations DetermineSocialOrganization()
        {
            var socialorgs = SocialOrganizations.None;
            var dice = new Dice();
            var roll = dice.Rng(2, 6);
            if (TrophicDiet == TrophicDiets.ChasingCarnivore || TrophicDiet == TrophicDiets.HijackingCarnivore || TrophicDiet == TrophicDiets.PouncingCarnivore || TrophicDiet == TrophicDiets.TrappingCarnivore)
            {
                roll -= 1;
            }
            if (TrophicDiet == TrophicDiets.GrazingHerbivore)
            {
                roll += 1;
            }
            if (SizeClass == SizeClasses.Large)
            {
                roll -= 1;
            }
            if (MatingBahavior == MatingBehaviours.Harem)
            {
                roll += 1;
            }
            if (MatingBahavior == MatingBehaviours.NoPair)
            {
                roll -= 1;
            }
            if (roll <= 6)
            {
                socialorgs = SocialOrganizations.Solitary;
            }

            switch (roll)
            {
                case 7:
                case 8:
                    socialorgs = SocialOrganizations.PairBonded;
                    SocialGroupSize = 2;
                    break;
                case 9:
                case 10:
                    socialorgs = SocialOrganizations.SmallGroup;
                    SocialGroupSize = dice.Rng(2, 6);
                    break;
                case 11:
                    socialorgs = SocialOrganizations.MediumGroup;
                    SocialGroupSize = dice.Rng(4, 6);
                    break;
                case 12:
                case 13:
                case 14:
                    socialorgs = SocialOrganizations.LargeHerd;
                    SocialGroupSize = dice.Rng(10) * 10;
                    break;
            }
            return socialorgs;
        }

        private Concentrations DetermineConcentration()
        {
            var concentration = Concentrations.None;
            var dice = new Dice();
            var roll1 = dice.Rng(6);
            var roll2 = dice.Rng(6);
            var roll = roll1 - roll2;
            if (TrophicDiet == TrophicDiets.PouncingCarnivore || TrophicDiet == TrophicDiets.ChasingCarnivore)
            {
                roll += 1;
            }
            if (TrophicDiet == TrophicDiets.GrazingHerbivore || TrophicDiet == TrophicDiets.GatheringHerbivore)
            {
                roll -= 1;
            }
            if (Strategy == Strategies.StrongK)
            {
                roll += 1;
            }
            if (roll >= 3)
            {
                concentration = Concentrations.VerySingleMinded;
                //TODO: Add SingleMinded Advantage
                //TODO: Add High Pain Threshold Advantage
            }
            if (roll <= -3)
            {
                concentration = Concentrations.VeryShortAttentionSpan;
                //TODO: Add Short Attention Span disadvantage
            }
            switch (roll)
            {
                case 2:
                    concentration = Concentrations.SingleMinded;
                    //TODO: Add SingleMinded Advantage
                    break;
                case 1:
                    concentration = Concentrations.Attentive;
                    //TODO: Add Attentive Quirk
                    break;
                case 0:
                    concentration = Concentrations.Normal;
                    break;
                case -1:
                    concentration = Concentrations.Distractible;
                    //TODO: Add Distractible Quirk
                    break;
                case -2:
                    concentration = Concentrations.ShortAttentionSpan;
                    //TODO: Add Short Attention Span disadvantage
                    break;
            }
            return concentration;
        }

        private Curiosities DetermineCuriosity()
        {
            var curiosity = Curiosities.None;
            var dice = new Dice();
            var roll1 = dice.Rng(6);
            var roll2 = dice.Rng(6);
            var roll = roll1 - roll2;
            if (TrophicDiet == TrophicDiets.Omnivore)
            {
                roll += 1;
            }
            if (TrophicDiet == TrophicDiets.GrazingHerbivore || TrophicDiet == TrophicDiets.FilterFeeder)
            {
                roll -= 1;
            }
            if (Vision == Visions.Blindness || Vision == Visions.BlindnessSoft)
            {
                roll -= 1;
            }
            if (Strategy == Strategies.StrongR)
            {
                roll -= 1;
            }
            if (Strategy == Strategies.StrongK)
            {
                roll += 1;
            }
            if (roll >= 3)
            {
                curiosity = Curiosities.VeryCurious;
                //TODO: Add Curios Disadvantage
            }
            if (roll <= -3)
            {
                curiosity = Curiosities.VeryIncurious;
                //TODO: Add Incurious
            }
            switch (roll)
            {
                case 2:
                    curiosity = Curiosities.Curious;
                    //TODO: Add Curios Disadvantage
                    break;
                case 1:
                    curiosity = Curiosities.Nosy;
                    if (Concentration == Concentrations.Distractible || Concentration == Concentrations.ShortAttentionSpan || Concentration == Concentrations.VeryShortAttentionSpan)
                    {
                        //TODO: Add Curios Disadvatage
                    }
                    break;
                case 0:
                    curiosity = Curiosities.Normal;
                    break;
                case -1:
                    curiosity = Curiosities.Staid;
                    //TODO: Add Staid Quirk
                    break;
                case -2:
                    curiosity = Curiosities.Incurious;
                    //TODO: Add Incurious
                    break;
            }
            return curiosity;
        }

        private Egoisms DetermineEgoism()
        {
            var egoism = Egoisms.None;
            var dice = new Dice();
            var roll1 = dice.Rng(6);
            var roll2 = dice.Rng(6);
            var roll = roll1 - roll2;
            if (SocialOrganization == SocialOrganizations.Solitary)
            {
                roll += 1;
            }
            if (MatingBahavior == MatingBehaviours.Harem)
            {
                roll += 1;
            }
            if (MatingBahavior == MatingBehaviours.Hive)
            {
                roll -= 1;
            }
            if (Strategy == Strategies.StrongR)
            {
                roll -= 1;
            }
            if (Strategy == Strategies.StrongK)
            {
                roll += 1;
            }
            if (roll >= 3)
            {
                egoism = Egoisms.VerySelfish;
                //TODO: Add Selfish
            }
            if (roll <= -3)
            {
                egoism = Egoisms.VerySelfless;
                //TODO: Add Selfless
            }
            switch (roll)
            {
                case 2:
                    egoism = Egoisms.Selfish;
                    //TODO: Add Selfish
                    break;
                case 1:
                    egoism = Egoisms.Proud;
                    //TODO: Add Proud Quirk
                    break;
                case 0:
                    egoism = Egoisms.Normal;
                    break;
                case -1:
                    egoism = Egoisms.Humble;
                    //TODO: Add Humble Quirk
                    break;
                case -2:
                    egoism = Egoisms.Selfless;
                    //TODO: Add Selfless
                    break;
            }
            return egoism;
        }

        private Empathies DetermineEmpathy()
        {
            var empathy = Empathies.None;
            var dice = new Dice();
            var roll1 = dice.Rng(6);
            var roll2 = dice.Rng(6);
            var roll = roll1 - roll2;
            if (TrophicDiet == TrophicDiets.ChasingCarnivore)
            {
                roll += 1;
            }
            if (TrophicDiet == TrophicDiets.AutothrophPhotosynthetic || TrophicDiet == TrophicDiets.AutothrophChemosynthetic || TrophicDiet == TrophicDiets.AutothrophOther || TrophicDiet == TrophicDiets.FilterFeeder ||
                TrophicDiet == TrophicDiets.GrazingHerbivore || TrophicDiet == TrophicDiets.Scavenger)
            {
                roll -= 1;
            }
            if (SocialOrganization == SocialOrganizations.Solitary || SocialOrganization == SocialOrganizations.PairBonded)
            {
                roll -= 1;
            }
            if (SocialOrganization == SocialOrganizations.SmallGroup || SocialOrganization == SocialOrganizations.MediumGroup)
            {
                roll += 1;
            }
            if (Strategy == Strategies.StrongK)
            {
                roll += 1;
            }
            if (roll >= 3)
            {
                empathy = Empathies.VeryEmpathic;
                //TODO: Add Empathy
            }
            if (roll <= -3)
            {
                empathy = Empathies.LowEmpathy;
                //TODO: Add Low Empathy
                if (TrophicDiet == TrophicDiets.ChasingCarnivore || TrophicDiet == TrophicDiets.HijackingCarnivore || TrophicDiet == TrophicDiets.PouncingCarnivore || TrophicDiet == TrophicDiets.TrappingCarnivore)
                {
                    //TODO: Add bloodlust
                }
            }
            switch (roll)
            {
                case 2:
                    empathy = Empathies.Empathy;
                    //TODO: Add Empathy
                    break;
                case 1:
                    empathy = Empathies.Responsive;
                    //TODO: Add Responsive Quirk
                    break;
                case 0:
                    empathy = Empathies.Normal;
                    break;
                case -1:
                    empathy = Empathies.Oblivious;
                    break;
                case -2:
                    empathy = Empathies.Callous;
                    break;
            }
            return empathy;
        }

        private Gegariousnesses DetermineGegariousness()
        {
            var gregariousness = Gegariousnesses.None;
            var dice = new Dice();
            var roll1 = dice.Rng(6);
            var roll2 = dice.Rng(6);
            var roll = roll1 - roll2;
            if (TrophicDiet == TrophicDiets.PouncingCarnivore || TrophicDiet == TrophicDiets.Scavenger || TrophicDiet == TrophicDiets.FilterFeeder || TrophicDiet == TrophicDiets.AutothrophPhotosynthetic ||
                TrophicDiet == TrophicDiets.AutothrophChemosynthetic || TrophicDiet == TrophicDiets.AutothrophOther || TrophicDiet == TrophicDiets.GatheringHerbivore || TrophicDiet == TrophicDiets.GrazingHerbivore)
            {
                roll -= 1;
            }
            if (SocialOrganization == SocialOrganizations.Solitary || SocialOrganization == SocialOrganizations.PairBonded)
            {
                roll -= 1;
            }
            if (SocialOrganization == SocialOrganizations.SmallGroup || SocialOrganization == SocialOrganizations.MediumGroup)
            {
                roll += 1;
            }
            if (MatingBahavior == MatingBehaviours.Hive)
            {
                roll += 2;
            }
            if (Sex == Sexes.Asexual || Sex == Sexes.Hermaphrodite)
            {
                roll -= 1;
            }
            if (Gestation == Gestations.SpawningPollinating)
            {
                roll -= 1;
            }
            if (roll >= 3)
            {
                gregariousness = Gegariousnesses.Gregarious;
            }
            if (roll <= -3)
            {
                gregariousness = Gegariousnesses.ExtremeLoner;
            }
            switch (roll)
            {
                case 2:
                    gregariousness = Gegariousnesses.Chummy;
                    break;
                case 1:
                    gregariousness = Gegariousnesses.Congenial;
                    //TODO: Add Congenial quirk
                    break;
                case 0:
                    gregariousness = Gegariousnesses.Normal;
                    break;
                case -1:
                    gregariousness = Gegariousnesses.Uncongenial;
                    //TODO: Add UnCongenial quirk
                    break;
                case -2:
                    gregariousness = Gegariousnesses.Loner;
                    break;
            }
            return gregariousness;
        }

        private Imaginations DetermineImagination()
        {
            var imagination = Imaginations.None;
            var dice = new Dice();
            var roll1 = dice.Rng(6);
            var roll2 = dice.Rng(6);
            var roll = roll1 - roll2;
            if (TrophicDiet == TrophicDiets.PouncingCarnivore || TrophicDiet == TrophicDiets.Omnivore || TrophicDiet == TrophicDiets.GatheringHerbivore)
            {
                roll -= 1;
            }
            if (TrophicDiet == TrophicDiets.FilterFeeder || TrophicDiet == TrophicDiets.AutothrophPhotosynthetic || TrophicDiet == TrophicDiets.AutothrophChemosynthetic || TrophicDiet == TrophicDiets.AutothrophOther ||
                TrophicDiet == TrophicDiets.GrazingHerbivore)
            {
                roll -= 1;
            }
            if (Strategy == Strategies.StrongR)
            {
                roll -= 1;
            }
            if (Strategy == Strategies.StrongK)
            {
                roll += 1;
            }
            if (roll >= 3)
            {
                imagination = Imaginations.ExtremlyImaginative;
            }
            if (roll <= -3)
            {
                imagination = Imaginations.VeryHidebound;
            }
            switch (roll)
            {
                case 2:
                    imagination = Imaginations.VeryImaginative;
                    break;
                case 1:
                    imagination = Imaginations.Imaginative;
                    break;
                case 0:
                    imagination = Imaginations.Normal;
                    break;
                case -1:
                    imagination = Imaginations.Dull;
                    break;
                case -2:
                    imagination = Imaginations.Hidebound;
                    break;
            }
            return imagination;
        }

        private Suspicions DetermineSuspicion()
        {
            var suspicion = Suspicions.None;
            var dice = new Dice();
            var roll1 = dice.Rng(6);
            var roll2 = dice.Rng(6);
            var roll = roll1 - roll2;
            if (TrophicDiet == TrophicDiets.PouncingCarnivore || TrophicDiet == TrophicDiets.ChasingCarnivore || TrophicDiet == TrophicDiets.HijackingCarnivore || TrophicDiet == TrophicDiets.TrappingCarnivore)
            {
                roll -= 1;
            }
            if (TrophicDiet == TrophicDiets.GrazingHerbivore)
            {
                roll += 1;
            }
            if (Vision == Visions.Blindness || Vision == Visions.BlindnessSoft)
            {
                roll += 1;
            }
            if (SizeClass == SizeClasses.Large)
            {
                roll -= 1;
            }
            if (SizeClass == SizeClasses.Small)
            {
                roll += 1;
            }
            if (SocialOrganization == SocialOrganizations.Solitary || SocialOrganization == SocialOrganizations.PairBonded)
            {
                roll += 1;
            }
            if (roll >= 3)
            {
                suspicion = Suspicions.VeryFearfull;
            }
            if (roll <= -3)
            {
                suspicion = Suspicions.ExtremlyFearless;
            }
            switch (roll)
            {
                case 2:
                    suspicion = Suspicions.Fearfulness;
                    break;
                case 1:
                    suspicion = Suspicions.Careful;
                    break;
                case 0:
                    suspicion = Suspicions.Normal;
                    break;
                case -1:
                    suspicion = Suspicions.Fearlessness;
                    break;
                case -2:
                    suspicion = Suspicions.VeryFearless;
                    break;
            }
            return suspicion;
        }

        private Chauvinisms DetermineChauvinism()
        {
            var chauvinism = Chauvinisms.None;
            var dice = new Dice();
            var roll1 = dice.Rng(6);
            var roll2 = dice.Rng(6);
            var roll = roll1 - roll2;
            if (TrophicDiet == TrophicDiets.PouncingCarnivore || TrophicDiet == TrophicDiets.Omnivore || TrophicDiet == TrophicDiets.GatheringHerbivore)
            {
                roll -= 1;
            }
            if (TrophicDiet == TrophicDiets.FilterFeeder || TrophicDiet == TrophicDiets.AutothrophPhotosynthetic || TrophicDiet == TrophicDiets.AutothrophChemosynthetic || TrophicDiet == TrophicDiets.AutothrophOther ||
                TrophicDiet == TrophicDiets.GrazingHerbivore)
            {
                roll -= 1;
            }
            if (Strategy == Strategies.StrongR)
            {
                roll -= 1;
            }
            if (Strategy == Strategies.StrongK)
            {
                roll += 1;
            }
            if (roll >= 3)
            {
                chauvinism = Chauvinisms.ExtremlyChauvinistic;
                if (Empathy != Empathies.Responsive || Empathy != Empathies.Empathy || Empathy != Empathies.VeryEmpathic || Suspicion == Suspicions.Normal || Suspicion == Suspicions.Careful || Suspicion == Suspicions.Fearfulness ||
                    Suspicion == Suspicions.VeryFearfull)
                {
                    //Todo: Add Intolerance (Racial)
                }
                if (Suspicion == Suspicions.Careful || Suspicion == Suspicions.Fearfulness || Suspicion == Suspicions.VeryFearfull)
                {
                    //Todo: Add Xenophobia
                }
            }
            if (roll <= -3)
            {
                chauvinism = Chauvinisms.Undisciminating;
            }
            switch (roll)
            {
                case 2:
                    chauvinism = Chauvinisms.VeryChauvinistic;
                    if (Empathy != Empathies.Responsive || Empathy != Empathies.Empathy || Empathy != Empathies.VeryEmpathic || Suspicion == Suspicions.Normal || Suspicion == Suspicions.Careful || Suspicion == Suspicions.Fearfulness ||
                        Suspicion == Suspicions.VeryFearfull)
                    {
                        //Todo: Add Intolerance (Racial)
                    }
                    break;
                case 1:
                    chauvinism = Chauvinisms.Chauvinistic;
                    if (Empathy != Empathies.Normal || Empathy != Empathies.Responsive || Empathy != Empathies.Empathy || Empathy != Empathies.VeryEmpathic || Suspicion == Suspicions.Careful || Suspicion == Suspicions.Fearfulness ||
                        Suspicion == Suspicions.VeryFearfull)
                    {
                        //Todo: Add Intolerance (Racial)
                    }
                    break;
                case 0:
                    chauvinism = Chauvinisms.Normal;
                    break;
                case -1:
                    chauvinism = Chauvinisms.BroadMinded;
                    if (Suspicion == Suspicions.Fearlessness || Suspicion == Suspicions.VeryFearless || Suspicion == Suspicions.ExtremlyFearless)
                    {
                        if (Empathy == Empathies.Responsive || Empathy == Empathies.Empathy || Empathy == Empathies.VeryEmpathic)
                        {
                            //TODO: Add Xenophilia 15
                        }
                    }
                    break;
                case -2:
                    chauvinism = Chauvinisms.VeryBroadMinded;
                    break;
            }
            return chauvinism;
        }

        private Playfulnessess DeterminePlayfulness()
        {
            var playfulness = Playfulnessess.None;
            var dice = new Dice();
            var roll1 = dice.Rng(6);
            var roll2 = dice.Rng(6);
            var roll = roll1 - roll2;
            if (Strategy == Strategies.ModerateK)
            {
                roll += 1;
            }
            if (Strategy == Strategies.StrongK)
            {
                roll += 2;
            }
            if (IntelligenceValue >= 2)
            {
                roll += 1;
            }
            if (SocialOrganization == SocialOrganizations.Solitary)
            {
                roll -= 1;
            }
            //TODO
            //PSEUDOCODE
            //if (Advantages == "Cannot Learn")
            //{
            //    roll -= 3;
            //}
            if (roll >= 3)
            {
                playfulness = Playfulnessess.ExtremeCompulsivePlayfulness;
            }
            if (roll <= -3)
            {
                playfulness = Playfulnessess.NoSenseofHumor;
            }
            switch (roll)
            {
                case 2:
                    playfulness = Playfulnessess.CompulsivePlayfulness;
                    break;
                case 1:
                    playfulness = Playfulnessess.Playful;
                    //TODO add playful quirk
                    break;
                case 0:
                    playfulness = Playfulnessess.Normal;
                    break;
                case -1:
                    playfulness = Playfulnessess.Serious;
                    //TODO add Serious quirk
                    break;
                case -2:
                    playfulness = Playfulnessess.OdiousRacialHabit;
                    //TODO add Wet Blanket
                    break;
            }
            return playfulness;
        }

        #endregion
    }
}