<script setup>
import { Keep } from '@/models/Keep.js';
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { ref } from 'vue';

const props = defineProps({
  keep: { type: Keep, required: true }
})

const showErrorImg = ref(false)

async function getKeepById(keepId) {
  try {
    await keepsService.getKeepById(keepId);
  }
  catch (error) {
    Pop.error(error, "could not get keep by id");
  }
}

function swapSource() {
  logger.warn('Error loading image', props.keep.img)
  showErrorImg.value = true
}

</script>


<template>
  <!-- <div class="position-relative"> -->
  <div @click="getKeepById(keep.id)" class="card my-2" :style="{ backgroundImage: `url(${keep.img})` }"
    data-bs-toggle="modal" data-bs-target="#KeepDetailsModal" role="button">


    <div class="keep-title fs-5 fw-bold text-light custom-font t-shadow mb-1">
      {{ keep.name }}
    </div>
    <img v-if="!showErrorImg" @error="swapSource" class="keep-creator-img mb-2 me-2" :src="keep.creator.picture" alt="" :title="keep.creator.name">
    <img v-else class="keep-creator-img mb-2 me-2" src="https://images.unsplash.com/photo-1466477234737-8a3b3faed8c3?w=900&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fHBvbGFyJTIwYmVhcnxlbnwwfHwwfHx8MA%3D%3D" alt="" :title="keep.creator.name">

    <img v-if="!showErrorImg" @error="swapSource" :src="keep.img" class="img-fluid" alt="Image" height="500"
      width="500">
    <img v-else
      src="https://images.unsplash.com/photo-1466477234737-8a3b3faed8c3?w=900&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fHBvbGFyJTIwYmVhcnxlbnwwfHwwfHx8MA%3D%3D"
      class="img-fluid" alt="">
    <!-- Image is now just there to determine size of the whole card -->


  </div>

  <!-- </div> -->

</template>


<style lang="scss" scoped>
.card {
  border: none;
  background-size: cover;
  display: inline-block;
  // background-repeat: no-repeat;
  // background:local;
  background-position: center;
  width: 100%;
  min-height: 10dvh;
  // display: inline;
  // flex-grow: 1;
  // size:0cap;
  // flex-grow: 1;
  // min-height: 30dvh;

  // aspect-ratio: 1/1;
}

.hide {
  opacity: 0;
}

.t-shadow {
  text-shadow: 1px 1px 0.4rem rgb(0, 0, 0);
}

.keep-title {
  position: absolute;
  bottom: 0;
  padding-left: 0.5rem;
  width: 8ch; // NOTE ch is CHARACTERS
  word-break: normal;
  z-index: 2;


}

.keep-creator-img {
  position: absolute;
  bottom: 0;
  right: 0%;
  z-index: 1;
  height: 2.5rem;
  aspect-ratio: 1/1;
  border-radius: 50%;
  object-fit: cover;
}
</style>