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

/***/ "./src/createBlog.ts":
/*!***************************!*\
  !*** ./src/createBlog.ts ***!
  \***************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {

eval("\r\nvar __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {\r\n    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }\r\n    return new (P || (P = Promise))(function (resolve, reject) {\r\n        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }\r\n        function rejected(value) { try { step(generator[\"throw\"](value)); } catch (e) { reject(e); } }\r\n        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }\r\n        step((generator = generator.apply(thisArg, _arguments || [])).next());\r\n    });\r\n};\r\nvar __generator = (this && this.__generator) || function (thisArg, body) {\r\n    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;\r\n    return g = { next: verb(0), \"throw\": verb(1), \"return\": verb(2) }, typeof Symbol === \"function\" && (g[Symbol.iterator] = function() { return this; }), g;\r\n    function verb(n) { return function (v) { return step([n, v]); }; }\r\n    function step(op) {\r\n        if (f) throw new TypeError(\"Generator is already executing.\");\r\n        while (_) try {\r\n            if (f = 1, y && (t = op[0] & 2 ? y[\"return\"] : op[0] ? y[\"throw\"] || ((t = y[\"return\"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;\r\n            if (y = 0, t) op = [op[0] & 2, t.value];\r\n            switch (op[0]) {\r\n                case 0: case 1: t = op; break;\r\n                case 4: _.label++; return { value: op[1], done: false };\r\n                case 5: _.label++; y = op[1]; op = [0]; continue;\r\n                case 7: op = _.ops.pop(); _.trys.pop(); continue;\r\n                default:\r\n                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }\r\n                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }\r\n                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }\r\n                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }\r\n                    if (t[2]) _.ops.pop();\r\n                    _.trys.pop(); continue;\r\n            }\r\n            op = body.call(thisArg, _);\r\n        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }\r\n        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };\r\n    }\r\n};\r\nObject.defineProperty(exports, \"__esModule\", ({ value: true }));\r\nvar quill_1 = __webpack_require__(/*! ./package/quill */ \"./src/package/quill/index.ts\");\r\nvar helper_1 = __webpack_require__(/*! ./package/quill/helper */ \"./src/package/quill/helper.ts\");\r\nvar axios_1 = __webpack_require__(/*! ./package/axios */ \"./src/package/axios/index.ts\");\r\nwindow.onload = function () {\r\n    return __awaiter(this, void 0, void 0, function () {\r\n        var btnSend, btnSave;\r\n        return __generator(this, function (_a) {\r\n            quill_1.editor.getModule('toolbar').addHandler('image', function () {\r\n                (0, helper_1.selectLocalImage)(quill_1.editor, helper_1.saveToServer);\r\n            });\r\n            btnSend = document.getElementById('send');\r\n            btnSave = document.getElementById('save');\r\n            btnSend === null || btnSend === void 0 ? void 0 : btnSend.addEventListener('click', function () {\r\n                axios_1.http.post('/blog', {\r\n                    title: 'hello',\r\n                    contain: quill_1.editor.root.innerHTML,\r\n                });\r\n            });\r\n            btnSave === null || btnSave === void 0 ? void 0 : btnSave.addEventListener('click', function () {\r\n                var blogId = document.getElementById('blogId');\r\n                if (blogId) {\r\n                    axios_1.http.post('/blog/save', {\r\n                        BlogId: blogId.innerHTML.trim(),\r\n                        Title: 'hello',\r\n                        Content: quill_1.editor.root.innerHTML,\r\n                    });\r\n                }\r\n            });\r\n            return [2 /*return*/];\r\n        });\r\n    });\r\n};\r\n\n\n//# sourceURL=webpack://mono-webpack/./src/createBlog.ts?");

/***/ }),

/***/ "./src/package/axios/index.ts":
/*!************************************!*\
  !*** ./src/package/axios/index.ts ***!
  \************************************/
