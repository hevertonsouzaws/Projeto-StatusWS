<template>
    <header>
        <div id="logows">
            <a :href="link_sitews" target="_blank">
                <img :src="logowebsupply" alt="logo WS" target="_blank">
            </a>
        </div>
        <nav>
            <div>
                <li>
                    <a :href="link_sitews" target="_blank">WebSupply</a>
                </li>
            </div>
            <div>
                <li v-if="esta_logado">
                    <a :href="link_tarefas" target="_blank">Tarefas</a>
                </li>
            </div>
            <div>
                <li v-if="esta_logado">
                    <a :href="link_tarefas" target="_blank">Portal</a>
                </li>
            </div>
            <div>
                <li>
                    <a :href="link_linkedin" target="_blank">LinkedIn</a>
                </li>
            </div>
            <div id="user">
                <div>

                    <p>#SomosWS <br/> {{ username }}</p>
                </div>
                <div>
                    <img :src="avatar" alt="">
                </div>
                <MudarImagem @mudar-imagem="trocarImagem"/>
            </div>
        </nav>
    </header>
</template>

<script>
import MudarImagem from './MudarImagem.vue';

export default {
    name: 'Header',
    props: ["esta_logado"],

    components: {
        MudarImagem
    },

    data() {
        return {
            logowebsupply: '/img/logo-websupply-new.svg',
            avatar: '/img/heverton.png',
            username: 'Heverton Souza',
            link_sitews: 'https://www.websupply.com.br/',
            link_tarefas: 'https://tarefas.websupply.com.br/painel/',
            link_linkedin: 'https://www.linkedin.com/company/websupply/',
        }
    },

    methods: {
        trocarImagem() {
            this.avatar = '/img/regis.png',
            this.username = 'Regis Beraldi'
        },
    }
}
</script>

<style scoped>
header {
    width: 100%;
    height: 150px;
    text-align: center;
}

#logows {
    height: 110px;
    border-bottom: 1px solid rgb(5, 77, 185);
}

#logows:hover {
    border-bottom: 1px solid #fff;
}

#logows img {
    width: 400px;
    height: 100px;
}

nav {
    height: 50px;
    display: flex;
    flex-direction: row;
    align-items: center;
    justify-content: space-between;
    font-size: 16px;
}

li,
ul,
a {
    text-decoration: none;
}

nav a:hover {
    border-bottom: 2px solid red;
    font-size: 18px;
}

#user {
    padding: 0px;
    height: 60px;
    display: flex;
    align-items: center;
    border: 2px solid rgba(255, 0, 0, 0.178);
    border-radius: 30px;
    padding: 0px;
    font-size: 16px;
    text-align: left;
}

#user img {
    width: 50px;
    height: 50px;
    border-radius: 50%;
    padding: 0px;
}
</style>