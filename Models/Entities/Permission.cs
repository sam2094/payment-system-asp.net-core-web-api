using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class Permission : BaseEntity
    {
        public Permission()
        {
            RolePermission = new HashSet<RolePermission>();
        }

        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime Added { get; set; }

        public virtual PermissionGroup Group { get; set; }
        public virtual ICollection<RolePermission> RolePermission { get; set; }
    }
}
