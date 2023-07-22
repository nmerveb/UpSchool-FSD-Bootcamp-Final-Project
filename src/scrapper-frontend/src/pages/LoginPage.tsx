import React from 'react';
import { Text, Button, Paper, Flex, Space, Image } from '@mantine/core';
import logo from '/crawler-logo.svg';
import { useSearchParams } from 'react-router-dom';

function LoginPage() {
  const [searchParams] = useSearchParams();

  return (
    <>
      <Flex sx={{ minHeight: 800 }} justify="center" align="center" gap="xl">
        <Paper
          sx={{ minHeight: 500, minWidth: 500 }}
          shadow="sm"
          radius="md"
          p="xl"
        >
          <Image maw={100} mx="auto" src={logo} />
          <Space h="xl" />
          <Text ta="center" fz="lg" weight={500} mt="md">
            Welcome to CRAWLER!
          </Text>
          <Space h="xl" />
          <Text ta="center" c="dimmed" fz="sm">
            Please Login
          </Text>
          <Space h="xl" />
          <Space h="xl" />
          <Button variant="outline" color="gray" radius="md" fullWidth mt="md">
            Send message
          </Button>
        </Paper>
      </Flex>
    </>
  );
}

export default LoginPage;
