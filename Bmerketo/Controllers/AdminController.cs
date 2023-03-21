using Bmerketo.Models.Entities;
using Bmerketo.Models.Form;
using Bmerketo.Models.ViewModels.Account;
using Bmerketo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bmerketo.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IdentityContext _identityContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserService _userService;
        private readonly ProfileService _profileService;
        public AdminController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IdentityContext identityContext, RoleManager<IdentityRole> roleManager, UserService userService, ProfileService profileService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityContext = identityContext;
            _roleManager = roleManager;
            _userService = userService;
            _profileService = profileService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Installation(string ReturnUrl = null!)
        {
            if (await _userManager.Users.AnyAsync())
                return RedirectToAction("SignIn", "Account");

            var form = new SignUpForm
            {
                ReturnUrl = ReturnUrl ?? Url.Content("~/")
            };

            return View(form);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Installation(SignUpForm form)
        {
            if (ModelState.IsValid)
            {
                if (!await _roleManager.Roles.AnyAsync())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Administrator"));
                    await _roleManager.CreateAsync(new IdentityRole("User Manager"));
                    await _roleManager.CreateAsync(new IdentityRole("Product Manager"));
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }

                var identityUser = new IdentityUser
                {
                    Email = form.Email,
                    UserName = form.Email
                };

                var result = await _userManager.CreateAsync(identityUser, form.Password);
                if (result.Succeeded)
                {
                    _identityContext.UserProfile.Add(new UserProfileEntity
                    {
                        UserId = identityUser.Id,
                        FirstName = form.FirstName,
                        LastName = form.LastName,
                        StreetName = form.StreetName ?? null!,
                        PostalCode = form.PostalCode ?? null!,
                        City = form.City ?? null!,
                        PhoneNumber = form.PhoneNumber ?? null!,
                        Company = form.Company ?? null!,                     
                        CreatedAt = form.CreatedAt,                    
                        

                    });
                    await _identityContext.SaveChangesAsync();

                    await _userManager.AddToRoleAsync(identityUser, "Administrator");

                    var signInResult = await _signInManager.PasswordSignInAsync(identityUser, form.Password, false, false);
                    if (signInResult.Succeeded)
                        return LocalRedirect(form.ReturnUrl);
                    else
                        return RedirectToAction("SignIn", "Account");


                }
            }

            ModelState.AddModelError(string.Empty, "Unable to create an Account.");
            return View(form);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _userService.GetAllAsync();
            return View(data);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var UserProfileEdit = await _userService.GetByIdAsync(id);
            if (UserProfileEdit == null) { return View("NotFound"); }

            var profileEntity = new UpdateUserProfileVM()
            {
                Id = UserProfileEdit.Id,
                FirstName = UserProfileEdit.FirstName,
                LastName = UserProfileEdit.LastName,
                StreetName = UserProfileEdit.StreetName ?? null!,
                PostalCode = UserProfileEdit.PostalCode ?? null!,
                City = UserProfileEdit.City ?? null!,
                PhoneNumber = UserProfileEdit.PhoneNumber ?? null!,
                Company = UserProfileEdit.Company ?? null!,
                Image = UserProfileEdit.ImageName ?? null,

            };



            return View(profileEntity);
        }

        //Set
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, UpdateUserProfileVM userProfile)
        {
            if (id != userProfile.Id) return View("NotFound");

            if (userProfile.ProfileImage != null)
                userProfile.Image = await _profileService.UploadProfileImageAsync(userProfile.ProfileImage);

            await _userService.UpdateAsync(userProfile);
            return RedirectToAction(nameof(Index));
        }
        //-------------------------
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteuser = await _userService.GetByIdAsync(id);
            if (deleteuser == null) { return View("NotFound"); }


            return View(deleteuser);
        }
        //Set
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var brandName = await _userService.GetByIdAsync(id);
            if (brandName == null) { return View("NotFound"); }
            await _userService.DeleteAsync(id);


            return RedirectToAction(nameof(Index));
        }
    }
}
