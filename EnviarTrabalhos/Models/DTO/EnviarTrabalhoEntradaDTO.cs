namespace EnviarTrabalhos.Models.DTO
{
    public class EnviarTrabalhoEntradaDTO
    { // Padroniza os dados que chega do controller e é enviado para o UseCase (serve como um esqueleto)

        public string NomeAluno { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
    }
}
