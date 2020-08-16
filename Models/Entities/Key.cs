namespace Models.Entities
{
    public partial class Key : BaseEntity
    {
        public int Id { get; set; }
        public int AppId { get; set; }
        public string AppKey { get; set; }
        public int EncType { get; set; }
        public bool State { get; set; }
    }
}
