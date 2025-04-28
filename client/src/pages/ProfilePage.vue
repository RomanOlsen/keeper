<script setup>
import { AppState } from '@/AppState.js';
import { keepsService } from '@/services/KeepsService.js';
import { profilesService } from '@/services/ProfilesService.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const profile = computed(() => AppState.activeProfile)
const account = computed(() => AppState.account)
const keeps = computed(() => AppState.keeps)
const vaults = computed(() => AppState.vaults)


const route = useRoute()

onMounted(() => {
  const id = route.params.id
  getAUsersProfileInfo(id)
})

async function getAUsersProfileInfo(id) {
  try {
    await profilesService.getAUsersProfile(id)
    await profilesService.getAUsersKeeps(id)
    await profilesService.getAUsersVaults(id)
  }
  catch (error) {
    Pop.error(error, "Could not get users profile information");
    logger.error("Could not get profile info", error)
  }
}

async function deleteVault(id) {
  try {
    const confirm = await Pop.confirm("Are you sure you want to delete this vault?")
    if (!confirm) {
      return
    }
    await vaultsService.deleteVault(id)
  }
  catch (error) {
    Pop.error(error);
  }
}
async function deleteKeep(id) {
  try {
    const confirm = await Pop.confirm("Are you sure you want to delete this keep?")
    if (!confirm) {
      return
    }
    await keepsService.deleteKeep(id)

  }
  catch (error) {
    Pop.error(error);
  }
}


</script>


<template>
  <div v-if="profile" class="container">
    <div class="row">
      <div class="col-12 d-flex justify-content-center pt-2">
        <div class="position-relative">
          <div class="image">

            <img class="image rounded" :src="profile.coverImg" alt="Cover Image Goes Here">
          </div>
          <div class="">
            <img class="pfp" :src="profile.picture" alt="">
          </div>
        </div>
      </div>
      <div class="col-12 text-center pt-5">
        <h1>{{ profile.name }}</h1>
        <div>
          <span>{{ vaults.length }} Vaults</span> |
          <span>{{ keeps.length }} Keeps</span>
        </div>
      </div>
      <div class="col-12 text-center pt-5">
        <div class="fs-1">Vaults</div>
        <div class="container">
          <div class="row">


            <div v-for="vault in vaults" :key="vault.id" class="col-6 col-md-4 col-lg-2 pb-3">
              <RouterLink :to="{ name: 'Vault Page', params: { id: vault.id } }">
                
                <div class="position-relative">
                  <img class="vault-image rounded" :src="vault.img" alt="vault img">
                  <div v-if="vault.isPrivate" class="mdi mdi-lock lock p-1"></div>
                  <div v-else class="mdi mdi-earth lock p-1"></div>
                  
                  <div class="title p-1">{{ vault.name }}</div>
                  
                </div>
              </RouterLink>
              <button v-if="profile.id == account?.id" @click="deleteVault(vault.id)"
                class="btn btn-danger p-0 mdi mdi-delete">Delete</button>

            </div>

          </div>
        </div>
      </div>
      <div class="col-12 text-center">
        <div class="fs-1">Keeps</div>
        <div class="container">
          <div class="row">


            <div v-for="keep in keeps" :key="keep.id" class="col-6 col-md-4 col-lg-2 pb-3">

              <div class="position-relative">
                <img class="vault-image rounded" :src="keep.img" alt="keep img">
                <button v-if="profile.id == account?.id" @click="deleteKeep(keep.id)"
                  class="btn btn-danger p-0 fs-5 mdi mdi-delete delete"></button>

                <div class="title p-1">{{ keep.name }}</div>

              </div>

            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.image {
  max-width: 100%;
  height: 40dvh;
  aspect-ratio: 2/1;
  object-fit: cover;
}

.pfp {
  // size: 1rem;
  height: 5rem;
  border-radius: 50%;
  // border-color: #468000;
  border-color: white;
  border-style: solid;
  border-width: thick;
  position: absolute;
  bottom: -12%;

  left: 0;
  right: 0;
  margin-inline: auto;
  width: fit-content;

}

.vault-image {
  width: 100%;
  aspect-ratio: 1/1;
  object-fit: cover;
}

.title {
  position: absolute;
  bottom: 0;
  color: white;
  text-shadow: 1px 1px 0.4rem rgb(0, 0, 0);

}

.lock {
  position: absolute;
  bottom: 0;
  right: 0;
  color: white;
  text-shadow: 1px 1px 0.4rem rgb(0, 0, 0);

}

.delete {
  position: absolute;
  top: 0;
  right: 0;
  color: white;
  text-shadow: 1px 1px 0.4rem rgb(0, 0, 0);

}
</style>