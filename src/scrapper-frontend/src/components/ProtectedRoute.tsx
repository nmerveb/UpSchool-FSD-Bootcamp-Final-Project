import React, { useContext } from 'react';
import { AuthContext } from '../context/AuthContext';
import { Navigate } from 'react-router-dom';

type ProtectedRouteProps = {
  children: React.ReactElement;
};
export default function ProtectedRoute({ children }: ProtectedRouteProps) {
  const { token } = useContext(AuthContext);

  if (!token) return <Navigate to="/login" />;

  return children;
}
