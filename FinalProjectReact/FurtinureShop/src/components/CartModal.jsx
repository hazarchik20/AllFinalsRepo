import React, { useState } from 'react'
import { useCart } from "../context/СartContext";
import OrderModal from "./OrderModal";

const CartModal = ({ isOpen, onClose }) => {
    const {
        items,
        total,
        removeItem,
        updateQuantity,
        clearCart,
        itemCount,
    } = useCart();
    console.log(total);

    const [isOpenOrderModal, setIsOpenOrderModal] = useState(false);

    const handleDecrease = (item) => {
        updateQuantity(item.Id, item.quantity - 1);
    };

    const handleIncrease = (item) => {
        updateQuantity(item.Id, item.quantity + 1);
    };
    if (isOpenOrderModal) {
        <OrderModal isOpen={isOpenOrderModal} onClose={() => setIsOpenOrderModal(false)} />
    }

    return (
        <>
        {isOpen &&
        (<div className="cart-backdrop" onClick={onClose}>
            <div className="cart-modal" onClick={(e) => e.stopPropagation()}>
                <div className="cart-header">
                    <h2 className="title">Кошик</h2>
                    {itemCount > 0 && (
                        <span className="cart-count">{itemCount}</span>
                    )}
                    <button className="btn-close" onClick={onClose}>
                        ✕
                    </button>
                </div>

                {items.length === 0 ?
                    (
                        <div className="cart-empty">
                            <p className="subtitle">Кошик порожній</p>
                            <p className="description">
                                Додайте товари з каталогу
                            </p>
                        </div>
                    )
                    :
                    (
                        <>
                            <div className="cart-items">
                                {items.map((item) => (
                                    <div className="cart-item" key={item.Id}>
                                        <div className="item-info">
                                            <h4>{item.Name}</h4>
                                            <span className="item-price">
                                                {item.Price} грн
                                            </span>
                                        </div>

                                        <div className="item-controls">
                                            <div className="quantity-controls">
                                                <button
                                                    onClick={() =>
                                                        handleDecrease(item)
                                                    }
                                                >
                                                    −
                                                </button>
                                                <span>{item.quantity}</span>
                                                <button
                                                    onClick={() =>
                                                        handleIncrease(item)
                                                    }
                                                >
                                                    +
                                                </button>
                                            </div>

                                            <button
                                                className="btn-remove"
                                                onClick={() =>
                                                    removeItem(item.Id)
                                                }
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
                                        onClick={clearCart}
                                    >
                                        Очистити
                                    </button>
                                    <button onClick={() => {
                                        setIsOpenOrderModal(true)
                                        onClose()
                                    }} className="btn-primary">
                                        Оформити
                                    </button>
                                </div>
                            </div>
                        </>
                    )
                }
            </div>
        </div>)
        }
         <OrderModal isOpen={isOpenOrderModal} onClose={() => setIsOpenOrderModal(false)} />
        </>
    );
}
export default CartModal;