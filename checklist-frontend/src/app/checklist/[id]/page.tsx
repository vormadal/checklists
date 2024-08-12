'use client'
import { Edit as EditIcon } from '@mui/icons-material'
import { Box, IconButton, Typography } from '@mui/material'
import Link from 'next/link'
import { IUpdateChecklistItemDto } from '../../../api/ApiClient'
import { useChecklist } from '../../../api/useChecklist'
import ChecklistItem from '../../../components/checklistItem/ChecklistItem'
import NavigationBar from '../../../components/navigation/NavigationBar'

interface Type {
  params: {
    id: string
  }
}
export default function Page({ params }: Type) {
  const { data, isFetching } = useChecklist(params.id)

  const handleChange = (item: IUpdateChecklistItemDto) => {}

  const handleDelete = () => {}

  if (isFetching) return null

  if (!data) return <Typography>Checklist not found</Typography>

  return (
    <>
      <NavigationBar
        title={data.title}
        backUrl="/"
        actions={
          <IconButton
            component={Link}
            color="inherit"
            href={`/checklist/${data.id}/edit`}
          >
            <EditIcon />
          </IconButton>
        }
      />
      <Box>
        {data.items.map((item) => (
          <ChecklistItem
            key={item.id}
            item={item}
            onChange={handleChange}
            onDelete={handleDelete}
          />
        ))}
      </Box>
    </>
  )
}
