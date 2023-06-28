using Services.Dtos.Connection;

namespace Services.Dtos.Attribute
{
    public class AttributeDTO : BaseDTO
    {
        public string? Name { get; set; }
        public string DataType { get; set; }
        public int DataTypeId { get; set; }
        public IEnumerable<ConnectionDTO> ConnectionsTo { get; set; }
        public IEnumerable<ConnectionDTO> ConnectionsFrom { get; set; }
    }
}
