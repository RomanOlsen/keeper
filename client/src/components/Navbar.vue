<script setup>
import { computed, ref, watch } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import { AppState } from '@/AppState.js';
import { modalsService } from '@/services/ModalsService.js';

const theme = ref(loadState('theme') || 'light')
const account = computed(()=> AppState.account)

// function toggleTheme() {
//   theme.value = theme.value == 'light' ? 'dark' : 'light'
// }

watch(theme, () => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}, { immediate: true })

async function setModalMode(boolean) {
  modalsService.setModalMode(boolean)
}



</script>

<template>
  <nav class="navbar navbar-expand-md bg-keeper border-bottom border-keeper-dark shadow">
    <div class="container gap-2">
      <RouterLink :to="{ name: 'Home' }" class="d-flex align-items-center text-light">
        <img class="navbar-brand" alt="logo" src="@/assets/img/keeper-logo.png" height="45" />
        <b class="fs-5">Keeper</b>
      </RouterLink>
      <!-- collapse button -->
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbar-links"
        aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
        <span class="mdi mdi-menu text-light"></span>
      </button>
      <!-- collapsing menu -->
      <div class="collapse navbar-collapse " id="navbar-links">
        <ul class="navbar-nav d-flex gap-2">
          <li>
            <RouterLink :to="{ name: 'About' }" class="btn text-light selectable">
              About
            </RouterLink>
          </li>
          <li v-if="account">
            <!-- <button class="btn text-light selectable router-link-exact-active">
              Create
            </button> -->
            <div class="dropdown">
              <button class="btn text-light router-link-exact-active dropdown-toggle" type="button"
                data-bs-toggle="dropdown" aria-expanded="false"> Create
              </button>
              <ul class="dropdown-menu">
                <li @click="setModalMode(true)" data-bs-toggle="modal" data-bs-target="#CreateModal" type="button"><a
                    class="dropdown-item">Keep</a></li>
                <li @click="setModalMode(false)" data-bs-toggle="modal" data-bs-target="#CreateModal" type="button"><a
                    class="dropdown-item">Vault</a></li>
              </ul>
            </div>
          </li>
        </ul>
        <!-- LOGIN COMPONENT HERE -->
        <div class="ms-auto">
          <!-- <button class="btn text-light" @click="toggleTheme"
            :title="`Enable ${theme == 'light' ? 'dark' : 'light'} theme.`">
            <i v-if="theme == 'dark'" class="mdi mdi-weather-sunny"></i>
            <i v-if="theme == 'light'" class="mdi mdi-weather-night"></i>
          </button> -->
        </div>
        <Login />
      </div>
    </div>
  </nav>
</template>

<style lang="scss" scoped>
a {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-keeper-light);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>
