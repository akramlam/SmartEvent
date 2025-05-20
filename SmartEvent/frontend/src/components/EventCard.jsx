import React from 'react';
import { Link } from 'react-router-dom';

const EventCard = ({ event }) => {
  if (!event) {
    return <div className="card event-card">Chargement des données de l'événement...</div>;
  }

  const formatDate = (dateString) => {
    if (!dateString) return 'À déterminer';
    try {
      const options = { year: 'numeric', month: 'long', day: 'numeric' };
      return new Date(dateString).toLocaleDateString('fr-FR', options);
    } catch (e) {
      console.error('Error formatting date:', e);
      return 'Date invalide';
    }
  };

  return (
    <div className="card event-card">
      <div className="card-content">
        <h3 className="card-title">{event.title || 'Événement sans titre'}</h3>
        
        <div className="card-details">
          <div className="card-detail">
            <svg xmlns="http://www.w3.org/2000/svg" className="icon" viewBox="0 0 20 20" fill="currentColor">
              <path fillRule="evenodd" d="M6 2a1 1 0 00-1 1v1H4a2 2 0 00-2 2v10a2 2 0 002 2h12a2 2 0 002-2V6a2 2 0 00-2-2h-1V3a1 1 0 10-2 0v1H7V3a1 1 0 00-1-1zm0 5a1 1 0 000 2h8a1 1 0 100-2H6z" clipRule="evenodd" />
            </svg>
            <span>{formatDate(event.startDate)}</span>
          </div>
          
          <div className="card-detail">
            <svg xmlns="http://www.w3.org/2000/svg" className="icon" viewBox="0 0 20 20" fill="currentColor">
              <path fillRule="evenodd" d="M5.05 4.05a7 7 0 119.9 9.9L10 18.9l-4.95-4.95a7 7 0 010-9.9zM10 11a2 2 0 100-4 2 2 0 000 4z" clipRule="evenodd" />
            </svg>
            <span>{event.location || 'Lieu à déterminer'}</span>
          </div>
        </div>
        
        {event.description && (
          <p className="card-description">
            {event.description.length > 100 ? `${event.description.substring(0, 100)}...` : event.description}
          </p>
        )}
        
        <div className="attendance-info">
          <span className="attendance-count">
            {(event.currentAttendees ?? 0)}/{(event.maxAttendees ?? 0)} participants
          </span>
        </div>
      </div>
      
      <div className="card-actions">
        <Link to={`/events/${event.id}`} className="btn">
          Voir les détails
        </Link>
      </div>
    </div>
  );
};

export default EventCard; 