/*
 * ATTENTION: The "eval" devtool has been used (maybe by default in mode: "development").
 * This devtool is neither made for production nor for readable output files.
 * It uses "eval()" calls to create a separate source file in the browser devtools.
 * If you are trying to read the output file, select a different devtool (https://webpack.js.org/configuration/devtool/)
 * or disable the default devtool with "devtool: false".
 * If you are looking for production-ready output files, see mode: "production" (https://webpack.js.org/configuration/mode/).
 */
/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./src/navbar/navbar.ts":
/*!******************************!*\
  !*** ./src/navbar/navbar.ts ***!
  \******************************/
/***/ (() => {

eval("\r\nvar navBtn = document.getElementById('nav-btn');\r\nnavBtn === null || navBtn === void 0 ? void 0 : navBtn.addEventListener('click', function () {\r\n    var navBtn1 = document.getElementById('nav-btn-1');\r\n    var navBtn2 = document.getElementById('nav-btn-2');\r\n    var navBtn3 = document.getElementById('nav-btn-3');\r\n    var navBg = document.getElementById('nav-bg');\r\n    var navMenu = document.getElementById('nav-menu');\r\n    if (navBtn1 !== null && navBtn2 !== null && navBtn3 !== null && navBg !== null && navMenu !== null) {\r\n        navBg.classList.toggle('z-40');\r\n        navBg.classList.toggle('z-0');\r\n        navBg.classList.toggle('opacity-0');\r\n        navBg.classList.toggle('invisible');\r\n        navMenu.classList.toggle('-translate-x-full');\r\n        navBtn1.classList.toggle('translate-y-3.5');\r\n        navBtn1.classList.toggle('rotate-45');\r\n        navBtn2.classList.toggle('-translate-x-2');\r\n        navBtn2.classList.toggle('opacity-0');\r\n        navBtn3.classList.toggle('-translate-y-2');\r\n        navBtn3.classList.toggle('-rotate-45');\r\n    }\r\n});\r\nvar currentY = window.scrollY;\r\nwindow.addEventListener('scroll', function () {\r\n    console.log(this.window.scrollY);\r\n    var navbar = this.document.getElementById('navbar-layout');\r\n    if (currentY < this.window.scrollY) {\r\n        console.log('hello');\r\n        currentY = this.window.screenY;\r\n        navbar === null || navbar === void 0 ? void 0 : navbar.classList.add('md:-translate-y-18');\r\n    }\r\n    else {\r\n        navbar === null || navbar === void 0 ? void 0 : navbar.classList.remove('md:-translate-y-18');\r\n    }\r\n});\r\n\n\n//# sourceURL=webpack://mono-webpack/./src/navbar/navbar.ts?");

/***/ })

/******/ 	});
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module can't be inlined because the eval devtool is used.
/******/ 	var __webpack_exports__ = {};
/******/ 	__webpack_modules__["./src/navbar/navbar.ts"]();
/******/ 	
/******/ })()
;