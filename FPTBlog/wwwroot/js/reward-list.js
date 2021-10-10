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

/***/ "./src/package/axios/index.ts":
/*!************************************!*\
  !*** ./src/package/axios/index.ts ***!
  \************************************/
/***/ ((__unused_webpack_module, exports, __webpack_require__) => {

eval("\r\nObject.defineProperty(exports, \"__esModule\", ({ value: true }));\r\nexports.http = void 0;\r\nvar interceptor_1 = __webpack_require__(/*! ./interceptor */ \"./src/package/axios/interceptor.ts\");\r\nvar http = axios;\r\nexports.http = http;\r\nhttp.defaults.withCredentials = true;\r\nhttp.interceptors.request.use(interceptor_1.requestInterceptor);\r\nhttp.interceptors.response.use(interceptor_1.responseSuccessInterceptor, interceptor_1.responseFailedInterceptor);\r\n\n\n//# sourceURL=webpack://mono-webpack/./src/package/axios/index.ts?");

/***/ }),

/***/ "./src/package/axios/interceptor.ts":
/*!******************************************!*\
  !*** ./src/package/axios/interceptor.ts ***!
  \******************************************/
/***/ ((__unused_webpack_module, exports, __webpack_require__) => {

eval("\r\nObject.defineProperty(exports, \"__esModule\", ({ value: true }));\r\nexports.responseFailedInterceptor = exports.responseSuccessInterceptor = exports.handleCommonResponse = exports.requestInterceptor = void 0;\r\nvar toastify_1 = __webpack_require__(/*! ../toastify */ \"./src/package/toastify/index.ts\");\r\nfunction requestInterceptor(req) {\r\n    var btn = document.getElementById('form-btn');\r\n    var loading = document.getElementById('loading');\r\n    var message = document.getElementById('MESSAGEERROR');\r\n    var errorMessage = document.getElementById('ERRORMESSAGEERROR');\r\n    for (var key in req.data) {\r\n        var error = document.getElementById(key.toUpperCase() + \"ERROR\");\r\n        if (error) {\r\n            error.innerHTML = \"\";\r\n        }\r\n    }\r\n    if (errorMessage) {\r\n        errorMessage.innerHTML = '';\r\n    }\r\n    if (message) {\r\n        message.innerHTML = '';\r\n    }\r\n    if (loading && btn) {\r\n        btn.classList.add('hidden');\r\n        loading.classList.remove('hidden');\r\n        loading.classList.add('flex');\r\n    }\r\n    return req;\r\n}\r\nexports.requestInterceptor = requestInterceptor;\r\nfunction handleCommonResponse() {\r\n    var btn = document.getElementById('form-btn');\r\n    var loading = document.getElementById('loading');\r\n    if (loading && btn) {\r\n        btn.classList.remove('hidden');\r\n        btn.classList.add('block');\r\n        loading.classList.add('hidden');\r\n        loading.classList.remove('flex');\r\n    }\r\n}\r\nexports.handleCommonResponse = handleCommonResponse;\r\nfunction responseSuccessInterceptor(response) {\r\n    var _a, _b, _c, _d, _e, _f;\r\n    handleCommonResponse();\r\n    if ((_b = (_a = response === null || response === void 0 ? void 0 : response.data) === null || _a === void 0 ? void 0 : _a.details) === null || _b === void 0 ? void 0 : _b.message) {\r\n        var message = document.getElementById('MESSAGEERROR');\r\n        if (message) {\r\n            var successMessage = (_d = (_c = response === null || response === void 0 ? void 0 : response.data) === null || _c === void 0 ? void 0 : _c.details) === null || _d === void 0 ? void 0 : _d.message;\r\n            message.innerHTML = successMessage.slice(0, 1).toUpperCase() + successMessage.slice(1, successMessage.length);\r\n        }\r\n        var sideMessage = document.getElementById('toastify');\r\n        if (sideMessage) {\r\n            var successMessage = (_f = (_e = response === null || response === void 0 ? void 0 : response.data) === null || _e === void 0 ? void 0 : _e.details) === null || _f === void 0 ? void 0 : _f.message;\r\n            (0, toastify_1.toastify)({\r\n                text: successMessage.slice(0, 1).toUpperCase() + successMessage.slice(1, successMessage.length),\r\n                duration: 2000,\r\n                newWindow: true,\r\n                close: true,\r\n                gravity: 'top',\r\n                position: 'right',\r\n                backgroundColor: '#fa983a',\r\n                stopOnFocus: true,\r\n            });\r\n        }\r\n    }\r\n    return response;\r\n}\r\nexports.responseSuccessInterceptor = responseSuccessInterceptor;\r\nfunction responseFailedInterceptor(error) {\r\n    var _a, _b;\r\n    handleCommonResponse();\r\n    if ((_b = (_a = error.response) === null || _a === void 0 ? void 0 : _a.data) === null || _b === void 0 ? void 0 : _b.details) {\r\n        var details = error.response.data.details;\r\n        for (var key in details) {\r\n            var error_1 = document.getElementById(key.toUpperCase() + \"ERROR\");\r\n            if (error_1) {\r\n                error_1.innerHTML = error_1.getAttribute('data-label') + \" \" + details[key];\r\n            }\r\n            if (error_1 && (key === 'errorMessage' || key === 'message')) {\r\n                var errorMessage = \"\" + details[key];\r\n                error_1.innerHTML = errorMessage.slice(0, 1).toUpperCase() + errorMessage.slice(1, errorMessage.length);\r\n                var sideMessage = document.getElementById('toastify');\r\n                if (sideMessage) {\r\n                    (0, toastify_1.toastify)({\r\n                        text: errorMessage.slice(0, 1).toUpperCase() + errorMessage.slice(1, errorMessage.length),\r\n                        duration: 2000,\r\n                        newWindow: true,\r\n                        close: true,\r\n                        gravity: 'top',\r\n                        position: 'right',\r\n                        backgroundColor: 'rgb(239, 68, 68)',\r\n                        stopOnFocus: true,\r\n                    });\r\n                }\r\n            }\r\n        }\r\n    }\r\n    return Promise.reject(error.response);\r\n}\r\nexports.responseFailedInterceptor = responseFailedInterceptor;\r\n\n\n//# sourceURL=webpack://mono-webpack/./src/package/axios/interceptor.ts?");

/***/ }),

