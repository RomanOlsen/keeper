<script setup>
import { computed, ref } from 'vue';
import { AppState } from '../AppState.js';
import { AuthService } from '../services/AuthService.js';
import { logger } from '@/utils/Logger.js';

const identity = computed(() => AppState.identity)
const account = computed(() => AppState.account)

function login() {
  AuthService.loginWithRedirect()
}
function logout() {
  AuthService.logout()
}

const showErrorImg = ref(false)

function swapSource() {
  logger.warn(`Error account's image`)
  showErrorImg.value = true
}

</script>

<template>
  <span class="navbar-text">
    <button class="btn selectable text-light" @click="login" v-if="!identity">
      Login
    </button>
    <div v-else>
      <div class="dropdown">
        <div role="button" class="selectable no-select" data-bs-toggle="dropdown" aria-expanded="false"
          title="open account menu">
          <div v-if="account?.picture || identity?.picture">
            <img v-if="!showErrorImg" @error="swapSource" :src="account?.picture || identity?.picture" alt="account photo" height="40" class="user-img" />
            <img v-else :src="identity?.picture" alt="account photo" height="40" class="user-img" />

          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-sm-end dropdown-menu-start p-0" role="menu" title="account menu">
          <div class="list-group">
            <RouterLink :to="{ name: 'Account' }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Manage Account
              </div>
            </RouterLink>
            <RouterLink v-if="account" :to="{ name: 'Profile Page', params: { id: account.id } }">
              <div class="list-group-item dropdown-item list-group-item-action">
                View Your Profile
              </div>
            </RouterLink>
            <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
        </div>
      </div>
    </div>
  </span>
</template>

<style lang="scss" scoped>
.user-img {
  height: 40px;
  width: 40px;
  border-radius: 100px;
  object-fit: cover;
  object-position: center;
}
</style>
