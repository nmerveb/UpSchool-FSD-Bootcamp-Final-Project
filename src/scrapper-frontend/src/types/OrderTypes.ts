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
