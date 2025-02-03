import { Request, Response } from 'express';
import { TrabalhoController } from '../src/controllers/trabalho.controller';
import * as TrabalhoService from '../src/services/trabalho.service';
import { Trabalho } from '../src/models/trabalho.model';

jest.mock('../src/services/trabalho.service');

describe('TrabalhoController', () => {
    let controller: TrabalhoController;
    let mockRequest: Partial<Request>;
    let mockResponse: Partial<Response>;
    let mockTrabalho: Trabalho;

    beforeEach(() => {
        controller = new TrabalhoController();
        mockResponse = {
            status: jest.fn().mockReturnThis(),
            json: jest.fn().mockReturnThis()
        };
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

    describe('enviarTrabalho', () => {
        beforeEach(() => {
            mockRequest = {
                body: {
                    titulo: 'Trabalho de Teste',
                    aluno: 'João',
                    disciplina: 'Matemática'
                },
                file: {
                    path: 'caminho/do/arquivo.pdf'
                } as Express.Multer.File
            };
        });

        it('deve criar um novo trabalho com sucesso', async () => {
            jest.spyOn(TrabalhoService, 'salvarTrabalho').mockResolvedValue(mockTrabalho);

            await controller.enviarTrabalho(mockRequest as Request, mockResponse as Response);

            expect(mockResponse.status).toHaveBeenCalledWith(201);
            expect(mockResponse.json).toHaveBeenCalledWith(mockTrabalho);
        });

        it('deve retornar erro quando arquivo não for fornecido', async () => {
            mockRequest.file = undefined;

            await controller.enviarTrabalho(mockRequest as Request, mockResponse as Response);

            expect(mockResponse.status).toHaveBeenCalledWith(400);
            expect(mockResponse.json).toHaveBeenCalledWith({ error: 'Arquivo não fornecido' });
        });
    });

    describe('listarTrabalhos', () => {
        it('deve listar todos os trabalhos com sucesso', async () => {
            const mockTrabalhos = [mockTrabalho];
            jest.spyOn(TrabalhoService, 'listarTrabalhos').mockResolvedValue(mockTrabalhos);

            await controller.listarTrabalhos(mockRequest as Request, mockResponse as Response);

            expect(mockResponse.json).toHaveBeenCalledWith(mockTrabalhos);
        });
    });

    describe('avaliarTrabalho', () => {
        beforeEach(() => {
            mockRequest = {
                params: { id: '1' },
                body: {
                    nota: 9.5,
                    comentarios: 'Muito bom!'
                }
            };
        });

        it('deve avaliar um trabalho com sucesso', async () => {
            const trabalhoAvaliado = { ...mockTrabalho, nota: 9.5, comentarios: 'Muito bom!', status: 'corrigido' };
            jest.spyOn(TrabalhoService, 'avaliarTrabalho').mockResolvedValue(trabalhoAvaliado);

            await controller.avaliarTrabalho(mockRequest as Request, mockResponse as Response);

            expect(mockResponse.json).toHaveBeenCalledWith(trabalhoAvaliado);
        });
    });
});
