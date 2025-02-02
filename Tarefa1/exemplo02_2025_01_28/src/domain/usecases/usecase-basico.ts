import { IUseCase } from "../../contracts/iusecase";

export interface IEntradaUseCaseBasico {
    valor: number;
}

export interface ISaidaUseCaseBasico {
    valor: number;
}

export class UseCaseBasico implements IUseCase<IEntradaUseCaseBasico, ISaidaUseCaseBasico> {
    constructor() {
        console.log('UseCaseBasico instanciado');
    }

    public async perform(entrada: IEntradaUseCaseBasico): Promise<ISaidaUseCaseBasico> {
        const { valor } = entrada;

        console.log('UseCaseBasico.metodoBasico() chamado', valor);
        const valor_processado: number = valor;

        const saida: ISaidaUseCaseBasico = {
            valor: valor_processado,
        };
        return saida;
    }
}
