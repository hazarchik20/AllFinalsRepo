import { openModal, loadCar, nextPage, previousPage,applySorting } from "./start.js";

function app() {
    document.getElementById('mainAddBtn').addEventListener("click",()=>{openModal(null)});
    document.getElementById('1chil').addEventListener("click",()=>{previousPage(1)});
    document.getElementById('3chil').addEventListener("click",()=>{nextPage(1)});
    document.getElementById('fiveToLeft').addEventListener("click",()=>{previousPage(5)});
    document.getElementById('fiveToRigth').addEventListener("click",()=>{nextPage(5)});

    document.getElementById("sortType").addEventListener("change", applySorting);
    document.getElementById("searchWord").addEventListener("input", applySorting);
    document.getElementById("sort").addEventListener("change", applySorting);

    document.querySelectorAll("input[name='features']")
        .forEach(cb => cb.addEventListener("change", applySorting));
   

    loadCar();
}
document.addEventListener("DOMContentLoaded", app);
