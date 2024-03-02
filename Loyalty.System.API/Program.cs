using Authentication.System.API.Extensions;
using AutoMapper;
using Loyalty.System.API.Configuration;
using Loyalty.System.API.Models;
using Loyalty.System.API.Models.Settings;
using Loyalty.System.Data.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Drawing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
string sqlServer = "";

try
{
    builder.Services.AddDbContext<BaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"))
            );
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

builder.Services.AddAuthorizationCore(options =>
{
    var schemes = "Jwt";
    options.DefaultPolicy = new AuthorizationPolicyBuilder(schemes)
      .RequireAuthenticatedUser()
      .Build();

    options.AddPolicy("JwtPolicy", new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddAuthenticationSchemes("Jwt")
        .Build());

});


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Dodaj obs³ugê generowania dokumentacji dla typów niestandardowych
    c.MapType<Bitmap>(() => new OpenApiSchema { Type = "file", Format = "binary" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
{
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
});
});

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
#region AutoMapper
var mappingCongig = new MapperConfiguration(mc =>
     mc.AddProfile(
 new AutoMapperInitializator()
 )
);
IMapper mapper = mappingCongig.CreateMapper();
builder.Services.AddSingleton(mapper);


#endregion
var googleSettings = new GoogleSettings();
var bindJwtSettings = new JWTConfig();
builder.Configuration.Bind("JsonWebTokenKeys", bindJwtSettings);
builder.Services.AddSingleton(bindJwtSettings);
builder.Services.AddSingleton(googleSettings);
builder.Services.Configure<GoogleSettings>(builder.Configuration.GetSection("GoogleAuth"));
builder.Services.Configure<JWTConfig>(builder.Configuration.GetSection("JsonWebTokenKeys"));


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})

.AddCookie()
.AddGoogle(options =>

{
    options.ClientId =googleSettings.clientId;
    options.ClientSecret = googleSettings.clientSecret;

})
               .AddJwtBearer("Jwt", options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.SaveToken = true;
                   options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                   {
                       ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
                       IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
                       ValidateIssuer = bindJwtSettings.ValidateIssuer,
                       ValidIssuer = bindJwtSettings.ValidIssuer,
                       ValidateAudience = bindJwtSettings.ValidateAudience,
                       ValidAudience = bindJwtSettings.ValidAudience,
                       RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                       ValidateLifetime = bindJwtSettings.RequireExpirationTime,
                       ClockSkew = TimeSpan.FromDays(1),
                   };
               });
builder.Services.RegisterServices();
var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
