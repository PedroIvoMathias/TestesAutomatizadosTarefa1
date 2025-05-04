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



//registrando o servi�o
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//registrando para a inje��o de depend�ncia
builder.Services.AddTransient<ITrabalhoRepository, TrabalhoRepository>();
builder.Services.AddScoped<IUseCase<EnviarTrabalhoEntradaDTO, EnviarTrabalhoSaidaDTO>, EnviarTrabalhoUseCase>();

//builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();  // Aplica as migrações automaticamente ao iniciar
}
//exce��es
app.UseMiddleware<ExceptionMiddleware>();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//agora para web
//var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
//app.Urls.Add($"http://*:{port}");

//teste para rodar no rail e local:
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    app.Urls.Add($"http://*:{port}");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Trabalho}/{action=ListarTrabalhos}/{id?}");

app.Run();
