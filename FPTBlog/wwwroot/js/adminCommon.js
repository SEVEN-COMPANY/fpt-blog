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

/***/ "./src/adminCommon.ts":
/*!****************************!*\
  !*** ./src/adminCommon.ts ***!
  \****************************/
/***/ (() => {

eval("\r\nvar _loop_1 = function (index) {\r\n    var btn = document.getElementById(\"menu-btn-\" + index);\r\n    var container = document.getElementById(\"menu-container-\" + index);\r\n    var menu = document.getElementById(\"menu-list-\" + index);\r\n    var arrow = document.getElementById(\"menu-arrow-\" + index);\r\n    if (btn && menu && arrow && container) {\r\n        btn.addEventListener('click', function () {\r\n            console.log('hello');\r\n            menu.classList.toggle('scale-y-0');\r\n            btn.setAttribute('disabled', 'disabled');\r\n            if (container.clientHeight <= menu.clientHeight) {\r\n                container.style.height = container.clientHeight + menu.clientHeight + 4 + 'px';\r\n            }\r\n            else {\r\n                container.style.height = '40px';\r\n            }\r\n            arrow.classList.toggle('rotate-180');\r\n        }, false);\r\n    }\r\n};\r\nfor (var index = 0; index <= 4; index++) {\r\n    _loop_1(index);\r\n}\r\n\n\n//# sourceURL=webpack://mono-webpack/./src/adminCommon.ts?");

/***/ })

/******/ 	});
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module can't be inlined because the eval devtool is used.
/******/ 	var __webpack_exports__ = {};
/******/ 	__webpack_modules__["./src/adminCommon.ts"]();
/******/ 	
/******/ })()
;