import React from "react";
import { useParams } from "react-router-dom";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";

import { getOrdersByUserId, updateOrder } from "../../api/services/orderServices";
import "../../styles/adminPages.scss"

const orderStates = [
  "CREATED",
  "WAITING",
  "SENT",
  "DELIVERED",
  "CANCELLED",
];

const OrdersAdmin = () => {
  const { id } = useParams(); 
  const queryClient = useQueryClient();

  const { data: orders, isLoading } = useQuery({
    queryKey: ["userOrders", id],
    queryFn: () => getOrdersByUserId(id),
  });

  const mutation = useMutation({
    mutationFn: ({ orderId, state }) =>
      updateOrder(orderId, { state }),
    onSuccess: () => {
      queryClient.invalidateQueries(["userOrders", id]);
    },
  });

  if (isLoading) return <p>Loading...</p>;

  return (
    <div className="orders-admin">
      <h1>Замовлення користувача #{id}</h1>

      {orders?.length === 0 && <p>Немає замовлень</p>}

      <table className="orders-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Телефон</th>
            <th>Коментар</th>
            <th>Адреса</th>
            <th>Статус</th>
            <th></th>
          </tr>
        </thead>

        <tbody>
          {orders.map((order) => (
            <tr key={order.Id}>
              <td>{order.Id}</td>
              <td>{order.Phone}</td>
              <td>{order.Comment}</td>
              <td>{`${order.Address?.City} ${order.Address?.Street} ${order.Address?.HouseNumber}`}</td>

              <td>
                <select
                  defaultValue={order.State}
                  onChange={(e) =>
                    mutation.mutate({
                      orderId: order.Id,
                      state: e.target.value,
                    })
                  }
                >
                  {orderStates.map((s) => (
                    <option key={s} value={s}>
                      {s}
                    </option>
                  ))}
                </select>
              </td>

              <td>
                {mutation.isPending && <span>⏳</span>}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default OrdersAdmin;