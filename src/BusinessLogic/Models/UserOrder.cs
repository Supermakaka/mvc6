using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
   public class UserOrder
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public string TrackingNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
