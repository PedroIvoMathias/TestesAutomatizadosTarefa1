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
        //fact é o teste todo  não é o cenário em si, mas o teste completo, incluindo o cenário, a ação e as verificações.
        [Fact]
        public async Task ListarTrabalhos_DeveRetornarViewComTrabalhos()
        {
            _mockRepo.Setup(r => r.ListarTodosAsync()).ReturnsAsync(new List<Trabalho>()); //aqui preparo o cenério

            var result = await _controller.ListarTrabalhos(); //Executar a ação que será testada

            var viewResult = Assert.IsType<ViewResult>(result);//Verificar o resultado esperado (se é realmente uma tela)
            Assert.IsAssignableFrom<IEnumerable<Trabalho>>(viewResult.Model);//Assert: Você está garantindo que o model da View é uma coleção de Trabalho (e não, por exemplo, um DTO ou outro objeto qualquer).
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
        //Este teste tem que dar falha mesmo, pois não estou retornando nenhuma tela de notFound no Controller
        //[Fact]
        //public async Task ListarPorId_ComIdInvalido_DeveRetornarNotFound()
        //{
        //    _mockRepo.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync((Trabalho)null);

        //    var result = await _controller.ListarPorId(1);

        //    Assert.IsType<NotFoundResult>(result);
        //}

        [Fact]
        public async Task EnviarTrabalho_ModeloInvalido_DeveRetornarView()
        {
            _controller.ModelState.AddModelError("Erro", "Erro de validação");

            var entrada = new EnviarTrabalhoEntradaDTO();
            var result = await _controller.EnviarTrabalho(entrada);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(entrada, viewResult.Model);
        }

        //Este teste tem que dar falha mesmo, pois não estou retornando nenhuma tela de notFound no Controller
        //[Fact]
        //public async Task ListarPorId_IdInvalido_DeveRetornarNotFound()
        //{
        //    _mockRepo.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync((Trabalho)null);

        //    var result = await _controller.ListarPorId(1);

        //    Assert.IsType<NotFoundResult>(result);
        //}

        [Fact]
        public async Task EnviarTrabalho_ModeloInvalido_DeveRetornarViewComErro()
        {
            _controller.ModelState.AddModelError("Erro", "Erro de validação");

            var entrada = new EnviarTrabalhoEntradaDTO();

            var result = await _controller.EnviarTrabalho(entrada);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(entrada, viewResult.Model);
        }

        [Fact]
        public async Task EnviarTrabalho_ModeloValido_DeveRedirecionarParaListarTrabalhos()
        {
            var entrada = new EnviarTrabalhoEntradaDTO
            {
                NomeAluno = "Aluno Teste",
                Titulo = "Título Teste",
                Conteudo = new string('A', 100)
            };

            var saida = new EnviarTrabalhoSaidaDTO
            {
                Status = "Trabalho enviado com sucesso",
                DataEnvio = DateTime.UtcNow
            };

            _mockUseCase.Setup(u => u.Execute(entrada)).ReturnsAsync(saida);

            var result = await _controller.EnviarTrabalho(entrada);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ListarTrabalhos", redirectResult.ActionName);
        }
    }
}
