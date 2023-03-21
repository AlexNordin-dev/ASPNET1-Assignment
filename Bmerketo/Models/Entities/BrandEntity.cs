using System.ComponentModel.DataAnnotations;

namespace Bmerketo.Models.Entities
{
    public class BrandEntity
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Brand")]
        public string BrandName { get; set; } = null!;       
    }
}
