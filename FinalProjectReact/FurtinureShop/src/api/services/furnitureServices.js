import api from "../api";


export const getFurnitures = async (params) => {
  const { data } = await api.get("/product", { params });
  return data;
};

export const getFurnitureById = async (id) => {
  const { data } = await api.get(`/product/${id}`);
  return data;
};


export const createFurniture = async (furniture) => {
  const { data } = await api.post("/product", furniture);
  return data;
};


export const updateFurniture = async ({ id, furniture }) => {
  const { data } = await api.put(`/product/${id}`, furniture);
  return data;
};

export const deleteFurniture = async (id) => {
  await api.delete(`/product/${id}`);
};