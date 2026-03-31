namespace SistemaRH.Models
{
    /// <summary>
    /// Representa um funcionário da empresa.
    /// Irá se relacionar com departamento e cargo.
    /// </summary>
    public class Funcionario : EntidadesBase // Herdando da classe base para ter Id e CriadoEm
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime DataAdmissao { get; set; }
        public decimal Salario { get; set; }
        public bool Ativo { get; set; } = true;

        // Chaves estrangeiras (FK) — ligam ao Departamento e Cargo, criando relacionamento
        public int DepartamentoId { get; set; }
        public int CargoId { get; set; }

        // Navegação — permite acessar os dados do Departamento e Cargo relacionados a este funcionário
        public Departamento? Departamento { get; set; }
        public Cargo? Cargo { get; set; }
    }
}
