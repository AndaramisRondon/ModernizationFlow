import { zodResolver } from '@hookform/resolvers/zod'
import { useEffect } from 'react'
import { useForm } from 'react-hook-form'
import { Link, useParams } from 'react-router-dom'
import { useRequest } from '../api/useRequest'
import {
  requestFormSchema,
  type RequestFormData,
} from '../schemas/requestFormSchema'
import './NewRequestPage.css'

export function EditRequestPage() {
  const { id } = useParams<{ id: string }>()

  const {
    data: request,
    isLoading,
    isError,
  } = useRequest(id ?? '')

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<RequestFormData>({
    resolver: zodResolver(requestFormSchema),
    mode: 'onBlur',
  })

  useEffect(() => {
    if (request) {
      reset({
        title: request.title,
        description: request.description,
        amount: request.amount,
      })
    }
  }, [request, reset])

  function onSubmit(data: RequestFormData) {
    console.log(data)
  }

  if (!id) {
    return <p>Solicitação inválida.</p>
  }

  if (isLoading) {
    return <p>Carregando solicitação...</p>
  }

  if (isError || !request) {
    return <p>Não foi possível carregar a solicitação.</p>
  }

  if (request.status !== 'Draft') {
    return (
      <section>
        <p>
          Apenas solicitações em rascunho podem ser editadas.
        </p>

        <Link to={`/requests/${id}`}>
          Voltar
        </Link>
      </section>
    )
  }

  return (
    <section className="request-form-page">
      <div className="request-form-page__header">
        <h1>Editar Solicitação</h1>

        <Link
          to={`/requests/${id}`}
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
          <label htmlFor="title">
            Título
          </label>

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
          <label htmlFor="description">
            Descrição
          </label>

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
          <label htmlFor="amount">
            Valor
          </label>

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