import { useCopyChecklist } from '@/api/useCopyChecklist'
import { MenuItemProps } from './MenuItemProps'
import { ChecklistType } from '@/api/ApiClient'
import { useRouter } from 'next/navigation'
import { MenuItem } from '@mui/material'

export default function SaveAsTemplateMenuItem({ handleClose, checklist }: MenuItemProps) {
  const copy = useCopyChecklist()
  const router = useRouter()

  async function saveAsTemplate() {
    handleClose()
    const template = await copy.mutateAsync({
      id: checklist.id,
      details: {
        newType: ChecklistType.Template
      }
    })
    router.push(`/checklist/${template.id}/edit`)
  }

  return <MenuItem onClick={saveAsTemplate}>Save as template</MenuItem>
}
