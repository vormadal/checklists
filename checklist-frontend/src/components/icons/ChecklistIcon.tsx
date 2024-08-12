import { BallotOutlined as OpenChecklistIcon } from '@mui/icons-material'
import { ChecklistType, IChecklistDto } from '../../api/ApiClient'
import { CompletedChecklistIcon } from './CompletedChecklistIcon'
import { TemplateIcon } from './TemplateIcon'

interface Props {
  checklist?: IChecklistDto
}

export function ChecklistIcon({ checklist }: Props) {
  if (checklist?.isComplete) {
    return <CompletedChecklistIcon />
  }

  if (checklist?.type === ChecklistType.Template) {
    return <TemplateIcon />
  }

  return <OpenChecklistIcon />
}
