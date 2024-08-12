'use client'
import Link from 'next/link'
import { ChecklistDto, ChecklistType } from '../api/ApiClient'
import { useChecklists } from '../api/useChecklists'
import { ChecklistOverview } from '../components/ChecklistOverview'
import NavigationBar from '../components/navigation/NavigationBar'
import { Box, Button, Container } from '@mui/material'
import { QuickCreate } from '../components/navigation/QuickCreate'
import { useCopyChecklist } from '../api/useCopyChecklist'
import { useRouter } from 'next/navigation'

export default function Home() {
  const { data: checklists } = useChecklists(ChecklistType.Checklist)
  const { data: templates } = useChecklists(ChecklistType.Template)
  const createCopy = useCopyChecklist()
  const router = useRouter()

  async function createFromTemplate(template: ChecklistDto) {
    const checklist = await createCopy.mutateAsync({ id: template.id, details: { newType: ChecklistType.Checklist } })
    router.push(`/checklist/${checklist.id}/edit`)
  }

  return (
    <Box sx={{ minHeight: '100svh' }}>
      <NavigationBar title="Checklists" />
      <Container>
        {checklists && (
          <ChecklistOverview
            subheader="Latest"
            checklists={checklists}
            secondaryText='modified'
            emptyText={<p>You don&apos;t have any lists yet. Click the +  </p>}
          />
        )}
        {templates && templates.length > 0 && (
          <ChecklistOverview
            subheader="Create a new..."
            checklists={templates}
            onClick={createFromTemplate}
          />
        )}
      </Container>
      <QuickCreate />
    </Box>
  )
}
