import axios from 'axios';

const BASE_URL = 'http://localhost:5089/api/registrations';

export const register = async (eventId, data) => {
  try {
    console.log(`Registering for event ${eventId} with data:`, data);
    const response = await axios.post(`${BASE_URL}/events/${eventId}`, {
      name: data.name,
      email: data.email
    });
    console.log('Registration response:', response);
    return response.data;
  } catch (error) {
    console.error('Error registering for event:', error);
    if (error.response) {
      console.error('Error response:', error.response.data);
      if (error.response.status === 409) {
        throw new Error('You are already registered for this event.');
      } else if (error.response.status === 400 && error.response.data.includes('maximum capacity')) {
        throw new Error('This event has reached maximum capacity.');
      } else {
        throw new Error(error.response.data || 'Registration failed. Please try again.');
      }
    } else if (error.request) {
      console.error('No response received:', error.request);
      throw new Error('No response from server. Please check your connection.');
    } else {
      throw error;
    }
  }
}; 