import React, { createContext, useContext } from 'react';
import { LocalUser } from '../types/AuthTypes';

type AuthContextType = {
  token: LocalUser | undefined;
  setToken: React.Dispatch<React.SetStateAction<LocalUser | undefined>>;
};

export const AuthContext = createContext<AuthContextType>({
  token: undefined,
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  setToken: () => {},
});

export const useAuth = () => useContext(AuthContext);
