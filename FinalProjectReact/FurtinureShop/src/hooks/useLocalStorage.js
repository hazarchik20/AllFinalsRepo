import { useState, useEffect } from "react";

export const useLocalStorage = (key, defaultValue) => {
    const [value, setValue] = useState(() => {
        try {
            const savedValue = localStorage.getItem(key);
            return savedValue ? JSON.parse(savedValue) : defaultValue;
        } catch (error) {
            console.error("Помилка при зчитуванні localStorage:", error);
            return defaultValue;
        }
    });

    useEffect(() => {
        try {
            localStorage.setItem(key, JSON.stringify(value));
        } catch (error) {
            console.error("Помилка при записі в localStorage:", error);
        }
    }, [key, value]);

    return [value, setValue];
};