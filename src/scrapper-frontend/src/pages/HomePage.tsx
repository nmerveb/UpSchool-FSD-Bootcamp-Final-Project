import '../App.css';
import {
  Modal,
  Table,
  Group,
  Button,
  ScrollArea,
  Flex,
  Avatar,
} from '@mantine/core';
import { useDisclosure } from '@mantine/hooks';
import { IconPlus } from '@tabler/icons-react';
import { useEffect, useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import NavBar from '../components/NavBar';
import { AuthContext } from '../context/AuthContext';
import { getStatusText } from '../utils/statusHelper';
import { OrdersGetAllDto, OrdersGetAllQuery } from '../types/OrderTypes';
import { ProductsGetAllDto, ProductsGetAllQuery } from '../types/ProductTypes';
import {
  OrderEventsGetAllDto,
  OrderEventsGetAllQuery,
} from '../types/OrderEventTypes';

function HomePage() {
  const [opened, { open, close }] = useDisclosure(false);
  const [modalType, setModalType] = useState('');
  const { token } = useContext(AuthContext);
  const [orders, setOrders] = useState<OrdersGetAllDto[]>([]);
  const [products, setProducts] = useState<ProductsGetAllDto[]>([]);
  const [orderEvents, setOrderEvents] = useState<OrderEventsGetAllDto[]>([]);
  const navigate = useNavigate();

  useEffect(() => {
    const api = axios.create({
      baseURL: 'https://localhost:7287/api',
    });

    api.interceptors.request.use((config) => {
      if (token) {
        config.headers['Authorization'] = `Bearer ${token.accessToken}`;
        config.headers['Content-Type'] = 'application/json';
      }
      return config;
    });

    const orderGetAll: OrdersGetAllQuery = { user: token?.id };
    const response = api
      .post('/Orders/GetAll', orderGetAll)
      .then((response) => {
        // eslint-disable-next-line @typescript-eslint/no-unsafe-argument
        setOrders(response.data);
      });
  }, [token, setOrders]);

  const handleAddOrderClick = () => {
    // Yönlendirme işlemini burada gerçekleştirin
    navigate('/createOrder');
  };

  const fetchDataForProductsModal = (orderId: string) => {
    setModalType('Products');
    const api = axios.create({
      baseURL: 'https://localhost:7287/api',
    });

    api.interceptors.request.use((config) => {
      if (token) {
        config.headers['Authorization'] = `Bearer ${token.accessToken}`;
        config.headers['Content-Type'] = 'application/json';
      }
      return config;
    });
    const productGetAll: ProductsGetAllQuery = { orderId: orderId };
    const response = api
      .post('/Products/GetAll', productGetAll)
      .then((response) => {
        // eslint-disable-next-line @typescript-eslint/no-unsafe-argument
        setProducts(response.data);
        console.log(response.data);
      })
      .then(() => {
        open();
      });

    console.log(response);

    open();
  };

  const fetchDataForOrderEventsModal = (orderId: string) => {
    setModalType('OrderEvents');
    const api = axios.create({
      baseURL: 'https://localhost:7287/api',
    });

    api.interceptors.request.use((config) => {
      if (token) {
        config.headers['Authorization'] = `Bearer ${token.accessToken}`;
        config.headers['Content-Type'] = 'application/json';
      }
      return config;
    });
    const orderEventsGetAll: OrderEventsGetAllQuery = { orderId: orderId };
    const response = api
      .post('/OrderEvents/GetAll', orderEventsGetAll)
      .then((response) => {
        // eslint-disable-next-line @typescript-eslint/no-unsafe-argument
        setOrderEvents(response.data);
        console.log(response.data);
      })
      .then(() => {
        open();
      });

    console.log(response);

    open();
  };

  const rowsForProducts = products.map((item) => (
    <tr key={item.orderId}>
      <td>{item.orderId}</td>
      <td>{item.name}</td>
      <td>
        <Avatar size={100} src={item.picture} />
      </td>
      <td>{item.isOnSale ? 'Yes' : 'No'}</td>
      <td>{item.price}</td>
      <td>{item.salePrice}</td>
    </tr>
  ));
  const rowsForOrderEvents = orderEvents.map((item) => (
    <tr key={item.orderId}>
      <td>{item.orderId}</td>
      <td>{getStatusText(item.status)}</td>
      <td>{item.createdOn}</td>
    </tr>
  ));
  // eslint-disable-next-line @typescript-eslint/no-unsafe-assignment
  const rows = orders.map((item) => (
    <tr key={item.id}>
      <td>{item.id}</td>
      <td>{item.scrapingType}</td>
      <td>{item.requestedAmount}</td>
      <td>{item.totalFoundAmount}</td>
      <td>{item.createdOn}</td>

      <td>
        <Group spacing={10} position="right">
          <Button
            variant="outline"
            color="blue.9"
            radius="md"
            onClick={() => {
              fetchDataForProductsModal(item.id);
            }}
          >
            Products
          </Button>
          <Button
            variant="filled"
            color="blue.9"
            radius="md"
            onClick={() => {
              fetchDataForOrderEventsModal(item.id);
            }}
          >
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
      <Modal
        opened={opened}
        onClose={close}
        scrollAreaComponent={ScrollArea.Autosize}
        size="55%"
        centered
      >
        {modalType === 'OrderEvents' ? (
          <Table verticalSpacing="sm">
            <thead>
              <tr>
                <th>OrderId</th>
                <th>Status</th>
                <th>CreatedOn</th>
                <th />
              </tr>
            </thead>
            <tbody>{rowsForOrderEvents}</tbody>
          </Table>
        ) : modalType === 'Products' ? (
          <Table verticalSpacing="sm">
            <thead>
              <tr>
                <th>OrderId</th>
                <th>Name</th>
                <th>Picture</th>
                <th>IsOnSale</th>
                <th>Price</th>
                <th>Sale Price</th>
                <th />
              </tr>
            </thead>
            <tbody>{rowsForProducts}</tbody>
          </Table>
        ) : null}
      </Modal>
      <NavBar />
      <Flex
        sx={{ maxWidth: 1700 }}
        justify="flex-end"
        align="center"
        direction="row"
      >
        <Button
          leftIcon={<IconPlus />}
          onClick={handleAddOrderClick}
          radius="md"
          color="lime.7"
        >
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
