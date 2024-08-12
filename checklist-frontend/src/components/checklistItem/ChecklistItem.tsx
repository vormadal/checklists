import { Delete as DeleteIcon, Reorder as ReorderIcon } from '@mui/icons-material'
import { Box, Checkbox, IconButton, InputBase, Typography } from '@mui/material'
import {
  ChecklistItemDto,
  IUpdateChecklistItemDto
} from '../../api/ApiClient'
import { useDeleteChecklistItem } from '../../api/useDeleteChecklistItem'
import { useUpdateChecklistItem } from '../../api/useUpdateChecklistItem'

interface Type {
  item: ChecklistItemDto
  onChange?: (item: IUpdateChecklistItemDto) => void
  onDelete?: () => void
  editMode?: boolean
}
export default function ChecklistItem({ item, onChange, onDelete, editMode }: Type) {
  const updateItem = useUpdateChecklistItem()
  const deleteItem = useDeleteChecklistItem()

  function handleChecked(e: React.ChangeEvent<HTMLInputElement>, checked: boolean) {
    const update = { ...item, isComplete: checked }
    updateItem.mutate({
      item: update,
      id: item.id,
      checklistId: item.checklistId
    })
    onChange && onChange(update)
  }

  function handleChange(e: React.FocusEvent<HTMLInputElement>) {
    const title = e.target.value
    const update = { ...item, title }
    updateItem.mutate({
      item: update,
      id: item.id,
      checklistId: item.checklistId
    })
    onChange && onChange(update)
  }

  function handleDelete() {
    deleteItem.mutate({ item, checklistId: item.checklistId })
    onDelete && onDelete()
  }

  return (
    <Box sx={{ p: '2px 4px', display: 'flex', alignItems: 'center', width: '100%', marginBottom: 1 }}>
      {editMode ? (
        <IconButton
          sx={{ p: '10px' }}
          aria-label="reorder"
        >
          <ReorderIcon />
        </IconButton>
      ) : (
        <Checkbox
          sx={{ p: '10px' }}
          inputProps={{ 'aria-label': 'check item' }}
          checked={!!item.isComplete}
          onChange={handleChecked}
        ></Checkbox>
      )}
      {editMode ? (
        <InputBase
          sx={{ ml: 1, flex: 1 }}
          placeholder="Enter checklist item..."
          defaultValue={item.title}
          onBlur={handleChange}
          inputProps={{ 'aria-label': 'checklist item' }}
        />
      ) : (
        <Typography sx={{ ml: 1, flex: 1 }}>{item.title}</Typography>
      )}

      {editMode && (
        <>
          <IconButton
            color="default"
            sx={{ p: '10px' }}
            aria-label="remove checklist item"
            onClick={handleDelete}
          >
            <DeleteIcon />
          </IconButton>
        </>
      )}
    </Box>
  )
}
