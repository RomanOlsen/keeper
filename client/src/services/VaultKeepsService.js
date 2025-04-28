import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepsService{
  async createVaultKeep(vaultId, keepId) {
    const response = await api.post('api/vaultkeeps', { vaultId: vaultId, keepId: keepId})
    logger.log(response.data)
  }
}
export const vaultKeepsService = new VaultKeepsService() //single ton (not singletary!)