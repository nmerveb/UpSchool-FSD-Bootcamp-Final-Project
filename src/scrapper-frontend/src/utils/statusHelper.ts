export const getStatusText = (status: number): string => {
  switch (status) {
    case 1:
      return 'Bot Started';
    case 2:
      return 'Scraping Started';
    case 3:
      return 'Scraping Completed';
    case 4:
      return 'Scraping Failed';
    case 5:
      return 'Order Completed';
    default:
      return '';
  }
};
