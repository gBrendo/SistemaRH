using Microsoft.EntityFrameworkCore;
using SistemaRH.Data;
using SistemaRH.Models;


namespace SistemaRH.Repositories
/// <summary>
/// Responsavel por implementar toda comunicação com o banco
/// referente aos cargos.
/// </summary>
{
    public class CargoRepository : IRepository<Cargo>
    {
        private readonly AppDbContext _context;

        public CargoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cargo>> ObterTodosAsync()
        {
            return await _context.Cargos
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<Cargo> ObterPorIdAsync(int id)
        {
            return await _context.Cargos
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cargo> CriarAsync(Cargo cargo)
        {
            _context.Cargos.Add(cargo);
            await _context.SaveChangesAsync();
            return cargo;
        }

        public async Task<Cargo> AtualizarAsync(Cargo cargo)
        {
            _context.Cargos.Update(cargo);
            await _context.SaveChangesAsync();
            return cargo;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var cargo = await ObterPorIdAsync(id);
            if (cargo == null) return false;

            _context.Cargos.Remove(cargo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
