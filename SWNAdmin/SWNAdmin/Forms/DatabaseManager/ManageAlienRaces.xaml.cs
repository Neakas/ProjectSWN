using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UniverseGeneration;

namespace SWNAdmin.Forms
{
    /// <summary>
    /// Interaction logic for ManageAlienRaces.xaml
    /// </summary>
    public partial class ManageAlienRaces : Window
    {
        public Alien alien;
        public ManageAlienRaces()
        {
            InitializeComponent();
        }

        private void btgenTest_Click(object sender, RoutedEventArgs e)
        {
            alien = new Alien();
            this.DataContext = alien;
            cbChemicalBasis.ItemsSource = alien.TypesofLifeDict;
            cbChemicalBasis.DisplayMemberPath = "Value";
            cbChemicalBasis.SelectedValuePath = "Key";
            cbChemicalBasis.SelectedValue = alien.chemicalBasis;

            cbLandOrWater.ItemsSource = alien.LandWaterDict;
            cbLandOrWater.DisplayMemberPath = "Value";
            cbLandOrWater.SelectedValuePath = "Key";
            cbLandOrWater.SelectedValue = alien.LandOrWater;

            cbLandHabitat.ItemsSource = alien.TypesofLandHabitatsDict;
            cbLandHabitat.DisplayMemberPath = "Value";
            cbLandHabitat.SelectedValuePath = "Key";
            cbLandHabitat.SelectedValue = alien.LandHabitat;

            cbWaterHabitat.ItemsSource = alien.TypesofWaterHabitatsDict;
            cbWaterHabitat.DisplayMemberPath = "Value";
            cbWaterHabitat.SelectedValuePath = "Key";
            cbWaterHabitat.SelectedValue = alien.WaterHabitat;

            cbThrophicDiet.ItemsSource = alien.TrophicDietDict;
            cbThrophicDiet.DisplayMemberPath = "Value";
            cbThrophicDiet.SelectedValuePath = "Key";
            cbThrophicDiet.SelectedValue = alien.TrophicDiet;

            cbPrimaryLocomotion.ItemsSource = alien.LocomotionDict;
            cbPrimaryLocomotion.DisplayMemberPath = "Value";
            cbPrimaryLocomotion.SelectedValuePath = "Key";
            cbPrimaryLocomotion.SelectedValue = alien.PrimaryLocomotion;

            cbSecondaryLocomotion.ItemsSource = alien.LocomotionDict;
            cbSecondaryLocomotion.DisplayMemberPath = "Value";
            cbSecondaryLocomotion.SelectedValuePath = "Key";
            cbSecondaryLocomotion.SelectedValue = alien.SecondaryLocomotion;

            checkHasSecondaryLocmotion.IsChecked = alien.hasSecondaryLocomotion;

            cbSizeClass.ItemsSource = alien.SizeClassDict;
            cbSizeClass.DisplayMemberPath = "Value";
            cbSizeClass.SelectedValuePath = "Key";
            cbSizeClass.SelectedValue = alien.SizeClass;

            cbSize.Text = alien.Size.ToString();

            cbSymmetry.ItemsSource = alien.SymmetryDict;
            cbSymmetry.DisplayMemberPath = "Value";
            cbSymmetry.SelectedValuePath = "Key";
            cbSymmetry.SelectedValue = alien.Symmetry;

            cbSides.Text = alien.Sides.ToString();

            cbLimbSegments.Text = alien.LimbSegments.ToString();

            cbTails.ItemsSource = alien.TailsDict;
            cbTails.DisplayMemberPath = "Value";
            cbTails.SelectedValuePath = "Key";
            cbTails.SelectedValue = alien.Tail;

            cbManipulators.Text = alien.Manipulators.ToString();

            cbSkeleton.ItemsSource = alien.SkeletonDict;
            cbSkeleton.DisplayMemberPath = "Value";
            cbSkeleton.SelectedValuePath = "Key";
            cbSkeleton.SelectedValue = alien.Skeleton;

            cbSkinClass.ItemsSource = alien.SkinTypeDict;
            cbSkinClass.DisplayMemberPath = "Value";
            cbSkinClass.SelectedValuePath = "Key";
            cbSkinClass.SelectedValue = alien.SkinClass;

            cbSkin.ItemsSource = alien.SkinDict;
            cbSkin.DisplayMemberPath = "Value";
            cbSkin.SelectedValuePath = "Key";
            cbSkin.SelectedValue = alien.Skin;

            cbBreathing.ItemsSource = alien.BreathingMethodDict;
            cbBreathing.DisplayMemberPath = "Value";
            cbBreathing.SelectedValuePath = "Key";
            cbBreathing.SelectedValue = alien.Breathing;

            cbTemperture.ItemsSource = alien.TemperatureDict;
            cbTemperture.DisplayMemberPath = "Value";
            cbTemperture.SelectedValuePath = "Key";
            cbTemperture.SelectedValue = alien.Temperture;

            cbGrowthRate.ItemsSource = alien.GrowthDict;
            cbGrowthRate.DisplayMemberPath = "Value";
            cbGrowthRate.SelectedValuePath = "Key";
            cbGrowthRate.SelectedValue = alien.Growth;

            cbSexes.ItemsSource = alien.SexesDict;
            cbSexes.DisplayMemberPath = "Value";
            cbSexes.SelectedValuePath = "Key";
            cbSexes.SelectedValue = alien.Sex;

            cbGestation.ItemsSource = alien.GestationDict;
            cbGestation.DisplayMemberPath = "Value";
            cbGestation.SelectedValuePath = "Key";
            cbGestation.SelectedValue = alien.Gestation;

            cbStrategy.ItemsSource = alien.StrategyDict;
            cbStrategy.DisplayMemberPath = "Value";
            cbStrategy.SelectedValuePath = "Key";
            cbStrategy.SelectedValue = alien.Strategy;

            cbOffspringCount.Text = alien.OffspringCount.ToString();

            cbPrimarySense.ItemsSource = alien.PrimarySenseDict;
            cbPrimarySense.DisplayMemberPath = "Value";
            cbPrimarySense.SelectedValuePath = "Key";
            cbPrimarySense.SelectedValue = alien.PrimarySense;

            cbVision.ItemsSource = alien.VisionDict;
            cbVision.DisplayMemberPath = "Value";
            cbVision.SelectedValuePath = "Key";
            cbVision.SelectedValue = alien.Vision;

            cbHearing.ItemsSource = alien.HearingDict;
            cbHearing.DisplayMemberPath = "Value";
            cbHearing.SelectedValuePath = "Key";
            cbHearing.SelectedValue = alien.Hearing;

            cbTouch.ItemsSource = alien.TouchDict;
            cbTouch.DisplayMemberPath = "Value";
            cbTouch.SelectedValuePath = "Key";
            cbTouch.SelectedValue = alien.Touch;

            cbTasteSmell.ItemsSource = alien.TasteSmellDict;
            cbTasteSmell.DisplayMemberPath = "Value";
            cbTasteSmell.SelectedValuePath = "Key";
            cbTasteSmell.SelectedValue = alien.TasteSmell;

            cbIntelligence.ItemsSource = alien.IntelligenceDict;
            cbIntelligence.DisplayMemberPath = "Value";
            cbIntelligence.SelectedValuePath = "Key";
            cbIntelligence.SelectedValue = alien.Intelligence;

            cbIntelligenceValue.Text = alien.IntelligenceValue.ToString();

            cbMatingBehaviour.ItemsSource = alien.MatingBahaviourDict;
            cbMatingBehaviour.DisplayMemberPath = "Value";
            cbMatingBehaviour.SelectedValuePath = "Key";
            cbMatingBehaviour.SelectedValue = alien.MatingBahavior;

            cbSocialOrganization.ItemsSource = alien.SocialOrganizationDict;
            cbSocialOrganization.DisplayMemberPath = "Value";
            cbSocialOrganization.SelectedValuePath = "Key";
            cbSocialOrganization.SelectedValue = alien.SocialOrganization;

            cbSocialGroupSize.Text = alien.SocialGroupSize.ToString();

            cbConcentration.ItemsSource = alien.ConcentrationDict;
            cbConcentration.DisplayMemberPath = "Value";
            cbConcentration.SelectedValuePath = "Key";
            cbConcentration.SelectedValue = alien.Concentration;

            cbCuriosity.ItemsSource = alien.CuriosityDict;
            cbCuriosity.DisplayMemberPath = "Value";
            cbCuriosity.SelectedValuePath = "Key";
            cbCuriosity.SelectedValue = alien.Curiosity;

            cbEgoism.ItemsSource = alien.EgoismDict;
            cbEgoism.DisplayMemberPath = "Value";
            cbEgoism.SelectedValuePath = "Key";
            cbEgoism.SelectedValue = alien.Egoism;

            cbEmpathy.ItemsSource = alien.EmpathyDict;
            cbEmpathy.DisplayMemberPath = "Value";
            cbEmpathy.SelectedValuePath = "Key";
            cbEmpathy.SelectedValue = alien.Empathy;

            cbGegariousness.ItemsSource = alien.GegariousnessnessDict;
            cbGegariousness.DisplayMemberPath = "Value";
            cbGegariousness.SelectedValuePath = "Key";
            cbGegariousness.SelectedValue = alien.Gegariousness;

            cbImagination.ItemsSource = alien.ImaginationDict;
            cbImagination.DisplayMemberPath = "Value";
            cbImagination.SelectedValuePath = "Key";
            cbImagination.SelectedValue = alien.Imagination;

            cbChauvinism.ItemsSource = alien.ChauvinismDict;
            cbChauvinism.DisplayMemberPath = "Value";
            cbChauvinism.SelectedValuePath = "Key";
            cbChauvinism.SelectedValue = alien.Chauvinism;

            cbSuspicion.ItemsSource = alien.SuspicionDict;
            cbSuspicion.DisplayMemberPath = "Value";
            cbSuspicion.SelectedValuePath = "Key";
            cbSuspicion.SelectedValue = alien.Suspicion;

            cbPlayfulness.ItemsSource = alien.PlayfulnessDict;
            cbPlayfulness.DisplayMemberPath = "Value";
            cbPlayfulness.SelectedValuePath = "Key";
            cbPlayfulness.SelectedValue = alien.Playfulness;
        }

