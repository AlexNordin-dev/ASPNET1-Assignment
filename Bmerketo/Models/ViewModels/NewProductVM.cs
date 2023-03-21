using Bmerketo.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bmerketo.Models.ViewModels
{
    public class NewProductVM
    {
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = null!;

        [Display(Name = "Product Price")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "SKU Number")]
        public string SKU { get; set; } = null!;

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Short Description")]
        public string? ShortDescription { get; set; }

        [Display(Name = "Long Description")]
        public string? LongDescription { get; set; }

        //______________________________       
        [Display(Name = "Image Alt")]
        public string? ImageAlt { get; set; }

        [Display(Name = "Image Name")]
        public string? ImageName { get; set; }
        [NotMapped]
        [Display(Name = "Upload Image")]
        public IFormFile? ImageFile { get; set; }

        //Relationships
        [Display(Name = "Brand")]
        public string? BrandName { get; set; }
        public int BrandEntityId { get; set; }

        [Display(Name = "Category")]
        public string? CategoryName { get; set; }
        public int CategoryEntityId { get; set; }

        [Display(Name = "Color")]
        public List<ColorEntity>? ColorName { get; set; }
        public List<int>? ColorEntityIds { get; set; }
        public List<int>? Product_ColorEntityIds { get; set; }
        public List<Product_ColorEntity>? Product_Colors { get; set; }

        [Display(Name = "Review")]
        public int Rating { get; set; }
        public List<int>? ReviewEntityIds { get; set; }
    }
}
