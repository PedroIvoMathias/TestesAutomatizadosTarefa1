import * as TrabalhoService from '../src/services/trabalho.service';
import { Trabalho } from '../src/models/trabalho.model';

describe('TrabalhoService', () => {
    let mockTrabalho: Trabalho;

    beforeEach(() => {
        mockTrabalho = {
            id: '1',
            titulo: 'Trabalho de Teste',
            aluno: 'João',
            disciplina: 'Matemática',
            dataEntrega: new Date(),
            arquivo: 'caminho/do/arquivo.pdf',
            status: 'entregue'
        };
    });

    describe('salvarTrabalho', () => {
        it('deve salvar um novo trabalho', async () => {
            const { id, ...trabalhoSemId } = mockTrabalho;
            const resultado = await TrabalhoService.salvarTrabalho(trabalhoSemId as Trabalho);

            expect(resultado.id).toBeDefined();
            expect(resultado.titulo).toBe(mockTrabalho.titulo);
            expect(resultado.status).toBe('entregue');
        });
    });

    describe('listarTrabalhos', () => {
        it('deve retornar lista de trabalhos', async () => {
            await TrabalhoService.salvarTrabalho(mockTrabalho);
            const trabalhos = await TrabalhoService.listarTrabalhos();

            expect(trabalhos.length).toBeGreaterThan(0);
            expect(trabalhos[0].titulo).toBe(mockTrabalho.titulo);
        });
    });

    describe('avaliarTrabalho', () => {
        it('deve avaliar um trabalho existente', async () => {
            const trabalhoSalvo = await TrabalhoService.salvarTrabalho(mockTrabalho);
            const nota = 9.5;
            const comentarios = 'Muito bom!';

            const trabalhoAvaliado = await TrabalhoService.avaliarTrabalho(
                trabalhoSalvo.id,
                nota,
                comentarios
            );

            expect(trabalhoAvaliado.nota).toBe(nota);
            expect(trabalhoAvaliado.comentarios).toBe(comentarios);
            expect(trabalhoAvaliado.status).toBe('corrigido');
        });

        it('deve lançar erro ao avaliar trabalho inexistente', async () => {
            await expect(
                TrabalhoService.avaliarTrabalho('id-inexistente', 9.5, 'comentário')
            ).rejects.toThrow('Trabalho não encontrado');
        });
    });
});
