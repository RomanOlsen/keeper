<script setup>
import CreateModal from './components/CreateModal.vue';
import Navbar from './components/Navbar.vue';
import { RouterView } from 'vue-router';

</script>

<template>
  <header class="custom-font sticky-top">
    <Navbar />
  </header>
  <main class="custom-font">
    <RouterView />
    <CreateModal />

  </main>
  <footer class=" text-center">

  </footer>
</template>

<style lang="scss"></style>
