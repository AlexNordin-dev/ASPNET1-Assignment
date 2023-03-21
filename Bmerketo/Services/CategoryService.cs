using Bmerketo.Models.Entities;
using Bmerketo.Models.ViewModels;
using Bmerketo.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Bmerketo.Services
{
    public class CategoryService : ICategoryInterface
    {
        private readonly DataContext _dataContext;

        public CategoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddAsync(CategoryEntity category)
        {
            await _dataContext.CategoryEntity.AddAsync(category);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ShowCategoryBarVM>> GetCategoryForView()
        {
            return _dataContext.CategoryEntity.Select(c => new ShowCategoryBarVM()
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
            });
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _dataContext.CategoryEntity.FirstOrDefaultAsync(x => x.Id == id);
            _dataContext.CategoryEntity.Remove(result);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryEntity>> GetAllAsync()
        {
            var result = await _dataContext.CategoryEntity.ToListAsync();
            return result;
        }

        public async Task<CategoryEntity> GetByIdAsync(int id)
        {
            var result = await _dataContext.CategoryEntity.FirstOrDefaultAsync(x => x.Id == id);
            return result;

        }

        public async Task<CategoryEntity> UpdateAsync(int id, CategoryEntity newcategory)
        {
            _dataContext.Update(newcategory);
            await _dataContext.SaveChangesAsync();
            return newcategory;

        }
    }
}
