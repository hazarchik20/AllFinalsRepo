import { useState, useEffect } from "react";

const STORAGE_KEY = "auth";

export const useAuth = () => {
  const [isLogined, setIsLogined] = useState(false);
  const [user, setUser] = useState(null);
  const [isAuthReady, setIsAuthReady] = useState(false);

  useEffect(() => {
    const stored = localStorage.getItem(STORAGE_KEY);
    if (stored) {
      const parsed = JSON.parse(stored);
      setIsLogined(parsed.isLogined);
      setUser(parsed.user);
    }
    setIsAuthReady(true);
  }, []);

  useEffect(() => {
    if (!isAuthReady) return;
    localStorage.setItem(
      STORAGE_KEY,
      JSON.stringify({ isLogined, user })
    );
  }, [isLogined, user, isAuthReady]);

  const logIn = (userFromApi) => {
    setIsLogined(true);
    setUser(userFromApi);
  };

  const logOut = () => {
    setIsLogined(false);
    setUser(null);
    localStorage.removeItem(STORAGE_KEY);
  };

  return {
    isLogined,
    user,
    isAuthReady,
    logIn,
    logOut,
  };
};
