<template>
    <div class="px-10 py-2 bg-gray-900 min-h-screen text-gray-100">
        <div v-if="loading" class="text-center py-6 mt-10 bg-gray-800 w-2xl rounded-full m-auto border-1 border-green-500">
            <h1 class="text-1xl">Calma ai, ta carregando o trem aqui...</h1>
        </div>
        <div v-else class="max-w-full">
            <HomeInfo :active-count="activeEmployees.length" :status-counts="statusCounts" />

            <div class="rounded-2xl px-6">
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
import HomeInfo from '../components/home/HomeInfo.vue';

const employees = ref([]);
const loading = ref(true);

async function fetchEmployees() {
    loading.value = true;
    try {
        const data = await getEmployees();
        employees.value = data;
    } catch (error) {
        console.error('Vix! deu erro para buscar os WS´s, liga a api ai no visual studio', error);
    } finally {
        setTimeout(() => {
            loading.value = false;
        }, 1500)
    }
}

const activeEmployees = computed(() => {
    return employees.value.filter(emp => emp.isActive);
});

const statusCounts = computed(() => {
    const counts = {};
    activeEmployees.value.forEach(emp => {
        const statusType = emp.status?.statusType;
        if (statusType) {
            const statusId = statusType.statusTypeId;
            if (!counts[statusId]) {
                counts[statusId] = {
                    name: statusType.description,
                    iconUrl: statusType.iconUrl,
                    count: 0
                };
            }
            counts[statusId].count++;
        }
    });
    return Object.values(counts);
});

onMounted(() => {
    fetchEmployees();
});

watch(isMockMode, () => {
    fetchEmployees();
});
</script>

<style scoped></style>