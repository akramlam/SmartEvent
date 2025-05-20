import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import EventList from './pages/EventList';
import EventDetail from './pages/EventDetail';
import RegistrationForm from './pages/RegistrationForm';
import CreateEvent from './pages/CreateEvent';
import EditEvent from './pages/EditEvent';
import Header from './components/Header';
import Footer from './components/Footer';

function App() {
  return (
    <Router>
      <div className="app">
        <Header />
        <main className="container">
          <Routes>
            <Route path="/" element={<Navigate to="/events" replace />} />
            <Route path="/events" element={<EventList />} />
            <Route path="/events/create" element={<CreateEvent />} />
            <Route path="/events/:id" element={<EventDetail />} />
            <Route path="/events/:id/edit" element={<EditEvent />} />
            <Route path="/events/:id/register" element={<RegistrationForm />} />
          </Routes>
        </main>
        <Footer />
      </div>
    </Router>
  );
}

export default App; 