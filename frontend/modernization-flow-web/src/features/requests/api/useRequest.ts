import { useQuery } from '@tanstack/react-query'
import { requestsApi } from './requestsApi'

export function useRequest(id: string) {
  return useQuery({
    queryKey: ['requests', id],
    queryFn: () => requestsApi.getById(id),
    enabled: Boolean(id),
  })
}