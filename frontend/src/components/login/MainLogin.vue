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
      message.value = 'Não foi possível concluir o login. Ocorreu um problema inesperado.';
      isError.value = true;
      loadingLogin.value = false;
       showToast('Erro no sistema. Tente novamente ou entre em contato com o suporte.', 'error');
    }
  } catch (error) {
    const errorMessage = error.message || 'Erro de autenticação.';

    if (errorMessage.includes('Senha incorreta')) {
      message.value = 'A senha está incorreta. Por favor, verifique e tente novamente.';
      showToast('Senha incorreta.', 'error');
    } else {
      message.value = 'Não foi possível fazer o login. Por favor, verifique se selecionou o perfil correto e tente novamente.';
      showToast('Erro ao fazer login. Tente novamente.', 'error');
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
    showToast('Não foi possível carregar os perfis. Por favor, tente recarregar a página.', 'error');
  } finally {
    loading.value = false;
  }
}

onMounted(() => {
  fetchEmployees();
});
</script>