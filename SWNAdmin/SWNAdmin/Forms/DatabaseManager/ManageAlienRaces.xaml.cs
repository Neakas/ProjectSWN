using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using SWNAdmin.Networking;
using SWNAdmin.Utility;
using UniverseGeneration.OtherGeneration;

namespace SWNAdmin.Forms.DatabaseManager
{
    /// <summary>
    ///     Interaction logic for ManageAlienRaces.xaml
    /// </summary>
    public partial class ManageAlienRaces
    {
        private Alien _alien;
        private string _imagePath = "";
        private ListBox _loadBox;
        private Aliens _loadedAlien;
        private Window _loadWindow;
        private BitmapImage _raceBitmapImage;

        public ManageAlienRaces()
        {
            InitializeComponent();
            var imageContextMenu = new ContextMenu();
            RaceImageWindow.ContextMenu = imageContextMenu;
            var sendToClients = new Button {Content = "Send to Clients"};
            imageContextMenu.Items.Add(sendToClients);
            sendToClients.Click += SendToClients_Click;
        }

        private void btgenTest_Click(object sender, RoutedEventArgs e)
        {
            BtSave.IsEnabled = true;
            BtSave.Visibility = Visibility.Visible;
            BtUpdate.IsEnabled = false;
            BtUpdate.Visibility = Visibility.Hidden;
            BtDelete.IsEnabled = false;
            BtDelete.Visibility = Visibility.Hidden;

            _alien = new Alien();
            DataContext = _alien;
            CbChemicalBasis.ItemsSource = _alien.TypesofLifeDict;
            CbChemicalBasis.DisplayMemberPath = "Value";
            CbChemicalBasis.SelectedValuePath = "Key";
            CbChemicalBasis.SelectedValue = _alien.chemicalBasis;

            CbLandOrWater.ItemsSource = _alien.LandWaterDict;
            CbLandOrWater.DisplayMemberPath = "Value";
            CbLandOrWater.SelectedValuePath = "Key";
            CbLandOrWater.SelectedValue = _alien.LandOrWater;

            CbLandHabitat.ItemsSource = _alien.TypesofLandHabitatsDict;
            CbLandHabitat.DisplayMemberPath = "Value";
            CbLandHabitat.SelectedValuePath = "Key";
            CbLandHabitat.SelectedValue = _alien.LandHabitat;

            CbWaterHabitat.ItemsSource = _alien.TypesofWaterHabitatsDict;
            CbWaterHabitat.DisplayMemberPath = "Value";
            CbWaterHabitat.SelectedValuePath = "Key";
            CbWaterHabitat.SelectedValue = _alien.WaterHabitat;

            CbThrophicDiet.ItemsSource = _alien.TrophicDietDict;
            CbThrophicDiet.DisplayMemberPath = "Value";
            CbThrophicDiet.SelectedValuePath = "Key";
            CbThrophicDiet.SelectedValue = _alien.TrophicDiet;

            CbPrimaryLocomotion.ItemsSource = _alien.LocomotionDict;
            CbPrimaryLocomotion.DisplayMemberPath = "Value";
            CbPrimaryLocomotion.SelectedValuePath = "Key";
            CbPrimaryLocomotion.SelectedValue = _alien.PrimaryLocomotion;

            CbSecondaryLocomotion.ItemsSource = _alien.LocomotionDict;
            CbSecondaryLocomotion.DisplayMemberPath = "Value";
            CbSecondaryLocomotion.SelectedValuePath = "Key";
            CbSecondaryLocomotion.SelectedValue = _alien.SecondaryLocomotion;

            CheckHasSecondaryLocmotion.IsChecked = _alien.hasSecondaryLocomotion;

            CbSizeClass.ItemsSource = _alien.SizeClassDict;
            CbSizeClass.DisplayMemberPath = "Value";
            CbSizeClass.SelectedValuePath = "Key";
            CbSizeClass.SelectedValue = _alien.SizeClass;

            CbSize.Text = _alien.Size.ToString();

            CbSymmetry.ItemsSource = _alien.SymmetryDict;
            CbSymmetry.DisplayMemberPath = "Value";
            CbSymmetry.SelectedValuePath = "Key";
            CbSymmetry.SelectedValue = _alien.Symmetry;

            CbSides.Text = _alien.Sides.ToString();

            CbLimbSegments.Text = _alien.LimbSegments.ToString();

            CbTails.ItemsSource = _alien.TailsDict;
            CbTails.DisplayMemberPath = "Value";
            CbTails.SelectedValuePath = "Key";
            CbTails.SelectedValue = _alien.Tail;

            CbManipulators.Text = _alien.Manipulators.ToString();

            CbSkeleton.ItemsSource = _alien.SkeletonDict;
            CbSkeleton.DisplayMemberPath = "Value";
            CbSkeleton.SelectedValuePath = "Key";
            CbSkeleton.SelectedValue = _alien.Skeleton;

            CbSkinClass.ItemsSource = _alien.SkinTypeDict;
            CbSkinClass.DisplayMemberPath = "Value";
            CbSkinClass.SelectedValuePath = "Key";
            CbSkinClass.SelectedValue = _alien.SkinClass;

            CbSkin.ItemsSource = _alien.SkinDict;
            CbSkin.DisplayMemberPath = "Value";
            CbSkin.SelectedValuePath = "Key";
            CbSkin.SelectedValue = _alien.Skin;

            CbBreathing.ItemsSource = _alien.BreathingMethodDict;
            CbBreathing.DisplayMemberPath = "Value";
            CbBreathing.SelectedValuePath = "Key";
            CbBreathing.SelectedValue = _alien.Breathing;

            CbTemperture.ItemsSource = _alien.TemperatureDict;
            CbTemperture.DisplayMemberPath = "Value";
            CbTemperture.SelectedValuePath = "Key";
            CbTemperture.SelectedValue = _alien.Temperture;

            CbGrowthRate.ItemsSource = _alien.GrowthDict;
            CbGrowthRate.DisplayMemberPath = "Value";
            CbGrowthRate.SelectedValuePath = "Key";
            CbGrowthRate.SelectedValue = _alien.Growth;

            CbSexes.ItemsSource = _alien.SexesDict;
            CbSexes.DisplayMemberPath = "Value";
            CbSexes.SelectedValuePath = "Key";
            CbSexes.SelectedValue = _alien.Sex;

            CbGestation.ItemsSource = _alien.GestationDict;
            CbGestation.DisplayMemberPath = "Value";
            CbGestation.SelectedValuePath = "Key";
            CbGestation.SelectedValue = _alien.Gestation;

            CbStrategy.ItemsSource = _alien.StrategyDict;
            CbStrategy.DisplayMemberPath = "Value";
            CbStrategy.SelectedValuePath = "Key";
            CbStrategy.SelectedValue = _alien.Strategy;

            CbOffspringCount.Text = _alien.OffspringCount.ToString();

            CbPrimarySense.ItemsSource = _alien.PrimarySenseDict;
            CbPrimarySense.DisplayMemberPath = "Value";
            CbPrimarySense.SelectedValuePath = "Key";
            CbPrimarySense.SelectedValue = _alien.PrimarySense;

            CbVision.ItemsSource = _alien.VisionDict;
            CbVision.DisplayMemberPath = "Value";
            CbVision.SelectedValuePath = "Key";
            CbVision.SelectedValue = _alien.Vision;

            CbHearing.ItemsSource = _alien.HearingDict;
            CbHearing.DisplayMemberPath = "Value";
            CbHearing.SelectedValuePath = "Key";
            CbHearing.SelectedValue = _alien.Hearing;

            CbTouch.ItemsSource = _alien.TouchDict;
            CbTouch.DisplayMemberPath = "Value";
            CbTouch.SelectedValuePath = "Key";
            CbTouch.SelectedValue = _alien.Touch;

            CbTasteSmell.ItemsSource = _alien.TasteSmellDict;
            CbTasteSmell.DisplayMemberPath = "Value";
            CbTasteSmell.SelectedValuePath = "Key";
            CbTasteSmell.SelectedValue = _alien.TasteSmell;

            CbIntelligence.ItemsSource = _alien.IntelligenceDict;
            CbIntelligence.DisplayMemberPath = "Value";
            CbIntelligence.SelectedValuePath = "Key";
            CbIntelligence.SelectedValue = _alien.Intelligence;

            CbIntelligenceValue.Text = _alien.IntelligenceValue.ToString();

            CbMatingBehaviour.ItemsSource = _alien.MatingBahaviourDict;
            CbMatingBehaviour.DisplayMemberPath = "Value";
            CbMatingBehaviour.SelectedValuePath = "Key";
            CbMatingBehaviour.SelectedValue = _alien.MatingBahavior;

            CbSocialOrganization.ItemsSource = _alien.SocialOrganizationDict;
            CbSocialOrganization.DisplayMemberPath = "Value";
            CbSocialOrganization.SelectedValuePath = "Key";
            CbSocialOrganization.SelectedValue = _alien.SocialOrganization;

            CbSocialGroupSize.Text = _alien.SocialGroupSize.ToString();

            CbConcentration.ItemsSource = _alien.ConcentrationDict;
            CbConcentration.DisplayMemberPath = "Value";
            CbConcentration.SelectedValuePath = "Key";
            CbConcentration.SelectedValue = _alien.Concentration;

            CbCuriosity.ItemsSource = _alien.CuriosityDict;
            CbCuriosity.DisplayMemberPath = "Value";
            CbCuriosity.SelectedValuePath = "Key";
            CbCuriosity.SelectedValue = _alien.Curiosity;

            CbEgoism.ItemsSource = _alien.EgoismDict;
            CbEgoism.DisplayMemberPath = "Value";
            CbEgoism.SelectedValuePath = "Key";
            CbEgoism.SelectedValue = _alien.Egoism;

            CbEmpathy.ItemsSource = _alien.EmpathyDict;
            CbEmpathy.DisplayMemberPath = "Value";
            CbEmpathy.SelectedValuePath = "Key";
            CbEmpathy.SelectedValue = _alien.Empathy;

            CbGegariousness.ItemsSource = _alien.GegariousnessnessDict;
            CbGegariousness.DisplayMemberPath = "Value";
            CbGegariousness.SelectedValuePath = "Key";
            CbGegariousness.SelectedValue = _alien.Gegariousness;

            CbImagination.ItemsSource = _alien.ImaginationDict;
            CbImagination.DisplayMemberPath = "Value";
            CbImagination.SelectedValuePath = "Key";
            CbImagination.SelectedValue = _alien.Imagination;

            CbChauvinism.ItemsSource = _alien.ChauvinismDict;
            CbChauvinism.DisplayMemberPath = "Value";
            CbChauvinism.SelectedValuePath = "Key";
            CbChauvinism.SelectedValue = _alien.Chauvinism;

            CbSuspicion.ItemsSource = _alien.SuspicionDict;
            CbSuspicion.DisplayMemberPath = "Value";
            CbSuspicion.SelectedValuePath = "Key";
            CbSuspicion.SelectedValue = _alien.Suspicion;

            CbPlayfulness.ItemsSource = _alien.PlayfulnessDict;
            CbPlayfulness.DisplayMemberPath = "Value";
            CbPlayfulness.SelectedValuePath = "Key";
            CbPlayfulness.SelectedValue = _alien.Playfulness;
        }

