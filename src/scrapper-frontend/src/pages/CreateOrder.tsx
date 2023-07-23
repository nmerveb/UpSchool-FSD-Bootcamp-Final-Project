import NavBar from '../components/NavBar';
import {
  SimpleGrid,
  TextInput,
  Group,
  Button,
  Title,
  Flex,
  Space,
  Radio,
  List,
  ThemeIcon,
  ScrollArea,
  Alert,
} from '@mantine/core';
import { IconWorldSearch, IconAlertCircle } from '@tabler/icons-react';
import { useContext, useEffect, useState } from 'react';
import { AddOrderDto } from '../types/OrderTypes';
import { AuthContext } from '../context/AuthContext';
import axios from 'axios';
import * as signalR from '@microsoft/signalr';
import { LogDto } from '../types/LogTypes';

function CreateOrder() {
  const { token } = useContext(AuthContext);
  const [scrapingType, setScrapingType] = useState<string>();
  const [productCount, setProductCount] = useState<string>('');
  const [showAlert, setShowAlert] = useState(false);
  const [disableButton, setDisableButton] = useState(true);
  const [log, setLog] = useState<LogDto[]>([]);

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(
        // eslint-disable-next-line @typescript-eslint/restrict-template-expressions
        `https://localhost:7287/Hubs/ScraperLogHub?access_token=${token?.accessToken}`
      )
      .build();

    newConnection
      .start()
      .then(() => {
        console.log('Hub bağlantısı başarılı.');
      })
      .catch((err) => console.error('Hub bağlantısı başarısız.', err));

    newConnection.on('NewScraperLogAdded', (message) => {
      // eslint-disable-next-line @typescript-eslint/no-unsafe-return
      setLog((prevLog) => [...prevLog, message]);
    });

    return async () => {
      newConnection.off('NewScraperLogAdded');
      await newConnection.stop();
    };
  }, []);

  const handleCreateOrderClick = () => {
    if (
      productCount === 'All' ||
      (!isNaN(Number(productCount)) && productCount.trim() !== '')
    ) {
      const addOrderDto: AddOrderDto = {
        user: token?.id,
        accessToken: token?.accessToken,
        requestedAmount: productCount,
        scrapingType: Number(scrapingType),
      };

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

      setDisableButton(!disableButton);
      const response = api.post('/Orders/CreateOrder', addOrderDto).then();
    } else {
      setShowAlert(true);
    }
  };

  const handleNewOrder = () => {
    setDisableButton(!disableButton);
    setLog([]);
  };

  const logMap = log.map((item) => (
    <List.Item>
      {item.message}
      <p>{item.createdOn}</p>
    </List.Item>
  ));

  return (
    <>
      {showAlert && (
        <Alert
          icon={<IconAlertCircle size="40" />}
          title="Invalid Data!"
          color="red"
          mb="lg"
          pb="lg"
          withCloseButton
          onClose={() => setShowAlert(false)} // Close the alert when the close button is clicked
        >
          Please enter a valid product count. All or any number.
        </Alert>
      )}
      <NavBar />
      <Flex justify="center">
        <SimpleGrid cols={2} spacing="lg" verticalSpacing="xs">
          <Flex sx={{ minWidth: 700 }} justify="centers" direction="column">
            <Title mb="lg" order={2}>
              Create New Order
            </Title>
            <Space h="xl" />
            <Radio.Group
              value={scrapingType}
              onChange={setScrapingType}
              size="md"
              name="scrapingType"
              label="Select scraping type for order"
              withAsterisk
            >
              <Group mt="xs">
                <Radio value="0" label="All" color="yellow" />
                <Radio value="1" label="OnDiscount" color="yellow" />
                <Radio value="2" label="NonDiscount" color="yellow" />
              </Group>
            </Radio.Group>
            <Space h="xl" />
            <TextInput
              mt="lg"
              size="md"
              radius="md"
              label="Product Count"
              placeholder="Product count"
              onChange={(e) => setProductCount(e.target.value)}
              sx={{ maxWidth: 250 }}
            />
            <Space h="xl" />
            <Flex
              direction="row"
              sx={{ maxWidth: 350 }}
              justify="space-between"
            >
              <Button
                sx={{ maxWidth: 100 }}
                mt="lg"
                variant="filled"
                color="lime.8"
                radius="md"
                size="md"
                disabled={!disableButton}
                onClick={() => {
                  handleCreateOrderClick();
                }}
              >
                Create
              </Button>
              <Button
                mt="lg"
                variant="filled"
                color="blue.6"
                radius="md"
                size="md"
                disabled={disableButton}
                onClick={() => {
                  handleNewOrder();
                }}
              >
                New Order
              </Button>
            </Flex>
          </Flex>
          <Flex
            sx={{
              borderStyle: 'dashed',
              borderColor: '#CCCCCC',
              borderRadius: '8px',
              maxWidth: 400,
              padding: 20,
            }}
          >
            <ScrollArea h={500} w={500}>
              <List
                spacing="xs"
                size="sm"
                center
                icon={
                  <ThemeIcon color="teal" size={40} radius="xl">
                    <IconWorldSearch size={20} />
                  </ThemeIcon>
                }
              >
                {logMap}
              </List>
            </ScrollArea>
          </Flex>
        </SimpleGrid>
      </Flex>
    </>
  );
}

export default CreateOrder;
