import api from '@/api'
import { useMutation, useQueryClient } from '@tanstack/react-query'

export function useDeleteChecklist() {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: async ({ id }: { id: number }) => {
      return await api.deleteChecklist(id)
    },
    onSuccess(data, variables, context) {
      queryClient.invalidateQueries({
        queryKey: ['checklists']
      })
    }
  })
}
