import { Link, useParams } from 'react-router-dom'
import { useRequest } from '../api/useRequest'
import './RequestDetailsPage.css'

function formatAmount(amount: number) {
  return new Intl.NumberFormat('pt-BR', {
    style: 'currency',
    currency: 'BRL',
  }).format(amount)
}

function formatDate(date: string) {
  return new Intl.DateTimeFormat('pt-BR', {
    dateStyle: 'short',
    timeStyle: 'short',
  }).format(new Date(date))
}

export function RequestDetailsPage() {
  const { id } = useParams<{ id: string }>()

  const {
    data: request,
    isLoading,
    isError,
  } = useRequest(id ?? '')

  if (!id) {
    return <p>Solicitação inválida.</p>
  }

  if (isLoading) {
    return <p>Carregando solicitação...</p>
  }

  if (isError || !request) {
    return <p>Não foi possível carregar a solicitação.</p>
  }

  return (
    <section className="request-details-page">
      <div className="request-details-page__header">
        <h1>Detalhes da Solicitação</h1>

        <div className="request-details-page__actions">
          {request.status === 'Draft' && (
            <Link
              to={`/requests/${request.id}/edit`}
              className="request-details-page__edit"
            >
              Editar
            </Link>
          )}

          <Link
            to="/requests"
            className="request-details-page__back"
          >
            Voltar
          </Link>
        </div>
      </div>
      <div className="request-details">
        <div className="request-details__field">
          <span className="request-details__label">
            Título
          </span>

          <span>{request.title}</span>
        </div>

        <div className="request-details__field">
          <span className="request-details__label">
            Descrição
          </span>

          <span>{request.description}</span>
        </div>

        <div className="request-details__field">
          <span className="request-details__label">
            Valor
          </span>

          <span>{formatAmount(request.amount)}</span>
        </div>

        <div className="request-details__field">
          <span className="request-details__label">
            Status
          </span>

          <span>{request.status}</span>
        </div>

        <div className="request-details__field">
          <span className="request-details__label">
            Data de Criação
          </span>

          <span>{formatDate(request.createdAt)}</span>
        </div>

        {request.updatedAt && (
          <div className="request-details__field">
            <span className="request-details__label">
              Última Atualização
            </span>

            <span>{formatDate(request.updatedAt)}</span>
          </div>
        )}
      </div>
    </section>
  )
}