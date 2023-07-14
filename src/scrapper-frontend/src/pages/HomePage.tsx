import '../App.css';
import NavBar from '../components/NavBar';
import { Table, Group, Button, ScrollArea, Flex } from '@mantine/core';
import { IconPlus } from '@tabler/icons-react';

interface UsersTableProps {
  data: {
    orderId: string;
    requestedAmount: string;
    totalAmount: string;
    scrapingType: string;
    createdOn: string;
  }[];
}

const dat = [
  {
    orderId: '064b3441-e8a1-4d2c-bf0a-fda1fd8060c7',
    requestedAmount: '10',
    totalAmount: '50',
    scrapingType: 'OnSale',
    createdOn: '2023-06-04 08:25:14.601',
  },
  {
    orderId: '064b3441-e8a1-4d2c-bf0a-fda1fd8060c7',
    requestedAmount: '10',
    totalAmount: '50',
    scrapingType: 'OnSale',
    createdOn: '2023-06-04 08:25:14.601',
  },
  {
    orderId: '064b3441-e8a1-4d2c-bf0a-fda1fd8060c7',
    requestedAmount: '10',
    totalAmount: '50',
    scrapingType: 'OnSale',
    createdOn: '2023-06-04 08:25:14.601',
  },
  {
    orderId: '064b3441-e8a1-4d2c-bf0a-fda1fd8060c7',
    requestedAmount: '10',
    totalAmount: '50',
    scrapingType: 'OnSale',
    createdOn: '2023-06-04 08:25:14.601',
  },
  {
    orderId: '064b3441-e8a1-4d2c-bf0a-fda1fd8060c7',
    requestedAmount: '10',
    totalAmount: '50',
    scrapingType: 'OnSale',
    createdOn: '2023-06-04 08:25:14.601',
  },
  {
    orderId: '064b3441-e8a1-4d2c-bf0a-fda1fd8060c7',
    requestedAmount: '10',
    totalAmount: '50',
    scrapingType: 'OnSale',
    createdOn: '2023-06-04 08:25:14.601',
  },
  {
    orderId: '064b3441-e8a1-4d2c-bf0a-fda1fd8060c7',
    requestedAmount: '10',
    totalAmount: '50',
    scrapingType: 'OnSale',
    createdOn: '2023-06-04 08:25:14.601',
  },
  {
    orderId: '064b3441-e8a1-4d2c-bf0a-fda1fd8060c7',
    requestedAmount: '10',
    totalAmount: '50',
    scrapingType: 'OnSale',
    createdOn: '2023-06-04 08:25:14.601',
  },
];
function HomePage() {
  const data = dat;
  // eslint-disable-next-line @typescript-eslint/no-unsafe-assignment
  const rows = data.map((item) => (
    <tr key={item.orderId}>
      <td>{item.orderId}</td>

      <td>{item.requestedAmount}</td>
      <td>{item.totalAmount}</td>
      <td>{item.scrapingType}</td>
      <td>{item.createdOn}</td>
      <td>
        <Group spacing={10} position="right">
          <Button variant="outline" color="blue.9" radius="md">
            Products
          </Button>
          <Button variant="filled" color="blue.9" radius="md">
            Events
          </Button>
          <Button variant="filled" color="teal.9" radius="md">
            Excel
          </Button>
        </Group>
      </td>
    </tr>
  ));

  return (
    <>
      <NavBar />
      <Flex
        sx={{ maxWidth: 1700 }}
        justify="flex-end"
        align="center"
        direction="row"
      >
        <Button leftIcon={<IconPlus />} radius="md" color="lime.7">
          Add Order
        </Button>
      </Flex>
      <Flex justify="center" align="center">
        <ScrollArea>
          <Table sx={{ minWidth: 1500 }} verticalSpacing="sm">
            <thead>
              <tr>
                <th>OrderId</th>
                <th>Crawl Type</th>
                <th>Requested Amount</th>
                <th>Total Amount</th>
                <th>Created On</th>
                <th />
              </tr>
            </thead>
            <tbody>{rows}</tbody>
          </Table>
        </ScrollArea>
      </Flex>
    </>
  );
}

export default HomePage;
