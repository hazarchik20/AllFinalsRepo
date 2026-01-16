import { Navigate } from "react-router-dom";

const ProtectedRoute = ({ isValiidity, children, isAuthReady }) => {
  if (!isAuthReady) {
    return <div>Завантаження...</div>; 
  }
  return isValiidity ? children : <Navigate to="/login" replace />;
};

export default ProtectedRoute;
