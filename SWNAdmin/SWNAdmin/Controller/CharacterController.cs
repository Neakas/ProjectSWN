using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWNAdmin.Utility;

namespace SWNAdmin.Controller
{
    public static class CharacterController
    {
        public static Character GetBlankCharacter(Client client)
        {
            Character c = new Character();
            c.Age = 18;
            c.Name = "";
            c.Strenght = 10;
            c.Intelligence = 10;
            c.Dexterity = 10;
            c.Health = 10;
            c.BasicLift = CalcBasicLift((int)c.Strenght);
            c.HitPoints = CalcHP((int)c.Strenght);
            c.WillPower = CalcWillPower((int)c.Intelligence);
            c.Perception = CalcPerception((int)c.Intelligence);
            c.FatiguePoints = CalcBaseFatiguePoints((int)c.Health);
            c.BasicSpeed = CalcBasicSpeed((int)c.Health, (int)c.Dexterity);
            c.BasicMove = CalcBasicMove((double)c.BasicSpeed);
            c.PlayerName = client.UserName;
            c.PointTotal = 100;
            var context = new Db1Entities();
            c.PlayerId = (from cn in context.Registration where cn.Username == client.UserName select cn.Id).FirstOrDefault();
            return c;
        }

        

        public static int CalcHP(int strenght)
        {
            return strenght;
        }
        public static int CalcWillPower(int Intelligence)
        {
            return Intelligence;
        }
        public static int CalcPerception(int Intelligence)
        {
            return Intelligence;
        }
        public static int CalcBaseFatiguePoints(int Health)
        {
            return Health;
        }
        public static int CalcBasicLift(int strenght)
        {
            return (strenght * strenght) / 5;
        }

        public static double CalcBasicSpeed(int Health,int Dexterity)
        {
            return (Health + Dexterity) / 4;
        }
         
        public static int CalcBasicMove(double BasicSpeed)
        {
            return (int)Math.Floor(BasicSpeed);
        }


    }
}
