using CloudinaryDotNet;
using DormSearchBe.Api.Infrastructure.Extensions;
using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.Module;
using DormSearchBe.Domain.Entity;
using DormSearchBe.Infrastructure.Context;
using DormSearchBe.Infrastructure.Exceptions;
using DormSearchBe.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationModules();

//Jwt
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Cấu hình bảo mật Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [Space] then your token"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
            new string[]{}
        }
        });
});
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));
builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddAuthorization();

//ConnectStrings
builder.Services.AddDbContext<DormSearch_DbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DormSearch_Context")));

//Cloudinary
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var cloudinaryAccount = new Account(
    configuration["Cloudinary:CloudName"],
    configuration["Cloudinary:ApiKey"],
    configuration["Cloudinary:ApiSecret"]
);
var cloudinary = new Cloudinary(cloudinaryAccount);
builder.Services.AddSingleton(cloudinary);
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    try
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<DormSearch_DbContext>();

        await dbContext.Database.MigrateAsync();

        if (!dbContext.Users.Any())
        {
            DateTime now = DateTime.Now;
            string password = "12345678a@";

            await dbContext.Users.AddAsync(
                new User
                {
                    createdAt = DateTime.Today.AddDays(1).AddHours(now.Hour).AddMinutes(now.Minute).AddSeconds(now.Second),
                    FullName = "Vân Anh",
                    Email = "admin@gmail.com",
                    Role = "Admin",
                    Gender = "Nữ",
                    Password = PasswordHelper.CreateHashedPassword(password),
                    Address = "Hà Nội",
                    Avatar = "https://res.cloudinary.com/dyo42vgdj/image/upload/v1713719988/ngb9ow9rzwuqaovlr5qo.jpg",
                    PhoneNumber = "0328301422",
                    UserId = Guid.NewGuid(),
                    Is_Active = false,
                }
            );
            await dbContext.SaveChangesAsync();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
        throw new ApiException(400, $"An error occurred: {ex.Message}");
    }
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseFileServer();
app.UseStaticFiles();
app.UseCustomExceptionMiddleware();
app.UseAuthentication();
app.UseCors(builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});
app.UseAuthorization();
app.MapControllers();

app.Run();
