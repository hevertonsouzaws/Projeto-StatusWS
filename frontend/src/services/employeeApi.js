import api from './axiosConfig';
import { showToast } from '../helpers/toastState';
import { getErrorMessage } from '../helpers/apiHelpers';

export async function getEmployees() {
  const defaultMessage = 'Falha ao buscar a lista de WS´s.';
  try {
    const response = await api.get('/api/employee');
    return response.data;
  } catch (error) {
    const errorMessage = getErrorMessage(error, defaultMessage);
    showToast(errorMessage, 'error');
    throw error;
  }
}

export async function getInactiveEmployees() {
  const defaultMessage = 'Falha ao buscar a lista de funcionários inativos.';
  try {
    const response = await api.get('/api/Employee/Inactive');
    return response.data;
  } catch (error) {
    const errorMessage = getErrorMessage(error, defaultMessage);
    showToast(errorMessage, 'error');
    throw error;
  }
}

export async function getEmployeeById(id) {
  const defaultMessage = `Falha ao bsucar funcionário ${id}.`;
  try {
    const response = await api.get(`/api/Employee/${id}`);
    return response.data;
  } catch (error) {
    const errorMessage = getErrorMessage(error, defaultMessage);
    showToast(errorMessage, 'error');
    throw error;
  }
}

export async function createEmployee(newEmployee) {
  const defaultMessage = 'Erro ao criar funcionário. Verifique os dados.';
  try {
    const response = await api.post('/api/Employee', newEmployee);
    showToast('Funcionário criado com sucesso!', 'success');
    return response.data;
  } catch (error) {
    const errorMessage = getErrorMessage(error, defaultMessage);
    showToast(errorMessage, 'error');
    throw error;
  }
}

export async function updateEmployee(id, updateData) {
  const defaultMessage = `Erro ao atualizar funcionário ${id}.`;
  try {
    await api.put(`/api/Employee/${id}`, updateData);
    showToast('Funcionário atualizado com sucesso!', 'success');
  } catch (error) {
    const errorMessage = getErrorMessage(error, defaultMessage);
    showToast(errorMessage, 'error');
    throw error;
  }
}

export async function getJiraIssueDetails(jiraKey) {
  const defaultMessage = `Falha ao buscar detalhes do Jira para ${jiraKey}.`;
  try {
    const response = await api.get(`/api/Employee/jira-details/${jiraKey}`);
    return response.data;
  } catch (error) {
    const errorMessage = getErrorMessage(error, defaultMessage);
    showToast(errorMessage, 'error');
    throw error;
  }
}

export async function searchJiraIssues(query) {
  const defaultMessage = `Falha ao buscar tarefas do Jira para ${query}.`;
  try {
    const response = await api.get(`/api/Employee/jira-search?q=${query}`);
    return response.data;
  } catch (error) {
    const errorMessage = getErrorMessage(error, defaultMessage);
    showToast(errorMessage, 'error');
    return [];
  }
}

export async function authenticateLogin(employeeId, password) {
  const loginDto = {
    employeeId: employeeId,
    password: password
  };
  const defaultMessage = 'Falha na comunicação com o servidor de autenticação.';

  try {
    const response = await api.post('/api/Employee/login', loginDto);
    return response.data;
  } catch (error) {
    const errorMessage = getErrorMessage(error, defaultMessage);

    if (error.response && error.response.status === 401) {
      throw new Error(errorMessage);
    }

    showToast(errorMessage, 'error');
    throw new Error(errorMessage);
  }
}