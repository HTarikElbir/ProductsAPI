using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductsAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Adds support for controllers in the application.
builder.Services.AddControllers();

// Adds support for database operations.
builder.Services.AddDbContext<ProductsContext>(x => x.UseSqlite("Data Source=products.db"));

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<ProductsContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;

    options.User.RequireUniqueEmail = true;
    
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
});


// Configure JWT authentication for the application.
// Sets the authentication scheme to JwtBearer and defines token validation parameters.
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false; // Disable HTTPS requirement for token validation.
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // Skip validating the token issuer.
        ValidIssuer = "abc.com", // Expected issuer (if validation is enabled).
        ValidateAudience = false, // Skip validating the audience.
        ValidAudiences = new string[] { "a", "b" }, // Expected audiences (if validation is enabled).
        ValidateIssuerSigningKey = true, // Enable validation of the signing key.
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
            builder.Configuration.GetSection("AppSettings:Secret").Value ?? "")), // The signing key used to validate tokens.
        ValidateLifetime = true, // Ensure tokens are not expired.
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enforces HTTPS for all requests.
app.UseHttpsRedirection();

// Enable the app to validate and process incoming authentication tokens or credentials.
app.UseAuthentication();

// Ensure the app checks the user's authorization status for accessing resources.
app.UseAuthorization();

// Map controller routes to enable the application to handle HTTP requests.
app.MapControllers();

app.Run();
