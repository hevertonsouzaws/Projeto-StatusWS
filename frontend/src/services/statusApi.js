import api from './axiosConfig';

export async function getStatusTypes() {
  try {
    const response = await api.get('/StatusType');
    return response.data;
  } catch (error) {
    console.error('Erro ao buscar tipos de status:', error);
    throw error;
  }
}

export async function createStatusType(statusTypeData) {
  try {
    const response = await api.post('/StatusType', statusTypeData);
    return response.data;
  } catch (error) {
    console.error('Erro ao criar tipo de status:', error);
    throw error;
  }
}

export async function updateStatusType(id, statusTypeData) {
  try {
    await api.put(`/StatusType/${id}`, statusTypeData);
  } catch (error) {
    console.error(`Erro ao atualizar tipo de status ${id}:`, error);
    throw error;
  }
}

export async function deleteStatusType(id) {
  try {
    await api.delete(`/StatusType/${id}`);
  } catch (error) {
    console.error(`Erro ao deletar tipo de status ${id}:`, error);
    throw error;
  }
}