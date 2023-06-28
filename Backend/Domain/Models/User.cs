using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class User : IdentityUser<int>
    {
        public IEnumerable<Scheme> Schemas { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? ProfilePicture { get; set; }

    }
}
