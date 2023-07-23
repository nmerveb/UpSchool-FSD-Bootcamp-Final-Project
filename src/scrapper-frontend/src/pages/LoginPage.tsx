/* eslint-disable @typescript-eslint/no-unsafe-assignment */
import React, { useEffect, useContext } from 'react';
import { Text, Button, Paper, Flex, Space, Image } from '@mantine/core';
import logo from '/crawler-logo.svg';
import { useSearchParams, useNavigate } from 'react-router-dom';
import { LocalUser } from '../types/AuthTypes';
import { AuthContext } from '../context/AuthContext';
import { getClaimsFromJwt } from '../utils/jwtHelper.ts';

const BASE_URL = import.meta.env.BASE_URL;

function LoginPage() {
  const [searchParams] = useSearchParams();

  const { setToken } = useContext(AuthContext);

  const navigate = useNavigate();

  useEffect(() => {
    const accessToken = searchParams.get('access_token');

    const expiryDate = searchParams.get('expiry_date');

    if (accessToken !== null && expiryDate !== null) {
      const decodedJwt = getClaimsFromJwt(accessToken);

      const localUser: LocalUser = {
        id: decodedJwt.uid,
        email: decodedJwt.email,
        firstName: decodedJwt.given_name,
        lastName: decodedJwt.family_name,
        expires: expiryDate,
        accessToken: accessToken,
      };

      setToken(localUser);

      navigate('/');
    }
  }, []);

  const onGoogleLoginClick = (e: React.FormEvent) => {
    e.preventDefault();

    window.location.href = `https://localhost:7287/api/Authentication/GoogleSignInStart`;
  };

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
          <Button
            variant="outline"
            color="gray"
            radius="md"
            fullWidth
            mt="md"
            onClick={onGoogleLoginClick}
          >
            Send message
          </Button>
        </Paper>
      </Flex>
    </>
  );
}

export default LoginPage;
