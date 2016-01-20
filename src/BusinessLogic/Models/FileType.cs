using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class FileType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<FileInfo> Files { get; set; }
    }
}
