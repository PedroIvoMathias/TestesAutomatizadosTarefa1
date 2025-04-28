using EnviarTrabalhos.Models.Entities;

namespace EnviarTrabalhos.Models.DTO
{
    public class EnviarTrabalhoSaidaDTO
    { // Padroniza os dados que sai do controller e é enviado para o banco de dados (serve como um esqueleto)
        public int IdTrabalho { get; set; }
        public string Status { get; set; }
        public DateTime DataEnvio { get; set; }
        public Trabalho Trabalho { get; set; }
    }
}
