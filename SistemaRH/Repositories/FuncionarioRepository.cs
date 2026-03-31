// Repositories/FuncionarioRepository.cs
using Microsoft.EntityFrameworkCore;
using SistemaRH.Data;
using SistemaRH.Models;

namespace SistemaRH.Repositories;

public class FuncionarioRepository : IRepository<Funcionario>
{
    private readonly AppDbContext _context;

    public FuncionarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Funcionario>> ObterTodosAsync()
    {
        return await _context.Funcionarios
            .Include(f => f.Departamento)  // carrega dados do Departamento
            .Include(f => f.Cargo)         // carrega dados do Cargo
            .OrderBy(f => f.Nome)
            .ToListAsync();
    }

    public async Task<Funcionario?> ObterPorIdAsync(int id)
    {
        return await _context.Funcionarios
            .Include(f => f.Departamento)
            .Include(f => f.Cargo)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Funcionario> CriarAsync(Funcionario funcionario)
    {
        _context.Funcionarios.Add(funcionario);
        await _context.SaveChangesAsync();
        return funcionario;
    }

    public async Task<Funcionario> AtualizarAsync(Funcionario funcionario)
    {
        _context.Funcionarios.Update(funcionario);
        await _context.SaveChangesAsync();
        return funcionario;
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var funcionario = await ObterPorIdAsync(id);
        if (funcionario == null) return false;

        _context.Funcionarios.Remove(funcionario);
        await _context.SaveChangesAsync();
        return true;
    }

    // Método extra — filtro por nome ou departamento
    public async Task<IEnumerable<Funcionario>> FiltrarAsync(
        string? nome, int? departamentoId)
    {
        var query = _context.Funcionarios
            .Include(f => f.Departamento)
            .Include(f => f.Cargo)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(nome))
            query = query.Where(f => f.Nome.Contains(nome));

        if (departamentoId.HasValue)
            query = query.Where(f => f.DepartamentoId == departamentoId);

        return await query.OrderBy(f => f.Nome).ToListAsync();
    }
}