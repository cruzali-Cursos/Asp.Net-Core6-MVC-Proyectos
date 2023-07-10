using Aprende_ASPNETCoreMVC6;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Se especifica que solo se acepten usuarios autenticados
var politicaUsuariosAutenticados = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

// Add services to the container.
builder.Services.AddControllersWithViews(opciones => 
{
    opciones.Filters.Add(new AuthorizeFilter(politicaUsuariosAutenticados));
});

builder.Services.AddDbContext<AppDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));

// Identity
// Activar servicios de autenticacion
builder.Services.AddAuthentication().AddMicrosoftAccount(opciones =>
{
    opciones.ClientId = builder.Configuration["MicrosoftClientId"];
    opciones.ClientSecret = builder.Configuration["MicrosoftSecretId"];
});

// Agregar el sistema de Identity como tal
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
{
    opciones.SignIn.RequireConfirmedAccount = false;

}).AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// No usar las vistas que proporciona Identity, SINO mas bien usar nuestro propio diseño
builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
    opciones =>
    {
        // Se redirecciona cuando un usuario no tiene permisos
        opciones.LoginPath = "/usuarios/login";
        opciones.AccessDeniedPath = "/usuarios/login";
    });

// IStringLocalizer
builder.Services.AddLocalization(opciones =>
{
    opciones.ResourcesPath = "Recursos";
});

var app = builder.Build();

var culturasUISoportadas = new[] { "es", "en" };

app.UseRequestLocalization(opciones =>
{
    opciones.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("es");
    opciones.SupportedUICultures = culturasUISoportadas
        .Select(cultura => new System.Globalization.CultureInfo(cultura)).ToList();
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Middelware - Nos permite obtener la data del usuario autenticado
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
