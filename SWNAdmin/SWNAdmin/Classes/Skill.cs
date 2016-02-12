using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNAdmin.Classes
{
    class Skill
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Attribute ControllingAttribute { get; set; }
    }
}
