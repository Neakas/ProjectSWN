using System;
using System.Data;
using System.Windows;
using SWNAdmin.Utility;
using UniverseGeneration.Stellar_Bodies;
using UniverseGeneration.Utility;

namespace SWNAdmin.Forms
{
    public partial class SystemGeneration
    {
        /// <summary>
        ///     Constructor for the form object.
        /// </summary>
        public SystemGeneration()
        {
            OurSystem = new StarSystem();
            VelvetBag = new Dice();
            InitializeComponent();

            StarTable = new DataTable("StarTable");
            StarTable.Columns.Add("Current Mass (sol mass)", typeof (double));
            StarTable.Columns.Add("Name", typeof (string));
            StarTable.Columns.Add("Order", typeof (string));
            StarTable.Columns.Add("Spectral Type", typeof (string));
            StarTable.Columns.Add("Current Luminosity (sol lumin)", typeof (double));
            StarTable.Columns.Add("Effective Temperature(K)", typeof (double));
            StarTable.Columns.Add("Orbital Radius (AU)", typeof (double));
            StarTable.Columns.Add("Gas Giant", typeof (string));
            StarTable.Columns.Add("Color", typeof (string));
            StarTable.Columns.Add("Stellar Evolution Stage", typeof (string));
            StarTable.Columns.Add("Flare Star", typeof (string));
            StarTable.Columns.Add("Orbital Details", typeof (string));

            PlanetTable = new DataTable("PlanetTable");
            PlanetTable.Columns.Add("Name", typeof (string));
            PlanetTable.Columns.Add("Size (Type)", typeof (string));
            PlanetTable.Columns.Add("Diameter (KM)", typeof (double));
            PlanetTable.Columns.Add("Orbital Radius (AU)", typeof (double));
            PlanetTable.Columns.Add("Gravity (m/s)", typeof (double));
            PlanetTable.Columns.Add("Atmosphere Pressure (atm)", typeof (string));
            PlanetTable.Columns.Add("Atmosphere Notes", typeof (string));
            PlanetTable.Columns.Add("Hydrographic Coverage", typeof (string));
            PlanetTable.Columns.Add("Climate Data", typeof (string));
            PlanetTable.Columns.Add("Resource Indicator", typeof (string));

            CreateStarsFinished = false;
            CreatePlanetsFinished = false;
            DgvPlanets.ItemsSource = PlanetTable.AsDataView();
            DgvStars.ItemsSource = StarTable.AsDataView();
        }

        /// <summary>
        ///     This is our star system. There are many like it, but this is ours.
        ///     Used to keep all of our details
        /// </summary>
        public StarSystem OurSystem { get; set; }

        /// <summary>
        ///     The Ddice this program uses. Our PRNG.
        /// </summary>
        public Dice VelvetBag { get; set; }

        /// <summary>
        ///     The star display table. Used to abstract things.
        /// </summary>
        private DataTable StarTable { get; }

        /// <summary>
        ///     The plaent display table. Used to abstract things.
        /// </summary>
        private DataTable PlanetTable { get; }

        /// <summary>
        ///     This is used to tell the parent form (this form) we're done with star generation
        /// </summary>
        public bool CreateStarsFinished { get; set; }

        /// <summary>
        ///     This is used to tell the parent form (this form) we're done with planet generation.
        /// </summary>
        public bool CreatePlanetsFinished { get; set; }

        /// <summary>
        ///     This refreshes the DataGridView for displaying stars
        /// </summary>
        /// <summary>
        ///     Begin Step 1 - generating the base system and stars, then displays them
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event arguments</param>
        private void btnGenStars_Click( object sender, EventArgs e )
        {
            CreateStarsFinished = false;

            //clear the tables.
            if (OurSystem.CountStars() > 0)
            {
                OurSystem.ClearPlanets();
                PlanetTable.Clear();

                OurSystem.SysStars.Clear();
                StarTable.Clear();
            }

            var nCs = new CreateStars(OurSystem, VelvetBag, this);

            //register a closed event here.
            nCs.Closing += createStars_Closed;
            nCs.ShowDialog();
        }

        /// <summary>
        ///     Resets the system.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event arguments</param>
        private void btnReset_Click( object sender, EventArgs e )
        {
            OurSystem.ResetSystem();
            LblSysAge.Content = "";
            LblSysName.Content = "";

            OurSystem.SysStars.Clear();
            StarTable.Clear();

            LblNumberPlanets.Content = "";
            PlanetTable.Clear();
        }

