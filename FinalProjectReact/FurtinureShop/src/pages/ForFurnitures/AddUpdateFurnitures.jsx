import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";

import {
  createFurniture,
  updateFurniture,
  getFurnitureById,
} from "../../api/services/furnitureServices";
import { getCategories } from "../../api/services/categoryServices";

import "../../styles/furnitures.scss";

const emptyForm = {
  Name: "",
  Description: "",
  Price: "",
  Discount: "",
  Count: "",
  Image: "",
  CategoryId: "",
};

const AddUpdateFurnitures = () => {
  const { id } = useParams();
  const isEdit = Boolean(id);

  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const [error, setError] = useState("");
  const [Form, setForm] = useState(emptyForm);

  // Отримання товару
  const {
    data: Furniture,
    isLoading: furnitureLoading,
    isError: furnitureError,
  } = useQuery({
    queryKey: ["furniture", id],
    queryFn: () => getFurnitureById(id),
    enabled: isEdit,
  });

  useEffect(() => {
    if (Furniture) {
      setForm({
        Name: Furniture.Name ?? "",
        Description: Furniture.Description ?? "",
        Price: Furniture.Price ?? "",
        Discount: Furniture.Discount ?? "",
        Count: Furniture.Count ?? "",
        Image: Furniture.Image ?? "",
        CategoryId: Furniture.CategoryId ?? "",
      });
    }
  }, [Furniture]);

  // Отримання категорій
  const {
    data: Categories = [],
    isLoading: categoriesLoading,
    isError: categoriesError,
  } = useQuery({
    queryKey: ["categories"],
    queryFn: getCategories,
  });

  // Мутація
  const mutation = useMutation({
    mutationFn: (data) =>
      isEdit ? updateFurniture({ id, furniture: data }) : createFurniture(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["furnitures"] });
      queryClient.invalidateQueries({ queryKey: ["furniture", id] });
      navigate("/furnitures");
    },
    onError: () => {
      setError(isEdit ? "Помилка оновлення товару" : "Помилка додавання товару");
    },
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (!Form.Name || !Form.Price || !Form.CategoryId) {
      setError("Заповніть обовʼязкові поля");
      return;
    }

    mutation.mutate({
      ...Form,
      Price: Number(Form.Price),
      Discount: Number(Form.Discount),
      Count: Number(Form.Count),
      CategoryId: Number(Form.CategoryId),
    });
  };

  if (furnitureLoading || categoriesLoading) return <p>Завантаження...</p>;
  if (furnitureError) return <p>Не вдалося завантажити товар</p>;
  if (categoriesError) return <p>Не вдалося завантажити категорії</p>;

  return (
    <main className="add-furniture">
      <h1 className="title">{isEdit ? "Редагувати товар" : "Додати товар"}</h1>

      {error && <p className="error">{error}</p>}

      <form onSubmit={handleSubmit}>
        <label>Назва *</label>
        <input name="Name" value={Form.Name} onChange={handleChange} />

        <label>Опис</label>
        <textarea
          name="Description"
          value={Form.Description}
          onChange={handleChange}
        />

        <label>Ціна *</label>
        <input
          type="number"
          name="Price"
          value={Form.Price}
          onChange={handleChange}
        />

        <label>Знижка (%)</label>
        <input
          type="number"
          name="Discount"
          value={Form.Discount}
          onChange={handleChange}
        />

        <label>Кількість</label>
        <input
          type="number"
          name="Count"
          value={Form.Count}
          onChange={handleChange}
        />

        <label>Зображення (URL)</label>
        <input name="Image" value={Form.Image} onChange={handleChange} />

        <label>Категорія *</label>
        <select
          name="CategoryId"
          value={Form.CategoryId}
          onChange={handleChange}
        >
          <option value="">-- Оберіть категорію --</option>
          {Categories.map((c) => (
            <option key={c.Id} value={c.Id}>
              {c.Name}
            </option>
          ))}
        </select>

        <button type="submit" disabled={mutation.isLoading}>
          {mutation.isLoading
            ? isEdit
              ? "Оновлення..."
              : "Додавання..."
            : isEdit
            ? "Оновити"
            : "Додати"}
        </button>
      </form>
    </main>
  );
};

export default AddUpdateFurnitures;
