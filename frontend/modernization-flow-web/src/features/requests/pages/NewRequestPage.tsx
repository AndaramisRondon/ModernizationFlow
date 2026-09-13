import { zodResolver } from '@hookform/resolvers/zod'
//import { Link } from 'react-router-dom'
import { useForm } from 'react-hook-form'
import {
  createRequestSchema,
  type CreateRequestFormData,
} from '../schemas/createRequestSchema'
import './NewRequestPage.css'
import { Link, useNavigate } from 'react-router-dom'
import { useCreateRequest } from '../api/useCreateRequest'


export function NewRequestPage() {
  const navigate = useNavigate()
  const createRequest = useCreateRequest()

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<CreateRequestFormData>({
    resolver: zodResolver(createRequestSchema),
  })



  // function onSubmit(data: CreateRequestFormData) {
  //   console.log(data)
  // }
  function onSubmit(data: CreateRequestFormData) {
    createRequest.mutate(data, {
      onSuccess: () => {
        navigate('/requests')
      },
    })
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

        {createRequest.isError && (
          <div className="form-error">
            Não foi possível salvar a solicitação.
          </div>
        )}

        <div className="request-form__actions">
          <button
            type="submit"
            disabled={createRequest.isPending}
          >
            {createRequest.isPending ? 'Salvando...' : 'Salvar'}
          </button>
        </div>
      </form>
    </section>
  )
}