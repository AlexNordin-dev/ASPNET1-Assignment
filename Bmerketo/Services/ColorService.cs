using Bmerketo.Models.Entities;
using Bmerketo.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Bmerketo.Services
{
    public class ColorService : IColorInterface
    {
        private readonly DataContext _dataContext;

        public ColorService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddAsync(ColorEntity newcolor)
        {
            await _dataContext.ColorEntity.AddAsync(newcolor);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _dataContext.ColorEntity.FirstOrDefaultAsync(x => x.Id == id);
            _dataContext.ColorEntity.Remove(result);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ColorEntity>> GetAllAsync()
        {
            var result = await _dataContext.ColorEntity.ToListAsync();
            return result;
        }

        public async Task<ColorEntity> GetByIdAsync(int id)
        {
            var result = await _dataContext.ColorEntity.FirstOrDefaultAsync(x => x.Id == id);
            return result;

        }

        public async Task<ColorEntity> UpdateAsync(int id, ColorEntity updateColor)
        {
            _dataContext.Update(updateColor);
            await _dataContext.SaveChangesAsync();
            return updateColor;
                
        }
    }
}
