using EnviarTrabalhos.Models.Entities;

namespace EnviarTrabalhos.Repositories.Interfaces
{
    public interface ITrabalhoRepository
    {
        Task AdicionarAsync(Trabalho trabalho);
        Task<Trabalho?> ObterPorIdAsync(int id);
        Task<List<Trabalho>> ListarTodosAsync();
    }
}
