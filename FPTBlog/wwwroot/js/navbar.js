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

/***/ "./src/navbar.ts":
/*!***********************!*\
  !*** ./src/navbar.ts ***!
  \***********************/
/***/ (() => {

eval("\r\nvar btnNav = document.getElementById('nav-btn');\r\nbtnNav === null || btnNav === void 0 ? void 0 : btnNav.addEventListener('click', function () {\r\n    var navbarMenu = document.getElementById('nav-menu');\r\n    if (navbarMenu) {\r\n        navbarMenu.classList.toggle('opacity-100');\r\n        navbarMenu.classList.toggle('z-20');\r\n    }\r\n});\r\n\n\n//# sourceURL=webpack://mono-webpack/./src/navbar.ts?");

/***/ })

/******/ 	});
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module can't be inlined because the eval devtool is used.
/******/ 	var __webpack_exports__ = {};
/******/ 	__webpack_modules__["./src/navbar.ts"]();
/******/ 	
/******/ })()
;