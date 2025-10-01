import api from './axiosConfig';
import { showToast } from '../helpers/toastState'; 

export async function getEmployees() {
  try {
    const response = await api.get('/api/employee');
    return response.data;
  } catch (error) {
    showToast('Falha ao buscar a lista de funcionários.', 'error');
    throw error;
  }
}

export async function getInactiveEmployees() {
  try {
    const response = await api.get('/api/Employee/Inactive');
    return response.data;
  } catch (error) {
    showToast('Falha ao buscar a lista de funcionários inativos.', 'error');
    throw error;
  }
}

export async function getEmployeeById(id) {
  try {
    const response = await api.get(`/api/Employee/${id}`);
    return response.data;
  } catch (error) {
    showToast(`Falha ao buscar funcionário ${id}.`, 'error');
    throw error;
  }
}

export async function createEmployee(newEmployee) {
  try {
    const response = await api.post('/api/Employee', newEmployee);
    showToast('Funcionário criado com sucesso!', 'success');
    return response.data;
  } catch (error) {
    showToast('Erro ao criar funcionário. Verifique os dados.', 'error');
    throw error;
  }
}

export async function updateEmployee(id, updateData) {
  try {
    await api.put(`/api/Employee/${id}`, updateData);
    showToast('Funcionário atualizado com sucesso!', 'success');
  } catch (error) {
    showToast(`Erro ao atualizar funcionário ${id}.`, 'error');
    throw error;
  }
}

export async function getJiraIssueDetails(jiraKey) {
  try {
    const response = await api.get(`/api/Employee/jira-details/${jiraKey}`);
    return response.data;
  } catch (error) {
    showToast(`Falha ao buscar detalhes do Jira para ${jiraKey}.`, 'error');
    throw error;
  }
}

export async function searchJiraIssues(query) {
  try {
    const response = await api.get(`/api/Employee/jira-search?q=${query}`);
    return response.data;
  } catch (error) {
    showToast(`Falha ao buscar tarefas do Jira para ${query}.`, 'error');
    return [];
  }
}

export async function authenticateLogin(employeeId, password) {
  const loginDto = {
    employeeId: employeeId,
    password: password
  };

  try {
    const response = await api.post('/api/Employee/login', loginDto);
    return response.data;

  } catch (error) {
    if (error.response && error.response.status === 401) {
      throw new Error('Senha incorreta ou perfil inválido.');
    }

    showToast('Falha na comunicação com o servidor de autenticação.', 'error');
    throw new Error('Falha na comunicação com o servidor de autenticação.');
  }
}