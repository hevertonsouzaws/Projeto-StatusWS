<template>
  <div class="flex flex-col items-center bg-gray-800 rounded-2xl py-4 px-5 shadow-lg transform duration-300 min-w-84">
    <div class="flex items-center w-full mb-1">
      <div
        class="relative w-14 h-14 rounded-full overflow-hidden border border-transparent bg-green-500 flex-shrink-0">
        <img :src="employee.photo" :alt="employee.name" class="w-full h-full object-cover">
      </div>
      <div class="ml-4 flex-grow">
        <h3 class="text-white text-lg font-semibold truncate">{{ employee.name }}</h3>
      </div>
    </div>

    <div class="bg-gray-800/50 rounded-lg px-1 py-4 w-full">
      <div class="flex items-center justify-between">
        <div class="flex flex-col space-y-2">
          <p class="text-lg font-medium text-green-400">
            {{ employee.status?.statusType?.description }}
          </p>
          <p class="text-sm text-gray-200 bg-red-00 ">
            {{ employee.status?.customText || '...' }}
          </p>
          <p class="text-sm text-green-100">
            Atualização: {{ formattedTimeAgo }}
          </p>
        </div>
        <div class="flex-shrink-0">
          <div v-if="employee.status?.statusType?.iconUrl"
            class="bg-white rounded-full p-2 flex items-center justify-center">
            <img :src="employee.status.statusType.iconUrl" class="w-15 h-15 rounded-full"
              :alt="employee.status.statusType.description" />
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

const formattedTimeAgo = computed(() => {
    if (!props.employee?.status?.updateAt) {
        return 'Nunca';
    }
    return getTimeAgo(props.employee.status.updateAt);
});

// A propriedade 'statusInfo' foi removida, pois 'formattedTimeAgo' a substitui
</script>