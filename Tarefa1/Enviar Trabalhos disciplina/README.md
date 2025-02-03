# Sistema de Entrega de Trabalhos

Este é um sistema para gerenciamento de entregas de trabalhos acadêmicos, onde alunos podem enviar seus trabalhos e professores podem avaliar.

## Funcionalidades

- Envio de trabalhos com arquivo anexado
- Listagem de todos os trabalhos
- Avaliação de trabalhos com nota e comentários
- Status de trabalho (pendente, entregue, corrigido)

## Tecnologias Utilizadas

- Node.js
- TypeScript
- Express
- Multer (para upload de arquivos)

## Como Executar

1. Instale o Node.js: https://nodejs.org/

2. Instale as dependências:
```bash
npm install
```

3. Inicie o servidor em modo desenvolvimento:
```bash
npm run dev
```

O servidor estará rodando em http://localhost:3000

## Endpoints da API

- POST /api/trabalhos - Enviar um novo trabalho
- GET /api/trabalhos - Listar todos os trabalhos
- PUT /api/trabalhos/:id/avaliar - Avaliar um trabalho

## Estrutura do Projeto

```
src/
  ├── controllers/    # Controladores da aplicação
  ├── models/        # Interfaces e tipos
  ├── routes/        # Rotas da API
  ├── services/      # Lógica de negócio
  └── index.ts       # Ponto de entrada da aplicação
```