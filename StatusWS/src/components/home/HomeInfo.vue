<template>
    <div class="flex flex-row items-center justify-between max-w-[100%] py-3 px-6">
        <div class="rounded-2xl py-4 px-6 mb-8 gap-4 flex items-center justify-right flex-wrap border border-gray-700 hover:border-green-500/50 w-auto">
            <div class="bg-gray-800 rounded-xl text-center min-w-45 py-2 border border-gray-400 flex items-center justify-around">
                <div>
                    <p class="text-gray-200 text-sm">Ativos</p>
                    <h3 class="text-white text-xl font-bold">{{ activeCount }}</h3>
                </div>
                <img class="w-10 h-10 rounded-full" src="https://cdn-icons-gif.flaticon.com/17091/17091713.gif" alt="">
            </div>
            <div v-for="status in statusCounts" :key="status.name"
                 class="bg-gray-800 rounded-xl text-center min-w-45 py-2 border border-green-800 flex items-center justify-around">
                <div>
                    <p class="text-gray-200 text-sm">{{ status.name }}</p>
                    <h3 class="text-white text-xl font-bold">{{ status.count }}</h3>
                </div>
                <div class="flex flex-col items-center justify-center gap-2">
                    <img :src="status.iconUrl" class="w-10 h-10 rounded-full" :alt="status.name" />
                </div>
            </div>
        </div>

        <div class="text-right text-3xl w-[40%] px-8 mb-15">
            <h1 class="text-4xl text-white"><span class="text-red-600">#</span>SomosWS</h1>
            <p class="text-red-600 uppercase">Unidos pela Inovação,</p>
            <p class="uppercase">Impulsionando</p>
            <p class="text-red-600 uppercase">O futuro!</p>
        </div>
    </div>
</template>

<script setup>
import { defineProps } from 'vue';

defineProps({
    activeCount: {
        type: Number,
        required: true
    },
    statusCounts: {
        type: Array,
        required: true
    }
});
</script>