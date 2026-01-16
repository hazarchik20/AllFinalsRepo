import React from 'react'
import { FiSettings } from "react-icons/fi";
import AdminDropdown from "./AdminDropdown.jsx"
import { NavLink } from 'react-router-dom'
const NavBar = ({ isLogined, isAdmin, email, logOut }) => {
    
    const adminMenus = [
        {
            label: "Product",
            actions: [
                { label: "All", to: "/furnitures" },
                { label: "Add", to: "/furnitures/add" },
            ],
        },
        {
            label: "Category",
            actions: [
                { label: "All", to: "/categories" },
            ],
        },
        {
            label: "Users",
            actions: [
                { label: "All", to: "/users" },
            ],
        },
    ];
    return (
        <nav className="navbar">

            <div className="nav-left">
                <NavLink to="/options" className="nav-link">
                   <FiSettings className='silver-antique-text' size={20} />
                </NavLink>
                
                {isLogined && (
                    <p className="email">{email}</p>
                )}
            </div>

            <div className="logo">Furniture</div>

            <div className="nav-right">
                {!isAdmin &&<NavLink to="/" className="nav-link">
                    Home
                </NavLink>}

                {!isAdmin &&<NavLink to="/furnitures" className="nav-link">
                    Furnitures
                </NavLink>
                }
                {!isLogined && (
                    <NavLink to="/login" className={({ isActive }) => isActive ? "active" : ""}>
                        Login
                    </NavLink>
                )}
                {isAdmin && (
                    <div className="admin-wrapper">
                        {adminMenus.map(menu => (
                            <AdminDropdown
                                key={menu.label}
                                label={menu.label}
                                actions={menu.actions}
                            />
                        ))}
                    </div>
                )}
                {isLogined && (
                    <button className="logOut-btn" onClick={logOut}>
                        Вийти
                    </button>
                )}
            </div>
        </nav>
    );
};

export default NavBar