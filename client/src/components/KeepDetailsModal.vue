<script setup>
import { AppState } from '@/AppState.js';
import { computed } from 'vue';

const keep = computed(()=> AppState.activeKeep)
</script>


<template>
<!-- Modal -->
<div class="modal fade" id="KeepDetailsModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div v-if="keep" class="modal-dialog modal-dialog-centered modal-xl">
    <div class="modal-content">
      <!-- <div class="modal-header">
        <h1 class="modal-title fs-5" id="exampleModalLabel">{{ keep.name }}</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div> -->
      <!-- <div class="modal-body"> -->
        
        <!-- SECTION lots of this is copied from allspice -->
        <!-- <div class="container-fluid"> -->
          <div class="row">
          <div class="col-12 col-md-6">
            <img :src="keep.img" alt="" class="img-fluid image rounded-start">
          </div>
          <div class="col-12 col-md-6">
            <div class="d-flex justify-content-between align-items-center">
              <h1 class="mt-2">{{ keep.name }}</h1>
              <button type="button" class="btn-close pe-4 mb-3" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="d-flex justify-content-between">
              <h2 class="fs-5">By {{ keep.creator.name }}</h2>
              <!-- <div v-if="keep.creatorId == account.id" class="me-2">

                <button @click="deleteRecipe()" class="btn btn-outline-danger" data-bs-dismiss="modal">Delete Recipe</button>
                <button @click="editRecipe()" class="btn btn-outline-primary mx-1">Edit Recipe</button>
              </div> -->
            </div>
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
        <h1 class="modal-title fs-5" id="exampleModalLabel">Loading... <span class="mdi mdi-loading mdi-spin"></span></h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>

    </div>
  </div>
</div>
</template>


<style lang="scss" scoped>

</style>