<script setup>
import { AppState } from '@/AppState.js';
import { keepsService } from '@/services/KeepsService.js';
import { vaultsService } from '@/services/VaultsService.js';
import { Pop } from '@/utils/Pop.js';
import { computed, ref } from 'vue';


const isKeeps = computed(() => AppState.createModalIsForKeeps)

const keepFormData = ref({
  name: '',
  img: '',
  description: ''
})

const vaultFormData = ref({
  name: '',
  img: '',
  description: '',
  isPrivate: false
})

async function createKeep() {
  try {
    await keepsService.createKeep(keepFormData)
    Pop.success("Keep has been created!")
  }
  catch (error) {
    Pop.error(error);
  }
}

async function createVault() {
  try {
    await vaultsService.createVault(vaultFormData)
    Pop.success("Vault has been created!")

  }
  catch (error) {
    Pop.error(error);
  }
}
</script>


<template>
  <!-- Modal -->
  <div class="modal fade" id="CreateModal" tabindex="-1" aria-labelledby="createModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-lg">
      <div v-if="isKeeps" class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="createModalLabel">Create a Keep</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <form @submit.prevent="createKeep()">


          <div class="modal-body">

            <div class="text-center">
              <div for="">Name</div>
              <input v-model="keepFormData.name" class="" type="text" required>

            </div>
            <div class="text-center py-4">
              <div for="">Image</div>
              <input v-model="keepFormData.img" class="" type="url" required>

            </div>
            <div class="text-center">
              <div>Description</div>
              <input v-model="keepFormData.description" class="" type="text">

            </div>

          </div>
          <div class="modal-footer">
            <button class="btn btn-keeper text-light" type="submit" data-bs-dismiss="modal">Create</button>
          </div>
        </form>
      </div>
      <div v-else class="modal-content">
        <!-- ANCHOR for VAULTS -->
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="createModalLabel">Create a Vault</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <form @submit.prevent="createVault()">


          <div class="modal-body">

            <div class="text-center">
              <div for="">Name</div>
              <input v-model="vaultFormData.name" class="" type="text" required>

            </div>
            <div class="text-center py-4">
              <div for="">Image</div>
              <input v-model="vaultFormData.img" class="" type="url" required>

            </div>
            <div class="text-center">
              <div>Description</div>
              <input v-model="vaultFormData.description" class="" type="text">

            </div>

          </div>
          <div class="modal-footer justify-content-between">
            <div>
              <span class="fs-5 pe-2">Mark Private?</span>
              <input v-model="vaultFormData.isPrivate" class="checkbox" type="checkbox">  
              <div>Private vaults can only be seen by you.</div>
            </div>
            <button class="btn btn-keeper text-light" type="submit" data-bs-dismiss="modal">Create</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
input {
  width: 75%;
}
.checkbox{
  width: unset;
}
</style>