import { httpClient } from '../../../api/httpClient'
import type {
  CreateRequest,
  Request,
  UpdateRequest,
} from '../types/request'

export const requestsApi = {
  getAll() {
    return httpClient.get<Request[]>('/requests')
  },

  getById(id: string) {
    return httpClient.get<Request>(`/requests/${id}`)
  },

  create(request: CreateRequest) {
    return httpClient.post<Request>('/requests', request)
  },

  update(id: string, request: UpdateRequest) {
    return httpClient.put<void>(`/requests/${id}`, {
      id,
      ...request,
    })
  },

  submit(id: string) {
    return httpClient.post<void>(`/requests/${id}/submit`)
  },

  approve(id: string) {
    return httpClient.post<void>(`/requests/${id}/approve`)
  },

  reject(id: string) {
    return httpClient.post<void>(`/requests/${id}/reject`)
  },
}
