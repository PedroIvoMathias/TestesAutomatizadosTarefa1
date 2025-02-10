import { Trabalho } from '../models/trabalho.model';

// Simulando um banco de dados em memória
let trabalhos: Trabalho[] = [];
let nextId = 1;

export const criarTrabalho = async (trabalho: Omit<Trabalho, 'id'>): Promise<Trabalho> => {
    const novoTrabalho = {
        ...trabalho,
        id: nextId++
    };
    trabalhos.push(novoTrabalho);
    return novoTrabalho;
};

export const listarTrabalhos = async (): Promise<Trabalho[]> => {
    return trabalhos;
};

export const buscarTrabalhoPorId = async (id: number): Promise<Trabalho | undefined> => {
    return trabalhos.find(t => t.id === id);
};

export const deletarTrabalho = async (id: number): Promise<void> => {
    const index = trabalhos.findIndex(t => t.id === id);
    if (index !== -1) {
        trabalhos.splice(index, 1);
    }
};

export const avaliarTrabalho = async (id: number, nota: number, comentarios: string): Promise<Trabalho> => {
    const trabalho = trabalhos.find(t => t.id === id);
    if (!trabalho) {
        throw new Error('Trabalho não encontrado');
    }

    trabalho.nota = nota;
    trabalho.comentarios = comentarios;
    trabalho.status = 'corrigido';

    return trabalho;
};
