import { Vault } from "@/models/Vault.js"
import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { accountService } from "./AccountService.js"
import { Pop } from "@/utils/Pop.js"

class VaultsService{
  async getVaultById(id) {
    const response = await api.get(`api/vaults/${id}`)
    logger.log("Heres the vault!!!", response.data)
    // if (response.data.isPrivate == true && response.data.creatorId != AppState.account.id) {
    //   throw new Error("This vault is private and not yours.")
    // }
    // if (response.data.isPrivate == true) {
    //   try {
        
    //     await accountService.getAccount()
    //     if (response.data.creatorId != AppState.account.id) {
    //       throw new Error("This vault is private and not yours.");
    //     } else{
    //       return;
    //     }
    //   }
    //   catch (error){
    //     Pop.error("Could not check account when vault is private",error);
    //   }
    // }
    const vault = new Vault(response.data)
    AppState.activeVault = vault
  }
  async deleteVault(id) {

      const response = await api.delete(`api/vaults/${id}`)
      logger.log(response.data)
      const index = AppState.vaults.findIndex(vault => vault.id == id)
      AppState.vaults.splice(index, 1)

  }
  async createVault(vaultFormData) {
    const response = await api.post('api/vaults', vaultFormData.value)
    logger.log(response.data)
    const vault = new Vault(response.data)
    AppState.vaults.push(vault)
  }

  
}
export const vaultsService = new VaultsService()