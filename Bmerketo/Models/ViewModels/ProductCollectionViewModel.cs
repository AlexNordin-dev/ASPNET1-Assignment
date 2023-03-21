using Bmerketo.Models.Entities;

namespace Bmerketo.Models.ViewModels
{
    public class ProductCollectionViewModel
    {
        public string Title { get; set; } = null!;
        public IEnumerable<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();
        public IEnumerable<ProductEntity> Products { get; set; } = new List<ProductEntity>();
        public IEnumerable<ReviewEntity> Reviews { get; set; } = new List<ReviewEntity>();
        public IEnumerable<NewProductVM> ProductVMs { get; set; } = new List<NewProductVM>();
    }
}
