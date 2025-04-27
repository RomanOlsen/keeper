<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import KeepDetailsModal from '@/components/KeepDetailsModal.vue';
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
          <div class="masonry-cols mt-3">
            <div v-for="keep in keeps" :key="keep.id">
              <KeepCard :keep="keep" />
            </div>

          </div>
          <!-- col-12 col-sm-12 col-md-6 col-lg-4 col-xl-4 -->
        <!-- </div> -->
      <!-- </div> -->
    </div>
  </div>
  <KeepDetailsModal/>
</template>

<style scoped lang="scss">
.masonry-roman {
  // display: flex;
  // flex-flow: column-reverse;
  // align-items: flex-end;
  justify-content: center;
}
// .grid {
//    display: grid;
//    grid-gap: 10px;
//    grid-template-columns: repeat(auto-fill, minmax(250px,1fr));
//    grid-auto-rows: 200px;
// }

.masonry-cols{
  // column-count: 2;
  columns: 220px;
}
</style>
