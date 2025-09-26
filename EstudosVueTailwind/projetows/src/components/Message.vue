<template>
    <div class="message-container">
        <p>{{ msg }}</p>
    </div>
</template>

<script>
export default {
    name: 'Message',
    props: {
        msg: String
    }
}


</script>

<style acoped>
.message-container {
    color: #004085;
    background-color: #ccffd941;
    border: 2px solid #3cff00;
    padding: 10px;
    border-radius: 10px;
    padding: 10px;
    max-width: 350px;
    margin: 30px auto;
    font-size: 16px;
    transition: 1.s;
}
</style>