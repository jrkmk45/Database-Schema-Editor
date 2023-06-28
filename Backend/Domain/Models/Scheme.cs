
using Domain.Enums;

namespace Domain.Models
{
    public class Scheme : ModelBase
    {
        public string Name { get; set; }
        public IEnumerable<Table> Tables { get; set; }
        public SchemeAccessability Accessability { get; set; }
    }
}
