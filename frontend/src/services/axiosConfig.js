import axios from "axios";

const api = axios.create({
  baseURL: "https://unmildewed-wilburn-obsequent.ngrok-free.dev",
   headers: {
    'Content-Type': 'application/json',
    // <<< ADICIONE ESTE CABEÇALHO PARA PULAR O AVISO DO NGORK
    'ngrok-skip-browser-warning': 'true', 
  },
});

export default api;
