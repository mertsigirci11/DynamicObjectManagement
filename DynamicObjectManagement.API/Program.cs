using DynamicObjectManagement.API.Middlewares;
using DynamicObjectManagement.Core.Repositories;
using DynamicObjectManagement.Core.Services;
using DynamicObjectManagement.Core.UnitOfWorks;
using DynamicObjectManagement.Repository;
using DynamicObjectManagement.Repository.Repositories;
using DynamicObjectManagement.Repository.UnitOfWorks;
using DynamicObjectManagement.Service.Services;
using DynamicObjectManagement.Service.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Inversion
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped(typeof(IDynamicObjectRepository), typeof(DynamicObjectRepository));
builder.Services.AddScoped(typeof(IDynamicObjectService), typeof(DynamicObjectService));
builder.Services.AddScoped<ObjectValidator>();

//EntityFramework Connection
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        #pragma warning disable CS8602
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseGlobalExceptionHandling();
app.UseObjectValidator();
app.UseAuthorization();
app.MapControllers();
app.Run();