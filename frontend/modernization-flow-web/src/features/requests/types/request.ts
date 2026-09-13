export type Request = {
  id: string
  title: string
  description: string
  amount: number
  status: string
  createdAt: string
  updatedAt: string | null
}

export type CreateRequest = {
  title: string
  description: string
  amount: number
}

export type UpdateRequest = {
  title: string
  description: string
  amount: number
}