import React, { useEffect, useState } from "react";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { getOrdersByUserId } from "../api/services/orderServices";
import { putUser } from "../api/services/userServices";

import "../styles/modal.scss";

const ProfileModal = ({ user, onClose }) => {
  const queryClient = useQueryClient();

  console.log(user);
  
  const [form, setForm] = useState({
    UserName: user?.UserName || "",
    Email: user?.Email || "",
    OldPassword: "",
    NewPassword: "",
  });

    useEffect(() => {
    if (user) {
        setForm({
        UserName: user.UserName || "",
        Email: user.Email || "",
        OldPassword: "",
        NewPassword: "",
        });
    }
    }, [user]);

  const { data: orders, isLoading } = useQuery({
    queryKey: ["userOrders", user?.Id],
    queryFn: () => getOrdersByUserId(user.Id),
  });

  const mutation = useMutation({
    mutationFn: putUser,
    onSuccess: (data) => {
      queryClient.setQueryData(["user"], data); 
      queryClient.invalidateQueries({ queryKey: ["user"] });
      setForm(prev => ({ ...prev, OldPassword: "", NewPassword: "" }));
      alert("Профіль оновлено успішно!");
    },
    onError: (error) => {
        alert("Помилка при оновленні: " + error.message);
    }
  });

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    
    if (form.OldPassword || form.NewPassword) {
      if (!form.OldPassword || !form.NewPassword) {
        alert("Для зміни пароля заповни обидва поля");
        return;
      }

      if (form.OldPassword !== user.Password) {
        alert("Старий пароль невірний");
        return;
      }
    }

    mutation.mutate({
      Id: user.Id,
      UserName: form.UserName,
      Email: form.Email,
      Password: form.NewPassword ? form.NewPassword : user.Password,
      IsAdmin: user.IsAdmin,
      IsBlocked: user.IsBlocked,
      CartId: user.CartId,
    });
  };

  return (
    <div className="modal-backdrop">
      <div className="modal profile-modal">
        <h2>Профіль користувача</h2>

        <form onSubmit={handleSubmit} className="profile-form">
          <label>
            Нікнейм
            <input
              type="text"
              name="UserName"
              value={form.UserName}
              onChange={handleChange}
            />
          </label>

          <label>
            Email
            <input
              type="email"
              name="Email"
              value={form.Email}
              onChange={handleChange}
            />
          </label>

          <label>
            Старий пароль
            <input
              type="password"
              name="OldPassword"
              value={form.OldPassword}
              onChange={handleChange}
            />
          </label>

          <label>
            Новий пароль
            <input
              type="password"
              name="NewPassword"
              value={form.NewPassword}
              onChange={handleChange}
            />
          </label>

          <div className="actions">
            <button type="submit" className="btn">
              Зберегти
            </button>
            <button
              type="button"
              className="btn cancel"
              onClick={onClose}
            >
              Закрити
            </button>
          </div>
        </form>

        <hr />

        <h3>Мої замовлення</h3>

        {isLoading ? (
          <p>Завантаження...</p>
        ) : orders?.length ? (
          <table className="orders-table">
            <thead>
              <tr>
                <th>ID</th>
                <th>Статус</th>
                <th>Телефон</th>
                <th>Коментар</th>
              </tr>
            </thead>
            <tbody>
              {orders.map((o) => (
                <tr key={o.Id}>
                  <td>{o.Id}</td>
                  <td>{o.State}</td>
                  <td>{o.Phone}</td>
                  <td>{o.Comment}</td>
                </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <p>Замовлень ще немає</p>
        )}
      </div>
    </div>
  );
};

export default ProfileModal;