/***/ "./src/package/axios/routes.ts":
/*!*************************************!*\
  !*** ./src/package/axios/routes.ts ***!
  \*************************************/
/***/ ((__unused_webpack_module, exports) => {

eval("\r\nObject.defineProperty(exports, \"__esModule\", ({ value: true }));\r\nexports.routers = exports.routerLinks = void 0;\r\nexports.routerLinks = {\r\n    home: '/',\r\n    loginForm: '/auth/login',\r\n};\r\nexports.routers = {\r\n    category: {\r\n        create: '/api/category',\r\n        update: '/api/category',\r\n    },\r\n    post: {\r\n        create: '/api/post',\r\n        addNewTagToPost: '/api/post/tag',\r\n        getTagOfPost: function (postId) { return \"/api/post/tag?postId=\" + postId; },\r\n        save: '/api/post/save',\r\n        uploadImagePost: '/api/post/image',\r\n        addCategoryToPost: '/api/post/category',\r\n        likePost: '/api/post/like',\r\n        dislikePost: '/api/post/dislike',\r\n        sendPost: '/api/post/send',\r\n        approvedPost: '/api/admin/post/approved',\r\n        deletePost: '/api/post/delete',\r\n    },\r\n    user: {\r\n        changePassword: '/api/user/change-password',\r\n        update: '/api/user',\r\n        get: '/api/user',\r\n        status: '/api/admin/user/status',\r\n        role: '/api/admin/user/role',\r\n        follow: '/api/user/follow',\r\n    },\r\n    tag: {\r\n        getAll: '/api/tag/all',\r\n        clearUnused: '/api/tag/unused',\r\n        getByName: function (name) { return \"/api/tag?name=\" + name; },\r\n    },\r\n    auth: {\r\n        login: '/api/auth/login',\r\n        register: '/api/auth/register',\r\n    },\r\n    reward: {\r\n        create: '/api/reward',\r\n        update: '/api/reward',\r\n    },\r\n};\r\n\n\n//# sourceURL=webpack://mono-webpack/./src/package/axios/routes.ts?");

/***/ }),

