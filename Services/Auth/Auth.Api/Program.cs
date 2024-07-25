using AuthDAL.Repositories;
using AutoMapper.Features;
using AutoMapper;
using DAL.Models.DbModels;
using DAL.Repositories.UOW;
using FileHandler;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Data;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// ************ This is for Auth Service ************* //
#region Register Authentication Services 

builder.Services.AddTokenAuthentication(builder.Configuration);
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.TryAddScoped<TokenService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.TryAddScoped<UploadFiles>();

//CORS
var corsapp = "corsapp";
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment}.json", optional: true)
        .AddEnvironmentVariables();

builder.Services.AddDbContext<AdvancedLearningSystemDbContext>(options =>
options.UseSqlServer(
          builder.Configuration.GetConnectionString("DefaultConnection")
         ));

//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.InstanceName = builder.Configuration.GetSection("RedisCache:InstanceName").Value; // For Production "Prod-HMIS-Portal" , For Staging and Dev "Prod-HMIS-Portal"
//    options.Configuration = $"{builder.Configuration.GetSection("RedisCache:Host").Value}:{builder.Configuration.GetSection("RedisCache:Port").Value},password={builder.Configuration.GetSection("RedisCache:Password").Value}";
//});

#endregion



#region Register Project Services 
//builder.Services.TryAddScoped(typeof(PagedListDto<>));
builder.Services.TryAddScoped<AuthRespository>();
builder.Services.TryAddScoped<UserRepository<User>>();
builder.Services.TryAddScoped<HttpClient>();


//*********** UOW Registered ************** //
builder.Services.TryAddScoped<UnitOfWork<User>>();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
