<template>
  <div class="px-8 py-20 bg-gray-900 min-h-auto text-gray-100 flex flex-col justify-center items-center">
    <div class="rounded-2xl p-6 text-center min-w-4xl max-w-7xl">
      <ProfileSelect v-if="!selectedEmployee" :employees="employees" :loading="loading"
        @select-employee="selectEmployee" />

      <LoginForm v-else :employee="selectedEmployee" :password="password" :password-visible="passwordVisible"
        :loading-login="loadingLogin" :message="message" :is-error="isError" @update:password="password = $event"
        @toggle-password-visibility="togglePasswordVisibility" @submit-login="handleLogin"
        @reset-selection="resetSelection" />
    </div>
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router';
import { ref, onMounted, nextTick } from 'vue';
import { getEmployees, authenticateLogin } from '../../services/employeeApi.js';
import { setUsuarioLogado } from '../../modeState.js';
import { showToast } from '../../helpers/toastState.js';

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
      showToast('Login realizado com sucesso!', 'success');
      router.push({ name: 'home' });

    } else {
      message.value = 'Login falhou, mas a API retornou um sucesso vazio.';
      isError.value = true;
      loadingLogin.value = false;
      showToast('Erro: Resposta de login inesperada.', 'error');
    }
  } catch (error) {
    const errorMessage = error.message || 'Erro de autenticação.';

    if (errorMessage.includes('Senha incorreta')) {
      message.value = 'Senha incorreta ou perfil inválido.';
      showToast('Senha incorreta ou perfil inválido.', 'error');
    } else {
      message.value = 'Falha no login. Verifique as credenciais e o status do servidor.';
      showToast('Falha no login. Verifique as credenciais e o status do servidor.', 'error');
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
    showToast('Falha ao buscar perfis. Verifique a API.', 'error');
  } finally {
    loading.value = false;
  }
}

onMounted(() => {
  fetchEmployees();
});
</script>