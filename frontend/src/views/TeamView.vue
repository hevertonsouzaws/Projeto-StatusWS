<template>
  <div class="p-8 bg-gray-900 min-h-screen text-gray-100">
    <div ref="formAnchorRef"></div>
    <div class="flex justify-end mb-4">
      <button @click="editingEmployee = {}"
        class="cursor-pointer bg-gradient-to-r from-red-600 to-purple-600 hover:red-blue-700 hover:to-purple-700 text-white px-6 py-3 rounded-xl font-medium transition-all duration-300 transform hover:scale-105 neon-glow ">
        <span class="font-bold">+</span> Novo WS
      </button>
    </div>
    <div v-if="editingEmployee" class="mb-8">
      <TeamForm :initial-data="editingEmployee" @submit="handleFormSubmit" @cancel-edit="clearEditingEmployee" />
    </div>
    <div v-if="loading" class="text-center">Calma ai, o trem está carregando...</div>
    <div v-else>
      <div class="glass-effect rounded-2xl p-6 max-w-7xl mx-auto mb-8">
        <h2 class="text-xl font-semibold text-white mb-6">WS's Ativos</h2>
        <div v-if="employees.length === 0" class="text-center text-gray-400">
          Nenhum WS ativo encontrado.
        </div>
        <div v-else class="flex flex-wrap justify-start gap-6">
          <TeamCard v-for="employee in employees" :key="employee.employeeId" :employee="employee"
            @edit-employee="handleEditEmployee" />
        </div>
      </div>
      <div v-if="inactiveEmployees.length > 0" class="glass-effect rounded-2xl p-6 max-w-7xl mx-auto">
        <h2 class="text-xl font-semibold text-white mb-6">Inativos</h2>
        <div class="flex flex-wrap justify-start gap-6">
          <TeamCard v-for="employee in inactiveEmployees" :key="employee.employeeId" :employee="employee"
            @edit-employee="handleEditEmployee" />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { getEmployees, getInactiveEmployees, createEmployee, updateEmployee } from '../services/employeeApi';
import TeamCard from '../components/team/TeamCard.vue';
import TeamForm from '../components/team/TeamForm.vue';
import { showToast } from '../helpers/toastState.js';

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
    showToast('Falha ao carregar listas de WSs.', 'error');
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
    if (employeeData.employeeId) {
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
      showToast('WS atualizado com sucesso!', 'success');
    } else {
      const newEmployee = await createEmployee(employeeData);
      employees.value.push(newEmployee);
      clearEditingEmployee();
      showToast('WS adicionado com sucesso!', 'success');
    }
  } catch (error) {
    showToast('Erro ao salvar WS. Verifique os dados.', 'error');
  }
}

onMounted(() => {
  fetchAllEmployees();
});
</script>

<style scoped></style>
