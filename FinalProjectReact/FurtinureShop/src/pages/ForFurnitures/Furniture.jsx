import clsx from 'clsx';
import React from 'react'
import { Link } from 'react-router-dom';
import { useCart } from '../../context/СartContext';
import { deleteFurniture } from '../../api/services/furnitureServices';
import { useMutation, useQueryClient } from '@tanstack/react-query';

const Furniture = ({ furniture, fromAll, isAdmin }) => {
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
    const { addItem } = useCart();
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
        <div className="furniture-card">
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
                        <Link to={isAdmin ? `/addFurniture/${id}` : `/furniture/details/${id}`} className="more-btn btn">{isAdmin ? "Редагувати  →" : "Детальніше →"}</Link>
                        :
                        <button
                            className={"buy-btn btn"}
                            onClick={
                                () => { addItem(furniture) }
                            }
                            disabled={count === 0}>
                            {count > 0 ? 'Додати у кошик' : 'Очікується'}
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