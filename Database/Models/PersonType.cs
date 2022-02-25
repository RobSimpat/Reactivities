using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class PersonType
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int Type { get; set; }
        public int Role { get; set; }
        public bool Active { get; set; } = false;
    }
}
