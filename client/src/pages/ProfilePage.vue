<script setup>
import { AppState } from '@/AppState.js';
import { profilesService } from '@/services/ProfilesService.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const profile = computed(()=> AppState.activeProfile)
const route = useRoute()

onMounted(()=> {
  const id = route.params.id
  getAUsersProfile(id)
})

async function getAUsersProfile(id) {
try {
  await profilesService.getAUsersProfile(id)
}
catch (error){
  Pop.error(error);
}
}
</script>


<template>
<div>Profile page</div>
{{ profile.name }}
</template>


<style lang="scss" scoped>

</style>