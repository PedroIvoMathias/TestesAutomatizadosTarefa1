export interface Trabalho {
    id?: string;
    titulo: string;
    aluno: string;
    arquivo: string;
    dataEntrega: Date;
    status: 'pendente' | 'entregue' | 'corrigido';
    nota?: number;
    comentarios?: string;
}
