import { useMutation, useQueryClient } from '@tanstack/react-query'
import { requestsApi } from './requestsApi'

export function useCreateRequest() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: requestsApi.create,

    onSuccess: async () => {
      await queryClient.invalidateQueries({
        queryKey: ['requests'],
      })
    },
  })
}