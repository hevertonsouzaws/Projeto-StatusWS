<template>
    <div class="p-10 bg-gray-900 min-h-screen text-gray-100">
        <div v-if="loading" class="text-center">Calma ai, ta carregando o trem aqui...</div>
        <div v-else class="max-w-full">
            <div class="flex flex-row items-center justify-between max-w-[98%] rounded-full">
                <div class="rounded-2xl py-8 px-8 mb-8 gap-4 flex items-center justify-right flex-wrap">
                    <div class="bg-gray-800 rounded-lg py-2 text-center min-w-50 border border-green-800">
                        <p class="text-green-400">Ativos</p>
                        <h3 class="text-white text-xl font-bold">{{ activeEmployees.length }}</h3>
                    </div>
                    <div v-for="(count, statusName) in statusCounts" :key="statusName"
                        class="bg-gray-800/50 rounded-lg py-2 text-center min-w-50 border border-red-800">
                        <p class="text-gray-400">{{ statusName }}</p>
                        <h3 class="text-white text-xl font-bold">{{ count }}</h3>
                    </div>
                </div>
                <div class="text-right text-3xl w-[40%] py-2 px-8">
                    <h1 class="text-4xl text-white"><span class="text-red-600">#</span>SomosWS</h1>
                    <p class="text-red-600 uppercase">Unidos pela Inovação,</p>
                    <p class="uppercase">Impulsionando</p>
                    <p class="text-red-600 uppercase">O futuro!</p>
                </div>
                
            </div>

            <div class="rounded-2xl p-6">
                <div v-if="activeEmployees.length === 0" class="text-center text-gray-400">
                    Nenhum WS ativo encontrado.
                </div>
                <div v-else class="flex flex-wrap justify-start gap-6">
                    <StatusCard v-for="employee in activeEmployees" :key="employee.employeeId" :employee="employee" />
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted, watch, computed } from 'vue';
import { getEmployees } from '../services/employeeApi';
import { isMockMode } from '../modeState.js';
import StatusCard from '../components/home/StatusCard.vue';

const employees = ref([]);
const loading = ref(true);

async function fetchEmployees() {
    loading.value = true;
    try {
        const data = await getEmployees();
        employees.value = data;
    } catch (error) {
        console.error('Falha ao buscar funcionários:', error);
    } finally {
        loading.value = false;
    }
}

const activeEmployees = computed(() => {
    return employees.value.filter(emp => emp.isActive);
});

const statusCounts = computed(() => {
    const counts = {};
    activeEmployees.value.forEach(emp => {
        const statusName = emp.status?.statusType?.description || 'Sem status';
        counts[statusName] = (counts[statusName] || 0) + 1;
    });
    return counts;
});

onMounted(() => {
    fetchEmployees();
});

watch(isMockMode, () => {
    fetchEmployees();
});
</script>

<style scoped>
</style>