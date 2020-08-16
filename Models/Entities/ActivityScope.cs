namespace Models.Entities
{
    public partial class ActivityScope : BaseEntity
    {
        public int OrgId { get; set; }
        public int ScopeId { get; set; }

        public virtual Organisation Org { get; set; }
        public virtual OrganiztaionScope Scope { get; set; }
    }
}
