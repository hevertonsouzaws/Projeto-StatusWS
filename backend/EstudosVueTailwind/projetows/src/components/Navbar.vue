<template>
    <div id="nav">
        <div>
            <router-link class="router" to="/">Home</router-link>
        </div>
        <div id="logo">
            <router-link to="/" id="logo-url">
                <img :src="logo" :alt="alt">
                <p>BURGER WS</p>
            </router-link>
        </div>
        <div>
            <router-link class="router" to="/pedidos">Pedidos</router-link>
        </div>
    </div>
</template>

<script>
export default {
    name: 'Navbar',
    props: ["logo", "alt"]
}
</script>

<style scoped>
#nav {
    background-color: #222;
    border-bottom: 4px solid #FCBA03;
    padding: 15px 20px;
    display: flex;
    align-items: center;
    justify-content: center;
}

#nav div {
    padding: 0 80px;
}

#logo {
    text-align: center;
}

#logo p {
    color: #FCBA03;
    font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
    font-size: 34px;
}

#logo img {
    width: 80px;
    height: 80px;
    text-align: center;
}

#nav a {
    text-decoration: none;
}

.router {
    color: #FFF;
    margin: 12px;
    transition: .5s;
    font-size: 18px;
    border: 1px solid #FCBA03;
    border-radius: 15px;
    padding: 10px 20px;
}


.router:hover {
    border: 1px solid #fc7303;
    border-radius: 15px;
    padding: 10px 20px;
    transition: .5s;
}
</style>