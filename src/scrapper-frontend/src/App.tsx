import { Route, Routes } from 'react-router-dom';
import { useState } from 'react';
import { AuthContext } from './context/AuthContext';
import './App.css';
import HomePage from './pages/HomePage';
import LoginPage from './pages/LoginPage';
import NotFoundPage from './pages/NotFoundPage';
import CreateOrder from './pages/CreateOrder';
import SettingsPage from './pages/SettingsPage';
import { LocalUser } from './types/AuthTypes';
import ProtectedRoute from './components/ProtectedRoute';

function App() {
  const [token, setToken] = useState<LocalUser | undefined>(undefined);

  return (
    <>
      <AuthContext.Provider value={{ token, setToken }}>
        <Routes>
          <Route
            path="/"
            element={
              <ProtectedRoute>
                <HomePage />
              </ProtectedRoute>
            }
          />
          <Route
            path="/createOrder"
            element={
              <ProtectedRoute>
                <CreateOrder />
              </ProtectedRoute>
            }
          />
          <Route
            path="/settings"
            element={
              <ProtectedRoute>
                <SettingsPage />
              </ProtectedRoute>
            }
          />
          <Route path="/login" element={<LoginPage />} />
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </AuthContext.Provider>
    </>
  );
}

export default App;
