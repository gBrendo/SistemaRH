namespace SistemaRH.Models
{
    /// <summary>
    /// Representa um cargo dentro da empresa.
    /// Ex: analista, gerente, coordenador, desenvolvedor, etc.
    /// </summary>
    public class Cargo : EntidadesBase // herda o Id e CriadoEm da classe base
    {
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; } // ? = campo opcional
        
        // Navegação: um Cargo tem vários Funcionários
        public ICollection<Funcionario> Funcionarios { get; set; }
            = new List<Funcionario>();
    }
}
