<template>

    <div class="container-form">
        <Message :msg="msg" v-show="msg" />
        <h1>Monte seu Burger</h1>
        <form id="burger-form" method="POST" @submit="createBurger">
            <div class="input-container">
                <label for="nome">Nome do cliente</label>
                <input type="text" id="nome" name="nome" v-model="nome" placeholder="Digite o seu nome">
            </div>

            <div class="input-container">
                <label for="pao">Escolha o pão:</label>
                <select name="pao" id="pao" v-model="pao">
                    <option v-for="pao in paes" :key="pao.id" :value="pao.tipo">{{ pao.tipo }}</option>
                </select>
            </div>

            <div class="input-container">
                <label for="carne">Escolha a carne do seu Burger:</label>
                <select name="carne" id="carne" v-model="carne">
                    <option v-for="carne in carnes" :key="carne.id" :value="carne.tipo">{{ carne.tipo }}
                    </option>
                </select>
            </div>

            <div id="opcionais-container" class="input-container">
                <label id="opcionais-title" for="opcionais">Selecione os opcionais:</label>
                <div class="checkbox-container" v-for="opcional in opcionaisdata" :key="opcional.id">
                    <input type="checkbox" name="opcionais" v-model="opcionais" :value="opcional.tipo">
                    <span>{{ opcional.tipo }}</span>
                </div>
            </div>

            <div class="input-container">
                <input type="submit" class="submit-btn" value="Criar meu Burguer!">
            </div>

        </form>
        <div id="btn-container">
            <button class="pedidos-btn">
                <a href="http://localhost:8080/pedidos">
                    <p>Ver Pedidos</p>
                </a>
            </button>
        </div>
    </div>
</template>

<script>

import Message from './Message.vue';

export default {
    name: 'BurgerForm',
    data() {
        return {
            paes: null,
            carnes: null,
            opcionaisdata: null,
            nome: null,
            pao: null,
            carne: null,
            opcionais: [],
            status: "Solicitado",
            msg: null
        }
    },

    methods: {
        async getIngredientes() {
            const req = await fetch('http://localhost:3000/ingredientes');
            const data = await req.json();

            this.paes = data.paes;
            this.carnes = data.carnes;
            this.opcionaisdata = data.opcionais
        },

        async createBurger(e) {

            e.preventDefault()

            const data = {
                nome: this.nome,
                carne: this.carne,
                pao: this.pao,
                opcionais: Array.from(this.opcionais),
                status: "Solicitado"
            }

            const dataJson = JSON.stringify(data)

            const req = await fetch("http://localhost:3000/burgers", {
                method: "POST",
                headers: { "Content-Type" : "application/json" },
                body: dataJson
            });

            const res = await req.json()

            console.log(res)

            //msg de retorno
            this.msg = `Pedido Nº${res.id} realizado com sucesso!`

            //limpar msg
            setTimeout(() => this.msg = "", 3000)

            // limpar campos
            this.nome = "";
            this.carne = "";
            this.pao = "";
            this.opcionais = []
        }

    },

    mounted() {
        this.getIngredientes();
    },

    components: {
        Message
    }

}
</script>

<style scoped>
.container-form {
    width: 100%;
    border: 1px solid;
    margin: auto;
    padding: 20px 20px;
    border-top: 10px solid #fcba03;
}

h1 {
    color: #fcba03;
}

#burger-form {
    max-width: 380px;
    margin: 10px auto;
    border: 2px solid #fcba03;
    padding: 20px;
    border-radius: 25px;
    color: #fff;
    ;

}

.input-container {
    display: flex;
    flex-direction: row;
    margin-bottom: 20px;
    flex-wrap: wrap;
}

label {
    font-weight: bold;
    margin-bottom: 15px;
    padding: 5px 10px;
    border-left: 4px solid #fcba03;
}

input,
select,
.pedidos-btn {
    padding: 8px 10px;
    width: 330px;
    background-color: #fcba0313;
    border-radius: 8px;
    color: #fff;
    border: 1px solid #fff;
}

select option {
    background-color: #222;
}

input:hover,
select:hover {
    border: 1px solid #fcba03;
}

#opcionais-container {
    flex-direction: row;
    flex-wrap: wrap;
}

#opcionais-title {
    width: 100%;
}

.checkbox-container {
    display: flex;
    align-items: flex-start;
    width: 50%;
    margin-bottom: 20px;
}

.checkbox-container span,
.checkbox-container input {
    width: auto;
}

.checkbox-container span {
    margin-left: 6px;
    font-weight: bold;
}

.submit-btn {
    background-color: #fcba03;
    border: 2px solid #fcba03;
    color: #222;
    font-weight: bold;
    padding: 10px;
    font-size: 16px;
    margin: 0 auto;
    border-radius: 10px;
    cursor: pointer;
}

.submit-btn:hover {
    background-color: transparent;
    color: #fcba03;
    transition: .5s;
    border: 2px solid #fcba03;
}

#btn-container {
    text-align: center;
}

.pedidos-btn {
    background-color: #222;
    margin: 12px;
    transition: .5s;
    font-size: 18px;
    border: 1px solid #FCBA03;
    border-radius: 15px;
    padding: 10px 20px;
}

.pedidos-btn a {
    text-decoration: none;
    color: #222;
    color: #FFF;
}

.pedidos-btn:hover,
.pedidos-btn a:hover {
    background-color: #fcba0334;
    transition: .5s;
    border: 1px solid #fcba03;
}
</style>