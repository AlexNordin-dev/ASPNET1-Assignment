using Bmerketo.Models.Entities;
using Bmerketo.Models.ViewModels;

namespace Bmerketo.Services.Interface
{
    public interface ICategoryInterface
    {
        Task<IEnumerable<CategoryEntity>> GetAllAsync();
        Task<CategoryEntity> GetByIdAsync(int id);
        Task AddAsync(CategoryEntity category);
        Task<CategoryEntity> UpdateAsync(int id, CategoryEntity newCategory);
        Task DeleteAsync(int id);

        Task<IEnumerable<ShowCategoryBarVM>> GetCategoryForView();
    }
}
