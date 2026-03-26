
// Services/CargoService.cs
using SistemaRH.Models;
using SistemaRH.Repositories;

namespace SistemaRH.Services
{
    /// <summary>
    /// Aplica as regras de negocio para cargos
    /// </summary>
    public class CargoService
    {
        private readonly IRepository<Cargo> _repository;

        public CargoService(IRepository<Cargo> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Cargo>> ObterTodosAsync()
        {
            return await _repository.ObterTodosAsync();
        }

        public async Task<Cargo?> ObterPorIdAsync(int id)
        {
            return await _repository.ObterPorIdAsync(id);
        }

        public async Task<Cargo> CriarAsync(Cargo cargo)
        {
            if (string.IsNullOrWhiteSpace(cargo.Nome))
                throw new ArgumentException("O nome do cargo é obrigatorio.");

            return await _repository.CriarAsync(cargo);
        }

        public async Task<Cargo?> AtualizarAsync(int id, Cargo cargoAtualizado)
        {
            var cargo = await _repository.ObterPorIdAsync(id);
            if (cargo == null) return null;

            cargo.Nome = cargoAtualizado.Nome;
            cargo.Descricao = cargoAtualizado.Descricao;

            return await _repository.AtualizarAsync(cargo);
        }

        public async Task<bool> DeletarAsync(int id)
        {
            return await _repository.DeletarAsync(id);
        }
    }
}
