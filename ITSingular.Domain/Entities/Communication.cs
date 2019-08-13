using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSingular.Domain.Entities
{
    public class Communication:EntityBase
    {
        public DateTime Last { get; set; }
        public string  Machine { get; set; }
    }
}
