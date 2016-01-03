using System;
using System.Collections.Generic;

namespace SWNAdmin.Utility
{

    public partial class CharacterBonus
    {
        public CharacterBonus()
        {
            this.UsedBonus = new HashSet<UsedBonus>();
        }

        public int Id { get; set; }
        public string Discription { get; set; }
        public Nullable<int> Value { get; set; }
        public string ModifiedStat { get; set; }
        public string BonusName { get; set; }

        public virtual ICollection<UsedBonus> UsedBonus { get; set; }
    }
}