
import React, { useState } from "react";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import {
  getCategories,
  createCategory,
  deleteCategory,
} from "../../api/services/categoryServices";


import "../../styles/adminPages.scss"
import DeleteModal from "../../components/DeleteModal";
const CategoryAdmin = () => {

  const [isOpenDeleteModal, setIsOpenDeleteModal] = useState(false);
  const [confirmFunctionForDelete, setConfirmFunctionForDelete] = useState(null);
  const queryClient = useQueryClient();
  const [name, setName] = useState("");

  const { data: categories = [], isLoading, isError } = useQuery({
    queryKey: ["categories"],
    queryFn: getCategories,
  });

  const createMutation = useMutation({
    mutationFn: createCategory,
    onSuccess: () => {
      setName("");
      queryClient.invalidateQueries({ queryKey: ["categories"] });
    },
  });

  const deleteMutation = useMutation({
    mutationFn: deleteCategory,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["categories"] });
    },
    onError: (error) => {
      if (error.response && error.response.status === 409) {
        alert("Неможливо видалити категорію: спочатку потрібно видалити всі залежні продукти");
      } else {
        alert("Сталася помилка при видаленні: " + (error.response?.data?.message || error.message));
      }
    }
  });

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!name.trim()) return;

    createMutation.mutate({ name });
  };

  if (isLoading) return <p>Loading...</p>;
  if (isError) return <p>Error loading categories</p>;

  return (
    <div className="category-admin">
    
      <div className="category-table">
        <h2>Категорії</h2>

        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Назва</th>
              <th></th>
            </tr>
          </thead>

          <tbody>
            {categories.map((c) => (
              <tr key={c.Id}>
                <td>{c.Id}</td>
                <td>{c.Name}</td>
                <td>
                  <button
                    className="btn delete"
                    onClick={() =>{setIsOpenDeleteModal(true); setConfirmFunctionForDelete(()=> ()=>deleteMutation.mutate(c.Id))}}
                  >
                    ✖
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

     
      <div className="category-form">
        <h2>Додати категорію</h2>

        <form onSubmit={handleSubmit}>
          <input
            type="text"
            placeholder="Назва категорії"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />

          <button className="btn" disabled={createMutation.isLoading}>
            {createMutation.isLoading ? "Додавання..." : "Додати"}
          </button>
        </form>
      </div>
       <DeleteModal
          isOpen={isOpenDeleteModal} 
          onConfirm={() => { confirmFunctionForDelete?.(); setIsOpenDeleteModal(false);}}
          onCancel={() => setIsOpenDeleteModal(false)}/>
    </div>
  );
};

export default CategoryAdmin;