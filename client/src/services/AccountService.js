import { Vault } from '@/models/Vault.js'
import { AppState } from '../AppState.js'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger.js'
import { api } from './AxiosService.js'

class AccountService {
  async editMyAccount(value) {
    // value.name ?? AppState.account.name
let name
let picture
let coverImg
    if (value.name == '') {
      name = AppState.account.name
    }
    else{
      name = value.name
    }
    if (value.picture == '') {
      picture = AppState.account.picture
    } else{
      picture = value.picture
    }
    if (value.coverImg == '') {
      coverImg = AppState.account.coverImg
    } else{
      coverImg = value.coverImg
    }
    const response = await api.put('/account', {name: name, picture: picture, coverImg: coverImg})
    AppState.account = new Account(response.data)
  }
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async getMyVaults() {
    const response = await api.get('/account/vaults')
    logger.log(response.data);
    const vaults = response.data.map(pojo => new Vault(pojo))
    AppState.vaults = vaults
  }
}

export const accountService = new AccountService()
