import { useState } from 'react';
import NavBar from '../components/NavBar';
import { Flex, Switch, Group } from '@mantine/core';

function SettingsPage() {
  const [value, setValue] = useState<string[]>([]);

  return (
    <>
      <NavBar />
      <Flex justify="center" align="center" mt="lg">
        <Switch.Group
          value={value}
          size="md"
          onChange={setValue}
          label="How would you like to notified about your orders?"
        >
          <Group
            mt="lg"
            sx={{
              display: 'flex',
              flexDirection: 'column',
              alignItems: 'flex-start',
            }}
          >
            <Switch value="email" label="Email" />
            <Switch value="notification" label="Notification" />
            <Switch value="none" label="None" />
          </Group>
        </Switch.Group>
      </Flex>
    </>
  );
}

export default SettingsPage;
