using System.Collections.Generic;

namespace Models.Entities
{
    public partial class AppType : BaseEntity
    {
        public AppType()
        {
            Applications = new HashSet<Application>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Application> Applications { get; set; }
    }
}
