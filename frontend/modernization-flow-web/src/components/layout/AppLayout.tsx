import { NavLink, Outlet } from 'react-router-dom'
import './AppLayout.css'

export function AppLayout() {
  return (
    <div className="app-layout">
      <header className="app-header">
        <div className="app-header__brand">
          Modernization Flow
        </div>

        <nav className="app-nav">
          <NavLink
            to="/"
            className={({ isActive }) =>
              isActive ? 'app-nav__link active' : 'app-nav__link'
            }
          >
            Home
          </NavLink>

          <NavLink
            to="/requests"
            className={({ isActive }) =>
              isActive ? 'app-nav__link active' : 'app-nav__link'
            }
          >
            Requests
          </NavLink>
        </nav>
      </header>

      <main className="app-content">
        <Outlet />
      </main>
    </div>
  )
}