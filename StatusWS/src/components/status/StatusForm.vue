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

      <div class="flex items-end gap-5">
        <button type="submit"
          class="cursor-pointer bg-gradient-to-r from-blue-600 to-purple-600 hover:from-green-700 hover:to-green-600 text-white px-6 py-3 rounded-xl font-medium transition-all duration-300 transform hover:scale-105 neon-glow">
          {{ isEditing ? 'Salvar' : 'Adicionar Status' }}
        </button>

        <button v-if="isEditing" type="button" @click="cancelEdit"
         class="cursor-pointer bg-gradient-to-r from-red-600 to-red-700 hover:from-red-700 hover:to-red-600 text-white px-6 py-3 rounded-xl font-medium transition-all duration-300 transform hover:scale-105 neon-glow">
          Cancelar
        </button>
      </div>
    </form>
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