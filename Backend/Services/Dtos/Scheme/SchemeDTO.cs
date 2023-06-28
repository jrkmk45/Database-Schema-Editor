using Services.Dtos.Table;
using Services.Dtos.User;

namespace Services.Dtos.Scheme
{
    public class SchemeDTO : BaseDTO
    {
        public string Name { get; set; }
        public IEnumerable<TableDTO> Tables { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public UserDTO User { get; set; }
    }
}
