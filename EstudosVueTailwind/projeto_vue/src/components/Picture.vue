<template>
    <img :src="avatar">
  
</template>

<script>
export default {
    name: 'Picture',
    
    components: {

    }, 

    data() {
        return {
            avatar: "/img/logows.jpg", 
            descricao: "logo websupply"

        }
    },
}

</script>