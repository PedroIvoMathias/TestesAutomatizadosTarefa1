import { Request, Response } from 'express';
import { Trabalho } from '../models/trabalho.model';
import * as TrabalhoService from '../services/trabalho.service';

export class TrabalhoController {
    async enviarTrabalho(req: Request, res: Response) {
        try {
            const trabalho: Trabalho = req.body;
            const arquivo = req.file;

            if (!arquivo) {
                return res.status(400).json({ error: 'Arquivo não fornecido' });
            }

            trabalho.arquivo = arquivo.path;
            trabalho.status = 'entregue';
            trabalho.dataEntrega = new Date();

            const novoTrabalho = await TrabalhoService.salvarTrabalho(trabalho);
            res.status(201).json(novoTrabalho);
        } catch (error) {
            res.status(500).json({ error: 'Erro ao enviar trabalho' });
        }
    }

    async listarTrabalhos(req: Request, res: Response) {
        try {
            const trabalhos = await TrabalhoService.listarTrabalhos();
            res.json(trabalhos);
        } catch (error) {
            res.status(500).json({ error: 'Erro ao listar trabalhos' });
        }
    }

    async avaliarTrabalho(req: Request, res: Response) {
        try {
            const { id } = req.params;
            const { nota, comentarios } = req.body;

            const trabalho = await TrabalhoService.avaliarTrabalho(id, nota, comentarios);
            res.json(trabalho);
        } catch (error) {
            res.status(500).json({ error: 'Erro ao avaliar trabalho' });
        }
    }
}
