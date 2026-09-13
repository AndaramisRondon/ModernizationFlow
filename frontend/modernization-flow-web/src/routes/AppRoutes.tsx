import { BrowserRouter, Route, Routes } from 'react-router-dom'
import { HomePage } from '../pages/HomePage'
import { RequestsPage } from '../features/requests/pages/RequestsPage'

export function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/requests" element={<RequestsPage />} />
      </Routes>
    </BrowserRouter>
  )
}