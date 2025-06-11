using Aplicacao;
using Dominio.Interfaces.Aplicacao;
using Dominio.Interfaces.Repositorio;
using Repositorio.Repositorios;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

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
