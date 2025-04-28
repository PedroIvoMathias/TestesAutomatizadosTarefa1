using EnviarTrabalhos.Models.DTO;
using EnviarTrabalhos.Models.Entities;
using EnviarTrabalhos.Models.UseCase;
using EnviarTrabalhos.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnviarTrabalhos.Controllers
{
    public class TrabalhoController : Controller
    {
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
            return View(trabalhos);
        }
        
        public async Task< IActionResult> ListarPorId(int id)
        {
            var trabalho = await _trabalhoRepository.ObterPorIdAsync(id);

            return View(trabalho);
        }

        public IActionResult EnviarTrabalho()
        {
            return View();
        }

        // POST: Enviar Trabalho
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnviarTrabalho(EnviarTrabalhoEntradaDTO entrada)
        {
            if (!ModelState.IsValid)
            {
                return View(entrada);
            }

            var resultado = await _enviarTrabalhoUseCase.Execute(entrada);


            await _trabalhoRepository.AdicionarAsync(resultado.Trabalho);

            return RedirectToAction(nameof(ListarTrabalhos));
        }

    }
}
