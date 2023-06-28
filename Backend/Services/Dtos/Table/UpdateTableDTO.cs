using Domain.Models;
using Services.Dtos.Attribute;

namespace Services.Dtos.Table
{
    public class UpdateTableDTO : ModelBase
    {
        public string Name { get; set; }
    }
}
