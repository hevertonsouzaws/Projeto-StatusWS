<template>
  <div class="bg-gray-900 min-h-screen text-white">
    <Header />
    <RouterView />
  </div>
</template>

<script setup>
import { RouterView } from 'vue-router';
import Header from './components/Header.vue';
</script>

<style>

</style>