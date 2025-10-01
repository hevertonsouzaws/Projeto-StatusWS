<template>
  <div class="bg-gray-900 min-h-screen text-white">
    <Header />
    <RouterView />
    <ToastNotification />
  </div>
</template>

<script setup>
import { RouterView } from 'vue-router';
import Header from './components/Header.vue';
import ToastNotification from './components/ToastNotification.vue';

</script>

<style>

</style>