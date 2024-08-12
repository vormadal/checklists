'use client'
import MenuIcon from '@mui/icons-material/Menu'
import AppBar from '@mui/material/AppBar'
import Box from '@mui/material/Box'
import IconButton from '@mui/material/IconButton'
import Toolbar from '@mui/material/Toolbar'
import Typography from '@mui/material/Typography'
import React from 'react'
import { ArrowBack as ArrowBackIcon } from '@mui/icons-material'
import { useRouter } from 'next/navigation'
import ResponsiveDrawer from './Drawer'

interface Type {
  title?: string
  actions?: React.ReactNode
  backUrl?: string
}
export default function NavigationBar({ title, actions, backUrl }: Type) {
  const router = useRouter()
  const [showMenu, setShowMenu] = React.useState(false)

  return (
    <Box sx={{ flexGrow: 1, marginBottom: 2 }}>
      <ResponsiveDrawer
        open={showMenu}
        onClose={() => setShowMenu(false)}
      />
      <AppBar position="static">
        <Toolbar>
          {backUrl ? (
            <IconButton
              size="large"
              edge="start"
              color="inherit"
              aria-label="go back"
              sx={{ mr: 2 }}
              onClick={() => router.push(backUrl)}
            >
              <ArrowBackIcon />
            </IconButton>
          ) : (
            <IconButton
              size="large"
              edge="start"
              color="inherit"
              aria-label="menu"
              sx={{ mr: 2 }}
              onClick={() => setShowMenu(true)}
            >
              <MenuIcon />
            </IconButton>
          )}
          <Typography
            component="div"
            variant="h6"
            sx={{ flexGrow: 1 }}
          >
            {title}
          </Typography>
          {actions}
        </Toolbar>
      </AppBar>
    </Box>
  )
}
