
//Repositories/IRepository.cs
namespace SistemaRH.Repositories
{
    /// <summary>
    /// Contrato generico para todos os repositorios do sistema.
    /// O  T  significa que a interface é generica e pode ser implementada por qualquer tipo de entidade.
    /// </summary>
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<T> ObterPorIdAsync(int id);
        Task<T> CriarAsync(T entidade);
        Task<T> AtualizarAsync(T entidade);
        Task<bool> DeletarAsync(int id);

    }
}
