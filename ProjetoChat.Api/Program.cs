
// Registra o DbContext (Contexto)
// Registra o repositório (IJogoRepositorio JogoRepositorio)
// Permite injeção de dependência no Controller
// Permite acesso ao banco de dados (EF Core)
// Sua API funciona com Repository Pattern
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using ProjetoChat.Aplicacao;
using DataAccess.Repositorio;
using ProjetoChat.Servicos.interfaces;
using ProjetoChat.Dominio.Entidades;

var builder = WebApplication.CreateBuilder(args);

//Adicionar serviços ao conteiner OBS todas ver que tiver um Aplicacao tem q adicionar aqui
builder.Services.AddScoped<IUsuarioAplicacao, UsuarioAplicacao>();
//Adicionar as interfaces de banco de dados OBS todas ver que tiver um Aplicacao tem q adicionar aqui
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
//Adicionar serviços ao conteiner OBS todas ver que tiver um Aplicacao tem q adicionar aqui
builder.Services.AddScoped<ITarefaAplicacao, TarefaAplicacao>();

//Adicionar Atendimento as interfaces de banco de dados OBS todas ver que tiver um Aplicacao tem q adicionar aqui
builder.Services.AddScoped<IAtendimentoAplicacao, AtendimentoAplicacao>();
builder.Services.AddScoped<IAtendimentoRepositorio, AtendimentoRepositorio>();

//Adicionar Mensagem as interfaces de banco de dados OBS todas ver que tiver um Aplicacao tem q adicionar aqui
builder.Services.AddScoped<IMensagemAplicacao, MensagemAplicacao>();
builder.Services.AddScoped<IMensagemRepositorio, MensagemRepositorio>();

builder.Services.AddScoped<IJsonPlaceHolderServico, JsonPlaceHolderServico>();

builder.Services.AddControllers();

//Adicionando serviço de banco de dados 
builder.Services.AddDbContext<ProjetoChatContexto>(options =>options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();



