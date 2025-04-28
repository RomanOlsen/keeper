import { Vault } from "@/models/Vault.js"
import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"

class VaultsService{
  async deleteVault(id) {

      const response = await api.delete(`api/vaults/${id}`)
      logger.log(response.data)
      const index = AppState.activeVaults.findIndex(vault => vault.id == id)
      AppState.activeVaults.splice(index, 1)

  }
  async createVault(vaultFormData) {
    const response = await api.post('api/vaults', vaultFormData.value)
    logger.log(response.data)
    const vault = new Vault(response.data)
    AppState.vaults.push(vault)
  }

  
}
export const vaultsService = new VaultsService()