        private void cbLandOrWater_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Db1Entities())
            {
                var dbAlien = new Aliens
                {
                    Name = TbName.Text,
                    chemicalBasis = CbChemicalBasis.SelectedValue.ToString(),
                    LandOrWater = CbLandOrWater.SelectedValue.ToString(),
                    LandHabitat = CbLandHabitat.SelectedValue.ToString(),
                    WaterHabitat = CbWaterHabitat.SelectedValue.ToString(),
                    TrophicDiet = CbThrophicDiet.SelectedValue.ToString(),
                    PrimaryLocomotion = CbPrimaryLocomotion.SelectedValue.ToString(),
                    SecondaryLocomotion = CbSecondaryLocomotion.SelectedValue.ToString(),
                    hasSecondaryLocomotuib = CheckHasSecondaryLocmotion.IsChecked,
                    Gravity = null,
                    SizeClass = CbSizeClass.SelectedValue.ToString(),
                    Size = double.Parse(CbSize.Text),
                    Symmetry = CbSymmetry.SelectedValue.ToString(),
                    Sides = int.Parse(CbSides.Text),
                    LimbSegments = int.Parse(CbLimbSegments.Text),
                    Tail = CbTails.SelectedValue.ToString(),
                    Manipulators = int.Parse(CbManipulators.Text),
                    Skeleton = CbSkeleton.SelectedValue.ToString(),
                    SkinClass = CbSkinClass.SelectedValue.ToString(),
                    Skin = CbSkin.SelectedValue.ToString(),
                    Breathing = CbBreathing.SelectedValue.ToString(),
                    Temperatur = CbTemperture.SelectedValue.ToString(),
                    Growth = CbGrowthRate.SelectedValue.ToString(),
                    Sex = CbSexes.SelectedValue.ToString(),
                    Gestation = CbGestation.SelectedValue.ToString(),
                    Strategy = CbStrategy.SelectedValue.ToString(),
                    OffspringCount = int.Parse(CbOffspringCount.Text),
                    PrimarySense = CbPrimarySense.SelectedValue.ToString(),
                    Vision = CbVision.SelectedValue.ToString(),
                    Hearing = CbHearing.SelectedValue.ToString(),
                    Touch = CbTouch.SelectedValue.ToString(),
                    TasteSmell = CbTasteSmell.SelectedValue.ToString(),
                    Intelligence = CbIntelligence.SelectedValue.ToString(),
                    IntelligenceValue = int.Parse(CbIntelligenceValue.Text),
                    MatingBehaviour = CbMatingBehaviour.SelectedValue.ToString(),
                    SocialOrganization = CbSocialOrganization.SelectedValue.ToString(),
                    SocialGroupSize = int.Parse(CbSocialGroupSize.Text),
                    Concentration = CbConcentration.SelectedValue.ToString(),
                    Curiosity = CbCuriosity.SelectedValue.ToString(),
                    Egoism = CbEgoism.SelectedValue.ToString(),
                    Empathy = CbEmpathy.SelectedValue.ToString(),
                    Gegariousness = CbGegariousness.SelectedValue.ToString(),
                    Imagination = CbImagination.SelectedValue.ToString(),
                    Chauvinism = CbChauvinism.SelectedValue.ToString(),
                    Suspicion = CbSuspicion.SelectedValue.ToString(),
                    Playfulness = CbPlayfulness.SelectedValue.ToString(),
                    Image = BuildByteArrayFromImage(_raceBitmapImage)
                };
                //FIXME

                //if (RaceImageWindow.Source != null)
                //{
                //    byte[] buffer;
                //    FileStream fileStream = new FileStream(_imagePath, FileMode.Open, FileAccess.Read);
                //    try
                //    {
                //        int length = (int)fileStream.Length;  // get file length
                //        buffer = new byte[length];            // create buffer
                //        int count;                            // actual number of bytes read
                //        int sum = 0;                          // total number of bytes read

                //        // read until Read method returns 0 (end of the stream has been reached)
                //        while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                //            sum += count;  // sum is a buffer offset for next reading
                //    }
                //    finally
                //    {
                //        fileStream.Close();
                //    }
                //    DBAlien.image = buffer;
                //}



                context.Aliens.Add(dbAlien);
                context.SaveChanges();
            }

