import { Link } from 'react-router-dom'
import { useRequests } from '../api/useRequests'
import './RequestsPage.css'

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

function getStatusClass(status: string) {
  return `status-badge status-badge--${status
    .replace(/\s+/g, '-')
    .toLowerCase()}`
}

export function RequestsPage() {
  const {
    data: requests,
    isLoading,
    isError,
    error,
  } = useRequests()

  if (isLoading) {
    return <p>Carregando Solicitações...</p>
  }

  if (isError) {
    return (
      <p>
        Erro ao carregar solicitações:{' '}
        {error instanceof Error ? error.message : 'Unknown error'}
      </p>
    )
  }

  if (!requests || requests.length === 0) {
    return <p>Sem solicitações por enquanto.</p>
  }

  return (
    <section className="requests-page">
      <div className="requests-page__header">
        <h1 className="requests-page__title">Solicitações</h1>

        <Link
          to="/requests/new"
          className="requests-page__new-button"
        >
          Nova Solicitação
        </Link>
      </div>

      <div className="requests-table-wrapper">
        <table className="requests-table">
          <thead>
            <tr>
              <th>Título</th>
              <th>Descrição</th>
              <th>Valor</th>
              <th>Status</th>
              <th>Data de Criação</th>
            </tr>
          </thead>

          <tbody>
            {requests.map((request) => (
              <tr key={request.id}>
                <td>{request.title}</td>
                <td>{request.description}</td>

                <td className="requests-table__amount">
                  {formatAmount(request.amount)}
                </td>

                <td>
                  <span className={getStatusClass(request.status)}>
                    {request.status}
                  </span>
                </td>

                <td className="requests-table__date">
                  {formatDate(request.createdAt)}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </section>
  )
}