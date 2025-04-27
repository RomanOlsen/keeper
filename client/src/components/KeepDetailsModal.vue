<script setup>
import { AppState } from '@/AppState.js';
import { Pop } from '@/utils/Pop.js';
import { computed, ref } from 'vue';

const keep = computed(() => AppState.activeKeep)
const account = computed(() => AppState.account)
const vaults = computed(()=> AppState.activeVaults)

const formData = ref({
  body: '',
  imgUrl: '',
})

async function createVaultKeep() {
try {
  await 
}
catch (error){
  Pop.error(error);
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
              <img :src="keep.img" alt="" class="img-fluid image rounded-start">
              <!-- NOTE rounded-start looks good on PC but bad mobile. -->
            </div>
            <div class="col-12 col-md-6">

              <button type="button" class="btn-close close-button mt-2" data-bs-dismiss="modal"
                aria-label="Close"></button>

              <div class="d-flex justify-content-center align-items-center fs-4 m-3">
                <div>
                  <span class="mdi mdi-eye"> {{ keep.views }}</span>
                </div>
                <div v-if="keep.kept != 0">
                  <span class="mdi mdi-safe-square"> {{ keep.kept }}</span>
                </div>
              </div>
              <div class="text-center">
                <div class="fs-1 fw-bold"> {{ keep.name }} </div>
                <p>{{ keep.description }}</p>
              </div>

              <div class="d-flex align-items-center flex-grow-1 justify-content-between">
                <div v-if="account">
                  <form @submit.prevent="createVaultKeep()">
                    <select name="" id="">
                      <option v-for="vault in vaults" :key="vault.id" value="">{{ vault.name}}</option>
                    </select>
                    <button type="submit" class="btn btn-keeper text-light">Save to Vault</button>
                  </form>
                </div>
                <div v-else>
                  <!-- giving it space for justify-content-between to work how we want -->
                </div>

                <div>
                  <img class="image-fluid pfp" :src="keep.creator.picture" :alt="keep.creator.name + `'s picture`"
                    :title="keep.creator.name + `'s picture`">
                  <span>{{ keep.creator.name }}</span>
                </div>
              </div>
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