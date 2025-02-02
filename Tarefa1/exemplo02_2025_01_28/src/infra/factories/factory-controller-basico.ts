import { ControllerBasico } from "../../controllers/controller-basico";
import { UseCaseBasico } from "../../domain/usecases/usecase-basico";

export function factoryControllerBasico() {
    const uc = new UseCaseBasico();
    const contr = new ControllerBasico(uc);
    return contr;
}