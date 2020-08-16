namespace Models.Entities
{
    public partial class MerchantSetting : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SuccesUri { get; set; }
        public string ErrorUri { get; set; }
        public string EncKey { get; set; }

        public virtual Application IdNavigation { get; set; }
    }
}
