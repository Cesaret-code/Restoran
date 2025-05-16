using Restoran.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restoran.Models
{
    public class Chef:BaseEntity
    {
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }


    }
}
