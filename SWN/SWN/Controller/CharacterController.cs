using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SWNAdmin
{

    public class CharacterController
    {
        public string Name { get; set; }
        public string Player { get; set; }
        public int PointTotal { get; set; }
        public Double Height { get; set; }
        public Double Weight { get; set; }
        public int SizeModifier { get; set; }
        public int Age { get; set; }
        public int UnspendPoints { get; set; }
        public string Appearance { get; set; }
        public int Strenght { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public int Health { get; set; }
        public int HitPoints { get; set; }
        public int WillPower { get; set; }
        public int Perception { get; set; }
        public int FatiguePoints { get; set; }
        public int BasicLift { get; set; }
        public int BasicSpeed { get; set; } 
        public int BasicMove { get; set; }
        public int PersonalTechnologyLevel { get; set; }
        public int ParryValue { get; set; }
        public int BlockValue { get; set; }
    }
}
