using System;

namespace Models.Entities
{
    public partial class ForgetPasswords : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Code { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
