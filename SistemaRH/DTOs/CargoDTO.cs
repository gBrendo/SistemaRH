// DTOs/CargoDTO.cs
namespace SistemaRH.DTOs;

/// <summary>
/// Dados que o usuário envia para criar/atualizar um Cargo.
/// </summary>
public class CargoInputDTO
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
}

/// <summary>
/// Dados que a API retorna sobre um Cargo.
/// </summary>
public class CargoOutputDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
}