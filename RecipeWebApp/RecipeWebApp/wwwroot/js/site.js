let burgerMenuRef = document.querySelector('.burger-menu');
let sideMenuRef = document.querySelector('.sidemenu');

function onClickBurgerMenu() {

    if (!sideMenuRef.classList.contains('open')) {
        // sideMenuRef.styles.setProperty("display", "flex");
        sideMenuRef.classList.add('open');
    }
    else {
        sideMenuRef.classList.remove('open');
    }
}

burgerMenuRef.addEventListener('click', onClickBurgerMenu);



let deleteRecipeBtn = document.querySelector('.delete-recipe-btn');
let confirmDeleteBtn = document.querySelector('.confirm-delete');
let closeDeletePopupBtn = document.querySelector('.close-delete-popup');
let popupDeleteModal = document.querySelector('.popup-delete');

function onClickDeleteRecipeBtn()
{
    console.log('ok');
    if (!popupDeleteModal.classList.contains('open')) {
        popupDeleteModal.classList.add('open');
    }
}

function onClickCloseModalBtn() {
    if (popupDeleteModal.classList.contains('open')) {
        popupDeleteModal.classList.remove('open');
    }
}

deleteRecipeBtn.addEventListener('click', onClickDeleteRecipeBtn);
closeDeletePopupBtn.addEventListener('click', onClickCloseModalBtn);


