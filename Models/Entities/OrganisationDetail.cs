namespace Models.Entities
{
    public partial class OrganisationDetail : BaseEntity
    {
        public int Id { get; set; }
        public string OrgName { get; set; }
        public string Voen { get; set; }
        public string Sun { get; set; }
        public string Kppcode { get; set; }
    }
}
