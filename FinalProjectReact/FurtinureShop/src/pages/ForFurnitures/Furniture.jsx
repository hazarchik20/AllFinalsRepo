import clsx from 'clsx';
import React from 'react'
import { Link, useNavigate } from 'react-router-dom';
import { useCart } from '../../context/СartContext';
import { deleteFurniture } from '../../api/services/furnitureServices';
import { useMutation, useQueryClient } from '@tanstack/react-query';

const Furniture = ({ furniture, fromAll, isAdmin }) => {
    const { addItem } = useCart();
    const navigate = useNavigate();
    const {
        Id: id,
        Name: name,
        Description: description,
        Price: price,
        Discount: discount,
        Image: image,
        Count: count,
        Category: category
    } = furniture;
    const queryClient = useQueryClient();

    const deleteMutation = useMutation({
        mutationFn: deleteFurniture,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["furnitures"] });
        },
    });

    const hasDiscount = discount > 0;
    const finalPrice = hasDiscount ? price * (1 - discount / 100) : price;

    return (
        <div className={`furniture-card ${!fromAll ? "furniture-card--details" : ""}`}>
            <div className="image-wrapper">
                <img src={image} alt={name} />
                {hasDiscount && (
                    <div className="discount-badge">-{discount}%</div>
                )}
            </div>

            <div className="content">
                <span className="category">{category.name}</span>
                <h3 className="name">{name}</h3>
                <p className="description">{description}</p>

                <div className="price-section">
                    <div className="prices">
                        <span className="current-price">{finalPrice.toLocaleString()} грн</span>
                        {hasDiscount && (
                            <span className="old-price">{price.toLocaleString()} грн</span>
                        )}
                    </div>
                    <div className={`stock-status ${count > 0 ? 'in-stock' : 'out-of-stock'}`}>
                        {count > 0 ? `В наявності: ${count}` : 'Немає на складі'}
                    </div>
                </div>
            </div>

            <div className="card-footer">
                {
                    fromAll ?
                        <Link to={isAdmin ? `/furnitures/update/${id}` : `/furniture/details/${id}`} className="more-btn btn">{isAdmin ? "Редагувати  →" : "Детальніше →"}</Link>
                        :
                        <button
                            className="buy-btn btn"
                            disabled={count === 0}
                            onClick={() => {
                                addItem(furniture);
                                navigate("/furnitures", { state: { openCart: true } });
                            }}
                        >
                            Додати у кошик
                        </button>
                }
                {isAdmin &&
                    <button className="delete-btn btn" onClick={() => confirm("Are you sure?") && deleteMutation.mutate(id)}>delete</button>
                }
            </div>
        </div>
    );
};

export default Furniture;