const employeesData = [
  {
    employeeId: 1,
    name: 'Heverton Souza',
    position: 'Desenvolvedor',
    photo: 'https://media.licdn.com/dms/image/v2/D4D03AQHNCUKIxaNiAQ/profile-displayphoto-scale_100_100/B4DZlDytuPG8Ac-/0/1757778995556?e=1761177600&v=beta&t=Y1g8rZbICkIX1KjKfLRp1h5EOifIke_fnW9b1h0l35k',
    isActive: true,
    status: {
      customText: 'Projeto Status WS',
      updateAt: new Date().toISOString(),
      statusType: {
        statusTypeId: 2,
        description: 'Codando',
        iconUrl: 'https://cdn-icons-gif.flaticon.com/8722/8722699.gif'
      }
    }
  },
  {
    employeeId: 2,
    name: 'Rafaela Nascimento',
    position: 'Desenvolvedor',
    photo: 'https://media.licdn.com/dms/image/v2/D4D03AQGOlDiOrstV0w/profile-displayphoto-shrink_200_200/B4DZcjF6rkGUAY-/0/1748640445474?e=1761177600&v=beta&t=taw7ziI8lszfM1B8BsIYV3BBvUQV4vV69mnnmCinc7s',
    isActive: true,
    status: {
      customText: 'Projeto Crespusculo',
      updateAt: new Date().toISOString(),
      statusType: {
        statusTypeId: 2,
        description: 'Codando',
        iconUrl: 'https://cdn-icons-gif.flaticon.com/8722/8722699.gif'
      }
    }
  },
  {
    employeeId: 3,
    name: 'Aline Gallo',
    position: 'Desenvolvedora',
    photo: 'https://media.licdn.com/dms/image/v2/D4D03AQHuUM6gwwRNNw/profile-displayphoto-shrink_200_200/B4DZanYy4FG8AY-/0/1746565019342?e=1761782400&v=beta&t=jmUdM0wmqYHOdqiG--zi53NDTy_T34tEO81deCVUhe8',
    isActive: true,
    status: {
      customText: 'Alinhamento Conect',
      updateAt: new Date().toISOString(),
      statusType: {
        statusTypeId: 3,
        description: 'Reunião',
        iconUrl: 'https://cdn-icons-gif.flaticon.com/10690/10690276.gif'
      }
    }
  }
];

export const getEmployees = () => {
  return Promise.resolve(employeesData);
};

const generateNewId = () => {
  const ids = employeesData.map(e => e.employeeId);
  return ids.length > 0 ? Math.max(...ids) + 1 : 1;
};

export const mockService = {
  getEmployees: async () => {
    return new Promise(resolve => setTimeout(() => resolve(employeesData), 500));
  },
  getEmployeeById: async (id) => {
    return new Promise(resolve => {
      const employee = employeesData.find(emp => emp.employeeId === id);
      resolve(employee || null); 
    });
  },
  addEmployee: async (employeeData) => {
    return new Promise(resolve => {
      const newEmployee = { ...employeeData, employeeId: generateNewId() };
      employeesData.push(newEmployee);
      resolve(newEmployee);
    });
  },
  updateEmployee: async (updatedData) => {
    return new Promise(resolve => {
      const index = employeesData.findIndex(e => e.employeeId === updatedData.employeeId);
      if (index !== -1) {
        employeesData[index] = updatedData;
      }
      resolve(updatedData);
    });
  }
};