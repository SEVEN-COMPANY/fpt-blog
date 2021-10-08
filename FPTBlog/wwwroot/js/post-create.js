(()=>{"use strict";var e={284:(e,t,o)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.http=void 0;var s=o(603),n=axios;t.http=n,n.defaults.withCredentials=!0,n.interceptors.request.use(s.requestInterceptor),n.interceptors.response.use(s.responseSuccessInterceptor,s.responseFailedInterceptor)},603:(e,t,o)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.responseFailedInterceptor=t.responseSuccessInterceptor=t.handleCommonResponse=t.requestInterceptor=void 0;var s=o(973);function n(){var e=document.getElementById("form-btn"),t=document.getElementById("loading");t&&e&&(e.classList.remove("hidden"),e.classList.add("block"),t.classList.add("hidden"),t.classList.remove("flex"))}t.requestInterceptor=function(e){var t=document.getElementById("form-btn"),o=document.getElementById("loading"),s=document.getElementById("MESSAGEERROR"),n=document.getElementById("ERRORMESSAGEERROR");for(var r in e.data){var a=document.getElementById(r.toUpperCase()+"ERROR");a&&(a.innerHTML="")}return n&&(n.innerHTML=""),s&&(s.innerHTML=""),o&&t&&(t.classList.add("hidden"),o.classList.remove("hidden"),o.classList.add("flex")),e},t.handleCommonResponse=n,t.responseSuccessInterceptor=function(e){var t,o,r,a,i,d;if(n(),null===(o=null===(t=null==e?void 0:e.data)||void 0===t?void 0:t.details)||void 0===o?void 0:o.message){var l=document.getElementById("MESSAGEERROR");l&&(l.innerHTML=null===(a=null===(r=null==e?void 0:e.data)||void 0===r?void 0:r.details)||void 0===a?void 0:a.message),document.getElementById("toastify")&&(0,s.toastify)({text:null===(d=null===(i=null==e?void 0:e.data)||void 0===i?void 0:i.details)||void 0===d?void 0:d.message,duration:2e3,newWindow:!0,close:!0,gravity:"top",position:"right",backgroundColor:"#F37124",stopOnFocus:!0})}return e},t.responseFailedInterceptor=function(e){var t,o;if(n(),null===(o=null===(t=e.response)||void 0===t?void 0:t.data)||void 0===o?void 0:o.details){var r=e.response.data.details;for(var a in r){var i=document.getElementById(a.toUpperCase()+"ERROR");i&&(i.innerHTML=i.getAttribute("data-label")+" "+r[a]),!i||"errorMessage"!==a&&"message"!==a||(i.innerHTML=""+r[a],document.getElementById("toastify")&&(0,s.toastify)({text:""+r[a],duration:2e3,newWindow:!0,close:!0,gravity:"top",position:"right",backgroundColor:"rgb(239, 68, 68)",stopOnFocus:!0}))}}return Promise.reject(e.response)}},312:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.routers=t.routerLinks=void 0,t.routerLinks={home:"/",loginForm:"/auth/login"},t.routers={category:{create:"/api/category",update:"/api/category"},post:{create:"/api/post",addNewTagToPost:"/api/post/tag",getTagOfPost:function(e){return"/api/post/tag?postId="+e},save:"/api/post/save",uploadImagePost:"/api/post/image",addCategoryToPost:"/api/post/category",likePost:"/api/post/like",dislikePost:"/api/post/dislike",sendPost:"/api/post/send",deletePost:"/api/post/delete"},user:{changePassword:"/api/user/change-password",update:"/api/user",get:"/api/user",status:"/api/admin/user/status",role:"/api/admin/user/role"},tag:{getAll:"/api/tag/all",getByName:function(e){return"/api/tag?name="+e}},auth:{login:"/api/auth/login",register:"/api/auth/register"}}},973:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.toastify=void 0,t.toastify=function(e){return Toastify(e).showToast()}}},t={};function o(s){var n=t[s];if(void 0!==n)return n.exports;var r=t[s]={exports:{}};return e[s](r,r.exports,o),r.exports}(()=>{var e=o(284),t=o(312),s=document.getElementById("createNewPost");null==s||s.addEventListener("click",(function(o){e.http.post(t.routers.post.create).then((function(e){window.location.reload()}))}));for(var n=document.getElementsByClassName("delete-post"),r=function(o){var s=n[o];s.addEventListener("click",(function(){if(confirm("Are you sure to delete this draft?")){var o=s.getAttribute("data-postId");e.http.put(t.routers.post.deletePost,{PostId:o}).then((function(e){setTimeout((function(){window.location.reload()}),1e3)}))}}))},a=0;a<n.length;a++)r(a)})()})();