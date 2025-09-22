<template>
  <div class="max-w-7xl mx-auto p-8 bg-gray-900 min-h-screen text-gray-100">
    <div class="flex items-center justify-between mb-8">
      <h1 class="text-3xl font-bold">Gerenciar Plaquinhas de Status</h1>
    </div>

    <StatusForm :initial-data="editingStatus" @submit="handleFormSubmit" @cancel-edit="cancelEdit" class="mb-8" />

    <div v-if="loading" class="text-center">Carregando...</div>
    <div v-else>
      <StatusList :status-types="statusTypes" @edit-status="handleEditStatus" @delete-status="handleDeleteStatus" />
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { getStatusTypes, createStatusType, updateStatusType, deleteStatusType } from '../services/statusApi';
import StatusForm from '../components/status/StatusForm.vue';
import StatusList from '../components/status/StatusList.vue';

const statusTypes = ref([]);
const loading = ref(true);
const editingStatus = ref(null);

async function fetchStatusTypes() {
  loading.value = true;
  try {
    statusTypes.value = await getStatusTypes();
  } catch (error) {
    console.error('Falha ao buscar tipos de status:', error);
  } finally {
    loading.value = false;
  }
}

async function handleFormSubmit(formData) {
  try {
    if (editingStatus.value) {
      await updateStatusType(formData.statusTypeId, formData);
      alert('Status atualizado com sucesso!');
    } else {
      await createStatusType(formData);
      alert('Status adicionado com sucesso!');
    }
    cancelEdit();
    fetchStatusTypes();
  } catch (error) {
    alert('Erro ao processar a requisição. Verifique o console.');
  }
}

function handleEditStatus(status) {
  editingStatus.value = { ...status };
  window.scrollTo({ top: 0, behavior: 'smooth' });
}

function cancelEdit() {
  editingStatus.value = null;
}

async function handleDeleteStatus(id) {
  if (confirm('Tem certeza que deseja remover este status?')) {
    try {
      await deleteStatusType(id);
      alert('Status removido com sucesso!');
      fetchStatusTypes();
    } catch (error) {
      alert('Erro ao remover status. Ele pode estar em uso por um funcionário.');
    }
  }
}

onMounted(() => {
  fetchStatusTypes();
});
</script>