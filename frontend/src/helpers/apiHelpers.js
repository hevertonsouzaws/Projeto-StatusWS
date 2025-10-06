export const getErrorMessage = (error, defaultMessage) => {
  const message = error.response?.data?.message; 

  if (message) {
    return message;
  }
  
  return defaultMessage;
};