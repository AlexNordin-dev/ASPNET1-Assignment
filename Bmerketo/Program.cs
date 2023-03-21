global using Bmerketo.Contexts;
using Microsoft.EntityFrameworkCore;
using Bmerketo.Services;
using Bmerketo.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Bmerketo.Models.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DataSql")));
builder.Services.AddScoped<IBrandInterface, BrandService>();
builder.Services.AddScoped<ICategoryInterface, CategoryService>();
builder.Services.AddScoped<IColorInterface, ColorService>();
builder.Services.AddScoped<IProductInterface, ProductService>();



//Identity
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddDbContext<IdentityContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("IdentitySql")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(x =>
{
    x.Password.RequiredLength = 8;
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
  
}).AddEntityFrameworkStores<IdentityContext>();
builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/Account/SignIn";
    x.AccessDeniedPath = "/AccessDenied";
    //x.LogoutPath = "/";
    //x.Cookie.Expiration.Value = TimeSpan.Zero;

});
builder.Services.AddScoped<IUserClaimsPrincipalFactory<IdentityUser>, AppUserClaims>();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
////____________________
DefaultValue.DefaultItem(app);
app.Run();
