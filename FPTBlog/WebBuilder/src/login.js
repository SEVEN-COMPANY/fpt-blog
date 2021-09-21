"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var axios_1 = require("./package/axios");
var routes_1 = require("./package/axios/routes");
var loginForm = document.getElementById('loginForm');
loginForm === null || loginForm === void 0 ? void 0 : loginForm.addEventListener('submit', function (event) {
    event.preventDefault();
    var username = document.getElementById('username');
    var password = document.getElementById('password');
    if (username && password) {
        var input = {
            username: username.value,
            password: password.value,
        };
        axios_1.http.post(routes_1.routers.loginUser, input).then(function () { return window.location.assign(routes_1.routerLinks.home); });
    }
});
