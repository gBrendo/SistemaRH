// Repositories/DepartamentoRepository.cs
using Microsoft.EntityFrameworkCore;
using SistemaRH.Data;
using SistemaRH.Models;

namespace SistemaRH.Repositories;

/// <summary>
/// Resposnavel por implementar toda comunicação com o banco
/// referente aos departamentos.
/// </summary>
public class DepartamentoRepository : IRepository<Departamento>
{
    private readonly AppDbContext _context;
    public DepartamentoRepository(AppDbContext context)
    {
        _context = context;
    }
    //acessa a tabela, ordena por nome, converte para lista e retorna.
    public async Task<IEnumerable<Departamento>> ObterTodosAsync()
    {
        return await _context.Departamentos
            .OrderBy(d => d.Nome)
            .ToListAsync();
    }
    // procura o primeiro departamento com id, ou nada se nao existir.
    public async Task<Departamento> ObterPorIdAsync(int id)
    {
        return await _context.Departamentos
            .FirstOrDefaultAsync(d => d.Id == id);
    }
    //salva os departamentos no banco de dados
    public async Task<Departamento> CriarAsync(Departamento departamento)
    {
        _context.Departamentos.Add(departamento);
        await _context.SaveChangesAsync();
        return departamento;
    }
    //marca o obj com atualizado, salva no banco.
    public async Task<Departamento> AtualizarAsync(Departamento departamento)
    {
       _context.Departamentos.Update(departamento);
        await _context.SaveChangesAsync();
        return departamento;
    }
    // remove do banco, salva, retorna true se deletou, false se nao encontrou o departamento.
    public async Task<bool> DeletarAsync(int id)
    {
        var departamento = await ObterPorIdAsync(id);
        if (departamento == null)
            return false;
        _context.Departamentos.Remove(departamento);
        await _context.SaveChangesAsync();
        return true;
    }
}
