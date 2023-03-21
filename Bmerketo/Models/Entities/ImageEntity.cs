using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bmerketo.Models.Entities
{
    public class ImageEntity
    {
        public int Id { get; set; }
        [Display(Name = "Image Alt")]
        public string? ImageAlt{ get; set; }

        [Display(Name = "Image Name")]
        public string? ImageName { get; set; }
        [NotMapped]
        [Display(Name = "Upload Image")]
        public IFormFile? ImageFile { get; set; }
    }
}
