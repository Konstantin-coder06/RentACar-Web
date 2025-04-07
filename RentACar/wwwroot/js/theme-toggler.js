document.addEventListener("DOMContentLoaded", function () {
    const sideMenu = document.querySelector('aside');
    const menuBtn = document.querySelector('#menu_bar');
    const closeBtn = document.querySelector('#close_btn');
    const themeToggler = document.querySelector('.theme-toggler');

    
    if (menuBtn && sideMenu) {
        menuBtn.addEventListener('click', () => {
            sideMenu.style.display = "block";
        });
    } else {
        console.error("Menu button or side menu not found");
    }

    
    if (closeBtn && sideMenu) {
        closeBtn.addEventListener('click', () => {
            sideMenu.style.display = "none";
        });
    } else {
        console.error("Close button or side menu not found");
    }

   
    if (themeToggler) {
        themeToggler.addEventListener('click', () => {
            document.body.classList.toggle('dark-theme-variables');
            themeToggler.querySelector('span:nth-child(1)').classList.toggle('active');
            themeToggler.querySelector('span:nth-child(2)').classList.toggle('active');
        });
    } else {
        console.error("Theme toggler not found");
    }
});