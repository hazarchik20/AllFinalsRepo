import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";

import {
  createFurniture,
  updateFurniture,
  getFurnitureById,
} from "../../api/services/furnitureServices";
import { getCategories } from "../../api/services/categoryServices";

const emptyForm = {
  name: "",
  description: "",
  price: "",
  discount: "",
  count: "",
  image: "",
  categoryId: "",
};

const AddUpdateFurnitures = () => {
  const { id } = useParams();  
  const isEdit = Boolean(id);

  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const [error, setError] = useState("");
  const [form, setForm] = useState(emptyForm);

  const {
    data: furniture,
    isLoading: furnitureLoading,
    isError: furnitureError,
  } = useQuery({
    queryKey: ["furniture", id],
    queryFn: () => getFurnitureById(id),
    enabled: isEdit,
  });

  useEffect(() => {
    if (furniture) {
      setForm({
        name: furniture.Name ?? "",
        description: furniture.Description ?? "",
        price: furniture.Price ?? "",
        discount: furniture.Discount ?? "",
        count: furniture.Count ?? "",
        image: furniture.Image ?? "",
        categoryId: furniture.CategoryId ?? "",
      });
    }
  }, [furniture]);

  const {
    data: categories = [],
    isLoading: categoriesLoading,
    isError: categoriesError,
  } = useQuery({
    queryKey: ["categories"],
    queryFn: getCategories,
  });

  
  const mutation = useMutation({
    mutationFn: (data) =>
      isEdit ? updateFurniture(id, data) : createFurniture(data),
    onSuccess: () => {
      queryClient.invalidateQueries(["furnitures"]);
      navigate("/furnitures");
    },
    onError: () => {
      setError(
        isEdit ? "Помилка оновлення товару" : "Помилка додавання товару"
      );
    },
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (!form.name || !form.price || !form.categoryId) {
      setError("Заповніть обовʼязкові поля");
      return;
    }

    mutation.mutate({
      ...form,
      price: Number(form.price),
      discount: Number(form.discount),
      count: Number(form.count),
      categoryId: Number(form.categoryId),
    });
  };

  if (furnitureLoading || categoriesLoading) return <p>Завантаження...</p>;
  if (furnitureError) return <p>Не вдалося завантажити товар</p>;
  if (categoriesError) return <p>Не вдалося завантажити категорії</p>;

  return (
    <main className="add-furniture">
      <h1 className="title">
        {isEdit ? "Редагувати товар" : "Додати товар"}
      </h1>

      {error && <p className="error">{error}</p>}

      <form onSubmit={handleSubmit}>
        <label>Назва *</label>
        <input name="name" value={form.name} onChange={handleChange} />

        <label>Опис</label>
        <textarea
          name="description"
          value={form.description}
          onChange={handleChange}
        />

        <label>Ціна *</label>
        <input
          type="number"
          name="price"
          value={form.price}
          onChange={handleChange}
        />

        <label>Знижка (%)</label>
        <input
          type="number"
          name="discount"
          value={form.discount}
          onChange={handleChange}
        />

        <label>Кількість</label>
        <input
          type="number"
          name="count"
          value={form.count}
          onChange={handleChange}
        />

        <label>Зображення (URL)</label>
        <input name="image" value={form.image} onChange={handleChange} />

        <label>Категорія *</label>
        <select
          name="categoryId"
          value={form.categoryId}
          onChange={handleChange}
        >
          <option value="">-- Оберіть категорію --</option>
          {categories.map((c) => (
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