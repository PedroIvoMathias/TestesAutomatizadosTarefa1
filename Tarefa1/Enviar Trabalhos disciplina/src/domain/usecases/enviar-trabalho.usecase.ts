import { IUseCase } from "../../contracts/iusecase";
import { Trabalho } from "../entities/trabalho";

export interface EnviarTrabalhoInput {
    titulo: string;
    aluno: string;
    arquivo: Express.Multer.File;
}

export interface EnviarTrabalhoOutput {
    trabalho: Trabalho;
}

export class EnviarTrabalhoUseCase implements IUseCase<EnviarTrabalhoInput, EnviarTrabalhoOutput> {
    async execute(input: EnviarTrabalhoInput): Promise<EnviarTrabalhoOutput> {
        const trabalho: Trabalho = {
            titulo: input.titulo,
            aluno: input.aluno,
            arquivo: input.arquivo.filename,
            dataEntrega: new Date(),
            status: 'entregue'
        };

        // Aqui você implementaria a lógica para salvar no banco de dados
        
        return { trabalho };
    }
}
