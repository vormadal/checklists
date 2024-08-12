import api from '@/api'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { IUpdateChecklistItemDto, UpdateChecklistItemDto } from './ApiClient'

export function useUpdateChecklistItem() {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async ({
      item,
      id,
      checklistId
    }: {
      item: IUpdateChecklistItemDto
      id: number
      checklistId: number
    }) => {
      return await api.updateChecklistItem(checklistId, id, new UpdateChecklistItemDto(item))
    },
    onSuccess(data, variables, context) {
      queryClient.invalidateQueries({
        queryKey: ['checklist', variables.checklistId]
      })
      //TODO something more?
    }
  })
}
