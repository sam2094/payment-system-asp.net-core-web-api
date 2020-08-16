using System.Collections.Generic;

namespace Models.Entities
{
    public partial class PermissionGroup : BaseEntity
    {
        public PermissionGroup()
        {
            Permission = new HashSet<Permission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Permission> Permission { get; set; }
    }
}
