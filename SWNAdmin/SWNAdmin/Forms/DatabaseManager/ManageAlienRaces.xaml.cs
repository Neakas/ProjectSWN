using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        string ImagePath = "";

        public Alien alien;
        public Utility.Aliens LoadedAlien;
        public ListBox LoadBox;
        public Window LoadWindow;
        public BitmapImage RaceBitmapImage;
        public ManageAlienRaces CurrentInstance;

        public ManageAlienRaces()
        {
            CurrentInstance = this;
            InitializeComponent();
            ContextMenu ImageContextMenu = new ContextMenu();
            RaceImageWindow.ContextMenu = ImageContextMenu;
            Button SendToClients = new Button();
            SendToClients.Content = "Send to Clients";
            ImageContextMenu.Items.Add(SendToClients);
            SendToClients.Click += SendToClients_Click;
        }

        private void btgenTest_Click(object sender, RoutedEventArgs e)
        {

            btSave.IsEnabled = true;
            btSave.Visibility = Visibility.Visible;
            btUpdate.IsEnabled = false;
            btUpdate.Visibility = Visibility.Hidden;
            btDelete.IsEnabled = false;
            btDelete.Visibility = Visibility.Hidden;

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
                DBAlien.WaterHabitat = cbWaterHabitat.SelectedValue.ToString();
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

                //if (RaceImageWindow.Source != null)
                //{
                //    byte[] buffer;
                //    FileStream fileStream = new FileStream(ImagePath, FileMode.Open, FileAccess.Read);
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
                //    DBAlien.Image = buffer;
                //}

                
                DBAlien.Image = BuildByteArrayFromImage(RaceBitmapImage);

                context.Aliens.Add(DBAlien);
                context.SaveChanges();
            }

            MessageBox.Show("The Alien '" + tbName.Text + "' has been saved in the Database");
        }

        private void btLoadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            ImagePath = ofd.FileName;
            RaceBitmapImage = new BitmapImage(new Uri(ofd.FileName));
            RaceImageWindow.Source = RaceBitmapImage;
        }

        private void btLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadWindow = new Window();
            LoadWindow.Width = 200;
            LoadWindow.Height = 200;
            LoadBox = new ListBox();
            LoadWindow.Content = LoadBox;
            using (var Context = new Utility.Db1Entities())
            {
                LoadBox.ItemsSource = (from c in Context.Aliens select c).ToList();
                LoadBox.DisplayMemberPath = "Name";
                LoadBox.MouseDoubleClick += LoadboxSelectionChanged;
                
            }
            LoadWindow.ShowDialog();
        }

        private void LoadboxSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (LoadBox.SelectedItem != null)
            {
                LoadedAlien = (Utility.Aliens)LoadBox.SelectedItem;
                LoadWindow.Close();
                LoadAlien();
            }
        }

        private void LoadAlien()
        {
            btUpdate.Visibility = Visibility.Visible;
            btUpdate.IsEnabled = true;
            btSave.IsEnabled = false;
            btSave.Visibility = Visibility.Hidden;
            btDelete.Visibility = Visibility.Visible;
            btDelete.IsEnabled = true;

            alien = new Alien();
            tbName.Text = LoadedAlien.Name;
            RaceImageWindow.Source = BuildImageFromByteArray(LoadedAlien.Image);

            cbChemicalBasis.SelectedValue = LoadedAlien.chemicalBasis;
            cbLandOrWater.SelectedValue = LoadedAlien.LandOrWater;
            cbLandHabitat.SelectedValue = LoadedAlien.LandHabitat;
            cbWaterHabitat.SelectedValue = LoadedAlien.WaterHabitat;
            cbThrophicDiet.SelectedValue = LoadedAlien.TrophicDiet;
            cbPrimaryLocomotion.SelectedValue = LoadedAlien.PrimaryLocomotion;
            cbSecondaryLocomotion.SelectedValue = LoadedAlien.SecondaryLocomotion;
            checkHasSecondaryLocmotion.IsChecked = LoadedAlien.hasSecondaryLocomotuib;
            cbSizeClass.SelectedValue = LoadedAlien.SizeClass;
            cbSize.Text = LoadedAlien.Size.ToString();
            cbSymmetry.SelectedValue = LoadedAlien.Symmetry;
            cbSides.Text = LoadedAlien.Sides.ToString();
            cbLimbSegments.Text = LoadedAlien.LimbSegments.ToString();
            cbTails.SelectedValue = LoadedAlien.Tail;
            cbManipulators.Text = LoadedAlien.Manipulators.ToString();
            cbSkeleton.SelectedValue = LoadedAlien.Skeleton;
            cbSkinClass.SelectedValue = LoadedAlien.SkinClass;
            cbSkin.SelectedValue = LoadedAlien.Skin;
            cbBreathing.SelectedValue = LoadedAlien.Breathing;
            cbTemperture.SelectedValue = LoadedAlien.Temperatur;
            cbGrowthRate.SelectedValue = LoadedAlien.Growth;
            cbSexes.SelectedValue = LoadedAlien.Sex;
            cbGestation.SelectedValue = LoadedAlien.Gestation;
            cbStrategy.SelectedValue = LoadedAlien.Strategy;
            cbOffspringCount.Text = LoadedAlien.OffspringCount.ToString();
            cbPrimarySense.SelectedValue = LoadedAlien.PrimarySense;
            cbVision.SelectedValue = LoadedAlien.Vision;
            cbHearing.SelectedValue = LoadedAlien.Hearing;
            cbTouch.SelectedValue = LoadedAlien.Touch;
            cbTasteSmell.SelectedValue = LoadedAlien.TasteSmell;
            cbIntelligence.SelectedValue = LoadedAlien.Intelligence;
            cbIntelligenceValue.Text = LoadedAlien.IntelligenceValue.ToString();
            cbMatingBehaviour.SelectedValue = LoadedAlien.MatingBehaviour;
            cbSocialOrganization.SelectedValue = LoadedAlien.SocialOrganization;
            cbSocialGroupSize.Text = LoadedAlien.SocialGroupSize.ToString();
            cbConcentration.SelectedValue = LoadedAlien.Concentration;
            cbCuriosity.SelectedValue = LoadedAlien.Curiosity;
            cbEgoism.SelectedValue = LoadedAlien.Egoism;
            cbEmpathy.SelectedValue = LoadedAlien.Empathy;
            cbGegariousness.SelectedValue = LoadedAlien.Gegariousness;
            cbImagination.SelectedValue = LoadedAlien.Imagination;
            cbChauvinism.SelectedValue = LoadedAlien.Chauvinism;
            cbSuspicion.SelectedValue = LoadedAlien.Suspicion;
            cbPlayfulness.SelectedValue = LoadedAlien.Playfulness;

            cbChemicalBasis.ItemsSource = alien.TypesofLifeDict;
            cbChemicalBasis.DisplayMemberPath = "Value";
            cbChemicalBasis.SelectedValuePath = "Key";

            cbLandOrWater.ItemsSource = alien.LandWaterDict;
            cbLandOrWater.DisplayMemberPath = "Value";
            cbLandOrWater.SelectedValuePath = "Key";

            cbLandHabitat.ItemsSource = alien.TypesofLandHabitatsDict;
            cbLandHabitat.DisplayMemberPath = "Value";
            cbLandHabitat.SelectedValuePath = "Key";

            cbWaterHabitat.ItemsSource = alien.TypesofWaterHabitatsDict;
            cbWaterHabitat.DisplayMemberPath = "Value";
            cbWaterHabitat.SelectedValuePath = "Key";

            cbThrophicDiet.ItemsSource = alien.TrophicDietDict;
            cbThrophicDiet.DisplayMemberPath = "Value";
            cbThrophicDiet.SelectedValuePath = "Key";

            cbPrimaryLocomotion.ItemsSource = alien.LocomotionDict;
            cbPrimaryLocomotion.DisplayMemberPath = "Value";
            cbPrimaryLocomotion.SelectedValuePath = "Key";

            cbSecondaryLocomotion.ItemsSource = alien.LocomotionDict;
            cbSecondaryLocomotion.DisplayMemberPath = "Value";
            cbSecondaryLocomotion.SelectedValuePath = "Key";


            cbSizeClass.ItemsSource = alien.SizeClassDict;
            cbSizeClass.DisplayMemberPath = "Value";
            cbSizeClass.SelectedValuePath = "Key";

            cbSymmetry.ItemsSource = alien.SymmetryDict;
            cbSymmetry.DisplayMemberPath = "Value";
            cbSymmetry.SelectedValuePath = "Key";

            cbTails.ItemsSource = alien.TailsDict;
            cbTails.DisplayMemberPath = "Value";
            cbTails.SelectedValuePath = "Key";


            cbSkeleton.ItemsSource = alien.SkeletonDict;
            cbSkeleton.DisplayMemberPath = "Value";
            cbSkeleton.SelectedValuePath = "Key";

            cbSkinClass.ItemsSource = alien.SkinTypeDict;
            cbSkinClass.DisplayMemberPath = "Value";
            cbSkinClass.SelectedValuePath = "Key";

            cbSkin.ItemsSource = alien.SkinDict;
            cbSkin.DisplayMemberPath = "Value";
            cbSkin.SelectedValuePath = "Key";

            cbBreathing.ItemsSource = alien.BreathingMethodDict;
            cbBreathing.DisplayMemberPath = "Value";
            cbBreathing.SelectedValuePath = "Key";

            cbTemperture.ItemsSource = alien.TemperatureDict;
            cbTemperture.DisplayMemberPath = "Value";
            cbTemperture.SelectedValuePath = "Key";

            cbGrowthRate.ItemsSource = alien.GrowthDict;
            cbGrowthRate.DisplayMemberPath = "Value";
            cbGrowthRate.SelectedValuePath = "Key";

            cbSexes.ItemsSource = alien.SexesDict;
            cbSexes.DisplayMemberPath = "Value";
            cbSexes.SelectedValuePath = "Key";

            cbGestation.ItemsSource = alien.GestationDict;
            cbGestation.DisplayMemberPath = "Value";
            cbGestation.SelectedValuePath = "Key";

            cbStrategy.ItemsSource = alien.StrategyDict;
            cbStrategy.DisplayMemberPath = "Value";
            cbStrategy.SelectedValuePath = "Key";

            cbPrimarySense.ItemsSource = alien.PrimarySenseDict;
            cbPrimarySense.DisplayMemberPath = "Value";
            cbPrimarySense.SelectedValuePath = "Key";

            cbVision.ItemsSource = alien.VisionDict;
            cbVision.DisplayMemberPath = "Value";
            cbVision.SelectedValuePath = "Key";

            cbHearing.ItemsSource = alien.HearingDict;
            cbHearing.DisplayMemberPath = "Value";
            cbHearing.SelectedValuePath = "Key";

            cbTouch.ItemsSource = alien.TouchDict;
            cbTouch.DisplayMemberPath = "Value";
            cbTouch.SelectedValuePath = "Key";

            cbTasteSmell.ItemsSource = alien.TasteSmellDict;
            cbTasteSmell.DisplayMemberPath = "Value";
            cbTasteSmell.SelectedValuePath = "Key";

            cbIntelligence.ItemsSource = alien.IntelligenceDict;
            cbIntelligence.DisplayMemberPath = "Value";
            cbIntelligence.SelectedValuePath = "Key";

            cbIntelligenceValue.Text = alien.IntelligenceValue.ToString();

            cbMatingBehaviour.ItemsSource = alien.MatingBahaviourDict;
            cbMatingBehaviour.DisplayMemberPath = "Value";
            cbMatingBehaviour.SelectedValuePath = "Key";

            cbSocialOrganization.ItemsSource = alien.SocialOrganizationDict;
            cbSocialOrganization.DisplayMemberPath = "Value";
            cbSocialOrganization.SelectedValuePath = "Key";

            cbConcentration.ItemsSource = alien.ConcentrationDict;
            cbConcentration.DisplayMemberPath = "Value";
            cbConcentration.SelectedValuePath = "Key";

            cbCuriosity.ItemsSource = alien.CuriosityDict;
            cbCuriosity.DisplayMemberPath = "Value";
            cbCuriosity.SelectedValuePath = "Key";

            cbEgoism.ItemsSource = alien.EgoismDict;
            cbEgoism.DisplayMemberPath = "Value";
            cbEgoism.SelectedValuePath = "Key";

            cbEmpathy.ItemsSource = alien.EmpathyDict;
            cbEmpathy.DisplayMemberPath = "Value";
            cbEmpathy.SelectedValuePath = "Key";

            cbGegariousness.ItemsSource = alien.GegariousnessnessDict;
            cbGegariousness.DisplayMemberPath = "Value";
            cbGegariousness.SelectedValuePath = "Key";

            cbImagination.ItemsSource = alien.ImaginationDict;
            cbImagination.DisplayMemberPath = "Value";
            cbImagination.SelectedValuePath = "Key";

            cbChauvinism.ItemsSource = alien.ChauvinismDict;
            cbChauvinism.DisplayMemberPath = "Value";
            cbChauvinism.SelectedValuePath = "Key";

            cbSuspicion.ItemsSource = alien.SuspicionDict;
            cbSuspicion.DisplayMemberPath = "Value";
            cbSuspicion.SelectedValuePath = "Key";

            cbPlayfulness.ItemsSource = alien.PlayfulnessDict;
            cbPlayfulness.DisplayMemberPath = "Value";
            cbPlayfulness.SelectedValuePath = "Key";
        }

        public BitmapImage BuildImageFromByteArray(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        public byte[] BuildByteArrayFromImage(BitmapImage Image)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(Image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        public void SendToClients_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Send!");
            SWNService.CurrentService.SendImage(LoadedAlien.Image);
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Utility.Db1Entities())
            {
                Utility.Aliens UpdateAlien = (from c in context.Aliens where c.Id == LoadedAlien.Id select c).FirstOrDefault();
                UpdateAlien.Name = tbName.Text;
                UpdateAlien.chemicalBasis = cbChemicalBasis.SelectedValue.ToString();
                UpdateAlien.LandOrWater = cbLandOrWater.SelectedValue.ToString();
                UpdateAlien.LandHabitat = cbLandHabitat.SelectedValue.ToString();
                UpdateAlien.WaterHabitat = cbWaterHabitat.SelectedValue.ToString();
                UpdateAlien.TrophicDiet = cbThrophicDiet.SelectedValue.ToString();
                UpdateAlien.PrimaryLocomotion = cbPrimaryLocomotion.SelectedValue.ToString();
                UpdateAlien.SecondaryLocomotion = cbSecondaryLocomotion.SelectedValue.ToString();
                UpdateAlien.hasSecondaryLocomotuib = checkHasSecondaryLocmotion.IsChecked;
                UpdateAlien.Gravity = null; //FIXME
                UpdateAlien.SizeClass = cbSizeClass.SelectedValue.ToString();
                UpdateAlien.Size = Double.Parse(cbSize.Text);
                UpdateAlien.Symmetry = cbSymmetry.SelectedValue.ToString();
                UpdateAlien.Sides = Int32.Parse(cbSides.Text);
                UpdateAlien.LimbSegments = Int32.Parse(cbLimbSegments.Text);
                UpdateAlien.Tail = cbTails.SelectedValue.ToString();
                UpdateAlien.Manipulators = Int32.Parse(cbManipulators.Text);
                UpdateAlien.Skeleton = cbSkeleton.SelectedValue.ToString();
                UpdateAlien.SkinClass = cbSkinClass.SelectedValue.ToString();
                UpdateAlien.Skin = cbSkin.SelectedValue.ToString();
                UpdateAlien.Breathing = cbBreathing.SelectedValue.ToString();
                UpdateAlien.Temperatur = cbTemperture.SelectedValue.ToString();
                UpdateAlien.Growth = cbGrowthRate.SelectedValue.ToString();
                UpdateAlien.Sex = cbSexes.SelectedValue.ToString();
                UpdateAlien.Gestation = cbGestation.SelectedValue.ToString();
                UpdateAlien.Strategy = cbStrategy.SelectedValue.ToString();
                UpdateAlien.OffspringCount = Int32.Parse(cbOffspringCount.Text);
                UpdateAlien.PrimarySense = cbPrimarySense.SelectedValue.ToString();
                UpdateAlien.Vision = cbVision.SelectedValue.ToString();
                UpdateAlien.Hearing = cbHearing.SelectedValue.ToString();
                UpdateAlien.Touch = cbTouch.SelectedValue.ToString();
                UpdateAlien.TasteSmell = cbTasteSmell.SelectedValue.ToString();
                UpdateAlien.Intelligence = cbIntelligence.SelectedValue.ToString();
                UpdateAlien.IntelligenceValue = Int32.Parse(cbIntelligenceValue.Text);
                UpdateAlien.MatingBehaviour = cbMatingBehaviour.SelectedValue.ToString();
                UpdateAlien.SocialOrganization = cbSocialOrganization.SelectedValue.ToString();
                UpdateAlien.SocialGroupSize = Int32.Parse(cbSocialGroupSize.Text);
                UpdateAlien.Concentration = cbConcentration.SelectedValue.ToString();
                UpdateAlien.Curiosity = cbCuriosity.SelectedValue.ToString();
                UpdateAlien.Egoism = cbEgoism.SelectedValue.ToString();
                UpdateAlien.Empathy = cbEmpathy.SelectedValue.ToString();
                UpdateAlien.Gegariousness = cbGegariousness.SelectedValue.ToString();
                UpdateAlien.Imagination = cbImagination.SelectedValue.ToString();
                UpdateAlien.Chauvinism = cbChauvinism.SelectedValue.ToString();
                UpdateAlien.Suspicion = cbSuspicion.SelectedValue.ToString();
                UpdateAlien.Playfulness = cbPlayfulness.SelectedValue.ToString();

                if (RaceImageWindow.Source != null)
                {
                    if (!LoadedAlien.Image.SequenceEqual(BuildByteArrayFromImage(RaceBitmapImage)))
                    {
                        byte[] buffer;
                        FileStream fileStream = new FileStream(ImagePath, FileMode.Open, FileAccess.Read);
                        try
                        {
                            int length = (int)fileStream.Length; // get file length
                            buffer = new byte[length]; // create buffer
                            int count; // actual number of bytes read
                            int sum = 0; // total number of bytes read

                            // read until Read method returns 0 (end of the stream has been reached)
                            while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                                sum += count; // sum is a buffer offset for next reading
                        }
                        finally
                        {
                            fileStream.Close();
                        }
                        UpdateAlien.Image = buffer;
                    }  
                }
                context.Entry(UpdateAlien).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            MessageBox.Show("The Alien '" + tbName.Text + "' has been Updated");
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            using (var Context = new Utility.Db1Entities())
            {
                Utility.Aliens delAlien = (from c in Context.Aliens where c.Id == LoadedAlien.Id select c).FirstOrDefault();
                Context.Entry(delAlien).State = System.Data.Entity.EntityState.Deleted;
                Context.SaveChanges();
                MessageBox.Show("'" + LoadedAlien.Name + "' has been removed from the Database");
                ClearControls();
            }
        }

        private void ClearControls()
        {
            cbChemicalBasis.SelectedValue = null;
            cbLandOrWater.SelectedValue = null;
            cbLandHabitat.SelectedValue = null;
            cbWaterHabitat.SelectedValue = null;
            cbThrophicDiet.SelectedValue = null;
            cbPrimaryLocomotion.SelectedValue = null;
            cbSecondaryLocomotion.SelectedValue = null;
            cbSizeClass.SelectedValue = null;
            cbSize.Text = null;
            cbSymmetry.SelectedValue = null;
            cbSides.Text = null;
            cbLimbSegments.Text = null;
            cbTails.SelectedValue = null;
            cbManipulators.Text = null;
            cbSkeleton.SelectedValue = null;
            cbSkinClass.SelectedValue = null;
            cbSkin.SelectedValue = null;
            cbBreathing.SelectedValue = null;
            cbTemperture.SelectedValue = null;
            cbGrowthRate.SelectedValue = null;
            cbSexes.SelectedValue = null;
            cbGestation.SelectedValue = null;
            cbStrategy.SelectedValue = null;
            cbOffspringCount.Text = null;
            cbPrimarySense.SelectedValue = null;
            cbVision.SelectedValue = null;
            cbHearing.SelectedValue = null;
            cbTouch.SelectedValue = null;
            cbTasteSmell.SelectedValue = null;
            cbIntelligence.SelectedValue = null;
            cbIntelligenceValue.Text = null;
            cbMatingBehaviour.SelectedValue = null;
            cbSocialOrganization.SelectedValue = null;
            cbSocialGroupSize.Text = null;
            cbConcentration.SelectedValue = null;
            cbCuriosity.SelectedValue = null;
            cbEgoism.SelectedValue = null;
            cbEmpathy.SelectedValue = null;
            cbGegariousness.SelectedValue = null;
            cbImagination.SelectedValue = null;
            cbChauvinism.SelectedValue = null;
            cbSuspicion.SelectedValue = null;
            cbPlayfulness.SelectedValue = null;
            RaceBitmapImage = null;
            RaceImageWindow.Source = null;
            tbName.Text = "";
        }

        private void btClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }
    }
}
