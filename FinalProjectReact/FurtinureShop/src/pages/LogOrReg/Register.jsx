import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../../api/api"
import "../../styles/auth.scss"

const Register = () => {
    const [UserName, setUserName] = useState("");
    const [Email, setEmail] = useState("");
    const [Password, setPassword] = useState("");
    const [error, setError] = useState("");
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();

       
        if (!UserName || !Email || !Password) {
            setError("Заповніть всі поля");
            return;
        }

        if (Password.length < 6) {
            setError("Пароль має містити мінімум 6 символів");
            return;
        }

        try {
            await api.post("/user", {
                UserName,
                Email,
                Password,
                IsAdmin: false,
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
                    value={UserName}
                    onChange={(e) => setUserName(e.target.value)}
                />

                <label>Email</label>
                <input
                    type="email"
                    placeholder="example@gmail.com"
                    value={Email}
                    onChange={(e) => setEmail(e.target.value)}
                />

                <label>Пароль</label>
                <input
                    type="password"
                    placeholder="Мінімум 6 символів"
                    value={Password}
                    onChange={(e) => setPassword(e.target.value)}
                />

                {error && <p className="error">{error}</p>}

                <button type="button" onClick={handleSubmit}>Зареєструватись</button>
           
        </main>
    );
};

export default Register;