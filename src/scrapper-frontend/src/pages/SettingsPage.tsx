import { useState, useContext, useEffect } from 'react';
import { Flex, Switch, Group, Notification, Title } from '@mantine/core';
import { IconCheck } from '@tabler/icons-react';
import NavBar from '../components/NavBar';
import { NotificationContext } from '../context/NotificationContext';
import * as signalR from '@microsoft/signalr';
import { AuthContext } from '../context/AuthContext';

function SettingsPage() {
  const { isUserAllow, setIsUserAllow } = useContext(NotificationContext);
  const { token } = useContext(AuthContext);
  const [notification, setNotification] = useState<string[]>([]);

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
      console.log(notification);
      // eslint-disable-next-line @typescript-eslint/no-unsafe-member-access
      console.log(message.message);
      setNotification((prevLog: string[]) => {
        if (prevLog.length >= 5) {
          prevLog.shift();
        }
        console.log(prevLog);
        console.log(message);
        // eslint-disable-next-line @typescript-eslint/no-unsafe-return, @typescript-eslint/no-unsafe-member-access
        return [...prevLog, message.message];
      });
    });
    return async () => {
      newConnection.off('NewScraperLogAdded');
      await newConnection.stop();
    };
  }, [token]);

  const toast = notification.map((item, index) => (
    <Notification
      key={index}
      icon={<IconCheck size="1.2rem" />}
      color="cyan"
      title="Crawler"
      onClick={() => {
        setNotification((prevNotify) =>
          prevNotify.filter((_, i) => i !== index)
        );
      }}
      withCloseButton={false}
    >
      {item}
    </Notification>
  ));

  return (
    <>
      <NavBar />
      <div style={{ marginBottom: '100px' }}>{isUserAllow ? toast : null}</div>
      <Flex justify="center" direction="column" align="center" mt="lg">
        <Title order={4}>
          How would you like to notified about your orders?
        </Title>

        <Group
          mt="lg"
          sx={{
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'flex-start',
          }}
        >
          <Switch value="email" label="Email" />
          <Switch
            checked={isUserAllow}
            onChange={(event) => setIsUserAllow(event.currentTarget.checked)}
            value="notification"
            label="Notification"
          />
        </Group>
      </Flex>
    </>
  );
}

export default SettingsPage;
