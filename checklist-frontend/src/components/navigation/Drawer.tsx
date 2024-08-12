import { Close, Logout } from '@mui/icons-material'
import {
  Box,
  Divider,
  Drawer,
  IconButton,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  styled
} from '@mui/material'
import Link from 'next/link'
import { ChecklistIcon } from '../icons/ChecklistIcon'
import { CompletedChecklistIcon } from '../icons/CompletedChecklistIcon'
import { TemplateIcon } from '../icons/TemplateIcon'

interface Props {
  open?: boolean
  onClose: () => void | Promise<void>
}

const drawerItems = [
  {
    Icon: ChecklistIcon,
    title: 'Open checklists',
    route: '/checklist/open'
  },
  {
    Icon: CompletedChecklistIcon,
    title: 'Completed Checklists',
    route: '/checklist/completed'
  },
  {
    Icon: TemplateIcon,
    title: 'Templates',
    route: '/checklist/template'
  }
]

const DrawerHeader = styled('div')(({ theme }) => ({
  display: 'flex',
  alignItems: 'center',
  padding: theme.spacing(0, 1),
  // necessary for content to be below app bar
  ...theme.mixins.toolbar,
  justifyContent: 'flex-end'
}))
export default function ResponsiveDrawer({ open, onClose }: Props) {
  return (
    <Drawer
      open={open}
      onClose={onClose}
    >
      <Box
        sx={{ width: 280, display: 'flex', flexDirection: 'column', height: '100%' }}
        role="presentation"
        onClick={onClose}
      >
        <DrawerHeader>
          <IconButton
            onClick={onClose}
            color="inherit"
          >
            <Close />
          </IconButton>
        </DrawerHeader>
        <Divider />
        <List sx={{ flex: 1 }}>
          {drawerItems.map(({ Icon, title, route }) => (
            <ListItem
              key={title}
              disablePadding
            >
              <ListItemButton
                component={Link}
                href={route}
              >
                <ListItemIcon>
                  <Icon />
                </ListItemIcon>
                <ListItemText primary={title} />
              </ListItemButton>
            </ListItem>
          ))}
        </List>
        <List>
          <Divider />
          <ListItem disablePadding>
            <ListItemButton>
              <ListItemIcon>
                <Logout />
              </ListItemIcon>
              <ListItemText primary="Log out" />
            </ListItemButton>
          </ListItem>
        </List>
      </Box>
    </Drawer>
  )
}
