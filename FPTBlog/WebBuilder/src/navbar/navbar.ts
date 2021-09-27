const navBtn = document.getElementById('nav-btn');

navBtn?.addEventListener('click', function () {
    const navBtn1 = document.getElementById('nav-btn-1');
    const navBtn2 = document.getElementById('nav-btn-2');
    const navBtn3 = document.getElementById('nav-btn-3');
    const navBg = document.getElementById('nav-bg');
    const navMenu = document.getElementById('nav-menu');

    if (navBtn1 !== null && navBtn2 !== null && navBtn3 !== null && navBg !== null && navMenu !== null) {
        navBg.classList.toggle('z-40');
        navBg.classList.toggle('z-0');
        navBg.classList.toggle('opacity-0');
        navMenu.classList.toggle('-translate-x-full');
        navBtn1.classList.toggle('translate-y-3.5');
        navBtn1.classList.toggle('rotate-45');
        navBtn2.classList.toggle('-translate-x-2');
        navBtn2.classList.toggle('opacity-0');
        navBtn3.classList.toggle('-translate-y-2');
        navBtn3.classList.toggle('-rotate-45');
    }
});