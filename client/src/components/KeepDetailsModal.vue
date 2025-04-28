<script setup>
import { AppState } from '@/AppState.js';
import { vaultKeepsService } from '@/services/VaultKeepsService.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, ref } from 'vue';

const keep = computed(() => AppState.activeKeep)
const account = computed(() => AppState.account)
const vaults = computed(() => AppState.activeVaults)

const formData = ref({
  vaultId: ''
})

async function createVaultKeep() {
  try {
    // const vaultKeepData = event.target
    await vaultKeepsService.createVaultKeep(formData.value.vaultId, keep.value.id);
    Pop.success("Keep added to vault!")
  }
  catch (error) {
    Pop.error(error);
    logger.log("couldnt create a VK", error)
  }
}

</script>


<template>
  <!-- Modal -->
  <div class="modal fade" id="KeepDetailsModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div v-if="keep" class="modal-dialog modal-dialog-centered modal-xl">
      <div class="modal-content bg-offwhite">
        <div class="container-fluid">
          <div class="row">
            <div class="col-12 col-md-6 p-0">
              <img :src="keep.img" :alt="'Image of ' + keep.name + ' would be here but it is missing!'"
                class="img-fluid image rounded-start">
              <!-- NOTE rounded-start looks good on PC but bad mobile. -->
            </div>
            <div class="col-12 col-md-6 d-flex flex-column">
              <button type="button" class="btn-close close-button mt-2" data-bs-dismiss="modal"
                aria-label="Close"></button>
              <!-- <div> -->
              <div class="d-flex justify-content-center align-items-center fs-4 m-3">
                <div>
                  <span class="mdi mdi-eye"> {{ keep.views }}</span>
                </div>
                <div v-if="keep.kept != 0">
                  <span class="mdi mdi-safe-square"> {{ keep.kept }}</span>
                </div>
              </div>
              <div class="text-center d-flex flex-grow-1 flex-column justify-content-center">
                <div class="fs-1 fw-bold"> {{ keep.name }} </div>
                <p>{{ keep.description }}</p>
              </div>

              <div class="d-flex align-items-center justify-content-between mb-2">
                <div v-if="account">
                  <form @submit.prevent="createVaultKeep()">
                    <select v-model="formData.vaultId" title="Choose a vault to save this to" required>
                      <option v-for="vault in vaults" :key="vault.id" :value="vault.id">{{ vault.name }}</option>
                    </select>
                    <button data-bs-dismiss="modal" type="submit" class="btn btn-keeper text-light mx-2">Save to
                      Vault</button>
                  </form>
                </div>
                <div v-else>
                  <!-- giving it space for justify-content-between to work how we want -->
                </div>

                <div class="d-flex gap-2 align-items-center">
                  <RouterLink :to="{ name: 'Profile Page', params: { id: keep.creatorId } }">
                    <img class="image-fluid pfp" :src="keep.creator.picture" :alt="keep.creator.name + `'s picture`"
                      :title="keep.creator.name + `'s picture`" data-bs-dismiss="modal">
                    <span data-bs-dismiss="modal">{{ keep.creator.name }}</span>
                  </RouterLink>
                </div>
              </div>
              <!-- </div> -->


              <!-- <div v-if="keep.creatorId == account.id" class="me-2">

                <button @click="deleteRecipe()" class="btn btn-outline-danger" data-bs-dismiss="modal">Delete Recipe</button>
                <button @click="editRecipe()" class="btn btn-outline-primary mx-1">Edit Recipe</button>
              </div> -->

              <!-- <span class="recipe-category-bg rounded-pill px-3 text-light">{{ recipe.category }}</span>
            <hr class="me-4">
            <h3 class="my-2 fs-3">Ingredients</h3> -->

              <!-- <p v-for="ingredient in ingredients" :key="ingredient.id"> {{ ingredient.quantity }} {{ ingredient.name }}
            </p>

            <h4 class="my-2 fs-3">Instructions</h4>
            <p>{{ recipe.instructions }}</p> -->

              <!-- <hr class="p-0 m-0"> -->
            </div>
          </div>
        </div>
        <!-- !SECTION -->


        <!-- </div> -->
        <!-- <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div> -->
      </div>
    </div>
    <div v-else class="modal-dialog modal-dialog-centered modal-xl">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="exampleModalLabel">Loading... <span class="mdi mdi-loading mdi-spin"></span>
          </h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>

      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.image {
  // border-radius: 50%;
  width: 100%;
  max-height: 70dvh; // 80-90dvh ish. keep it low for mobile users.
  object-fit: cover;
}

.close-button {
  position: absolute;
  top: 0;
  right: 1%;

}

.pfp {
  height: 3rem;
  border-radius: 50%;
  aspect-ratio: 1/1;

}

// .modal-dialog{
//   max-height: 70dvh;
// }</style>