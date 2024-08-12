'use client'
import { ChecklistType } from '../../../api/ApiClient'
import { useChecklists } from '../../../api/useChecklists'
import { ChecklistOverview } from '../../../components/ChecklistOverview'
import NavigationBar from '../../../components/navigation/NavigationBar'
import { QuickCreate } from '../../../components/navigation/QuickCreate'
export default function OpenChecklistPage() {
  const { data } = useChecklists(ChecklistType.Checklist, false)
  if (!data) return null
  return (
    <>
    <NavigationBar backUrl='/' title='Open lists' />
      <ChecklistOverview checklists={data} />
      <QuickCreate />
    </>
  )
}
