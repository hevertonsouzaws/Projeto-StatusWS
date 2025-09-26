<template>
    <div id="forms">
        <h2>Meu formalário:</h2>
        <form action="" @submit="enviarFormulario($event)">
            <div>
                <input type="text" v-model="name">
            </div>

            <div>
                <input type="text" v-model="email"></input>
            </div>

            <div>
                <Submit />
            </div>

        </form>
    </div>
</template>

<script>
import InputText from './form/InputText.vue';
import Submit from './form/Submit.vue';

export default {
    name: 'Form',
    components: {
        InputText,
        Submit
    },
    data() {
        return {
            name: "",
            email: "",
        }
    },
    methods: {
        enviarFormulario(e) {
            e.preventDefault();

            const name = this.name;
            const email = this.email;

            console.log("Formulário enviado!")
            console.log("O nome é: " + name)
            console.log("O email é: " + email)

            // Ajax ou Axios 

            // Inserir no Banco de dados 
        }
    }
}
</script>

<style scoped>
form input {
    background-color: rgb(235, 235, 253);
    color: black;
    border: none;
    padding: 8px;
    border-radius: 5px;
    margin-top: 10px;
}
</style>