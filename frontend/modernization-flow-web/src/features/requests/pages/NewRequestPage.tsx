import { zodResolver } from '@hookform/resolvers/zod'
import { Link } from 'react-router-dom'
import { useForm } from 'react-hook-form'
import {
  createRequestSchema,
  type CreateRequestFormData,
} from '../schemas/createRequestSchema'
import './NewRequestPage.css'

export function NewRequestPage() {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<CreateRequestFormData>({
    resolver: zodResolver(createRequestSchema),
  })

  function onSubmit(data: CreateRequestFormData) {
    console.log(data)
  }

  return (
    <section className="request-form-page">
      <div className="request-form-page__header">
        <h1>Nova Solicitação</h1>

        <Link
          to="/requests"
          className="request-form-page__back"
        >
          Voltar
        </Link>
      </div>

      <form
        className="request-form"
        onSubmit={handleSubmit(onSubmit)}
      >
        <div className="form-field">
          <label htmlFor="title">Título</label>

          <input
            id="title"
            type="text"
            {...register('title')}
          />

          {errors.title && (
            <span className="form-field__error">
              {errors.title.message}
            </span>
          )}
        </div>

        <div className="form-field">
          <label htmlFor="description">Descrição</label>

          <textarea
            id="description"
            rows={5}
            {...register('description')}
          />

          {errors.description && (
            <span className="form-field__error">
              {errors.description.message}
            </span>
          )}
        </div>

        <div className="form-field">
          <label htmlFor="amount">Valor</label>

          <input
            id="amount"
            type="number"
            step="0.01"
            {...register('amount', {
              valueAsNumber: true,
            })}
          />

          {errors.amount && (
            <span className="form-field__error">
              {errors.amount.message}
            </span>
          )}
        </div>

        <div className="request-form__actions">
          <button type="submit">
            Salvar
          </button>
        </div>
      </form>
    </section>
  )
}