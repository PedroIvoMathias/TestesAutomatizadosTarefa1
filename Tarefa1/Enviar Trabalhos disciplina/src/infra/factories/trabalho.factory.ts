import { EnviarTrabalhoUseCase } from "../../domain/usecases/enviar-trabalho.usecase";

export class TrabalhoFactory {
    static makeEnviarTrabalhoUseCase(): EnviarTrabalhoUseCase {
        return new EnviarTrabalhoUseCase();
    }
}
