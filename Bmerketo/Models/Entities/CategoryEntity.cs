using System.ComponentModel.DataAnnotations;

namespace Bmerketo.Models.Entities
{
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Category")]
        public string? CategoryName { get; set; }
    }
}
