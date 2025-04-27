<script setup>
import { Keep } from '@/models/Keep.js';

defineProps({
  keep: { type: Keep, required: true }
})

</script>


<template>
  <div class="card my-2" :style="{ backgroundImage: `url(${keep.img})` }">

    <div class="fs-3 fw-bold text-light custom-font t-shadow">
      {{ keep.name }}
    </div>
    <img :src="keep.creator.picture" alt="img-fluid" :title="keep.creator.name">
      <img :src="keep.img" class="img-fluid hide" alt="Image">
      <!-- Image is now just there to determine size of the whole card -->


  </div> 

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
.hide{
  opacity: 0;
}

.t-shadow{
  text-shadow: 1px 1px 0.4rem rgb(0, 0, 0);
}
</style>