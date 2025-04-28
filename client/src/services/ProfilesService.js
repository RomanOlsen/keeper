import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Account } from "@/models/Account.js"
import { Keep } from "@/models/Keep.js"
import { Vault } from "@/models/Vault.js"

class ProfilesService{
  async getAUsersProfile(id) {
    const response = await api.get(`api/profiles/${id}`)
    logger.log(response.data)
    const profile = new Account(response.data)
    AppState.activeProfile = profile
  }
  async getAUsersKeeps(id) {
    const response = await api.get(`api/profiles/${id}/keeps`)
    logger.log(response.data)
    const keeps = response.data.map(pojo => new Keep(pojo))
    AppState.keeps = keeps
  }
  async getAUsersVaults(id) {
    const response = await api.get(`api/profiles/${id}/vaults`)
    logger.log(response.data)
    const vaults = response.data.map(pojo => new Vault(pojo))
    AppState.vaults = vaults
  }


}
export const profilesService = new ProfilesService()