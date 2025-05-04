using EnviarTrabalhos.Exceptions;
using EnviarTrabalhos.Models.DTO;
using EnviarTrabalhos.Models.UseCase;
using Xunit;

namespace EnviarTrabalhos.Tests.UseCases
{
    public class EnviarTrabalhoUseCaseTests
    {
        private readonly EnviarTrabalhoUseCase _useCase;

        public EnviarTrabalhoUseCaseTests()
        {
            _useCase = new EnviarTrabalhoUseCase();
        }

        [Fact]
        public async Task Execute_DadosValidos_DeveRetornarSaida()
        {
            var entrada = new EnviarTrabalhoEntradaDTO
            {
                NomeAluno = "João",
                Titulo = "Trabalho de História",
                Conteudo = new string('A', 60)
            };

            var resultado = await _useCase.Execute(entrada);

            Assert.NotNull(resultado);
            Assert.Equal("Trabalho enviado com sucesso", resultado.Status);
            Assert.True(resultado.DataEnvio <= DateTime.UtcNow);
        }

        [Theory]
        [InlineData("", "Título", "Conteúdo válido com mais de 50 caracteres...")]
        [InlineData("Aluno", "", "Conteúdo válido com mais de 50 caracteres...")]
        [InlineData("Aluno", "Título", "")]
        public async Task Execute_CamposObrigatoriosVazios_DeveLancarBusinessException(string nome, string titulo, string conteudo)
        {
            var entrada = new EnviarTrabalhoEntradaDTO
            {
                NomeAluno = nome,
                Titulo = titulo,
                Conteudo = conteudo
            };

            await Assert.ThrowsAsync<BusinessException>(() => _useCase.Execute(entrada));
        }

        [Fact]
        public async Task Execute_ComConteudoCurto_DeveLancarBusinessException()
        {
            var entrada = new EnviarTrabalhoEntradaDTO
            {
                NomeAluno = "Aluno",
                Titulo = "Titulo",
                Conteudo = "Curto"
            };

            await Assert.ThrowsAsync<BusinessException>(() => _useCase.Execute(entrada));
        }
        [Fact]
        public async Task Execute_ConteudoComExatamente50Caracteres_DeveRetornarSaida()
        {
            var entrada = new EnviarTrabalhoEntradaDTO
            {
                NomeAluno = "Aluno",
                Titulo = "Título",
                Conteudo = new string('A', 50) // Exatamente 50 caracteres
            };

            var resultado = await _useCase.Execute(entrada);

            Assert.NotNull(resultado);
            Assert.Equal("Trabalho enviado com sucesso", resultado.Status);
        }

        //[Fact]
        //public async Task Execute_NomeComEspacosEmBranco_DeveLancarBusinessException()
        //{
        //    var entrada = new EnviarTrabalhoEntradaDTO
        //    {
        //        NomeAluno = "   ",  // Só espaços
        //        Titulo = "Título",
        //        Conteudo = new string('A', 60)
        //    };

        //    await Assert.ThrowsAsync<BusinessException>(() => _useCase.Execute(entrada));
        //}

        [Fact]
        public async Task Execute_ConteudoMuitoGrande_DeveRetornarSaida()
        {
            var entrada = new EnviarTrabalhoEntradaDTO
            {
                NomeAluno = "Aluno",
                Titulo = "Título",
                Conteudo = new string('A', 1000) // 1000 caracteres
            };

            var resultado = await _useCase.Execute(entrada);

            Assert.NotNull(resultado);
            Assert.Equal("Trabalho enviado com sucesso", resultado.Status);
        }

        [Fact]
        public async Task Execute_EntradaNula_DeveLancarBusinessException()
        {
            await Assert.ThrowsAsync<BusinessException>(() => _useCase.Execute(null));
        }
    }
}
