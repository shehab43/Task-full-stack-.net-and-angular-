using Application.Helpers.MappingProfile;
using AutoMapper;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Presentation.MiddleWares;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
               .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
               .LogTo(log => Debug.WriteLine(log), LogLevel.Information)

               .EnableSensitiveDataLogging()
            );
builder.Services.AddCors();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Application.AssemblyMarker).Assembly));

builder.Services.AddAutoMapper(
    typeof(Program).Assembly,
    typeof(Application.AssemblyMarker).Assembly);
builder.Services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepository<>));

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

AutoMapperService.Mapper = app.Services.GetService<IMapper>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
