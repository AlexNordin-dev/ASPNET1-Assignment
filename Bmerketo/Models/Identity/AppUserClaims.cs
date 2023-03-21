using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Bmerketo.Models.Identity
{
    public class AppUserClaims : UserClaimsPrincipalFactory<IdentityUser, IdentityRole>
    {
        private readonly IdentityContext _identityContext;

        public AppUserClaims(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options, IdentityContext identityContext) : base(userManager, roleManager, options)
        {
            _identityContext = identityContext;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
        {
            var claimsIdentity = await base.GenerateClaimsAsync(user);
            var userProfile = await _identityContext.UserProfile.FirstOrDefaultAsync(x => x.UserId == user.Id);

            claimsIdentity.AddClaim(new Claim("UserId", user.Id ?? ""));
            claimsIdentity.AddClaim(new Claim("DisplayName", $"{userProfile?.FirstName} {userProfile?.LastName}" ?? ""));

            return claimsIdentity;
        }
    }
}
