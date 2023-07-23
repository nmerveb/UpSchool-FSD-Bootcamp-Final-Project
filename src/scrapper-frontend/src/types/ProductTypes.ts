export type ProductsGetAllQuery = {
  orderId: string | undefined;
};

export type ProductsGetAllDto = {
  orderId: string;
  name: string;
  picture: string;
  isOnSale: boolean;
  price: string;
  salePrice: string | null;
};
