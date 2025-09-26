<template>
    <div>
        <button @click="primeiro($event), segundo($event)">Ativar múltipos eventos</button>
        <p>ola: {{ multiplos_eventos }}</p>
    </div>
</template>

<script>
export default {
    name: 'MultiplosEventos',
    data() {
        return {
            multiplos_eventos: "VOU MUDAR AQUI!"
        }
    },
    methods: {
        primeiro(e) {
            this.multiplos_eventos = "#SOMOSWS"
        },
        segundo(e) {
            setTimeout(() => this.multiplos_eventos = "WebSupply", 1000)
        }
    }
} 
</script>

<style scoped>
div {
    margin: 150px;
}

</style>

