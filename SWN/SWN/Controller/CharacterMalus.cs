using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWNAdmin.Utility
{
    using System;
    using System.Collections.Generic;

    public partial class CharacterMalus
    {
        public CharacterMalus()
        {
            this.UsedMalus = new HashSet<UsedMalus>();
        }

        public int Id { get; set; }
        public string Discription { get; set; }
        public Nullable<int> Value { get; set; }
        public string ModifiedStat { get; set; }
        public string MalusName { get; set; }

        public virtual ICollection<UsedMalus> UsedMalus { get; set; }
    }
}
