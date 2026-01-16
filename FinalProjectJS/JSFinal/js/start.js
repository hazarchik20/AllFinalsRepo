import { Modal } from "./modal.js";

let allCar = [];
let filteredCars = [];
let currentPage = 1;
const itemsPerPage = 8;
const modal = new Modal(0);

export async function loadCar() {
    try {
        const response = await fetch("http://localhost:12000/Car/all");

        if (!response.ok) {
            throw new Error("Помилка завантаження машин");
        }

        allCar = await response.json();
        console.log("Завантажені машини:", allCar);

        if (allCar.length != 0)
            RenderCar();

    }
    catch (err) {
        console.error("Помилка:", err);
    }
}
async function deleteCar(id) {
    try {
        const response = await fetch(`http://localhost:12000/Car/${id}`,
            {
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json"
                },
            });

        if (!response.ok) {
            throw new Error("Помилка видалення машини");
        }

        console.log(`Car with id ${id} deleted`);

        allCar = allCar.filter(item => item.id !== id);

            RenderCar();
    }
    catch (err) {
        console.error("Помилка:", err);
    }
}
async function updateCar(carData) {
    console.log("start UPDATE function\n" + carData);
    
    const response = await fetch("http://localhost:12000/Car", {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(carData)
    });

    if (!response.ok) {
        throw new Error("Помилка оновлення машини");
    }

    const updatedCar = await response.json();
    console.log("Оновлено:", updatedCar);

    // заміняємо у масиві
    const index = allCar.findIndex(c => c.id === updatedCar.id);
    if (index !== -1) {
        allCar[index] = updatedCar;
    }
}
async function addCar(carData) {
    console.log("start ADD function\n" + carData);

    const response = await fetch("http://localhost:12000/Car", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(carData)
    });

    if (!response.ok) {
        throw new Error("Помилка при створенні машини");
    }

    const createdCar = await response.json();
    console.log("Створено:", createdCar);

    allCar.push(createdCar);
}


export function openModal(car)
{
    modal.id = car ? car.id : 0;

    modal.onSubmit = async (formData) => {
        const carData = {
            id: car ? car.id : 0,
            make: formData.make,
            model: formData.model,
            year: +formData.year,
            mileage: +formData.mileage,
            description: formData.description,
            type: formData.type,
            price: +formData.price,
            discount: +formData.discount,
            imageUrl: formData.imageUrl
        };
        console.log(carData);

        if (car===null) {
            await addCar(carData);
        } 
        else if(car!=null) {
            await updateCar(carData);
        }
        
        RenderCar();
    };

    if (car) modal.setData(car);
    modal.open();
}


export function nextPage(count = 1) {
    const maxPage = Math.ceil(allCar.length / itemsPerPage);

    currentPage += count;
    if (currentPage > maxPage) currentPage = maxPage;

    RenderCar();
}
export function previousPage(count = 1) {
    currentPage -= count;
    if (currentPage < 1) currentPage = 1;

    RenderCar();
}
function updatePaginationUI() {
    document.getElementById("1chil").textContent = currentPage - 1 < 1 ? "" : currentPage - 1;
    document.getElementById("2chil").textContent = currentPage;
    document.getElementById("3chil").textContent = currentPage + 1 > Math.ceil(allCar.length / itemsPerPage)
        ? ""
        : currentPage + 1;

    document.getElementById("1chil").classList.remove("activ");
    document.getElementById("2chil").classList.remove("activ");
    document.getElementById("3chil").classList.remove("activ");

    if (document.getElementById("2chil").textContent !== "")
        document.getElementById("2chil").classList.add("activ");
}


export function applySorting() {
    let filtered = [...allCar];

    const make = document.getElementById("sortType").value;
    if (make !== "") {
        filtered = filtered.filter(car => car.make.toLowerCase() === make);
    }

    const word = document.getElementById("searchWord").value.toLowerCase();
    if (word.trim() !== "") {
        filtered = filtered.filter(car =>
            car.make.toLowerCase().includes(word) ||
            car.model.toLowerCase().includes(word) ||
            car.description.toLowerCase().includes(word)
        );
    }

    const sortValue = document.getElementById("sort").value; 

    const lowest = document.querySelector("input[value='lowest']").checked;
    const higher = document.querySelector("input[value='higher']").checked;

    if (lowest && !higher) {
        filtered.sort((a, b) => a[sortValue] - b[sortValue]);
    } 
    else if (higher && !lowest) {
        filtered.sort((a, b) => b[sortValue] - a[sortValue]);
    }
  
    filteredCars = filtered;

    currentPage = 1;
    RenderCar(filteredCars);
}
function RenderCar(list = allCar) {
    const container = document.getElementById("cardContainer");
    container.innerHTML = "";
    
    const start = (currentPage - 1) * itemsPerPage;
    const end = start + itemsPerPage;
    const carsToShow = list.slice(start, end);

    for (let i = 0; i < carsToShow.length; i++) {
        let car = carsToShow[i];

        let card = document.createElement("div");
        card.classList.add("car-card");

        let image = document.createElement("img");
        image.src = car.imageUrl;
        image.classList.add("thumbnail");

        let title = document.createElement("div");
        title.textContent = `${car.make} ${car.model} (${car.year})`;
        title.classList.add("title");

        let desc = document.createElement("div");
        desc.textContent = car.description;
        desc.classList.add("description");

        let type = document.createElement("div");
        type.textContent = `Тип: ${car.type}`;
        type.classList.add("category");

        let mileage = document.createElement("div");
        mileage.textContent = `Пробіг: ${car.mileage} км`;
        mileage.classList.add("mileage");

        let price = document.createElement("div");
        price.textContent = `${car.price} $ (Знижка: ${car.discount}%)`;
        price.classList.add("price");

        let cartBtn = document.createElement("div");
        cartBtn.classList.add("fa", "fa-shopping-cart", "cart-btn");
        cartBtn.addEventListener("click", () => {
            console.log(`Car with id ${car.id} added`);
        });

        let delbtn = document.createElement("div");
        delbtn.classList.add("fa", "fa-trash", "trash-btn");
        delbtn.addEventListener("click", () => { deleteCar(car.id) });

        let updatebtn = document.createElement("div");
        updatebtn.classList.add("fa", "fa-pencil", "update-btn");
        updatebtn.addEventListener("click", () => { openModal(car) });

        card.append(image, title, desc, type, mileage, price, cartBtn, delbtn, updatebtn);
        container.appendChild(card);
    }

    updatePaginationUI();
}