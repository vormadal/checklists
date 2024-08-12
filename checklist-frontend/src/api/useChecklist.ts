import { useQuery } from '@tanstack/react-query'
import api from '@/api'

export function useChecklist(id: string) {
  return useQuery({
    queryKey: ['checklist', parseInt(id)],
    queryFn: async ({ queryKey }) => {
      const [, id] = queryKey
      return await api.getChecklistById(id as number)
    },
  })
}
