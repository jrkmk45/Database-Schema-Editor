
namespace Domain.Models
{
    public class Table : ModelBase
    {
        public string Name { get; set; }
        public IEnumerable<Attribute> Attributes { get; set; }
        public int SchemeId { get; set; }
        public Scheme Scheme { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
