<template>
  <div class="px-8 py-20 bg-gray-900 min-h-auto text-gray-100 flex flex-col justify-center items-center">
    <div class="glass-effect rounded-2xl p-6 text-center min-w-4xl max-w-7xl">
      <div class="p-2 flex items-center justify-center">
        <img src="https://cdn-icons-gif.flaticon.com/16904/16904032.gif" alt="" class="w-18 h-18 rounded-full">
      </div>
      <h2 class="text-2xl text-white mb-4">Selecione seu perfil</h2>
      <hr class="mb-10 text-gray-500">
      <div v-if="loading" class="text-center">Carregando perfis...</div>
      <div v-else class="flex flex-wrap justify-center gap-8">
        <div v-for="employee in employees" :key="employee.employeeId" @click="handleLogin(employee)"
          class="flex flex-col items-center cursor-pointer transition-transform transform hover:scale-105 duration-300 hover:text-red-500 saturate-0 hover:saturate-100">
          <div
            class="w-28 h-28 rounded-full overflow-hidden border-2 border-transparent hover:border-red-500 transition-colors duration-300 ">
            <img :src="employee.photo" :alt="employee.name" class="w-full h-full object-cover ">
          </div>
          <p class="mt-4 text-base font-medium truncate w-28">{{ employee.name }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router';
import { ref, onMounted, watch } from 'vue';
import { getEmployees } from '../services/employeeApi';
import { isMockMode, setUsuarioLogado } from '../modeState.js';

const router = useRouter();
const employees = ref([]);
const loading = ref(true);

const handleLogin = (employee) => {
  setUsuarioLogado(employee.employeeId, employee);
  router.push({ name: 'home' })
}

async function fetchEmployees() {
  loading.value = true;
  try {
    const data = await getEmployees();
    employees.value = data;
  } catch (error) {
    console.error('Falha ao buscar funcionários:', error);
  } finally {
    loading.value = false;
  }
}

onMounted(() => {
  fetchEmployees();
});

watch(isMockMode, () => {
  fetchEmployees();
});
</script>

<style scoped></style>