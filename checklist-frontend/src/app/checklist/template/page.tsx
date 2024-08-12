'use client'
import { ChecklistType } from '../../../api/ApiClient'
import { useChecklists } from '../../../api/useChecklists'
import { ChecklistOverview } from '../../../components/ChecklistOverview'
import NavigationBar from '../../../components/navigation/NavigationBar'
import { QuickCreate } from '../../../components/navigation/QuickCreate'
export default function TemplatesPage() {
  const { data } = useChecklists(ChecklistType.Template)
  if (!data) return null
  return (
    <>
    <NavigationBar backUrl='/' title='Templates' />
      <ChecklistOverview checklists={data} />
      <QuickCreate defaultType={ChecklistType.Template} />
    </>
  )
}
