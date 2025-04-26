namespace EnviarTrabalhos.Models.DTO
{
    public class EnviarTrabalhoSaidaDTO
    {
        public int IdTrabalho { get; set; }
        public string Status { get; set; }
        public DateTime DataEnvio { get; set; }
    }
}
