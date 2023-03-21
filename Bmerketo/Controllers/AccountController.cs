using Bmerketo.Models.Entities;
using Bmerketo.Models.Form;
using Bmerketo.Models.Identity;
using Bmerketo.Models.ViewModels.Account;
using Bmerketo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bmerketo.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IdentityContext _identityContext;

        private readonly ProfileService _profileService;
        private readonly UserService _userService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IdentityContext identityContext, ProfileService profileService, UserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityContext = identityContext;

            _profileService = profileService;
            _userService = userService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> SignUp(string ReturnUrl = null!)
        {
            if (!await _userManager.Users.AnyAsync())
                return RedirectToAction("Installation", "Admin");

            var form = new SignUpForm
            {
                ReturnUrl = ReturnUrl ?? Url.Content("~/")
            };

            return View(form);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpForm form)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.Users.AnyAsync(x => x.Email == form.Email))
                {
                    ModelState.AddModelError(string.Empty, "A user with the same email already exists.");
                    return View(form);
                }

                var identityUser = new IdentityUser
                {
                    Email = form.Email,
                    UserName = form.Email
                };

                var result = await _userManager.CreateAsync(identityUser, form.Password);
                if (result.Succeeded)
                {
                    var profileEntity = new UserProfileEntity()
                    {
                        UserId = identityUser.Id,
                        FirstName = form.FirstName,
                        LastName = form.LastName,
                        StreetName = form.StreetName ?? null!,
                        PostalCode = form.PostalCode ?? null!,
                        City = form.City ?? null!,
                        PhoneNumber = form.PhoneNumber ?? null!,
                        Company = form.Company ?? null!,
                        ImageName = form.Image ?? null,
                        CreatedAt = form.CreatedAt,
                    };
                    if (form.ProfileImage != null)
                        profileEntity.ImageName = await _profileService.UploadProfileImageAsync(form.ProfileImage);
                    _identityContext.Add(profileEntity);
                    await _identityContext.SaveChangesAsync();

                    await _userManager.AddToRoleAsync(identityUser, "User");

                    var signInResult = await _signInManager.PasswordSignInAsync(identityUser, form.Password, false, false);
                    if (signInResult.Succeeded)
                        return LocalRedirect(form.ReturnUrl);
                    else
                        return RedirectToAction("SignIn");
                }
            }

            ModelState.AddModelError(string.Empty, "Unable to create an Account.");
            return View(form);
        }


        [AllowAnonymous]
        public IActionResult SignIn(string ReturnUrl = null!)
        {
            var form = new SignInForm
            {
                ReturnUrl = ReturnUrl ?? Url.Content("~/")
            };

            return View(form);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInForm form)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, false, false);
                if (result.Succeeded)
                    return LocalRedirect(form.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Incorrect email or password");
            return View(form);
        }

        public async Task<IActionResult> SignOut()
        {
            if (_signInManager.IsSignedIn(User))
                await _signInManager.SignOutAsync();

            return LocalRedirect("/");
        }



        public async Task<IActionResult> Index(Guid id)
        {

            var data = await _userService.GetByIdAsync(id);

           
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
        public IActionResult Orders()
        {
            return View();
        }

        public IActionResult Tickets()
        {
            return View();
        }
    }
}
