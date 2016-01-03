using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNAdmin
{
    public class RaceController
    {
        public int MyProperty { get; set; }
        public Language SpokenLanguage { get; set; }
        // More Stuff here later
    }

    public class Language
    {
        public string Name { get; set; }
        public bool Spoken { get; set; }
        public bool Written { get; set; }
    }
    public class Culture
    {
        public string Name { get; set; }
    }

}
