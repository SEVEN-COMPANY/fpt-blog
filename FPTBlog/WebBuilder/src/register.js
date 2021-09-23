"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var axios_1 = require("./package/axios");
var routes_1 = require("./package/axios/routes");
var registerForm = document.getElementById('registerForm');
registerForm === null || registerForm === void 0 ? void 0 : registerForm.addEventListener('submit', function (event) {
    event.preventDefault();
    var username = document.getElementById('username');
    var password = document.getElementById('password');
    var name = document.getElementById('name');
    var confirmPassword = document.getElementById('confirmPassword');
    if (username && password && name && confirmPassword) {
        var input = {
            username: username.value,
            password: password.value,
            name: name.value,
            confirmPassword: confirmPassword.value,
        };
        axios_1.http.post(routes_1.routers.registerUser, input).then(function () { return window.location.assign(routes_1.routerLinks.loginForm); });
    }
});
