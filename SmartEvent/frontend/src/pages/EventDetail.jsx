import React, { useState, useEffect } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import { getEventById, deleteEvent } from '../services/events';

const EventDetail = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [event, setEvent] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [deleteLoading, setDeleteLoading] = useState(false);

  useEffect(() => {
    const fetchEvent = async () => {
      try {
        const data = await getEventById(id);
        setEvent(data);
        setLoading(false);
      } catch (err) {
        setError('Échec du chargement des détails de l\'événement. Veuillez réessayer plus tard.');
        setLoading(false);
      }
    };

    fetchEvent();
  }, [id]);

  const formatDate = (dateString) => {
    const options = { year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit' };
    return new Date(dateString).toLocaleDateString('fr-FR', options);
  };

  const handleEdit = () => {
    navigate(`/events/${id}/edit`);
  };

  const handleDeleteClick = () => {
    setShowDeleteModal(true);
  };

  const handleCancelDelete = () => {
    setShowDeleteModal(false);
  };

  const handleConfirmDelete = async () => {
    setDeleteLoading(true);
    try {
      await deleteEvent(id);
      setShowDeleteModal(false);
      // Redirect to events page after successful deletion
      navigate('/events');
    } catch (err) {
      setError('Échec de la suppression de l\'événement. Veuillez réessayer plus tard.');
      setDeleteLoading(false);
      setShowDeleteModal(false);
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

  if (error) {
    return <div className="error">{error}</div>;
  }

  if (!event) {
    return <div className="error">Événement introuvable.</div>;
  }

  // Calculate if event is upcoming
  const isUpcoming = new Date(event.startDate) > new Date();
  
  // Calculate available spots
  const availableSpots = event.maxAttendees - event.currentAttendees;
  const spotsAvailable = availableSpots > 0;

  return (
    <div className="event-detail">
      {showDeleteModal && (
        <div className="modal-overlay">
          <div className="modal">
            <h3>Confirmer la suppression</h3>
            <p>Êtes-vous sûr de vouloir supprimer cet événement ? Cette action ne peut pas être annulée.</p>
            <div className="modal-actions">
              <button 
                className="btn btn-secondary" 
                onClick={handleCancelDelete}
                disabled={deleteLoading}
              >
                Annuler
              </button>
              <button 
                className="btn btn-danger" 
                onClick={handleConfirmDelete}
                disabled={deleteLoading}
              >
                {deleteLoading ? 'Suppression...' : 'Supprimer l\'événement'}
              </button>
            </div>
          </div>
        </div>
      )}
      
      <div className="event-header">
        {isUpcoming && (
          <span className="event-badge upcoming">À venir</span>
        )}
        <h2>{event.title}</h2>
        <div className="event-organizer">
          <span>Organisé par : </span>
          <strong>{event.organizerName || 'Anonyme'}</strong>
        </div>
      </div>
      
      <div className="event-body">
        <div className="description">
          <h3>À propos de cet événement</h3>
          <p>{event.description}</p>
        </div>
        
        <div className="event-sidebar">
          <div className="event-info">
            <div className="info-item">
              <svg xmlns="http://www.w3.org/2000/svg" className="icon" viewBox="0 0 20 20" fill="currentColor">
                <path fillRule="evenodd" d="M6 2a1 1 0 00-1 1v1H4a2 2 0 00-2 2v10a2 2 0 002 2h12a2 2 0 002-2V6a2 2 0 00-2-2h-1V3a1 1 0 10-2 0v1H7V3a1 1 0 00-1-1zm0 5a1 1 0 000 2h8a1 1 0 100-2H6z" clipRule="evenodd" />
              </svg>
              <div>
                <h4>Date de début</h4>
                <p>{formatDate(event.startDate)}</p>
              </div>
            </div>
            
            <div className="info-item">
              <svg xmlns="http://www.w3.org/2000/svg" className="icon" viewBox="0 0 20 20" fill="currentColor">
                <path fillRule="evenodd" d="M6 2a1 1 0 00-1 1v1H4a2 2 0 00-2 2v10a2 2 0 002 2h12a2 2 0 002-2V6a2 2 0 00-2-2h-1V3a1 1 0 10-2 0v1H7V3a1 1 0 00-1-1zm0 5a1 1 0 000 2h8a1 1 0 100-2H6z" clipRule="evenodd" />
              </svg>
              <div>
                <h4>Date de fin</h4>
                <p>{formatDate(event.endDate)}</p>
              </div>
            </div>
            
            <div className="info-item">
              <svg xmlns="http://www.w3.org/2000/svg" className="icon" viewBox="0 0 20 20" fill="currentColor">
                <path fillRule="evenodd" d="M5.05 4.05a7 7 0 119.9 9.9L10 18.9l-4.95-4.95a7 7 0 010-9.9zM10 11a2 2 0 100-4 2 2 0 000 4z" clipRule="evenodd" />
              </svg>
              <div>
                <h4>Lieu</h4>
                <p>{event.location}</p>
              </div>
            </div>
            
            <div className="info-item">
              <svg xmlns="http://www.w3.org/2000/svg" className="icon" viewBox="0 0 20 20" fill="currentColor">
                <path d="M9 6a3 3 0 11-6 0 3 3 0 016 0zM17 6a3 3 0 11-6 0 3 3 0 016 0zM12.93 17c.046-.327.07-.66.07-1a6.97 6.97 0 00-1.5-4.33A5 5 0 0119 16v1h-6.07zM6 11a5 5 0 015 5v1H1v-1a5 5 0 015-5z" />
              </svg>
              <div>
                <h4>Capacité</h4>
                <p className={spotsAvailable ? '' : 'full'}>
                  {spotsAvailable 
                    ? `${availableSpots} places disponibles (${event.currentAttendees}/${event.maxAttendees})` 
                    : 'Événement complet'}
                </p>
              </div>
            </div>
          </div>
          
          <div className="actions">
            {isUpcoming && spotsAvailable ? (
              <Link to={`/events/${event.id}/register`} className="btn btn-primary">
                S'inscrire maintenant
              </Link>
            ) : isUpcoming ? (
              <button className="btn btn-disabled" disabled>
                Événement complet
              </button>
            ) : (
              <button className="btn btn-disabled" disabled>
                Événement terminé
              </button>
            )}
            
            <div className="admin-actions">
              <button onClick={handleEdit} className="btn btn-secondary">
                <svg xmlns="http://www.w3.org/2000/svg" className="action-icon" viewBox="0 0 20 20" fill="currentColor">
                  <path d="M13.586 3.586a2 2 0 112.828 2.828l-.793.793-2.828-2.828.793-.793zM11.379 5.793L3 14.172V17h2.828l8.38-8.379-2.83-2.828z" />
                </svg>
                Modifier
              </button>
              <button onClick={handleDeleteClick} className="btn btn-danger">
                <svg xmlns="http://www.w3.org/2000/svg" className="action-icon" viewBox="0 0 20 20" fill="currentColor">
                  <path fillRule="evenodd" d="M9 2a1 1 0 00-.894.553L7.382 4H4a1 1 0 000 2v10a2 2 0 002 2h8a2 2 0 002-2V6a1 1 0 100-2h-3.382l-.724-1.447A1 1 0 0011 2H9zM7 8a1 1 0 012 0v6a1 1 0 11-2 0V8zm5-1a1 1 0 00-1 1v6a1 1 0 102 0V8a1 1 0 00-1-1z" clipRule="evenodd" />
                </svg>
                Supprimer
              </button>
            </div>
            
            <Link to="/events" className="btn btn-secondary">
              Retour aux événements
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default EventDetail; 