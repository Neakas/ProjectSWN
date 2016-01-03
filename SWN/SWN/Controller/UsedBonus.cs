using System;
using System.Collections.Generic;

namespace SWNAdmin.Utility
{

    public partial class UsedBonus
    {
        public int Id { get; set; }
        public Nullable<int> AdvantageId { get; set; }
        public Nullable<int> DisadvantageId { get; set; }
        public int BonusId { get; set; }

        public virtual Advantages Advantages { get; set; }
        public virtual CharacterBonus CharacterBonus { get; set; }
        public virtual Disadvantages Disadvantages { get; set; }
    }
}
