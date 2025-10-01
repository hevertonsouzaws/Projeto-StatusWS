<template>
  <div class="glass-effect rounded-2xl p-6 text-center min-w-4xl m-w-full">
    <div class="p-2 flex items-center justify-center">
      <img src="https://cdn-icons-gif.flaticon.com/16904/16904032.gif" alt="" class="w-18 h-18 rounded-full">
    </div>
    <h2 class="text-2xl text-white mb-4">Selecione seu perfil</h2>
    <hr class="mb-10 text-gray-500">
    <div v-if="loading" class="text-center">Carregando perfis...</div>
    <div v-else class="flex flex-wrap justify-center gap-8">
      <div v-for="employee in employees" :key="employee.employeeId" @click="$emit('select-employee', employee)"
        class="flex flex-col items-center cursor-pointer transition-transform transform hover:scale-105 duration-300 hover:text-red-500 saturate-0 hover:saturate-100">
        <div
          class="w-28 h-28 rounded-full overflow-hidden border-2 border-transparent hover:border-red-500 transition-colors duration-300 ">
          <img :src="employee.photo" :alt="employee.name" class="w-full h-full object-cover ">
        </div>
        <p class="mt-4 text-base font-medium truncate w-28">{{ employee.name }}</p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { defineProps, defineEmits } from 'vue';

defineProps({
  employees: {
    type: Array,
    required: true,
  },
  loading: {
    type: Boolean,
    required: true,
  },
});

defineEmits(['select-employee']);
</script>