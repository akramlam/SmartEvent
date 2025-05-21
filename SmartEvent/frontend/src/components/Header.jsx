import React, { useState } from 'react';
import { Link, NavLink } from 'react-router-dom';

const Header = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  
  const toggleMenu = () => {
    setIsMenuOpen(!isMenuOpen);
  };

  return (
    <header className="main-header">
      <div className="header-container">
        <div className="logo-container">
          <Link to="/" className="logo">
            <span className="logo-text">Smart</span>
            <span className="logo-highlight">Event</span>
          </Link>
        </div>
        
        <div className="desktop-nav">
          <NavLink 
            to="/"
            className={({isActive}) => 
              isActive ? 'nav-link active' : 'nav-link'
            }
          >
            Accueil
          </NavLink>
          <NavLink 
            to="/events" 
            className={({isActive}) => 
              isActive && !window.location.pathname.includes('/create') 
                ? 'nav-link active' 
                : 'nav-link'
            }
          >
            Événements
          </NavLink>
          <NavLink 
            to="/about" 
            className={({isActive}) => 
              isActive ? 'nav-link active' : 'nav-link'
            }
          >
            À propos
          </NavLink>
          <NavLink 
            to="/contact" 
            className={({isActive}) => 
              isActive ? 'nav-link active' : 'nav-link'
            }
          >
            Contact
          </NavLink>
          <Link to="/events/create" className="create-btn">
            Créer un Événement
          </Link>
        </div>
        
        <button className="mobile-menu-button" onClick={toggleMenu}>
          <svg 
            xmlns="http://www.w3.org/2000/svg" 
            fill="none" 
            viewBox="0 0 24 24" 
            stroke="currentColor" 
          >
            {isMenuOpen ? (
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
            ) : (
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h16" />
            )}
          </svg>
        </button>
      </div>
      
      {isMenuOpen && (
        <div className="mobile-nav">
          <NavLink 
            to="/"
            className={({isActive}) => 
              isActive ? 'mobile-nav-link active' : 'mobile-nav-link'
            }
            onClick={() => setIsMenuOpen(false)}
          >
            Accueil
          </NavLink>
          <NavLink 
            to="/events" 
            className={({isActive}) => 
              isActive && !window.location.pathname.includes('/create') 
                ? 'mobile-nav-link active' 
                : 'mobile-nav-link'
            }
            onClick={() => setIsMenuOpen(false)}
          >
            Événements
          </NavLink>
          <NavLink 
            to="/about" 
            className={({isActive}) => 
              isActive ? 'mobile-nav-link active' : 'mobile-nav-link'
            }
            onClick={() => setIsMenuOpen(false)}
          >
            À propos
          </NavLink>
          <NavLink 
            to="/contact" 
            className={({isActive}) => 
              isActive ? 'mobile-nav-link active' : 'mobile-nav-link'
            }
            onClick={() => setIsMenuOpen(false)}
          >
            Contact
          </NavLink>
          <Link 
            to="/events/create" 
            className="mobile-create-btn"
            onClick={() => setIsMenuOpen(false)}
          >
            Créer un Événement
          </Link>
        </div>
      )}
    </header>
  );
};

export default Header; 