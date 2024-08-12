import api from '@/api'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { UpdateChecklistDto } from './ApiClient'

export function useUpdateChecklist() {
  const queryClient = useQueryClient()
  return useMutation({
    mutationKey: ['update-checklist'],
    mutationFn: async ({ id, checklist }: { id: number; checklist: UpdateChecklistDto }) => {
      return await api.updateChecklist(id, checklist)
    },
    onSuccess(data, variables, context) {
      queryClient.invalidateQueries({
        queryKey: ['checklist', variables.id]
      })
    }
  })
}
