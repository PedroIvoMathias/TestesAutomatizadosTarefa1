import { Trabalho } from '../models/trabalho.model';

// Simulando um banco de dados em memória
let trabalhos: Trabalho[] = [];

export const salvarTrabalho = async (trabalho: Trabalho): Promise<Trabalho> => {
    trabalho.id = Date.now().toString();
    trabalhos.push(trabalho);
    return trabalho;
};

export const listarTrabalhos = async (): Promise<Trabalho[]> => {
    return trabalhos;
};

export const avaliarTrabalho = async (id: string, nota: number, comentarios: string): Promise<Trabalho> => {
    const trabalho = trabalhos.find(t => t.id === id);
    if (!trabalho) {
        throw new Error('Trabalho não encontrado');
    }

    trabalho.nota = nota;
    trabalho.comentarios = comentarios;
    trabalho.status = 'corrigido';

    return trabalho;
};
