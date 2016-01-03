using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNAdmin.Utility
{

    public partial class UsedMalus
    {
        public int Id { get; set; }
        public Nullable<int> AdvantageId { get; set; }
        public Nullable<int> DisadvantageId { get; set; }
        public Nullable<int> MalusId { get; set; }

        public virtual Advantages Advantages { get; set; }
        public virtual CharacterMalus CharacterMalus { get; set; }
        public virtual Disadvantages Disadvantages { get; set; }
    }
}

