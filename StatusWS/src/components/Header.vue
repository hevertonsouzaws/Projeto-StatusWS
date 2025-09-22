<template>
  <header class="bg-gray-900 shadow-xl text-amber-50 border-b border-blue-700 sticky top-0  z-50">
    <div class="w-full mx-auto px-6 py-4">
      <div class="flex items-center justify-between">
        <div class="flex items-center space-x-4">
          <div>
            <img class="w-18 h-14" src="/logo-vetor.png" alt="">
          </div>
          <div>
            <h1 class="text-2xl text-white">Status WS</h1>
            <p class="text-sm text-gray-400">Dashboard de Equipe</p>
          </div>
        </div>
        <nav class="flex items-center gap-10">
          <router-link to="/" class="hover:text-blue-300">Home</router-link>
          <router-link to="/team" class="hover:text-red-300">Equipe</router-link>
          <router-link to="/status" class="hover:text-green-300">Status</router-link>

          <button @click="toggleMode" :class="[
            'px-4 py-2 rounded-lg font-medium transition-colors cursor-pointer',
            isMockMode ? 'bg-green-600 hover:bg-green-700' : 'bg-blue-600 hover:bg-blue-700'
          ]">
            {{ isMockMode ? 'Desenvovimento' : 'Produção' }}
          </button>
        </nav>
      </div>
    </div>
  </header>
</template>

<script setup>
import { isMockMode, toggleMode } from '../modeState';
</script>

<style scoped></style>