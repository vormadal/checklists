import api from '@/api'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { ChecklistItemDto } from './ApiClient'

export function useDeleteChecklistItem() {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async ({ item, checklistId }: { item: ChecklistItemDto; checklistId: number }) => {
      return await api.deleteChecklistItem(checklistId, item.id)
    },
    onSuccess(data, variables, context) {
      queryClient.invalidateQueries({
        queryKey: ['checklist', variables.checklistId]
      })
    }
  })
}
