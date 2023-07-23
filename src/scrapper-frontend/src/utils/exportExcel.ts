import ExcelJS from 'exceljs';
import { saveAs } from 'file-saver';
import { ProductsGetAllDto } from '../types/ProductTypes';

export function exportExcel(data: ProductsGetAllDto[]) {
  const workbook = new ExcelJS.Workbook();
  const worksheet = workbook.addWorksheet('Sheet1');

  worksheet.columns = [
    { header: 'OrderId', key: 'orderId' },
    { header: 'Name', key: 'name' },
    { header: 'Picture', key: 'picture' },
    { header: 'IsOnSale', key: 'isOnSale' },
    { header: 'Price', key: 'price' },
    { header: 'SalePrice', key: 'salePrice' },
  ];

  // eslint-disable-next-line @typescript-eslint/no-unsafe-call
  data.forEach((row) => {
    worksheet.addRow(row);
  });

  void workbook.xlsx.writeBuffer().then((buffer) => {
    const blob = new Blob([buffer], {
      type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
    });
    // eslint-disable-next-line @typescript-eslint/no-unsafe-call
    saveAs(blob, 'Order.xlsx');
  });
}
