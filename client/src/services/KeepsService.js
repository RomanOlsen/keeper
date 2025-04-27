import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Keep } from "@/models/Keep.js"
import { AppState } from "@/AppState.js"
import { accountService } from "./AccountService.js"

class KeepsService{

  async getKeepById(keepId) {
    const response = await api.get(`api/keeps/${keepId}`)
    logger.log(response.data)
    const keep = new Keep(response.data)
    AppState.activeKeep = keep

    if (AppState.account) { // for opening keep details mode so far
      accountService.getMyVaults();
    }
  }
  async getAllKeeps() {
    const response = await api.get('api/keeps')
    logger.log(response.data)
    const keeps = response.data.map(pojo => new Keep(pojo))
    AppState.keeps = keeps
  }
}
export const keepsService = new KeepsService()