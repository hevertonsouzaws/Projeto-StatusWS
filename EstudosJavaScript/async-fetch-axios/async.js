/*sintaxe

function primeiraFuncao() {
   
    return new Promise((resolve) => {
         setTimeout(() => {
            console.log("Esperou isso aqui")
            resolve()
         },1000)
    })
}

async function segundaFuncao() {
    console.log("Iniciou")

    await primeiraFuncao()

    console.log("Terminou")
}

segundaFuncao()
*/

// API 

/*
function getUser(username) {
    return fetch(`https://api.github.com/users/${username}`)
        .then((data) => data.json())
        .catch((err) => console.log(err))
}

async function mostrarNomeUsuario(username) {

    try {
        const user = await getUser(username)
        console.log(`O nome do usuário é: ${user.name}`)
    } catch (err) {
        console.log(err)
    }

    console.log()
}

mostrarNomeUsuario('octocat') */

/*
const URL = 'https://dummyjson.com/products'

async function chamarApi() {
    const resp = await fetch(URL);
    console.log(resp)
    if(resp.status === 200) {
        const obj = await resp.json()
        console.log(obj)
    }
}

chamarApi() */