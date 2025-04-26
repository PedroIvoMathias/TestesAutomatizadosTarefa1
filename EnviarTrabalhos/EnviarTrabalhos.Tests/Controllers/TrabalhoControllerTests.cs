using EnviarTrabalhos.Controllers;
using EnviarTrabalhos.Models.DTO;
using EnviarTrabalhos.Models.Entities;
using EnviarTrabalhos.Models.UseCase;
using EnviarTrabalhos.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EnviarTrabalhos.Tests.Controllers
{
    public class TrabalhoControllerTests
    {
        private readonly Mock<ITrabalhoRepository> _mockRepo;
        private readonly Mock<IUseCase<EnviarTrabalhoEntradaDTO, EnviarTrabalhoSaidaDTO>> _mockUseCase;
        private readonly TrabalhoController _controller;

        public TrabalhoControllerTests()
        {
            _mockRepo = new Mock<ITrabalhoRepository>();
            _mockUseCase = new Mock<IUseCase<EnviarTrabalhoEntradaDTO, EnviarTrabalhoSaidaDTO>>();
            _controller = new TrabalhoController(_mockRepo.Object, _mockUseCase.Object);
        }

        [Fact]
        public async Task ListarTrabalhos_DeveRetornarViewComTrabalhos()
        {
            _mockRepo.Setup(r => r.ListarTodosAsync()).ReturnsAsync(new List<Trabalho>());

            var result = await _controller.ListarTrabalhos();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<IEnumerable<Trabalho>>(viewResult.Model);
        }

        [Fact]
        public async Task ListarPorId_ComIdValido_DeveRetornarView()
        {
            var trabalho = new Trabalho(1, "Aluno", "Titulo", "Conteudo", DateTime.UtcNow);
            _mockRepo.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(trabalho);

            var result = await _controller.ListarPorId(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(trabalho, viewResult.Model);
        }

        [Fact]
        public async Task ListarPorId_ComIdInvalido_DeveRetornarNotFound()
        {
            _mockRepo.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync((Trabalho)null);

            var result = await _controller.ListarPorId(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EnviarTrabalho_ModeloInvalido_DeveRetornarView()
        {
            _controller.ModelState.AddModelError("Erro", "Erro de validação");

            var entrada = new EnviarTrabalhoEntradaDTO();
            var result = await _controller.EnviarTrabalho(entrada);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(entrada, viewResult.Model);
        }
    }
}
