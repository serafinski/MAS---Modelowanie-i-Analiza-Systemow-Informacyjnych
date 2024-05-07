using MP05.Models;
using Microsoft.EntityFrameworkCore;
using MP05.Services;

var builder = WebApplication.CreateBuilder(args);

//Dodanie Kontrolerów
builder.Services.AddControllers();

//Dodanie DoktorServices
builder.Services.AddScoped<IDoktorServices,DoktorServices>();

//Dodanie LekServices
builder.Services.AddScoped<ILekServices, LekServices>();

//Dodanie WizytaServices
builder.Services.AddScoped<IWizytaServices, WizytaServices>();

//Connection String
builder.Services.AddDbContext<MyDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Add services to the container.
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

app.MapControllers();

app.Run();