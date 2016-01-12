using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class Address
    {
        public int Id { get; set; }

        public virtual int UserId { get; set; }
        public virtual User User { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string Zip { get; set; }

        public string City { get; set; }
                
        public int StateId { get; set; }
        public virtual State State { get; set; }
    }
}
