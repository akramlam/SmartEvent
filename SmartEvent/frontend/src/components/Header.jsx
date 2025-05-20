import React from 'react';
import { Link, NavLink } from 'react-router-dom';

const Header = () => {
  return (
    <header className="header">
      <div className="container">
        <div className="header-content">
          <h1>
            <Link to="/" className="logo">
              <span className="logo-text">Smart</span>
              <span className="logo-highlight">Event</span>
            </Link>
          </h1>
          <nav className="nav">
            <NavLink 
              to="/events" 
              className={({ isActive }) => 
                isActive && !window.location.pathname.includes('/create') 
                  ? 'nav-link active' 
                  : 'nav-link'
              }
            >
              Événements
            </NavLink>
            <Link 
              to="/events/create" 
              className="btn btn-sm create-event-btn"
            >
              Créer un Événement
            </Link>
          </nav>
        </div>
      </div>
    </header>
  );
};

export default Header; 