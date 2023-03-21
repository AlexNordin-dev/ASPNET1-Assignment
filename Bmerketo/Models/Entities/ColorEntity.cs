using System.ComponentModel.DataAnnotations;

namespace Bmerketo.Models.Entities
{
    public class ColorEntity
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Color")]
        public string? ColorName { get; set; }
        public List<Product_ColorEntity>? Product_ColorEntity { get; set; }
    }
}
