import api from './axiosConfig';
import { showToast } from '../helpers/toastState';
import { getErrorMessage } from '../helpers/apiHelpers';

export async function getStatusTypes() {
  const defaultMessage = 'Falha ao carregar os tipos de status.';
  try {
    const response = await api.get('/api/StatusType');
    return response.data;
  } catch (error) {
    const errorMessage = getErrorMessage(error, defaultMessage);
    showToast(errorMessage, 'error');
    throw error;
  }
}

export async function createStatusType(statusTypeData) {
  const defaultMessage = 'Erro ao criar tipo de status. Verifique os dados.';
  try {
    const response = await api.post('/api/StatusType', statusTypeData);
    showToast('Tipo de status criado com sucesso!', 'success');
    return response.data;
  } catch (error) {
    const errorMessage = getErrorMessage(error, defaultMessage);
    showToast(errorMessage, 'error');
    throw error;
  }
}

export async function updateStatusType(id, statusTypeData) {
  const defaultMessage = `Erro ao atualizar tipo de status ${id}.`;
  try {
    await api.put(`/api/StatusType/${id}`, statusTypeData);
    showToast(`Tipo de status ${id} atualizado com sucesso!`, 'success');
  } catch (error) {
    const errorMessage = getErrorMessage(error, defaultMessage);
    showToast(errorMessage, 'error');
    throw error;
  }
}

export async function deleteStatusType(id) {
  const defaultMessage = `Erro ao deletar tipo de status ${id}.`;
  try {
    await api.delete(`/api/StatusType/${id}`);
    showToast(`Tipo de status ${id} deletado com sucesso!`, 'success');
  } catch (error) {
    const errorMessage = getErrorMessage(error, defaultMessage);
    showToast(errorMessage, 'error');
    throw error;
  }
}