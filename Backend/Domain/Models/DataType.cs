using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class DataType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
