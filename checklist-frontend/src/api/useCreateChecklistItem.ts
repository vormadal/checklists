import api from '@/api'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { CreateChecklistItemDto, ICreateChecklistItemDto } from './ApiClient'

export function useCreateChecklistItem() {
  const queryClient = useQueryClient()
  return useMutation({
    mutationKey: ['create-checklist-item'],
    mutationFn: async ({ item, checklistId }: { item: ICreateChecklistItemDto; checklistId: number }) => {
      return await api.createChecklistItem(checklistId, new CreateChecklistItemDto(item))
    },
    onSuccess(data, variables, context) {
      queryClient.invalidateQueries({
        queryKey: ['checklist', variables.checklistId]
      })
      //TODO something more?
    }
  })
}
