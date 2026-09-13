import { useRequests } from '../api/useRequests'

export function RequestsPage() {
  const {
    data: requests,
    isLoading,
    isError,
    error,
  } = useRequests()

  if (isLoading) {
    return <p>Loading requests...</p>
  }

  if (isError) {
    return (
      <p>
        Error loading requests:{' '}
        {error instanceof Error ? error.message : 'Unknown error'}
      </p>
    )
  }

  if (!requests || requests.length === 0) {
    return <p>No requests found.</p>
  }

  return (
    <main>
      <h1>Requests</h1>

      <table>
        <thead>
          <tr>
            <th>Title</th>
            <th>Description</th>
            <th>Amount</th>
            <th>Status</th>
            <th>Created At</th>
          </tr>
        </thead>

        <tbody>
          {requests.map((request) => (
            <tr key={request.id}>
              <td>{request.title}</td>
              <td>{request.description}</td>
              <td>{request.amount}</td>
              <td>{request.status}</td>
              <td>
                {new Date(request.createdAt).toLocaleString('pt-BR')}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </main>
  )
}