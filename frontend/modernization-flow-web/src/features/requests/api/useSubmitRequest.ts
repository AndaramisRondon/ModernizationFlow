import { useMutation, useQueryClient } from '@tanstack/react-query'
import { requestsApi } from './requestsApi'

export function useSubmitRequest() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: (id: string) =>
      requestsApi.submit(id),

    onSuccess: async (_, id) => {
      await Promise.all([
        queryClient.invalidateQueries({
          queryKey: ['requests'],
        }),

        queryClient.invalidateQueries({
          queryKey: ['requests', id],
        }),
      ])
    },
  })
}