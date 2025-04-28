<script setup>
import { AppState } from '@/AppState.js';
import { router } from '@/router.js';
import { vaultKeepsService } from '@/services/VaultKeepsService.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute()
const id = route.params.id
const vault = computed(() => AppState.activeVault)
const keeps = computed(() => AppState.keeps)
const account = computed(() => AppState.account)

onMounted(() => {
  getVaultById(id)
})

async function getVaultById(id) {
  try {
    await vaultsService.getVaultById(id)
    await vaultKeepsService.getKeepsInVault(id)
  }
  catch (error) {
    Pop.error(error, "Could not get vault info. It may be Private.");
    logger.error("Could not get vault info", error)
    router.push({ name: 'Home' })
  }
}
async function deleteKeepFromVault(vaultKeepId) {
  try {
    const confirm = await Pop.confirm("Are you sure you want to remove your keep from this vault?")
    if (!confirm) {
      return
    }
    await vaultKeepsService.deleteVaultKeep(vaultKeepId)
  }
  catch (error) {
    Pop.error(error);
    logger.error("Could not delete vaultKeep", error)


  }
}
</script>


<template>
  <div v-if="vault" class="container pt-2">
    <div class="row">
      <div class="col-12">
        <div class="d-flex justify-content-center">
          <img class="image rounded" :src="vault.img" alt="Cover Image Goes Here">
        </div>
        <div class="text-center fs-1 fw-bold fst-italic">{{ vault.name }}</div>
        <div class="d-flex justify-content-center align-items-center">
          <img class="pfp" :src="vault.creator.picture" alt="Creator's picture">
          <span>By {{ vault.creator.name }}</span>
        </div>
        <div class="d-flex justify-content-center pt-3">

          <span class="fs-5 bg-keeper-light px-2 rounded">{{ keeps.length }} Keeps</span>
        </div>

      </div>
      <div class="col-12 pt-5">

        <div class="container">
          <div class="row">
            <div v-for="keep in keeps" :key="keep.id" class="col-6 col-md-4 col-lg-2 pb-3">


              <div class="position-relative">
                <img class="vault-image rounded" :src="keep.img" alt="vault img">
                <button v-if="keep.creator.id == account?.id" @click="deleteKeepFromVault(keep.vaultKeepId)"
                  class="btn btn-danger p-0 fs-5 mdi mdi-delete delete"></button>
                <img :src="keep.creator.picture" class="keepIcon p-1">
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
.pfp {
  // size: 1rem;
  height: 4rem;
  border-radius: 50%;
  // border-color: #468000;
  border-color: white;
  border-style: solid;
  border-width: thick;

}

.image {
  max-width: 100%;
  height: 40dvh;
  aspect-ratio: 2/1;
  object-fit: cover;
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

.delete {
  position: absolute;
  top: 0;
  right: 0;
  color: white;
  text-shadow: 1px 1px 0.4rem rgb(0, 0, 0);

}

.keepIcon {
  position: absolute;
  bottom: 0;
  right: 0;

  text-shadow: 1px 1px 0.4rem rgb(0, 0, 0);

  height: 3rem;
  border-radius: 50%;
  // border-color: #468000;


}
</style>