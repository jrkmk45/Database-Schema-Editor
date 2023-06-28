
namespace Domain.Models
{
    public class Attribute : ModelBase
    {
        public string? Name { get; set; }
        public int DataTypeId { get; set; }
        public DataType DataType { get; set; }
        public int TableId { get; set; }
        public Table Table { get; set; }
        public IEnumerable<Connection> ConnectionsTo { get; set; }
        public IEnumerable<Connection> ConnectionsFrom { get; set; }
    }
}
