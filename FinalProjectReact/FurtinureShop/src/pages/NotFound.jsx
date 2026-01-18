import React from 'react'
import { Link } from 'react-router-dom'
import "../styles/auth.scss"

const NotFound = () => {
  return (
    <div className='notFound'>
      <h1>404 - Page not found</h1>
      <p>That page not exist</p>
      <button className="link-wrapper">
        <Link to="/" className="text">Back to Home page</Link>
      </button>
    </div>
  )
}

export default NotFound