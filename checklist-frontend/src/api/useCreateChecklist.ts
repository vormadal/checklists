import { useMutation, useQueryClient } from '@tanstack/react-query'
import { CreateChecklistDto, ICreateChecklistDto } from './ApiClient'
import api from '@/api'

export function useCreateChecklist() {
  const queryClient = useQueryClient()
  return useMutation({
    mutationKey: ['create-checklist'],
    mutationFn: async (checklist: ICreateChecklistDto) => {
      return await api.createChecklist(new CreateChecklistDto(checklist))
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
