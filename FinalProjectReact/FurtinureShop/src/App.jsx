import { useState } from 'react'
import { Route, Routes } from 'react-router-dom'

import Furnitures from "./pages/ForFurnitures/Furnitures.jsx"
import AddUpdateFurnitures from "./pages/ForFurnitures/AddUpdateFurnitures.jsx"
import FurnituresDetails from "./pages/ForFurnitures/FurnituresDetails.jsx"

import CategoryAdmin from './pages/AdminPages/CategoryAdmin.jsx'
import UsersAdmin from './pages/AdminPages/UsersAdmin.jsx'

import Login from './pages/LogOrReg/Login.jsx'
import Register from './pages/LogOrReg/Register.jsx'

import ProtectedRoute from './components/ProtectedRouted.jsx'
import Home from "./pages/Home.jsx"
import NavBar from './components/NavBar.jsx'
import NotFound from './pages/NotFound.jsx'

import { useAuth } from './hooks/useAuth.js'

import './styles/App.scss'
import { CartProvider } from './context/СartContext.jsx'
import OrdersAdmin from './pages/AdminPages/OrdersAdmin.jsx'

function App() {
  const {
    isLogined,
    user,
    isAuthReady,
    logIn,
    logOut
  } = useAuth();
console.log(user);

  return (
   <CartProvider key={user?.Id ?? "guest"} userId={user?.Id}>
      <div className='app'>
        <NavBar
          isLogined={isLogined}
          isAdmin={user?.IsAdmin}
          user={user}
          logOut={logOut}
        />
        <div className="main-wrapper">
          <Routes>
            <Route path='/' element={<Home />} />

            <Route path='/furnitures' element={<Furnitures isAdmin={user?.IsAdmin} userId ={user?.Id} />} />
            <Route path='/furnitures/add' element={
              <ProtectedRoute isAuthReady={isAuthReady} isValiidity={user?.IsAdmin}>
                <AddUpdateFurnitures />
              </ProtectedRoute>
            } />
            <Route path='/furnitures/update/:id' element={
              <ProtectedRoute isAuthReady={isAuthReady} isValiidity={user?.IsAdmin}>
                <AddUpdateFurnitures />
              </ProtectedRoute>
            } />
            <Route path="/furniture/details/:id" element={<FurnituresDetails />} />

            <Route path="/categories" element={
              <ProtectedRoute isAuthReady={isAuthReady} isValiidity={user?.IsAdmin}>
                <CategoryAdmin />
              </ProtectedRoute>
            } />
            <Route path="/orders/:id" element={
              <ProtectedRoute isAuthReady={isAuthReady} isValiidity={user?.IsAdmin}>
                <OrdersAdmin />
              </ProtectedRoute>
            } />
            <Route path="/users" element={
              <ProtectedRoute isAuthReady={isAuthReady} isValiidity={user?.IsAdmin}>
                <UsersAdmin />
              </ProtectedRoute>
            } />

            <Route path='/register' element={<Register />} />
            <Route path="/login" element={
              <ProtectedRoute isAuthReady={isAuthReady} isValiidity={!isLogined}>
                <Login onLogIn={logIn} />
              </ProtectedRoute>
            } />

            <Route path='*' element={<NotFound />} />
          </Routes>
        </div>


      </div>
    </CartProvider>
  )
}

export default App
