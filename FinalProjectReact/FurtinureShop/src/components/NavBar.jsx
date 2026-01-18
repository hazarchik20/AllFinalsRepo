import React, { useState } from 'react'
import { FiSettings } from "react-icons/fi";
import AdminDropdown from "./AdminDropdown.jsx"
import { NavLink } from 'react-router-dom'
import "../styles/navBar.scss"
import ProfileModal from './ProfileModal.jsx';
const NavBar = ({ isLogined, isAdmin, user, logOut }) => {
    
    const [isProfileModalOpen,setIsProfileModalOpen] = useState(false)
    
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
                <button onClick={()=>setIsProfileModalOpen(true)} className="email-btn">{user?.Email}</button>
               
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
            {isProfileModalOpen && <ProfileModal user = {user} onClose ={()=>setIsProfileModalOpen(false)}/>}
        </nav>
    );
};

export default NavBar