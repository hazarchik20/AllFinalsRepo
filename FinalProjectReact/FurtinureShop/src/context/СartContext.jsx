import { createContext, useContext, useReducer, useMemo, useEffect } from "react";
import { useLocalStorage } from "../hooks/useLocalStorage";
import { AiTwotoneAlert } from "react-icons/ai";

const CartContext = createContext(null);

const CART_ACTIONS = {
    ADD: "ADD_ITEM",
    REMOVE: "REMOVE_ITEM",
    UPDATE: "UPDATE_QUANTITY",
    CLEAR: "CLEAR_CART",
};

const cartReducer = (state, action) => {
    switch (action.type) {
        case CART_ACTIONS.ADD: {
            const existingItem = state.find(
                (item) => item.Id === action.payload.Id
            );

            if (existingItem) {
                return state.map((item) =>
                    item.Id === action.payload.Id
                        ? { ...item, quantity: item.quantity + 1 }
                        : item
                );
            }

            return [...state, { ...action.payload, quantity: 1 }];
        }

        case CART_ACTIONS.REMOVE:
           return state.filter((item) => item.Id !== action.payload);

        case CART_ACTIONS.UPDATE: {
            const { id, quantity } = action.payload;

            if (quantity <= 0) {
                return state.filter((item) => item.Id !== id);
            }

            return state.map((item) =>
                item.Id === id ? { ...item, quantity } : item
            );
        }

        case CART_ACTIONS.CLEAR:
            return [];

        default:
            return state;
    }
};

export const CartProvider = ({ children }) => {
    const [storedItems, setStoredItems] = useLocalStorage("cart", []);
    const [items, dispatch] = useReducer(cartReducer, storedItems);

    useEffect(() => {
        setStoredItems(items);
    }, [items, setStoredItems]);

    const addItem = (item) => {
        dispatch({ type: CART_ACTIONS.ADD, payload: item });
    };

    const removeItem = (id) => {
        dispatch({ type: CART_ACTIONS.REMOVE, payload: id });
    };

    const updateQuantity = (id, quantity) => {
        dispatch({
            type: CART_ACTIONS.UPDATE,
            payload: { id, quantity },
        });
    };

    const clearCart = () => {
        dispatch({ type: CART_ACTIONS.CLEAR });
    };

   
    const itemCount = items.reduce(
        (sum, item) => sum + item.quantity,
        0
    );

    const total = items.reduce(
        (sum, item) => sum + item.Price * item.quantity,
        0
    );

    console.log(items);
    
    const value = {
        items,
        total,
        itemCount,
        addItem,
        removeItem,
        updateQuantity,
        clearCart,
    };

    return (
        <CartContext.Provider value={value}>
            {children}
        </CartContext.Provider>
    );
};

export const useCart = () => {
    const context = useContext(CartContext);
    if (!context) {
        throw new Error("useCart must be used within CartProvider");
    }
    return context;
};