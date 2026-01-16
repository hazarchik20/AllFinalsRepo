import React, { useState } from 'react'
import Furniture from "./Furniture.jsx"
import { useQuery } from '@tanstack/react-query'
import { getFurnitures } from '../../api/services/furnitureServices'
import CartModal from '../../components/CartModal.jsx';

const Furnitures = ({isAdmin}) => {

    
    const [isCartOpen, setIsCartOpen] = useState(false);
    const [filters, setFilters] = useState({
        page: 1,
        pageSize: 8,
        search: "",
        categoryId: "",
        priceFrom: "",
        priceTo: "",
        sortBy: "name",
        sortDir: "asc",
    });

    const { data, isLoading, isError, error } = useQuery({
        queryKey: ["furnitures", filters],
        queryFn: () => getFurnitures(filters),
        placeholderData: (prev) => prev,
    });

    const furnitures = data?.items ?? [];
    const totalPages = Math.max(
        1,
        Math.ceil((data?.total ?? 0) / filters.pageSize)
    );

    if (isLoading) return <p>Loading...</p>;
    if (isError) return <p>Error: {error.message}</p>;

    return (
        <div className="furniture-page">

            <aside className="filters-sidebar">
                <h3>Фільтри</h3>

                <input
                    type="text"
                    placeholder="Пошук..."
                    value={filters.search}
                    onChange={(e) =>
                        setFilters(prev => ({
                            ...prev,
                            search: e.target.value,
                            page: 1,
                        }))
                    }
                />

                <select
                    value={filters.categoryId}
                    onChange={(e) =>
                        setFilters(prev => ({
                            ...prev,
                            categoryId: e.target.value,
                            page: 1,
                        }))
                    }
                >
                    <option value="">Усі категорії</option>
                    <option value="1">Мʼякі меблі</option>
                    <option value="2">Столи</option>
                </select>

                <input
                    type="number"
                    placeholder="Ціна від"
                    value={filters.priceFrom}
                    onChange={(e) =>
                        setFilters(prev => ({ ...prev, priceFrom: e.target.value, page: 1 }))
                    }
                />

                <input
                    type="number"
                    placeholder="Ціна до"
                    value={filters.priceTo}
                    onChange={(e) =>
                        setFilters(prev => ({ ...prev, priceTo: e.target.value, page: 1 }))
                    }
                />

                <select
                    value={`${filters.sortBy}-${filters.sortDir}`}
                    onChange={(e) => {
                        const [sortBy, sortDir] = e.target.value.split("-");
                        setFilters(prev => ({ ...prev, sortBy, sortDir }));
                    }}
                >
                    <option value="price-asc">Ціна ↑</option>
                    <option value="price-desc">Ціна ↓</option>
                    <option value="name-asc">Назва A-Z</option>
                    <option value="name-desc">Назва Z-A</option>
                </select>

                <button
                    onClick={() =>
                        setFilters({
                            page: 1,
                            pageSize: 8,
                            search: "",
                            categoryId: "",
                            priceFrom: "",
                            priceTo: "",
                            sortBy: "name",
                            sortDir: "asc",
                        })}
                >
                    Скинути фільтри
                </button>
            </aside>

            <section className="catalog-content">
                <h2 className="title">Каталог меблів</h2>

                <div className="furniture-grid">
                    {furnitures.map(f => (
                        <Furniture key={f.Id} furniture={f} fromAll={true} isAdmin={isAdmin}/>
                    ))}
                </div>

                <div className="pagination">
                    <button
                        disabled={filters.page === 1}
                        onClick={() =>
                            setFilters(prev => ({ ...prev, page: prev.page - 1 }))
                        }
                    >
                        ←
                    </button>

                    <span>{filters.page} / {totalPages}</span>

                    <button
                        disabled={filters.page === totalPages}
                        onClick={() =>
                            setFilters(prev => ({ ...prev, page: prev.page + 1 }))
                        }
                    >
                        →
                    </button>
                </div>
            </section>
            <button  className="floating-cart-btn"  onClick={() => setIsCartOpen(true)}>🛒</button>

            <CartModal
                isOpen={isCartOpen}
                onClose={() => setIsCartOpen(false)}
            />
        </div>
    );
};

export default Furnitures;