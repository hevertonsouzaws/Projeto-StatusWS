<template>
  <h1>Descrição: {{ nome }}</h1>
</template>

<script>
export default {
    name: 'Lyfecycle',
    data() {
       return {
         nome: '#SomosWS'
       }
    },
    created() {
      setTimeout(() => {
      this.nome = 'Unidos pela inovação'
      }, 2000);
    },
    mounted() {
      setTimeout(() => {
        this.nome = 'Impusionando o futuro!'
      }, 3000),
      setTimeout(() => {
        this.nome = 'WebSupply'
      }, 4000);

      this.LifeCycle()
    },
    methods: {
      LifeCycle() {
        console.log('Executou')
      }
    }
    
}
</script>

