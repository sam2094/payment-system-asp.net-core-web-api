using System.Collections.Generic;

namespace Models.Entities
{
    public partial class OrganiztaionScope : BaseEntity
    {
        public OrganiztaionScope()
        {
            ActivityScope = new HashSet<ActivityScope>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ActivityScope> ActivityScope { get; set; }
    }
}
