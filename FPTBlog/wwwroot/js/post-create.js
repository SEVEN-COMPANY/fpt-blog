(()=>{"use strict";var e={284:(e,t,o)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.http=void 0;var s=o(603),a=axios;t.http=a,a.defaults.withCredentials=!0,a.interceptors.request.use(s.requestInterceptor),a.interceptors.response.use(s.responseSuccessInterceptor,s.responseFailedInterceptor)},603:(e,t,o)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.responseFailedInterceptor=t.responseSuccessInterceptor=t.handleCommonResponse=t.requestInterceptor=void 0;var s=o(973);function a(){var e=document.getElementById("form-btn"),t=document.getElementById("loading");t&&e&&(e.classList.remove("hidden"),e.classList.add("block"),t.classList.add("hidden"),t.classList.remove("flex"))}t.requestInterceptor=function(e){var t=document.getElementById("form-btn"),o=document.getElementById("loading"),s=document.getElementById("MESSAGEERROR"),a=document.getElementById("ERRORMESSAGEERROR");for(var r in e.data){var n=document.getElementById(r.toUpperCase()+"ERROR");n&&(n.innerHTML="")}return a&&(a.innerHTML=""),s&&(s.innerHTML=""),o&&t&&(t.classList.add("hidden"),o.classList.remove("hidden"),o.classList.add("flex")),e},t.handleCommonResponse=a,t.responseSuccessInterceptor=function(e){var t,o,r,n,i,d;if(a(),null===(o=null===(t=null==e?void 0:e.data)||void 0===t?void 0:t.details)||void 0===o?void 0:o.message){var l=document.getElementById("MESSAGEERROR");if(l){var p=null===(n=null===(r=null==e?void 0:e.data)||void 0===r?void 0:r.details)||void 0===n?void 0:n.message;l.innerHTML=p.slice(0,1).toUpperCase()+p.slice(1,p.length)}document.getElementById("toastify")&&(p=null===(d=null===(i=null==e?void 0:e.data)||void 0===i?void 0:i.details)||void 0===d?void 0:d.message,(0,s.toastify)({text:p.slice(0,1).toUpperCase()+p.slice(1,p.length),duration:2e3,newWindow:!0,close:!0,gravity:"top",position:"right",backgroundColor:"#fa983a",stopOnFocus:!0}))}return e},t.responseFailedInterceptor=function(e){var t,o;if(a(),null===(o=null===(t=e.response)||void 0===t?void 0:t.data)||void 0===o?void 0:o.details){var r=e.response.data.details;for(var n in r){var i=document.getElementById(n.toUpperCase()+"ERROR");if(i&&(i.innerHTML=i.getAttribute("data-label")+" "+r[n]),i&&("errorMessage"===n||"message"===n)){var d=""+r[n];i.innerHTML=d.slice(0,1).toUpperCase()+d.slice(1,d.length),document.getElementById("toastify")&&(0,s.toastify)({text:d.slice(0,1).toUpperCase()+d.slice(1,d.length),duration:2e3,newWindow:!0,close:!0,gravity:"top",position:"right",backgroundColor:"rgb(239, 68, 68)",stopOnFocus:!0})}}}return Promise.reject(e.response)}},312:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.routers=t.routerLinks=void 0,t.routerLinks={home:"/",loginForm:"/auth/login"},t.routers={category:{create:"/api/category",update:"/api/category"},post:{create:"/api/post",addNewTagToPost:"/api/post/tag",getTagOfPost:function(e){return"/api/post/tag?postId="+e},save:"/api/post/save",uploadImagePost:"/api/post/image",addCategoryToPost:"/api/post/category",likePost:"/api/post/like",dislikePost:"/api/post/dislike",sendPost:"/api/post/send",approvedPost:"/api/admin/post/approved",deletePost:"/api/post/delete"},user:{changePassword:"/api/user/change-password",update:"/api/user",get:"/api/user",status:"/api/admin/user/status",role:"/api/admin/user/role",follow:"/api/user/follow"},tag:{getAll:"/api/tag/all",clearUnused:"/api/tag/unused",getByName:function(e){return"/api/tag?name="+e}},auth:{login:"/api/auth/login",register:"/api/auth/register"},reward:{create:"/api/reward",update:"/api/reward"}}},973:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.toastify=void 0,t.toastify=function(e){return Toastify(e).showToast()}}},t={};function o(s){var a=t[s];if(void 0!==a)return a.exports;var r=t[s]={exports:{}};return e[s](r,r.exports,o),r.exports}(()=>{var e=o(284),t=o(312),s=document.getElementById("createNewPost");null==s||s.addEventListener("click",(function(o){e.http.post(t.routers.post.create).then((function(e){window.location.reload()}))}));for(var a=document.getElementsByClassName("delete-post"),r=function(o){var s=a[o];s.addEventListener("click",(function(){if(confirm("Are you sure to delete this draft?")){var o=s.getAttribute("data-postId");e.http.put(t.routers.post.deletePost,{PostId:o}).then((function(e){setTimeout((function(){window.location.reload()}),1e3)}))}}))},n=0;n<a.length;n++)r(n)})()})();