// Program.cs
using Microsoft.EntityFrameworkCore;
using SistemaRH.Data;

var builder = WebApplication.CreateBuilder(args);

// Registra o banco de dados SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=sistemarh.db"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Permite que o Front End (HTML/JS) acesse a API
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontEnd", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("PermitirFrontEnd");
app.UseStaticFiles();  // serve os arquivos HTML/CSS/JS da wwwroot
app.MapControllers();

app.Run();