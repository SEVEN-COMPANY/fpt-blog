(()=>{"use strict";var e,t,r,n={284:(e,t,r)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.http=void 0;var n=r(603),o=axios;t.http=o,o.defaults.withCredentials=!0,o.interceptors.request.use(n.requestInterceptor),o.interceptors.response.use(n.responseSuccessInterceptor,n.responseFailedInterceptor)},603:(e,t)=>{function r(){var e=document.getElementById("form-btn"),t=document.getElementById("loading");t&&e&&(e.classList.remove("hidden"),e.classList.add("block"),t.classList.add("hidden"),t.classList.remove("flex"))}Object.defineProperty(t,"__esModule",{value:!0}),t.responseFailedInterceptor=t.responseSuccessInterceptor=t.handleCommonResponse=t.requestInterceptor=void 0,t.requestInterceptor=function(e){var t=document.getElementById("form-btn"),r=document.getElementById("loading"),n=document.getElementById("MESSAGEERROR"),o=document.getElementById("ERRORMESSAGEERROR");for(var s in e.data){var a=document.getElementById(s.toUpperCase()+"ERROR");a&&(a.innerHTML="")}return o&&(o.innerHTML=""),n&&(n.innerHTML=""),r&&t&&(t.classList.add("hidden"),r.classList.remove("hidden"),r.classList.add("flex")),e},t.handleCommonResponse=r,t.responseSuccessInterceptor=function(e){var t,n,o,s;if(r(),null===(n=null===(t=null==e?void 0:e.data)||void 0===t?void 0:t.details)||void 0===n?void 0:n.message){var a=document.getElementById("MESSAGEERROR");a&&(a.innerHTML=null===(s=null===(o=null==e?void 0:e.data)||void 0===o?void 0:o.details)||void 0===s?void 0:s.message)}return e},t.responseFailedInterceptor=function(e){var t,n;if(r(),null===(n=null===(t=e.response)||void 0===t?void 0:t.data)||void 0===n?void 0:n.details){var o=e.response.data.details;for(var s in o){var a=document.getElementById(s.toUpperCase()+"ERROR");a&&(a.innerHTML=a.getAttribute("data-label")+" "+o[s]),!a||"errorMessage"!==s&&"message"!==s||(a.innerHTML=""+o[s])}}return Promise.reject(e.response)}},312:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.routers=t.routerLinks=void 0,t.routerLinks={home:"/",loginForm:"/auth/login"},t.routers={category:{create:"/api/category",update:"/api/category/update"},post:{create:"/api/post",addNewTagToPost:"/api/post/tag",getTagOfPost:function(e){return"/api/post/tag?postId="+e},save:"/api/post/save",uploadImagePost:"/api/post/image"},user:{changePassword:"/api/user/change-password",update:"/api/user",get:"/api/user"},tag:{getAll:"/api/tag/all",getByName:function(e){return"/api/tag?name="+e}},auth:{login:"/api/auth/login",register:"/api/auth/register"}}}},o={};function s(e){var t=o[e];if(void 0!==t)return t.exports;var r=o[e]={exports:{}};return n[e](r,r.exports,s),r.exports}e=s(284),t=s(312),null==(r=document.getElementById("registerForm"))||r.addEventListener("submit",(function(r){r.preventDefault();var n=document.getElementById("username"),o=document.getElementById("password"),s=document.getElementById("name"),a=document.getElementById("confirmPassword");if(null!==n&&null!==o&&null!==s&&null!==a){var i={username:n.value,password:o.value,name:s.value,confirmPassword:a.value};e.http.post(t.routers.auth.register,i).then((function(){return window.location.assign(t.routerLinks.loginForm)}))}else console.log("register form wrong")}))})();