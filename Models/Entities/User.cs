using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class User : BaseEntity
    {
        public User()
        {
            Invoices = new HashSet<Invoice>();
            UserTokens = new HashSet<UserToken>();
        }

        public int Id { get; set; }
        public int BranchId { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronomic { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Phone { get; set; }
        public bool? State { get; set; }
        public string LastAuthIp { get; set; }
        public DateTime? LastAuthDate { get; set; }
        public DateTime Added { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
    }
}
