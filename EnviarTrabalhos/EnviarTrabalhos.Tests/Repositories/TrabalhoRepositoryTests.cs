using EnviarTrabalhos.Context;
using EnviarTrabalhos.Models.Entities;
using EnviarTrabalhos.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EnviarTrabalhos.Tests.Repositories
{
    public class TrabalhoRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly TrabalhoRepository _repository;

        public TrabalhoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new AppDbContext(options);
            _repository = new TrabalhoRepository(_context);
        }

        [Fact]
        public async Task AdicionarAsync_DeveAdicionarTrabalho()
        {
            var trabalho = new Trabalho(0, "Aluno Teste", "Titulo Teste", "Conteudo Teste muito grande para passar da validação...", DateTime.UtcNow);

            await _repository.AdicionarAsync(trabalho);

            Assert.True(_context.Trabalhos.Any());
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarTrabalho()
        {
            var trabalho = new Trabalho(0, "Aluno Teste", "Titulo Teste", "Conteudo Teste com muitos caracteres...", DateTime.UtcNow);
            _context.Trabalhos.Add(trabalho);
            await _context.SaveChangesAsync();

            var resultado = await _repository.ObterPorIdAsync(trabalho.TrabalhoId);

            Assert.NotNull(resultado);
            Assert.Equal(trabalho.NomeAluno, resultado.NomeAluno);
        }

        [Fact]
        public async Task ListarTodosAsync_DeveRetornarLista()
        {
            _context.Trabalhos.Add(new Trabalho(0, "Aluno 1", "Titulo 1", "Conteudo 1 com muitos caracteres...", DateTime.UtcNow));
            _context.Trabalhos.Add(new Trabalho(0, "Aluno 2", "Titulo 2", "Conteudo 2 com muitos caracteres...", DateTime.UtcNow));
            await _context.SaveChangesAsync();

            var lista = await _repository.ListarTodosAsync();

            Assert.Equal(2, lista.Count());
        }
    }
}
