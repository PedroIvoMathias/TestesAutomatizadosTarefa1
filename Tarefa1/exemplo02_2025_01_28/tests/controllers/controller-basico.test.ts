import { IUseCase } from "../../src/contracts/iusecase";
import { ControllerBasico } from "../../src/controllers/controller-basico";
import { IEntradaUseCaseBasico, ISaidaUseCaseBasico } from "../../src/domain/usecases/usecase-basico";
import { Request, Response } from "express";

class UseCaseFake implements IUseCase<IEntradaUseCaseBasico, ISaidaUseCaseBasico> {
    chamado: boolean = false;
    async perform(entrada: IEntradaUseCaseBasico): Promise<ISaidaUseCaseBasico> {
        this.chamado = true;
        return {
            valor: 0,
        } as ISaidaUseCaseBasico;
    }
}

class ResponseFake {
    statusCodeInformado: number = 0;
    jsonInformado: any = null;
    endChamado: boolean = false;

    status(code: number): ResponseFake {
        this.statusCodeInformado = code;
        return this;
    }

    json(data: any): ResponseFake {
        this.jsonInformado = data;
        return this;
    }

    end(): ResponseFake {
        this.endChamado = true;
        return this;
    }
}

function makeSUT() {
    const requestStub = {
        params: {
            valor: '10',
        },
    } as any as Request;
    const responseFake = new ResponseFake();
    const uc = new UseCaseFake();
    const controller = new ControllerBasico(uc);
    return { uc, controller, requestStub, responseFake };
}

describe('ControllerBasico', () => {
    
    it('deve instanciar ControllerBasico', () => {
        let { uc, controller, requestStub, responseFake } = makeSUT();
        expect(controller).toBeDefined();
    });

    it('deve chamar handle', async () => {
        let { uc, controller, requestStub, responseFake } = makeSUT();
        await controller.handle(requestStub, responseFake as any as Response);
        
        expect(uc.chamado).toBe(true);
        expect(responseFake.statusCodeInformado).toBe(200);
        expect(responseFake.jsonInformado.mensagem).toBe('ControllerBasico.metodoBasico() chamado');
        expect(responseFake.jsonInformado.valor).toBe(0);
        
    });

});