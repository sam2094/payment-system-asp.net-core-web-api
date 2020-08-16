using System.Collections.Generic;

namespace Models.Entities
{
    public partial class Role : BaseEntity
    {
        public Role()
        {
            RolePermission = new HashSet<RolePermission>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool State { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RolePermission> RolePermission { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
