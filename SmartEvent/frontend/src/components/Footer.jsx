import React from 'react';

const Footer = () => {
  return (
    <footer className="footer">
      <div className="container">
        <p>© {new Date().getFullYear()} SmartEvent - Tous droits réservés</p>
      </div>
    </footer>
  );
};

export default Footer; 