import { Router } from "express";
import { factoryControllerBasico } from "../factories/factory-controller-basico";

const routerBasico = Router();

const objeto = factoryControllerBasico();

routerBasico.get('/vivo', (req, res) => {
  res.status(200).json({ message: 'Estou vivo!' });
});

routerBasico.get('/basico/:valor', (req, res) => {
  objeto.handle(req, res);
});

export { routerBasico };