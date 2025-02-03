export interface Trabalho {
    id: string;
    titulo: string;
    aluno: string;
    disciplina: string;
    dataEntrega: Date;
    arquivo: string;  // caminho do arquivo
    status: 'pendente' | 'entregue' | 'corrigido';
    nota?: number;
    comentarios?: string;
}
