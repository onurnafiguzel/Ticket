using Microsoft.EntityFrameworkCore;
using Ticket.Business.Abstract;
using Ticket.Business.Concrete;
using Ticket.Data;
using Ticket.Data.Abstract;
using Ticket.Data.Concrete.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("db");
builder.Services.AddDbContext<Context>(opt => opt.UseMySql(connectionString,
    new MySqlServerVersion(new Version(8, 0, 11))));

builder.Services.AddTransient<IAdminService, AdminManager>();
builder.Services.AddTransient<IAdminRepository, EfAdminRepository>();
builder.Services.AddTransient<DbContext, Context>();

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
