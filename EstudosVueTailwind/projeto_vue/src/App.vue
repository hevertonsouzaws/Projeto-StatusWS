<template>
  <div>
    <Header :esta_logado="true" />
    <PrimeiroComponente />
    <LifeCycle />
    <Pessoa />
    <MultiplosEventos />
    <Reutilizacao />
    <MudarImagem />
  </div>
</template>

<script>

import PrimeiroComponente from './components/PrimeiroComponente.vue'
import LifeCycle from './components/LifeCycle.vue'
import Pessoa from './components/Pessoa.vue'
import Header from './components/Header.vue';
import MultiplosEventos from './components/MultiplosEventos.vue';
import Reutilizacao from './components/Reutilizacao.vue';
import MudarImagem from './components/MudarImagem.vue';

export default {
  name: 'App',
  components: {
    Header,
    PrimeiroComponente,
    LifeCycle,
    Pessoa,
    MultiplosEventos,
    Reutilizacao,
    MudarImagem
  }
}
</script>

<style>
* {
  padding: 10px;
  margin: 0;
  color: #fff;
}

body {
  background: linear-gradient(180.73deg, #0e0c2b -51.9%, #0b1336 131.43%, #0c1253 131.45%);
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

body ul,
li {
  list-style: none;
  text-decoration: none;
}

button {
  background-color: rgb(235, 235, 253);
  border: 1px solid black;
  color: black;
  padding: 8px;
  border-radius: 5px;
  padding: 20px;
}

button:hover {
  background: linear-gradient(180.73deg, #453bce -51.9%, #162568 131.43%, #1b28b4 131.45%);
  color: white;
  border: none;
}
</style>