        private void cbLandOrWater_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Utility.Db1Entities())
            {
                Utility.Aliens DBAlien = new Utility.Aliens();
                DBAlien.Name = tbName.Text;
                DBAlien.chemicalBasis = cbChemicalBasis.SelectedValue.ToString();
                DBAlien.LandOrWater = cbLandOrWater.SelectedValue.ToString();
                DBAlien.LandHabitat = cbLandHabitat.SelectedValue.ToString();
                DBAlien.WaterHabitat = cbLandHabitat.SelectedValue.ToString();
                DBAlien.TrophicDiet = cbThrophicDiet.SelectedValue.ToString();
                DBAlien.PrimaryLocomotion = cbPrimaryLocomotion.SelectedValue.ToString();
                DBAlien.SecondaryLocomotion = cbSecondaryLocomotion.SelectedValue.ToString();
                DBAlien.hasSecondaryLocomotuib = checkHasSecondaryLocmotion.IsChecked;
                DBAlien.Gravity = null; //FIXME
                DBAlien.SizeClass = cbSizeClass.SelectedValue.ToString();
                DBAlien.Size = Double.Parse(cbSize.Text);
                DBAlien.Symmetry = cbSymmetry.SelectedValue.ToString();
                DBAlien.Sides = Int32.Parse(cbSides.Text);
                DBAlien.LimbSegments = Int32.Parse(cbLimbSegments.Text);
                DBAlien.Tail = cbTails.SelectedValue.ToString();
                DBAlien.Manipulators = Int32.Parse(cbManipulators.Text);
                DBAlien.Skeleton = cbSkeleton.SelectedValue.ToString();
                DBAlien.SkinClass = cbSkinClass.SelectedValue.ToString();
                DBAlien.Skin = cbSkin.SelectedValue.ToString();
                DBAlien.Breathing = cbBreathing.SelectedValue.ToString();
                DBAlien.Temperatur = cbTemperture.SelectedValue.ToString();
                DBAlien.Growth = cbGrowthRate.SelectedValue.ToString();
                DBAlien.Sex = cbSexes.SelectedValue.ToString();
                DBAlien.Gestation = cbGestation.SelectedValue.ToString();
                DBAlien.Strategy = cbStrategy.SelectedValue.ToString();
                DBAlien.OffspringCount = Int32.Parse(cbOffspringCount.Text);
                DBAlien.PrimarySense = cbPrimarySense.SelectedValue.ToString();
                DBAlien.Vision = cbVision.SelectedValue.ToString();
                DBAlien.Hearing = cbHearing.SelectedValue.ToString();
                DBAlien.Touch = cbTouch.SelectedValue.ToString();
                DBAlien.TasteSmell = cbTasteSmell.SelectedValue.ToString();
                DBAlien.Intelligence = cbIntelligence.SelectedValue.ToString();
                DBAlien.IntelligenceValue = Int32.Parse(cbIntelligenceValue.Text);
                DBAlien.MatingBehaviour = cbMatingBehaviour.SelectedValue.ToString();
                DBAlien.SocialOrganization = cbSocialOrganization.SelectedValue.ToString();
                DBAlien.SocialGroupSize = Int32.Parse(cbSocialGroupSize.Text);
                DBAlien.Concentration = cbConcentration.SelectedValue.ToString();
                DBAlien.Curiosity = cbCuriosity.SelectedValue.ToString();
                DBAlien.Egoism = cbEgoism.SelectedValue.ToString();
                DBAlien.Empathy = cbEmpathy.SelectedValue.ToString();
                DBAlien.Gegariousness = cbGegariousness.SelectedValue.ToString();
                DBAlien.Imagination = cbImagination.SelectedValue.ToString();
                DBAlien.Chauvinism = cbChauvinism.SelectedValue.ToString();
                DBAlien.Suspicion = cbSuspicion.SelectedValue.ToString();
                DBAlien.Playfulness = cbPlayfulness.SelectedValue.ToString();
                context.Aliens.Add(DBAlien);
                context.SaveChanges();
            }

            MessageBox.Show("The Alien '" + tbName.Text + "' has been saved in the Database");
        }
    }
}
