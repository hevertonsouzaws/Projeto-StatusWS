/* 1 - get fetch

fetch('https://jsonplaceholder.typicode.com/todos')
    .then((response) => response.json())
    .then((data) => {
        console.log('Retorno do FETCH:')
        console.log(data)
    }).catch((error) => {
        console.log(error)
    })


// 2 - get com axios
axios
    .get("https://jsonplaceholder.typicode.com/todos")
    .then((response) => {
        console.log('Retorno do AXIOS')
        console.log(response.data)
    }).catch((error) => {
        console.log(error);
    })

// 3 - POST com fetch

const data = {
    title: "#SomosWS",
    content: "POST com fetch",
    userId: 1
}

fetch('https://jsonplaceholder.typicode.com/posts', {
    method: "POST",
    headers: {
        "Content-Type": "application/json" // tipo de contéudo 
    },
    body: JSON.stringify(data),  //converte para Json    
})
    .then((response) => response.json())
    .then((data) => {
        console.log('Retorno POST do FETCH:')
        console.log(data)
    }).catch((error) => {
        console.log(error)
    })


// 4 - POST com axios

axios.post('https://jsonplaceholder.typicode.com/posts', data)
    .then((response) => {
        console.log('Retorno POST do AXIOS')
        console.log(response.data)
    }).catch((error) => {
        console.log(error);
    })
*/

/* Teste api beta do projeto 

axios
    .get("https://localhost:7166/api/Employees/")
    .then((response) => {
        console.log('Retorno PROJETOWS do AXIOS')
        console.log(response.data)
    }).catch((error) => {
        console.log(error);
    })


const employee = {
    name: "Index axios",
    position: "Post com axios"
}

axios
    .post('https://localhost:7166/api/Employees/', employee)
    .then((response) => {
        console.log('Retorno POST no projeto')
        console.log(response.employee)
    }).catch((error) => {
        console.log(error);
    })
*/

axios
    .get("https://localhost:7208/api/statustype/")
    .then((response) => {
        console.log('Retorno StatusWS do AXIOS')
        console.log(response.data)
    }).catch((error) => {
        console.log(error);
    })

// Renderizando no html 

axios.get("https://localhost:7208/api/statustype/")
    .then((response) => {
        const statusContainer = document.getElementById('status-container');
        const statusData = response.data;
        statusContainer.innerHTML = '';

        statusData.forEach(status => {
            const statusItem = document.createElement('div');
            statusItem.classList.add('status-item');

            const statusIcon = document.createElement('img');
            statusIcon.src = status.iconUrl;
            statusIcon.alt = status.description;

            const statusDescription = document.createElement('p');
            statusDescription.textContent = status.description;

            statusItem.appendChild(statusIcon);
            statusItem.appendChild(statusDescription);
            statusContainer.appendChild(statusItem);
        });
    })
    .catch((error) => {
        console.error("erro ao buscar os dados da API:", error);
        document.getElementById('status-container').innerHTML = '<p style="color: red;">API desligada. Não foi possível carregar os status.</p>';
    });

