using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class FileInfo
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string Extension { get; set; }

        [Required]
        public int FileTypeId { get; set; }
        public FileType FileType { get; set; }
    }
}
