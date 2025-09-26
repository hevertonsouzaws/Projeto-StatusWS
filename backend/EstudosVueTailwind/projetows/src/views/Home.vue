<template>
    <main>
        <Banner />
    <div class="main-container">
        <BurgerForm />
    </div>
  
    </main>
</template>

<script>
import BurgerForm from '../components/BurgerForm.vue';
import Banner from '../components/Banner.vue';

export default {
    name: "Home",
    components: {
        Banner,
        BurgerForm,
    }
}
</script>

<style scoped>

</style>

