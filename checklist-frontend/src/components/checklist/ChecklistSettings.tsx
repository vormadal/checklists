import { Global } from '@emotion/react'
import { Skeleton, styled, SwipeableDrawer, TextField, Typography } from '@mui/material'
import { grey } from '@mui/material/colors'
import { FocusEventHandler, useState } from 'react'
import { IChecklistDto, UpdateChecklistDto } from '../../api/ApiClient'
import { useUpdateChecklist } from '../../api/useUpdateChecklist'

const drawerBleeding = 56
const StyledBox = styled('div')(({ theme }) => ({
  backgroundColor: theme.palette.mode === 'light' ? '#fff' : grey[800]
}))

const Puller = styled('div')(({ theme }) => ({
  width: 30,
  height: 6,
  backgroundColor: theme.palette.mode === 'light' ? grey[300] : grey[900],
  borderRadius: 3,
  position: 'absolute',
  top: 8,
  left: 'calc(50% - 15px)'
}))

interface Props {
  checklist: IChecklistDto
}

export function ChecklistSettings({ checklist }: Props) {
  const [open, setOpen] = useState(false)
  const mutation = useUpdateChecklist()
  const toggleDrawer = (newOpen: boolean) => () => {
    setOpen(newOpen)
  }

  const handleSave: FocusEventHandler<HTMLInputElement | HTMLTextAreaElement> = (e) => {
    mutation.mutateAsync({
      id: checklist.id,
      checklist: new UpdateChecklistDto({
        title: e.target.value
      })
    })
    setOpen(false)
  }
  return (
    <>
      <Global
        styles={{
          '.MuiDrawer-root > .MuiPaper-root': {
            height: `calc(50% - ${drawerBleeding}px)`,
            overflow: 'visible'
          }
        }}
      />
      <SwipeableDrawer
        anchor="bottom"
        open={open}
        onClose={toggleDrawer(false)}
        onOpen={toggleDrawer(true)}
        swipeAreaWidth={drawerBleeding}
        disableSwipeToOpen={false}
        ModalProps={{
          keepMounted: true
        }}
      >
        <StyledBox
          sx={{
            position: 'absolute',
            top: -drawerBleeding,
            borderTopLeftRadius: 8,
            borderTopRightRadius: 8,
            visibility: 'visible',
            right: 0,
            left: 0
          }}
        >
          <Puller />
          <Typography sx={{ p: 2, color: 'text.secondary' }}>Settings</Typography>
        </StyledBox>
        <StyledBox
          sx={{
            px: 2,
            pb: 2,
            height: '100%',
            overflow: 'auto'
          }}
        >
          <TextField
            margin="normal"
            required
            fullWidth
            id="title"
            label="Checklist Name"
            placeholder="Enter the checklist name"
            name="title"
            autoFocus
            defaultValue={checklist.title}
            onBlur={handleSave}
          />
        </StyledBox>
      </SwipeableDrawer>
    </>
  )
}
