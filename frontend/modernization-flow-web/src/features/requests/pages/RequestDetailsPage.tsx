import { Link, useParams } from 'react-router-dom'
import { confirmDialog } from '../../../components/common/dialogs/confirmDialog'
import { useRequest } from '../api/useRequest'
import { useSubmitRequest } from '../api/useSubmitRequest'
import { useApproveRequest } from '../api/useApproveRequest'
import { useRejectRequest } from '../api/useRejectRequest'
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
  
  const submitRequest = useSubmitRequest()
  const approveRequest = useApproveRequest()
  const rejectRequest = useRejectRequest()

  async function handleSubmitRequest() {
    if (!id) {
      return
    }

    const confirmed = await confirmDialog({
      title: 'Enviar para análise?',
      text: 'A solicitação será enviada para análise e não poderá mais ser editada.',
      confirmText: 'Sim, enviar',
    })

    if (!confirmed) {
      return
    }

    submitRequest.mutate(id)
  }

  async function handleApproveRequest() {
    if (!id) {
      return
    }

    const confirmed = await confirmDialog({
      title: 'Aprovar solicitação?',
      text: 'A solicitação será aprovada.',
      confirmText: 'Sim, aprovar',
    })

    if (!confirmed) {
      return
    }

    approveRequest.mutate(id)
  }

  async function handleRejectRequest() {
    if (!id) {
      return
    }

    const confirmed = await confirmDialog({
      title: 'Reprovar solicitação?',
      text: 'A solicitação será reprovada.',
      confirmText: 'Sim, reprovar',
    })

    if (!confirmed) {
      return
    }

    rejectRequest.mutate(id)
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

  return (
    <section className="request-details-page">
      <div className="request-details-page__header">
        <h1>Detalhes da Solicitação</h1>

        <div className="request-details-page__actions">

          {request.status === 'UnderReview' && (
            <>
              <button
                type="button"
                className="request-details-page__approve"
                disabled={
                  approveRequest.isPending ||
                  rejectRequest.isPending
                }
                onClick={handleApproveRequest}
              >
                {approveRequest.isPending
                  ? 'Aprovando...'
                  : 'Aprovar'}
              </button>

              <button
                type="button"
                className="request-details-page__reject"
                disabled={
                  approveRequest.isPending ||
                  rejectRequest.isPending
                }
                onClick={handleRejectRequest}
              >
                {rejectRequest.isPending
                  ? 'Reprovando...'
                  : 'Reprovar'}
              </button>
            </>
          )}

          {request.status === 'Draft' && (
            <>
              <Link
                to={`/requests/${request.id}/edit`}
                className="request-details-page__edit"
              >
                Editar
              </Link>

              <button
                type="button"
                className="request-details-page__submit"
                disabled={submitRequest.isPending}
                onClick={handleSubmitRequest}
              >
                {submitRequest.isPending
                  ? 'Enviando...'
                  : 'Enviar para análise'}
              </button>
            </>
          )}

          <Link
            to="/requests"
            className="request-details-page__back"
          >
            Voltar
          </Link>
        </div>

      </div>

      {submitRequest.isError && (
        <div className="request-details-page__error">
          Não foi possível enviar a solicitação para análise.
        </div>
      )}

      {approveRequest.isError && (
        <div className="request-details-page__error">
          Não foi possível aprovar a solicitação.
        </div>
      )}

      {rejectRequest.isError && (
        <div className="request-details-page__error">
          Não foi possível reprovar a solicitação.
        </div>
      )}      

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

          <span>{request.statusDescription}</span>
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