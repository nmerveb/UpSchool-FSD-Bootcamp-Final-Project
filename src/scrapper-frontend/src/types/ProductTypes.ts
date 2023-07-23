export type ProductsGetAllQuery = {
  orderId: string | undefined;
};

export type ProductsGetAllDto = {
  forEach(arg0: (row: any) => void): unknown;
  orderId: string;
  name: string;
  picture: string;
  isOnSale: boolean;
  price: string;
  salePrice: string | null;
};
