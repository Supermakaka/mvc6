using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class FileType
    {
        int Id { get; set; }

        int Name { get; set; }

        public virtual ICollection<FileInfo> Files { get; set; }

        public virtual ICollection<Product_FileInfo> Products { get; set; }
    }
}
