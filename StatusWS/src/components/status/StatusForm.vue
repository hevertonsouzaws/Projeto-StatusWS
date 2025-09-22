<template>
  <div class="glass-effect rounded-2xl p-6 mb-8">
    <h2 class="text-xl font-semibold text-white mb-6">{{ isEditing ? 'Editar Status' : 'Adicionar Novo Status' }}</h2>
    
    <form @submit.prevent="submitForm" class="flex flex-col lg:flex-row gap-6">
      <div class="flex-1 grid grid-cols-1 md:grid-cols-2 gap-4">
        <div>
          <label for="description" class="block text-sm font-medium text-gray-300 mb-2">Descrição</label>
          <input type="text" id="description" v-model="formData.description" required
                 class="w-full px-4 py-3 bg-gray-700 border border-gray-600 rounded-xl focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-white placeholder-gray-400">
        </div>
        
        <div>
          <label for="iconUrl" class="block text-sm font-medium text-gray-300 mb-2">URL do Ícone</label>
          <input type="url" id="iconUrl" v-model="formData.iconUrl"
                 class="w-full px-4 py-3 bg-gray-700 border border-gray-600 rounded-xl focus:ring-2 focus:ring-blue-500 focus:border-blue-500 text-white placeholder-gray-400">
        </div>
      </div>
      
      <div class="flex items-end">
        <button type="submit"
                class="bg-gradient-to-r from-blue-600 to-purple-600 hover:from-blue-700 hover:to-purple-700 text-white px-6 py-3 rounded-xl font-medium transition-all duration-300 transform hover:scale-105 neon-glow">
          {{ isEditing ? 'Atualizar Status' : 'Adicionar Status' }}
        </button>
      </div>
    </form>
    <button v-if="isEditing" @click="cancelEdit"
              class="mt-4 bg-gray-700 hover:bg-gray-600 text-white font-bold py-3 px-4 rounded-lg transition-colors">
      Cancelar Edição
    </button>
  </div>
</template>

<script setup>
import { ref, defineProps, defineEmits, watch } from 'vue';

const props = defineProps({
  initialData: {
    type: Object,
    default: null
  }
});

const emits = defineEmits(['submit', 'cancel-edit']);

const defaultFormData = {
  statusTypeId: null,
  description: '',
  iconUrl: ''
};

const formData = ref({ ...defaultFormData });
const isEditing = ref(false);

// A função foi movida para o início do script
const resetForm = () => {
  formData.value = { ...defaultFormData };
};

watch(() => props.initialData, (newData) => {
  if (newData) {
    formData.value = { ...newData };
    isEditing.value = true;
  } else {
    resetForm();
    isEditing.value = false;
  }
}, { immediate: true });

const submitForm = () => {
  emits('submit', formData.value);
};

const cancelEdit = () => {
  emits('cancel-edit');
};
</script>