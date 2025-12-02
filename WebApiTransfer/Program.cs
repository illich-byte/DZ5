using Core.Interfaces;
using Core.Services;
using Domain;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApiTransfer.Filters;
using YourProject.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// --- БЛОК КОНФІГУРАЦІЇ JWT ---
var jwtSettings = configuration.GetSection("JwtSettings");

// Отримуємо секретний ключ з конфігурації.
// Якщо ключ відсутній, додаток не повинен запускатися.
var secretKey = jwtSettings["Key"] ?? throw new InvalidOperationException("Секретний ключ JWT ('Key') не знайдено у конфігурації JwtSettings.");
var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // 1. Валідація ключа підпису (Обов'язково!)
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = securityKey,

        // 2. Включаємо валідацію Видавця (Issuer)
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"], // Очікуване значення Issuer

        // 3. Включаємо валідацію Аудиторії (Audience)
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"], // Очікуване значення Audience

        // 4. Валідація часу життя токена
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero // Запобігаємо додатковій толерантності часу
    };
});
// --- КІНЕЦЬ БЛОКУ КОНФІГУРАЦІЇ JWT ---

builder.Services.AddDbContext<AppDbTransferContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

// Реєстрація сервісів Core
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICityService, CityService>();

// ДОДАНО: Реєстрація служби автентифікації Google
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddTransient<ITokenService, TokenService>();


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

// Явне посилання на збірку Core для AutoMapper
builder.Services.AddAutoMapper(typeof(ICountryService).Assembly);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Явне посилання на збірку Core для FluentValidation.
builder.Services.AddValidatorsFromAssemblies(new[] { typeof(ICountryService).Assembly });

builder.Services.AddMvc(options =>
{
    options.Filters.Add<ValidationFilter>();
});


var app = builder.Build();

app.UseCors(policy =>
    policy.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader());

app.UseSwagger();
app.UseSwaggerUI();

// Обробка статичних файлів (зображень)
var dirImageName = configuration.GetValue<string>("DirImageName") ?? "images";
// Використовуємо wwwrootPath для статичних файлів, якщо він доступний
var wwwrootPath = app.Environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
var imagePath = Path.Combine(wwwrootPath, dirImageName);

Directory.CreateDirectory(imagePath);

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagePath),
    RequestPath = $"/{dirImageName}"
});

// --- Middleware для JWT ---
app.UseAuthentication();
app.UseAuthorization();
// --- Кінець Middleware ---

app.MapControllers();


app.Run();