import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { createEvent } from '../services/events';

const CreateEvent = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [formData, setFormData] = useState({
    title: '',
    description: '',
    location: '',
    startDate: '',
    endDate: '',
    maxAttendees: 50,
    isPublic: true,
    organizerId: 'user-1-guid' // Default organizer - in a real app, you'd get this from auth
  });

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData({
      ...formData,
      [name]: type === 'checkbox' ? checked : value
    });
  };

  const handleNumberChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: parseInt(value, 10)
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError(null);
    
    try {
      // Validate dates
      const startDate = new Date(formData.startDate);
      const endDate = new Date(formData.endDate);
      
      if (endDate < startDate) {
        throw new Error('La date de fin ne peut pas être antérieure à la date de début');
      }
      
      console.log('Creating event with data:', formData);
      const newEvent = await createEvent(formData);
      console.log('Created event:', newEvent);
      
      // Navigate to the new event page
      navigate(`/events/${newEvent.id}`);
    } catch (err) {
      console.error('Error creating event:', err);
      setError(err.message || 'Échec de la création de l\'événement. Veuillez réessayer.');
      setLoading(false);
    }
  };

  return (
    <div className="create-event-container">
      <h2 className="page-title">Créer un nouvel événement</h2>
      
      {error && (
        <div className="error">
          <p>{error}</p>
        </div>
      )}
      
      <form onSubmit={handleSubmit} className="create-event-form">
        <div className="form-group">
          <label htmlFor="title">Titre de l'événement</label>
          <input
            type="text"
            id="title"
            name="title"
            value={formData.title}
            onChange={handleChange}
            placeholder="Entrez le titre de l'événement"
            required
          />
        </div>
        
        <div className="form-group">
          <label htmlFor="description">Description</label>
          <textarea
            id="description"
            name="description"
            value={formData.description}
            onChange={handleChange}
            placeholder="Décrivez votre événement"
            rows="4"
            required
            className="form-textarea"
          />
        </div>
        
        <div className="form-row">
          <div className="form-group">
            <label htmlFor="startDate">Date et heure de début</label>
            <input
              type="datetime-local"
              id="startDate"
              name="startDate"
              value={formData.startDate}
              onChange={handleChange}
              required
            />
          </div>
          
          <div className="form-group">
            <label htmlFor="endDate">Date et heure de fin</label>
            <input
              type="datetime-local"
              id="endDate"
              name="endDate"
              value={formData.endDate}
              onChange={handleChange}
              required
            />
          </div>
        </div>
        
        <div className="form-group">
          <label htmlFor="location">Lieu</label>
          <input
            type="text"
            id="location"
            name="location"
            value={formData.location}
            onChange={handleChange}
            placeholder="Entrez le lieu de l'événement"
            required
          />
        </div>
        
        <div className="form-group">
          <label htmlFor="maxAttendees">Nombre maximum de participants</label>
          <input
            type="number"
            id="maxAttendees"
            name="maxAttendees"
            value={formData.maxAttendees}
            onChange={handleNumberChange}
            min="1"
            max="1000"
            required
          />
        </div>
        
        <div className="form-group checkbox-group">
          <input
            type="checkbox"
            id="isPublic"
            name="isPublic"
            checked={formData.isPublic}
            onChange={handleChange}
          />
          <label htmlFor="isPublic">Rendre cet événement public</label>
        </div>
        
        <div className="form-actions">
          <button type="submit" className="btn btn-primary" disabled={loading}>
            {loading ? 'Création en cours...' : 'Créer l\'événement'}
          </button>
          <Link to="/events" className="btn btn-secondary">
            Annuler
          </Link>
        </div>
      </form>
    </div>
  );
};

export default CreateEvent; 