namespace Models.Entities
{
    public partial class RolePermission : BaseEntity
    {
        public int RoleId { get; set; }
        public byte ActionTypeId { get; set; }
        public int PermissionId { get; set; }

        public virtual PermissionActionType ActionType { get; set; }
        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }
}
