const mix = require("laravel-mix");
const tailwindcss = require("tailwindcss");
const buildPath = "./../wwwroot/ui/admin";
/*
 |--------------------------------------------------------------------------
 | Mix Asset Management
 |--------------------------------------------------------------------------
 |
 | Mix provides a clean, fluent API for defining some Webpack build steps
 | for your Laravel applications. By default, we are compiling the CSS
 | file for the application as well as bundling up all the JS files.
 |
 */

mix
  .js("src/js/app.js", buildPath)
  .js("src/js/ckeditor-classic.js", buildPath)
  .js("src/js/ckeditor-inline.js", buildPath)
  .js("src/js/ckeditor-balloon.js", buildPath)
  .js("src/js/ckeditor-balloon-block.js", buildPath)
  .js("src/js/ckeditor-document.js", buildPath)
  .sass("src/sass/app.scss", buildPath)
  .options({
    processCssUrls: false,
    postCss: [tailwindcss("./tailwind.config.js")],
  })
  .autoload({
    "cash-dom": ["cash"],
  })
  .copyDirectory("src/fonts", buildPath)
  .browserSync({
    proxy: "tinker-html.test",
    files: ["src/**/*.*"],
  });
