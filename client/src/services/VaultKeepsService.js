import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Keep, VaultKeep } from "@/models/Keep.js"

class VaultKeepsService{
  async deleteVaultKeep(vaultKeepId) {
    const response = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
    logger.log(response.data)
    const index = AppState.keeps.findIndex(keep => keep.vaultKeepId == vaultKeepId)
    AppState.keeps.splice(index, 1)
  }
  async createVaultKeep(vaultId, keepId) {
    const response = await api.post('api/vaultkeeps', { vaultId: vaultId, keepId: keepId})
    logger.log(response.data)
  }
  async getKeepsInVault(id){
    const response = await api.get(`api/vaults/${id}/keeps`)
    logger.log(response.data)
    const keeps = response.data.map(pojo => new VaultKeep(pojo))
    AppState.keeps = keeps
  }
}
export const vaultKeepsService = new VaultKeepsService() //single ton (not singletary!)