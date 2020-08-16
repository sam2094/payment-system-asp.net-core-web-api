namespace Models.Entities
{
    public partial class Timezone : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Prev { get; set; }
        public string Description { get; set; }
    }
}
