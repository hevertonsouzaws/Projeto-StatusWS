<template>
    <div class="px-10 py-2 bg-gray-900 min-h-screen text-gray-100">
        <div v-if="loading" class="text-center py-6 mt-10 bg-gray-800 w-2xl rounded-full m-auto border-1 border-green-500">
            <h1 class="text-1xl">Calma aí, carregando seu perfil...</h1>
        </div>
        <div v-else class="max-w-4xl mx-auto">
            <h1 class="text-3xl font-bold text-white mb-8">Meu Perfil</h1>

            <UserProfileCard :employee="currentUser" class="mb-12" />
            
            <UserStatusUpdate :employee-id="currentUser.employeeId" @status-updated="handleStatusUpdated" />
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import UserProfileCard from '../components/user/UserProfileCard.vue';
import UserStatusUpdate from '../components/user/UserStatusUpdate.vue'; 
import { usuarioLogadoId } from '../modeState';
import { getEmployeeById } from '../services/employeeApi';


const currentUser = ref(null);
const loading = ref(true);

async function fetchCurrentUser() {
  loading.value = true;
  try {
    const user = await getEmployeeById(usuarioLogadoId.value);
    currentUser.value = user;
  } catch (error) {
    console.error('Erro ao buscar dados do usuário logado:', error);
  } finally {
    loading.value = false;
  }
}

const handleStatusUpdated = () => {
  fetchCurrentUser();
};

onMounted(() => {
  fetchCurrentUser();
});
</script>