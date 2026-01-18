import React, { useState } from 'react';
import { useCart } from "../context/СartContext";
import OrderModal from "./OrderModal";
import DeleteModal from './DeleteModal';
import "../styles/modal.scss";

const CartModal = ({ isOpen, onClose, userId }) => {
    const { items, total, removeItem, updateQuantity, clearCart, itemCount } = useCart();
    
    const [isOpenOrderModal, setIsOpenOrderModal] = useState(false);
    const [isOpenDeleteModal, setIsOpenDeleteModal] = useState(false);
    const [confirmFunctionForDelete, setConfirmFunctionForDelete] = useState(null);
    
    const handleDecrease = (item) => updateQuantity(item.Id, item.quantity - 1);
    const handleIncrease = (item) => updateQuantity(item.Id, item.quantity + 1);

    return (
        <>
        {isOpen && (
            <div className="modal-backdrop" onClick={onClose}>
                <div className="cart-modal" onClick={(e) => e.stopPropagation()}>
                    <div className="cart-header">
                        <h2 className="title">Кошик</h2>
                        {itemCount > 0 && <span className="cart-count">{itemCount}</span>}
                        <button className="btn-close" onClick={onClose}>✕</button>
                    </div>

                    {items.length === 0 ? (
                        <div className="cart-empty">
                            <p className="subtitle">Кошик порожній</p>
                            <p className="description">Додайте товари з каталогу</p>
                        </div>
                    ) : (
                        <>
                            <div className="cart-items">
                                {items.map((item) => (
                                    <div className="cart-item" key={item.Id}>
                                        <div className="item-info">
                                            <h4>{item.Name}</h4>
                                            <span className="item-price">{item.Price} грн</span>
                                        </div>

                                        <div className="item-controls">
                                            <div className="quantity-controls">
                                                <button onClick={() => handleDecrease(item)}>−</button>
                                                <span>{item.quantity}</span>
                                                <button onClick={() => handleIncrease(item)}>+</button>
                                            </div>
                                            <button
                                                className="btn-remove"
                                                onClick={() => {
                                                    setConfirmFunctionForDelete(() => () => removeItem(item.Id));
                                                    setIsOpenDeleteModal(true);
                                                }}
                                            >
                                                ✕
                                            </button>
                                        </div>

                                        <div className="item-total">
                                            {item.quantity * item.Price} грн
                                        </div>
                                    </div>
                                ))}
                            </div>

                            <div className="cart-footer">
                                <div className="cart-total">
                                    <strong>Разом:</strong>
                                    <strong>{total} грн</strong>
                                </div>

                                <div className="cart-actions">
                                    <button
                                        className="btn-clear"
                                        onClick={() => {
                                            setConfirmFunctionForDelete(() => clearCart);
                                            setIsOpenDeleteModal(true);
                                        }}
                                    >
                                        Очистити
                                    </button>
                                    <button
                                        className="btn-primary"
                                        onClick={() => {
                                            setIsOpenOrderModal(true);
                                            onClose();
                                        }}
                                    >
                                        Оформити
                                    </button>
                                </div>
                            </div>
                        </>
                    )}
                </div>
            </div>
        )}
        <OrderModal userId={userId} isOpen={isOpenOrderModal}  onClose={() => setIsOpenOrderModal(false)}/>
        <DeleteModal isOpen={isOpenDeleteModal} onConfirm={() => { confirmFunctionForDelete?.(); setIsOpenDeleteModal(false); }} onCancel={() => setIsOpenDeleteModal(false)}/>
        </>
    );
};
export default CartModal;
