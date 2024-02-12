using Microsoft.AspNetCore.ResponseCompression;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using MercadonaBlazor2024.Server.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();





String _cadenaConexionBD = builder.Configuration.GetConnectionString("BlazorSqlServerConnectionString");
String _nombreEnsamblado = Assembly.GetExecutingAssembly().GetName().Name;

builder.Services.AddDbContext<AaplicacionDBContext>((DbContextOptionsBuilder opciones) =>
{
    opciones.UseSqlServer(_cadenaConexionBD, (SqlServerDbContextOptionsBuilder opc) => opc.MigrationsAssembly(_nombreEnsamblado));
});

//2º paso: configuro servicios de Identity: UserManager y SignInManager
builder.Services.AddIdentity<MiClienteIdentity, IdentityRole>(
                        (IdentityOptions opciones) =>
                        {

                            //opciones configuracion UserManager...
                            opciones.Password = new PasswordOptions
                            {
                                RequireDigit = true,
                                RequireUppercase = true,
                                RequireLowercase = true,
                                RequireNonAlphanumeric = true,
                                RequiredLength = 6
                            };
                            opciones.User = new UserOptions { RequireUniqueEmail = true };


                            //opciones configuracion SignInManager...
                            opciones.SignIn = new SignInOptions { RequireConfirmedEmail = true };
                            opciones.Lockout = new LockoutOptions
                            {
                                AllowedForNewUsers = false,
                                MaxFailedAccessAttempts = 3,
                                DefaultLockoutTimeSpan = TimeSpan.FromHours(3)
                            };
                        }
                 )
                .AddEntityFrameworkStores<AaplicacionDBContext>()
                .AddDefaultTokenProviders();

byte[] _bytesFirma = System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:firmaJWT"]);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) //<---- cambia el esquema de autentificacion a JWT frente a las cookies q se usan por defecto
                .AddJwtBearer(
                    (JwtBearerOptions opciones) =>
                    {
                        opciones.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidateIssuer = true, //<---- validar si el jwt ha sido generado por mi servidor (claim "issuer")
                            ValidateLifetime = true, //<---validar la fecha de expiracion del jwt (claim "exp")
                            ValidateIssuerSigningKey = true,//<----validar si el jwt ha sido firmado por el servidor
                            ValidateAudience = false, //<---- validar subdominios para los q es valido el jwt (claim "audience")
                            ValidIssuer = builder.Configuration["JWT:issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(_bytesFirma)
                        };
                    }
                );

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
