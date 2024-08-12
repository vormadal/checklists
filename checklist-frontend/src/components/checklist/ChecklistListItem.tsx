import { ListItem, ListItemIcon, ListItemText } from '@mui/material'
import Link from 'next/link'
import { IChecklistDto } from '../../api/ApiClient'
import { formatDate } from '../../utils/DateFunctions'
import { ChecklistIcon } from '../icons/ChecklistIcon'

export type SecondaryTextType = 'created' | 'modified'
interface ListItemComponentType {
  checklist: IChecklistDto
  onClick?: (checklist: IChecklistDto) => void
  secondaryText?: SecondaryTextType
}
export function ChecklistListItem({ checklist, onClick, secondaryText }: ListItemComponentType) {
  const additionProps = onClick
    ? {
        onClick: () => onClick(checklist)
      }
    : {
        component: Link,
        href: `/checklist/${checklist.id}`
      }
  return (
    <ListItem
      {...additionProps}
      key={checklist.id}
    >
      <ListItemIcon>
        <ChecklistIcon checklist={checklist} />
      </ListItemIcon>
      <ListItemText
        primary={checklist.title}
        secondary={
          secondaryText && formatDate(secondaryText === 'created' ? checklist.createdOn : checklist.modifiedOn)
        }
      />
    </ListItem>
  )
}
