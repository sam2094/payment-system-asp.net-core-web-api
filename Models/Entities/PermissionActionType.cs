using System.Collections.Generic;

namespace Models.Entities
{
    public partial class PermissionActionType : BaseEntity
    {
        public PermissionActionType()
        {
            RolePermission = new HashSet<RolePermission>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RolePermission> RolePermission { get; set; }
    }
}
