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
using Xceed.Wpf.Toolkit;

namespace SWN
{
    /// <summary>
    /// Interaction logic for Create_New_Character.xaml
    /// </summary>
    public partial class CreateNewCharacter : Window
    {
        private int Points;

        public int points
        {
            get { return Points; }
            set
            {
                Points = value;
                OnPointsChange();
            }
        }

        public CreateNewCharacter(SWNServiceReference.Character c)
        {
            InitializeComponent();
            InitializeSheet(c);
            this.DataContext = c;
            Points = (int)c.PointTotal;
        }

        private void OnPointsChange()
        {
            tbPoints.Text = Points.ToString();
        }

        private void InitializeSheet(SWNServiceReference.Character c)
        {

        }

        //Strenght

        private void iudStrenght_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.OldValue != null)
            {
                if ((int)e.OldValue < (int)e.NewValue)
                {
                    BuyStrenght();
                    UpdateBasicLift((int)e.NewValue);
                    UpdateHitpoints(1);
                }
                else
                {
                    SellStrenght();
                    UpdateBasicLift((int)e.NewValue);
                    UpdateHitpoints(-1);
                }
            }
        }

        private void BuyStrenght()
        {
            //+-10 Points/Level
            points = points - 10;
            //Reset Hitpoints-Bought
            int hitpointvalue = (int)iudHitPoints.Value;
            for (int i = (int)iudStrenght.Value; i < hitpointvalue - 1; i++)
            {
                BuyHitpoints();
                UpdateHitpoints(1);
            }
        }

        private void SellStrenght()
        {
            //+-10 Points/Level
            points = points + 10;
            //Reset Hitpoints-Bought
            int hitpointvalue = (int)iudHitPoints.Value;
            for (int i = (int)iudStrenght.Value; i < hitpointvalue -1; i++)
            {
                SellHitpoints();
                UpdateHitpoints(-1);
            }
        }

        //Dexterity

        private void iudDexterity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.OldValue != null)
            {
                if ((int)e.OldValue < (int)e.NewValue)
                {
                    BuyDexterity();
                }
                else
                {
                    SellDexterity();
                }
            }
        }
        private void BuyDexterity()
        {
            //+-20 Points/Level
            points = points - 20;
        }

        private void SellDexterity()
        {
            //+-20 Points/Level
            points = points + 20;
        }

        //Intelligence

        private void iudIntelligence_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.OldValue != null)
            {
                if ((int)e.OldValue < (int)e.NewValue)
                {
                    BuyIntelligence();
                }
                else
                {
                    SellIntelligence();
                }
            }
        }
        private void BuyIntelligence()
        {
            //+-20 Points/Level
            points = points - 20;
        }

        private void SellIntelligence()
        {
            //+-20 Points/Level
            points = points + 20;
        }

        //Health

        private void iudHealth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.OldValue != null)
            {
                if ((int)e.OldValue < (int)e.NewValue)
                {
                    BuyHealth();
                }
                else
                {
                    SellHealth();
                }
            }
        }

        private void BuyHealth()
        {
            //+-10 Points/Level
            points = points - 10;
        }

        private void SellHealth()
        {
            //+-10 Points/Level
            points = points + 10;
        }

        //BasicLift

        private void UpdateBasicLift(int newStrenght)
        {
            tbBasicLift.Text = Math.Floor((Double)(((newStrenght * newStrenght) / 5)/ 2)).ToString();
        }

        //Hitpoints

        private void iudHitPoints_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            int StrenghtPercent = 30;
            int CalcValueLowEnd = (int)(((Double)iudStrenght.Value / 100) * (100 - StrenghtPercent));
            int CalcValueHighEnd = (int)(((Double)iudStrenght.Value / 100) * (100 + StrenghtPercent));
            if (e.OldValue != null)
            {
                if ((int)e.OldValue < (int)e.NewValue)
                {
                    if ((int)e.NewValue > CalcValueHighEnd)
                    {
                        System.Windows.MessageBox.Show("Can not deviate from the Main Stat: Strenght by more then " + StrenghtPercent + " Percent!");
                        UpdateHitpoints(-1);
                    }
                    else
                    {
                        BuyHitpoints();
                    }
                }
                else
                {
                    if ((int)e.NewValue < CalcValueLowEnd)
                    {
                        System.Windows.MessageBox.Show("Can not deviate from the Main Stat: Strenght by more then " + StrenghtPercent + " Percent!");
                        UpdateHitpoints(+1);
                    }
                    else
                    {
                        SellHitpoints();
                    }
                }
            }
        }

        private void BuyHitpoints()
        {
            //+-2 Points per +- 1 Hp
            points = points - 2;
        }

        private void SellHitpoints()
        {
            //+-2 Points per +- 1 Hp
            points = points + 2;
        }

        private void UpdateHitpoints(int Value)
        {
            iudHitPoints.ValueChanged -= iudHitPoints_ValueChanged;
            iudHitPoints.Value += Value;
            iudHitPoints.ValueChanged += iudHitPoints_ValueChanged;
        }
    }
}
