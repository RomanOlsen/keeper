import { AppState } from "@/AppState.js"

class ModalsService{
  setModalMode(boolean) {
    AppState.createModalIsForKeeps = boolean;
  }
}
export const modalsService = new ModalsService()