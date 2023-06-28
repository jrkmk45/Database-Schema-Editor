using Services.Dtos.Attribute;

namespace Services.Dtos.Table
{
    public class TableDTO : BaseDTO
    {
        public string Name { get; set; }
        public IEnumerable<AttributeDTO> Attributes { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
