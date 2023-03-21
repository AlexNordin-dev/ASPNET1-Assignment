using Bmerketo.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Bmerketo.Models.ViewModels
{
    public class ShowCategoryBarVM
    {
        public int Id { get; set; }
        [Display(Name = "Category")]
        public string? CategoryName { get; set; }
        public List<CategoryEntity> CategoryLinks { get; set; } = new List<CategoryEntity>();
    }
}
