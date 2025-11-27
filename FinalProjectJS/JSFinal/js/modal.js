export class Modal {
    constructor(id) {
        this.modal = document.getElementById('modal');
        this.btnClose = document.getElementById('btnClose');
        this.form = document.getElementById('carForm'); 
        this.onSubmit = null;
        this.id = id ?? 0;

        this.initEventListeners();
        this.initValidation();
    }

    open() {
        this.modal.classList.add('show');
    }

    close() {
        this.modal.classList.remove('show');
        this.form.reset();
        this.clearErrors();
    }

    initEventListeners() {
        this.btnClose.addEventListener('click', () => this.close());

        this.modal.addEventListener('click', (e) => {
            if (e.target === this.modal) {
                this.close();
            }
        });

        this.form.addEventListener('submit', (e) => this.handleSubmit(e));
    }

    initValidation() {
        const fields = this.form.querySelectorAll("input, select, textarea");
        fields.forEach(field => {
            field.addEventListener("input", () => this.validateField(field));
            field.addEventListener("blur", () => this.validateField(field));
        });
    }
    setData(car) {

    this.form.make.value = car.make ?? "";
    this.form.model.value = car.model ?? "";
    this.form.year.value = car.year ?? "";
    this.form.mileage.value = car.mileage ?? "";
    this.form.description.value = car.description ?? "";
    this.form.type.value = car.type ?? "";
    this.form.price.value = car.price ?? "";
    this.form.discount.value = car.discount ?? "";
    this.form.imageUrl.value = car.imageUrl ?? "";

    const errors = this.form.querySelectorAll(".error-message");
    errors.forEach(e => e.textContent = "");
}

    validateField(field) {
        field.setCustomValidity("");
        const value = field.value.trim();

        switch (field.id) {
            case "make":
            case "model":
                if (value.length < 2) {
                    field.setCustomValidity("Повинно бути мінімум 2 символи.");
                }
                else{
                    console.log("model|make is good"); 
                }
                break;
            case "year":
                const year = parseInt(value);
                if (isNaN(year) || year < 1900 || year > 2100) {
                    field.setCustomValidity("Вкажіть коректний рік (1900-2100).");
                }
                else{
                    console.log("year is good"); 
                }
                break;
            case "mileage":
                const mileage = parseInt(value);
                if (isNaN(mileage) || mileage < 0) {
                    field.setCustomValidity("Пробіг повинен бути ≥ 0.");
                } 
                else{
                    console.log("mileage is good"); 
                }
                break;
            case "description":
                if (value.length > 0 && value.length < 10) {
                    field.setCustomValidity("Опис має бути хоча б 10 символів або залиште порожнім.");
                }
                else{
                    console.log("description is good"); 
                }
                break;
            case "type":
                if (!value) {
                    field.setCustomValidity("Оберіть тип машини.");
                }
                else{
                    console.log("type is good"); 
                }
                break;
            case "price":
                const price = parseFloat(value);
                if (isNaN(price) || price < 100) {
                    field.setCustomValidity("Ціна повинна бути додатнім числом більшим за 100.");
                }
                else{
                    console.log("price is good"); 
                }
                break;
            case "discount":
                const discount = parseFloat(value);
                if (!isNaN(discount) && (discount < 0 || discount > 90)) {
                    field.setCustomValidity("Знижка має бути від 0 до 90%.");
                }
                else{
                    console.log("discount is good"); 
                }
                break;
            case "imageUrl":
                if (value.length > 0 && !/^https?:\/\/.+/i.test(value)) {
                    field.setCustomValidity("Лінк на картинку має починатися з http або https.");
                }
                else{
                    console.log("imageUrl is good"); 
                }
                break;
        }

        const errorSpan = field.parentElement.querySelector(".error-message");
        errorSpan.textContent = field.checkValidity() ? "" : field.validationMessage;
      
        return field.checkValidity();
    }

    clearErrors() {
        const errors = this.form.querySelectorAll(".error-message");
        errors.forEach(span => span.textContent = "");
    }

    handleSubmit(e) {
        e.preventDefault();

        const formData = Object.fromEntries(new FormData(this.form));

        if (formData.year) 
            formData.year = parseInt(formData.year);
        if (formData.mileage) 
            formData.mileage = parseInt(formData.mileage);
        if (formData.price) 
            formData.price = parseFloat(formData.price);
        if (formData.discount) 
            formData.discount = parseFloat(formData.discount);

        console.log('Дані форми:', formData);

        if (this.onSubmit) {
            this.onSubmit(formData);
        }

        this.close();
    }
}
