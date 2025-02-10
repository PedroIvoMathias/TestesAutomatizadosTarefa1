import { Request, Response } from 'express';
import { Trabalho } from '../models/trabalho.model';
import * as TrabalhoService from '../services/trabalho.service';
import { TrabalhoIController } from '../contracts/iusecase';

export class TrabalhoController implements TrabalhoIController {
    public async criarTrabalho(req: Request, res: Response): Promise<void> {
        try {
            const { titulo, aluno, disciplina } = req.body;
            
            if (!titulo || !aluno || !disciplina) {
                return res.status(400).json({ 
                    error: 'Dados incompletos. Título, aluno e disciplina são obrigatórios.' 
                });
            }

            const trabalho: Omit<Trabalho, 'id'> = {
                titulo,
                aluno,
                disciplina,
                dataEntrega: new Date(),
                arquivo: req.file?.path || '',
                status: 'pendente'
            };

            const novoTrabalho = await TrabalhoService.criarTrabalho(trabalho);
            res.status(201).json(novoTrabalho);
        } catch (error) {
            res.status(500).json({ error: 'Erro ao criar trabalho' });
        }
    }

    public async findById(req: Request, res: Response): Promise<void> {
        try {
            const id = Number(req.params.id);
            
            if (isNaN(id)) {
                return res.status(400).json({ error: 'ID inválido' });
            }

            const trabalho = await TrabalhoService.buscarTrabalhoPorId(id);
            
            if (!trabalho) {
                return res.status(404).json({ error: 'Trabalho não encontrado' });
            }

            res.json(trabalho);
        } catch (error) {
            res.status(500).json({ error: 'Erro ao buscar trabalho' });
        }
    }

    public async listarTodosOsTrabalhos(req: Request, res: Response): Promise<void> {
        try {
            const trabalhos = await TrabalhoService.listarTrabalhos();
            res.json(trabalhos);
        } catch (error) {
            res.status(500).json({ error: 'Erro ao listar trabalhos' });
        }
    }

    public async delete(req: Request, res: Response): Promise<void> {
        try {
            const id = Number(req.params.id);

            if (isNaN(id)) {
                return res.status(400).json({ error: 'ID inválido' });
            }
            
            const trabalhoExistente = await TrabalhoService.buscarTrabalhoPorId(id);
            if (!trabalhoExistente) {
                return res.status(404).json({ error: 'Trabalho não encontrado' });
            }

            await TrabalhoService.deletarTrabalho(id);
            res.status(204).send();
        } catch (error) {
            res.status(500).json({ error: 'Erro ao deletar trabalho' });
        }
    }
}
