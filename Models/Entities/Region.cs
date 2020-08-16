namespace Models.Entities
{
    public partial class Region : BaseEntity
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string DigitCode { get; set; }
        public string Description { get; set; }

        public virtual Country Country { get; set; }

    }
}
