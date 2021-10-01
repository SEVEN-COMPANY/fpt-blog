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

/***/ "./src/package/helper/pagination.ts":
/*!******************************************!*\
  !*** ./src/package/helper/pagination.ts ***!
  \******************************************/
/***/ ((__unused_webpack_module, exports) => {

eval("\r\nObject.defineProperty(exports, \"__esModule\", ({ value: true }));\r\nexports.pageChange = void 0;\r\nvar pageChange = function (formId) {\r\n    var paginationSize = document.getElementById('pagination-size');\r\n    var paginationBtn = document.getElementById('pagination-btn');\r\n    paginationSize === null || paginationSize === void 0 ? void 0 : paginationSize.addEventListener('change', function (_) {\r\n        var option = paginationSize.options[paginationSize.selectedIndex];\r\n        var pageSizeInput = document.getElementById('pageSize');\r\n        pageSizeInput.value = option.value;\r\n        var pageIndexInput = document.getElementById('pageIndex');\r\n        pageIndexInput.value = '0';\r\n        var form = document.getElementById(formId);\r\n        form.submit();\r\n    });\r\n    var pageBtn = paginationBtn === null || paginationBtn === void 0 ? void 0 : paginationBtn.getElementsByTagName('button');\r\n    if (pageBtn) {\r\n        var _loop_1 = function (index) {\r\n            var element = pageBtn[index];\r\n            element.addEventListener('click', function (_) {\r\n                var pageIndexInput = document.getElementById('pageIndex');\r\n                var value = element.getAttribute('data-index');\r\n                if (value) {\r\n                    pageIndexInput.value = value;\r\n                }\r\n                var form = document.getElementById(formId);\r\n                form.submit();\r\n            });\r\n        };\r\n        for (var index = 0; index < pageBtn.length; index++) {\r\n            _loop_1(index);\r\n        }\r\n    }\r\n};\r\nexports.pageChange = pageChange;\r\n\n\n//# sourceURL=webpack://mono-webpack/./src/package/helper/pagination.ts?");

/***/ }),

/***/ "./src/user/list.ts":
/*!**************************!*\
  !*** ./src/user/list.ts ***!
  \**************************/
/***/ ((__unused_webpack_module, exports, __webpack_require__) => {

eval("\r\nObject.defineProperty(exports, \"__esModule\", ({ value: true }));\r\nvar pagination_1 = __webpack_require__(/*! ../package/helper/pagination */ \"./src/package/helper/pagination.ts\");\r\n(0, pagination_1.pageChange)('listUserForm');\r\n\n\n//# sourceURL=webpack://mono-webpack/./src/user/list.ts?");

/***/ })

/******/ 	});
/************************************************************************/
/******/ 	// The module cache
/******/ 	var __webpack_module_cache__ = {};
/******/ 	
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/ 		// Check if module is in cache
/******/ 		var cachedModule = __webpack_module_cache__[moduleId];
/******/ 		if (cachedModule !== undefined) {
/******/ 			return cachedModule.exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = __webpack_module_cache__[moduleId] = {
/******/ 			// no module.id needed
/******/ 			// no module.loaded needed
/******/ 			exports: {}
/******/ 		};
/******/ 	
/******/ 		// Execute the module function
/******/ 		__webpack_modules__[moduleId](module, module.exports, __webpack_require__);
/******/ 	
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/ 	
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module can't be inlined because the eval devtool is used.
/******/ 	var __webpack_exports__ = __webpack_require__("./src/user/list.ts");
/******/ 	
/******/ })()
;