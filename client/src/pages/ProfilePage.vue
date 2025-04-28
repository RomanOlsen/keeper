<script setup>
import { AppState } from '@/AppState.js';
import { profilesService } from '@/services/ProfilesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const profile = computed(() => AppState.activeProfile)
const keeps = computed(() => AppState.keeps)
const vaults = computed(() => AppState.activeVaults)


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


            <div v-for="vault in vaults" :key="vault.id" class="col-2 pb-3">
              <div class="position-relative">
                <img class="vault-image rounded" :src="vault.img" alt="vault img">
                <div  v-if="vault.isPrivate" class="mdi mdi-lock lock p-1"></div>
                <div  v-else class="mdi mdi-earth lock p-1"></div>

                <div class="title p-1">{{ vault.name }}</div>

              </div>
              
            </div>

          </div>
        </div>
      </div>
      <div class="col-12 text-center">
        <div class="fs-1">Keeps</div>
        <div class="container">
          <div class="row">


            <div v-for="keep in keeps" :key="keep.id" class="col-2 pb-3">
              <div class="position-relative">
                <img class="vault-image rounded" :src="keep.img" alt="keep img">


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
.title{
  position: absolute;
bottom: 0;
color: white;
text-shadow: 1px 1px 0.4rem rgb(0, 0, 0);

}
.lock{
  position: absolute;
bottom: 0;
right: 0;
color: white;
text-shadow: 1px 1px 0.4rem rgb(0, 0, 0);

}
</style>