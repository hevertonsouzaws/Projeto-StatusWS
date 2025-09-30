<template>
  <div class="px-8 py-20 bg-gray-900 min-h-auto text-gray-100 flex flex-col justify-center items-center">

    <div class="rounded-2xl p-6 text-center min-w-4xl max-w-7xl">
      
      <ProfileSelect
        v-if="!selectedEmployee"
        :employees="employees"
        :loading="loading"
        @select-employee="selectEmployee"
      />

      <LoginForm
        v-else
        :employee="selectedEmployee"
        :password="password"
        :password-visible="passwordVisible"
        :loading-login="loadingLogin"
        :message="message"
        :is-error="isError"
        @update:password="password = $event"
        @toggle-password-visibility="togglePasswordVisibility"
        @submit-login="handleLogin"
        @reset-selection="resetSelection"
      />

    </div>
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router';
import { ref, onMounted, watch, nextTick } from 'vue';
import { getEmployees, authenticateLogin } from '../../services/employeeApi.js'; 
import { isMockMode, setUsuarioLogado } from '../../modeState.js';
import LoginForm from './FormLogin.vue';
import ProfileSelect from './ProfileSelect.vue';

const router = useRouter();
const employees = ref([]);
const loading = ref(true);
const passwordVisible = ref(false);

const selectedEmployee = ref(null);
const password = ref('');
const loadingLogin = ref(false);
const message = ref('');
const isError = ref(false);

const togglePasswordVisibility = () => {
  passwordVisible.value = !passwordVisible.value;
};

const selectEmployee = (employee) => {
  selectedEmployee.value = employee;
  message.value = '';
  isError.value = false;
  password.value = '';
};

const handleLogin = async () => {
    loadingLogin.value = true;
    message.value = '';
    isError.value = false;

    try {
        const data = await authenticateLogin(selectedEmployee.value.employeeId, password.value);

        if (data && data.employeeId) {
            const userDataToStore = {
                ...selectedEmployee.value,
                employeeId: data.employeeId,
                name: data.name,
                id: data.employeeId,
            };

            setUsuarioLogado(userDataToStore);
            
            await nextTick(); 
            router.push({ name: 'home' });

            message.value = 'Login bem-sucedido!';

        } else {
            message.value = 'Login falhou, mas a API retornou um sucesso vazio.';
            isError.value = true;
            loadingLogin.value = false;
        }
    } catch (error) {
        const errorMessage = error.message || 'Erro de autenticação.';

        if (errorMessage.includes('Senha incorreta')) {
            message.value = 'Senha incorreta ou perfil inválido.';
        } else if (errorMessage.includes('Network Error') && !isMockMode.value) {
            message.value = 'Falha na conexão com a API de Produção. Verifique o servidor.';
        } else {
            message.value = errorMessage;
        }

        isError.value = true;
        password.value = '';
        loadingLogin.value = false;
    } 
};

const resetSelection = () => {
  selectedEmployee.value = null;
  password.value = '';
  message.value = '';
};

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

onMounted(() => {
  fetchEmployees();
});

watch(isMockMode, () => {
  fetchEmployees();
});
</script>

<style scoped>
</style>