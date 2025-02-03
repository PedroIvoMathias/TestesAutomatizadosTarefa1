import { EnviarTrabalhoUseCase } from '../src/domain/usecases/enviar-trabalho.usecase';

describe('EnviarTrabalhoUseCase', () => {
    let useCase: EnviarTrabalhoUseCase;

    beforeEach(() => {
        useCase = new EnviarTrabalhoUseCase();
    });

    it('deve criar um novo trabalho com status entregue', async () => {
        const input = {
            titulo: 'Trabalho de Teste',
            aluno: 'João',
            arquivo: {
                filename: 'teste.pdf'
            } as Express.Multer.File
        };

        const result = await useCase.execute(input);

        expect(result.trabalho).toBeDefined();
        expect(result.trabalho.titulo).toBe(input.titulo);
        expect(result.trabalho.aluno).toBe(input.aluno);
        expect(result.trabalho.arquivo).toBe(input.arquivo.filename);
        expect(result.trabalho.status).toBe('entregue');
        expect(result.trabalho.dataEntrega).toBeInstanceOf(Date);
    });
});
