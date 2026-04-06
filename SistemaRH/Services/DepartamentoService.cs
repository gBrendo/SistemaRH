// Services/DepartamentoService.cs
using SistemaRH.DTOs;
using SistemaRH.Models;
using SistemaRH.Repositories;

namespace SistemaRH.Services;

public class DepartamentoService
{
    private readonly IRepository<Departamento> _repository;

    public DepartamentoService(IRepository<Departamento> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DepartamentoOutputDTO>> ObterTodosAsync()
    {
        var departamentos = await _repository.ObterTodosAsync();
        return departamentos.Select(MapearParaOutputDTO);
    }

    public async Task<DepartamentoOutputDTO?> ObterPorIdAsync(int id)
    {
        var departamento = await _repository.ObterPorIdAsync(id);
        if (departamento == null) return null;
        return MapearParaOutputDTO(departamento);
    }

    public async Task<DepartamentoOutputDTO> CriarAsync(DepartamentoInputDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nome))
            throw new ArgumentException("O nome do departamento é obrigatório.");

        var departamento = new Departamento
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao
        };

        var criado = await _repository.CriarAsync(departamento);
        return MapearParaOutputDTO(criado);
    }

    public async Task<DepartamentoOutputDTO?> AtualizarAsync(int id, DepartamentoInputDTO dto)
    {
        var departamento = await _repository.ObterPorIdAsync(id);
        if (departamento == null) return null;

        departamento.Nome = dto.Nome;
        departamento.Descricao = dto.Descricao;

        var atualizado = await _repository.AtualizarAsync(departamento);
        return MapearParaOutputDTO(atualizado);
    }

    public async Task<bool> DeletarAsync(int id)
    {
        return await _repository.DeletarAsync(id);
    }

    private DepartamentoOutputDTO MapearParaOutputDTO(Departamento d)
    {
        return new DepartamentoOutputDTO
        {
            Id = d.Id,
            Nome = d.Nome,
            Descricao = d.Descricao
        };
    }
}