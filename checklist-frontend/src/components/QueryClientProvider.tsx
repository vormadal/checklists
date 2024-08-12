'use client'

import { QueryClient, QueryClientProvider } from '@tanstack/react-query'

const queryClient = new QueryClient()

interface Type {
  children: React.ReactNode
}
export default function QueryProvider({ children }: Type) {
  return <QueryClientProvider client={queryClient}>{children}</QueryClientProvider>
}
