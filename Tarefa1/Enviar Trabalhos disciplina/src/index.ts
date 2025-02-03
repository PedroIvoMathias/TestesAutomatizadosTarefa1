import express from 'express';
import { trabalhoRoutes } from './routes/trabalho.routes';

const app = express();
const PORT = process.env.PORT || 3000;

app.use(express.json());

// Rotas
app.use('/api/trabalhos', trabalhoRoutes);

app.listen(PORT, () => {
    console.log(`Servidor rodando na porta ${PORT}`);
});
