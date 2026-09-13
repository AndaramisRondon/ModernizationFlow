import { useQuery } from '@tanstack/react-query'
import { requestsApi } from './requestsApi'

export function useRequests() {
  return useQuery({
    queryKey: ['requests'],
    queryFn: () => requestsApi.getAll(),
  })
}