import { MenuItem } from '@mui/material'
import { useRouter } from 'next/navigation'
import { useDeleteChecklist } from '../../../../api/useDeleteChecklist'
import { MenuItemProps } from './MenuItemProps'

export default function DeleteMenuItem({ handleClose, checklist }: MenuItemProps) {
  const mutation = useDeleteChecklist()
  const router = useRouter()

  async function deleteList() {
    handleClose()
    await mutation.mutateAsync({
      id: checklist.id
    })
    router.push(`/`)
  }

  return <MenuItem onClick={deleteList}>Delete</MenuItem>
}
