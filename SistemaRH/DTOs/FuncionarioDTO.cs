// DTOs/FuncionarioDTO.cs
namespace SistemaRH.DTOs;

/// <summary>
/// Dados que o usuário envia para criar/atualizar um Funcionário.
/// </summary>
public class FuncionarioInputDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public DateTime DataAdmissao { get; set; }
    public decimal Salario { get; set; }
    public int DepartamentoId { get; set; }
    public int CargoId { get; set; }
}

/// <summary>
/// Dados que a API retorna sobre um Funcionário.
/// Inclui o nome do Departamento e Cargo ao invés dos IDs.
/// </summary>
public class FuncionarioOutputDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public DateTime DataAdmissao { get; set; }
    public decimal Salario { get; set; }
    public bool Ativo { get; set; }
    public string DepartamentoNome { get; set; } = string.Empty;
    public string CargoNome { get; set; } = string.Empty;
}