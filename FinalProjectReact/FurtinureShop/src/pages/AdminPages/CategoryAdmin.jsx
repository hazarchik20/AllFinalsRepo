
import React, { useState } from "react";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import {
  getCategories,
  createCategory,
  deleteCategory,
} from "../../api/services/categoryServices";

const CategoryAdmin = () => {
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
                    onClick={() =>
                      confirm("Видалити категорію?") &&
                      deleteMutation.mutate(c.Id)
                    }
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
    </div>
  );
};

export default CategoryAdmin;