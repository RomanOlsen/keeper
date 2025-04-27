<script setup>
import { Keep } from '@/models/Keep.js';
import { keepsService } from '@/services/KeepsService.js';
import { Pop } from '@/utils/Pop.js';

defineProps({
  keep: { type: Keep, required: true }
})

async function getKeepById(keepId) {
try {
  await keepsService.getKeepById(keepId);
}
catch (error){
  Pop.error(error, "could not get keep by id");
}
}

</script>


<template>
  <!-- <div class="position-relative"> -->
  <div @click="getKeepById(keep.id)" class="card my-2" :style="{ backgroundImage: `url(${keep.img})` }"
    data-bs-toggle="modal" data-bs-target="#KeepDetailsModal" role="button">

    <div class="keep-title fs-3 fw-bold text-light custom-font t-shadow mb-1">
      {{ keep.name }}
    </div>
    <img class="keep-creator-img mb-2 me-2" :src="keep.creator.picture" alt="" :title="keep.creator.name">
    <img :src="keep.img" class="img-fluid hide" alt="Image">
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
}

.keep-creator-img {
  position: absolute;
  bottom: 0;
  right: 0%;
  z-index: 1;
  height: 2.5rem;
  aspect-ratio: 1/1;
  border-radius: 50%;
}
</style>