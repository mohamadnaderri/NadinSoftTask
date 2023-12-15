using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NadinSoftTask.Configuration;
using NadinSoftTask.Host.Helpers;
using NadinSoftTask.Infrastructure.Persistence.Context;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Db")));

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Db")));

builder.Services.AddScoped<IJwtFactory, JwtFactory>();
Bootstrapper.WireUp();

// jwt wire up
// Get options from app settings
var jwtAppSettingOptions = builder.Configuration.GetSection("JwtIssuerOptions");

SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAppSettingOptions[nameof(JwtIssuerOptions.SecretKey)]));

// Configure JwtIssuerOptions
builder.Services.Configure<JwtIssuerOptions>(options =>
{
    options.Issuer = builder.Configuration["JwtIssuerOptions:Issuer"];
    options.Audience = builder.Configuration["JwtIssuerOptions:Audience"];
    options.SecretKey = builder.Configuration["JwtIssuerOptions:SecretKey"];
    options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
});

// add identity
var identityBuilder = builder.Services.AddIdentity<IdentityUser, IdentityRole>(o =>
{
    // configure identity options
    o.Password.RequireDigit = false;
    o.Password.RequireLowercase = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequiredLength = 6;
    o.Tokens.ChangePhoneNumberTokenProvider = "Phone";
});
identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(IdentityRole), builder.Services);
identityBuilder.AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

    ValidateAudience = true,
    ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

    ValidateIssuerSigningKey = true,
    IssuerSigningKey = signingKey,

    RequireExpirationTime = false,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero
};

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions =>

{
    configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
    configureOptions.TokenValidationParameters = tokenValidationParameters;
    configureOptions.SaveToken = true;
});

// api user claim policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiUser",
        policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol,
            Constants.Strings.JwtClaims.ApiAccess));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DatabaseContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        throw new Exception("An error occurred while migrating the database.");
    }
}

using (var serviceScope = app.Services.CreateScope())
{
    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    serviceScope.ServiceProvider.GetRequiredService<IdentityContext>().Database.Migrate();

    var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
    if (!adminRoleExists)
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    var user = await userManager.FindByNameAsync("admin");
    if (user == null)
    {
        user = new IdentityUser
        {
            UserName = "admin",
            Email = "admin@example.com"
        };

        var result = await userManager.CreateAsync(user, "123456");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}


app.Run();
