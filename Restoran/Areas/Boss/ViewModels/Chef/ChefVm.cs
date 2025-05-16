using System.ComponentModel.DataAnnotations.Schema;

namespace Restoran.Areas.Boss.ViewModels.Chef
{
    public class ChefVm
    {

        public string FullName { get; set; }
        public string Designation { get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }

    }
}
