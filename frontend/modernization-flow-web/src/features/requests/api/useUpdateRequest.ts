import { useMutation, useQueryClient } from '@tanstack/react-query'
import { requestsApi } from './requestsApi'
import type { UpdateRequest } from '../types/request'

type UpdateRequestParams = {
  id: string
  request: UpdateRequest
}

export function useUpdateRequest() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: ({ id, request }: UpdateRequestParams) =>
      requestsApi.update(id, request),

    onSuccess: async (_, variables) => {
      await Promise.all([
        queryClient.invalidateQueries({
          queryKey: ['requests'],
        }),

        queryClient.invalidateQueries({
          queryKey: ['requests', variables.id],
        }),
      ])
    },
  })
}