<template>
  <div v-if="loading" class="text-center">Carregando...</div>
  <div v-else class="text-center">
    <h1>defina seu status</h1>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { getStatusTypes, createStatusType, updateStatusType, deleteStatusType } from '../services/statusApi';
import StatusList from '../components/status/StatusList.vue';

const statusTypes = ref([]);
const loading = ref(true);

async function fetchStatusTypes() {
  loading.value = true;
  try {
    statusTypes.value = await getStatusTypes();
  } catch (error) {
    console.error('Deu erro aqui, verifique se o back end ta ligado!:', error);
  } finally {
    loading.value = false;
  }
}

onMounted(() => {
  fetchStatusTypes();
});
</script>