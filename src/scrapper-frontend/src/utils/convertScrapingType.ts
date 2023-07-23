export const convertScrapingType = (status: number): string => {
  switch (status) {
    case 0:
      return 'All';
    case 1:
      return 'OnDiscount';
    case 2:
      return 'NonDiscount';
    default:
      return '';
  }
};
