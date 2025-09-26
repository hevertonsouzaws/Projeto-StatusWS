<template>
  <NavBar />
  <router-view />
  <Footer />
</template>

<script>
import NavBar from './components/NavBar.vue';

export default {
  components: {
    NavBar,
    Footer
  }
}

</script>

<style scoped></style>
