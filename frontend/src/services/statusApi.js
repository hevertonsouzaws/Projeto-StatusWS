import api from './axiosConfig';
import { showToast } from '../helpers/toastState'; 

export async function getStatusTypes() {
  try {
    const response = await api.get('/api/StatusType');
    return response.data;
  } catch (error) {
    showToast('Falha ao carregar os tipos de status.', 'error');
    throw error;
  }
}

export async function createStatusType(statusTypeData) {
  try {
    const response = await api.post('/api/StatusType', statusTypeData);
    showToast('Tipo de status criado com sucesso!', 'success');
    return response.data;
  } catch (error) {
    showToast('Erro ao criar tipo de status. Verifique os dados.', 'error');
    throw error;
  }
}

export async function updateStatusType(id, statusTypeData) {
  try {
    await api.put(`/api/StatusType/${id}`, statusTypeData);
    showToast(`Tipo de status ${id} atualizado com sucesso!`, 'success');
  } catch (error) {
    showToast(`Erro ao atualizar tipo de status ${id}.`, 'error');
    throw error;
  }
}

export async function deleteStatusType(id) {
  try {
    await api.delete(`/api/StatusType/${id}`);
    showToast(`Tipo de status ${id} deletado com sucesso!`, 'success');
  } catch (error) {
    showToast(`Erro ao deletar tipo de status ${id}.`, 'error');
    throw error;
  }
}