import express from 'express';
import { trabalhoRoutes } from './routes/trabalho.routes';

const app = express();

app.use(express.json());
app.use('/api/trabalhos', trabalhoRoutes);

app.listen(3000, () => {
    console.log('Server is running on port 3000');
});
