using Core.Interfaces;
using Core.Services;
using Domain;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebApiTransfer.Filters;

var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
=======
// Add services to the container.
>>>>>>> aea59e1b4ac8a1b0e26c6e93adb7a6774902ac26
builder.Services.AddDbContext<AppDbTransferContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

<<<<<<< HEAD
// Реєстрація сервісів
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IImageService, ImageService>();

// Додаємо реєстрацію сервісу для Міст
builder.Services.AddScoped<ICityService, CityService>();

=======
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IImageService, ImageService>();

>>>>>>> aea59e1b4ac8a1b0e26c6e93adb7a6774902ac26

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMvc(options =>
{
    options.Filters.Add<ValidationFilter>();
});

var app = builder.Build();

<<<<<<< HEAD
=======
// Configure the HTTP request pipeline.
>>>>>>> aea59e1b4ac8a1b0e26c6e93adb7a6774902ac26
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

<<<<<<< HEAD
app.UseDefaultFiles();
app.UseStaticFiles();

=======
>>>>>>> aea59e1b4ac8a1b0e26c6e93adb7a6774902ac26
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

var dirImageName = builder.Configuration
    .GetValue<string>("DirImageName") ?? "duplo";

<<<<<<< HEAD
var wwwrootPath = app.Environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

var imagePath = Path.Combine(wwwrootPath, dirImageName);

Directory.CreateDirectory(imagePath);


app.Run();
=======
// Console.WriteLine("Image dir {0}", dirImageName);
var path = Path.Combine(Directory.GetCurrentDirectory(), dirImageName);
Directory.CreateDirectory(dirImageName);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(path),
    RequestPath = $"/{dirImageName}"
});

app.Run();
>>>>>>> aea59e1b4ac8a1b0e26c6e93adb7a6774902ac26
