using Aplicacao;
using Dominio.Interfaces.Aplicacao;
using Dominio.Interfaces.Repositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Repositorio.Repositorios;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Secrets.json", optional: true, reloadOnChange: true);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    }); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Repositórios (camada de infraestrutura)
builder.Services.AddScoped<ITipoUsuarioRepositorio, TipoUsuarioRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IStatusConsultaRepositorio, StatusConsultaRepositorio>();
builder.Services.AddScoped<IPacienteRepositorio, PacienteRepositorio>();
builder.Services.AddScoped<IMedicoRepositorio, MedicoRepositorio>();
builder.Services.AddScoped<IEspecialidadeRepositorio, EspecialidadeRepositorio>();
builder.Services.AddScoped<IConsultaRepositorio, ConsultaRepositorio>();

// Aplicações (camada de aplicação)
builder.Services.AddScoped<ITipoUsuarioAplicacao, TipoUsuarioAplicacao>();
builder.Services.AddScoped<IUsuarioAplicacao, UsuarioAplicacao>();
builder.Services.AddScoped<IStatusConsultaAplicacao, StatusConsultaAplicacao>();
builder.Services.AddScoped<IPacienteAplicacao, PacienteAplicacao>();
builder.Services.AddScoped<IMedicoAplicacao, MedicoAplicacao>();
builder.Services.AddScoped<IEspecialidadeAplicacao, EspecialidadeAplicacao>();
builder.Services.AddScoped<IConsultaAplicacao, ConsultaAplicacao>();

//configs  authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
