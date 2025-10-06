<template>
  <div class="max-w-7xl mx-auto p-8 bg-gray-900 min-h-screen text-gray-100">
    <div class="flex items-center justify-between mb-8">
      <h1 class="text-3xl font-bold">Tipos de status</h1>
    </div>
    <StatusForm :initial-data="editingStatus" @submit="handleFormSubmit" @cancel-edit="cancelEdit"
      @delete="handleDeleteStatus" class="mb-8" />
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
import { showToast } from '../helpers/toastState.js';

const statusTypes = ref([]);
const loading = ref(true);
const editingStatus = ref(null);

async function fetchStatusTypes() {
  loading.value = true;
  try {
    statusTypes.value = await getStatusTypes();
  } catch (error) {
    showToast('Falha ao buscar tipos de status:', error);
  } finally {
    loading.value = false;
  }
}

async function handleFormSubmit(formData) {
  try {
    if (editingStatus.value) {
      await updateStatusType(formData.statusTypeId, formData);
      showToast('Status atualizado com sucesso!');
    } else {
      await createStatusType(formData);
      showToast('Status adicionado com sucesso!');
    }
    cancelEdit();
    fetchStatusTypes();
  } catch (error) {
    showToast('Erro ao processar a requisição. Verifique o console.');
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
  try {
    await deleteStatusType(id);
    showToast('Status removido com sucesso!');
    cancelEdit();
    fetchStatusTypes();
  } catch (error) {
    showToast('Erro ao remover status:', error);
  }
}

onMounted(() => {
  fetchStatusTypes();
});
</script>