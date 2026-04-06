// Services/CargoService.cs
using SistemaRH.DTOs;
using SistemaRH.Models;
using SistemaRH.Repositories;

namespace SistemaRH.Services;

public class CargoService
{
    private readonly IRepository<Cargo> _repository;

    public CargoService(IRepository<Cargo> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CargoOutputDTO>> ObterTodosAsync()
    {
        var cargos = await _repository.ObterTodosAsync();
        return cargos.Select(MapearParaOutputDTO);
    }

    public async Task<CargoOutputDTO?> ObterPorIdAsync(int id)
    {
        var cargo = await _repository.ObterPorIdAsync(id);
        if (cargo == null) return null;
        return MapearParaOutputDTO(cargo);
    }

    public async Task<CargoOutputDTO> CriarAsync(CargoInputDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nome))
            throw new ArgumentException("O nome do cargo é obrigatório.");

        var cargo = new Cargo
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao
        };

        var criado = await _repository.CriarAsync(cargo);
        return MapearParaOutputDTO(criado);
    }

    public async Task<CargoOutputDTO?> AtualizarAsync(int id, CargoInputDTO dto)
    {
        var cargo = await _repository.ObterPorIdAsync(id);
        if (cargo == null) return null;

        cargo.Nome = dto.Nome;
        cargo.Descricao = dto.Descricao;

        var atualizado = await _repository.AtualizarAsync(cargo);
        return MapearParaOutputDTO(atualizado);
    }

    public async Task<bool> DeletarAsync(int id)
    {
        return await _repository.DeletarAsync(id);
    }

    private CargoOutputDTO MapearParaOutputDTO(Cargo c)
    {
        return new CargoOutputDTO
        {
            Id = c.Id,
            Nome = c.Nome,
            Descricao = c.Descricao
        };
    }
}