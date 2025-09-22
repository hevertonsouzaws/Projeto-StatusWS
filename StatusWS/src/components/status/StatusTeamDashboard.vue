<template>
  <div class="glass-effect rounded-2xl p-6 max-w-7xl mx-auto">
    <h2 class="text-xl font-semibold text-white mb-6">Status da Equipe</h2>
    
    <div v-if="loading" class="text-center text-gray-400">
      Carregando a equipe...
    </div>
    <div v-else-if="employees.length === 0" class="text-center text-gray-400">
      Nenhum funcionário ativo encontrado.
    </div>
    <div v-else class="flex flex-wrap justify-center gap-6">
      <TeamCard 
        v-for="employee in employees" 
        :key="employee.employeeId" 
        :employee="employee"
      />
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { getEmployees } from '../../services/employeeApi';
import TeamCard from '../team/TeamCard.vue'; // Importa o componente TeamCard da pasta correta

const employees = ref([]);
const loading = ref(true);

const fetchEmployees = async () => {
  loading.value = true;
  try {
    const data = await getEmployees();
    employees.value = data;
  } catch (error) {
    console.error('Falha ao buscar funcionários:', error);
  } finally {
    loading.value = false;
  }
};

onMounted(() => {
  fetchEmployees();
});
</script>

<style scoped>
.glass-effect {
  background: rgba(255, 255, 255, 0.1);
  backdrop-filter: blur(10px);
  border: 1px solid rgba(255, 255, 255, 0.2);
}
</style>