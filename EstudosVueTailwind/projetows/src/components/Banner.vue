<template>
    <div id="main-banner">
        <h1>Make Your Burger</h1>
    </div>
</template>

<script>
export default {
    name: 'Banner',

}

</script>

<style scoped>
#main-banner{
    background-image: url('/public/img/burger.jpg');
    background-position:  0 -300px;
    background-size: cover;
    background-repeat: no-repeat;
    height: 750px;
    display: flex;
    align-items: center;
    justify-content: flex-start;
}

#main-banner h1{
    color: #fff;
    text-align: center;
    font-size: 80px;
    padding: 20px 40px;
    background-color: #222;
    border-bottom: 2px solid YELLOW;
    border-right: 5px solid yellow;
    ;
}


</style>