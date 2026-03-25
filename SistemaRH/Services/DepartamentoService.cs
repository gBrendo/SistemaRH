// Services/DepartamentoService.cs
using SistemaRH.Models;
using SistemaRH.Repositories;

namespace SistemaRH.Services;

/// <summary>
/// Aplica as regras de negócio para Departamentos.
/// Faz a ponte entre o Controller e o Repository.
/// </summary>
public class DepartamentoService
{
    private readonly IRepository<Departamento> _repository;

    public DepartamentoService(IRepository<Departamento> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Departamento>> ObterTodosAsync()
    {
        return await _repository.ObterTodosAsync();
    }

    public async Task<Departamento?> ObterPorIdAsync(int id)
    {
        return await _repository.ObterPorIdAsync(id);
    }

    public async Task<Departamento> CriarAsync(Departamento departamento)
    {
        // Regra de negócio: nome não pode ser vazio
        if (string.IsNullOrWhiteSpace(departamento.Nome))
            throw new ArgumentException("O nome do departamento é obrigatório.");

        return await _repository.CriarAsync(departamento);
    }

    public async Task<Departamento?> AtualizarAsync(int id, Departamento departamentoAtualizado)
    {
        var departamento = await _repository.ObterPorIdAsync(id);
        if (departamento == null) return null;

        departamento.Nome = departamentoAtualizado.Nome;
        departamento.Descricao = departamentoAtualizado.Descricao;

        return await _repository.AtualizarAsync(departamento);
    }

    public async Task<bool> DeletarAsync(int id)
    {
        return await _repository.DeletarAsync(id);
    }
}