using EnviarTrabalhos.Exceptions;
using EnviarTrabalhos.Models.DTO;
using EnviarTrabalhos.Models.Entities;

namespace EnviarTrabalhos.Models.UseCase
{
    public class EnviarTrabalhoUseCase : IUseCase<EnviarTrabalhoEntradaDTO, EnviarTrabalhoSaidaDTO>
    {
        public async Task<EnviarTrabalhoSaidaDTO> Execute(EnviarTrabalhoEntradaDTO entrada)
        {
            // Validação dos dados de entrada
            if (string.IsNullOrWhiteSpace(entrada.NomeAluno) || string.IsNullOrWhiteSpace(entrada.Titulo) || string.IsNullOrWhiteSpace(entrada.Conteudo))
                throw new BusinessException("Nome do aluno, título e conteúdo são obrigatórios.");

            if (entrada.Conteudo.Length < 50)  // Validação simples de conteúdo
                throw new BusinessException("O conteúdo do trabalho deve ter pelo menos 50 caracteres.");

            // Criação do objeto Trabalho
            var trabalho = new Trabalho(
                trabalhoId:0,  // Aqui o ID do trabalho poderia ser gerado pelo banco de dados
                nomeAluno: entrada.NomeAluno,
                titulo: entrada.Titulo,
                conteudo: entrada.Conteudo,
                dataEnvio: DateTime.UtcNow
            );

            // Retornando um objeto de saída que pode incluir um status de sucesso, ID do trabalho, etc
            var saida =  new EnviarTrabalhoSaidaDTO
            {
                IdTrabalho = trabalho.TrabalhoId, // O ID gerado pelo banco
                Status = "Trabalho enviado com sucesso",
                DataEnvio = trabalho.DataEnvio
            };

            return await Task.FromResult( saida );
        }
    }
}
