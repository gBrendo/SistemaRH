// DTOs/DepartamentoDTO.cs
namespace SistemaRH.DTOs;

/// <summary>
/// Dados que o usuário envia para criar/atualizar um Departamento.
/// </summary>
public class DepartamentoInputDTO
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
}

/// <summary>
/// Dados que a API retorna sobre um Departamento.
/// </summary>
public class DepartamentoOutputDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
}