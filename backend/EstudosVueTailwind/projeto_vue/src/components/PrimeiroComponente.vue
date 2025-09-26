<template>
  <div>
    <h1>Olá Vue!</h1>
    <p>Meu nome é: {{ nome }}</p>
    <p>E trabalho como: {{ profissao }}</p>
    <Reutilizacao />
  </div>
</template>


<script>
import Reutilizacao from './Reutilizacao.vue';

export default {
  name: "PrimeiroComponente",
  components: {
    Reutilizacao
  },
  data() {
    return {
      nome: "Heverton",
      profissao: "Dev WS"
    }
  } 
};
</script>

<style scoped>

div {
  padding-top: 80px;
}

</style>