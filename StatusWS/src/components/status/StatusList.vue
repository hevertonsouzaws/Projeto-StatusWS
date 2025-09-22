<template>
    <div class="glass-effect rounded-2xl p-6">
        <h2 class="text-xl font-semibold text-white mb-6">Status Disponíveis</h2>
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
            <div v-for="status in statusTypes" :key="status.statusTypeId"
                class="glass-effect rounded-2xl p-6 transition-all duration-300 fade-in group hover:scale-105 cursor-pointer">
                <div class="flex flex-col items-center text-center">
                    <img :src="status.iconUrl" :alt="status.description" class="w-15 h-15 mb-4 rounded-full">
                    <h3 class="font-semibold text-white mb-1">{{ status.description }}</h3>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { defineProps, defineEmits } from 'vue';

defineProps({
    statusTypes: {
        type: Array,
        required: true
    }
});

defineEmits(['edit-status', 'delete-status']);
</script>