        /// <summary>
        ///     The object called when the create stars form is closed. Checks to see if we should update the listing
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event arguments</param>
        private void createStars_Closed( object sender, EventArgs e )
        {
            if (CreateStarsFinished)
            {
                foreach (var s in OurSystem.SysStars)
                {
                    var rowVal = new object[12];
                    rowVal[0] = s.CurrMass;
                    rowVal[1] = s.Name;
                    rowVal[2] = Star.GetDescFromFlag(s.SelfId);
                    rowVal[3] = s.SpecType;
                    rowVal[4] = Math.Round(s.CurrLumin, 4);
                    rowVal[5] = s.EffTemp;
                    rowVal[6] = s.OrbitalRadius;
                    rowVal[7] = Star.DescGasGiantFlag(s.GasGiantFlag);
                    rowVal[8] = s.StarColor;
                    rowVal[9] = s.ReturnCurrentBranchDesc();
                    rowVal[10] = s.IsFlareStar;
                    rowVal[11] = s.PrintOrbitalDetails();

                    StarTable.Rows.Add(rowVal);
                }

                LblSysAge.Content = OurSystem.SysAge + " GYr";
                LblSysName.Content = OurSystem.SysName;
            }
        }

        /// <summary>
        ///     The object called when the create planets form is closed. Checks to see if we should update the listing
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event arguments</param>
        private void createPlanets_Closed( object sender, EventArgs e )
        {
            if (CreatePlanetsFinished)
            {
                LblNumberPlanets.Content = OurSystem.CountPlanets().ToString();
                foreach (var s in OurSystem.SysStars)
                {
                    foreach (var pl in s.SysPlanets)
                    {
                        if (pl.BaseType == Satellite.BasetypeEmpty)
                        {
                            continue;
                        }
                        var ourValues = new object[10];
                        ourValues[0] = pl.Name;

                        if (OptionCont.ExpandAsteroidBelt != null && ( pl.BaseType != Satellite.BasetypeAsteroidbelt || (bool) OptionCont.ExpandAsteroidBelt ))
                        {
                            ourValues[1] = pl.DescSizeType();
                        }
                        if (pl.BaseType == Satellite.BasetypeAsteroidbelt)
                        {
                            ourValues[1] = "Asteroid Belt";
                        }

                        ourValues[2] = Math.Round(pl.DiameterInKm(), 2);
                        ourValues[3] = Math.Round(pl.OrbitalRadius, 2);
                        ourValues[4] = Math.Round(pl.Gravity * Satellite.Gforce, 2);

                        if (pl.BaseType == Satellite.BasetypeAsteroidbelt)
                        {
                            ourValues[5] = "None.";
                        }

                        if (pl.BaseType == Satellite.BasetypeGasgiant)
                        {
                            ourValues[5] = "Superdense Atmosphere.";
                        }

                        if (pl.BaseType == Satellite.BasetypeMoon || pl.BaseType == Satellite.BasetypeTerrestial)
                        {
                            ourValues[5] = pl.GetDescAtmCategory() + "(" + Math.Round(pl.AtmPres, 2) + ")";
                        }

                        ourValues[6] = pl.DescAtm();
                        ourValues[7] = pl.HydCoverage * 100 + "%";

                        if (pl.BaseType == Satellite.BasetypeMoon || pl.BaseType == Satellite.BasetypeTerrestial)
                        {
                            ourValues[8] = pl.GetClimateDesc(pl.GetClimate(pl.SurfaceTemp)) + "( " + Math.Round(pl.SurfaceTemp, 2) + "K/ " + Math.Round(LibStarGen.ConvertTemp("kelvin", "celsius", pl.SurfaceTemp), 2) + "C)";
                        }
                        else
                        {
                            ourValues[8] = "Blackbody Temperature: " + Math.Round(pl.BlackbodyTemp, 2) + "K";
                        }

                        ourValues[9] = pl.GetRvmDesc();

                        PlanetTable.Rows.Add(ourValues);
                    }
                }
            }
        }

        /// <summary>
        ///     Starts the planetary generator
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event arguments</param>
        private void btnGenPlanets_Click( object sender, EventArgs e )
        {
            CreatePlanetsFinished = false;

            //clear the tables.
            if (OurSystem.CountPlanets() > 0)
            {
                OurSystem.ClearPlanets();
                PlanetTable.Clear();
            }

            var pCs = new CreatePlanets(OurSystem, VelvetBag, this);

            //register a closed event here.
            pCs.Closing += createPlanets_Closed;
            pCs.ShowDialog();
        }

        private void btnGenFull_Click( object sender, EventArgs e )
        {
            var amount = Convert.ToInt32(TbAmount.Text);

            var mbr = MessageBox.Show("Generate '" + amount + "' Systems?", "Sure?", MessageBoxButton.YesNo);
            if (mbr != MessageBoxResult.Yes)
            {
                return;
            }
            var start = DateTime.Now;
            for (var i = 0; i < amount; i++)
            {
                var ss = new StarSystem();
                var d = new Dice();
                var cs = new CreateStars(ss, d);
                ss = cs.CreateNewSystem();
                SqlManager.InsertSystem(ss);
            }
            var finish = DateTime.Now;
            var span = finish - start;
            MessageBox.Show("Done! Took: " + span.Seconds + "s and " + span.Milliseconds + "ms");
        }
    }
}