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
    if (!popupDeleteModal.classList.contains('open')) {
        popupDeleteModal.classList.add('open');
    }
}

function onClickCloseModalBtn() {
    if (popupDeleteModal.classList.contains('open')) {
        popupDeleteModal.classList.remove('open');
    }
}

if (deleteRecipeBtn != null) {
    deleteRecipeBtn.addEventListener('click', onClickDeleteRecipeBtn);
}

if (closeDeletePopupBtn != null) {
    closeDeletePopupBtn.addEventListener('click', onClickCloseModalBtn);
}


let successModaleCloseBtn = document.querySelector('.success-modale-close');
let successModale = document.querySelector('.success-modale');

function onClickCloseBtnSuccessModale()
{

    successModale.classList.remove('close');

    if (!successModale.classList.contains('close')) {
        successModale.classList.add('close');
    }
}

if (successModaleCloseBtn != null) {
    successModaleCloseBtn.addEventListener('click', onClickCloseBtnSuccessModale);
}



