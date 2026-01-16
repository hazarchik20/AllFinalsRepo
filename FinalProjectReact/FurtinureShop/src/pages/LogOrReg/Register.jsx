import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../../api/api"

const Register = () => {
    const [userName, setUserName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();

       
        if (!userName || !email || !password) {
            setError("Заповніть всі поля");
            return;
        }

        if (password.length < 6) {
            setError("Пароль має містити мінімум 6 символів");
            return;
        }

        try {
            await api.post("/user", {
                userName,
                email,
                password,
                isAdmin: false,
                IsBlocked: false
            });

            navigate("/login");
        } catch (err) {
            setError("Помилка реєстрації");
        }
    };

    return (
        <main className="register">
            <h1 className="title">Реєстрація</h1>

           
                <label>Імʼя користувача</label>
                <input
                    type="text"
                    placeholder="Ваше імʼя"
                    value={userName}
                    onChange={(e) => setUserName(e.target.value)}
                />

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
                    placeholder="Мінімум 6 символів"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />

                {error && <p className="error">{error}</p>}

                <button type="button" onClick={handleSubmit}>Зареєструватись</button>
           
        </main>
    );
};

export default Register;