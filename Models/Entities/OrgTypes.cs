using System.Collections.Generic;

namespace Models.Entities
{
    public partial class OrgTypes : BaseEntity
    {
        public OrgTypes()
        {
            Organisations = new HashSet<Organisation>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Organisation> Organisations { get; set; }
    }
}
