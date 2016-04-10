using System;
using System.Linq;
using SWNAdmin.Classes;
using SWNAdmin.Utility;

namespace SWNAdmin.Controller
{
    public static class CharacterController
    {
        public static Character GetBlankCharacter( Client client )
        {
            var c = new Character
            {
                Age = 18,
                Name = "",
                Strenght = 10,
                Intelligence = 10,
                Dexterity = 10,
                Health = 10
            };
            if (c.Strenght != null)
            {
                c.BasicLift = CalcBasicLift((int) c.Strenght);
                c.HitPoints = CalcHp((int) c.Strenght);
            }
            if (c.Intelligence != null)
            {
                c.WillPower = CalcWillPower((int) c.Intelligence);
                c.Perception = CalcPerception((int) c.Intelligence);
            }
            if (c.Health != null)
            {
                c.FatiguePoints = CalcBaseFatiguePoints((int) c.Health);
                if (c.Dexterity != null)
                {
                    c.BasicSpeed = CalcBasicSpeed((int) c.Health, (int) c.Dexterity);
                }
            }
            if (c.BasicSpeed != null)
            {
                c.BasicMove = CalcBasicMove((double) c.BasicSpeed);
            }
            c.PlayerName = client.UserName;
            c.PointTotal = 100;
            var context = new Db1Entities();
            c.PlayerId = ( from cn in context.Registration where cn.Username == client.UserName select cn.Id ).FirstOrDefault();
            return c;
        }

        public static int CalcHp( int strenght )
        {
            return strenght;
        }

        public static int CalcWillPower( int intelligence )
        {
            return intelligence;
        }

        public static int CalcPerception( int intelligence )
        {
            return intelligence;
        }

        public static int CalcBaseFatiguePoints( int health )
        {
            return health;
        }

        public static int CalcBasicLift( int strenght )
        {
            return strenght * strenght / 5;
        }

        public static double CalcBasicSpeed( int health, int dexterity )
        {
            return ( health + dexterity ) / 4.00;
        }

        public static int CalcBasicMove( double basicSpeed )
        {
            return (int) Math.Floor(basicSpeed);
        }
    }
}