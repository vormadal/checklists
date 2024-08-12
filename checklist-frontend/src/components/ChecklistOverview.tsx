import { List, ListSubheader } from '@mui/material'
import { ReactNode } from 'react'
import { IChecklistDto } from '../api/ApiClient'
import { ChecklistListItem, SecondaryTextType } from './checklist/ChecklistListItem'

interface Type {
  checklists: IChecklistDto[]
  subheader?: string
  onClick?: (checklist: IChecklistDto) => void
  secondaryText?: SecondaryTextType
  emptyText?: ReactNode
}

export function ChecklistOverview({ checklists, subheader, secondaryText, emptyText, onClick }: Type) {
  if (checklists.length === 0) return emptyText || <p>empty</p>
  return (
    <List
      dense
      subheader={subheader && <ListSubheader>{subheader}</ListSubheader>}
    >
      {checklists.map((checklist) => (
        <ChecklistListItem
          key={checklist.id}
          checklist={checklist}
          onClick={onClick}
          secondaryText={secondaryText}
        />
      ))}
    </List>
  )
}
