using System.ComponentModel.DataAnnotations;

namespace Services.Dtos.Table
{
    public class CreateTableDTO
    {

        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
