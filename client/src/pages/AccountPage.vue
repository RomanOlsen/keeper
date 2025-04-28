<script setup>
import { computed, onMounted, ref } from 'vue';
import { AppState } from '../AppState.js';
import { Pop } from '@/utils/Pop.js';
import { logger } from '@/utils/Logger.js';
import { accountService } from '@/services/AccountService.js';
import { profilesService } from '@/services/ProfilesService.js';

const account = computed(() => AppState.account)
const keeps = computed(() => AppState.keeps)
const vaults = computed(() => AppState.vaults)

onMounted(() => {
  getInfoToCalculateLength()
})

const accountFormData = ref({
  name: '',
  picture: '',
  coverImg: ''
})
async function editMyAccount() {
  try {
    await accountService.editMyAccount(accountFormData.value);
  }
  catch (error) {
    Pop.error(error);
    logger.log("could not update account", error)
  }

}

async function getInfoToCalculateLength() {
  try {
    await profilesService.getAUsersKeeps(account.value.id)
    await accountService.getMyVaults()
  }
  catch (error) {
    Pop.error(error);
  }
}
</script>

<template>
  <div class="about text-center">
    <div v-if="account">
      <div class="container text-light t-shadow account-view rounded mt-3"
        :style="{ backgroundImage: `url(${account.coverImg})` }">

        <h1>Welcome {{ account.name }}</h1>
        <img class="rounded" :src="account.picture" alt="" />
        <p>{{ account.email }}</p>
        <span>{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</span>
        
      </div>
      <div class="container">
        <div class="row justify-content-center">
          <div class="col-12 col-md-6 col-xl-4">
            <div class="card">
              <div class="card-header">Edit Account Details</div>
              <ul class="list-group list-group-flush">
                <form @submit.prevent="editMyAccount()">
                  <li class="list-group-item">
                    <div for="name">Name </div>
                    <input v-model="accountFormData.name" name="name" type="text" maxlength="255"
                      placeholder="Keep same name">
                  </li>
                  <li class="list-group-item">
                    <div for="picture"> Picture </div>
                    <input v-model="accountFormData.picture" name="picture" type="url" value="" maxlength="255"
                      placeholder="Keep same picture">
                  </li>
                  <li class="list-group-item">
                    <div for="coverImg"> Cover Photo </div>
                    <input v-model="accountFormData.coverImg" name="coverImg" type="url" maxlength="1000"
                      placeholder="Keep same cover img">
                  </li>
                  <li class="list-group-item">
                    <button type="submit" class="btn btn-keeper-light">Save</button>
                  </li>

                </form>
              </ul>


            </div>
           
          </div>
        </div>
      </div>
    </div>
    <div v-else>
      <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
    </div>
  </div>
</template>

<style scoped lang="scss">
img {
  max-width: 100px;
}

.t-shadow {
  text-shadow: 1px 1px 0.4rem rgb(0, 0, 0);
}

.account-view {
  background-size: cover;
  background-position: center;
  background-color: #2e4d08;
}
</style>
