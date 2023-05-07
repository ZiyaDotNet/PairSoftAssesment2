using Microsoft.EntityFrameworkCore;
using PairSoftAPI.Models;
using PairSoftAPI.ServiceRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<PairSoftDbContext>(option => option.UseSqlServer(
   builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddScoped<IToDoListRepository, ToDoListRepository>();

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
