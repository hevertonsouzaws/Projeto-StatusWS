<template>
  <div class="p-8 bg-gray-900 min-h-screen text-gray-100">

    <div ref="formAnchorRef"></div>

    <TeamForm :initial-data="editingEmployee" @submit="handleFormSubmit"
      @cancel-edit="clearEditingEmployee" class="mb-8" />

    <div v-if="loading" class="text-center">Carregando...</div>
    <div v-else>
      <div class="glass-effect rounded-2xl p-6 max-w-7xl mx-auto mb-8">
        <h2 class="text-xl font-semibold text-white mb-6">Funcionários Ativos</h2>
        <div v-if="employees.length === 0" class="text-center text-gray-400">
          Nenhum funcionário ativo encontrado.
        </div>
        <div v-else class="flex flex-wrap justify-start gap-6">
          <TeamCard v-for="employee in employees" :key="employee.employeeId" :employee="employee"
            @edit-employee="handleEditEmployee" />
        </div>
      </div>

      <div v-if="inactiveEmployees.length > 0" class="glass-effect rounded-2xl p-6 max-w-7xl mx-auto">
        <h2 class="text-xl font-semibold text-white mb-6">Funcionários Inativos</h2>
        <div class="flex flex-wrap justify-start gap-6">
          <TeamCard v-for="employee in inactiveEmployees" :key="employee.employeeId" :employee="employee"
            @edit-employee="handleEditEmployee" />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue';
import { getEmployees, getInactiveEmployees, createEmployee, updateEmployee } from '../services/employeeApi';
import { isMockMode } from '../modeState.js';
import TeamCard from '../components/team/TeamCard.vue';
import TeamForm from '../components/team/TeamForm.vue';

const employees = ref([]);
const inactiveEmployees = ref([]);
const loading = ref(true);
const editingEmployee = ref(null);
const formAnchorRef = ref(null);

async function fetchAllEmployees() {
  loading.value = true;
  try {
    const [activeData, inactiveData] = await Promise.all([
      getEmployees(),
      getInactiveEmployees()
    ]);
    employees.value = activeData;
    inactiveEmployees.value = inactiveData;
  } catch (error) {
    console.error('Falha ao buscar funcionários:', error);
  } finally {
    loading.value = false;
  }
}

function handleEditEmployee(employeeId) {
  const allEmployees = [...employees.value, ...inactiveEmployees.value];
  editingEmployee.value = allEmployees.find(e => e.employeeId === employeeId);

  if (formAnchorRef.value) {
    formAnchorRef.value.scrollIntoView({ behavior: 'smooth' })
  }
}

function clearEditingEmployee() {
  editingEmployee.value = null;
}

async function handleFormSubmit(employeeData) {
  try {
    if (editingEmployee.value) {
      const updateDto = {
        name: employeeData.name,
        position: employeeData.position,
        isActive: employeeData.isActive,
        photo: employeeData.photo,
        customText: employeeData.status?.customText || '...',
        statusId: employeeData.status?.statusType?.statusTypeId || null
      };

      await updateEmployee(employeeData.employeeId, updateDto);

      await fetchAllEmployees();

      clearEditingEmployee();
      console.log('Funcionário atualizado com sucesso!');
    } else {
      const newEmployee = await createEmployee(employeeData);
      employees.value.push(newEmployee);
      alert('Funcionário adicionado com sucesso!');
    }
  } catch (error) {
    console.error('Falha na operação:', error);
    alert('Erro ao processar a requisição. Verifique o console.');
  }
}

onMounted(() => {
  fetchAllEmployees();
});

watch(isMockMode, () => {
  fetchAllEmployees();
});
</script>

<style scoped>
.glass-effect {
  background: rgba(255, 255, 255, 0.1);
  backdrop-filter: blur(10px);
  border: 1px solid rgba(255, 255, 255, 0.2);
}
</style>