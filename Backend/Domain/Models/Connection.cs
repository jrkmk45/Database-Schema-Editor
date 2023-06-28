using Domain.Enums;

namespace Domain.Models
{
    public class Connection : ModelBase
    {
        public ConnectionType ConnectionType { get; set; }
        public int AttributeFromId { get; set; }
        public Attribute AttributeFrom { get; set; }

        public int AttributeToId { get; set; }
        public Attribute AttributeTo { get; set; }
    }
}
