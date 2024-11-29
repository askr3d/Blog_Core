using BlogCore.Data;
using BlogCoreAccesoDatos.Data.Repository;
using BlogCoreAccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BlogCoreAccesoDatos.Data.Inicializador;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Cadena de conexion
var connectionString = builder.Configuration.GetConnectionString("ConexionMySQL");

//Conexion a mysql
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();

builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IContenedorTrabajo, ContenedorTrabajo>();

builder.Services.AddScoped<IInicializadorDB, InicializadorDB>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

SiembraDatos();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Cliente}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

void SiembraDatos()
{
    using (var scope = app.Services.CreateScope())
    {
        var inicializadorDB = scope.ServiceProvider.GetRequiredService<IInicializadorDB>();
        inicializadorDB.Inicializar();
    }
}