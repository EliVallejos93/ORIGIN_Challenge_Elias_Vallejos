using Microsoft.EntityFrameworkCore;
using ORIGIN_Challenge_Backend.Data;
using ORIGIN_Challenge_Backend.Data.Repositories;
using ORIGIN_Challenge_Backend.Data.UnitOfWork;
using ORIGIN_Challenge_Backend.Models;
using ORIGIN_Challenge_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// ----------- Add services to the container.

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar ApplicationDbContext
#region Configuracion de BD
// Me traigo la configuración de appsettings.json
var configuration = builder.Configuration;

// Agregar el DbContext al contenedor de servicios
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
#endregion

// Registrar el Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Registrar servicios personalizados
builder.Services.AddSingleton<IPinService, PinService>();
builder.Services.AddScoped<ITarjetasRepository<Tarjeta>, TarjetasRepository>();
builder.Services.AddScoped<ITarjetasService, TarjetasService>();

// Registrar IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Configurar la sesión
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


// Registrar informacion de politicas CORS
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

app.UseSession();

app.UseAuthorization();

app.MapControllers();

app.Run();
