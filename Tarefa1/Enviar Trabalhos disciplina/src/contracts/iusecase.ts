import { Request, Response } from 'express';

export interface TrabalhoIController {
    criarTrabalho(req: Request, res: Response): Promise<void>;
    findById(req: Request, res: Response): Promise<void>;
    listarTodosOsTrabalhos(req: Request, res: Response): Promise<void>;
    delete(req: Request, res: Response): Promise<void>;
}
