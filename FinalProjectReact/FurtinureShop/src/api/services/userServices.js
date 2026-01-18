import api from "../api";

export const getUsers = async () => {
  console.log("prefer data");
  
  const { data } = await api.get("/user");
  console.log(data);
  
  return data;
};

export const getUserById = async (id) => {
  const { data } = await api.get(`/user/${id}`);
  return data;
};

export const LoginUser  = async (user) => {
  const { data } = await api.post("/user/login", user);
  return data;
};

export const putUser = async (user) => {
  const { data } = await api.put("/user",user);
  return data;
};

export const deleteUser = async (id) => {
  await api.delete(`/user/${id}`);
};