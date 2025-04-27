<script setup>
import { Keep } from '@/models/Keep.js';

defineProps({
  keep: { type: Keep, required: true }
})

</script>


<template>
  <div class="keep" :style="{ backgroundImage: `url(${keep.img})` }">
    <div class="card-header">{{ keep.name }}</div>
    <!-- <div class="card-body"> -->
      <!-- <img :src="keep.img" class="img-fluid"> -->
    <!-- </div> -->
    <!-- <div class="card-footer"></div> -->
  </div>

</template>


<style lang="scss" scoped>
.keep {
  border: none;
  background-size: cover;
  // background-repeat: no-repeat;
  // background:local;
  background-position: center;
  width: 100%;
  min-height: 10dvh;
  margin: 1rem;
  // display: inline;
  // flex-grow: 1;
  // size:0cap;
  // flex-grow: 1;
  // min-height: 30dvh;

  // aspect-ratio: 1/1;
}
</style>