            MessageBox.Show("The Alien '" + TbName.Text + "' has been saved in the Database");
        }

        private void btLoadImage_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.ShowDialog();
            _imagePath = ofd.FileName;
            _raceBitmapImage = new BitmapImage(new Uri(ofd.FileName));
            RaceImageWindow.Source = _raceBitmapImage;
        }

        private void btLoad_Click(object sender, RoutedEventArgs e)
        {
            _loadWindow = new Window
            {
                Width = 200,
                Height = 200
            };
            _loadBox = new ListBox();
            _loadWindow.Content = _loadBox;
            using (var context = new Db1Entities())
            {
                _loadBox.ItemsSource = (from c in context.Aliens select c).ToList();
                _loadBox.DisplayMemberPath = "Name";
                _loadBox.MouseDoubleClick += LoadboxSelectionChanged;
            }
            _loadWindow.ShowDialog();
        }

        private void LoadboxSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (_loadBox.SelectedItem != null)
            {
                _loadedAlien = (Aliens) _loadBox.SelectedItem;
                _loadWindow.Close();
                LoadAlien();
            }
        }

        private void LoadAlien()
        {
            BtUpdate.Visibility = Visibility.Visible;
            BtUpdate.IsEnabled = true;
            BtSave.IsEnabled = false;
            BtSave.Visibility = Visibility.Hidden;
            BtDelete.Visibility = Visibility.Visible;
            BtDelete.IsEnabled = true;

            _alien = new Alien();
            TbName.Text = _loadedAlien.Name;
            RaceImageWindow.Source = BuildImageFromByteArray(_loadedAlien.Image);

            CbChemicalBasis.SelectedValue = _loadedAlien.chemicalBasis;
            CbLandOrWater.SelectedValue = _loadedAlien.LandOrWater;
            CbLandHabitat.SelectedValue = _loadedAlien.LandHabitat;
            CbWaterHabitat.SelectedValue = _loadedAlien.WaterHabitat;
            CbThrophicDiet.SelectedValue = _loadedAlien.TrophicDiet;
            CbPrimaryLocomotion.SelectedValue = _loadedAlien.PrimaryLocomotion;
            CbSecondaryLocomotion.SelectedValue = _loadedAlien.SecondaryLocomotion;
            CheckHasSecondaryLocmotion.IsChecked = _loadedAlien.hasSecondaryLocomotuib;
            CbSizeClass.SelectedValue = _loadedAlien.SizeClass;
            CbSize.Text = _loadedAlien.Size.ToString();
            CbSymmetry.SelectedValue = _loadedAlien.Symmetry;
            CbSides.Text = _loadedAlien.Sides.ToString();
            CbLimbSegments.Text = _loadedAlien.LimbSegments.ToString();
            CbTails.SelectedValue = _loadedAlien.Tail;
            CbManipulators.Text = _loadedAlien.Manipulators.ToString();
            CbSkeleton.SelectedValue = _loadedAlien.Skeleton;
            CbSkinClass.SelectedValue = _loadedAlien.SkinClass;
            CbSkin.SelectedValue = _loadedAlien.Skin;
            CbBreathing.SelectedValue = _loadedAlien.Breathing;
            CbTemperture.SelectedValue = _loadedAlien.Temperatur;
            CbGrowthRate.SelectedValue = _loadedAlien.Growth;
            CbSexes.SelectedValue = _loadedAlien.Sex;
            CbGestation.SelectedValue = _loadedAlien.Gestation;
            CbStrategy.SelectedValue = _loadedAlien.Strategy;
            CbOffspringCount.Text = _loadedAlien.OffspringCount.ToString();
            CbPrimarySense.SelectedValue = _loadedAlien.PrimarySense;
            CbVision.SelectedValue = _loadedAlien.Vision;
            CbHearing.SelectedValue = _loadedAlien.Hearing;
            CbTouch.SelectedValue = _loadedAlien.Touch;
            CbTasteSmell.SelectedValue = _loadedAlien.TasteSmell;
            CbIntelligence.SelectedValue = _loadedAlien.Intelligence;
            CbIntelligenceValue.Text = _loadedAlien.IntelligenceValue.ToString();
            CbMatingBehaviour.SelectedValue = _loadedAlien.MatingBehaviour;
            CbSocialOrganization.SelectedValue = _loadedAlien.SocialOrganization;
            CbSocialGroupSize.Text = _loadedAlien.SocialGroupSize.ToString();
            CbConcentration.SelectedValue = _loadedAlien.Concentration;
            CbCuriosity.SelectedValue = _loadedAlien.Curiosity;
            CbEgoism.SelectedValue = _loadedAlien.Egoism;
            CbEmpathy.SelectedValue = _loadedAlien.Empathy;
            CbGegariousness.SelectedValue = _loadedAlien.Gegariousness;
            CbImagination.SelectedValue = _loadedAlien.Imagination;
            CbChauvinism.SelectedValue = _loadedAlien.Chauvinism;
            CbSuspicion.SelectedValue = _loadedAlien.Suspicion;
            CbPlayfulness.SelectedValue = _loadedAlien.Playfulness;

            CbChemicalBasis.ItemsSource = _alien.TypesofLifeDict;
            CbChemicalBasis.DisplayMemberPath = "Value";
            CbChemicalBasis.SelectedValuePath = "Key";

            CbLandOrWater.ItemsSource = _alien.LandWaterDict;
            CbLandOrWater.DisplayMemberPath = "Value";
            CbLandOrWater.SelectedValuePath = "Key";

            CbLandHabitat.ItemsSource = _alien.TypesofLandHabitatsDict;
            CbLandHabitat.DisplayMemberPath = "Value";
            CbLandHabitat.SelectedValuePath = "Key";

            CbWaterHabitat.ItemsSource = _alien.TypesofWaterHabitatsDict;
            CbWaterHabitat.DisplayMemberPath = "Value";
            CbWaterHabitat.SelectedValuePath = "Key";

            CbThrophicDiet.ItemsSource = _alien.TrophicDietDict;
            CbThrophicDiet.DisplayMemberPath = "Value";
            CbThrophicDiet.SelectedValuePath = "Key";

            CbPrimaryLocomotion.ItemsSource = _alien.LocomotionDict;
            CbPrimaryLocomotion.DisplayMemberPath = "Value";
            CbPrimaryLocomotion.SelectedValuePath = "Key";

            CbSecondaryLocomotion.ItemsSource = _alien.LocomotionDict;
            CbSecondaryLocomotion.DisplayMemberPath = "Value";
            CbSecondaryLocomotion.SelectedValuePath = "Key";


            CbSizeClass.ItemsSource = _alien.SizeClassDict;
            CbSizeClass.DisplayMemberPath = "Value";
            CbSizeClass.SelectedValuePath = "Key";

            CbSymmetry.ItemsSource = _alien.SymmetryDict;
            CbSymmetry.DisplayMemberPath = "Value";
            CbSymmetry.SelectedValuePath = "Key";

            CbTails.ItemsSource = _alien.TailsDict;
            CbTails.DisplayMemberPath = "Value";
            CbTails.SelectedValuePath = "Key";


            CbSkeleton.ItemsSource = _alien.SkeletonDict;
            CbSkeleton.DisplayMemberPath = "Value";
            CbSkeleton.SelectedValuePath = "Key";

            CbSkinClass.ItemsSource = _alien.SkinTypeDict;
            CbSkinClass.DisplayMemberPath = "Value";
            CbSkinClass.SelectedValuePath = "Key";

            CbSkin.ItemsSource = _alien.SkinDict;
            CbSkin.DisplayMemberPath = "Value";
            CbSkin.SelectedValuePath = "Key";

            CbBreathing.ItemsSource = _alien.BreathingMethodDict;
            CbBreathing.DisplayMemberPath = "Value";
            CbBreathing.SelectedValuePath = "Key";

            CbTemperture.ItemsSource = _alien.TemperatureDict;
            CbTemperture.DisplayMemberPath = "Value";
            CbTemperture.SelectedValuePath = "Key";

            CbGrowthRate.ItemsSource = _alien.GrowthDict;
            CbGrowthRate.DisplayMemberPath = "Value";
            CbGrowthRate.SelectedValuePath = "Key";

            CbSexes.ItemsSource = _alien.SexesDict;
            CbSexes.DisplayMemberPath = "Value";
            CbSexes.SelectedValuePath = "Key";

            CbGestation.ItemsSource = _alien.GestationDict;
            CbGestation.DisplayMemberPath = "Value";
            CbGestation.SelectedValuePath = "Key";

            CbStrategy.ItemsSource = _alien.StrategyDict;
            CbStrategy.DisplayMemberPath = "Value";
            CbStrategy.SelectedValuePath = "Key";

            CbPrimarySense.ItemsSource = _alien.PrimarySenseDict;
            CbPrimarySense.DisplayMemberPath = "Value";
            CbPrimarySense.SelectedValuePath = "Key";

            CbVision.ItemsSource = _alien.VisionDict;
            CbVision.DisplayMemberPath = "Value";
            CbVision.SelectedValuePath = "Key";

            CbHearing.ItemsSource = _alien.HearingDict;
            CbHearing.DisplayMemberPath = "Value";
            CbHearing.SelectedValuePath = "Key";

            CbTouch.ItemsSource = _alien.TouchDict;
            CbTouch.DisplayMemberPath = "Value";
            CbTouch.SelectedValuePath = "Key";

            CbTasteSmell.ItemsSource = _alien.TasteSmellDict;
            CbTasteSmell.DisplayMemberPath = "Value";
            CbTasteSmell.SelectedValuePath = "Key";

            CbIntelligence.ItemsSource = _alien.IntelligenceDict;
            CbIntelligence.DisplayMemberPath = "Value";
            CbIntelligence.SelectedValuePath = "Key";

            CbIntelligenceValue.Text = _alien.IntelligenceValue.ToString();

            CbMatingBehaviour.ItemsSource = _alien.MatingBahaviourDict;
            CbMatingBehaviour.DisplayMemberPath = "Value";
            CbMatingBehaviour.SelectedValuePath = "Key";

            CbSocialOrganization.ItemsSource = _alien.SocialOrganizationDict;
            CbSocialOrganization.DisplayMemberPath = "Value";
            CbSocialOrganization.SelectedValuePath = "Key";

            CbConcentration.ItemsSource = _alien.ConcentrationDict;
            CbConcentration.DisplayMemberPath = "Value";
            CbConcentration.SelectedValuePath = "Key";

            CbCuriosity.ItemsSource = _alien.CuriosityDict;
            CbCuriosity.DisplayMemberPath = "Value";
            CbCuriosity.SelectedValuePath = "Key";

            CbEgoism.ItemsSource = _alien.EgoismDict;
            CbEgoism.DisplayMemberPath = "Value";
            CbEgoism.SelectedValuePath = "Key";

            CbEmpathy.ItemsSource = _alien.EmpathyDict;
            CbEmpathy.DisplayMemberPath = "Value";
            CbEmpathy.SelectedValuePath = "Key";

            CbGegariousness.ItemsSource = _alien.GegariousnessnessDict;
            CbGegariousness.DisplayMemberPath = "Value";
            CbGegariousness.SelectedValuePath = "Key";

            CbImagination.ItemsSource = _alien.ImaginationDict;
            CbImagination.DisplayMemberPath = "Value";
            CbImagination.SelectedValuePath = "Key";

            CbChauvinism.ItemsSource = _alien.ChauvinismDict;
            CbChauvinism.DisplayMemberPath = "Value";
            CbChauvinism.SelectedValuePath = "Key";

            CbSuspicion.ItemsSource = _alien.SuspicionDict;
            CbSuspicion.DisplayMemberPath = "Value";
            CbSuspicion.SelectedValuePath = "Key";

            CbPlayfulness.ItemsSource = _alien.PlayfulnessDict;
            CbPlayfulness.DisplayMemberPath = "Value";
            CbPlayfulness.SelectedValuePath = "Key";
        }

        private static BitmapImage BuildImageFromByteArray(byte[] array)
        {
            using (var ms = new MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        private static byte[] BuildByteArrayFromImage(BitmapSource image)
        {
            byte[] data;
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        private void SendToClients_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Send!");
            SWNService.CurrentService.SendImage(_loadedAlien.Image);
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Db1Entities())
            {
                var updateAlien = (from c in context.Aliens where c.Id == _loadedAlien.Id select c).FirstOrDefault();
                if (updateAlien != null)
                {
                    updateAlien.Name = TbName.Text;
                    updateAlien.chemicalBasis = CbChemicalBasis.SelectedValue.ToString();
                    updateAlien.LandOrWater = CbLandOrWater.SelectedValue.ToString();
                    updateAlien.LandHabitat = CbLandHabitat.SelectedValue.ToString();
                    updateAlien.WaterHabitat = CbWaterHabitat.SelectedValue.ToString();
                    updateAlien.TrophicDiet = CbThrophicDiet.SelectedValue.ToString();
                    updateAlien.PrimaryLocomotion = CbPrimaryLocomotion.SelectedValue.ToString();
                    updateAlien.SecondaryLocomotion = CbSecondaryLocomotion.SelectedValue.ToString();
                    updateAlien.hasSecondaryLocomotuib = CheckHasSecondaryLocmotion.IsChecked;
                    updateAlien.Gravity = null; //FIXME
                    updateAlien.SizeClass = CbSizeClass.SelectedValue.ToString();
                    updateAlien.Size = double.Parse(CbSize.Text);
                    updateAlien.Symmetry = CbSymmetry.SelectedValue.ToString();
                    updateAlien.Sides = int.Parse(CbSides.Text);
                    updateAlien.LimbSegments = int.Parse(CbLimbSegments.Text);
                    updateAlien.Tail = CbTails.SelectedValue.ToString();
                    updateAlien.Manipulators = int.Parse(CbManipulators.Text);
                    updateAlien.Skeleton = CbSkeleton.SelectedValue.ToString();
                    updateAlien.SkinClass = CbSkinClass.SelectedValue.ToString();
                    updateAlien.Skin = CbSkin.SelectedValue.ToString();
                    updateAlien.Breathing = CbBreathing.SelectedValue.ToString();
                    updateAlien.Temperatur = CbTemperture.SelectedValue.ToString();
                    updateAlien.Growth = CbGrowthRate.SelectedValue.ToString();
                    updateAlien.Sex = CbSexes.SelectedValue.ToString();
                    updateAlien.Gestation = CbGestation.SelectedValue.ToString();
                    updateAlien.Strategy = CbStrategy.SelectedValue.ToString();
                    updateAlien.OffspringCount = int.Parse(CbOffspringCount.Text);
                    updateAlien.PrimarySense = CbPrimarySense.SelectedValue.ToString();
                    updateAlien.Vision = CbVision.SelectedValue.ToString();
                    updateAlien.Hearing = CbHearing.SelectedValue.ToString();
                    updateAlien.Touch = CbTouch.SelectedValue.ToString();
                    updateAlien.TasteSmell = CbTasteSmell.SelectedValue.ToString();
                    updateAlien.Intelligence = CbIntelligence.SelectedValue.ToString();
                    updateAlien.IntelligenceValue = int.Parse(CbIntelligenceValue.Text);
                    updateAlien.MatingBehaviour = CbMatingBehaviour.SelectedValue.ToString();
                    updateAlien.SocialOrganization = CbSocialOrganization.SelectedValue.ToString();
                    updateAlien.SocialGroupSize = int.Parse(CbSocialGroupSize.Text);
                    updateAlien.Concentration = CbConcentration.SelectedValue.ToString();
                    updateAlien.Curiosity = CbCuriosity.SelectedValue.ToString();
                    updateAlien.Egoism = CbEgoism.SelectedValue.ToString();
                    updateAlien.Empathy = CbEmpathy.SelectedValue.ToString();
                    updateAlien.Gegariousness = CbGegariousness.SelectedValue.ToString();
                    updateAlien.Imagination = CbImagination.SelectedValue.ToString();
                    updateAlien.Chauvinism = CbChauvinism.SelectedValue.ToString();
                    updateAlien.Suspicion = CbSuspicion.SelectedValue.ToString();
                    updateAlien.Playfulness = CbPlayfulness.SelectedValue.ToString();

                    if (RaceImageWindow.Source != null)
                    {
                        if (!_loadedAlien.Image.SequenceEqual(BuildByteArrayFromImage(_raceBitmapImage)))
                        {
                            byte[] buffer;
                            var fileStream = new FileStream(_imagePath, FileMode.Open, FileAccess.Read);
                            try
                            {
                                var length = (int) fileStream.Length; // get file length
                                buffer = new byte[length]; // create buffer
                                int count; // actual number of bytes read
                                var sum = 0; // total number of bytes read

                                // read until Read method returns 0 (end of the stream has been reached)
                                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                                    sum += count; // sum is a buffer offset for next reading
                            }
                            finally
                            {
                                fileStream.Close();
                            }
                            updateAlien.Image = buffer;
                        }
                    }
                    context.Entry(updateAlien).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
            MessageBox.Show("The Alien '" + TbName.Text + "' has been Updated");
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Db1Entities())
            {
                var delAlien = (from c in context.Aliens where c.Id == _loadedAlien.Id select c).FirstOrDefault();
                context.Entry(delAlien).State = EntityState.Deleted;
                context.SaveChanges();
                MessageBox.Show("'" + _loadedAlien.Name + "' has been removed from the Database");
                ClearControls();
            }
        }

        private void ClearControls()
        {
            CbChemicalBasis.SelectedValue = null;
            CbLandOrWater.SelectedValue = null;
            CbLandHabitat.SelectedValue = null;
            CbWaterHabitat.SelectedValue = null;
            CbThrophicDiet.SelectedValue = null;
            CbPrimaryLocomotion.SelectedValue = null;
            CbSecondaryLocomotion.SelectedValue = null;
            CbSizeClass.SelectedValue = null;
            CbSize.Text = null;
            CbSymmetry.SelectedValue = null;
            CbSides.Text = null;
            CbLimbSegments.Text = null;
            CbTails.SelectedValue = null;
            CbManipulators.Text = null;
            CbSkeleton.SelectedValue = null;
            CbSkinClass.SelectedValue = null;
            CbSkin.SelectedValue = null;
            CbBreathing.SelectedValue = null;
            CbTemperture.SelectedValue = null;
            CbGrowthRate.SelectedValue = null;
            CbSexes.SelectedValue = null;
            CbGestation.SelectedValue = null;
            CbStrategy.SelectedValue = null;
            CbOffspringCount.Text = null;
            CbPrimarySense.SelectedValue = null;
            CbVision.SelectedValue = null;
            CbHearing.SelectedValue = null;
            CbTouch.SelectedValue = null;
            CbTasteSmell.SelectedValue = null;
            CbIntelligence.SelectedValue = null;
            CbIntelligenceValue.Text = null;
            CbMatingBehaviour.SelectedValue = null;
            CbSocialOrganization.SelectedValue = null;
            CbSocialGroupSize.Text = null;
            CbConcentration.SelectedValue = null;
            CbCuriosity.SelectedValue = null;
            CbEgoism.SelectedValue = null;
            CbEmpathy.SelectedValue = null;
            CbGegariousness.SelectedValue = null;
            CbImagination.SelectedValue = null;
            CbChauvinism.SelectedValue = null;
            CbSuspicion.SelectedValue = null;
            CbPlayfulness.SelectedValue = null;
            _raceBitmapImage = null;
            RaceImageWindow.Source = null;
            TbName.Text = "";
        }

        private void btClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }
    }
}