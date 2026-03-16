using Microsoft.EntityFrameworkCore;
using SistemaRH.Models;


namespace SistemaRH.Data;

/// <summary>
/// Ponto central de comunicação com o bando de dados
/// o entity framework usa essa classe para criar e gerenciar as tabelas.
/// </summary>

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<Cargo> Cargos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configuração do funcionario
        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.Property(f => f.Nome).IsRequired().HasMaxLength(100);
            entity.Property(f => f.Email).IsRequired().HasMaxLength(150);
            entity.Property(f => f.Salario).HasColumnType("decimal(10,2)");
        });
        //Configuração do departamento
        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.Property(d => d.Nome).IsRequired().HasMaxLength(80);
        });
        //Configuração do cargo
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.Property(c => c.Nome).IsRequired().HasMaxLength(80);
        });
    }
}