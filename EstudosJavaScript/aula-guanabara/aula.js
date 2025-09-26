function mudarTexto() {
    var ws = document.getElementById('t1')
    ws.innerText = 'Unidos pela inovação'

    var slogan1 = document.getElementById('t2')
    slogan1.innerText = 'Impulsionando o futuro!'

    var slogan2 = document.getElementById('t3')
    slogan2.innerText = 'Websupply'
    slogan2.style.textAlign = "center"

    return;
}

function desfazer() {
    var ws = document.getElementById('t1')
    ws.innerText = 'Websupply '

    var slogan1 = document.getElementById('t2')
    slogan1.innerText = 'Unidos pela inovação'

    var slogan2 = document.getElementById('t3')
    slogan2.innerText = 'Impulsionando o futuro!'
}

var area = document.getElementById('area')

function clicar() {
    area.innerText = 'clicou!'
}
function entrar() {
    area.innerText = 'entrou'
}
function sair() {
    area.innerText = 'saiu'
}

function somar() {
    var tn1 = document.getElementById('txtn1')
    var tn2 = document.querySelector('input#txtn2')
    var resultado = document.getElementById('resultado')
    var n1 = Number(tn1.value)
    var n2 = Number(tn2.value)
    var soma = n1 + n2
    resultado.innerHTML = soma
}

function carregar() {
    var msg = document.getElementById('msg')
    var img = document.getElementById('foto')

    var data = new Date
    var hora = data.getHours()
    msg.innerHTML = `Horário:  ${hora}H`

    if (hora >= 4 && hora < 12) {
        img.src = 'imagens/sol.jpg'
    } else if (hora >= 12 && hora <= 18) {
        img.src = 'imagens/tarde.jpg'
    } else {
        img.src = 'imagens/lua.jpg'
    }

}