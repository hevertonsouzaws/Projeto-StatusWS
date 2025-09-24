<template>
  <div class="glass-effect rounded-2xl p-8 max-w-7xl mx-auto fade-in">
    <div class="flex flex-col md:flex-row gap-10">

      <div class="flex-1">
        <h2 class="text-xl font-semibold text-white mb-6">
          {{ isEditing ? 'Editar Funcionário' : 'Adicionar Novo Funcionário' }}
        </h2>
        <form @submit.prevent="submitForm">
          <div class="space-y-5">
            <div>
              <label for="name" class="block text-sm font-medium text-gray-400">Nome</label>
              <input type="text" id="name" v-model="formData.name" required
                class="mt-1 block w-full bg-gray-800 border border-gray-700 rounded-lg shadow-sm text-white focus:ring-blue-500 focus:border-blue-500 p-3">
            </div>
            <div>
              <label for="position" class="block text-sm font-medium text-gray-400">Cargo</label>
              <input type="text" id="position" v-model="formData.position" required
                class="mt-1 block w-full bg-gray-800 border border-gray-700 rounded-lg shadow-sm text-white focus:ring-blue-500 focus:border-blue-500 p-3">
            </div>
            <div>
              <label for="photo" class="block text-sm font-medium text-gray-400">URL da Foto</label>
              <input type="url" id="photo" v-model="formData.photo"
                class="mt-1 block w-full bg-gray-800 border border-gray-700 rounded-lg shadow-sm text-white focus:ring-blue-500 focus:border-blue-500 p-3">
            </div>

            <div v-if="isEditing" class="flex items-center">
              <input type="checkbox" id="isActive" v-model="formData.isActive"
                class="h-4 w-4 text-blue-600 bg-gray-800 border-gray-600 rounded focus:ring-blue-500">
              <label for="isActive" class="ml-2 block text-sm font-medium text-gray-400">Funcionário Ativo</label>
            </div>

          </div>

          <button type="submit"
            class="mt-8 w-full bg-blue-500 hover:bg-blue-600 cursor-pointer text-white font-bold py-3 px-4 rounded-lg transition-colors">
            {{ isEditing ? 'Atualizar Funcionário' : 'Adicionar Funcionário' }}
          </button>
        </form>
        <button v-if="isEditing" @click="cancelEdit"
          class="mt-4 w-full bg-red-900 hover:bg-red-700 cursor-pointer text-white font-bold py-3 px-4 rounded-lg transition-colors">
          Cancelar
        </button>
      </div>

      <div class="flex-1 flex justify-center items-center p-4">
        <div class="p-4 rounded-lg bg-gray-800/50 w-full max-w-md flex flex-col items-center">
          <h3 class="text-white font-semibold text-center text-lg mb-4">Pré-visualização</h3>
          <TeamCard :employee="formData" />
        </div>
      </div>

    </div>
  </div>
</template>

<script setup>
import { ref, defineEmits, defineProps, watch } from 'vue';
import TeamCard from './TeamCard.vue';

const props = defineProps({
  initialData: {
    type: Object,
    default: null,
  },
});

const emits = defineEmits(['submit', 'cancel-edit']);

const defaultEmployeeData = {
  name: 'Nome Sobrenome',
  position: 'Cargo',
  photo: 'https://tarefas.websupply.com.br/painel/assets/userDefault-uMDAqLiz.png',
  isActive: true,
  status: {
    customText: null,
    updateAt: new Date().toISOString(),
    statusType: {
      iconUrl: 'https://cdn-icons-gif.flaticon.com/8756/8756038.gif',
      description: 'Sem status definido'
    }
  }
};

const formData = ref({ ...defaultEmployeeData });
const isEditing = ref(false);

watch(() => props.initialData, (newData) => {
  if (newData) {
    formData.value = { ...newData };
    isEditing.value = !!newData.employeeId;
  } else {
    formData.value = { ...defaultEmployeeData };
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