/***/ "./src/package/helper/pagination.ts":
/*!******************************************!*\
  !*** ./src/package/helper/pagination.ts ***!
  \******************************************/
/***/ ((__unused_webpack_module, exports) => {

eval("\r\nObject.defineProperty(exports, \"__esModule\", ({ value: true }));\r\nexports.pageChange = void 0;\r\nvar pageChange = function (formId) {\r\n    var paginationSize = document.getElementById('pagination-size');\r\n    var paginationBtn = document.getElementById('pagination-btn');\r\n    paginationSize === null || paginationSize === void 0 ? void 0 : paginationSize.addEventListener('change', function (_) {\r\n        var option = paginationSize.options[paginationSize.selectedIndex];\r\n        var pageSizeInput = document.getElementById('pageSize');\r\n        pageSizeInput.value = option.value;\r\n        var pageIndexInput = document.getElementById('pageIndex');\r\n        pageIndexInput.value = '0';\r\n        var form = document.getElementById(formId);\r\n        form.submit();\r\n    });\r\n    var pageBtn = paginationBtn === null || paginationBtn === void 0 ? void 0 : paginationBtn.getElementsByTagName('button');\r\n    if (pageBtn) {\r\n        var _loop_1 = function (index) {\r\n            var element = pageBtn[index];\r\n            element.addEventListener('click', function (_) {\r\n                var pageIndexInput = document.getElementById('pageIndex');\r\n                var value = element.getAttribute('data-index');\r\n                if (value) {\r\n                    pageIndexInput.value = value;\r\n                }\r\n                var form = document.getElementById(formId);\r\n                form.submit();\r\n            });\r\n        };\r\n        for (var index = 0; index < pageBtn.length; index++) {\r\n            _loop_1(index);\r\n        }\r\n    }\r\n};\r\nexports.pageChange = pageChange;\r\n\n\n//# sourceURL=webpack://mono-webpack/./src/package/helper/pagination.ts?");

/***/ }),

/***/ "./src/package/helper/previewImage.ts":
/*!********************************************!*\
  !*** ./src/package/helper/previewImage.ts ***!
  \********************************************/
/***/ ((__unused_webpack_module, exports) => {

eval("\r\nObject.defineProperty(exports, \"__esModule\", ({ value: true }));\r\nexports.previewImage = void 0;\r\nfunction previewImage(inputId, previewId) {\r\n    var imageInput = document.getElementById(inputId);\r\n    imageInput.addEventListener('change', function () {\r\n        var image = imageInput.files ? imageInput.files[0] : null;\r\n        if (image) {\r\n            var reader_1 = new FileReader();\r\n            reader_1.onload = function () {\r\n                var dataURL = reader_1.result;\r\n                var output = document.getElementById(previewId);\r\n                if (output && dataURL && typeof dataURL == 'string')\r\n                    output.src = dataURL;\r\n            };\r\n            reader_1.readAsDataURL(image);\r\n        }\r\n    });\r\n}\r\nexports.previewImage = previewImage;\r\n\n\n//# sourceURL=webpack://mono-webpack/./src/package/helper/previewImage.ts?");

/***/ }),

/***/ "./src/package/modal/index.ts":
/*!************************************!*\
  !*** ./src/package/modal/index.ts ***!
  \************************************/
