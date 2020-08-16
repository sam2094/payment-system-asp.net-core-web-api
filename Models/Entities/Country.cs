namespace Models.Entities
{
    public partial class Country : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneCode { get; set; }
        public string Isocode { get; set; }
    }
}
