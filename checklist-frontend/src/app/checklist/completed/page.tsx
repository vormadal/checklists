'use client'
import { ChecklistType } from '../../../api/ApiClient'
import { useChecklists } from '../../../api/useChecklists'
import { ChecklistOverview } from '../../../components/ChecklistOverview'
import NavigationBar from '../../../components/navigation/NavigationBar'
import { QuickCreate } from '../../../components/navigation/QuickCreate'
export default function CompletedChecklistPage() {
  const { data } = useChecklists(ChecklistType.Checklist, true)
  if (!data) return null
  return (
    <>
      <NavigationBar
        backUrl="/"
        title="Completed lists"
      />
      <ChecklistOverview
        checklists={data}
        secondaryText="modified"
        emptyText={<p>You have not finished any lists yet. Click + to create one.</p>}
      />
      <QuickCreate />
    </>
  )
}
