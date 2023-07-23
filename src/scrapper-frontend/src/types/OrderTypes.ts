export type OrdersGetAllQuery = {
  user: string | undefined;
};

export type OrdersGetAllDto = {
  id: string;
  requestedAmount: string;
  totalFoundAmount: number;
  scrapingType: number;
  createdOn: string;
};

export type AddOrderDto = {
  user: string | undefined;
  accessToken: string | undefined;
  requestedAmount: string;
  scrapingType: number;
};
