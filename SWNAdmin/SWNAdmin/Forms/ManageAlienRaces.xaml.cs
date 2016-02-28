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
        public ManageAlienRaces()
        {
            InitializeComponent();
        }

        private void btgenTest_Click(object sender, RoutedEventArgs e)
        {
            Alien alien = new Alien();
            tbChemicalBasis.Text = alien.ChemicalBasis;
            tbHabitat.Text = alien.Habitat;
            tbLandWater.Text = alien.LandWater;
        }
    }
}
