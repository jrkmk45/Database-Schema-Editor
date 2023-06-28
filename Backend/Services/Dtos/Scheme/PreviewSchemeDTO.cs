using Services.Dtos.User;

namespace Services.Dtos.Scheme
{
    public class PreviewSchemeDTO : BaseDTO
    {
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }
        public UserDTO User { get; set; }
    }
}
