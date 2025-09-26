<template>
  <div>
    <h2>Dados do: {{ nome }}</h2>
    <Info :email="email" :esta_trabalhando="true"/>
    <Form />
  </div>
</template>

<script>
import Form from './Form.vue';
import Info from './Info.vue';

export default {
  name: 'Pessoa',
  components: {
    Info,
    Form
  },
  data() {
    return {
      nome: 'Heverton',
      email: 'somosws@gmail.com',
    }
  }
}
</script>
