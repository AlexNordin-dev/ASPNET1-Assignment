using Bmerketo.Models.Entities;
using Bmerketo.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Bmerketo.Services
{
    public class BrandService : IBrandInterface
    {
        private readonly DataContext _dataContext;

        public BrandService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddAsync(BrandEntity brand)
        {
            await _dataContext.BrandEntity.AddAsync(brand);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _dataContext.BrandEntity.FirstOrDefaultAsync(x => x.Id == id);
            _dataContext.BrandEntity.Remove(result);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BrandEntity>> GetAllAsync()
        {
            var result = await _dataContext.BrandEntity.ToListAsync();
            return result;
        }

        public async Task<BrandEntity> GetByIdAsync(int id)
        {
            var result = await _dataContext.BrandEntity.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<BrandEntity> UpdateAsync(int id, BrandEntity newBrand)
        {
            _dataContext.Update(newBrand);
            await _dataContext.SaveChangesAsync();
            return newBrand;
        }
    }
}
