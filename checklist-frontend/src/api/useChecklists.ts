import { useQuery } from '@tanstack/react-query'
import api from '@/api'
import { ChecklistType } from './ApiClient'

export function useChecklists(type?: ChecklistType, isComplete?: boolean) {
  return useQuery({
    queryKey: ['checklists', { type, isComplete }],
    queryFn: async ({ queryKey }) => {
      const [_, params] = queryKey
      const { type, isComplete } = params as {
        type: ChecklistType | undefined
        isComplete: boolean | undefined
      }
      return await api.getChecklists(type, isComplete)
    }
  })
}
