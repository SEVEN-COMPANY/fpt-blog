"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var axios_1 = require("../package/axios");
var routes_1 = require("../package/axios/routes");
var changeUserPassword = document.getElementById('changeUserPasswordForm');
changeUserPassword === null || changeUserPassword === void 0 ? void 0 : changeUserPassword.addEventListener('submit', function (event) {
    event.preventDefault();
    var oldPassword = document.getElementById('oldPassword');
    var newPassword = document.getElementById('newPassword');
    var confirmNewPassword = document.getElementById('confirmNewPassword');
    if (oldPassword !== null && newPassword !== null && confirmNewPassword !== null) {
        var input = {
            oldPassword: oldPassword.value,
            newPassword: newPassword.value,
            confirmNewPassword: confirmNewPassword.value,
        };
        axios_1.http.post(routes_1.routers.user.changePassword, input);
    }
});
