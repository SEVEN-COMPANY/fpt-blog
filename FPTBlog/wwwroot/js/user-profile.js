(()=>{"use strict";var e,t,o,a={284:(e,t,o)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.http=void 0;var a=o(603),r=axios;t.http=r,r.defaults.withCredentials=!0,r.interceptors.request.use(a.requestInterceptor),r.interceptors.response.use(a.responseSuccessInterceptor,a.responseFailedInterceptor)},603:(e,t,o)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.responseFailedInterceptor=t.responseSuccessInterceptor=t.handleCommonResponse=t.requestInterceptor=void 0;var a=o(973);function r(){var e=document.getElementById("form-btn"),t=document.getElementById("loading");t&&e&&(e.classList.remove("hidden"),e.classList.add("block"),t.classList.add("hidden"),t.classList.remove("flex"))}t.requestInterceptor=function(e){var t=document.getElementById("form-btn"),o=document.getElementById("loading"),a=document.getElementById("MESSAGEERROR"),r=document.getElementById("ERRORMESSAGEERROR");for(var s in e.data){var i=document.getElementById(s.toUpperCase()+"ERROR");i&&(i.innerHTML="")}return r&&(r.innerHTML=""),a&&(a.innerHTML=""),o&&t&&(t.classList.add("hidden"),o.classList.remove("hidden"),o.classList.add("flex")),e},t.handleCommonResponse=r,t.responseSuccessInterceptor=function(e){var t,o,s,i,n,d;if(r(),null===(o=null===(t=null==e?void 0:e.data)||void 0===t?void 0:t.details)||void 0===o?void 0:o.message){var l=document.getElementById("MESSAGEERROR");if(l){var p=null===(i=null===(s=null==e?void 0:e.data)||void 0===s?void 0:s.details)||void 0===i?void 0:i.message;l.innerHTML=p.slice(0,1).toUpperCase()+p.slice(1,p.length)}document.getElementById("toastify")&&(p=null===(d=null===(n=null==e?void 0:e.data)||void 0===n?void 0:n.details)||void 0===d?void 0:d.message,(0,a.toastify)({text:p.slice(0,1).toUpperCase()+p.slice(1,p.length),duration:4e3,newWindow:!0,close:!0,gravity:"top",position:"right",backgroundColor:"#4CAF50",avatar:"/images/check.svg",stopOnFocus:!0}))}return e},t.responseFailedInterceptor=function(e){var t,o;if(r(),null===(o=null===(t=e.response)||void 0===t?void 0:t.data)||void 0===o?void 0:o.details){var s=e.response.data.details;for(var i in s){var n=document.getElementById(i.toUpperCase()+"ERROR");if(n&&(n.innerHTML=n.getAttribute("data-label")+" "+s[i]),n&&("errorMessage"===i||"message"===i)){var d=""+s[i];n.innerHTML=d.slice(0,1).toUpperCase()+d.slice(1,d.length),document.getElementById("toastify")&&(0,a.toastify)({text:d.slice(0,1).toUpperCase()+d.slice(1,d.length),duration:4e3,newWindow:!0,close:!0,gravity:"top",position:"right",avatar:"/images/minus.svg",backgroundColor:"#F44336",stopOnFocus:!0})}}}return Promise.reject(e.response)}},312:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.routers=t.routerLinks=void 0,t.routerLinks={home:"/",loginForm:"/auth/login"},t.routers={category:{create:"/api/category",update:"/api/category",chart:"/api/category/chart"},post:{create:"/api/post",chart:"/api/admin/post/chart",addNewTagToPost:"/api/post/tag",getTagOfPost:function(e){return"/api/post/tag?postId="+e},save:"/api/post/save",uploadImagePost:"/api/post/image",addCategoryToPost:"/api/post/category",likePost:"/api/post/like",dislikePost:"/api/post/dislike",sendPost:"/api/post/send",approvedPost:"/api/admin/post/approved",deletePost:"/api/post/delete"},user:{changePassword:"/api/user/change-password",update:"/api/user",get:"/api/user",chart:"/api/admin/user/chart",status:"/api/admin/user/status",role:"/api/admin/user/role",follow:"/api/user/follow"},tag:{getAll:"/api/tag/all",chart:"/api/tag/chart",clearUnused:"/api/tag/unused",getByName:function(e){return"/api/tag?name="+e}},auth:{login:"/api/auth/login",register:"/api/auth/register"},reward:{create:"/api/reward",update:"/api/reward",getOne:"/api/reward",give:"/api/reward/give",delete:"/api/reward/delete",removeUserReward:"/api/reward/remove"},comment:{create:"/api/comment",getComment:"/api/comment/post",deleteComment:"/api/comment/delete",updateComment:"/api/comment",likeComment:"/api/comment/like",dislikeComment:"/api/comment/dislike"}}},973:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.toastify=void 0,t.toastify=function(e){return Toastify(e).showToast()}}},r={};function s(e){var t=r[e];if(void 0!==t)return t.exports;var o=r[e]={exports:{}};return a[e](o,o.exports,s),o.exports}e=s(284),t=s(312),null==(o=document.getElementById("follow-btn"))||o.addEventListener("click",(function(){var a=o.getAttribute("data-userId");a&&e.http.post(t.routers.user.follow,{followerId:a})}))})();