using System;
using System.Collections.Generic;

namespace SWNAdmin.Utility
{

    public partial class Advantages
    {
        public Advantages()
        {
            this.UsedBonus = new HashSet<UsedBonus>();
            this.UsedMalus = new HashSet<UsedMalus>();
        }

        public int Id { get; set; }
        public bool isEnabled { get; set; }
        public string Name { get; set; }
        public Nullable<int> PointCost { get; set; }
        public string Discription { get; set; }

        public virtual ICollection<UsedBonus> UsedBonus { get; set; }
        public virtual ICollection<UsedMalus> UsedMalus { get; set; }
    }
}
