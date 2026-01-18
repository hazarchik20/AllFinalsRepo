import api from "../api";

export const getOrders = async () => {
  const { data } = await api.get("/order");
  return data;
};

export const getOrderById = async (id) => {
  const { data } = await api.get(`/order/${id}`);
  return data;
};

export const getOrdersByUserId  = async (id) => {
  const { data } = await api.get(`/orders/${id}`);
  return data;
};

export const createOrder  = async (order) => {
  const { data } = await api.post("/order", order);
  return data;
};

export const updateOrder  = async (order) => {
  console.log(order);
   
  const { data } = await api.put("/order");
  return data;
};

export const deleteOrder = async (id) => {
  await api.delete(`/order/${id}`);
};