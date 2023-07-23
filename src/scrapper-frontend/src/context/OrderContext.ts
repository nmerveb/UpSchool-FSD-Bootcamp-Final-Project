import React, { createContext, useContext } from 'react';
import { OrdersGetAllDto } from '../types/OrderTypes';

type OrderContextType = {
  orders: OrdersGetAllDto[] | undefined;
  setOrders: React.Dispatch<
    React.SetStateAction<OrdersGetAllDto[] | undefined>
  >;
};

export const OrderContext = createContext<OrderContextType>({
  orders: undefined,
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  setOrders: () => {},
});

export const useOrder = () => useContext(OrderContext);
