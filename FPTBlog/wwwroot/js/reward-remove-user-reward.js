(()=>{"use strict";var e={284:(e,t,a)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.http=void 0;var s=a(603),n=axios;t.http=n,n.defaults.withCredentials=!0,n.interceptors.request.use(s.requestInterceptor),n.interceptors.response.use(s.responseSuccessInterceptor,s.responseFailedInterceptor)},603:(e,t,a)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.responseFailedInterceptor=t.responseSuccessInterceptor=t.handleCommonResponse=t.requestInterceptor=void 0;var s=a(973);function n(){var e=document.getElementById("form-btn"),t=document.getElementById("loading");t&&e&&(e.classList.remove("hidden"),e.classList.add("block"),t.classList.add("hidden"),t.classList.remove("flex"))}t.requestInterceptor=function(e){var t=document.getElementById("form-btn"),a=document.getElementById("loading"),s=document.getElementById("MESSAGEERROR"),n=document.getElementById("ERRORMESSAGEERROR");for(var o in e.data){var r=document.getElementById(o.toUpperCase()+"ERROR");r&&(r.innerHTML="")}return n&&(n.innerHTML=""),s&&(s.innerHTML=""),a&&t&&(t.classList.add("hidden"),a.classList.remove("hidden"),a.classList.add("flex")),e},t.handleCommonResponse=n,t.responseSuccessInterceptor=function(e){var t,a,o,r,l,i;if(n(),null===(a=null===(t=null==e?void 0:e.data)||void 0===t?void 0:t.details)||void 0===a?void 0:a.message){var d=document.getElementById("MESSAGEERROR");if(d){var c=null===(r=null===(o=null==e?void 0:e.data)||void 0===o?void 0:o.details)||void 0===r?void 0:r.message;d.innerHTML=c.slice(0,1).toUpperCase()+c.slice(1,c.length)}document.getElementById("toastify")&&(c=null===(i=null===(l=null==e?void 0:e.data)||void 0===l?void 0:l.details)||void 0===i?void 0:i.message,(0,s.toastify)({text:c.slice(0,1).toUpperCase()+c.slice(1,c.length),duration:2e3,newWindow:!0,close:!0,gravity:"top",position:"right",backgroundColor:"#fa983a",stopOnFocus:!0}))}return e},t.responseFailedInterceptor=function(e){var t,a;if(n(),null===(a=null===(t=e.response)||void 0===t?void 0:t.data)||void 0===a?void 0:a.details){var o=e.response.data.details;for(var r in o){var l=document.getElementById(r.toUpperCase()+"ERROR");if(l&&(l.innerHTML=l.getAttribute("data-label")+" "+o[r]),l&&("errorMessage"===r||"message"===r)){var i=""+o[r];l.innerHTML=i.slice(0,1).toUpperCase()+i.slice(1,i.length),document.getElementById("toastify")&&(0,s.toastify)({text:i.slice(0,1).toUpperCase()+i.slice(1,i.length),duration:2e3,newWindow:!0,close:!0,gravity:"top",position:"right",backgroundColor:"rgb(239, 68, 68)",stopOnFocus:!0})}}}return Promise.reject(e.response)}},312:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.routers=t.routerLinks=void 0,t.routerLinks={home:"/",loginForm:"/auth/login"},t.routers={category:{create:"/api/category",update:"/api/category",chart:"/api/category/chart"},post:{create:"/api/post",addNewTagToPost:"/api/post/tag",getTagOfPost:function(e){return"/api/post/tag?postId="+e},save:"/api/post/save",uploadImagePost:"/api/post/image",addCategoryToPost:"/api/post/category",likePost:"/api/post/like",dislikePost:"/api/post/dislike",sendPost:"/api/post/send",approvedPost:"/api/admin/post/approved",deletePost:"/api/post/delete"},user:{changePassword:"/api/user/change-password",update:"/api/user",get:"/api/user",chart:"/api/admin/user/chart",status:"/api/admin/user/status",role:"/api/admin/user/role",follow:"/api/user/follow"},tag:{getAll:"/api/tag/all",clearUnused:"/api/tag/unused",getByName:function(e){return"/api/tag?name="+e}},auth:{login:"/api/auth/login",register:"/api/auth/register"},reward:{create:"/api/reward",update:"/api/reward",getOne:"/api/reward",give:"/api/reward/give",delete:"/api/reward/delete",removeUserReward:"/api/reward/remove"},comment:{create:"/api/comment",getComment:"/api/comment/post",deleteComment:"/api/comment/delete",updateComment:"/api/comment"}}},973:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.toastify=void 0,t.toastify=function(e){return Toastify(e).showToast()}}},t={};function a(s){var n=t[s];if(void 0!==n)return n.exports;var o=t[s]={exports:{}};return e[s](o,o.exports,a),o.exports}(()=>{for(var e=a(284),t=a(312),s=document.getElementById("modal-btn-close-remove"),n=document.getElementById("modal-wrapper-remove"),o=document.getElementById("modal-bg-remove"),r=document.getElementById("modal-panel-remove"),l=document.getElementById("modal-btn-accept-remove"),i=document.getElementById("modal-btn-cancel-remove"),d=document.getElementsByTagName("tr"),c=null,u=null,p=function(){null==n||n.classList.add("invisible")},m=0;m<d.length;m++)for(var v=d[m].getElementsByClassName("modal-btn"),g=function(e){var t=v[e];console.log(t),t&&t.addEventListener("click",(function(){null==n||n.classList.remove("invisible"),null==o||o.classList.add("opacity-100"),null==o||o.classList.remove("opacity-0"),null==r||r.classList.add("opacity-100","translate-y-0","sm:scale-100"),null==r||r.classList.remove("opacity-0","translate-y-4","sm:translate-y-0","sm:scale-95"),null==r||r.removeEventListener("transitionend",p),c=t.getAttribute("data-userId"),u=t.getAttribute("data-rewardId")}))},y=0;y<v.length;y++)g(y);null==l||l.addEventListener("click",(function(){if(null==o||o.classList.remove("opacity-100"),null==o||o.classList.add("opacity-0"),null==r||r.classList.remove("opacity-100","translate-y-0","sm:scale-100"),null==r||r.classList.add("opacity-0","translate-y-4","sm:translate-y-0","sm:scale-95"),null==r||r.addEventListener("transitionend",p),null!==c&&null!==u){var a={userId:c,rewardId:u};e.http.put(t.routers.reward.removeUserReward,a).then((function(){window.location.reload()}))}})),null==s||s.addEventListener("click",(function(){null==o||o.classList.remove("opacity-100"),null==o||o.classList.add("opacity-0"),null==r||r.classList.remove("opacity-100","translate-y-0","sm:scale-100"),null==r||r.classList.add("opacity-0","translate-y-4","sm:translate-y-0","sm:scale-95"),null==r||r.addEventListener("transitionend",p)})),null==i||i.addEventListener("click",(function(){null==o||o.classList.remove("opacity-100"),null==o||o.classList.add("opacity-0"),null==r||r.classList.remove("opacity-100","translate-y-0","sm:scale-100"),null==r||r.classList.add("opacity-0","translate-y-4","sm:translate-y-0","sm:scale-95"),null==r||r.addEventListener("transitionend",p)}))})()})();