"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var routes_1 = require("./package/axios/routes");
var axios_1 = require("./package/axios");
var getCurrentUser = function () {
    axios_1.http.get(routes_1.routers.getUser).then(function (res) {
        var user = document.getElementById('user');
        var auth = document.getElementById('auth');
        var userAvatar = document.getElementById('user-avatar');
        if (user && userAvatar) {
            user.classList.remove('hidden');
            user.classList.add('flex');
            userAvatar.src = res.data.data.avatarUrl;
        }
        if (auth) {
            auth.classList.add('hidden');
            auth.classList.remove('md:flex');
        }
    });
};
getCurrentUser();
