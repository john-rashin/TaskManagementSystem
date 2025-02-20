using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Generate a random JWT Secret Key at runtime
var keyBytes = new byte[32]; // 256-bit key
using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
{
    rng.GetBytes(keyBytes);
}
var jwtSecret = Convert.ToBase64String(keyBytes);

// Store the secret in a configuration variable
builder.Configuration["Jwt:Secret"] = jwtSecret;

// Add services to the container
builder.Services.AddControllersWithViews();  // Enable MVC

// Configure Singleton Database Connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddControllers(); // Enable Web API

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();  // Enable serving CSS, JS, etc.
app.UseRouting();
app.UseAuthentication(); // Enable JWT Authentication
app.UseAuthorization();

// Set default routing for MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers(); // Keep Web API routing
app.Run();
