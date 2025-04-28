using EnviarTrabalhos.Context;
using EnviarTrabalhos.Exceptions;
using EnviarTrabalhos.Models.DTO;
using EnviarTrabalhos.Models.UseCase;
using EnviarTrabalhos.Repositories;
using EnviarTrabalhos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//registrando o serviço
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//registrando para a injeção de dependência
builder.Services.AddTransient<ITrabalhoRepository, TrabalhoRepository>();
builder.Services.AddScoped<IUseCase<EnviarTrabalhoEntradaDTO, EnviarTrabalhoSaidaDTO>, EnviarTrabalhoUseCase>();

//builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();
//exceções
app.UseMiddleware<ExceptionMiddleware>();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Trabalho}/{action=ListarTrabalhos}/{id?}");

app.Run();
