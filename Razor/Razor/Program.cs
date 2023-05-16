using Microsoft.AspNetCore.Authentication.Cookies;
using Razor.Services;
using Razor.Services.Abstracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(opt =>
        {
            opt.LoginPath = "/login";
            //opt.LogoutPath = new PathString("/Auth/Logout");
        });
builder.Services.AddAuthorization(x => x.AddPolicy("MemberPolicy", policy => policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Member")));
builder.Services.AddAuthorization(x => x.AddPolicy("AdminPolicy", policy => policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Administrator")));

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminPolicy");
    options.Conventions.AuthorizeAreaFolder("Member", "/", "MemberPolicy");

    //options.Conventions.AuthorizePage("/Index");
    //options.Conventions.AuthorizeFolder("/Private");
    //options.Conventions.AllowAnonymousToPage("/Private/PublicPage");
    //options.Conventions.AllowAnonymousToFolder("/Private/PublicPages");
});


builder.Services.AddHttpClient();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddTransient<CrudService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapAreaControllerRoute(
//    name: "MyAreaProducts",
//    areaName: "Products",
//    pattern: "Products/{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
