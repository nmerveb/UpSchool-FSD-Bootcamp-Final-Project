import NavBar from '../components/NavBar';
import { useForm } from '@mantine/form';
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
} from '@mantine/core';
import { IconWorldSearch } from '@tabler/icons-react';
import { useState } from 'react';

function CreateOrder() {
  // eslint-disable-next-line @typescript-eslint/no-unsafe-assignment
  const [value, setValue] = useState('');
  // eslint-disable-next-line @typescript-eslint/no-unsafe-assignment, @typescript-eslint/no-unsafe-call
  const form = useForm({
    initialValues: {
      scrapingType: '',
      email: '',
    },
  });
  return (
    <>
      <NavBar />
      <Flex justify="center">
        <SimpleGrid cols={2} spacing="lg" verticalSpacing="xs">
          <Flex sx={{ minWidth: 700 }} justify="centers" direction="column">
            <Title mb="lg" order={2}>
              Create New Order
            </Title>
            <Space h="xl" />
            <Radio.Group
              value={value}
              onChange={setValue}
              size="md"
              name="scrapingType"
              label="Select scraping type for order"
              withAsterisk
            >
              <Group mt="xs">
                <Radio value="all" label="All" color="yellow" />
                <Radio value="onSale" label="OnSale" color="yellow" />
                <Radio value="normal" label="Normal" color="yellow" />
              </Group>
            </Radio.Group>
            <Space h="xl" />
            <TextInput
              mt="lg"
              size="md"
              radius="md"
              label="Product Count"
              placeholder="Product count"
              sx={{ maxWidth: 250 }}
              // eslint-disable-next-line @typescript-eslint/no-unsafe-member-access, @typescript-eslint/no-unsafe-call
              {...form.getInputProps('email')}
            />
            <Space h="xl" />
            <Button
              sx={{ maxWidth: 100 }}
              mt="lg"
              variant="filled"
              color="lime.8"
              radius="md"
              size="md"
            >
              Create
            </Button>
          </Flex>
          <Flex
            sx={{
              borderStyle: 'dashed',
              borderColor: '#CCCCCC',
              borderRadius: '8px',
              maxWidth: 500,
              padding: 20,
            }}
          >
            <ScrollArea h={500} w={500}>
              <List
                spacing="xs"
                size="sm"
                center
                icon={
                  <ThemeIcon color="teal" size={30} radius="xl">
                    <IconWorldSearch size={20} />
                  </ThemeIcon>
                }
              >
                <List.Item>Clone or download repository from GitHub</List.Item>
                <List.Item>Install dependencies with yarn</List.Item>
                <List.Item>
                  To start development server run npm start command
                </List.Item>
                <List.Item>
                  Run tests to make sure your changes do not break the build
                </List.Item>
                <List.Item>Clone or download repository from GitHub</List.Item>
                <List.Item>Install dependencies with yarn</List.Item>
                <List.Item>
                  To start development server run npm start command
                </List.Item>
                <List.Item>
                  Run tests to make sure your changes do not break the build
                </List.Item>
                <List.Item>Clone or download repository from GitHub</List.Item>
                <List.Item>Install dependencies with yarn</List.Item>
                <List.Item>
                  To start development server run npm start command
                </List.Item>
                <List.Item>
                  Run tests to make sure your changes do not break the build
                </List.Item>
                <List.Item>Clone or download repository from GitHub</List.Item>
                <List.Item>Install dependencies with yarn</List.Item>
                <List.Item>
                  To start development server run npm start command
                </List.Item>
                <List.Item>
                  Run tests to make sure your changes do not break the build
                </List.Item>
              </List>
            </ScrollArea>
          </Flex>
        </SimpleGrid>
      </Flex>
    </>
  );
}

export default CreateOrder;
