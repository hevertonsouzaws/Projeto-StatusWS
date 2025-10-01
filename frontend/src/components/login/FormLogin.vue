<template>
  <div class="glass-effect rounded-2xl p-10 text-center max-w-md w-full m-auto">
    <div class="p-2 flex items-center justify-center mb-4">
      <div class="w-28 h-28 rounded-full overflow-hidden border-4 border-red-500">
        <img :src="employee.photo" :alt="employee.name" class="w-full h-full object-cover">
      </div>
    </div>
    <h2 class="text-2xl text-white mb-2">Entrar como {{ employee.name }}</h2>
    <p v-if="employee.position" class="text-sm text-gray-400 mb-6">{{ employee.position }}</p>
    <form @submit.prevent="$emit('submit-login')" class="flex flex-col gap-4">
      <div class="relative">
        <input :value="password" @input="$emit('update:password', $event.target.value)"
          :type="passwordVisible ? 'text' : 'password'" placeholder="Digite sua senha de acesso" required
          :disabled="loadingLogin"
          class="w-full px-4 py-3 bg-gray-800 border border-gray-700 text-white rounded-lg focus:outline-none focus:border-red-500 pr-12 transition duration-300">
        <button type="button" @click="$emit('toggle-password-visibility')"
          class="absolute inset-y-0 right-0 pr-3 flex items-center text-gray-400 hover:text-white">
          <i v-if="passwordVisible" class="fi fi-rr-eye text-xl cursor-pointer mt-1"></i>
          <i v-else class="fi fi-rr-lock text-xl cursor-pointer mt-1"></i>
        </button>
      </div>
      <button type="submit" :disabled="loadingLogin"
        class="bg-red-600 hover:bg-red-700 text-white font-bold py-3 rounded-lg transition duration-300 disabled:opacity-50">
        {{ loadingLogin ? 'Verificando...' : 'Entrar' }}
      </button>
    </form>
    <button @click="$emit('reset-selection')"
      class="mt-4 w-full bg-gray-700 hover:bg-gray-600 text-gray-300 font-bold py-3 rounded-lg transition duration-300">
      Voltar para a seleção de perfis
    </button>
    <p v-if="message" :class="{ 'text-red-500': isError, 'text-green-500': !isError }" class="mt-4 text-sm font-medium">
      {{ message }}
    </p>
  </div>
</template>

<script setup>
import { defineProps, defineEmits } from 'vue';

defineProps({
  employee: {
    type: Object,
    required: true,
  },
  password: {
    type: String,
    required: true,
  },
  passwordVisible: {
    type: Boolean,
    required: true,
  },
  loadingLogin: {
    type: Boolean,
    required: true,
  },
  message: {
    type: String,
    required: true,
  },
  isError: {
    type: Boolean,
    required: true,
  }
});

defineEmits(['update:password', 'toggle-password-visibility', 'submit-login', 'reset-selection']);
</script>