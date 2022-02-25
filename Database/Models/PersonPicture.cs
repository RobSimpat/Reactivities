using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class PersonPicture
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public byte[] Picture { get; set; }
        public DateTime DateTaken { get; set; } = DateTime.Now;
    }
}
