import { BrowserRouter, Route, Routes } from 'react-router-dom'
import { AppLayout } from '../components/layout/AppLayout'
import { HomePage } from '../pages/HomePage'
import { RequestsPage } from '../features/requests/pages/RequestsPage'
import { NewRequestPage } from '../features/requests/pages/NewRequestPage'
import { RequestDetailsPage } from '../features/requests/pages/RequestDetailsPage'

export function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<AppLayout />}>
          <Route path="/" element={<HomePage />} />
          <Route path="/requests" element={<RequestsPage />} />
          <Route path="/requests/new" element={<NewRequestPage />} />
          <Route
              path="/requests/:id"
              element={<RequestDetailsPage />}
            />
                      
        </Route>
      </Routes>
    </BrowserRouter>
  )
}