import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Keep } from "@/models/Keep.js"
import { AppState } from "@/AppState.js"

class KeepsService{
  async getAllKeeps() {
    const response = await api.get('api/keeps')
    logger.log(response.data)
    const keeps = response.data.map(pojo => new Keep(pojo))
    AppState.keeps = keeps
  }
}
export const keepsService = new KeepsService()