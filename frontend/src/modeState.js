import { ref } from 'vue';

const STORAGE_KEY = 'usuario_auth_data'; 
const MOCK_MODE_KEY = 'is_mock_mode';

const loadAuthData = () => {
    const data = localStorage.getItem(STORAGE_KEY);
    return data ? JSON.parse(data) : null;
};

export const isMockMode = ref(localStorage.getItem(MOCK_MODE_KEY) === 'true');

const initialAuthData = loadAuthData();

export const usuarioLogado = ref(initialAuthData);

export const usuarioLogadoId = ref(initialAuthData ? initialAuthData.EmployeeId : null);

export const setUsuarioLogado = (userData) => {
    if (userData && (userData.id || userData.employeeId)) {
        const id = userData.id || userData.employeeId;
        const normalizedUserData = { ...userData, id: id };
        
        usuarioLogado.value = normalizedUserData;
        usuarioLogadoId.value = id;
        localStorage.setItem(STORAGE_KEY, JSON.stringify(normalizedUserData));
    } else {
        usuarioLogado.value = null;
        usuarioLogadoId.value = null;
        localStorage.removeItem(STORAGE_KEY);
    }
};

export const logoutUsuario = () => {
    setUsuarioLogado(null);
};

export function toggleMode() {
    isMockMode.value = !isMockMode.value;
    localStorage.setItem(MOCK_MODE_KEY, isMockMode.value);
}