import { IChecklistDto } from '../../../../api/ApiClient'

export type MenuItemProps = {
  handleClose: () => void
  checklist: IChecklistDto
}
