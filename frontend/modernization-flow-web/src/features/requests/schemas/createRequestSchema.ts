import { z } from 'zod'

export const createRequestSchema = z.object({
  title: z
    .string()
    .min(1, 'O título é obrigatório.'),

  description: z
    .string()
    .min(1, 'A descrição é obrigatória.'),

  amount: z
    .number({
      error: 'O valor é obrigatório.',
    })
    .positive('O valor deve ser maior que zero.'),
})

export type CreateRequestFormData =
  z.infer<typeof createRequestSchema>