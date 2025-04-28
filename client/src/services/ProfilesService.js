import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Account } from "@/models/Account.js"

class ProfilesService{
  async getAUsersProfile(id) {
    const response = await api.get(`api/profiles/${id}`)
    logger.log(response.data)
    const profile = new Account(response.data)
    AppState.activeProfile = profile
  }


}
export const profilesService = new ProfilesService()