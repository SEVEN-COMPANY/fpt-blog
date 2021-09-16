"use strict";
var btnNav = document.getElementById('nav-btn');
btnNav === null || btnNav === void 0 ? void 0 : btnNav.addEventListener('click', function () {
    var navbarMenu = document.getElementById('nav-menu');
    if (navbarMenu) {
        navbarMenu.classList.toggle('opacity-100');
        navbarMenu.classList.toggle('z-20');
    }
});
