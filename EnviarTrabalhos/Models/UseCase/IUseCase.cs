namespace EnviarTrabalhos.Models.UseCase
{
    public interface IUseCase<TEntrada,TSaida>
    {
        Task<TSaida> Execute(TEntrada entrada);
    }
}
