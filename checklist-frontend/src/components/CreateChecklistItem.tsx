'use client'
import { Add as AddIcon } from '@mui/icons-material'
import { IconButton, InputBase, Paper } from '@mui/material'
import { useState } from 'react'
import { ICreateChecklistItemDto } from '../api/ApiClient'

interface Type {
  onAdd: (item: ICreateChecklistItemDto) => void
}
export default function ChecklistItem({ onAdd }: Type) {
  const [title, setTitle] = useState('')

  function onSubmit(e?: React.MouseEvent<HTMLButtonElement>) {
    e?.preventDefault()
    onAdd({ title, isComplete: false, order: 0 })
    setTitle('')
  }
  return (
    <Paper
      elevation={4}
      sx={{ p: '2px 4px', display: 'flex', alignItems: 'center', width: '100%', marginBottom: 1 }}
    >
      <InputBase
        sx={{ ml: 1, flex: 1 }}
        placeholder="What would you like to add?"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        inputProps={{ 'aria-label': 'checklist item' }}
        onKeyDown={(e) => e.key === 'Enter' && onSubmit()}
      />
      {/* <Divider
        sx={{ height: 28, m: 0.5 }}
        orientation="vertical"
      /> */}
      <IconButton
        color="primary"
        sx={{ p: '10px' }}
        aria-label="add checklist item"
        onClick={onSubmit}
      >
        <AddIcon />
      </IconButton>
    </Paper>
  )
}
