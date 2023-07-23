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
import { OrderContext } from './context/OrderContext';
import { NotificationContext } from './context/NotificationContext';
import { OrdersGetAllDto } from './types/OrderTypes';

function App() {
  const [token, setToken] = useState<LocalUser | undefined>(undefined);
  const [orders, setOrders] = useState<OrdersGetAllDto[] | undefined>(
    undefined
  );
  const [isUserAllow, setIsUserAllow] = useState<boolean | undefined>(true);

  return (
    <>
      <AuthContext.Provider value={{ token, setToken }}>
        <OrderContext.Provider value={{ orders, setOrders }}>
          <NotificationContext.Provider value={{ isUserAllow, setIsUserAllow }}>
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
          </NotificationContext.Provider>
        </OrderContext.Provider>
      </AuthContext.Provider>
    </>
  );
}

export default App;
