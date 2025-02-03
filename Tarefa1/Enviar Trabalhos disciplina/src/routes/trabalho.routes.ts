import { Router } from 'express';
import multer from 'multer';
import { TrabalhoController } from '../controllers/trabalho.controller';

const router = Router();
const trabalhoController = new TrabalhoController();

// Configuração do Multer para upload de arquivos
const storage = multer.diskStorage({
    destination: (req, file, cb) => {
        cb(null, './uploads');
    },
    filename: (req, file, cb) => {
        const uniqueSuffix = Date.now() + '-' + Math.round(Math.random() * 1E9);
        cb(null, uniqueSuffix + '-' + file.originalname);
    }
});

const upload = multer({ storage: storage });

// Rotas
router.post('/', upload.single('arquivo'), trabalhoController.enviarTrabalho);
router.get('/', trabalhoController.listarTrabalhos);
router.put('/:id/avaliar', trabalhoController.avaliarTrabalho);

export { router as trabalhoRoutes };
