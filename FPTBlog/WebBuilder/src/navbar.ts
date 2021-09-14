const btnNav = document.getElementById('nav-btn');

btnNav?.addEventListener('click', function () {
    const navbarMenu = document.getElementById('nav-menu');
    if (navbarMenu) {
        navbarMenu.classList.toggle('opacity-100');
        navbarMenu.classList.toggle('z-20');
    }
});
