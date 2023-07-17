import { createStyles, Header, Group, Box, Burger, rem } from '@mantine/core';
import { Link } from 'react-router-dom';

import { useDisclosure } from '@mantine/hooks';

import {
  IconHome,
  IconSettings,
  IconBell,
  IconBellPlus,
  IconLogout,
} from '@tabler/icons-react';

import logo from '/app-logo.svg';

const useStyles = createStyles((theme) => ({
  // eslint-disable-next-line @typescript-eslint/no-unsafe-assignment
  link: {
    display: 'flex',
    alignItems: 'center',
    height: '100%',
    // eslint-disable-next-line @typescript-eslint/no-unsafe-assignment, @typescript-eslint/no-unsafe-member-access
    paddingLeft: theme.spacing.md,
    paddingRight: theme.spacing.md,
    textDecoration: 'none',
    color: theme.colorScheme === 'dark' ? theme.white : theme.black,
    fontWeight: 500,
    fontSize: theme.fontSizes.sm,

    [theme.fn.smallerThan('sm')]: {
      height: rem(42),
      display: 'flex',
      alignItems: 'center',
      width: '100%',
    },

    ...theme.fn.hover({
      backgroundColor:
        theme.colorScheme === 'dark'
          ? theme.colors.dark[6]
          : theme.colors.gray[0],
    }),
  },

  hiddenMobile: {
    [theme.fn.smallerThan('sm')]: {
      display: 'none',
    },
  },

  hiddenDesktop: {
    [theme.fn.largerThan('sm')]: {
      display: 'none',
    },
  },
}));

function NavBar() {
  const [drawerOpened, { toggle: toggleDrawer, close: closeDrawer }] =
    useDisclosure(false);
  const { classes, theme } = useStyles();

  return (
    <Box pb={120}>
      <Header height={80} px="md">
        <Group position="apart" sx={{ height: '100%' }}>
          <img height={70} src={logo} className="logo" alt="Crawler logo" />
          <Group
            sx={{ height: '100%' }}
            spacing={0}
            className={classes.hiddenMobile}
          >
            <Link to="/" className={classes.link}>
              <IconHome size={rem(17)} style={{ paddingRight: '2px' }} />
              Home
            </Link>
            <Link to="/settings" className={classes.link}>
              <IconSettings size={rem(17)} style={{ paddingRight: '2px' }} />
              Settings
            </Link>
          </Group>

          <Group className={classes.hiddenMobile}>
            <IconBell
              size={rem(30)}
              style={{
                marginRight: '10px',
                padding: '5px',
                borderRadius: '5px',
                backgroundColor: '#E2E2E2',
              }}
            />
            <IconLogout
              size={rem(30)}
              style={{
                marginRight: '10px',
                padding: '5px',
                borderRadius: '5px',
                backgroundColor: '#E2E2E2',
              }}
            />
          </Group>

          <Burger
            opened={drawerOpened}
            onClick={toggleDrawer}
            className={classes.hiddenDesktop}
          />
        </Group>
      </Header>
    </Box>
  );
}

export default NavBar;
