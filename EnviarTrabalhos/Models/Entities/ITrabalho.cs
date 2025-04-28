namespace EnviarTrabalhos.Models.Entities
{
    public interface ITrabalho
    {
        int TrabalhoId { get; set; }
        public string NomeAluno { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataEnvio { get; set; }
    }
}
