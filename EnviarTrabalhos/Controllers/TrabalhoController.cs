using EnviarTrabalhos.Models.DTO;
using EnviarTrabalhos.Models.Entities;
using EnviarTrabalhos.Models.UseCase;
using EnviarTrabalhos.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnviarTrabalhos.Controllers
{
    public class TrabalhoController : BaseTrabalhoController
    {//dependendo apenas das interfaces, não dos objetos em si.
        private readonly ITrabalhoRepository _trabalhoRepository;
        private readonly IUseCase<EnviarTrabalhoEntradaDTO, EnviarTrabalhoSaidaDTO> _enviarTrabalhoUseCase;

        public TrabalhoController(ITrabalhoRepository trabalhoRepository, IUseCase<EnviarTrabalhoEntradaDTO, EnviarTrabalhoSaidaDTO> enviarTrabalhoUseCase)
        {
            _trabalhoRepository = trabalhoRepository;
            _enviarTrabalhoUseCase = enviarTrabalhoUseCase;
        }

        public async Task< IActionResult> ListarTrabalhos()
        {
            var trabalhos = await _trabalhoRepository.ListarTodosAsync();
            return ApiOrView(trabalhos);
        }
        
        public async Task< IActionResult> ListarPorId(int id)
        {
            var trabalho = await _trabalhoRepository.ObterPorIdAsync(id);
            return ApiOrView(trabalho);
        }

        public IActionResult EnviarTrabalho()
        {
            return View();
        }

        // POST: Enviar Trabalho
        //Depois de clicar em enviar na view, ele pega todos os dados que vem de lá e joga na variavel entrada
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnviarTrabalho(EnviarTrabalhoEntradaDTO entrada)
        {
            if (!ModelState.IsValid)
            {//retorna a tela preenchida com os valores que ele colocou antes
                return View(entrada);
            }

            var resultado = await _enviarTrabalhoUseCase.Execute(entrada);


            await _trabalhoRepository.AdicionarAsync(resultado.Trabalho);

            return RedirectToAction(nameof(ListarTrabalhos));
        }


        private bool IsApiRequest()
        {
            var acceptHeader = Request.Headers["Accept"].ToString();
            return acceptHeader.Contains("application/json", StringComparison.OrdinalIgnoreCase);
        }
    }
}