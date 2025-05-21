import React, { useState, useEffect } from 'react';
import EventCard from '../components/EventCard';
import { getEvents } from '../services/events';

const EventList = () => {
  const [events, setEvents] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchEvents = async () => {
      try {
        setLoading(true);
        const data = await getEvents();
        setEvents(data);
        setLoading(false);
      } catch (err) {
        console.error('Error in EventList:', err);
        setError(`Échec du chargement des événements: ${err.message}`);
        setLoading(false);
      }
    };

    fetchEvents();
  }, []);

  if (loading) {
    return (
      <div className="loading">
        <div className="loading-spinner"></div>
        <p>Chargement des événements...</p>
      </div>
    );
  }

  if (error) {
    return (
      <div className="error">
        <h3>Erreur</h3>
        <p>{error}</p>
        <button 
          className="btn" 
          onClick={() => window.location.reload()}
          style={{ marginTop: '1rem' }}
        >
          Réessayer
        </button>
      </div>
    );
  }

  return (
    <div>
      <h2 className="page-title text-center">Événements à venir</h2>
      {events.length === 0 ? (
        <div className="empty-state">
          <p>Aucun événement disponible pour le moment.</p>
          <p className="empty-state-sub">Revenez plus tard pour des événements passionnants!</p>
        </div>
      ) : (
        <div className="event-list">
          {events.map((event) => (
            <EventCard key={event.id} event={event} />
          ))}
        </div>
      )}
    </div>
  );
};

export default EventList; 