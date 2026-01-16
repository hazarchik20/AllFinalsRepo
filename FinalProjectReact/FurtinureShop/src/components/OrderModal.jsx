import React, { useState } from "react";
import { useCart } from "../context/СartContext";
import { useMutation } from "@tanstack/react-query";
import { createOrder } from "../api/services/orderServices";

const OrderModal = ({ isOpen, onClose }) => {
  const { items, total, clearCart } = useCart();
console.log("order modal");

  const [form, setForm] = useState({
    phone: "",
    comment: "",
    city: "",
    street: "",
    houseNumber: "",
    postalCode: "",
  });

  const mutation = useMutation({
    mutationFn: createOrder,
    onSuccess: () => {
      clearCart();
      onClose();
      alert("Замовлення успішно оформлено");
    },
    onError: () => {
      alert("Помилка при створенні замовлення");
    },
  });

  if (!isOpen) return null;

  const handleSubmit = (e) => {
    e.preventDefault();

    const order = {
      phone: form.phone,
      comment: form.comment,
      address: {
        city: form.city,
        street: form.street,
        houseNumber: form.houseNumber,
        postalCode: Number(form.postalCode),
      },
      items: items.map((i) => ({
        furnitureId: i.Id,
        quantity: i.quantity,
      })),
    };

    mutation.mutate(order);
  };

  return (
    <div className="modal-backdrop" onClick={onClose}>
      <div className="order-modal" onClick={(e) => e.stopPropagation()}>
        <h2 className="title">Оформлення замовлення</h2>

        <form onSubmit={handleSubmit}>
          <input
            placeholder="Телефон"
            value={form.phone}
            onChange={(e) => setForm({ ...form, phone: e.target.value })}
            required
          />

          <input
            placeholder="Місто"
            value={form.city}
            onChange={(e) => setForm({ ...form, city: e.target.value })}
            required
          />

          <input
            placeholder="Вулиця"
            value={form.street}
            onChange={(e) => setForm({ ...form, street: e.target.value })}
            required
          />

          <input
            placeholder="Будинок"
            value={form.houseNumber}
            onChange={(e) =>
              setForm({ ...form, houseNumber: e.target.value })
            }
            required
          />

          <input
            type="number"
            placeholder="Поштовий індекс"
            value={form.postalCode}
            onChange={(e) =>
              setForm({ ...form, postalCode: e.target.value })
            }
            required
          />

          <textarea
            placeholder="Коментар"
            value={form.comment}
            onChange={(e) => setForm({ ...form, comment: e.target.value })}
          />

          <div className="order-total">
            <strong>Разом:</strong> {total} грн
          </div>

          <button className="btn-primary" type="submit" disabled={mutation.isLoading}>
            Підтвердити замовлення
          </button>
        </form>
      </div>
    </div>
  );
};

export default OrderModal;
