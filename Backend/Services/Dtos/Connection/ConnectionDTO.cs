using Domain.Enums;

namespace Services.Dtos.Connection
{
    public class ConnectionDTO : BaseDTO
    {
        public ConnectionType ConnectionType { get; set; }
        public int AttributeFromId { get; set; }
        public int AttributeToId { get; set; }
    }
}
