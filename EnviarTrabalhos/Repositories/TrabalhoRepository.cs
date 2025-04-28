using EnviarTrabalhos.Context;
using EnviarTrabalhos.Models.Entities;
using EnviarTrabalhos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnviarTrabalhos.Repositories
{
    public class TrabalhoRepository : ITrabalhoRepository
    {
        private readonly AppDbContext _context;

        public TrabalhoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Trabalho trabalho)
        {
            await _context.Trabalhos.AddAsync(trabalho);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Trabalho>> ListarTodosAsync()
        {
            return await _context.Trabalhos.ToListAsync();
        }

        public async Task<Trabalho> ObterPorIdAsync(int id)
        {
            return await _context.Trabalhos.FindAsync(id);
        }
    }
}
