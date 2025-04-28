import { Vault } from "@/models/Vault.js"
import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"

class VaultsService{
  async createVault(vaultFormData) {
    const response = await api.post('api/vaults', vaultFormData.value)
    logger.log(response.data)
    const vault = new Vault(response.data)
    AppState.vaults.push(vault)
  }

  
}
export const vaultsService = new VaultsService()