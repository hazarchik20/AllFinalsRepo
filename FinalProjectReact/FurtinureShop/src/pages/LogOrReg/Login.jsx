import { useState } from "react";
import { useMutation } from "@tanstack/react-query";
import { Link, useNavigate } from "react-router-dom";
import api from "../../api/api";
import "../../styles/auth.scss"

const Login = ({ onLogIn }) => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const loginMutation = useMutation({
    mutationFn: async ({ email, password }) => {
      const { data } = await api.post("/user/login", {
        email,
        password,
      });
      console.log(data);
      
      return data;
    },

    onSuccess: (user) => {
      onLogIn(user);          
      navigate("/furnitures");
    },

    onError: () => {
      setError("Користувача не знайдено або невірний пароль");
    },
  });

  const handleSubmit = (e) => {
    e.preventDefault();

    if (!email || !password) {
      setError("Заповніть всі поля");
      return;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
      setError("Некоректний email");
      return;
    }

    if (password.length < 6) {
      setError("Пароль має містити мінімум 6 символів");
      return;
    }

    setError("");
    loginMutation.mutate({ email, password });
  };

  return (
    <main className="login">
      <h1 className="title">Вхід до системи</h1>
      <p className="subtitle">Увійдіть, щоб додавати книги</p>

      <label>Email</label>
      <input
        type="email"
        placeholder="example@gmail.com"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
      />

      <label>Пароль</label>
      <input
        type="password"
        placeholder="Введіть пароль"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />

      {(error || loginMutation.isError) && (
        <p className="error">{error}</p>
      )}

      <button onClick={handleSubmit} disabled={loginMutation.isPending}>
        {loginMutation.isPending ? "Перевірка..." : "Увійти"}
      </button>

      <button>
        <Link to="/register" className="text">
          Зареєструватись
        </Link>
      </button>
      
    </main>
  );
};

export default Login;