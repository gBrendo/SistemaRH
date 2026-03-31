// Services/FuncionarioService.cs
using SistemaRH.DTOs;
using SistemaRH.Models;
using SistemaRH.Repositories;

namespace SistemaRH.Services;

public class FuncionarioService
{
    private readonly FuncionarioRepository _repository;

    public FuncionarioService(FuncionarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<FuncionarioOutputDTO>> ObterTodosAsync()
    {
        var funcionarios = await _repository.ObterTodosAsync();
        return funcionarios.Select(MapearParaOutputDTO);
    }

    public async Task<FuncionarioOutputDTO?> ObterPorIdAsync(int id)
    {
        var funcionario = await _repository.ObterPorIdAsync(id);
        if (funcionario == null) return null;
        return MapearParaOutputDTO(funcionario);
    }

    public async Task<IEnumerable<FuncionarioOutputDTO>> FiltrarAsync(
        string? nome, int? departamentoId)
    {
        var funcionarios = await _repository.FiltrarAsync(nome, departamentoId);
        return funcionarios.Select(MapearParaOutputDTO);
    }

    public async Task<FuncionarioOutputDTO> CriarAsync(FuncionarioInputDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nome))
            throw new ArgumentException("O nome do funcionário é obrigatório.");

        if (string.IsNullOrWhiteSpace(dto.Email))
            throw new ArgumentException("O email do funcionário é obrigatório.");

        var funcionario = MapearParaModel(dto);
        var criado = await _repository.CriarAsync(funcionario);

        // Recarrega com Departamento e Cargo incluídos
        var completo = await _repository.ObterPorIdAsync(criado.Id);
        return MapearParaOutputDTO(completo!);
    }

    public async Task<FuncionarioOutputDTO?> AtualizarAsync(
        int id, FuncionarioInputDTO dto)
    {
        var funcionario = await _repository.ObterPorIdAsync(id);
        if (funcionario == null) return null;

        funcionario.Nome = dto.Nome;
        funcionario.Email = dto.Email;
        funcionario.Telefone = dto.Telefone;
        funcionario.DataAdmissao = dto.DataAdmissao;
        funcionario.Salario = dto.Salario;
        funcionario.DepartamentoId = dto.DepartamentoId;
        funcionario.CargoId = dto.CargoId;

        await _repository.AtualizarAsync(funcionario);
        var atualizado = await _repository.ObterPorIdAsync(id);
        return MapearParaOutputDTO(atualizado!);
    }

    public async Task<bool> DeletarAsync(int id)
    {
        return await _repository.DeletarAsync(id);
    }

    // Converte Model → OutputDTO
    private FuncionarioOutputDTO MapearParaOutputDTO(Funcionario f)
    {
        return new FuncionarioOutputDTO
        {
            Id = f.Id,
            Nome = f.Nome,
            Email = f.Email,
            Telefone = f.Telefone,
            DataAdmissao = f.DataAdmissao,
            Salario = f.Salario,
            Ativo = f.Ativo,
            DepartamentoNome = f.Departamento?.Nome ?? "Não informado",
            CargoNome = f.Cargo?.Nome ?? "Não informado"
        };
    }

    // Converte InputDTO → Model
    private Funcionario MapearParaModel(FuncionarioInputDTO dto)
    {
        return new Funcionario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Telefone = dto.Telefone,
            DataAdmissao = dto.DataAdmissao,
            Salario = dto.Salario,
            DepartamentoId = dto.DepartamentoId,
            CargoId = dto.CargoId
        };
    }
}