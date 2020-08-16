namespace Models.Entities
{
    public partial class City : BaseEntity
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public string Name { get; set; }
        public string DigitCode { get; set; }
        public int TimeZoneId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longtitude { get; set; }
        public virtual Region Region { get; set; }

    }
}
