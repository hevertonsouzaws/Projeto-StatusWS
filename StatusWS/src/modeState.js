import { ref } from 'vue';

export const isMockMode = ref(true);

export function toggleMode() {
  isMockMode.value = !isMockMode.value;
}