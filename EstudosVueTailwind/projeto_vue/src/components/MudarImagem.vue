<template>
    <div>
        <button @click="$emit('mudarImagem')">Mudar Conta</button>
    </div>
</template>

<script>
export default {
    name: 'MudarImagem',
    emits: ['mudarImagem']
}

</script>

<style scoped>
button {
    padding: 10px;
    background-color: transparent;
    border: 1px solid red;
    color: aliceblue;
    border-radius: 20px;
    font-size: 14px;
}


</style>