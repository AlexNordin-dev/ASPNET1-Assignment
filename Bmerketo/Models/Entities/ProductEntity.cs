using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bmerketo.Models.Entities
{
    public class ProductEntity
    {
        //Special detail for only one Product_____________________________  
        [Key]
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



        //General (Same) detail for one or many Product_____________________________  
        //One-to-many Relationships         
        public int BrandEntityId { get; set; } // Foreign key property
        [ForeignKey(nameof(BrandEntityId))]
        public BrandEntity BrandEntity { get; set; } = null!;  // Navigation property


        public int CategoryEntityId { get; set; }
        [ForeignKey(nameof(CategoryEntityId))]
        public CategoryEntity? CategoryEntity { get; set; }


        //List  
        public List<Product_ColorEntity>? Product_ColorEntity { get; set; }   
        public ICollection<ReviewEntity>? ReviewEntity { get; set; }

    }
}
