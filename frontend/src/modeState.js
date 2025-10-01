import { ref, computed } from 'vue';

const STORAGE_KEY = 'usuario_auth_data';

const loadAuthData = () => {
    const data = localStorage.getItem(STORAGE_KEY);
    return data ? JSON.parse(data) : null;
};

const initialAuthData = loadAuthData();

export const usuarioLogado = ref(initialAuthData);
export const usuarioLogadoId = ref(initialAuthData ? initialAuthData.id : null);

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

const ADMIN_NAMES = ['Heverton Souza', 'Aline Gallo', 'Regis Beraldi'];

export const isAdmin = computed(() => {
    return usuarioLogado.value && ADMIN_NAMES.includes(usuarioLogado.value.name);
});