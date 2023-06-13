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

