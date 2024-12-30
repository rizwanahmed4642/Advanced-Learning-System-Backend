using JWTAuthentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AutoMapper.Features;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using LMS.DAL.Models.DbModels;
using Microsoft.AspNetCore.Authentication;
using LMS.BAL.Interfaces;
using LMS.BAL.Services;
using LMS.DAL.Repositories._UOW;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Register Authentication Services 
builder.Services.AddTokenAuthentication(builder.Configuration);
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.TryAddScoped<TokenService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddDbContext<AdvancedLearningSystemdbContext>(options =>
options.UseSqlServer(
          builder.Configuration.GetConnectionString("DefaultConnection")
         ));

#region Register Project Services 
builder.Services.TryAddScoped<HttpClient>();
builder.Services.TryAddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<IStudent, StudentService>();
builder.Services.AddScoped<ITeacher, TeacherService>();
builder.Services.AddScoped<IParent, ParentService>();
builder.Services.AddScoped<ILibrary, LibraryService>();
#endregion

#region UnitOfWork
builder.Services.TryAddScoped<UnitOfWork<Student>>();
builder.Services.TryAddScoped<UnitOfWork<Teacher>>();
builder.Services.TryAddScoped<UnitOfWork<Parent>>();
builder.Services.TryAddScoped<UnitOfWork<Library>>();
#endregion

var app = builder.Build();
app.UseCors(corsapp);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
