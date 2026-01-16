import { useState } from "react";
import { Link, NavLink } from "react-router-dom";

const AdminDropdown = ({ label, actions }) => {
  const [open, setOpen] = useState(false);

  return (
    <div className="admin-dropdown">
      <button
        className="admin-select"
        onClick = {() => setOpen(prev => !prev)}
      >
        {label}
        <span className="arrow">▾</span>
      </button>

      {open && (
        <div className="dropdown-menu" >
          {actions.map(a => (
            <NavLink
              key={a.to}
              to={a.to}
              className="dropdown-item"
            >
              {a.label}
            </NavLink>
          ))}
        </div>
      )}
    </div>
  );
};

export default AdminDropdown;
