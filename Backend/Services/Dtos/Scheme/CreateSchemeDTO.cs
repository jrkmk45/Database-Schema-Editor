using System.ComponentModel.DataAnnotations;

namespace Services.Dtos.Scheme
{
    public class CreateSchemeDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
