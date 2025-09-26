<template>
  <div id="burger-table">
    <Message :msg="msg" v-show="msg" />
    <div>
      <div id="burger-table-heading">
        <div class="order-id">#:</div>
        <div>Cliente:</div>
        <div>Pão:</div>
        <div>Carne:</div>
        <div>Opcionais:</div>
        <div>Ações:</div>
      </div>
      <div id="burger-table-rows">
        <div class="burger-table-row" v-for="burger in burgers" :key="burger.id">
          <div class="order-number">{{ burger.id }}</div>
          <div>{{ burger.nome }}</div>
          <div>{{ burger.pao }}</div>
          <div>{{ burger.carne }}</div>
          <div>
            <ul>
              <li v-for="(opcional, index) in burger.opcionais" :key="index">{{ opcional }}</li>
            </ul>
          </div>
          <div>
            <select name="status" class="status" @change="updateBurger($event, burger.id)">
              <option :value="s.tipo" v-for="s in status" :key="s.id" :selected="burger.status == s.tipo">
                {{ s.tipo }}
              </option>
            </select>
            <button class="delete-btn" @click="deleteBurger(burger.id)">Cancelar</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Message from './Message.vue';

export default {
  name: 'Dashboard',
  data() {
    return {
      burgers: null,
      burger_id: null,
      status: [],
      msg: null
    }
  },
  components: {
    Message
  },
  methods: {
    async getPedidos() {

      const req = await fetch("http://localhost:3000/burgers")

      const data = await req.json();

      this.burgers = data;

      // resgatar status
      this.getStatus();

    },
    async getStatus() {

      const req = await fetch('http://localhost:3000/status')

      const data = await req.json();

      this.status = data;
    },
    async deleteBurger(id) {
      const req = await fetch(`http://localhost:3000/burgers/${id}`, {
        method: "DELETE"
      });

      const res = await req.json();

      // msg do pedido deletado
      this.msg = `Pedido Nº${res.id} removido com sucesso!`
      setTimeout(() => this.msg = "", 3000)

      this.getPedidos();
    },
    async updateBurger(event, id) {
      const option = event.target.value;

      const dataJson = JSON.stringify({ status: option });

      // Acesso ao pedido pelo ID
      // PATCH atualiza apenas a informação selcionada (status)
      const req = await fetch(`http://localhost:3000/burgers/${id}`, {
        method: "PATCH",
        headers: { "Content-Type": "application/json" },
        body: dataJson
      });

      const res = await req.json();

      this.msg = `Pedido Nº${res.id} foi atualizado para ${res.status} !`
      setTimeout(() => this.msg = "", 3000)
    },
  },
  mounted() {
    this.getPedidos();
  }

}
</script>

<style scoped>
#burger-table {
  max-width: 1400px;
  min-height: 700px;
  margin: 0 auto;
  border: 2px solid #c79200;
  padding: 20px;
  border-radius: 15px;
  color: #fff;
}


#burger-table-heading,
#burger-table-rows,
.burger-table-row {
  display: flex;
  flex-wrap: wrap;
  font-weight: bold;
}

#burger-table-heading {
  font-weight: bold;
  padding: 12px;
  border-bottom: 3px solid #333;
}

.burger-table-row {
  width: 100%;
  padding: 12px;
  border: 1.5px solid #c79200;
  border-radius: 10px;
  margin-top: 10px;
}

#burger-table-heading div,
.burger-table-row div {
  width: 19%;
}

#burger-table-heading .order-id,
.burger-table-row .order-number {
  width: 5%;
}

select {
  padding: 10px 8px;
  margin-right: 12px;
  border-radius: 10px;
  border: 1px solid #fcba03;
  cursor: pointer;
  background-color: #434405e0;
  color: #fff;
  font-size: 16px;
}

.delete-btn {
  background-color: #440505e0;
  color: #fff;
  border: 1px solid #fcba03;
  padding: 10px 15px;
  font-size: 16px;
  margin: 0 auto;
  cursor: pointer;
  transition: .5s;
  border-radius: 10px;
}

.delete-btn:hover {
  color: #fff;
  border: 1px solid #fff;
}
</style>