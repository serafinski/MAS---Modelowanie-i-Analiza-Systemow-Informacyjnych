using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Projekt_Koncowy.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Dodanie kontrolerów
builder.Services.AddControllers();

//Connection String
builder.Services.AddDbContext<MyDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Placówka medyczna", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Placówka medyczna API V1"));
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
// Mapuj kontrolery
app.MapControllers(); 
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();