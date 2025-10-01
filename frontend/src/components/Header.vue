<template>
  <header class="bg-gray-900 shadow-xl text-amber-50 border-b border-red-800 sticky top-0  z-50">
    <div class="w-full mx-auto px-6 py-5">
      <div class="flex items-center justify-between">
        <div class="flex items-center space-x-4">
          <div>
            <router-link to="/home">
              <img class="w-18 h-14" src="/logo-vetor.png" alt="">
            </router-link>
          </div>
          <div>
            <h1 class="text-2xl text-white">Status WS</h1>
          </div>
        </div>
        <nav class="flex items-center space-x-8 text-gray-400">
          <div v-if="usuarioLogado" class="flex space-x-10">
            <router-link to="/home" class="group flex flex-col items-center hover:text-red-500 transition duration-200">
              <i
                class="fi fi-rr-house-chimney text-xl transition-transform duration-200 group-hover:scale-115 mt-2"></i>
            </router-link>
            <router-link to="/user" class="group flex flex-col items-center hover:text-red-500 transition duration-200">
              <i class="fi fi-rr-square-plus text-xl transition-transform duration-200 group-hover:scale-115 mt-2"></i>
            </router-link>
            <router-link v-if="isAdmin" to="/team"
              class="group flex flex-col items-center hover:text-red-500 transition duration-200">
              <i class="fi fi-rr-users-alt text-xl transition-transform duration-200 group-hover:scale-115 mt-2"></i>
            </router-link>
            <router-link v-if="isAdmin" to="/status"
              class="group flex flex-col items-center hover:text-red-500 transition duration-200">
              <i class="fi fi-rr-list-check text-xl transition-transform duration-200 group-hover:scale-115 mt-2"></i>
            </router-link>
          </div>
          <router-link v-if="usuarioLogado" to="/user" class="ml-4">
            <img :src="usuarioLogado.photo" alt="Foto de perfil"
              class="w-14 h-14 rounded-full border-2 border-red-500 cursor-pointer transition-transform duration-200 hover:scale-110">
          </router-link>
          <div v-if="usuarioLogado" @click="handleLogout"
            class="group flex flex-col items-center hover:text-red-500 transition duration-200 cursor-pointer">
            <i class="fi fi-rr-exit text-xl transition-transform duration-200 group-hover:scale-115 mt-2"></i>
          </div>
        </nav>
      </div>
    </div>
  </header>
</template>

<script setup>
import { useRouter } from 'vue-router';
import { usuarioLogado, isAdmin, logoutUsuario } from '../modeState';

const router = useRouter();

const handleLogout = () => {
  logoutUsuario();
  router.push('/');
};
</script>