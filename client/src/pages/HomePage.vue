<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import { keepsService } from '@/services/KeepsService.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';

onMounted(() => {
  getAllKeeps()
})

const keeps = computed(() => AppState.keeps);

async function getAllKeeps() {
  try {
    await keepsService.getAllKeeps()
  }
  catch (error) {
    Pop.error(error);
  }
}

</script>

<template>
  <div class="container">
    <div class="row">
      <!-- <div class="col-6">

        <div class="masonry-roman"> -->
          <!-- col-12 col-sm-12 col-md-6 col-lg-4 col-xl-4 -->
          <div v-for="keep in keeps" :key="keep.id" class="col-12 col-sm-12 col-md-6 col-lg-4 col-xl-4">
            <KeepCard :keep="keep" />
          </div>
        <!-- </div> -->
      <!-- </div> -->
    </div>
  </div>
</template>

<style scoped lang="scss">
// .masonry-roman {
//   display: flex;
//   flex-flow: row wrap;
//   // align-items: flex-end;
//   justify-content: center;
// }
</style>