/***/ ((__unused_webpack_module, exports) => {

eval("\r\nObject.defineProperty(exports, \"__esModule\", ({ value: true }));\r\nexports.slideOver = void 0;\r\nvar slideOver = function (id) {\r\n    var btn = document.getElementById(id + \"-btn\");\r\n    var btnClose = document.getElementById(id + \"-btn-close\");\r\n    var wrapper = document.getElementById(id + \"-wrapper\");\r\n    var bg = document.getElementById(id + \"-bg\");\r\n    var panel = document.getElementById(id + \"-panel\");\r\n    var modalToggle = function () {\r\n        wrapper === null || wrapper === void 0 ? void 0 : wrapper.classList.add('invisible');\r\n    };\r\n    btn === null || btn === void 0 ? void 0 : btn.addEventListener('click', function () {\r\n        wrapper === null || wrapper === void 0 ? void 0 : wrapper.classList.remove('invisible');\r\n        bg === null || bg === void 0 ? void 0 : bg.classList.add('opacity-100');\r\n        bg === null || bg === void 0 ? void 0 : bg.classList.remove('opacity-0');\r\n        panel === null || panel === void 0 ? void 0 : panel.classList.add('translate-x-0');\r\n        panel === null || panel === void 0 ? void 0 : panel.classList.remove('translate-x-full');\r\n        panel === null || panel === void 0 ? void 0 : panel.removeEventListener('transitionend', modalToggle);\r\n    });\r\n    btnClose === null || btnClose === void 0 ? void 0 : btnClose.addEventListener('click', function () {\r\n        bg === null || bg === void 0 ? void 0 : bg.classList.remove('opacity-100');\r\n        bg === null || bg === void 0 ? void 0 : bg.classList.add('opacity-0');\r\n        panel === null || panel === void 0 ? void 0 : panel.classList.remove('translate-x-0');\r\n        panel === null || panel === void 0 ? void 0 : panel.classList.add('translate-x-full');\r\n        panel === null || panel === void 0 ? void 0 : panel.addEventListener('transitionend', modalToggle);\r\n    });\r\n};\r\nexports.slideOver = slideOver;\r\n\n\n//# sourceURL=webpack://mono-webpack/./src/package/modal/index.ts?");

/***/ }),

/***/ "./src/package/toastify/index.ts":
/*!***************************************!*\
  !*** ./src/package/toastify/index.ts ***!
  \***************************************/
/***/ ((__unused_webpack_module, exports) => {

eval("\r\n// @ts-nocheck\r\nObject.defineProperty(exports, \"__esModule\", ({ value: true }));\r\nexports.toastify = void 0;\r\nvar toastify = function (option) { return Toastify(option).showToast(); };\r\nexports.toastify = toastify;\r\n\n\n//# sourceURL=webpack://mono-webpack/./src/package/toastify/index.ts?");

/***/ }),

/***/ "./src/reward/listReward.ts":
/*!**********************************!*\
  !*** ./src/reward/listReward.ts ***!
  \**********************************/
/***/ ((__unused_webpack_module, exports, __webpack_require__) => {

eval("\r\nObject.defineProperty(exports, \"__esModule\", ({ value: true }));\r\nvar axios_1 = __webpack_require__(/*! ../package/axios */ \"./src/package/axios/index.ts\");\r\nvar routes_1 = __webpack_require__(/*! ../package/axios/routes */ \"./src/package/axios/routes.ts\");\r\nvar pagination_1 = __webpack_require__(/*! ../package/helper/pagination */ \"./src/package/helper/pagination.ts\");\r\nvar previewImage_1 = __webpack_require__(/*! ../package/helper/previewImage */ \"./src/package/helper/previewImage.ts\");\r\nvar modal_1 = __webpack_require__(/*! ../package/modal */ \"./src/package/modal/index.ts\");\r\n(0, pagination_1.pageChange)('listRewardForm');\r\n(0, modal_1.slideOver)('modal');\r\n(0, previewImage_1.previewImage)('file', 'preview-image');\r\nvar rewardForm = document.getElementById('createRewardForm');\r\nrewardForm === null || rewardForm === void 0 ? void 0 : rewardForm.addEventListener('submit', function (event) {\r\n    event.preventDefault();\r\n    var description = document.getElementById('description');\r\n    var name = document.getElementById('name');\r\n    var file = document.getElementById('file');\r\n    if (description != null && name != null && file != null) {\r\n        var image = file.files ? file.files[0] : null;\r\n        var fd = new FormData();\r\n        fd.append('name', name.value);\r\n        fd.append('description', description.value);\r\n        if (image) {\r\n            fd.append('file', image);\r\n        }\r\n        axios_1.http.post(routes_1.routers.reward.create, fd).then(function () {\r\n            setTimeout(function () {\r\n                window.location.reload();\r\n            }, 1000);\r\n        });\r\n    }\r\n});\r\n\n\n//# sourceURL=webpack://mono-webpack/./src/reward/listReward.ts?");

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
/******/ 	var __webpack_exports__ = __webpack_require__("./src/reward/listReward.ts");
/******/ 	
/******/ })()
;