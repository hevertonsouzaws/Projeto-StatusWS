import { reactive } from 'vue';

export const toastState = reactive({
    message: '',
    type: 'success', 
    isVisible: false,
});

export function showToast(message, type = 'success') {
    clearTimeout(toastState.timeoutId);

    toastState.message = message;
    toastState.type = type;
    toastState.isVisible = true;

    toastState.timeoutId = setTimeout(() => {
        toastState.isVisible = false;
    }, 4000); 
}