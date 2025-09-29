<template>
  <header class="bg-gray-900 shadow-xl text-amber-50 border-b border-blue-700 sticky top-0  z-50">
    <div class="w-full mx-auto px-6 py-5">
      <div class="flex items-center justify-between">
        <div class="flex items-center space-x-4">
          <div>
            <router-link to="/home" class="hover:text-blue-300">
              <img class="w-18 h-14" src="/logo-vetor.png" alt="">
            </router-link>            
          </div>
          <div>
            <h1 class="text-2xl text-white">Status WS</h1>
          </div>
        </div>
        <nav class="flex items-center gap-10">
          
          <router-link to="/" class="hover:text-white-300">Login</router-link>
          <router-link to="/home" class="hover:text-blue-300">Home</router-link>
          <router-link to="/user" class="hover:text-green-300">Usuário</router-link>
          <router-link v-if="isAdmin" to="/team" class="hover:text-red-300">Equipe</router-link>
          <router-link v-if="isAdmin" to="/status" class="hover:text-green-300">Status</router-link>

          <button @click="toggleMode" :class="[
            'px-4 py-2 rounded-lg font-medium transition-colors cursor-pointer',
            isMockMode ? 'bg-yellow-600 hover:bg-yellow-700' : 'bg-blue-600 hover:bg-blue-700'
          ]">
            {{ isMockMode ? 'Desenvovimento' : 'Produção' }}
          </button>

          <div v-if="usuarioLogado" class="flex items-center">
            <span class="text-white text-sm mr-2">Logado como:</span>
            <img :src="usuarioLogado.photo" alt="Foto de perfil" class="w-14 h-14 rounded-full border-2 border-red-500">
          </div>
        </nav>
      </div>
    </div>
  </header>
</template>

<script setup>
import { isMockMode, toggleMode, usuarioLogadoId, usuarioLogado } from '../modeState';
import { computed } from 'vue';

// lista de adm
const adminNames = ['Heverton Souza', 'Aline Gallo'];

const isAdmin = computed(() => {
  return usuarioLogado.value && adminNames.includes(usuarioLogado.value.name);
});
</script>

<style scoped></style>