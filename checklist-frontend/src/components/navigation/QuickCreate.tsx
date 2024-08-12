import { Fab } from '@mui/material'
import { Add as AddIcon } from '@mui/icons-material'
import Link from 'next/link'
import { ChecklistType } from '../../api/ApiClient'

interface Type {
  defaultType?: ChecklistType
}
export function QuickCreate({ defaultType }: Type) {
  const href = defaultType ? `/checklist/create?type=${defaultType}` : '/checklist/create'
  return (
    <Fab
      color="primary"
      aria-label="add"
      sx={{ position: 'absolute', bottom: 16, right: 16 }}
      LinkComponent={Link}
      href={href}
    >
      <AddIcon />
    </Fab>
  )
}
