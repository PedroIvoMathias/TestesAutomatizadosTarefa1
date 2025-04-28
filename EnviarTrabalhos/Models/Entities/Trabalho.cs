
namespace EnviarTrabalhos.Models.Entities
{
    public class Trabalho : ITrabalho
    {
        public int TrabalhoId { get; set; }
        public string NomeAluno { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataEnvio { get; set ; }

        public Trabalho() { }

        public Trabalho(int trabalhoId, string nomeAluno, string titulo, string conteudo, DateTime dataEnvio)
        {
            TrabalhoId = trabalhoId;
            NomeAluno = nomeAluno;
            Titulo = titulo;
            Conteudo = conteudo;
            DataEnvio = dataEnvio;
        }
    }
}
