using Bmerketo.Contexts;
using Bmerketo.Models.Entities;
using Bmerketo.Models.Form;
using Bmerketo.Models.Identity;
using Bmerketo.Models.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Bmerketo.Services
{
    public class UserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityContext _identityContext;
        private readonly ProfileService _profileService;

        public UserService(UserManager<IdentityUser> userManager, IdentityContext identityContext, ProfileService profileService)
        {
            _userManager = userManager;
            _identityContext = identityContext;
            _profileService = profileService;
        }
       
      

        //[HttpPut]
        public async Task UpdateAsync(UpdateUserProfileVM userProfile)
        {
            var db = await _identityContext.UserProfile.FirstOrDefaultAsync(n => n.Id == userProfile.Id);
            if (db != null)
            {

                db.FirstName = userProfile.FirstName;
                db.LastName = userProfile.LastName;
                db.StreetName = userProfile.StreetName;
                db.PostalCode = userProfile.PostalCode;
                db.City = userProfile.City;
                db.PhoneNumber = userProfile.PhoneNumber;
                db.Company = userProfile.Company;
                db.ImageName = userProfile.Image;
               
                await _identityContext.SaveChangesAsync();
            }
              
        }
        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            var result = await _identityContext.UserProfile.FirstOrDefaultAsync(x => x.Id == id);
            _identityContext.UserProfile.Remove(result);
            await _identityContext.SaveChangesAsync();
        }
        [HttpGet]
        public async Task<IEnumerable<UserProfileEntity>> GetAllAsync()
        {
            var result = await _identityContext.UserProfile.ToListAsync();
            return result;
        }


        public async Task<UserProfileEntity> GetByIdAsync(Guid id)
        {
            var result = await _identityContext.UserProfile.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
        public async Task<UserProfileEntity> GetProfileIdAsync(Guid id)
        {
            
            var result = await _identityContext.UserProfile.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
