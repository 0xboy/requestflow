using Microsoft.AspNetCore.Authentication.Cookies;
using RequestFlow.Application;
using RequestFlow.Infrastructure;
using RequestFlow.Persistence;
using RequestFlow.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddShared();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllersWithViews();

// TODO: Replace with ASP.NET Core Identity when implemented
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Error/Unauthorized";
        options.ReturnUrlParameter = "returnUrl";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
