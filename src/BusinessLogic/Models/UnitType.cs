using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class UnitType
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Deleted { get; set; }

        public bool Enabled { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public virtual ICollection<Unit> Units { get; set; }
    }
}
