import React, { useState, useEffect } from 'react';
import { useNavigate, Link, useParams } from 'react-router-dom';
import { getEventById, updateEvent } from '../services/events';

const EditEvent = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [loading, setLoading] = useState(true);
  const [saveLoading, setSaveLoading] = useState(false);
  const [error, setError] = useState(null);
  const [formData, setFormData] = useState({
    title: '',
    description: '',
    location: '',
    startDate: '',
    endDate: '',
    maxParticipants: 50,
    isPublic: true,
    organizerId: '' 
  });

  useEffect(() => {
    const fetchEvent = async () => {
      try {
        const data = await getEventById(id);
        
        const formatDateForInput = (dateString) => {
          const date = new Date(dateString);
          return date.toISOString().slice(0, 16);
        };
        
        setFormData({
          title: data.title || '',
          description: data.description || '',
          location: data.location || '',
          startDate: formatDateForInput(data.startDate) || '',
          endDate: formatDateForInput(data.endDate) || '',
          maxParticipants: data.maxParticipants || 50,
          isPublic: data.isPublic !== undefined ? data.isPublic : true,
          organizerId: data.organizerId || ''
        });
        
        setLoading(false);
      } catch (err) {
        console.error('Error fetching event:', err);
        setError('Échec du chargement des détails de l\'événement. Veuillez réessayer plus tard.');
        setLoading(false);
      }
    };

    fetchEvent();
  }, [id]);

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
    setSaveLoading(true);
    setError(null);
    
    try {
      const startDate = new Date(formData.startDate);
      const endDate = new Date(formData.endDate);
      
      if (endDate < startDate) {
        throw new Error('La date de fin ne peut pas être antérieure à la date de début');
      }
      
      console.log('Updating event with data:', formData);
      const updatedEvent = await updateEvent(id, formData);
      console.log('Updated event:', updatedEvent);
      
      navigate(`/events/${id}`);
    } catch (err) {
      console.error('Error updating event:', err);
      setError(err.message || 'Échec de la mise à jour de l\'événement. Veuillez réessayer.');
      setSaveLoading(false);
    }
  };

  if (loading) {
    return (
      <div className="loading">
        <div className="loading-spinner"></div>
        <p>Chargement des données de l'événement...</p>
      </div>
    );
  }

  return (
    <div className="create-event-container">
      <h2 className="page-title">Modifier l'événement</h2>
      
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
          <label htmlFor="maxParticipants">Nombre maximum de participants</label>
          <input
            type="number"
            id="maxParticipants"
            name="maxParticipants"
            value={formData.maxParticipants}
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
          <button type="submit" className="btn btn-primary" disabled={saveLoading}>
            {saveLoading ? 'Enregistrement...' : 'Enregistrer les modifications'}
          </button>
          <Link to={`/events/${id}`} className="btn btn-secondary">
            Annuler
          </Link>
        </div>
      </form>
    </div>
  );
};

export default EditEvent; 