using App.Repositorio.Implementaciones;
using App.Repositorio.Interfaces;
using App.Servicios.Implementaciones;
using App.Servicios.Interfaces;
using App.Servicios.Mapeos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using App.Repositorio.ConexionDB;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Configurar Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Parqueadero", Version = "v1" });

    // Definir esquema de seguridad (Bearer Token)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token en el siguiente formato: Bearer {token}"
    });

    // Hacer que todos los endpoints requieran el token por defecto
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//Inyeccion de Dependencias
//Conexion Base de Datos
builder.Configuration.AddJsonFile("appsettings.json");
Conexion.Inicializar(builder.Configuration);
//AutoMapper
builder.Services.AddAutoMapper(typeof(VehiculoMapper));
builder.Services.AddAutoMapper(typeof(TarifaMapper));
builder.Services.AddAutoMapper(typeof(FacturaMapper));
builder.Services.AddAutoMapper(typeof(ParqueaderoMapper));
builder.Services.AddAutoMapper(typeof(UsuarioMapper));
//Repositorio
builder.Services.AddScoped<IVehiculoRepositorio, VehiculoRepositorio>();
builder.Services.AddScoped<ITarifaRepositorio, TarifaRepositorio>();
builder.Services.AddScoped<IFacturaRepositorio, FacturaRepositorio>();
builder.Services.AddScoped<IParqueaderoRepositorio, ParqueaderoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
//Servicios-Negocio
builder.Services.AddScoped<IVehiculoServicio, VehiculoServicio>();
builder.Services.AddScoped<ITarifaServicio, TarifaServicio>();
builder.Services.AddScoped<IFacturaServicio, FacturaServicio>();
builder.Services.AddScoped<IParqueaderoServicio, ParqueaderoServicio>();
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();

//Configuracion Autenticacion JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
//Habilitar CORS para Consumir los servicios
builder.Services.AddCors(options =>
{
    options.AddPolicy("App.Web", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .WithMethods("GET", "POST", "PUT", "DELETE")
              .WithHeaders("Content-Type", "Authorization")
              .AllowCredentials(); 
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("App.Web");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
