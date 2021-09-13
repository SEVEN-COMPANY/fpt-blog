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
/***/ (() => {

eval("\r\n// import Quill from 'quill';\r\n// @ts-nocheck\r\nwindow.onload = function () {\r\n    var editor = new Quill('#editor', {\r\n        modules: {\r\n            toolbar: [\r\n                ['bold', 'italic', 'underline', 'strike'],\r\n                ['blockquote', 'code-block'],\r\n                ['link', 'video', 'image'],\r\n                [{ header: 1 }, { header: 2 }],\r\n                [{ list: 'ordered' }, { list: 'bullet' }],\r\n                [{ script: 'sub' }, { script: 'super' }],\r\n                [{ indent: '-1' }, { indent: '+1' }],\r\n                [{ direction: 'rtl' }],\r\n                [{ size: ['small', false, 'large', 'huge'] }],\r\n                [{ header: [1, 2, 3, 4, 5, 6, false] }],\r\n                // [({ color: [] } ,{ background: [] })], // dropdown with defaults from theme\r\n                [{ font: [] }],\r\n                [{ align: [] }],\r\n                ['clean'], // remove formatting button\r\n            ],\r\n        },\r\n        theme: 'snow',\r\n    });\r\n    function selectLocalImage() {\r\n        var input = document.createElement('input');\r\n        input.setAttribute('type', 'file');\r\n        input.click();\r\n        // Listen upload local image and save to server\r\n        input.onchange = function () {\r\n            var file = input.files[0];\r\n            // file type is only image.\r\n            if (/^image\\//.test(file.type)) {\r\n                saveToServer(file);\r\n            }\r\n            else {\r\n                console.warn('You could only upload images.');\r\n            }\r\n        };\r\n    }\r\n    /**\r\n     * Step2. save to server\r\n     *\r\n     * @param {File} file\r\n     */\r\n    function saveToServer(file) {\r\n        var fd = new FormData();\r\n        fd.append('image', file);\r\n        insertToEditor('https://picsum.photos/200/300');\r\n        // const xhr = new XMLHttpRequest();\r\n        // xhr.open('POST', 'http://localhost:3000/upload/image', true);\r\n        // xhr.onload = () => {\r\n        //     if (xhr.status === 200) {\r\n        //         // this is callback data: url\r\n        //         const url = JSON.parse(xhr.responseText).data;\r\n    }\r\n    // };\r\n    // xhr.send(fd);\r\n    /**\r\n     * Step3. insert image url to rich editor.\r\n     *\r\n     * @param {string} url\r\n     */\r\n    function insertToEditor(url) {\r\n        // push image url to rich editor.\r\n        var range = editor.getSelection();\r\n        editor.insertEmbed(range.index, 'image', \"\" + url);\r\n    }\r\n    editor.getModule('toolbar').addHandler('image', function () {\r\n        selectLocalImage();\r\n    });\r\n};\r\n\n\n//# sourceURL=webpack://mono-webpack/./src/createBlog.ts?");

/***/ })

/******/ 	});
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module can't be inlined because the eval devtool is used.
/******/ 	var __webpack_exports__ = {};
/******/ 	__webpack_modules__["./src/createBlog.ts"]();
/******/ 	
/******/ })()
;