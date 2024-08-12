import api from '@/api'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { CopyChecklistDto, ICopyChecklistDto } from './ApiClient'

export function useCopyChecklist() {
  const queryClient = useQueryClient()
  return useMutation({
    mutationKey: ['copy-checklist'],
    mutationFn: async ({ id, details }: { id: number; details: ICopyChecklistDto }) => {
      return await api.copyChecklist(id, new CopyChecklistDto(details))
    },
    onSuccess(data, variables, context) {
      // queryClient.setQueryData(['checklists'], (old: any) => {
      //   return [...old, data]
      // })
      // queryClient.setQueryData(['checklist', data.id], data)
      return data
    }
  })
}
