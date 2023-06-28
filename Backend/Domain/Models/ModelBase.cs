using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public abstract class ModelBase
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
