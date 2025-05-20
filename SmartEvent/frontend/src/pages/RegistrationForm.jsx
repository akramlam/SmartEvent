import React, { useState, useEffect } from 'react';
import { useParams, useNavigate, Link } from 'react-router-dom';
import { getEventById } from '../services/events';
import { register } from '../services/registrations';

const RegistrationForm = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [event, setEvent] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [submitting, setSubmitting] = useState(false);
  const [success, setSuccess] = useState(false);
  const [formData, setFormData] = useState({
    name: '',
    email: '',
  });

  useEffect(() => {
    const fetchEvent = async () => {
      try {
        console.log(`Fetching event details for ID: ${id}`);
        const data = await getEventById(id);
        console.log('Event data received:', data);
        setEvent(data);
        setLoading(false);
      } catch (err) {
        console.error('Error fetching event:', err);
        setError(`Échec du chargement des détails de l'événement: ${err.message}`);
        setLoading(false);
      }
    };

    fetchEvent();
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setSubmitting(true);
    setError(null); // Clear any previous errors
    
    try {
      console.log(`Submitting registration for event ${id}`);
      await register(id, formData);
      setSuccess(true);
    } catch (err) {
      console.error('Registration error:', err);
      setError(err.message || 'L\'inscription a échoué. Veuillez réessayer.');
      setSubmitting(false);
    }
  };

  if (loading) {
    return (
      <div className="loading">
        <div className="loading-spinner"></div>
        <p>Chargement des détails de l'événement...</p>
      </div>
    );
  }

  if (error && !success) {
    return (
      <div className="error">
        <h3>Erreur</h3>
        <p>{error}</p>
        <div style={{ marginTop: '1rem' }}>
          <Link to={`/events/${id}`} className="btn">
            Retour à l'événement
          </Link>
          <button 
            className="btn" 
            onClick={() => setError(null)}
            style={{ marginLeft: '0.5rem' }}
          >
            Réessayer
          </button>
        </div>
      </div>
    );
  }

  if (success) {
    return (
      <div className="success-message">
        <svg xmlns="http://www.w3.org/2000/svg" className="check-icon" viewBox="0 0 20 20" fill="currentColor" style={{ width: '50px', height: '50px', margin: '0 auto 1rem auto', display: 'block', color: 'var(--success)' }}>
          <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-9.293a1 1 0 00-1.414-1.414L9 10.586 7.707 9.293a1 1 0 00-1.414 1.414l2 2a1 1 0 001.414 0l4-4z" clipRule="evenodd" />
        </svg>
        <h2>Inscription réussie !</h2>
        <p>Vous vous êtes inscrit avec succès à <strong>{event?.title}</strong>.</p>
        <p>Votre inscription a été confirmée et vous recevrez les détails par e-mail.</p>
        
        <div style={{ marginTop: '1.5rem' }}>
          <Link to={`/events/${id}`} className="btn btn-primary">
            Retour aux détails de l'événement
          </Link>
          <Link to="/events" className="btn btn-secondary" style={{ marginLeft: '0.5rem' }}>
            Parcourir plus d'événements
          </Link>
        </div>
      </div>
    );
  }

  return (
    <div className="form-container">
      <h2 className="page-title">S'inscrire à l'événement</h2>
      <div className="event-summary">
        <h3>{event?.title}</h3>
        <p className="event-date">
          <svg xmlns="http://www.w3.org/2000/svg" className="icon" viewBox="0 0 20 20" fill="currentColor">
            <path fillRule="evenodd" d="M6 2a1 1 0 00-1 1v1H4a2 2 0 00-2 2v10a2 2 0 002 2h12a2 2 0 002-2V6a2 2 0 00-2-2h-1V3a1 1 0 10-2 0v1H7V3a1 1 0 00-1-1zm0 5a1 1 0 000 2h8a1 1 0 100-2H6z" clipRule="evenodd" />
          </svg>
          {event?.startDate && new Date(event.startDate).toLocaleDateString('fr-FR', { 
            year: 'numeric', 
            month: 'long', 
            day: 'numeric'
          })}
        </p>
      </div>
      
      <form onSubmit={handleSubmit} className="registration-form">
        <div className="form-group">
          <label htmlFor="name">Votre nom</label>
          <input
            type="text"
            id="name"
            name="name"
            value={formData.name}
            onChange={handleChange}
            placeholder="Entrez votre nom complet"
            required
          />
        </div>
        
        <div className="form-group">
          <label htmlFor="email">Adresse e-mail</label>
          <input
            type="email"
            id="email"
            name="email"
            value={formData.email}
            onChange={handleChange}
            placeholder="vous@example.com"
            required
          />
          <small>Nous enverrons votre confirmation à cette adresse e-mail.</small>
        </div>
        
        <div className="form-actions">
          <button type="submit" className="btn btn-primary" disabled={submitting}>
            {submitting ? 'Inscription en cours...' : 'Finaliser l\'inscription'}
          </button>
          <Link to={`/events/${id}`} className="btn btn-secondary">
            Annuler
          </Link>
        </div>
      </form>
    </div>
  );
};

export default RegistrationForm; 