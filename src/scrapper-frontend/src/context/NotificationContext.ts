/* eslint-disable @typescript-eslint/no-empty-function */
import React, { createContext, useContext } from 'react';

export type NotificationContextType = {
  isUserAllow: boolean | undefined;

  setIsUserAllow: React.Dispatch<React.SetStateAction<boolean | undefined>>;
};

export const NotificationContext = createContext<NotificationContextType>({
  isUserAllow: true,
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  setIsUserAllow: () => {},
});

export const useNotification = () => useContext(NotificationContext);
