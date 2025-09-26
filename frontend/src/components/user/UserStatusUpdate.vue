<template>
  <div class="bg-gray-800 rounded-3xl p-6 shadow-xl w-full mt-8">
    
    <h2 class="text-2xl font-bold text-white mb-6">Atualizar Status</h2>
    
    <div class="grid grid-cols-2 md:grid-cols-4 gap-4 mb-6">
      <div
        v-for="status in statusTypes"
        :key="status.statusTypeId"
        @click="selectStatus(status)"
        :class="[
          'p-4 flex flex-col items-center rounded-xl border-2 cursor-pointer transition-all duration-200 min-h-24 justify-center',
          'hover:bg-gray-700/50',
          selectedStatus?.statusTypeId === status.statusTypeId 
            ? 'bg-green-700/20 border-green-500 shadow-lg scale-105' 
            : 'bg-gray-900 border-gray-700'
        ]"
      >
        <img :src="status.iconUrl" :alt="status.description" class="w-12 h-12 mb-2 rounded-full" />
        <span class="text-sm font-medium text-white text-center">{{ status.description }}</span>
      </div>
    </div>

    <div class="mb-6">
      <label for="customText" class="block text-sm font-medium text-gray-400 mb-2">
        Descrição / Projeto (Opcional):
      </label>
      <input
        id="customText"
        v-model="customText"
        type="text"
        placeholder="Ex: Projeto Conect"
        class="w-full px-4 py-3 bg-gray-900 text-white rounded-lg border border-gray-700 focus:border-green-500 focus:ring-1 focus:ring-green-500 transition-colors"
      />
    </div>
    
    <button
      @click="saveStatus"
      :disabled="!selectedStatus || isLoading"
      :class="[
        'w-full py-3 rounded-xl text-lg font-bold transition-colors duration-200 cursor-pointer',
        (!selectedStatus || isLoading) 
          ? 'bg-gray-600 text-gray-400 cursor-not-allowed' 
          : 'bg-blue-600 hover:bg-blue-700 text-white shadow-blue-500/30 shadow-md'
      ]"
    >
      {{ isLoading ? 'Salvando...' : 'Atualizar Status' }}
    </button>

    <p v-if="message" :class="['mt-4 text-center text-sm', isError ? 'text-red-400' : 'text-green-400']">{{ message }}</p>

  </div>
</template>

<script setup>
import { ref, onMounted, defineEmits } from 'vue';
import { getStatusTypes } from '../../services/statusApi'; 
import { updateEmployee } from '../../services/employeeApi'; 

const props = defineProps({
  employeeId: {
    type: [Number, String],
    required: true,
  }
});

const emit = defineEmits(['status-updated']);

const statusTypes = ref([]);
const selectedStatus = ref(null);
const customText = ref('');
const isLoading = ref(false);
const message = ref('');
const isError = ref(false);

onMounted(async () => {
  try {
    statusTypes.value = await getStatusTypes();
  } catch (error) {
    console.error('Erro ao carregar tipos de status:', error);
    message.value = 'Erro ao carregar opções de status.';
    isError.value = true;
  }
});

const selectStatus = (status) => {
  selectedStatus.value = status;
  message.value = '';
};

const saveStatus = async () => {
  if (!selectedStatus.value) {
    message.value = 'Por favor, selecione um tipo de status.';
    isError.value = true;
    return;
  }

  isLoading.value = true;
  message.value = '';
  isError.value = false;

  const textToSend = customText.value.trim() === '' ? null : customText.value.trim();

  const newStatusData = {
    statusTypeId: selectedStatus.value.statusTypeId,
    customText: textToSend, 
  };

  try {
    await updateEmployee(props.employeeId, newStatusData);

    message.value = 'Status atualizado com sucesso!';
    emit('status-updated'); 
  } catch (error) {
    console.error('Erro ao salvar status:', error);
    message.value = 'Erro ao salvar status. Verifique a API ou o console.';
    isError.value = true;
  } finally {
    isLoading.value = false;
  }
};
</script>