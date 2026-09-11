using EmprestimoLibrary.Data;
using EmprestimoLibrary.Exceptions;
using EmprestimoLibrary.Repositories;
using EmprestimoLibrary.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ControleEmprestimoLivroContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddScoped<ILivroRepository, LivroRepository>();

builder.Services.AddScoped<ILivroService, LivroService>();

builder.Services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
builder.Services.AddScoped<IEmprestimoService, EmprestimoService>();


var app = builder.Build();

//UTILIZAR MIDDLEWARE
app.UseMiddleware<ExceptionMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
