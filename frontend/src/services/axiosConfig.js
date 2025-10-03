import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7208",
   headers: {
    'Content-Type': 'application/json',
    'ngrok-skip-browser-warning': 'true', 
  },
});

export default api;
