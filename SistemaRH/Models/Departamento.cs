namespace SistemaRH.Models
{
    /// <summary>
    /// Representa um departamento da empresa
    /// ex: TI, RH, Financeiro, Comercial.
    /// </summary>
    public class Departamento : EntidadesBase // herda o Id e CriadoEm da classe base
    {
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; } // ? = campo opcional

        // Navegação: um Departamento tem vários Funcionários
        public ICollection<Funcionario> Funcionarios { get; set; }
            = new List<Funcionario>();
    }
}
