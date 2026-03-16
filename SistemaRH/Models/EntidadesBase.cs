namespace SistemaRH.Models
{
    /// <summary>
    /// Classe base para todas as entidades do sistema
    /// Todo Model vai herndar Id e CriadoEm automaticamente devido a herança dessa classe
    /// </summary>
    public class EntidadesBase
    {
        public int Id { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;
    }
}