/***/ ((__unused_webpack_module, exports) => {

eval("\r\nObject.defineProperty(exports, \"__esModule\", ({ value: true }));\r\nexports.http = void 0;\r\nvar http = axios;\r\nexports.http = http;\r\nhttp.defaults.withCredentials = true;\r\n\n\n//# sourceURL=webpack://mono-webpack/./src/package/axios/index.ts?");

/***/ }),

/***/ "./src/package/quill/helper.ts":
/*!*************************************!*\
  !*** ./src/package/quill/helper.ts ***!
  \*************************************/
/***/ ((__unused_webpack_module, exports, __webpack_require__) => {

eval("\r\nObject.defineProperty(exports, \"__esModule\", ({ value: true }));\r\nexports.saveToServer = exports.selectLocalImage = exports.insertToEditor = void 0;\r\nvar axios_1 = __webpack_require__(/*! ../axios */ \"./src/package/axios/index.ts\");\r\nfunction insertToEditor(editor, url) {\r\n    var range = editor.getSelection();\r\n    if (range)\r\n        editor.insertEmbed(range.index, 'image', \"\" + url);\r\n}\r\nexports.insertToEditor = insertToEditor;\r\nfunction selectLocalImage(editor, cb) {\r\n    var input = document.createElement('input');\r\n    input.setAttribute('type', 'file');\r\n    input.click();\r\n    input.onchange = function () {\r\n        if ((input === null || input === void 0 ? void 0 : input.files) && input.files[0]) {\r\n            var file = input.files[0];\r\n            if (/^image\\//.test(file.type)) {\r\n                cb(editor, file);\r\n            }\r\n            else {\r\n                alert('You could only upload images');\r\n            }\r\n        }\r\n    };\r\n}\r\nexports.selectLocalImage = selectLocalImage;\r\nfunction saveToServer(editor, file) {\r\n    var fd = new FormData();\r\n    fd.append('image', file);\r\n    var formData = new FormData();\r\n    formData.append('image', file);\r\n    axios_1.http.post('/blog/image', formData).then(function (res) {\r\n        var imageUrl = res.data;\r\n        insertToEditor(editor, imageUrl);\r\n    });\r\n}\r\nexports.saveToServer = saveToServer;\r\n\n\n//# sourceURL=webpack://mono-webpack/./src/package/quill/helper.ts?");

/***/ }),

/***/ "./src/package/quill/index.ts":
/*!************************************!*\
  !*** ./src/package/quill/index.ts ***!
  \************************************/
/***/ ((__unused_webpack_module, exports) => {

eval("\r\nObject.defineProperty(exports, \"__esModule\", ({ value: true }));\r\nexports.editor = void 0;\r\nexports.editor = new Quill('#editor', {\r\n    modules: {\r\n        toolbar: [\r\n            ['bold', 'italic', 'underline', 'strike'],\r\n            ['blockquote', 'code-block'],\r\n            ['link', 'video', 'image'],\r\n            [{ header: 1 }, { header: 2 }],\r\n            [{ list: 'ordered' }, { list: 'bullet' }],\r\n            [{ script: 'sub' }, { script: 'super' }],\r\n            [{ indent: '-1' }, { indent: '+1' }],\r\n            [{ direction: 'rtl' }],\r\n            [{ size: ['small', false, 'large', 'huge'] }],\r\n            [{ header: [1, 2, 3, 4, 5, 6, false] }],\r\n            [{ color: [] }, { background: [] }],\r\n            [{ font: [] }],\r\n            [{ align: [] }],\r\n            ['clean'], // remove formatting button\r\n        ],\r\n    },\r\n    theme: 'snow',\r\n});\r\n\n\n//# sourceURL=webpack://mono-webpack/./src/package/quill/index.ts?");

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
/******/ 		__webpack_modules__[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/ 	
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/ 	
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module is referenced by other modules so it can't be inlined
/******/ 	var __webpack_exports__ = __webpack_require__("./src/createBlog.ts");
/******/ 	
/******/ })()
;