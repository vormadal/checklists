import { MenuItem } from '@mui/material'
import Link from 'next/link'
import { MenuItemProps } from './MenuItemProps'

export default function ListSettingsMenuItem({ checklist }: MenuItemProps) {
  return <MenuItem component={Link} href={`/checklist/${checklist.id}/settings`} >List Settings</MenuItem>
}
