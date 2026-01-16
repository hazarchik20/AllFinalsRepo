import api from "../api";


export const getCategories = async () => {
  const { data } = await api.get("/category");
  return data;
};

export const getCategoryById = async (id) => {
  const { data } = await api.get(`/category/${id}`);
  return data;
};

export const createCategory  = async (category) => {
  const { data } = await api.post("/category", category);
  return data;
};


export const deleteCategory = async (id) => {
  await api.delete(`/category/${id}`);
};