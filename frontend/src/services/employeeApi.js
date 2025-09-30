import api from './axiosConfig';
import { mockService } from './mockService';
import { isMockMode } from '../modeState.js';

export async function getEmployees() {
  if (isMockMode.value) {
    const allEmployees = await mockService.getEmployees();
    return allEmployees.filter(e => e.isActive);
  }

  try {
    const response = await api.get('/api/employee');
    return response.data;
  } catch (error) {
    console.error('Erro ao buscar funcionários:', error);
    throw error;
  }
}

export async function getInactiveEmployees() {
  if (isMockMode.value) {
    const allEmployees = await mockService.getEmployees();
    return allEmployees.filter(e => !e.isActive);
  }
  
  try {
    const response = await api.get('/Employee/Inactive');
    return response.data;
  } catch (error) {
    console.error('Erro ao buscar funcionários inativos:', error);
    throw error;
  }
}

export async function getEmployeeById(id) {
  if (isMockMode.value) {
    return mockService.getEmployeeById(id);
  }

  try {
    const response = await api.get(`/Employee/${id}`);
    return response.data;
  } catch (error) {
    console.error(`Erro ao buscar funcionário ${id}:`, error);
    throw error;
  }
}

export async function createEmployee(newEmployee) {
  if (isMockMode.value) {
    return mockService.addEmployee(newEmployee);
  }
  
  try {
    const response = await api.post('/Employee', newEmployee);
    return response.data;
  } catch (error) {
    console.error('Erro ao criar funcionário:', error);
    throw error;
  }
}

export async function updateEmployee(id, updateData) {
  if (isMockMode.value) {
    return mockService.updateEmployee(updateData);
  }
  
  try {
    await api.put(`/Employee/${id}`, updateData);
  } catch (error) {
    console.error(`Erro ao atualizar funcionário ${id}:`, error);
    throw error;
  }
}

// SERVIÇO PARA BUSCAR DETALHES DA TAREFA JIRA
export async function getJiraIssueDetails(jiraKey) {
  if (isMockMode.value) {
    return {
      key: jiraKey,
      summary: `[MOCK] ${jiraKey} - Título Simulado`,
      description: 'Esta é a descrição detalhada da tarefa Jira, vinda do mock service.',
    };
  }
  
  try {
    const response = await api.get(`/Employee/jira-details/${jiraKey}`);
    return response.data;
  } catch (error) {
    console.error(`Erro ao buscar detalhes do Jira para ${jiraKey}:`, error);
    throw error;
  }
}

export async function searchJiraIssues(query) {
  if (isMockMode.value) {
    return [
      { key: 'MOCK-1', summary: 'Tarefa de Exemplo 1', description: 'Descrição da Tarefa 1' },
      { key: 'MOCK-2', summary: 'Tarefa de Exemplo 2', description: 'Descrição da Tarefa 2' },
    ];
  }

  try {
    const response = await api.get(`/Employee/jira-search?q=${query}`);
    return response.data;
  } catch (error) {
    console.error(`Erro ao buscar tarefas do Jira para ${query}:`, error);
    return [];
  }
}

export async function authenticateLogin(employeeId, password) {
    if (isMockMode.value) {
        return mockService.authenticateLogin(employeeId, password);
    }

    const loginDto = {
        employeeId: employeeId,
        password: password
    };

    try {
        const response = await api.post('/Employee/login', loginDto); 
        return response.data; 

    } catch (error) {
        if (error.response && error.response.status === 401) {
            throw new Error('Senha incorreta ou perfil inválido.'); 
        }
        
        console.error('Erro de autenticação:', error);
        throw new Error('Falha na comunicação com o servidor de autenticação.');
    }
}
