import { ref } from 'vue';

export const isMockMode = ref(true);

export function toggleMode() {
  isMockMode.value = !isMockMode.value;
}

// Armazena o ID do funcionário logado
// Use null por padrão, indicando que ninguém está logado
export const usuarioLogadoId = ref(null);

// Armazena as informações completas do usuário logado
export const usuarioLogado = ref(null);

// Função para definir o usuário logado
export const setUsuarioLogado = (id, user) => {
  usuarioLogadoId.value = id;
  usuarioLogado.value = user;
};