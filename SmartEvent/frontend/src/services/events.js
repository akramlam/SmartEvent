import axios from 'axios';

const API_URL = 'http://localhost:5089/api/events';

export const getEvents = async () => {
  try {
    console.log('Fetching events from:', API_URL);
    const response = await axios.get(API_URL);
    console.log('API Response:', response);
    return response.data;
  } catch (error) {
    console.error('Error details:', error);
    if (error.response) {
      console.error('Response data:', error.response.data);
      console.error('Response status:', error.response.status);
      throw new Error(error.response.data || 'Failed to fetch events from server');
    } else if (error.request) {
      console.error('No response received:', error.request);
      throw new Error('No response from server. Please check your connection.');
    } else {
      console.error('Error setting up request:', error.message);
      throw error;
    }
  }
};

export const getEventById = async (id) => {
  try {
    console.log(`Fetching event with id ${id} from: ${API_URL}/${id}`);
    const response = await axios.get(`${API_URL}/${id}`);
    console.log('API Response for single event:', response);
    return response.data;
  } catch (error) {
    console.error(`Error fetching event with id ${id}:`, error);
    if (error.response) {
      throw new Error(error.response.data || `Failed to fetch event with ID ${id}`);
    } else if (error.request) {
      throw new Error('No response from server. Please check your connection.');
    } else {
      throw error;
    }
  }
};

export const createEvent = async (eventData) => {
  try {
    console.log('Creating event with data:', eventData);
    const response = await axios.post(API_URL, eventData);
    console.log('Create event response:', response);
    return response.data;
  } catch (error) {
    console.error('Error creating event:', error);
    if (error.response) {
      console.error('Response data:', error.response.data);
      throw new Error(error.response.data || 'Failed to create event');
    } else if (error.request) {
      console.error('No response received:', error.request);
      throw new Error('No response from server. Please check your connection.');
    } else {
      throw error;
    }
  }
};

export const updateEvent = async (id, eventData) => {
  try {
    console.log(`Updating event with id ${id} with data:`, eventData);
    const response = await axios.put(`${API_URL}/${id}`, eventData);
    console.log('Update event response:', response);
    return response.data;
  } catch (error) {
    console.error(`Error updating event with id ${id}:`, error);
    if (error.response) {
      console.error('Response data:', error.response.data);
      throw new Error(error.response.data || 'Failed to update event');
    } else if (error.request) {
      console.error('No response received:', error.request);
      throw new Error('No response from server. Please check your connection.');
    } else {
      throw error;
    }
  }
};

export const deleteEvent = async (id) => {
  try {
    console.log(`Deleting event with id ${id}`);
    const response = await axios.delete(`${API_URL}/${id}`);
    console.log('Delete event response:', response);
    return response.data;
  } catch (error) {
    console.error(`Error deleting event with id ${id}:`, error);
    if (error.response) {
      console.error('Response data:', error.response.data);
      throw new Error(error.response.data || 'Failed to delete event');
    } else if (error.request) {
      console.error('No response received:', error.request);
      throw new Error('No response from server. Please check your connection.');
    } else {
      throw error;
    }
  }
}; 