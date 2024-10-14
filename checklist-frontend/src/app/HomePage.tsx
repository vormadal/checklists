'use client'
import { Container } from '@mui/material'
import { GetServerSidePropsContext } from 'next'
import { Session } from 'next-auth'
import { getServerSession } from 'next-auth/next'
import { useRouter } from 'next/navigation'
import { ChecklistDto, ChecklistType } from '../api/ApiClient'
import { useChecklists } from '../api/useChecklists'
import { useCopyChecklist } from '../api/useCopyChecklist'
import { ChecklistOverview } from '../components/ChecklistOverview'
import { config } from '../auth'

interface Props {
  session: Session | null
}
export default function HomePage({ session }: Props) {
  const { data: checklists } = useChecklists(ChecklistType.Checklist)
  const { data: templates } = useChecklists(ChecklistType.Template)
  const createCopy = useCopyChecklist()
  const router = useRouter()

  async function createFromTemplate(template: ChecklistDto) {
    const checklist = await createCopy.mutateAsync({ id: template.id, details: { newType: ChecklistType.Checklist } })
    router.push(`/checklist/${checklist.id}/edit`)
  }

  return (
    <Container>
      user: {session?.user?.email}
      {checklists && (
        <ChecklistOverview
          subheader="Latest"
          checklists={checklists}
          secondaryText="modified"
          emptyText={<p>You don&apos;t have any lists yet. Click the + </p>}
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
  )
}

export async function getServerSideProps(context: GetServerSidePropsContext) {
  const session = await getServerSession(context.req, context.res, config)
  return {
    props: {
      session: session
    }
  }
}
