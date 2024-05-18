using Microsoft.EntityFrameworkCore;
using ORIGIN_Challenge_Backend.Models;
using ORIGIN_Challenge_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// ----------- Add services to the container.

#region Configuracion de BD
// Me traigo la configuración de appsettings.json
var configuration = builder.Configuration;

// Agregar el DbContext al contenedor de servicios
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IntentosPinService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Policy",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });

});

var app = builder.Build();

app.UseCors("Policy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
