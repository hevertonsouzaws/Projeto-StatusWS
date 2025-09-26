<template>
  <div v-if="employee" class="bg-gray-800 rounded-3xl p-6 shadow-xl w-full">
    <div class="flex flex-col md:flex-row items-center md:items-start space-y-4 md:space-y-0 md:space-x-6">
      
      <div class="flex flex-col items-center flex-shrink-0">
        <div
          class="relative w-28 h-28 rounded-full overflow-hidden border-4 border-red-500 bg-red-500 flex-shrink-0 mb-2">
          
          <img :src="employee.photo" :alt="employee.name" class="w-full h-full object-cover">
        </div>
        <p class="text-base text-gray-400 font-mono">ID: <span class="text-green-400 font-semibold">{{ employee.employeeId }}</span></p>
        <p v-if="employee.isActive"
            class="text-base font-semibold px-2.5 py-0.5 mt-5 rounded-full bg-green-900 text-green-300">
            Ativo
          </p>
      </div>

      <div class="flex-grow w-full">
        <h3 class="text-white text-3xl font-bold mb-3">Olá: {{ employee.name }} </h3>
        <div class="grid grid-cols-1 sm:grid-cols-2 gap-4 border-t border-gray-700 pt-4">
          
          <div>
            <div class="text-gray-300 font-medium mb-1">Cargo</div>
            <p class="text-white text-lg">{{ employee.position || 'N/A' }}</p>
          </div>
          
          <div class="sm:col-span-2 pt-4 border-t border-gray-700 mt-4">
            <h4 class="text-lg font-semibold text-green-400 mb-2 flex items-center">
                Status Atual:
            </h4>
            <div class="flex items-center space-x-3">
                <img v-if="employee.status?.statusType?.iconUrl" :src="employee.status.statusType.iconUrl" class="w-8 h-8 rounded-full" :alt="employee.status.statusType.description" />
                <p class="text-2xl font-bold text-white">{{ employee.status?.statusType?.description || 'Offline' }}</p>
            </div>
            <p class="text-md text-gray-400 mt-2">{{ employee.status?.customText || 'Sem descrição personalizada.' }}</p>
            <p class="text-sm text-gray-500 mt-2">Última atualização: {{ formattedTime }}</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { computed } from 'vue';
import { getTimeAgo } from '../../helpers/dateUtils'; 

const props = defineProps({
  employee: {
    type: Object,
    required: true,
  }
});

const formattedTime = computed(() => {
  if (!props.employee?.status?.updateAt) {
    return 'Nunca';
  }
  return getTimeAgo(props.employee.status.updateAt);
});
</script>