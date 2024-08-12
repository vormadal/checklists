'use client'
import { ChecklistItemDto, ChecklistType, IChecklistDto, ICreateChecklistItemDto } from '@/api/ApiClient'
import { MoreVert as MoreVertIcon } from '@mui/icons-material'
import { Box, Container, IconButton, Menu, MenuItem } from '@mui/material'
import { useRouter } from 'next/navigation'
import React, { useEffect, useState } from 'react'
import { useChecklist } from '../../../../api/useChecklist'
import { useCreateChecklistItem } from '../../../../api/useCreateChecklistItem'
import ChecklistItem from '../../../../components/checklistItem/ChecklistItem'
import CreateChecklistItem from '../../../../components/CreateChecklistItem'
import NavigationBar from '../../../../components/navigation/NavigationBar'
import SaveAsTemplateMenuItem from './SaveAsTemplateMenuItem'
import DeleteMenuItem from './DeleteMenuItem'
import ListSettingsMenuItem from './ListSettingsMenuItem'
import { ChecklistSettings } from '../../../../components/checklist/ChecklistSettings'

interface Type {
  params: {
    id: string
  }
}

interface MenuType {
  checklist: IChecklistDto
}
function EditChecklistMenu({ checklist }: MenuType) {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null)
  const open = Boolean(anchorEl)

  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget)
  }
  const handleClose = () => {
    setAnchorEl(null)
  }

  const props = {
    handleClose,
    checklist
  }

  return (
    <div>
      <IconButton
        color="inherit"
        aria-label="more"
        id="long-button"
        aria-controls={open ? 'long-menu' : undefined}
        aria-expanded={open ? 'true' : undefined}
        aria-haspopup="true"
        onClick={handleClick}
      >
        <MoreVertIcon />
      </IconButton>
      <Menu
        id="long-menu"
        MenuListProps={{
          'aria-labelledby': 'long-button'
        }}
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
      >
        {checklist.type !== ChecklistType.Template && <SaveAsTemplateMenuItem {...props} />}
        <DeleteMenuItem {...props}>Delete</DeleteMenuItem>
      </Menu>
      <ChecklistSettings checklist={checklist} />
    </div>
  )
}

export default function EditChecklist({ params }: Type) {
  const { data } = useChecklist(params.id)
  const [items, setItems] = React.useState<ChecklistItemDto[]>([])
  const createItem = useCreateChecklistItem()

  useEffect(() => {
    if (data) {
      setItems(data.items)
    }
  }, [data])

  const addItem = async (item: ICreateChecklistItemDto) => {
    await createItem.mutateAsync({
      item: { ...item, order: items.reduce((max, item) => Math.max(max, item.order), 0) + 100 },
      checklistId: parseInt(params.id)
    })
  }

  if (!data) return null

  return (
    <>
      <NavigationBar
        title={data?.title + (data.type === ChecklistType.Template ? ' (Template)' : '')}
        backUrl={`/checklist/${params.id}`}
        actions={<EditChecklistMenu checklist={data} />}
      />
      <Container>
        <Box
          gap={1}
          sx={{ mt: 1 }}
        >
          {items.map((item) => (
            <ChecklistItem
              editMode
              key={item.id}
              item={item}
            />
          ))}
          <CreateChecklistItem onAdd={addItem} />
        </Box>
      </Container>
    </>
  )
}
