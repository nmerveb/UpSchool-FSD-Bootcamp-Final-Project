export type OrderEventsGetAllQuery = {
  orderId: string | undefined;
};

export type OrderEventsGetAllDto = {
  orderId: string;
  createdOn: string;
  status: number;
};
